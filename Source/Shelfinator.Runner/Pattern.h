#pragma once

#include "Light.h"
#include "LightColor.h"
#include "Lights.h"
#include "PaletteSequences.h"
#include "Segment.h"
#include "SegmentItems.h"

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

				std::vector<Segment> segments;
				void ReadSegments(BufferFile::ptr file);

				int length = 0;
				SegmentItems segmentItems;

				PaletteSequences paletteSequences;

				Pattern();
				Pattern(std::string fileName);
				Pattern(BufferFile::ptr file);
				void ReadFile(BufferFile::ptr file);
			};
		}
	}
}
