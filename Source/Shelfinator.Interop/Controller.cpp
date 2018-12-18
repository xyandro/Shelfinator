#include "Controller.h"

#include "Audio.h"
#include "DotStar.h"

namespace Shelfinator
{
	namespace Interop
	{
		Controller::Controller(IDotStar ^dotStar, IAudio ^audio)
		{
			controller = new Runner::Controller::ptr(Runner::Controller::Create(DotStar::Create(dotStar), Audio::Create(audio)));
		}

		Controller::~Controller()
		{
			delete controller;
		}

		void Controller::Run(System::Collections::Generic::List<int> ^songNumbers, bool startPaused)
		{
			auto nativeSongNumbers = new int[songNumbers->Count];
			for (auto ctr = 0; ctr < songNumbers->Count; ++ctr)
				nativeSongNumbers[ctr] = songNumbers[ctr];
			(*controller)->Run(nativeSongNumbers, songNumbers->Count, startPaused);
			delete[] nativeSongNumbers;
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
