#include <signal.h>
#include <string.h>
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
	printf("Starting Shelfinator!\n");

	SetupBreakhandler();

	controller = Shelfinator::Runner::Controller::Create(Shelfinator::Runner::DotStar::Create());
	Shelfinator::Runner::Remote::Run(controller);

	if ((argc > 1) && (strcmp(argv[1], "test") == 0))
	{
		if (argc == 7)
			controller->Test(atoi(argv[2]), atoi(argv[3]), atoi(argv[4]), atoi(argv[5]), (unsigned char)atoi(argv[6]));
		else
			fprintf(stderr, "Usage: %s test firstLight lightCount concurrency delay brightness\n", argv[0]);
	}
	else if ((argc > 1) && (strcmp(argv[1], "testall") == 0))
	{
		if (argc == 5)
			controller->TestAll(atoi(argv[2]), atoi(argv[3]), (unsigned char)atoi(argv[4]));
		else
			fprintf(stderr, "Usage: %s testall lightCount delay brightness\n", argv[0]);
	}
	else
	{
		auto startPaused = false;
		auto patternNumbers = new int[argc];
		int patternNumberCount = 0;
		for (auto ctr = 1; ctr < argc; ++ctr)
		{
			if (strcmp(argv[ctr], "pause") == 0)
			{
				startPaused = true;
				continue;
			}

			auto value = atoi(argv[ctr]);
			if (value == 0)
				fprintf(stderr, "Invalid pattern: %s\n", argv[ctr]);
			else
				patternNumbers[patternNumberCount++] = value;
		}

		controller->Run(patternNumbers, patternNumberCount, startPaused);

		delete[] patternNumbers;
	}

	return 0;
}
