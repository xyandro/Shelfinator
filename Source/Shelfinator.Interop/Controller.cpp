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

		void Controller::Run(System::Collections::Generic::List<int> ^patternNumbers, bool startPaused)
		{
			auto nativePatternNumbers = new int[patternNumbers->Count];
			for (auto ctr = 0; ctr < patternNumbers->Count; ++ctr)
				nativePatternNumbers[ctr] = patternNumbers[ctr];
			(*controller)->Run(nativePatternNumbers, patternNumbers->Count, startPaused);
			delete[] nativePatternNumbers;
		}

		void Controller::Test(int firstLight, int lightCount, int concurrency, int delay, unsigned char brightness)
		{
			(*controller)->Test(firstLight, lightCount, concurrency, delay, brightness);
		}

		void Controller::TestAll(int lightCount, int delay, unsigned char brightness)
		{
			(*controller)->TestAll(lightCount, delay, brightness);
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
