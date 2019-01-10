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
			Controller(IDotStar ^dotStar, IAudio ^audio, System::Collections::Generic::List<int> ^songNumbers, bool startPaused);
			~Controller();
			void Run();
			void Stop();
			void AddRemoteCode(RemoteCode remoteCode);
		private:
			Runner::Controller::ptr *controller;
		};
	}
}
