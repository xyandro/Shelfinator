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
			timer = Now();
		}

		int Timer::Millis()
		{
			return (int)std::chrono::duration_cast<std::chrono::milliseconds>(Now() - timer).count();
		}

		std::chrono::steady_clock::time_point Timer::Now()
		{
			return std::chrono::steady_clock::now();
		}
	}
}
