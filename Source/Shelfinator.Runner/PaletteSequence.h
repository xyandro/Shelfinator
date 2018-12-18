#pragma once

#include "BufferFile.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace SongData
		{
			class PaletteSequence
			{
			public:
				int startTime, endTime, startPaletteIndex, endPaletteIndex;
				void Read(BufferFile::ptr file);
				double GetPercent(int time);
			};
		}
	}
}
