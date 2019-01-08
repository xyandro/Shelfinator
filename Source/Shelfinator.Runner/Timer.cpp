#include "Timer.h"

namespace Shelfinator
{
	namespace Runner
	{
		Timer::ptr Timer::Create()
		{
			return ptr(new Timer());
		}

		Timer::Timer()
		{
			Restart();
		}

		void Timer::Restart()
		{
			start = Now();
		}

		int Timer::Elapsed()
		{
			return CalcDiff(start, Now());
		}

		std::chrono::steady_clock::time_point Timer::Now()
		{
			return std::chrono::steady_clock::now();
		}

		int Timer::CalcDiff(std::chrono::steady_clock::time_point time1, std::chrono::steady_clock::time_point time2)
		{
			return (int)std::chrono::duration_cast<std::chrono::milliseconds>(time2 - time1).count();
		}
	}
}
