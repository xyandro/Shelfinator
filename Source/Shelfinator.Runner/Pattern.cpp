#include "Pattern.h"

#include "Helpers.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace PatternData
		{
			Pattern::ptr Pattern::CreateTest()
			{
				return ptr(new Pattern());
			}

			Pattern::ptr Pattern::Read(std::string fileName)
			{
				return ptr(new Pattern(fileName));
			}

			Pattern::ptr Pattern::Read(BufferFile::ptr file)
			{
				return ptr(new Pattern(file));
			}

			Pattern::Pattern()
			{
				test = true;
				length = 1 << 30;
			}

			Pattern::Pattern(std::string fileName)
			{
				FileName = fileName;
				ReadFile(BufferFile::Open(fileName));
			}

			Pattern::Pattern(BufferFile::ptr file)
			{
				FileName = "Network transfer";
				ReadFile(file);
			}

			void Pattern::ReadFile(BufferFile::ptr file)
			{
				ReadSegments(file);
				segmentItems.Read(file, length);
				paletteSequences.Read(file);
			}

			void Pattern::ReadSegments(BufferFile::ptr file)
			{
				segments.resize(file->GetInt());
				for (auto ctr = 0; ctr < segments.size(); ++ctr)
					segments[ctr].Read(file);
			}

			void Pattern::SetLights(int time, double brightness, Lights::ptr lights)
			{
				if ((time < 0) || (test))
					return;

				auto paletteSequence = paletteSequences.SequenceAtTime(time);
				auto palettePercent = paletteSequence.GetPercent(time);
				auto segmentItem = segmentItems.SegmentAtTime(time);
				auto segmentTime = segmentItem.GetSegmentTime(time);
				segments[segmentItem.segmentIndex].SetLights(segmentTime, brightness, lights, paletteSequence, palettePercent);
			}

			int Pattern::GetLength()
			{
				return length;
			}
		}
	}
}
