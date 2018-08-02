#include "Driver.h"

#include "DotStar.h"
#include "Remote.h"

namespace Shelfinator
{
	namespace Interop
	{
		Driver::Driver(IDotStar ^dotStar, IRemote ^remote)
		{
			driver = new Runner::Driver::ptr(Runner::Driver::Create(DotStar::Create(dotStar), Remote::Create(remote)));
		}

		Driver::~Driver()
		{
			delete driver;
		}

		void Driver::Run(System::Collections::Generic::List<int> ^patternNumbers)
		{
			auto nativePatternNumbers = new int[patternNumbers->Count];
			for (auto ctr = 0; ctr < patternNumbers->Count; ++ctr)
				nativePatternNumbers[ctr] = patternNumbers[ctr];
			(*driver)->Run(nativePatternNumbers, patternNumbers->Count);
			delete[] nativePatternNumbers;
		}

		void Driver::Stop()
		{
			(*driver)->Stop();
		}
	}
}
