#pragma once

#include <vector>
#include "BufferFile.h"
#include "SegmentItem.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace SongData
		{
			class SegmentItems
			{
			public:
				void Read(BufferFile::ptr file, int &length);
				SegmentItem &SegmentAtTime(int time);
			private:
				std::vector<SegmentItem> segmentItems;
				int current = 0;
			};
		}
	}
}
