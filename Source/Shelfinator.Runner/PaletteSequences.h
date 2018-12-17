#pragma once

#include <vector>
#include "BufferFile.h"
#include "PaletteSequence.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace PatternData
		{
			class PaletteSequences
			{
			public:
				void Read(BufferFile::ptr file);
				PaletteSequence &SequenceAtTime(int time);
			private:
				std::vector<PaletteSequence> paletteSequences;
				int current = 0;
			};
		}
	}
}
