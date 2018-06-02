#pragma once

#include <memory>
#include "DotStar.h"

namespace Shelfinator
{
	class Pattern
	{
	public:
		typedef std::shared_ptr<Pattern> ptr;

		static ptr Read(char *fileName);
		~Pattern();

		int GetNumSequences();
		int GetSequenceStartTime(int sequence);
		int GetSequenceEndTime(int sequence);
		int GetSequenceRepeat(int sequence);

		void SetLights(int time, DotStar::ptr dotStar);
	private:
		int numSequences;
		int *sequenceStartTime, *sequenceEndTime, *sequenceRepeat;

		int numLights;
		int **lightStartTime, **lightEndTime, **lightStartColor, **lightEndColor;
		int *currentIndex;

		Pattern(FILE *file);
	};
}
