#pragma once

#include <chrono>
#include "Banner.h"
#include "IDotStar.h"
#include "IRemote.h"
#include "Patterns.h"

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
			std::shared_ptr<std::chrono::steady_clock::time_point> start;
			Patterns::ptr patterns;
			IDotStar::ptr dotStar;
			IRemote::ptr remote;
			Banner::ptr banner;
			std::string patternsPath;
			int time = 0, multiplierIndex = 13, patternIndex = 0, selectedNumber = 0, selectedNumberTime = -1;
			Pattern::ptr pattern;

			Controller(IDotStar::ptr dotStar, IRemote::ptr remote);

			bool HandleRemote();
			void LoadPattern(bool startAtEnd = false);

			std::shared_ptr<std::chrono::steady_clock::time_point> GetTime();
			int Millis();
			int Millis(std::shared_ptr<std::chrono::steady_clock::time_point> atTime);
		};
	}
}
