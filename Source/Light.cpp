﻿#include "stdafx.h"
#include "Light.h"

namespace Shelfinator
{
	int Light::LightData::GetLight(int time)
	{
		auto mult = (double)(time - startTime) / (endTime - startTime);
		return ((int)(((startColor >> 16) & 0xff) * (1 - mult) + ((endColor >> 16) & 0xff) * mult) << 16) | ((int)(((startColor >> 8) & 0xff) * (1 - mult) + ((endColor >> 8) & 0xff) * mult) << 8) | ((int)(((startColor >> 0) & 0xff) * (1 - mult) + ((endColor >> 0) & 0xff) * mult) << 0);
	}

	int Light::GetLight(int time)
	{
		while ((currentData > 0) && (time < lightData[currentData].startTime))
			--currentData;
		while ((currentData < dataCount) && (time >= lightData[currentData].endTime))
			++currentData;
		if (currentData == dataCount)
			return 0;
		return lightData[currentData].GetLight(time);
	}
}
