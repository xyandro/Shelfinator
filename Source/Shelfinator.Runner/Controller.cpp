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
		Controller::ptr Controller::Create(IDotStar::ptr dotStar, IAudio::ptr audio)
		{
			return ptr(new Controller(dotStar, audio));
		}

		Controller::Controller(IDotStar::ptr dotStar, IAudio::ptr audio)
		{
			this->dotStar = dotStar;
			this->audio = audio;
			remoteTimer = Timer::Create();
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
			if ((selectedNumber != -1) && (remoteTimer->Elapsed() >= 1000))
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
				paused = !paused;
				banner = Banner::Create(paused ? L"‖" : L"▶", 0, 1000);
				break;
			case Rewind: audio->Play(audio->GetTime() - 5000); break;
			case FastForward: audio->Play(audio->GetTime() + 5000); break;
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
			audio->Open(song->SongFileName());
			audio->Play();
			fprintf(stderr, "Playing song %s\n", song->FileName.c_str());
		}

		void Controller::Run(int *songNumbers, int songNumberCount, bool startPaused)
		{
			int frameCount = 0;

			if (songNumberCount == 0)
				songs->MakeFirst(1); // Hello
			else
				for (auto ctr = songNumberCount - 1; ctr >= 0; --ctr)
					songs->MakeFirst(songNumbers[ctr]);

			paused = startPaused;

			auto startDraw = Timer::Create();
			auto driver = Driver::Create(dotStar);
			while (running)
			{
				if (HandleRemote())
					continue;

				auto lights = Lights::Create();
				if ((banner) && (banner->Expired()))
					banner.reset();

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

				if (banner)
					banner->SetLights(lights);

				driver->SetLights(lights);

				if (++frameCount == 100)
					fprintf(stderr, "FPS is %f.\n", (double)frameCount / startDraw->Elapsed() * 1000);
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

			std::unique_lock<std::mutex> lock(remoteMutex);
			if ((remoteCode != lastRemoteCode) || (remoteTimer->Elapsed() >= REMOTEDELAYMS))
			{
				remoteCodes.push(remoteCode);
				remoteTimer->Restart();
				lastRemoteCode = remoteCode;
			}
		}
	}
}
