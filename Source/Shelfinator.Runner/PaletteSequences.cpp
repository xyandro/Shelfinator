#include "PaletteSequences.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace SongData
		{
			void PaletteSequences::Read(BufferFile::ptr file)
			{
				paletteSequences.resize(file->GetInt());
				for (auto ctr = 0; ctr < paletteSequences.size(); ++ctr)
					paletteSequences[ctr].Read(file);
			}

			PaletteSequence &PaletteSequences::SequenceAtTime(int time)
			{
				while (time < paletteSequences[current].startTime)
					--current;
				while (time >= paletteSequences[current].endTime)
					++current;
				return paletteSequences[current];
			}
		}
	}
}
