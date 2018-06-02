#include "stdafx.h"
#include "DriverRunner.h"

#include "Driver.h"

#ifdef _WIN32

namespace Shelfinator
{
	void DriverRunner::Run(System::String ^fileName, IDotStar ^dotStar, IRemote ^remote)
	{
		auto bytes = System::Text::Encoding::UTF8->GetBytes(fileName);
		cli::pin_ptr<unsigned char> fileNamePtr = &bytes[0];
		auto driver = Driver::Create((char*)fileNamePtr, DotStar::Create(dotStar), Remote::Create(remote));
		driver->Run();
	}
}

#else

#include <signal.h>

Shelfinator::DotStar::ptr dotStar;

void BreakHandler(int s)
{
	dotStar->Clear();
	dotStar->Show();
	exit(1);
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
	if (argc < 2)
	{
		puts("Must specify filename");
		return -1;
	}

	dotStar = Shelfinator::DotStar::Create(2440);

	SetupBreakhandler();

	Shelfinator::Driver::Create(argv[1], dotStar, Shelfinator::Remote::Create())->Run();

	return 0;
}

#endif
