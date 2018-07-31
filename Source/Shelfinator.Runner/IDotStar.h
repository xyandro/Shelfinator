﻿#pragma once

namespace Shelfinator
{
	namespace Runner
	{
		class IDotStar
		{
		public:
			typedef std::shared_ptr<IDotStar> ptr;
			virtual void Show(int *lights, int count) = 0;
		};
	}
}
