#pragma once

#include "../Shelfinator.Runner/Controller.h"
#include "IDotStar.h"
#include "IRemote.h"

namespace Shelfinator
{
	namespace Interop
	{
		public ref class Controller
		{
		public:
			Controller(IDotStar ^dotStar, IRemote ^remote);
			~Controller();
			void Run(System::Collections::Generic::List<int> ^patternNumbers);
			void Test(int lightCount, int concurrency, int delay);
			void Stop();
		private:
			Runner::Controller::ptr *driver;
		};
	}
}
