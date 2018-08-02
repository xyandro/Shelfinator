#include "Controller.h"

#include "DotStar.h"
#include "Remote.h"

namespace Shelfinator
{
	namespace Interop
	{
		Controller::Controller(IDotStar ^dotStar, IRemote ^remote)
		{
			driver = new Runner::Controller::ptr(Runner::Controller::Create(DotStar::Create(dotStar), Remote::Create(remote)));
		}

		Controller::~Controller()
		{
			delete driver;
		}

		void Controller::Run(System::Collections::Generic::List<int> ^patternNumbers)
		{
			auto nativePatternNumbers = new int[patternNumbers->Count];
			for (auto ctr = 0; ctr < patternNumbers->Count; ++ctr)
				nativePatternNumbers[ctr] = patternNumbers[ctr];
			(*driver)->Run(nativePatternNumbers, patternNumbers->Count);
			delete[] nativePatternNumbers;
		}

		void Controller::Test(int lightCount, int concurrency, int delay)
		{
			(*driver)->Test(lightCount, concurrency, delay);
		}

		void Controller::Stop()
		{
			(*driver)->Stop();
		}
	}
}
