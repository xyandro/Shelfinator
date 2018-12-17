#pragma once

#include "Light.h"
#include "LightColor.h"
#include "Lights.h"
#include "LightSequences.h"
#include "PaletteSequences.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace PatternData
		{
			class Pattern
			{
			public:
				typedef std::shared_ptr<Pattern> ptr;

				static ptr CreateTest();
				static ptr Read(std::string fileName);
				static ptr Read(BufferFile::ptr file);

				std::string FileName = "";
				void SetLights(int time, double brightness, Lights::ptr lights);
				int GetLength();
			private:
				bool test = false;

				std::vector<Light> lightData;
				void ReadLights(BufferFile::ptr file);

				int length = 0;
				LightSequences lightSequences;

				std::vector<LightColor> lightColors;
				void ReadColors(BufferFile::ptr file);

				PaletteSequences paletteSequences;

				Pattern();
				Pattern(std::string fileName);
				Pattern(BufferFile::ptr file);
				void ReadFile(BufferFile::ptr file);
			};
		}
	}
}
