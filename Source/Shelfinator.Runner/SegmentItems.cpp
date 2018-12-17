#include "SegmentItems.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace PatternData
		{
			void SegmentItems::Read(BufferFile::ptr file, int &length)
			{
				segmentItems.resize(file->GetInt());
				length = 0;
				for (auto ctr = 0; ctr < segmentItems.size(); ++ctr)
					segmentItems[ctr].Read(file, length);
			}

			SegmentItem &SegmentItems::SegmentAtTime(int time)
			{
				while (time < segmentItems[current].startTime)
					--current;
				while (time >= segmentItems[current].endTime)
					++current;
				return segmentItems[current];
			}
		}
	}
}
