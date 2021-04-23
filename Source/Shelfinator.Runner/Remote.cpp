#include "Remote.h"

#include <lirc/lirc_client.h>
#include <thread>

namespace Shelfinator
{
	namespace Runner
	{
		void Remote::Run(Controller::ptr controller)
		{
			std::thread(&Remote::RunThread, controller).detach();
		}

		void Remote::RunThread(Controller::ptr controller)
		{
			try
			{
				if (lirc_init("lirc", 1) == -1)
					exit(EXIT_FAILURE);

				static const char* lircNames[] = { "KEY_PLAY", "KEY_PAUSE", "KEY_REWIND", "KEY_FASTFORWARD", "KEY_PREVIOUS", "KEY_NEXT", "KEY_ENTER", "KEY_NUMERIC_0", "KEY_NUMERIC_1", "KEY_NUMERIC_2", "KEY_NUMERIC_3", "KEY_NUMERIC_4", "KEY_NUMERIC_5", "KEY_NUMERIC_6", "KEY_NUMERIC_7", "KEY_NUMERIC_8", "KEY_NUMERIC_9", "KEY_INFO", "KEY_GREEN" };
				static const RemoteCode remoteCodes[] = { Play, Pause, Rewind, FastForward, Previous, Next, Enter, D0, D1, D2, D3, D4, D5, D6, D7, D8, D9, Info, Edited };

				while (true)
				{
					char* code;
					if (lirc_nextcode(&code) != 0)
						continue;

					if (code == nullptr)
						continue;

					for (auto ctr = 0; ctr < sizeof(lircNames) / sizeof(*lircNames); ++ctr)
						if (strstr(code, lircNames[ctr]) != nullptr)
						{
							controller->AddRemoteCode(remoteCodes[ctr]);
							break;
						}

					free(code);
				}

				lirc_deinit();
			}
			catch (char const* str)
			{
				fprintf(stderr, "Remote::RunThread failed: %s\n", str);
				throw;
			}
			catch (...)
			{
				fprintf(stderr, "Remote::RunThread failed\n");
				throw;
			}
		}
	}
}
