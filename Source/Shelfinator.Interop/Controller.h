#pragma once

#include "../Shelfinator.Runner/Controller.h"
#include "IAudio.h"
#include "IDotStar.h"
#include "RemoteCode.h"

namespace Shelfinator
{
	namespace Interop
	{
		public ref class Controller
		{
		public:
			Controller(IDotStar ^dotStar, IAudio ^audio);
			~Controller();
			void Run(System::Collections::Generic::List<int> ^songNumbers, bool startPaused);
			void Test(int firstLight, int lightCount, int concurrency, int delay, unsigned char brightness);
			void TestAll(int lightCount, int delay, unsigned char brightness);
			void Stop();
			void AddRemoteCode(RemoteCode remoteCode);
		private:
			Runner::Controller::ptr *controller;
		};
	}
}
