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
		Controller::ptr Controller::Create(IDotStar::ptr dotStar, IAudio::ptr audio, int *songNumbers, int songNumberCount, bool startPaused)
		{
			return ptr(new Controller(dotStar, audio, songNumbers, songNumberCount, startPaused));
		}

		Controller::Controller(IDotStar::ptr dotStar, IAudio::ptr audio, int *songNumbers, int songNumberCount, bool startPaused)
		{
			this->dotStar = dotStar;
			this->audio = audio;
			runStatus = startPaused ? TestPaused : Running;
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
					if (runStatus != Running)
					{
						runStatus = (code == Play) || (runStatus == Paused) ? Running : Paused;
						audio->Close();
						banner = Banner::Create(runStatus == Running ? L"▶" : L"‖", 0, 1000);
						break;
					}

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
				case Rewind: audio->SetTime(audio->GetTime() - 500); break;
				case FastForward: audio->SetTime(audio->GetTime() + 500); break;
				case Previous:
					if (audio->GetTime() >= 2000)
					{
						audio->SetTime(0);
						break;
					}

					songs->Move(-1);
					LoadSong();
					break;
				case Next:
					songs->Move(1);
					LoadSong();
					break;
				case Info: banner = Banner::Create(std::to_wstring(songs->CurrentSong()), 0, 1000, 1); break;
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
				case VolumeUp: audio->SetVolume(audio->GetVolume() + 1); break;
				case VolumeDown: audio->SetVolume(audio->GetVolume() - 1); break;
				case Edited:
					audio->SetEdited(!audio->GetEdited());
					banner = Banner::Create(audio->GetEdited() ? L"EDITED" : L"NORMAL", 500, 500, 1);
					break;
				}

				if (useSelectedSong)
				{
					if (songs->SetCurrent(selectedSong))
						LoadSong();

					selectedSong = -1;
				}

				if (done)
					break;
			}
		}

		void Controller::LoadSong()
		{
			runStatus = Running;
			audio->Stop();
			song = songs->LoadSong();
			audio->Open(song->NormalSongFileName(), song->EditedSongFileName());
			audio->Play();
			fprintf(stderr, "Playing song %s\n", song->FileName.c_str());
		}

		void Controller::PlayTestSong()
		{
			auto fileName = Helpers::GetRunPath() + "0.pat";
			song = SongData::Song::Read(fileName.c_str());
			audio->Open(song->NormalSongFileName(), song->EditedSongFileName());
			audio->Play();
		}

		void Controller::Run()
		{
			auto driver = Driver::Create(dotStar);
			while (running)
			{
				if ((banner) && (banner->Expired()))
					banner.reset();

				HandleRemote();

				if (audio->Finished())
				{
					switch (runStatus)
					{
					case TestPaused:
						PlayTestSong();
						break;
					case Running:
						songs->Move(1);
						LoadSong();
						break;
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
