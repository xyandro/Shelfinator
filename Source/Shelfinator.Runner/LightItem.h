#pragma once

#include "BufferFile.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace SongData
		{
			class LightItem
			{
			public:
				int startTime, endTime, origEndTime, startColorIndex, startColorValue, endColorIndex, endColorValue, intermediates;
				void Read(BufferFile::ptr file);
				double GetPercent(double time);
				double GetSameIndexColorValue(double time);
			};
		}
	}
}
