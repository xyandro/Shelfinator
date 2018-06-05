#pragma once

namespace Shelfinator
{
	class Helpers
	{
	public:
		static int CreateColor(int red, int green, int blue);
		static int GetRed(int color);
		static int GetGreen(int color);
		static int GetBlue(int color);

		static int MultiplyColor(int color, double multiplier);
		static int AddColor(int color1, int color2);
		static int GradientColor(int color1, int color2, double percent);
		static int GradientColor(double value, int minValue, int maxValue, int *colors, int colorCount);
	private:
		static int ToByte(int value);
		static int ToByte(double value);
	};
}
