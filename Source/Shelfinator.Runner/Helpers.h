#pragma once

namespace Shelfinator
{
	namespace Runner
	{
		class Helpers
		{
		public:
			static void SplitColor(int color, double &red, double &green, double &blue);
			static int CombineColor(double red, double green, double blue);
			static void MultiplyColor(double red1, double green1, double blue1, double multiplier, double &red, double &green, double &blue);
			static void GradientColor(double red1, double green1, double blue1, double red2, double green2, double blue2, double percent, double &red, double &green, double &blue);
			static void GradientColor(double value, int minValue, int maxValue, int *colors, int colorCount, double &red, double &green, double &blue);
			static void AddColor(double red1, double green1, double blue1, double red2, double green2, double blue2, double &red, double &green, double &blue);

			static int MultiplyColor(int color, double multiplier);

			static double FPart(double value);
		private:
			static int ToByte(double value);
		};
	}
}
