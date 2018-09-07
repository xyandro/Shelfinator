#pragma once

#include <memory>

namespace Shelfinator
{
	namespace Runner
	{
		class IDotStar
		{
		public:
			typedef std::shared_ptr<IDotStar> ptr;
			virtual void Show(int *lights) = 0;
		};
	}
}
