#include "stdafx.h"
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
			getSem = Semaphore::Create(0);
			addSem = Semaphore::Create(maxCapacity);
			mutex = Semaphore::Create(1);
		}

		Lights::ptr LightsQueue::Get()
		{
			getSem->Wait();

			mutex->Wait();
			auto result = lightsQueue.front();
			lightsQueue.pop();
			mutex->Signal();

			addSem->Signal();
			return result;
		}

		void LightsQueue::Add(Lights::ptr lights)
		{
			addSem->Wait();

			mutex->Wait();
			lightsQueue.push(lights);
			mutex->Signal();

			getSem->Signal();
		}
	}
}
