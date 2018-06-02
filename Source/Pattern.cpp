#include "stdafx.h"
#include "Pattern.h"

namespace Shelfinator
{
	Pattern::ptr Pattern::Read(const char *fileName)
	{
		auto file = fopen(fileName, "rb");
		auto result = ptr(new Pattern(file));
		fclose(file);
		return result;
	}

	Pattern::Pattern(FILE *file)
	{
		fread(&numSequences, 1, sizeof(numSequences), file);
		sequenceStartTime = new int[numSequences];
		sequenceEndTime = new int[numSequences];
		sequenceRepeat = new int[numSequences];
		for (auto sequenceCtr = 0; sequenceCtr < numSequences; ++sequenceCtr)
		{
			fread(&sequenceStartTime[sequenceCtr], 1, sizeof(sequenceStartTime[sequenceCtr]), file);
			fread(&sequenceEndTime[sequenceCtr], 1, sizeof(sequenceEndTime[sequenceCtr]), file);
			fread(&sequenceRepeat[sequenceCtr], 1, sizeof(sequenceRepeat[sequenceCtr]), file);
		}

		fread(&numLights, 1, sizeof(numLights), file);
		lightStartTime = new int*[numLights];
		lightEndTime = new int*[numLights];
		lightStartColor = new int*[numLights];
		lightEndColor = new int*[numLights];
		currentIndex = new int[numLights];

		for (auto lightCtr = 0; lightCtr < numLights; ++lightCtr)
		{
			int dataCount;
			fread(&dataCount, 1, sizeof(dataCount), file);

			lightStartTime[lightCtr] = new int[dataCount];
			lightEndTime[lightCtr] = new int[dataCount];
			lightStartColor[lightCtr] = new int[dataCount];
			lightEndColor[lightCtr] = new int[dataCount];
			currentIndex[lightCtr] = 0;

			for (auto dataCtr = 0; dataCtr < dataCount; ++dataCtr)
			{
				fread(&lightStartTime[lightCtr][dataCtr], 1, sizeof(lightStartTime[lightCtr][dataCtr]), file);
				fread(&lightEndTime[lightCtr][dataCtr], 1, sizeof(lightEndTime[lightCtr][dataCtr]), file);
				fread(&lightStartColor[lightCtr][dataCtr], 1, sizeof(lightStartColor[lightCtr][dataCtr]), file);
				fread(&lightEndColor[lightCtr][dataCtr], 1, sizeof(lightEndColor[lightCtr][dataCtr]), file);
			}
		}
	}

	Pattern::~Pattern()
	{
		delete[] sequenceStartTime;
		delete[] sequenceEndTime;
		delete[] sequenceRepeat;

		for (auto ctr = 0; ctr < numLights; ++ctr)
		{
			delete[] lightStartTime[ctr];
			delete[] lightEndTime[ctr];
			delete[] lightStartColor[ctr];
			delete[] lightEndColor[ctr];
		}
		delete[] lightStartTime;
		delete[] lightEndTime;
		delete[] lightStartColor;
		delete[] lightEndColor;
		delete[] currentIndex;
	}

	int Pattern::GetNumSequences() { return numSequences; }
	int Pattern::GetSequenceStartTime(int sequence) { return sequenceStartTime[sequence]; }
	int Pattern::GetSequenceEndTime(int sequence) { return sequenceEndTime[sequence]; }
	int Pattern::GetSequenceRepeat(int sequence) { return sequenceRepeat[sequence]; }

	void Pattern::SetLights(int time, DotStar::ptr dotStar)
	{
		if (time < 0)
			return;

		for (auto light = 0; light < numLights; ++light)
		{
			while (time < lightStartTime[light][currentIndex[light]])
				--currentIndex[light];
			while (time >= lightEndTime[light][currentIndex[light]])
				++currentIndex[light];

			auto mult = (double)(time - lightStartTime[light][currentIndex[light]]) / (lightEndTime[light][currentIndex[light]] - lightStartTime[light][currentIndex[light]]);
			auto result = ((int)(((lightStartColor[light][currentIndex[light]] >> 16) & 0xff) * (1 - mult) + ((lightEndColor[light][currentIndex[light]] >> 16) & 0xff) * mult) << 16) | ((int)(((lightStartColor[light][currentIndex[light]] >> 8) & 0xff) * (1 - mult) + ((lightEndColor[light][currentIndex[light]] >> 8) & 0xff) * mult) << 8) | ((int)(((lightStartColor[light][currentIndex[light]] >> 0) & 0xff) * (1 - mult) + ((lightEndColor[light][currentIndex[light]] >> 0) & 0xff) * mult) << 0);
			dotStar->SetPixelColor(light, result);
		}
	}
}
