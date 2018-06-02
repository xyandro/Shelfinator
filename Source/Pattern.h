#pragma once

#include <memory>
#include "DotStar.h"

namespace Shelfinator
{
	class Pattern
	{
	public:
		typedef std::shared_ptr<Pattern> ptr;

		static ptr Read(const char *fileName);
		~Pattern();

		void SetLights(int time, DotStar::ptr dotStar);
		int GetLength();
	private:
		int length = 0, currentSequence = 0;
		int *sequenceLightStartTime, *sequenceLightEndTime, *sequenceStartTime, *sequenceEndTime;

		int numLights;
		int **lightStartTime, **lightEndTime, **lightStartColor, **lightEndColor;
		int *currentIndex;

		Pattern(FILE *file);
	};
}
