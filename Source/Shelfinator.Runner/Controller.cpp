#include "Controller.h"

#include <string.h>
#include <thread>
#include "Driver.h"
#include "Helpers.h"

#define REMOTEDELAYMS 250

namespace Shelfinator
{
	namespace Runner
	{
		const double Controller::multipliers[] = { -4, -3, -2, -1, -0.75, -0.5, -0.25, -0.125, 0, 0.125, 0.25, 0.5, 0.75, 1, 2, 3, 4 };
		std::wstring Controller::multiplierNames[] = { L"◀◀◀◀", L"◀◀◀", L"◀◀", L"◀", L"◀3/4", L"◀1/2", L"◀1/4", L"◀1/8", L"‖", L"1/8▶", L"1/4▶", L"1/2▶", L"3/4▶", L"▶", L"▶▶", L"▶▶▶", L"▶▶▶▶" };

		Controller::ptr Controller::Create(IDotStar::ptr dotStar)
		{
			return ptr(new Controller(dotStar));
		}

		Controller::Controller(IDotStar::ptr dotStar)
		{
			this->dotStar = dotStar;
			timer = Timer::Create();
			patterns = Patterns::Create();
		}

		Controller::~Controller()
		{
			Stop();
		}

		bool Controller::HandleRemote()
		{
			auto result = true;
			auto useSelectedNumber = false;
			auto now = timer->Millis();
			if ((selectedNumber != -1) && (now - lastRemoteTime >= 1000))
				useSelectedNumber = true;

			auto lastMultiplierIndex = multiplierIndex;
			auto lastBrightness = brightness;

			auto code = None;
			{
				std::unique_lock<std::mutex> lock(remoteMutex);
				if (!remoteCodes.empty())
				{
					code = remoteCodes.front();
					remoteCodes.pop();
				}
			}

			switch (code)
			{
			case Play: multiplierIndex = 13; break;
			case Pause: multiplierIndex = 8; break;
			case Rewind:
				if (multiplierIndex > 0)
					--multiplierIndex;
				break;
			case FastForward:
				if (multiplierIndex < sizeof(multipliers) / sizeof(*multipliers) - 1)
					++multiplierIndex;
				break;
			case Previous:
				if ((multipliers[multiplierIndex] == 0) || (time / multipliers[multiplierIndex] < 2000))
				{
					--patternIndex;
					LoadPattern();
				}
				else
					time = 0;
				break;
			case Next:
				++patternIndex;
				LoadPattern();
				break;
			case Enter: useSelectedNumber = true; break;
			case VolumeUp: brightness += 10; break;
			case VolumeDown: brightness -= 10; break;
			case D0:
			case D1:
			case D2:
			case D3:
			case D4:
			case D5:
			case D6:
			case D7:
			case D8:
			case D9:
				if (selectedNumber == -1)
					selectedNumber = 0;
				selectedNumber = selectedNumber * 10 + code - D0;
				if (selectedNumber >= 10000)
					selectedNumber = -1;
				else
					banner = Banner::Create(std::to_wstring(selectedNumber), 0, 1000, 1);
				break;
			case Info: banner = Banner::Create(std::to_wstring(patterns->GetValue(patternIndex)), 0, 1000, 1); break;
			default: result = false; break;
			}

			if (lastMultiplierIndex != multiplierIndex)
				banner = Banner::Create(multiplierNames[multiplierIndex], 0, 1000);

			if (brightness < 0)
				brightness = 0;
			if (lastBrightness != brightness)
				banner = Banner::Create(std::to_wstring(brightness) + L'%', 0, 1000, 1);

			if ((useSelectedNumber) && (selectedNumber != -1))
			{
				auto found = patterns->GetIndex(selectedNumber);
				if (found != -1)
				{
					patternIndex = found;
					LoadPattern();
				}

				selectedNumber = -1;
			}

			return result;
		}

		void Controller::LoadPattern(bool startAtEnd)
		{
			if (patterns->Count() == 0)
				pattern = Pattern::CreateTest();
			else
			{
				while (patternIndex < 0)
					patternIndex += patterns->Count();
				while (patternIndex >= patterns->Count())
					patternIndex -= patterns->Count();

				pattern = patterns->LoadPattern(patternIndex);
				fprintf(stderr, "Displaying pattern %s\n", pattern->FileName.c_str());
			}
			time = startAtEnd ? pattern->GetLength() - 1 : 0;
		}

