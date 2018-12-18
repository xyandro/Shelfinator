#include "Song.h"

#include "Helpers.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace SongData
		{
			Song::ptr Song::CreateTest()
			{
				return ptr(new Song());
			}

			Song::ptr Song::Read(std::string fileName)
			{
				return ptr(new Song(fileName));
			}

			Song::ptr Song::Read(BufferFile::ptr file)
			{
				return ptr(new Song(file));
			}

			Song::Song()
			{
				test = true;
				length = 1 << 30;
			}

			Song::Song(std::string fileName)
			{
				FileName = fileName;
				ReadFile(BufferFile::Open(fileName));
			}

			Song::Song(BufferFile::ptr file)
			{
				FileName = "Network transfer";
				ReadFile(file);
			}

			void Song::ReadFile(BufferFile::ptr file)
			{
				ReadSegments(file);
				segmentItems.Read(file, length);
				paletteSequences.Read(file);
			}

			void Song::ReadSegments(BufferFile::ptr file)
			{
				segments.resize(file->GetInt());
				for (auto ctr = 0; ctr < segments.size(); ++ctr)
					segments[ctr].Read(file);
			}

			void Song::SetLights(int time, double brightness, Lights::ptr lights)
			{
				if ((time < 0) || (test))
					return;

				auto paletteSequence = paletteSequences.SequenceAtTime(time);
				auto palettePercent = paletteSequence.GetPercent(time);
				auto segmentItem = segmentItems.SegmentAtTime(time);
				auto segmentTime = segmentItem.GetSegmentTime(time);
				segments[segmentItem.segmentIndex].SetLights(segmentTime, brightness, lights, paletteSequence, palettePercent);
			}

			int Song::GetLength()
			{
				return length;
			}
		}
	}
}
