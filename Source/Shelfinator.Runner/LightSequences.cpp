#include "LightSequences.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace PatternData
		{
			void LightSequences::Read(BufferFile::ptr file, int &length)
			{
				lightSequences.resize(file->GetInt());
				length = 0;
				for (auto ctr = 0; ctr < lightSequences.size(); ++ctr)
					lightSequences[ctr].Read(file, length);
			}

			LightSequence &LightSequences::SequenceAtTime(int time)
			{
				while (time < lightSequences[current].startTime)
					--current;
				while (time >= lightSequences[current].endTime)
					++current;
				return lightSequences[current];
			}
		}
	}
}
