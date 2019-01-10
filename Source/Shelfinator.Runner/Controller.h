#pragma once

#ifndef __CLR_VER
#include <condition_variable>
#include <mutex>
#endif
#include <queue>
#include "Banner.h"
#include "IAudio.h"
#include "IDotStar.h"
#include "RemoteCode.h"
#include "Songs.h"
#include "Timer.h"

namespace Shelfinator
{
	namespace Runner
	{
		class Controller
		{
		public:
			typedef std::shared_ptr<Controller> ptr;
			static ptr Create(IDotStar::ptr dotStar, IAudio::ptr audio, int *songNumbers, int songNumberCount, bool startPaused);
			~Controller();
			void Run();
			void Stop();
			void AddRemoteCode(RemoteCode remoteCode);
		private:
			bool running = true, startPaused = false;
			int selectedSong = -1;
			RemoteCode lastRemoteCode = None;
			Banner::ptr banner;
			IDotStar::ptr dotStar;
			IAudio::ptr audio;
			SongData::Song::ptr song;
			Songs::ptr songs;
			Timer::ptr remoteTimer;

#ifndef __CLR_VER
			std::mutex remoteMutex;
#endif
			std::queue<RemoteCode> remoteCodes;

			Controller(IDotStar::ptr dotStar, IAudio::ptr audio, int *songNumbers, int songNumberCount, bool startPaused);
			void HandleRemote();
			void LoadSong();
			void PlayTestSong();
		};
	}
}
