﻿#pragma once

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
			static ptr Create(IDotStar::ptr dotStar, IAudio::ptr audio);
			~Controller();
			void Run(int *songNumbers, int songNumberCount, bool startPaused);
			void Test(int firstLight, int lightCount, int concurrency, int delay, unsigned char brightness);
			void TestAll(int lightCount, int delay, unsigned char brightness);
			void Stop();
			void AddRemoteCode(RemoteCode remoteCode);
		private:
			bool running = true, paused = false;
			int songIndex = -1, selectedNumber = -1;
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

			Controller(IDotStar::ptr dotStar, IAudio::ptr audio);
			bool HandleRemote();
			void LoadSong(bool startAtEnd = false);
		};
	}
}
