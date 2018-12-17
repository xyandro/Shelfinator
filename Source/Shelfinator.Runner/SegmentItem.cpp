#include "SegmentItem.h"

#include "Helpers.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace PatternData
		{
			void SegmentItem::Read(BufferFile::ptr file, int &length)
			{
				segmentIndex = file->GetInt();
				type = (SegmentItemType)file->GetByte();
				segmentStartTime = file->GetInt();
				segmentEndTime = file->GetInt();
				startVelocity = file->GetInt();
				endVelocity = file->GetInt();
				baseVelocity = file->GetInt();
				duration = file->GetInt();
				repeat = file->GetInt();
				startTime = length;
				endTime = length = length + duration * repeat;
			}

			double SegmentItem::GetSegmentTime(int time)
			{
				auto useTime = (double)time - startTime;
				switch (type)
				{
				case LINEAR:
					return Helpers::FPart(useTime / duration) * (segmentEndTime - segmentStartTime) + segmentStartTime;
				case VELOCITYBASED:
					return std::fmod(useTime * useTime * (endVelocity - startVelocity) * (endVelocity + startVelocity) / baseVelocity / baseVelocity / (segmentEndTime - segmentStartTime) / repeat / 4 + useTime * startVelocity / baseVelocity, segmentEndTime - segmentStartTime) + segmentStartTime;
				}
				throw "Invalid type";
			}

		}
	}
}
