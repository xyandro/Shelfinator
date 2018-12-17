#include "LightSequence.h"

#include "Helpers.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace PatternData
		{
			void LightSequence::Read(BufferFile::ptr file, int &length)
			{
				type = (LightSequenceType)file->GetByte();
				lightStartTime = file->GetInt();
				lightEndTime = file->GetInt();
				startVelocity = file->GetInt();
				endVelocity = file->GetInt();
				baseVelocity = file->GetInt();
				duration = file->GetInt();
				repeat = file->GetInt();
				startTime = length;
				endTime = length = length + duration * repeat;
			}

			double LightSequence::GetLightTime(int time)
			{
				auto useTime = (double)time - startTime;
				switch (type)
				{
				case LINEAR:
					return Helpers::FPart(useTime / duration) * (lightEndTime - lightStartTime) + lightStartTime;
				case VELOCITYBASED:
					return std::fmod(useTime * useTime * (endVelocity - startVelocity) * (endVelocity + startVelocity) / baseVelocity / baseVelocity / (lightEndTime - lightStartTime) / repeat / 4 + useTime * startVelocity / baseVelocity, lightEndTime - lightStartTime) + lightStartTime;
				}
				throw "Invalid type";
			}

		}
	}
}
