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
			auto arr = gcnew array<int>(9665);
			System::Runtime::InteropServices::Marshal::Copy((System::IntPtr)lights, arr, 0, 9665);
			return dotStar->Show(arr);
		}
	}
}
