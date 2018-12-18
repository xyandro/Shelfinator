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
				enum SegmentItemType { LINEAR, VELOCITYBASED };

				int segmentIndex;
				SegmentItemType type;
				int segmentStartTime, segmentEndTime, startVelocity, endVelocity, baseVelocity, duration, startTime, endTime, repeat;
				void Read(BufferFile::ptr file, int &length);
				double GetSegmentTime(int time);
			};
		}
	}
}
