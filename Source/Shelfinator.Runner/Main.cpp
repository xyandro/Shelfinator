#include "stdafx.h"

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

	driver = Shelfinator::Driver::Create(patternNumbers, patternNumberCount, Shelfinator::DotStar::Create(), Shelfinator::Remote::Create());
	driver->Run();

	delete[] patternNumbers;

	return 0;
}
