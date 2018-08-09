#include "DotStar.h"

namespace Shelfinator
{
	namespace Interop
	{
		DotStar::DotStar(Interop::IDotStar ^dotStar)
		{
			this->dotStar = dotStar;
		}

		DotStar::ptr DotStar::Create(Interop::IDotStar ^dotStar)
		{
			return ptr(new DotStar(dotStar));
		}

		void DotStar::Show(int *lights, int count)
		{
			return dotStar->Show(lights, count);
		}
	}
}
