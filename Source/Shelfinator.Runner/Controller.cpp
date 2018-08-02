#include "Controller.h"

#include <Windows.h>
#include "Driver.h"
#include "Helpers.h"

namespace Shelfinator
{
	namespace Runner
	{
		const double Controller::multipliers[] = { -4, -3, -2, -1, -0.75, -0.5, -0.25, -0.125, 0, 0.125, 0.25, 0.5, 0.75, 1, 2, 3, 4 };
		std::string Controller::multiplierNames[] = { "RRRR", "RRR", "RR", "R", "R3/4", "R1/2", "R1/4", "R1/8", "P", "1/8F", "1/4F", "1/2F", "3/4F", "F", "FF", "FFF", "FFFF" };

		Controller::ptr Controller::Create(IDotStar::ptr dotStar, IRemote::ptr remote)
		{
			return ptr(new Controller(dotStar, remote));
		}

		Controller::Controller(IDotStar::ptr dotStar, IRemote::ptr remote)
		{
			this->dotStar = dotStar;
			this->remote = remote;
			timer = Timer::Create();
			patterns = Patterns::Create(Helpers::GetRunPath());
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
			if ((selectedNumberTime != -1) && (now - selectedNumberTime >= 1000))
				useSelectedNumber = true;

			auto lastMultiplierIndex = multiplierIndex;
			auto code = remote->GetCode();
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
				selectedNumberTime = now;
				selectedNumber = selectedNumber * 10 + code - D0;
				if (selectedNumber >= 10000)
				{
					selectedNumber = 0;
					selectedNumberTime = -1;
				}
				else
					banner = Banner::Create(std::to_string(selectedNumber), 1000, 1);
				break;
			case Info: banner = Banner::Create(std::to_string(patterns->GetValue(patternIndex)), 1000, 1); break;
			default: result = false; break;
			}

			if (lastMultiplierIndex != multiplierIndex)
				banner = Banner::Create(multiplierNames[multiplierIndex], 1000);

			if (useSelectedNumber)
			{
				auto found = patterns->GetIndex(selectedNumber);
				if (found != -1)
				{
					patternIndex = found;
					LoadPattern();
				}

				selectedNumber = 0;
				selectedNumberTime = -1;
			}

			return result;
		}

		void Controller::LoadPattern(bool startAtEnd)
		{
			while (patternIndex < 0)
				patternIndex += patterns->Count();
			while (patternIndex >= patterns->Count())
				patternIndex -= patterns->Count();

			pattern = patterns->LoadPattern(patternIndex);
			time = startAtEnd ? pattern->GetLength() - 1 : 0;
		}

		int frameCount = 0;
		void Controller::Run(int *patternNumbers, int patternNumberCount)
		{
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

				auto lights = Lights::Create(2440);
				pattern->SetLights(time, lights);
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

				if (++frameCount == 300)
				{
					auto drawTime = timer->Millis() - startDraw;
					auto fps = (double)frameCount / drawTime * 1000;
					fprintf(stderr, "Load time was %i. FPS is %f.\n", loadTime, fps);
				}
			}
		}

		void Controller::Test(int lightCount, int concurrency, int delay)
		{
			auto current = new int[lightCount];
			memset(current, 0, sizeof(*current) * lightCount);

			auto color = 0x0000ff;
			auto set = 0, clear = -concurrency;
			auto driver = Driver::Create(dotStar);
			while (running)
			{
				current[set] = color;
				if (clear >= 0)
					current[clear] = 0;
				++set;
				if (set >= lightCount)
				{
					set = 0;
					if (color == 0xff0000)
						color = 0x0000ff;
					else
						color <<= 8;
				}
				++clear;
				if (clear >= lightCount)
					clear = 0;

				auto lights = Lights::Create(2440);
				for (auto ctr = 0; ctr < lightCount; ++ctr)
					lights->SetLight(ctr, current[ctr]);
				driver->SetLights(lights);
				std::this_thread::sleep_for(std::chrono::milliseconds(delay));
			}

			delete current;
		}

		void Controller::Stop()
		{
			running = false;
		}
	}
}
