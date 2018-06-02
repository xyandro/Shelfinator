#pragma once

#include <vector>
#include "Pattern.h"
#include "Remote.h"

namespace Shelfinator
{
	class Driver
	{
	public:
		typedef std::shared_ptr<Driver> ptr;
		static ptr Create(int *patternNumbers, int patternNumberCount, DotStar::ptr dotStar, Remote::ptr remote);
		void Run();
	private:
		std::vector<int> patternNumbers;
		DotStar::ptr dotStar;
		Remote::ptr remote;
		std::string patternsPath;

		Driver(int *patternNumbers, int patternNumberCount, DotStar::ptr dotStar, Remote::ptr remote);
		void SetupPatternsPath();
		void SetupPatternNumbers();
	};
}
