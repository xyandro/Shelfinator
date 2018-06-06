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
		ReadLights(file);
		ReadLightSequences(file);
		ReadColors(file);
		ReadPaletteSequences(file);
	}

	Pattern::~Pattern()
	{
		FreeLights();
		FreeLightSequences();
		FreeColors();
		FreePaletteSequences();
	}

	void Pattern::ReadLights(FILE *file)
	{
		numLights = ReadInt(file);
		lightStartTime = new int*[numLights];
		lightEndTime = new int*[numLights];
		lightOrigEndTime = new int*[numLights];
		lightStartColorIndex = new int*[numLights];
		lightStartColorValue = new int*[numLights];
		lightEndColorIndex = new int*[numLights];
		lightEndColorValue = new int*[numLights];
		currentIndex = new int[numLights];

		for (auto lightCtr = 0; lightCtr < numLights; ++lightCtr)
		{
			auto dataCount = ReadInt(file);
			lightStartTime[lightCtr] = new int[dataCount];
			lightEndTime[lightCtr] = new int[dataCount];
			lightOrigEndTime[lightCtr] = new int[dataCount];
			lightStartColorIndex[lightCtr] = new int[dataCount];
			lightStartColorValue[lightCtr] = new int[dataCount];
			lightEndColorIndex[lightCtr] = new int[dataCount];
			lightEndColorValue[lightCtr] = new int[dataCount];
			currentIndex[lightCtr] = 0;

			for (auto dataCtr = 0; dataCtr < dataCount; ++dataCtr)
			{
				lightStartTime[lightCtr][dataCtr] = ReadInt(file);
				lightEndTime[lightCtr][dataCtr] = ReadInt(file);
				lightOrigEndTime[lightCtr][dataCtr] = ReadInt(file);
				lightStartColorIndex[lightCtr][dataCtr] = ReadInt(file);
				lightStartColorValue[lightCtr][dataCtr] = ReadInt(file);
				lightEndColorIndex[lightCtr][dataCtr] = ReadInt(file);
				lightEndColorValue[lightCtr][dataCtr] = ReadInt(file);
			}
		}
	}

	void Pattern::FreeLights()
	{
		for (auto ctr = 0; ctr < numLights; ++ctr)
		{
			delete[] lightStartTime[ctr];
			delete[] lightEndTime[ctr];
			delete[] lightOrigEndTime[ctr];
			delete[] lightStartColorIndex[ctr];
			delete[] lightStartColorValue[ctr];
			delete[] lightEndColorIndex[ctr];
			delete[] lightEndColorValue[ctr];
		}
		delete[] lightStartTime;
		delete[] lightEndTime;
		delete[] lightOrigEndTime;
		delete[] lightStartColorIndex;
		delete[] lightStartColorValue;
		delete[] lightEndColorIndex;
		delete[] lightEndColorValue;
		delete[] currentIndex;
	}

	void Pattern::ReadLightSequences(FILE *file)
	{
		auto numLightSequences = ReadInt(file);
		lightSequenceLightStartTime = new int[numLightSequences];
		lightSequenceLightEndTime = new int[numLightSequences];
		lightSequenceStartTime = new int[numLightSequences];
		lightSequenceEndTime = new int[numLightSequences];
		length = 0;
		for (auto lightSequenceCtr = 0; lightSequenceCtr < numLightSequences; ++lightSequenceCtr)
		{
			lightSequenceLightStartTime[lightSequenceCtr] = ReadInt(file);
			lightSequenceLightEndTime[lightSequenceCtr] = ReadInt(file);
			lightSequenceStartTime[lightSequenceCtr] = length;
			lightSequenceEndTime[lightSequenceCtr] = length = length + ReadInt(file);
		}
	}

	void Pattern::FreeLightSequences()
	{
		delete[] lightSequenceLightStartTime;
		delete[] lightSequenceLightEndTime;
		delete[] lightSequenceStartTime;
		delete[] lightSequenceEndTime;
	}

	void Pattern::ReadColors(FILE *file)
	{
		numColors = ReadInt(file);
		colorMinValue = new int[numColors];
		colorMaxValue = new int[numColors];
		colorCount = new int[numColors];
		colorColorCount = new int*[numColors];
		colorColors = new int**[numColors];
		for (auto colorCtr = 0; colorCtr < numColors; ++colorCtr)
		{
			colorMinValue[colorCtr] = ReadInt(file);
			colorMaxValue[colorCtr] = ReadInt(file);
			colorCount[colorCtr] = ReadInt(file);
			colorColorCount[colorCtr] = new int[colorCount[colorCtr]];
			colorColors[colorCtr] = new int*[colorCount[colorCtr]];
			for (auto colorIndexCtr = 0; colorIndexCtr < colorCount[colorCtr]; ++colorIndexCtr)
			{
				colorColorCount[colorCtr][colorIndexCtr] = ReadInt(file);
				colorColors[colorCtr][colorIndexCtr] = new int[colorColorCount[colorCtr][colorIndexCtr]];
				for (auto colorColorCountCtr = 0; colorColorCountCtr < colorColorCount[colorCtr][colorIndexCtr]; ++colorColorCountCtr)
					colorColors[colorCtr][colorIndexCtr][colorColorCountCtr] = ReadInt(file);
			}
		}
	}

	void Pattern::FreeColors()
	{
		for (auto colorCtr = 0; colorCtr < numColors; ++colorCtr)
		{
			for (auto ctr = 0; ctr < colorCount[colorCtr]; ++ctr)
				delete[] colorColors[colorCtr][ctr];
			delete[] colorColors[colorCtr];
			delete[] colorColorCount[colorCtr];
		}
		delete[] colorMinValue;
		delete[] colorMaxValue;
		delete[] colorCount;
		delete[] colorColorCount;
		delete[] colorColors;
	}

	void Pattern::ReadPaletteSequences(FILE *file)
	{
		auto numPaletteSequences = ReadInt(file);
		paletteSequenceStartTime = new int[numPaletteSequences];
		paletteSequenceEndTime = new int[numPaletteSequences];
		paletteSequenceStartColorIndex = new int[numPaletteSequences];
		paletteSequenceEndColorIndex = new int[numPaletteSequences];
		for (auto paletteSequenceCtr = 0; paletteSequenceCtr < numPaletteSequences; ++paletteSequenceCtr)
		{
			paletteSequenceStartTime[paletteSequenceCtr] = ReadInt(file);
			paletteSequenceEndTime[paletteSequenceCtr] = ReadInt(file);
			paletteSequenceStartColorIndex[paletteSequenceCtr] = ReadInt(file);
			paletteSequenceEndColorIndex[paletteSequenceCtr] = ReadInt(file);
		}
	}

	void Pattern::FreePaletteSequences()
	{
		delete[] paletteSequenceStartTime;
		delete[] paletteSequenceEndTime;
		delete[] paletteSequenceStartColorIndex;
		delete[] paletteSequenceEndColorIndex;
	}

	void Pattern::SetLights(int time, DotStar::ptr dotStar)
	{
		if (time < 0)
			return;

		while (time < paletteSequenceStartTime[currentPaletteSequence])
			--currentPaletteSequence;
		while (time >= paletteSequenceEndTime[currentPaletteSequence])
			++currentPaletteSequence;

		auto startPaletteIndex = paletteSequenceStartColorIndex[currentPaletteSequence];
		auto endPaletteIndex = paletteSequenceEndColorIndex[currentPaletteSequence];
		auto palettePercent = startPaletteIndex == endPaletteIndex ? 0 : ((double)time - paletteSequenceStartTime[currentPaletteSequence]) / (paletteSequenceEndTime[currentPaletteSequence] - paletteSequenceStartTime[currentPaletteSequence]);

		while (time < lightSequenceStartTime[currentLightSequence])
			--currentLightSequence;
		while (time >= lightSequenceEndTime[currentLightSequence])
			++currentLightSequence;

		auto lightTime = ((double)time - lightSequenceStartTime[currentLightSequence]) / (lightSequenceEndTime[currentLightSequence] - lightSequenceStartTime[currentLightSequence]) * (lightSequenceLightEndTime[currentLightSequence] - lightSequenceLightStartTime[currentLightSequence]) + lightSequenceLightStartTime[currentLightSequence];

		for (auto light = 0; light < numLights; ++light)
		{
			while (lightTime < lightStartTime[light][currentIndex[light]])
				--currentIndex[light];
			while (lightTime >= lightEndTime[light][currentIndex[light]])
				++currentIndex[light];

			auto startColorIndex = lightStartColorIndex[light][currentIndex[light]];
			auto startColorValue = lightStartColorValue[light][currentIndex[light]];
			auto endColorIndex = lightEndColorIndex[light][currentIndex[light]];
			auto endColorValue = lightEndColorValue[light][currentIndex[light]];
			auto colorPercent = (lightTime - lightStartTime[light][currentIndex[light]]) / (lightOrigEndTime[light][currentIndex[light]] - lightStartTime[light][currentIndex[light]]);

			auto startColorStartPaletteIndex = startPaletteIndex % colorCount[startColorIndex];
			auto startColorEndPaletteIndex = endPaletteIndex % colorCount[startColorIndex];
			auto endColorStartPaletteIndex = startPaletteIndex % colorCount[endColorIndex];
			auto endColorEndPaletteIndex = endPaletteIndex % colorCount[endColorIndex];

			double redStart, greenStart, blueStart, red1, green1, blue1, red2, green2, blue2;
			Helpers::GradientColor(startColorValue, colorMinValue[startColorIndex], colorMaxValue[startColorIndex], colorColors[startColorIndex][startColorStartPaletteIndex], colorColorCount[startColorIndex][startColorStartPaletteIndex], red1, green1, blue1);
			Helpers::GradientColor(startColorValue, colorMinValue[startColorIndex], colorMaxValue[startColorIndex], colorColors[startColorIndex][startColorEndPaletteIndex], colorColorCount[startColorIndex][startColorEndPaletteIndex], red2, green2, blue2);
			Helpers::GradientColor(red1, green1, blue1, red2, green2, blue2, palettePercent, redStart, greenStart, blueStart);

			double redEnd, greenEnd, blueEnd;
			Helpers::GradientColor(endColorValue, colorMinValue[endColorIndex], colorMaxValue[endColorIndex], colorColors[endColorIndex][endColorStartPaletteIndex], colorColorCount[endColorIndex][endColorStartPaletteIndex], red1, green1, blue1);
			Helpers::GradientColor(endColorValue, colorMinValue[endColorIndex], colorMaxValue[endColorIndex], colorColors[endColorIndex][endColorEndPaletteIndex], colorColorCount[endColorIndex][endColorEndPaletteIndex], red2, green2, blue2);
			Helpers::GradientColor(red1, green1, blue1, red2, green2, blue2, palettePercent, redEnd, greenEnd, blueEnd);

			double red, green, blue;
			Helpers::GradientColor(redStart, greenStart, blueStart, redEnd, greenEnd, blueEnd, colorPercent, red, green, blue);
			dotStar->SetPixelColor(light, Helpers::CombineColor(red, green, blue));
		}
	}

	int Pattern::GetLength()
	{
		return length;
	}

	int Pattern::ReadInt(FILE *file)
	{
		int value;
		fread(&value, 1, sizeof(value), file);
		return value;
	}
}
