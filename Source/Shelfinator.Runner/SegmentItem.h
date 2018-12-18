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
				int segmentIndex, segmentStartTime, segmentEndTime, startTime, endTime, startVelocity, endVelocity, baseVelocity;
				void Read(BufferFile::ptr file);
				double GetSegmentTime(int time);
			};
		}
	}
}
