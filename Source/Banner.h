#pragma once

#include "DotStar.h"

namespace Shelfinator
{
	class Banner
	{
	public:
		typedef std::shared_ptr<Banner> ptr;
		static ptr Create(std::string text, int time, int spacing = -1);
		~Banner();
		void SetLights(DotStar::ptr dotStar);
		void AddElapsed(int delta);
		bool Expired();
	private:
		static int lightPosition[8][32];
		bool **grid;
		int elapsed = 0, time, width;

		Banner(std::string text, int time, int spacing = -1);
	};
}
