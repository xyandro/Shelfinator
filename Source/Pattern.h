#pragma once

#include "DotStar.h"

namespace Shelfinator
{
	class Pattern
	{
	public:
		typedef std::shared_ptr<Pattern> ptr;

		static ptr Read(const char *fileName);
		~Pattern();

		void SetLights(int time, DotStar::ptr dotStar);
		int GetLength();
	private:
		int numLights;
		int **lightStartTime, **lightEndTime, **lightOrigEndTime, **lightStartColorIndex, **lightStartColorValue, **lightEndColorIndex, **lightEndColorValue;
		int *currentIndex;
		void ReadLights(FILE *file);
		void FreeLights();

		int length = 0, currentLightSequence = 0;
		int *lightSequenceLightStartTime, *lightSequenceLightEndTime, *lightSequenceStartTime, *lightSequenceEndTime;
		void ReadLightSequences(FILE *file);
		void FreeLightSequences();

		int numColors;
		int *colorMinValue, *colorMaxValue, *colorCount, **colorColorCount, ***colorColors;
		void ReadColors(FILE *file);
		void FreeColors();

		int currentPaletteSequence = 0;
		int *paletteSequenceStartTime, *paletteSequenceEndTime, *paletteSequenceStartColorIndex, *paletteSequenceEndColorIndex;
		void ReadPaletteSequences(FILE *file);
		void FreePaletteSequences();

		Pattern(FILE *file);
		static int ReadInt(FILE *file);
	};
}
