#pragma once

#include "../Shelfinator.Runner/Driver.h"
#include "IDotStar.h"
#include "IRemote.h"

namespace Shelfinator
{
	namespace Interop
	{
		public ref class Driver
		{
		public:
			Driver(System::Collections::Generic::List<int> ^patternNumbers, IDotStar ^dotStar, IRemote ^remote);
			~Driver();
			void Run();
			void Stop();
		private:
			Runner::Driver::ptr *driver;
		};
	}
}
