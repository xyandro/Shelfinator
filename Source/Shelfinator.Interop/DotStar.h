#pragma once

#include <vcclr.h>
#include "../Shelfinator.Runner/IDotStar.h"
#include "IDotStar.h"

namespace Shelfinator
{
	namespace Interop
	{
		class DotStar : public Runner::IDotStar
		{
		public:
			typedef std::shared_ptr<DotStar> ptr;
			static ptr Create(Interop::IDotStar ^dotStar);
			void Show(int *lights, int count);
		private:
			gcroot<Interop::IDotStar^> dotStar;
			DotStar(Interop::IDotStar ^dotStar);
		};
	}
}
