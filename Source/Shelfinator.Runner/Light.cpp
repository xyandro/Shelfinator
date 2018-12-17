#include "Light.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace PatternData
		{
			void Light::Read(BufferFile::ptr file)
			{
				lights.resize(file->GetInt());
				for (auto ctr = 0; ctr < lights.size(); ++ctr)
					lights[ctr].Read(file);
			}

			LightItem &Light::LightAtTime(double lightTime)
			{
				while (lightTime < lights[current].startTime)
					--current;
				while (lightTime >= lights[current].endTime)
					++current;
				return lights[current];
			}
		}
	}
}
