#pragma once

#include <vector>
#include "BufferFile.h"
#include "LightSequence.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace PatternData
		{
			class LightSequences
			{
			public:
				void Read(BufferFile::ptr file, int &length);
				LightSequence &SequenceAtTime(int time);
			private:
				std::vector<LightSequence> lightSequences;
				int current = 0;
			};
		}
	}
}
