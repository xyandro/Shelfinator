#include "Driver.h"

namespace Shelfinator
{
	namespace Runner
	{
		Driver::ptr Driver::Create(IDotStar::ptr dotStar)
		{
			return ptr(new Driver(dotStar));
		}

		Driver::Driver(IDotStar::ptr dotStar)
		{
			this->dotStar = dotStar;
			lightsQueue = LightsQueue::Create(2);
			std::thread(&Driver::RunLights, this).detach();
		}

		Driver::~Driver()
		{
			std::unique_lock<decltype(mutex)> lock(mutex);
			lightsQueue->Add(Lights::Create(2440));
			lightsQueue->Add(nullptr);

			while (!finished)
				condVar.wait(lock);
		}

		void Driver::Add(Lights::ptr lights)
		{
			lightsQueue->Add(lights);
		}

		void Driver::RunLights()
		{
			while (true)
			{
				auto lights = lightsQueue->Get();
				if (!lights)
				{
					std::unique_lock<decltype(mutex)> lock(mutex);
					finished = true;
					condVar.notify_all();
					break;
				}
				lights->CheckOverage();
				dotStar->Show(lights->lights, lights->count);
			}
		}
	}
}
