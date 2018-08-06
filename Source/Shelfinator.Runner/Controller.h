#pragma once

#ifndef __CLR_VER
#include <mutex>
#endif
#include <queue>
#include "Banner.h"
#include "IDotStar.h"
#include "Patterns.h"
#include "RemoteCode.h"
#include "Timer.h"

namespace Shelfinator
{
	namespace Runner
	{
		class Controller
		{
		public:
			typedef std::shared_ptr<Controller> ptr;
			static ptr Create(IDotStar::ptr dotStar);
			~Controller();
			void Run(int *patternNumbers, int patternNumberCount);
			void Test(int firstLight, int lightCount, int concurrency, int delay, int brightness);
			void Stop();
			void AddRemoteCode(RemoteCode remoteCode);
		private:
			static const double multipliers[];
			static std::wstring multiplierNames[];

			bool running = true;
			int time = 0, multiplierIndex = 13, patternIndex = 0, selectedNumber = -1, lastRemoteTime = -1;
			RemoteCode lastRemoteCode = None;
			Banner::ptr banner;
			IDotStar::ptr dotStar;
			Pattern::ptr pattern;
			Patterns::ptr patterns;
			Timer::ptr timer;

#ifndef __CLR_VER
			std::mutex remoteMutex;
#endif
			std::queue<RemoteCode> remoteCodes;

			Controller(IDotStar::ptr dotStar);
			bool HandleRemote();
			void LoadPattern(bool startAtEnd = false);
		};
	}
}
