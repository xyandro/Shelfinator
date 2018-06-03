#pragma once

#include <vector>
#include "Banner.h"
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
		static const double multipliers[];
		static const char *multiplierNames[];

		std::vector<int> patternNumbers;
		DotStar::ptr dotStar;
		Remote::ptr remote;
		Banner::ptr banner;
		std::string patternsPath;
		int time = 0, multiplierIndex = 13, patternIndex = 0;
		Pattern::ptr pattern;

		Driver(int *patternNumbers, int patternNumberCount, DotStar::ptr dotStar, Remote::ptr remote);
		void AddIfPatternFile(std::string fileName);
		void SetupPatternsPath();
		void MakeFirst(int patternNumber);
		void SetupPatternNumbers();

		bool HandleRemote();
		void LoadPattern(bool startAtEnd = false);
	};
}
