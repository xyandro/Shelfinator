#include "SegmentItems.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace SongData
		{
			void SegmentItems::Read(BufferFile::ptr file)
			{
				segmentItems.resize(file->GetInt());
				for (auto ctr = 0; ctr < segmentItems.size(); ++ctr)
					segmentItems[ctr].Read(file);
				current = 0;
			}

			SegmentItem *SegmentItems::SegmentAtTime(int time)
			{
				while ((current > 0) && (time < segmentItems[current].startTime))
					--current;
				while ((current < segmentItems.size() - 1) && (time >= segmentItems[current].endTime))
					++current;
				if ((time < segmentItems[current].startTime) || ((time >= segmentItems[current].endTime)))
					return nullptr;
				return &segmentItems[current];
			}
		}
	}
}
