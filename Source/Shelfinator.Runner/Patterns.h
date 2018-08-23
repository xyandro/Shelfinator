#pragma once

#include <map>
#ifndef __CLR_VER
#include <condition_variable>
#include <mutex>
#endif
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
			static ptr Create();
			void MakeFirst(int patternNumber);
			int GetValue(int index);
			int GetIndex(int value);
			int Count();
			Pattern::ptr LoadPattern(int index);
		private:
			std::string path;
			std::vector<int> patternNumbers;
			std::vector<int> patternQueue;
			std::map<int, Pattern::ptr> patternCache;
#ifndef __CLR_VER
			std::mutex mutex;
			std::condition_variable condVar;
#endif
			int queueValue = 0;
			Patterns();
			void AddIfPatternFile(std::string fileName);
			void SetupPatterns();
			void LoadPatternsThread();
		};
	}
}
