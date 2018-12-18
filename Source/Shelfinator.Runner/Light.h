#pragma once

#include <vector>
#include "BufferFile.h"
#include "LightItem.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace SongData
		{
			class Light
			{
			public:
				void Read(BufferFile::ptr file);
				LightItem &LightAtTime(double time);
			private:
				std::vector<LightItem> lights;
				int current = 0;
			};
		}
	}
}
