#include "Song.h"

#include "Helpers.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace SongData
		{
			Song::ptr Song::Read(std::string fileName)
			{
				return ptr(new Song(fileName));
			}

			Song::ptr Song::Read(BufferFile::ptr file)
			{
				return ptr(new Song(file));
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
				normalAudioFile = file->GetString();
				editedAudioFile = file->GetString();
				ReadSegments(file);
				segmentItems.Read(file);
				paletteSequences.Read(file);
			}

			void Song::ReadSegments(BufferFile::ptr file)
			{
				segments.resize(file->GetInt());
				for (auto ctr = 0; ctr < segments.size(); ++ctr)
					segments[ctr].Read(file);
			}

			std::string Song::NormalSongFileName()
			{
				return FileName.substr(0, FileName.find_last_of(Helpers::PathSeparator)) + Helpers::PathSeparator + normalAudioFile;
			}

			std::string Song::EditedSongFileName()
			{
				return FileName.substr(0, FileName.find_last_of(Helpers::PathSeparator)) + Helpers::PathSeparator + editedAudioFile;
			}

			void Song::SetLights(int time, double brightness, Lights::ptr lights)
			{
				lights->Clear();

				auto segmentItem = segmentItems.SegmentAtTime(time);
				if (segmentItem == nullptr)
					return;

				auto paletteSequence = paletteSequences.SequenceAtTime(time);
				auto palettePercent = paletteSequence.GetPercent(time);
				auto segmentTime = segmentItem->GetSegmentTime(time);
				segments[segmentItem->segmentIndex].SetLights(segmentTime, brightness, lights, paletteSequence, palettePercent);
			}
		}
	}
}
