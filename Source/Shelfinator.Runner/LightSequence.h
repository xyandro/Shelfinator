#pragma once

#include "BufferFile.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace PatternData
		{
			class LightSequence
			{
			public:
				enum LightSequenceType { LINEAR, VELOCITYBASED };

				LightSequenceType type;
				int lightStartTime, lightEndTime, startVelocity, endVelocity, baseVelocity, duration, startTime, endTime, repeat;
				void Read(BufferFile::ptr file, int &length);
				double GetLightTime(int time);
			};
		}
	}
}
