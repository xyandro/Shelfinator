#pragma once

#include "Banner.h"
#include "IDotStar.h"
#include "IRemote.h"
#include "Patterns.h"
#include "Timer.h"

namespace Shelfinator
{
	namespace Runner
	{
		class Controller
		{
		public:
			typedef std::shared_ptr<Controller> ptr;
			static ptr Create(IDotStar::ptr dotStar, IRemote::ptr remote);
			~Controller();
			void Run(int *patternNumbers, int patternNumberCount);
			void Test(int lightCount, int concurrency, int delay);
			void Stop();
		private:
			static const double multipliers[];
			static std::string multiplierNames[];

			bool running = true;
			int time = 0, multiplierIndex = 13, patternIndex = 0, selectedNumber = 0, selectedNumberTime = -1;
			Banner::ptr banner;
			IDotStar::ptr dotStar;
			Pattern::ptr pattern;
			Patterns::ptr patterns;
			IRemote::ptr remote;
			Timer::ptr timer;

			Controller(IDotStar::ptr dotStar, IRemote::ptr remote);
			bool HandleRemote();
			void LoadPattern(bool startAtEnd = false);
		};
	}
}
