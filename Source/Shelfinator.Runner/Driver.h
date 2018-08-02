#pragma once

#include "IDotStar.h"
#include "LightsQueue.h"

namespace Shelfinator
{
	namespace Runner
	{
		class Driver
		{
		public:
			typedef std::shared_ptr<Driver> ptr;
			static ptr Create(IDotStar::ptr dotStar);
			~Driver();
			void Add(Lights::ptr lights);
		private:
			IDotStar::ptr dotStar;
			LightsQueue::ptr lightsQueue;
			bool finished = false;
#ifndef INTEROP
			std::mutex mutex;
			std::condition_variable condVar;
#endif
			Driver(IDotStar::ptr dotStar);
			void RunLights();
		};
	}
}
