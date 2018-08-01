#include "Driver.h"

#include "DotStar.h"
#include "Remote.h"

namespace Shelfinator
{
	namespace Interop
	{
		Driver::Driver(System::Collections::Generic::List<int> ^patternNumbers, IDotStar ^dotStar, IRemote ^remote)
		{
			auto nativePatternNumbers = new int[patternNumbers->Count];
			for (auto ctr = 0; ctr < patternNumbers->Count; ++ctr)
				nativePatternNumbers[ctr] = patternNumbers[ctr];
			driver = new Runner::Driver::ptr(Runner::Driver::Create(nativePatternNumbers, patternNumbers->Count, DotStar::Create(dotStar), Remote::Create(remote)));
			delete[] nativePatternNumbers;
		}

		Driver::~Driver()
		{
			delete driver;
		}

		void Driver::Run()
		{
			(*driver)->Run();
		}

		void Driver::Stop()
		{
			(*driver)->Stop();
		}
	}
}
