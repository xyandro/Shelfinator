#include "stdafx.h"
#include "Helpers.h"

namespace Shelfinator
{
	int Helpers::CreateColor(int red, int green, int blue) { return (red << 16) | (green << 8) | (blue); }
	int Helpers::GetRed(int color) { return ToByte((color >> 16) & 0xff); }
	int Helpers::GetGreen(int color) { return ToByte((color >> 8) & 0xff); }
	int Helpers::GetBlue(int color) { return ToByte(color & 0xff); }
	int Helpers::MultiplyColor(int color, double multiplier) { return CreateColor(ToByte(GetRed(color) * multiplier), ToByte(GetGreen(color) * multiplier), ToByte(GetBlue(color) * multiplier)); }
	int Helpers::AddColor(int color1, int color2) { return CreateColor(ToByte(GetRed(color1) + GetRed(color2)), ToByte(GetGreen(color1) + GetGreen(color2)), ToByte(GetBlue(color1) + GetBlue(color2))); }
	int Helpers::GradientColor(int color1, int color2, double percent) { return AddColor(MultiplyColor(color1, 1 - percent), MultiplyColor(color2, percent)); }

	int Helpers::ToByte(int value)
	{
		if (value < 0)
			return 0;
		if (value > 255)
			return 255;
		return value;
	}

	int Helpers::ToByte(double value) { return ToByte((int)(value + 0.5)); }
}
