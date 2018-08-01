#include "Remote.h"

namespace Shelfinator
{
	namespace Runner
	{
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
			static const char *lircNames[] = { "KEY_PLAY", "KEY_PAUSE", "KEY_REWIND", "KEY_FASTFORWARD", "KEY_PREVIOUS", "KEY_NEXT", "KEY_ENTER", "KEY_NUMERIC_0", "KEY_NUMERIC_1", "KEY_NUMERIC_2", "KEY_NUMERIC_3", "KEY_NUMERIC_4", "KEY_NUMERIC_5", "KEY_NUMERIC_6", "KEY_NUMERIC_7", "KEY_NUMERIC_8", "KEY_NUMERIC_9", "KEY_INFO" };
			static RemoteCode remoteCodes[] = { Play, Pause, Rewind, FastForward, Previous, Next, Enter, D0, D1, D2, D3, D4, D5, D6, D7, D8, D9, Info };

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
	}
}
