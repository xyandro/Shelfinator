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
			Driver(IDotStar ^dotStar, IRemote ^remote);
			~Driver();
			void Run(System::Collections::Generic::List<int> ^patternNumbers);
			void Test(int lightCount, int concurrency, int delay);
			void Stop();
		private:
			Runner::Driver::ptr *driver;
		};
	}
}
