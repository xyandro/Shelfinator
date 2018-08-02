#pragma once

#include <mutex>
#include "IDotStar.h"
#include "Lights.h"

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
			void SetLights(Lights::ptr lights);
		private:
			IDotStar::ptr dotStar;
			Lights::ptr showLights;
			int level = 0;
			std::mutex mutex;
			std::condition_variable condVar;
			Driver(IDotStar::ptr dotStar);
			void RunLights();
		};
	}
}
