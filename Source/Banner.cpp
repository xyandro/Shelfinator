#include "stdafx.h"
#include "Banner.h"

#include "Helpers.h"

namespace Shelfinator
{
	namespace
	{
#pragma pack(push, 1)
		struct fontdata
		{
			unsigned char count;
			struct fontchardata
			{
				char ch;
				char width;
				char data[8];
			} chars[1];
		};
#pragma pack(pop)

		unsigned char fontbinary[] =
		{
			14,
			'0', 6, 0x78, 0xfc, 0xcc, 0xcc, 0xcc, 0xcc, 0xfc, 0x78,
			'1', 6, 0x30, 0x70, 0xf0, 0x30, 0x30, 0x30, 0xfc, 0xfc,
			'2', 6, 0x78, 0xfc, 0xcc, 0x18, 0x30, 0x60, 0xfc, 0xfc,
			'3', 6, 0xf8, 0xfc, 0x0c, 0x78, 0x78, 0x0c, 0xfc, 0xf8,
			'4', 6, 0xcc, 0xcc, 0xcc, 0xfc, 0x7c, 0x0c, 0x0c, 0x0c,
			'5', 6, 0xfc, 0xfc, 0xc0, 0xf8, 0x7c, 0x0c, 0xfc, 0xf8,
			'6', 6, 0x78, 0xf8, 0xc0, 0xf8, 0xfc, 0xcc, 0xfc, 0x78,
			'7', 6, 0xf8, 0xfc, 0x0c, 0x18, 0x30, 0x60, 0xc0, 0xc0,
			'8', 6, 0x78, 0xfc, 0xcc, 0x78, 0xfc, 0xcc, 0xfc, 0x78,
			'9', 6, 0x78, 0xfc, 0xcc, 0xfc, 0x7c, 0x0c, 0x7c, 0x78,
			'/', 5, 0x18, 0x18, 0x30, 0x30, 0x60, 0x60, 0xc0, 0xc0,
			'F', 8, 0xc0, 0xf0, 0xfc, 0xff, 0xff, 0xfc, 0xf0, 0xc0,
			'R', 8, 0x03, 0x0f, 0x3f, 0xff, 0xff, 0x3f, 0x0f, 0x03,
			'P', 8, 0xe7, 0xe7, 0xe7, 0xe7, 0xe7, 0xe7, 0xe7, 0xe7,
		};

		fontdata::fontchardata &GetChar(char ch)
		{
			auto font = (fontdata*)&fontbinary;
			for (auto ctr = 0; ctr < font->count; ++ctr)
				if (font->chars[ctr].ch == ch)
					return font->chars[ctr];
			throw "Cannot find letter";
		}
	}

	int Banner::lightPosition[8][32] =
	{
		{ 0, 15, 16, 31, 32, 47, 48, 63, 64, 79, 80, 95, 96, 111, 112, 127, 128, 143, 144, 159, 160, 175, 176, 191, 192, 207, 208, 223, 224, 239, 240, 255 },
		{ 1, 14, 17, 30, 33, 46, 49, 62, 65, 78, 81, 94, 97, 110, 113, 126, 129, 142, 145, 158, 161, 174, 177, 190, 193, 206, 209, 222, 225, 238, 241, 254 },
		{ 2, 13, 18, 29, 34, 45, 50, 61, 66, 77, 82, 93, 98, 109, 114, 125, 130, 141, 146, 157, 162, 173, 178, 189, 194, 205, 210, 221, 226, 237, 242, 253 },
		{ 3, 12, 19, 28, 35, 44, 51, 60, 67, 76, 83, 92, 99, 108, 115, 124, 131, 140, 147, 156, 163, 172, 179, 188, 195, 204, 211, 220, 227, 236, 243, 252 },
		{ 4, 11, 20, 27, 36, 43, 52, 59, 68, 75, 84, 91, 100, 107, 116, 123, 132, 139, 148, 155, 164, 171, 180, 187, 196, 203, 212, 219, 228, 235, 244, 251 },
		{ 5, 10, 21, 26, 37, 42, 53, 58, 69, 74, 85, 90, 101, 106, 117, 122, 133, 138, 149, 154, 165, 170, 181, 186, 197, 202, 213, 218, 229, 234, 245, 250 },
		{ 6, 9, 22, 25, 38, 41, 54, 57, 70, 73, 86, 89, 102, 105, 118, 121, 134, 137, 150, 153, 166, 169, 182, 185, 198, 201, 214, 217, 230, 233, 246, 249 },
		{ 7, 8, 23, 24, 39, 40, 55, 56, 71, 72, 87, 88, 103, 104, 119, 120, 135, 136, 151, 152, 167, 168, 183, 184, 199, 200, 215, 216, 231, 232, 247, 248 },
	};

	Banner::ptr Banner::Create(std::string text, int time, int spacing)
	{
		return ptr(new Banner(text, time, spacing));
	}

	Banner::Banner(std::string text, int time, int spacing)
	{
		this->time = time;
		width = 0;
		for (auto ctr = 0; ctr < text.length(); ++ctr)
			width += GetChar(text[ctr]).width;

		if (spacing == -1)
			spacing = (32 - width) / ((int)text.length() + 1);
		width += spacing * ((int)text.length() - 1);

		grid = new bool*[8];
		for (auto ctr = 0; ctr < 8; ++ctr)
		{
			grid[ctr] = new bool[width];
			memset(grid[ctr], 0, sizeof(bool) * width);
		}

		auto xOfs = 0;
		for (auto ctr = 0; ctr < text.length(); ++ctr)
		{
			auto charData = GetChar(text[ctr]);
			for (auto y = 0; y < 8; ++y)
				for (auto x = 0; x < 8; ++x)
					grid[y][xOfs + x] = (charData.data[y] & (1 << (7 - x))) != 0;
			xOfs += charData.width + spacing;
		}
	}

	Banner::~Banner()
	{
		delete[] grid;
	}

	void Banner::SetLights(Lights::ptr lights)
	{
		auto xOfs = (32 - width) / 2;
		for (auto y = 0; y < 8; ++y)
			for (auto x = 0; x < width; ++x)
				if (grid[y][x])
					lights->SetLight(lightPosition[y][xOfs + x], Helpers::MultiplyColor(0x101010, 1 - (double)elapsed / time));
	}

	void Banner::AddElapsed(int delta)
	{
		elapsed += delta;
	}

	bool Banner::Expired()
	{
		return elapsed >= time;
	}
}
