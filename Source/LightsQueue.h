#pragma once

#include "Lights.h"
#include "Semaphore.h"

namespace Shelfinator
{
	class LightsQueue
	{
	public:
		typedef std::shared_ptr<LightsQueue> ptr;
		static LightsQueue::ptr Create(int maxCapacity);
		Lights::ptr Get();
		void Add(Lights::ptr lights);
	private:
		Semaphore::ptr getSem, addSem, mutex;
		std::queue<Lights::ptr> lightsQueue;
		LightsQueue(int maxCapacity);
	};
}
