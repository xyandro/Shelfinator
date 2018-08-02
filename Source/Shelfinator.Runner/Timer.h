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
			int Millis();
		private:
			std::chrono::steady_clock::time_point timer;
			Timer();
			std::chrono::steady_clock::time_point Now();
		};
	}
}
