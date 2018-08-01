#pragma once

#include <vector>
#include "BufferFile.h"
#include "Lights.h"

namespace Shelfinator
{
	namespace Runner
	{
		class Pattern
		{
		public:
			typedef std::shared_ptr<Pattern> ptr;

			static ptr Read(const char *fileName);

			void SetLights(int time, Lights::ptr lights);
			int GetLength();
#ifndef INTEROP
		private:
			class LightItem
			{
			public:
				int startTime, endTime, origEndTime, startColorIndex, startColorValue, endColorIndex, endColorValue, intermediates;
				void Read(BufferFile::ptr file);
				double GetPercent(double time);
				double GetSameIndexColorValue(double time);
			};
			class Light
			{
			public:
				void Read(BufferFile::ptr file);
				LightItem &LightAtTime(double time);
			private:
				std::vector<LightItem> lights;
				int current = 0;
			};
			std::vector<Light> lightData;
			void ReadLights(BufferFile::ptr file);

			class LightSequence
			{
			public:
				int lightStartTime, lightEndTime, duration, startTime, endTime;
				void Read(BufferFile::ptr file, int &length);
				double GetLightTime(int time);
			};
			class LightSequences
			{
			public:
				void Read(BufferFile::ptr file, int &length);
				LightSequence & SequenceAtTime(int time);
			private:
				std::vector<LightSequence> lightSequences;
				int current = 0;
			};
			int length = 0;
			LightSequences lightSequences;

			class LightColor
			{
			public:
				int minValue, maxValue;
				std::vector<std::vector<int>> colors;
				void Read(BufferFile::ptr file);
				void GradientColor(double value, int index, double &red, double &green, double &blue);
			};
			std::vector<LightColor> lightColors;
			void ReadColors(BufferFile::ptr file);

			class PaletteSequence
			{
			public:
				int startTime, endTime, startPaletteIndex, endPaletteIndex;
				void Read(BufferFile::ptr file);
				double GetPercent(int time);
			};
			class PaletteSequences
			{
			public:
				void Read(BufferFile::ptr file);
				PaletteSequence &SequenceAtTime(int time);
			private:
				std::vector<PaletteSequence> paletteSequences;
				int current = 0;
			};
			PaletteSequences paletteSequences;

			Pattern(BufferFile::ptr file);
#endif
		};
	}
}
