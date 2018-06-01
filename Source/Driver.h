#pragma once

#include <memory>
#include "Lights.h"

namespace Shelfinator
{
	class Driver
	{
	public:
		typedef std::shared_ptr<Driver> ptr;
		static ptr Create(char *fileName, DotStar::ptr dotStar);
		void Run();
	private:
		Lights::ptr lights;
		DotStar::ptr dotStar;
		Driver(char *fileName, DotStar::ptr dotStar);
	};

#ifdef _WIN32
	public ref class DriverRunner
	{
	public:
		static void Run(System::String ^fileName, IDotStar ^iDotStar);
	};
#endif
}
