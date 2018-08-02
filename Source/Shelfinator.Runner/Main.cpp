#include "Driver.h"

namespace
{
	Shelfinator::Runner::Driver::ptr driver;
}

void BreakHandler(int s)
{
	driver->Stop();
}

void SetupBreakhandler()
{
	struct sigaction breakHandler;
	breakHandler.sa_handler = BreakHandler;
	sigemptyset(&breakHandler.sa_mask);
	breakHandler.sa_flags = 0;
	sigaction(SIGINT, &breakHandler, NULL);
}

int main(int argc, char **argv)
{
	printf("Starting Shelfinator!\n");

	SetupBreakhandler();

	driver = Shelfinator::Driver::Create(Shelfinator::DotStar::Create(), Shelfinator::Remote::Create());

	if (argc == 5) && (strcmp(argv[1], "test") == 0)
		driver->Test(atoi(argv[2]), atoi(argv[3]), atoi(argv[4]));
	else
	{
		auto patternNumbers = new int[argc];
		int patternNumberCount = 0;
		for (auto ctr = 1; ctr < argc; ++ctr)
		{
			auto value = atoi(argv[ctr]);
			if (value == 0)
				fprintf(stderr, "Invalid pattern: %s\n", argv[ctr]);
			else
				patternNumbers[patternNumberCount++] = value;
		}

		driver->Run(patternNumbers, patternNumberCount);

		delete[] patternNumbers;
	}

	return 0;
}
