#pragma once

#include "Controller.h"

namespace Shelfinator
{
	namespace Runner
	{
		class Remote
		{
		public:
			static void Run(Controller::ptr controller);
		private:
			static void RunThread(Controller::ptr controller);
		};
	}
}
