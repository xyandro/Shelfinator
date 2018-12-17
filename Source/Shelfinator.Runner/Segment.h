#pragma once

#include <vector>
#include "BufferFile.h"
#include "Light.h"
#include "Lights.h"
#include "LightColor.h"
#include "PaletteSequence.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace PatternData
		{
			class Segment
			{
			public:
				void Read(BufferFile::ptr file);
				void SetLights(double time, double brightness, Lights::ptr lights, PaletteSequence &paletteSequence, double palettePercent);
			private:
				std::vector<Light> lightData;
				void ReadLights(BufferFile::ptr file);

				std::vector<LightColor> lightColors;
				void ReadColors(BufferFile::ptr file);
			};
		}
	}
}
