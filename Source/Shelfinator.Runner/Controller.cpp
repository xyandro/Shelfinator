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
		Controller::ptr Controller::Create(IDotStar::ptr dotStar, IAudio::ptr audio, int *songNumbers, int songNumberCount)
		{
			return ptr(new Controller(dotStar, audio, songNumbers, songNumberCount));
		}

		Controller::Controller(IDotStar::ptr dotStar, IAudio::ptr audio, int *songNumbers, int songNumberCount)
		{
			this->dotStar = dotStar;
			this->audio = audio;
			remoteTimer = Timer::Create();

			songs = Songs::Create();
			if (songNumberCount == 0)
				songs->MakeFirst(1); // Hello
			else
				for (auto ctr = songNumberCount - 1; ctr >= 0; --ctr)
					songs->MakeFirst(songNumbers[ctr]);
		}

		Controller::~Controller()
		{
			Stop();
		}

		void Controller::HandleRemote()
		{
			while (true)
			{
				auto useSelectedSong = false;
				if ((selectedSong != -1) && (remoteTimer->Elapsed() >= 1000))
					useSelectedSong = true;

				auto done = true;
				auto code = None;
				{
					std::unique_lock<std::mutex> lock(remoteMutex);
					if (!remoteCodes.empty())
					{
						code = remoteCodes.front();
						remoteCodes.pop();
						done = false;
					}
				}

				switch (code)
				{
				case Play:
				case Pause:
					if (audio->Playing())
					{
						audio->Stop();
						banner = Banner::Create(L"‖", 0, 1000);
					}
					else
					{
						audio->Play();
						banner = Banner::Create(L"▶", 0, 1000);
					}
					break;
				case Rewind: audio->SetTime(audio->GetTime() - 5000); break;
				case FastForward: audio->SetTime(audio->GetTime() + 5000); break;
				case Previous:
					if (audio->GetTime() >= 2000)
					{
						audio->SetTime(0);
						break;
					}

					--songIndex;
					LoadSong();
					break;
				case Next:
					++songIndex;
					LoadSong();
					break;
				case Info: banner = Banner::Create(std::to_wstring(songs->GetValue(songIndex)), 0, 1000, 1); break;
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
					if (selectedSong == -1)
						selectedSong = 0;
					selectedSong = selectedSong * 10 + code - D0;
					if (selectedSong >= 10000)
						selectedSong = -1;
					else
						banner = Banner::Create(std::to_wstring(selectedSong), 0, 1000, 1);
					break;
				case Enter: useSelectedSong = true; break;
				}

				if (useSelectedSong)
				{
					auto found = songs->GetIndex(selectedSong);
					if (found != -1)
					{
						songIndex = found;
						LoadSong();
					}

					selectedSong = -1;
				}

				if (done)
					break;
			}
		}

		void Controller::LoadSong(bool play)
		{
			audio->Stop();

			while (songIndex < 0)
				songIndex += songs->Count();
			while (songIndex >= songs->Count())
				songIndex -= songs->Count();

			song = songs->LoadSong(songIndex);
			audio->Open(song->SongFileName());
			if (play)
				audio->Play();
			fprintf(stderr, "Playing song %s\n", song->FileName.c_str());
			banner.reset();
		}

		void Controller::Run(bool startPaused)
		{
			auto driver = Driver::Create(dotStar);
			while (running)
			{
				if ((banner) && (banner->Expired()))
					banner.reset();

				HandleRemote();

				if (audio->Finished())
				{
					++songIndex;
					LoadSong(!startPaused);
					if (startPaused)
					{
						startPaused = false;
						banner = Banner::Create(L"•   ", 0, 86400000);
					}
				}

				auto lights = Lights::Create();
				if (song)
					song->SetLights(audio->GetTime(), 1, lights);
				if (banner)
					banner->SetLights(lights);
				driver->SetLights(lights);
			}
			driver->SetLights(Lights::Create());
			audio->Close();
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
