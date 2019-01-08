#pragma once

#include <chrono>
#include <memory>

namespace Shelfinator
{
	namespace Runner
	{
		class Timer
		{
		public:
			typedef std::shared_ptr<Timer> ptr;
			static ptr Create();
			void Restart();
			int Elapsed();
			static std::chrono::steady_clock::time_point Now();
			static int CalcDiff(std::chrono::steady_clock::time_point time1, std::chrono::steady_clock::time_point time2);
		private:
			std::chrono::steady_clock::time_point start;
			Timer();
		};
	}
}
