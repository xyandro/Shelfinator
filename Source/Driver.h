#pragma once

#include "Pattern.h"
#include "Remote.h"

namespace Shelfinator
{
	class Driver
	{
	public:
		typedef std::shared_ptr<Driver> ptr;
		static ptr Create(char *fileName, DotStar::ptr dotStar, Remote::ptr remote);
		void Run();
	private:
		Pattern::ptr pattern;
		DotStar::ptr dotStar;
		Remote::ptr remote;
		Driver(char *fileName, DotStar::ptr dotStar, Remote::ptr remote);
	};
}
