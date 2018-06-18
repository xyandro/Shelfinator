#pragma once

#include "Lights.h"

namespace Shelfinator
{
	class Pattern
	{
	public:
		typedef std::shared_ptr<Pattern> ptr;

		static ptr Read(const char *fileName);

		void SetLights(int time, Lights::ptr lights);
		int GetLength();
	private:
		class LightItem
		{
		public:
			int startTime, endTime, origEndTime, startColorIndex, startColorValue, endColorIndex, endColorValue, intermediates;
			void Read(FILE *file);
			double GetPercent(double time);
			double GetSameIndexColorValue(double time);
		};
		class Light
		{
		public:
			void Read(FILE *file);
			LightItem &LightAtTime(double time);
		private:
			std::vector<LightItem> lights;
			int current = 0;
		};
		std::vector<Light> lightData;
		void ReadLights(FILE *file);

		class LightSequence
		{
		public:
			int lightStartTime, lightEndTime, startTime, endTime;
			void Read(FILE *file, int &length);
			double GetLightTime(int time);
		};
		class LightSequences
		{
		public:
			void Read(FILE *file, int &length);
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
			void Read(FILE *file);
			void GradientColor(double value, int index, double &red, double &green, double &blue);
		};
		std::vector<LightColor> lightColors;
		void ReadColors(FILE *file);

		class PaletteSequence
		{
		public:
			int startTime, endTime, startPaletteIndex, endPaletteIndex;
			void Read(FILE *file);
			double GetPercent(int time);
		};
		class PaletteSequences
		{
		public:
			void Read(FILE *file);
			PaletteSequence &SequenceAtTime(int time);
		private:
			std::vector<PaletteSequence> paletteSequences;
			int current = 0;
		};
		PaletteSequences paletteSequences;

		Pattern(FILE *file);
		static bool ReadBool(FILE *file);
		static int ReadInt(FILE *file);
	};
}
