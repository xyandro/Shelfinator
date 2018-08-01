#pragma once

#ifndef INTEROP
#include <mutex>
#endif
#include "Lights.h"

namespace Shelfinator
{
	namespace Runner
	{
		class LightsQueue
		{
		public:
			typedef std::shared_ptr<LightsQueue> ptr;
			static LightsQueue::ptr Create(int maxCapacity);
			Lights::ptr Get();
			void Add(Lights::ptr lights);
#ifndef INTEROP
		private:
			int maxCapacity;
			std::mutex mutex;
			std::condition_variable getCond, addCond;
			std::queue<Lights::ptr> lightsQueue;
			LightsQueue(int maxCapacity);
#endif
		};
	}
}
