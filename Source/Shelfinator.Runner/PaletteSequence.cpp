#include "PaletteSequence.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace PatternData
		{
			void PaletteSequence::Read(BufferFile::ptr file)
			{
				startTime = file->GetInt();
				endTime = file->GetInt();
				startPaletteIndex = file->GetInt();
				endPaletteIndex = file->GetInt();
			}

			double PaletteSequence::GetPercent(int time)
			{
				return startPaletteIndex == endPaletteIndex ? 0 : ((double)time - startTime) / (endTime - startTime);
			}
		}
	}
}
