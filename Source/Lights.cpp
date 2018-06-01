#include "stdafx.h"

#include "Lights.h"

namespace Shelfinator
{
	Lights::ptr Lights::Read(char *fileName)
	{
		return ptr(new Lights(fileName));
	}

	Lights::Lights(char *fileName)
	{
		auto file = fopen(fileName, "rb");
		fread(&length, 1, sizeof(length), file);

		fread(&lightCount, 1, sizeof(lightCount), file);

		light = new Light[lightCount];
		for (int lightCtr = 0; lightCtr < lightCount; ++lightCtr)
		{
			fread(&light[lightCtr].dataCount, 1, sizeof(light[lightCtr].dataCount), file);
			light[lightCtr].lightData = new Light::LightData[light[lightCtr].dataCount];

			for (int dataCtr = 0; dataCtr < light[lightCtr].dataCount; ++dataCtr)
			{
				fread(&light[lightCtr].lightData[dataCtr].startTime, 1, sizeof(light[lightCtr].lightData[dataCtr].startTime), file);
				fread(&light[lightCtr].lightData[dataCtr].endTime, 1, sizeof(light[lightCtr].lightData[dataCtr].endTime), file);
				fread(&light[lightCtr].lightData[dataCtr].startColor, 1, sizeof(light[lightCtr].lightData[dataCtr].startColor), file);
				fread(&light[lightCtr].lightData[dataCtr].endColor, 1, sizeof(light[lightCtr].lightData[dataCtr].endColor), file);
			}
		}

		fclose(file);
	}

	Lights::~Lights()
	{
		delete light;
	}

	void Lights::SetLights(int time, DotStar::ptr dotStar)
	{
		for (int lightCtr = 0; lightCtr < lightCount; ++lightCtr)
			dotStar->SetPixelColor(lightCtr, light[lightCtr].GetLight(time));
	}

	int Lights::GetLength()
	{
		return length;
	}
}
