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
		namespace SongData
		{
			class Song
			{
			public:
				typedef std::shared_ptr<Song> ptr;

				static ptr CreateTest();
				static ptr Read(std::string fileName);
				static ptr Read(BufferFile::ptr file);

				std::string FileName = "";
				std::string SongFileName = "";
				void SetLights(int time, double brightness, Lights::ptr lights);
				int GetLength();
			private:
				bool test = false;

				std::vector<Segment> segments;
				void ReadSegments(BufferFile::ptr file);

				int length = 0;
				SegmentItems segmentItems;

				PaletteSequences paletteSequences;

				Song();
				Song(std::string fileName);
				Song(BufferFile::ptr file);
				void ReadFile(BufferFile::ptr file);
			};
		}
	}
}
