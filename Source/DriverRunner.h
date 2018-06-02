#pragma once

#include "DotStar.h"
#include "Remote.h"

namespace Shelfinator
{
#ifdef _WIN32
	public ref class DriverRunner
	{
	public:
		static void Run(System::Collections::Generic::List<int> ^patternNumbers, IDotStar ^dotStar, IRemote ^remote);
	};
#endif
}
