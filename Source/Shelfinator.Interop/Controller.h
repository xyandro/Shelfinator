#pragma once

#include "../Shelfinator.Runner/Controller.h"
#include "IDotStar.h"
#include "RemoteCode.h"

namespace Shelfinator
{
	namespace Interop
	{
		public ref class Controller
		{
		public:
			Controller(IDotStar ^dotStar);
			~Controller();
			void Run(System::Collections::Generic::List<int> ^patternNumbers);
			void Test(int lightCount, int concurrency, int delay, int brightness);
			void Stop();
			void AddRemoteCode(RemoteCode remoteCode);
		private:
			Runner::Controller::ptr *controller;
		};
	}
}
