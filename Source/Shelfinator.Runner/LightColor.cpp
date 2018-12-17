#include "LightColor.h"

#include "Helpers.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace PatternData
		{
			void LightColor::Read(BufferFile::ptr file)
			{
				minValue = file->GetInt();
				maxValue = file->GetInt();
				colors.resize(file->GetInt());
				for (auto paletteCtr = 0; paletteCtr < colors.size(); ++paletteCtr)
				{
					colors[paletteCtr].resize(file->GetInt());
					for (auto colorCtr = 0; colorCtr < colors[paletteCtr].size(); ++colorCtr)
						colors[paletteCtr][colorCtr] = file->GetInt();
				}
			}

			void LightColor::GradientColor(double value, int index, double &red, double &green, double &blue)
			{
				index = index % colors.size();
				Helpers::GradientColor(value, minValue, maxValue, colors[index].data(), (int)colors[index].size(), red, green, blue);
			}
		}
	}
}
