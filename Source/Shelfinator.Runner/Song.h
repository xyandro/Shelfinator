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

				static ptr Read(std::string fileName);
				static ptr Read(BufferFile::ptr file);

				std::string FileName = "";
				std::string NormalSongFileName();
				std::string EditedSongFileName();
				void SetLights(int time, double brightness, Lights::ptr lights);
			private:
				std::string normalAudioFile, editedAudioFile;
				std::vector<Segment> segments;
				void ReadSegments(BufferFile::ptr file);

				SegmentItems segmentItems;

				PaletteSequences paletteSequences;

				Song(std::string fileName);
				Song(BufferFile::ptr file);
				void ReadFile(BufferFile::ptr file);
			};
		}
	}
}
