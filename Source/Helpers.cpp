#include "stdafx.h"
#include "Helpers.h"

namespace Shelfinator
{
	void Helpers::MultiplyColor(double red1, double green1, double blue1, double multiplier, double &red, double &green, double &blue)
	{
		red = red1 * multiplier;
		green = green1 * multiplier;
		blue = blue1 * multiplier;
	}

	void Helpers::SplitColor(int color, double &red, double &green, double &blue)
	{
		red = (color >> 16) & 0xff;
		green = (color >> 8) & 0xff;
		blue = color & 0xff;
	}

	int Helpers::CombineColor(double red, double green, double blue)
	{
		return (ToByte(red)) << 16 | (ToByte(green) << 8) | ToByte(blue);
	}

	void Helpers::GradientColor(double red1, double green1, double blue1, double red2, double green2, double blue2, double percent, double &red, double &green, double &blue)
	{
		MultiplyColor(red1, green1, blue1, 1 - percent, red1, green1, blue1);
		MultiplyColor(red2, green2, blue2, percent, red2, green2, blue2);
		AddColor(red1, green1, blue1, red2, green2, blue2, red, green, blue);
	}

	void Helpers::GradientColor(double value, int minValue, int maxValue, int *colors, int colorCount, double &red, double &green, double &blue)
	{
		if (colorCount == 0)
		{
			red = green = blue = 0;
			return;
		}
		if ((colorCount == 1) || (value <= minValue))
			return SplitColor(colors[0], red, green, blue);
		if (value >= maxValue)
			return SplitColor(colors[colorCount - 1], red, green, blue);

		auto percent = (value - minValue) / (maxValue - minValue) * (colorCount - 1);
		auto color = (int)percent;
		percent -= color;
		double red1, green1, blue1, red2, green2, blue2;
		SplitColor(colors[color], red1, green1, blue1);
		SplitColor(colors[color + 1], red2, green2, blue2);
		return GradientColor(red1, green1, blue1, red2, green2, blue2, percent, red, green, blue);
	}

	void Helpers::AddColor(double red1, double green1, double blue1, double red2, double green2, double blue2, double &red, double &green, double &blue)
	{
		red = red1 + red2;
		green = green1 + green2;
		blue = blue1 + blue2;
	}

	int Helpers::MultiplyColor(int color, double multiplier)
	{
		double red, green, blue;
		SplitColor(color, red, green, blue);
		MultiplyColor(red, green, blue, multiplier, red, green, blue);
		return CombineColor(red, green, blue);
	}

	int Helpers::ToByte(double value)
	{
		auto intValue = (int)(value + 0.5);
		if (intValue < 0)
			return 0;
		if (intValue > 255)
			return 255;
		return intValue;
	}
}
