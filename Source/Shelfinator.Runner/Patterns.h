﻿#pragma once

#include <string>
#include <vector>
#include "Pattern.h"

namespace Shelfinator
{
	namespace Runner
	{
		class Patterns
		{
		public:
			typedef std::shared_ptr<Patterns> ptr;
			static ptr Create(std::string path);
			void MakeFirst(int patternNumber);
			int GetValue(int index);
			int GetIndex(int value);
			int Count();
			Pattern::ptr LoadPattern(int index);
		private:
			std::string path;
			std::vector<int> patternNumbers;
			Patterns(std::string path);
			void AddIfPatternFile(std::string fileName);
			void SetupPatterns();
		};
	}
}