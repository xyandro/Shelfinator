#pragma once

#include "DotStar.h"
#include "Remote.h"

namespace Shelfinator
{
#ifdef _WIN32
	public ref class DriverRunner
	{
	public:
		static void Run(System::String ^fileName, IDotStar ^iDotStar, IRemote ^remote);
	};
#endif
}
