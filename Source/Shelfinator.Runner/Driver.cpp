﻿#include "Driver.h"

#include <thread>

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
			std::thread(&Driver::RunLights, this).detach();
		}

		Driver::~Driver()
		{
			std::unique_lock<decltype(mutex)> lock(mutex);
			level = 1;
			condVar.notify_all();

			while (level != 2)
				condVar.wait(lock);
		}

		void Driver::SetLights(Lights::ptr lights)
		{
			std::unique_lock<decltype(mutex)> lock(mutex);
			while (showLights)
				condVar.wait(lock);

			showLights = lights;
			condVar.notify_all();
		}

		void Driver::RunLights()
		{
			try
			{
				while (true)
				{
					Lights::ptr lights;
					{
						std::unique_lock<decltype(mutex)> lock(mutex);
						while ((level == 0) && (!showLights))
							condVar.wait(lock);

						if ((level == 1) && (!showLights))
						{
							level = 2;
							condVar.notify_all();
							break;
						}

						lights = showLights;
						showLights.reset();
						condVar.notify_all();
					}

					lights->Show(dotStar);
				}
			}
			catch (char const* str)
			{
				fprintf(stderr, "Driver::RunLights failed: %s\n", str);
				throw;
			}
			catch (...)
			{
				fprintf(stderr, "Driver::RunLights failed.\n");
				throw;
			}
		}
	}
}
