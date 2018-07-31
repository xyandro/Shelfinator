#pragma once

#include "Banner.h"
#include "DotStar.h"
#include "LightsQueue.h"
#include "Pattern.h"
#include "Remote.h"

namespace Shelfinator
{
	namespace Runner
	{
		class Driver
		{
		public:
			typedef std::shared_ptr<Driver> ptr;
			static ptr Create(int *patternNumbers, int patternNumberCount, DotStar::ptr dotStar, Remote::ptr remote);
			void Run();
			void Stop();
		private:
			static const double multipliers[];
			static const char *multiplierNames[];

			bool running = true;
			std::shared_ptr<std::chrono::steady_clock::time_point> start;
			std::vector<int> patternNumbers;
			DotStar::ptr dotStar;
			Remote::ptr remote;
			Banner::ptr banner;
			std::string patternsPath;
			int time = 0, multiplierIndex = 13, patternIndex = 0, selectedNumber = 0, selectedNumberTime = -1;
			Pattern::ptr pattern;

			LightsQueue::ptr lightsQueue;
			Semaphore::ptr stopSem;

#ifdef _WIN32
			static DWORD RunUIThread(void *lpThreadParameter);
#endif
			void RunUIThread();

			Driver(int *patternNumbers, int patternNumberCount, DotStar::ptr dotStar, Remote::ptr remote);
			void AddIfPatternFile(std::string fileName);
			void SetupPatternsPath();
			void MakeFirst(int patternNumber);
			void SetupPatternNumbers();

			bool HandleRemote();
			void LoadPattern(bool startAtEnd = false);

			std::shared_ptr<std::chrono::steady_clock::time_point> GetTime();
			int Millis();
			int Millis(std::shared_ptr<std::chrono::steady_clock::time_point> atTime);
		};
	}
}
