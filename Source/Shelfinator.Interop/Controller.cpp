﻿#include "Controller.h"

#include "Audio.h"
#include "DotStar.h"

namespace Shelfinator
{
	namespace Interop
	{
		Controller::Controller(IDotStar ^dotStar, IAudio ^audio, System::Collections::Generic::List<int> ^songNumbers)
		{
			auto nativeSongNumbers = new int[songNumbers->Count];
			for (auto ctr = 0; ctr < songNumbers->Count; ++ctr)
				nativeSongNumbers[ctr] = songNumbers[ctr];
			controller = new Runner::Controller::ptr(Runner::Controller::Create(DotStar::Create(dotStar), Audio::Create(audio), nativeSongNumbers, songNumbers->Count));
			delete[] nativeSongNumbers;
		}

		Controller::~Controller()
		{
			delete controller;
		}

		void Controller::Run(bool startPaused)
		{
			return (*controller)->Run(startPaused);
		}

		void Controller::Stop()
		{
			return (*controller)->Stop();
		}

		void Controller::AddRemoteCode(RemoteCode remoteCode)
		{
			return (*controller)->AddRemoteCode((Runner::RemoteCode)remoteCode);
		}
	}
}
