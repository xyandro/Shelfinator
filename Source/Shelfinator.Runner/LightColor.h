#pragma once

#include <vector>
#include "BufferFile.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace PatternData
		{
			class LightColor
			{
			public:
				int minValue, maxValue;
				std::vector<std::vector<int>> colors;
				void Read(BufferFile::ptr file);
				void GradientColor(double value, int index, double &red, double &green, double &blue);
			};
		}
	}
}