		int frameCount = 0;
		void Controller::Run(int *patternNumbers, int patternNumberCount, bool startPaused)
		{
			multiplierIndex = startPaused ? 8 : 13;

			if (patternNumberCount == 0)
				patterns->MakeFirst(1); // Hello
			else
				for (auto ctr = patternNumberCount - 1; ctr >= 0; --ctr)
					patterns->MakeFirst(patternNumbers[ctr]);

			auto startLoad = timer->Millis();
			LoadPattern();
			auto loadTime = timer->Millis() - startLoad;
			auto startDraw = timer->Millis();
			int startTime, nextTime = -1;
			auto driver = Driver::Create(dotStar);
			while (running)
			{
				startTime = nextTime;
				nextTime = -1;

				if (HandleRemote())
					continue;

				if ((time < 0) || (time >= pattern->GetLength()))
				{
					patternIndex += time < 0 ? -1 : 1;
					LoadPattern(time < 0);
					continue;
				}

				auto lights = Lights::Create();
				pattern->SetLights(time, brightness / 100.0, lights);
				if (banner)
					banner->SetLights(lights);
				driver->SetLights(lights);

				nextTime = timer->Millis();
				if (startTime != -1)
				{
					auto elapsed = nextTime - startTime;
					time += (int)(elapsed * multipliers[multiplierIndex]);
					if (banner)
					{
						banner->AddElapsed(elapsed);
						if (banner->Expired())
							banner.reset();
					}
				}

				if (++frameCount == 100)
				{
					auto drawTime = timer->Millis() - startDraw;
					auto fps = (double)frameCount / drawTime * 1000;
					fprintf(stderr, "Load time was %i. FPS is %f.\n", loadTime, fps);
				}
			}
			driver->SetLights(Lights::Create());
		}

		void Controller::Test(int firstLight, int lightCount, int concurrency, int delay, unsigned char brightness)
		{
			auto current = new int[lightCount];
			memset(current, 0, sizeof(*current) * lightCount);

			unsigned color = 0;
			auto set = 0, clear = -concurrency;
			auto driver = Driver::Create(dotStar);
			while (running)
			{
				if (color == 0)
					color = (unsigned)brightness << 16;
				current[set] = color;
				if (clear >= 0)
					current[clear] = 0;
				++set;
				if (set >= lightCount)
				{
					set = 0;
					color >>= 8;
				}
				++clear;
				if (clear >= lightCount)
					clear = 0;

				auto lights = Lights::Create();
				for (auto ctr = 0; ctr < lightCount; ++ctr)
					lights->SetLight(firstLight + ctr, current[ctr]);
				driver->SetLights(lights);
				std::this_thread::sleep_for(std::chrono::milliseconds(delay));
			}

			delete[] current;
			driver->SetLights(Lights::Create());
		}

		void Controller::TestAll(int lightCount, int delay, unsigned char brightness)
		{
			unsigned color = 0;
			auto driver = Driver::Create(dotStar);
			while (running)
			{
				color >>= 8;
				if (color == 0)
					color = (unsigned)brightness << 16;

				auto lights = Lights::Create();
				for (auto ctr = 0; ctr < lightCount; ++ctr)
					lights->SetLight(ctr, color);
				driver->SetLights(lights);

				std::this_thread::sleep_for(std::chrono::milliseconds(delay));
			}

			driver->SetLights(Lights::Create());
		}

		void Controller::Stop()
		{
			running = false;
		}

		void Controller::AddRemoteCode(RemoteCode remoteCode)
		{
			if (remoteCode == None)
				return;

			auto now = timer->Millis();
			std::unique_lock<std::mutex> lock(remoteMutex);
			if ((remoteCode != lastRemoteCode) || (now - lastRemoteTime >= REMOTEDELAYMS))
			{
				remoteCodes.push(remoteCode);
				lastRemoteTime = now;
				lastRemoteCode = remoteCode;
			}
		}
	}
}
