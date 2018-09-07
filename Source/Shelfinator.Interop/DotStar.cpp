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

		void DotStar::Show(int *lights)
		{
			auto arr = gcnew array<int>(2440);
			System::Runtime::InteropServices::Marshal::Copy((System::IntPtr)lights, arr, 0, 2440);
			return dotStar->Show(arr);
		}
	}
}
