#pragma once

#include "BufferFile.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace SongData
		{
			class SegmentItem
			{
			public:
				int segmentIndex, segmentStartTime, segmentEndTime, startTime, endTime;
				void Read(BufferFile::ptr file);
				double GetSegmentTime(int time);
			private:
				double v0, a, b, c, mult;
			};
		}
	}
}
