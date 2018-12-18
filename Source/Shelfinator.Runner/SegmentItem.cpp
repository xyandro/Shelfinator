#include "SegmentItem.h"

#include <math.h>
#include "Helpers.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace SongData
		{
			void SegmentItem::Read(BufferFile::ptr file)
			{
				segmentIndex = file->GetInt();
				segmentStartTime = file->GetInt();
				segmentEndTime = file->GetInt();
				startTime = file->GetInt();
				endTime = file->GetInt();
				startVelocity = file->GetInt();
				endVelocity = file->GetInt();
				baseVelocity = file->GetInt();
			}

			double SegmentItem::GetSegmentTime(int time)
			{
				if (segmentEndTime == segmentStartTime)
					return segmentEndTime;

				auto useTime = (double)time - startTime;
				auto value = std::fmod(useTime * useTime * (endVelocity - startVelocity) / baseVelocity / (endTime - startTime) / 2 + useTime * startVelocity / baseVelocity, segmentEndTime - segmentStartTime) + segmentStartTime;
				return value;
			}
		}
	}
}
