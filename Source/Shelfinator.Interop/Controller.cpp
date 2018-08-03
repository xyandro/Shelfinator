#include "Controller.h"

#include "DotStar.h"

namespace Shelfinator
{
	namespace Interop
	{
		Controller::Controller(IDotStar ^dotStar)
		{
			controller = new Runner::Controller::ptr(Runner::Controller::Create(DotStar::Create(dotStar)));
		}

		Controller::~Controller()
		{
			delete controller;
		}

		void Controller::Run(System::Collections::Generic::List<int> ^patternNumbers)
		{
			auto nativePatternNumbers = new int[patternNumbers->Count];
			for (auto ctr = 0; ctr < patternNumbers->Count; ++ctr)
				nativePatternNumbers[ctr] = patternNumbers[ctr];
			(*controller)->Run(nativePatternNumbers, patternNumbers->Count);
			delete[] nativePatternNumbers;
		}

		void Controller::Test(int lightCount, int concurrency, int delay)
		{
			(*controller)->Test(lightCount, concurrency, delay);
		}

		void Controller::Stop()
		{
			(*controller)->Stop();
		}

		void Controller::AddRemoteCode(RemoteCode remoteCode)
		{
			(*controller)->AddRemoteCode((Runner::RemoteCode)remoteCode);
		}
	}
}
