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
				auto segmentTime = file->GetInt();
				startTime = file->GetInt();
				endTime = file->GetInt();
				auto startVelocity = file->GetInt();
				auto endVelocity = file->GetInt();
				auto baseVelocity = file->GetInt();

				auto time = endTime - startTime;

				v0 = (double)startVelocity / baseVelocity;
				auto v1 = (double)endVelocity / baseVelocity;
				a = 120 * segmentTime / pow(time, 5) - 60 * v0 / pow(time, 4) - 60 * v1 / pow(time, 4);
				b = -180 * segmentTime / pow(time, 4) + 96 * v0 / pow(time, 3) + 84 * v1 / pow(time, 3);
				c = 60 * segmentTime / pow(time, 3) - 36 * v0 / pow(time, 2) - 24 * v1 / pow(time, 2);
				mult = segmentEndTime >= segmentStartTime ? 1 : -1;
			}

			double SegmentItem::GetSegmentTime(int time)
			{
				if (segmentEndTime == segmentStartTime)
					return segmentEndTime;

				auto useTime = (double)time - startTime;
				auto value = std::fmod(a * pow(useTime, 5) / 20 + b * pow(useTime, 4) / 12 + c * pow(useTime, 3) / 6 + v0 * useTime, segmentEndTime - segmentStartTime) * mult + segmentStartTime;
				return value;
			}
		}
	}
}
