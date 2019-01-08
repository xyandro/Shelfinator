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

		Controller::ptr Controller::Create(IDotStar::ptr dotStar, IAudio::ptr audio)
		{
			return ptr(new Controller(dotStar, audio));
		}

		Controller::Controller(IDotStar::ptr dotStar, IAudio::ptr audio)
		{
			this->dotStar = dotStar;
			this->audio = audio;
			timer = Timer::Create();
			songs = Songs::Create();
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
			case Play:
			case Pause:
				banner = Banner::Create(L"▶", 0, 1000);
				paused = !paused;
				break;
			case Rewind: audio->SetTime(audio->GetTime() - 5000); break;
			case FastForward: audio->SetTime(audio->GetTime() + 5000); break;
			case Previous:
				if (audio->GetTime() < 2000)
					--songIndex;
				--songIndex;
				audio->Stop();
				break;
			case Next:
				audio->Stop();
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
				if (selectedNumber == -1)
					selectedNumber = 0;
				selectedNumber = selectedNumber * 10 + code - D0;
				if (selectedNumber >= 10000)
					selectedNumber = -1;
				else
					banner = Banner::Create(std::to_wstring(selectedNumber), 0, 1000, 1);
				break;
			case Info: banner = Banner::Create(std::to_wstring(songs->GetValue(songIndex)), 0, 1000, 1); break;
			default: result = false; break;
			}

			if ((useSelectedNumber) && (selectedNumber != -1))
			{
				auto found = songs->GetIndex(selectedNumber);
				if (found != -1)
				{
					songIndex = found;
					LoadSong();
				}

				selectedNumber = -1;
			}

			return result;
		}

		void Controller::LoadSong(bool startAtEnd)
		{
			while (songIndex < 0)
				songIndex += songs->Count();
			while (songIndex >= songs->Count())
				songIndex -= songs->Count();

			song = songs->LoadSong(songIndex);
			audio->Play(song->SongFileName());
			fprintf(stderr, "Playing song %s\n", song->FileName.c_str());
		}

		int frameCount = 0;
		void Controller::Run(int *songNumbers, int songNumberCount, bool startPaused)
		{
			if (songNumberCount == 0)
				songs->MakeFirst(1); // Hello
			else
				for (auto ctr = songNumberCount - 1; ctr >= 0; --ctr)
					songs->MakeFirst(songNumbers[ctr]);

			paused = startPaused;

			auto startDraw = timer->Millis();
			auto driver = Driver::Create(dotStar);
			while (running)
			{
				if (HandleRemote())
					continue;

				auto lights = Lights::Create();
				if (banner)
				{
					if (banner->Expired())
						banner.reset();
					else
						banner->SetLights(lights);
				}

				auto time = audio->GetTime();
				if (time == -1)
				{
					if (paused)
					{
						std::this_thread::sleep_for(std::chrono::milliseconds(100));
						if (!banner)
							banner = Banner::Create(L"•   ", 0, 5000);
					}
					else
					{
						++songIndex;
						LoadSong(false);
					}
				}
				else
					song->SetLights(time, 1, lights);

				driver->SetLights(lights);

				if (++frameCount == 100)
				{
					auto drawTime = timer->Millis() - startDraw;
					auto fps = (double)frameCount / drawTime * 1000;
					fprintf(stderr, "FPS is %f.\n", fps);
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
