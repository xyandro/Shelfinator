﻿#include "Banner.h"

#include <string.h>
#include <wctype.h>
#include "Helpers.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace
		{
#pragma pack(push, 1)
			struct fontdata
			{
				unsigned char count;
				struct fontchardata
				{
					short ch;
					char width;
					char data[8];
				} chars[1];
			};
#pragma pack(pop)

			unsigned char fontbinary[] =
			{
				71,
				32, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0,
				33, 0, 2, 192, 192, 192, 192, 192, 0, 192, 192,
				34, 0, 6, 204, 204, 0, 0, 0, 0, 0, 0,
				35, 0, 7, 108, 108, 254, 108, 108, 254, 108, 108,
				36, 0, 6, 48, 120, 204, 96, 24, 204, 120, 48,
				37, 0, 6, 204, 204, 24, 48, 48, 96, 204, 204,
				38, 0, 6, 120, 192, 112, 96, 204, 204, 120, 60,
				39, 0, 2, 192, 192, 0, 0, 0, 0, 0, 0,
				40, 0, 4, 48, 96, 192, 192, 192, 192, 96, 48,
				41, 0, 4, 192, 96, 48, 48, 48, 48, 96, 192,
				42, 0, 6, 180, 180, 120, 120, 120, 120, 180, 180,
				43, 0, 6, 0, 48, 48, 252, 252, 48, 48, 0,
				44, 0, 3, 0, 0, 0, 0, 0, 96, 96, 192,
				45, 0, 6, 0, 0, 0, 252, 252, 0, 0, 0,
				46, 0, 2, 0, 0, 0, 0, 0, 0, 192, 192,
				47, 0, 5, 24, 24, 48, 48, 96, 96, 192, 192,
				48, 0, 6, 120, 252, 204, 204, 204, 204, 252, 120,
				49, 0, 6, 48, 112, 240, 48, 48, 48, 252, 252,
				50, 0, 6, 120, 252, 204, 24, 48, 96, 252, 252,
				51, 0, 6, 248, 252, 12, 120, 120, 12, 252, 248,
				52, 0, 6, 204, 204, 204, 252, 124, 12, 12, 12,
				53, 0, 6, 252, 252, 192, 248, 124, 12, 252, 248,
				54, 0, 6, 120, 248, 192, 248, 252, 204, 252, 120,
				55, 0, 6, 248, 252, 12, 24, 48, 96, 192, 192,
				56, 0, 6, 120, 252, 204, 120, 252, 204, 252, 120,
				57, 0, 6, 120, 252, 204, 252, 124, 12, 124, 120,
				58, 0, 2, 0, 192, 192, 0, 0, 192, 192, 0,
				59, 0, 3, 0, 0, 96, 96, 0, 96, 96, 192,
				60, 0, 5, 24, 48, 96, 192, 192, 96, 48, 24,
				61, 0, 6, 0, 252, 252, 0, 0, 252, 252, 0,
				62, 0, 5, 192, 96, 48, 24, 24, 48, 96, 192,
				63, 0, 6, 120, 204, 12, 24, 48, 0, 48, 48,
				64, 0, 8, 126, 195, 219, 219, 223, 192, 195, 126,
				65, 0, 6, 48, 120, 252, 204, 204, 252, 204, 204,
				66, 0, 6, 240, 204, 204, 240, 240, 204, 204, 240,
				67, 0, 6, 120, 204, 204, 192, 192, 204, 204, 120,
				68, 0, 6, 248, 204, 204, 204, 204, 204, 204, 248,
				69, 0, 6, 252, 192, 192, 252, 192, 192, 192, 252,
				70, 0, 6, 252, 192, 192, 252, 192, 192, 192, 192,
				71, 0, 6, 120, 204, 204, 192, 220, 204, 204, 120,
				72, 0, 6, 204, 204, 204, 252, 204, 204, 204, 204,
				73, 0, 6, 252, 48, 48, 48, 48, 48, 48, 252,
				74, 0, 6, 252, 12, 12, 12, 12, 12, 204, 120,
				75, 0, 6, 204, 216, 240, 224, 224, 240, 216, 204,
				76, 0, 6, 192, 192, 192, 192, 192, 192, 192, 252,
				77, 0, 8, 195, 247, 255, 219, 195, 195, 195, 195,
				78, 0, 6, 204, 236, 252, 220, 204, 204, 204, 204,
				79, 0, 6, 120, 204, 204, 204, 204, 204, 204, 120,
				80, 0, 6, 248, 204, 204, 204, 248, 192, 192, 192,
				81, 0, 6, 120, 204, 204, 204, 204, 204, 220, 124,
				82, 0, 6, 248, 204, 204, 204, 248, 216, 204, 204,
				83, 0, 6, 120, 204, 192, 120, 12, 12, 204, 120,
				84, 0, 6, 252, 48, 48, 48, 48, 48, 48, 48,
				85, 0, 6, 204, 204, 204, 204, 204, 204, 204, 120,
				86, 0, 6, 204, 204, 204, 204, 204, 204, 120, 48,
				87, 0, 8, 195, 195, 195, 195, 219, 255, 247, 195,
				88, 0, 6, 204, 204, 120, 48, 120, 204, 204, 204,
				89, 0, 6, 204, 204, 120, 48, 48, 48, 48, 48,
				90, 0, 6, 252, 12, 24, 48, 96, 192, 192, 252,
				91, 0, 4, 240, 192, 192, 192, 192, 192, 192, 240,
				92, 0, 5, 192, 192, 96, 96, 48, 48, 24, 24,
				93, 0, 4, 240, 48, 48, 48, 48, 48, 48, 240,
				95, 0, 2, 0, 0, 0, 0, 0, 0, 192, 192,
				123, 0, 6, 60, 48, 48, 224, 224, 48, 48, 60,
				124, 0, 2, 192, 192, 192, 192, 192, 192, 192, 192,
				125, 0, 5, 240, 48, 48, 24, 24, 48, 48, 240,
				22, 32, 8, 231, 231, 231, 231, 231, 231, 231, 231,
				182, 37, 8, 192, 240, 252, 255, 255, 252, 240, 192,
				192, 37, 8, 3, 15, 63, 255, 255, 63, 15, 3,
				34, 32, 8, 128, 0, 0, 0, 0, 0, 0, 0,
				0, 32, 8, 0, 0, 0, 0, 0, 0, 0, 0,
			};

			fontdata::fontchardata &GetChar(wchar_t ch)
			{
				ch = towupper(ch);
				auto font = (fontdata*)&fontbinary;
				for (auto ctr = 0; ctr < font->count; ++ctr)
					if (font->chars[ctr].ch == ch)
						return font->chars[ctr];
				throw "Cannot find letter";
			}
		}

		int Banner::lightPosition[8][32] =
		{
			{ 255, 270, 271, 286, 287, 302, 303, 318, 319, 334, 335, 350, 351, 366, 367, 382, 383, 398, 399, 414, 415, 430, 431, 446, 447, 462, 463, 478, 479, 494, 495, 510 },
			{ 256, 269, 272, 285, 288, 301, 304, 317, 320, 333, 336, 349, 352, 365, 368, 381, 384, 397, 400, 413, 416, 429, 432, 445, 448, 461, 464, 477, 480, 493, 496, 509 },
			{ 257, 268, 273, 284, 289, 300, 305, 316, 321, 332, 337, 348, 353, 364, 369, 380, 385, 396, 401, 412, 417, 428, 433, 444, 449, 460, 465, 476, 481, 492, 497, 508 },
			{ 258, 267, 274, 283, 290, 299, 306, 315, 322, 331, 338, 347, 354, 363, 370, 379, 386, 395, 402, 411, 418, 427, 434, 443, 450, 459, 466, 475, 482, 491, 498, 507 },
			{ 259, 266, 275, 282, 291, 298, 307, 314, 323, 330, 339, 346, 355, 362, 371, 378, 387, 394, 403, 410, 419, 426, 435, 442, 451, 458, 467, 474, 483, 490, 499, 506 },
			{ 260, 265, 276, 281, 292, 297, 308, 313, 324, 329, 340, 345, 356, 361, 372, 377, 388, 393, 404, 409, 420, 425, 436, 441, 452, 457, 468, 473, 484, 489, 500, 505 },
			{ 261, 264, 277, 280, 293, 296, 309, 312, 325, 328, 341, 344, 357, 360, 373, 376, 389, 392, 405, 408, 421, 424, 437, 440, 453, 456, 469, 472, 485, 488, 501, 504 },
			{ 262, 263, 278, 279, 294, 295, 310, 311, 326, 327, 342, 343, 358, 359, 374, 375, 390, 391, 406, 407, 422, 423, 438, 439, 454, 455, 470, 471, 486, 487, 502, 503 },
		};

		Banner::ptr Banner::Create(std::wstring text, int scrollDuration, int fadeDuration, int spacing, int color)
		{
			return ptr(new Banner(text, scrollDuration, fadeDuration, spacing, color));
		}

		Banner::Banner(std::wstring text, int scrollDuration, int fadeDuration, int spacing, int color)
		{
			timer = Timer::Create();
			this->scrollDuration = scrollDuration;
			this->fadeDuration = fadeDuration;
			this->color = color;

			width = 0;
			for (auto ctr = 0; ctr < text.length(); ++ctr)
				width += GetChar(text[ctr]).width;

			if (spacing < 0)
			{
				spacing = (32 - width) / ((int)text.length() + 1);
				if (spacing < 0)
					spacing = 0;
			}
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
					for (auto x = 0; x < charData.width; ++x)
						grid[y][xOfs + x] = (charData.data[y] & (1 << (7 - x))) != 0;
				xOfs += charData.width + spacing;
			}
		}

		Banner::~Banner()
		{
			for (auto ctr = 0; ctr < 8; ++ctr)
				delete[] grid[ctr];
			delete[] grid;
		}

		void Banner::SetLights(Lights::ptr lights)
		{
			auto elapsed = timer->Elapsed();
			auto x1 = scrollDuration <= 0 ? 0 : elapsed * (width - 31) / scrollDuration;
			if (x1 > width - 31)
				x1 = width - 31;
			if (x1 < 0)
				x1 = 0;

			auto x2 = x1 + 32;
			if (x2 > width)
				x2 = width;

			auto xOfs = (32 - x2 + x1) / 2 - x1;

			auto fade = fadeDuration <= 0 ? 1 : 1 - ((double)elapsed - scrollDuration) / fadeDuration;
			if (fade < 0)
				fade = 0;
			if (fade > 1)
				fade = 1;

			auto useColor = Helpers::MultiplyColor(color, fade);
			for (auto y = 0; y < 8; ++y)
				for (auto x = x1; x < x2; ++x)
					if (grid[y][x])
						lights->SetLight(lightPosition[y][xOfs + x], useColor);
		}

		bool Banner::Expired()
		{
			return timer->Elapsed() >= scrollDuration + fadeDuration;
		}
	}
}
