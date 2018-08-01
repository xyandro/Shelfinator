#pragma once

#include <queue>
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
		private:
			int maxCapacity;
#ifndef INTEROP
			std::mutex mutex;
			std::condition_variable getCond, addCond;
#endif
			std::queue<Lights::ptr> lightsQueue;
			LightsQueue(int maxCapacity);
		};
	}
}
