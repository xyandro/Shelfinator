#pragma once

#include <string>
#include "Lights.h"

namespace Shelfinator
{
	namespace Runner
	{
		class Banner
		{
		public:
			typedef std::shared_ptr<Banner> ptr;
			static ptr Create(std::wstring text, int scrollTime, int fadeTime, int spacing = -1, int color = 0x101010);
			~Banner();
			void SetLights(Lights::ptr lights);
			void AddElapsed(int delta);
			bool Expired();
		private:
			static int lightPosition[8][32];
			bool **grid;
			int elapsed = 0, scrollTime, fadeTime, color, width;

			Banner(std::wstring text, int scrollTime, int fadeTime, int spacing = -1, int color = 0x101010);
		};
	}
}
