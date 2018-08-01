#include "LightsQueue.h"

namespace Shelfinator
{
	namespace Runner
	{
		LightsQueue::ptr LightsQueue::Create(int maxCapacity)
		{
			return ptr(new LightsQueue(maxCapacity));
		}

		LightsQueue::LightsQueue(int maxCapacity)
		{
			this->maxCapacity = maxCapacity;
		}

		Lights::ptr LightsQueue::Get()
		{
			std::unique_lock<decltype(mutex)> lock(mutex);
			while (lightsQueue.empty())
				getCond.wait(lock);

			auto result = lightsQueue.front();
			lightsQueue.pop();
			addCond.notify_one();

			return result;
		}

		void LightsQueue::Add(Lights::ptr lights)
		{
			std::unique_lock<decltype(mutex)> lock(mutex);
			while (lightsQueue.size() >= maxCapacity)
				addCond.wait(lock);

			lightsQueue.push(lights);
			getCond.notify_one();
		}
	}
}
