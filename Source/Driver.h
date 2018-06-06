#pragma once

#include "Banner.h"
#include "DotStar.h"
#include "Pattern.h"
#include "Remote.h"
#include "Semaphore.h"

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

		std::shared_ptr<std::chrono::steady_clock::time_point> start;
		std::vector<int> patternNumbers;
		DotStar::ptr dotStar;
		Remote::ptr remote;
		Banner::ptr banner;
		std::string patternsPath;
		int time = 0, multiplierIndex = 13, patternIndex = 0, selectedNumber = 0, selectedNumberTime = -1;
		Pattern::ptr pattern;

		static const int numBuffers = 2;
		Semaphore::ptr readSem, writeSem;
		int readBuf = -1, writeBuf = -1;
		Lights::ptr lights[numBuffers];

#ifdef _WIN32
		static DWORD RunUIThread(void *lpThreadParameter);
#endif
		void RunUIThread();

		Driver(int *patternNumbers, int patternNumberCount, DotStar::ptr dotStar, Remote::ptr remote);
		void AddIfPatternFile(std::string fileName);
		void SetupPatternsPath();
		void MakeFirst(int patternNumber);
		void SetupPatternNumbers();

		bool HandleRemote();
		void LoadPattern(bool startAtEnd = false);

		std::shared_ptr<std::chrono::steady_clock::time_point> GetTime();
		int Millis();
		int Millis(std::shared_ptr<std::chrono::steady_clock::time_point> atTime);
	};
}
