#include "stdafx.h"
#include "Pattern.h"

#include "Helpers.h"

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
		int numSequences;
		fread(&numSequences, 1, sizeof(numSequences), file);
		sequenceLightStartTime = new int[numSequences];
		sequenceLightEndTime = new int[numSequences];
		sequenceStartTime = new int[numSequences];
		sequenceEndTime = new int[numSequences];
		auto sequenceCtr = 0, time = 0;
		while (sequenceCtr < numSequences)
		{
			int lightStartTime, lightEndTime, duration, repeat;
			fread(&lightStartTime, 1, sizeof(lightStartTime), file);
			fread(&lightEndTime, 1, sizeof(lightEndTime), file);
			fread(&duration, 1, sizeof(duration), file);
			fread(&repeat, 1, sizeof(repeat), file);
			for (auto ctr = 0; ctr < repeat; ++ctr)
			{
				sequenceLightStartTime[sequenceCtr] = lightStartTime;
				sequenceLightEndTime[sequenceCtr] = lightEndTime;
				sequenceStartTime[sequenceCtr] = time;
				sequenceEndTime[sequenceCtr] = length = time + duration;
				++sequenceCtr;
				time += duration;
			}
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
		delete[] sequenceLightStartTime;
		delete[] sequenceLightEndTime;

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

	void Pattern::SetLights(int time, DotStar::ptr dotStar)
	{
		if (time < 0)
			return;

		while (time < sequenceStartTime[currentSequence])
			--currentSequence;
		while (time >= sequenceEndTime[currentSequence])
			++currentSequence;

		auto lightTime = (time - sequenceStartTime[currentSequence]) * (sequenceLightEndTime[currentSequence] - sequenceLightStartTime[currentSequence]) / (sequenceEndTime[currentSequence] - sequenceStartTime[currentSequence]) + sequenceLightStartTime[currentSequence];

		for (auto light = 0; light < numLights; ++light)
		{
			while (lightTime < lightStartTime[light][currentIndex[light]])
				--currentIndex[light];
			while (lightTime >= lightEndTime[light][currentIndex[light]])
				++currentIndex[light];

			auto mult = (double)(lightTime - lightStartTime[light][currentIndex[light]]) / (lightEndTime[light][currentIndex[light]] - lightStartTime[light][currentIndex[light]]);
			auto result = Helpers::GradientColor(lightStartColor[light][currentIndex[light]], lightEndColor[light][currentIndex[light]], mult);
			dotStar->SetPixelColor(light, result);
		}
	}

	int Pattern::GetLength()
	{
		return length;
	}
}
