#include "stdafx.h"
#include "DriverRunner.h"

#include "Driver.h"

namespace
{
	Shelfinator::Driver::ptr driver;
}

#ifdef _WIN32

namespace Shelfinator
{
	void DriverRunner::Run(System::Collections::Generic::List<int> ^patternNumbers, IDotStar ^dotStar, IRemote ^remote)
	{
		auto nativePatternNumbers = new int[patternNumbers->Count];
		for (auto ctr = 0; ctr < patternNumbers->Count; ++ctr)
			nativePatternNumbers[ctr] = patternNumbers[ctr];
		driver = Driver::Create(nativePatternNumbers, patternNumbers->Count, DotStar::Create(dotStar), Remote::Create(remote));
		driver->Run();
		driver.reset();
		delete[] nativePatternNumbers;
	}

	void DriverRunner::Stop()
	{
		if (driver)
			driver->Stop();
	}
}

#else

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

#endif
