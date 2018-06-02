#include "stdafx.h"
#include "Remote.h"

#ifndef _WIN32
#include <lirc/lirc_client.h>
#include <thread>
#endif

namespace Shelfinator
{
#ifdef _WIN32
	Remote::ptr Remote::Create(IRemote ^remote) { return ptr(new Remote(remote)); }
	Remote::Remote(IRemote ^remote) { this->remote = remote; }
	RemoteCode Remote::GetCode() { return (RemoteCode)remote->GetCode(); }
#else
	Remote::ptr Remote::Create()
	{
		return ptr(new Remote);
	}

	Remote::Remote()
	{
		if (lirc_init("lirc", 1) == -1)
			exit(EXIT_FAILURE);
		std::thread(&Remote::RunThread, this).detach();
	}

	Remote::~Remote()
	{
		lirc_deinit();
	}

	void Remote::RunThread()
	{
		static const char *lircNames[] = { "KEY_PLAY", "KEY_PAUSE" };
		static RemoteCode remoteCodes[] = { Play, Pause };

		while (true)
		{
			char *code;
			if (lirc_nextcode(&code) != 0)
				continue;

			if (code == nullptr)
				continue;

			for (auto ctr = 0; ctr < sizeof(lircNames) / sizeof(*lircNames); ++ctr)
				if (strstr(code, lircNames[ctr]) != nullptr)
				{
					AddCode(remoteCodes[ctr]);
					break;
				}

			free(code);
		}
	}

	RemoteCode Remote::GetCode()
	{
		std::unique_lock<std::mutex> mlock(mutex);
		if (queue.empty())
			return None;
		auto result = queue.front();
		queue.pop_front();
		return result;
	}

	void Remote::AddCode(RemoteCode code)
	{
		std::unique_lock<std::mutex> mlock(mutex);
		queue.push_back(code);
		mlock.unlock();
	}
#endif
}
