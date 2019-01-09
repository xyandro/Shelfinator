#include <signal.h>
#include <string.h>
#include "Audio.h"
#include "Controller.h"
#include "DotStar.h"
#include "Remote.h"

namespace
{
	Shelfinator::Runner::Controller::ptr controller;
}

void BreakHandler(int s)
{
	controller->Stop();
}

void SetupBreakhandler()
{
	struct sigaction breakHandler;
	breakHandler.sa_handler = BreakHandler;
	sigemptyset(&breakHandler.sa_mask);
	breakHandler.sa_flags = 0;
	sigaction(SIGINT, &breakHandler, NULL);
	sigaction(SIGTERM, &breakHandler, NULL);
}

int main(int argc, char **argv)
{
	try
	{
		printf("Starting Shelfinator!\n");

		SetupBreakhandler();

		auto startPaused = false;
		auto songNumbers = new int[argc];
		int songNumberCount = 0;
		for (auto ctr = 1; ctr < argc; ++ctr)
		{
			if (strcmp(argv[ctr], "pause") == 0)
			{
				startPaused = true;
				continue;
			}

			auto value = atoi(argv[ctr]);
			if (value == 0)
				fprintf(stderr, "Invalid song: %s\n", argv[ctr]);
			else
				songNumbers[songNumberCount++] = value;
		}

		controller = Shelfinator::Runner::Controller::Create(Shelfinator::Runner::DotStar::Create(), Shelfinator::Runner::Audio::Create(), songNumbers, songNumberCount);
		Shelfinator::Runner::Remote::Run(controller);
		controller->Run(startPaused);

		delete[] songNumbers;
	}
	catch (const char *str)
	{
		fprintf(stderr, "Error: %s\n", str);
	}
	catch (...)
	{
		fprintf(stderr, "Error.\n");
	}

	return 0;
}
