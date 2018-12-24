#pragma once

#include <string>
#include "Lights.h"
#include "Timer.h"

namespace Shelfinator
{
	namespace Runner
	{
		class Banner
		{
		public:
			typedef std::shared_ptr<Banner> ptr;
			static ptr Create(std::wstring text, int scrollDuration, int fadeDuration, int spacing = -1, int color = 0x101010);
			~Banner();
			void SetLights(Lights::ptr lights);
			bool Expired();
		private:
			static int lightPosition[8][32];
			bool **grid;
			int scrollDuration, fadeDuration, color, width;
			Timer::ptr timer;

			Banner(std::wstring text, int scrollDuration, int fadeDuration, int spacing = -1, int color = 0x101010);
		};
	}
}
