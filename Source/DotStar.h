﻿#pragma once

#include <vcclr.h>
#include <memory>

namespace Shelfinator
{
	public interface class IDotStar
	{
		void Clear();
		void SetPixelColor(int led, int color);
		void Show();
	};

	class DotStar
	{
	public:
		typedef std::shared_ptr<DotStar> ptr;
		static ptr Create(IDotStar^);
		void Clear();
		void SetPixelColor(int led, int color);
		void Show();
	private:
		DotStar(IDotStar ^dotStar);
		gcroot<IDotStar^> dotStar;
	};
}
