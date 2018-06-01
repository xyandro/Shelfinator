#include "stdafx.h"
#include "Driver.h"
#include "DotStar.h"

#ifndef _WIN32
#include <signal.h>
#endif

namespace Shelfinator
{
	Driver::ptr Driver::Create(char *fileName, DotStar::ptr dotStar)
	{
		return ptr(new Driver(fileName, dotStar));
	}

	Driver::Driver(char *fileName, DotStar::ptr dotStar)
	{
		pattern = Pattern::Read(fileName);
		this->dotStar = dotStar;
	}

	void Driver::Run()
	{
		for (auto sequenceCtr = 0; sequenceCtr < pattern->GetNumSequences(); ++sequenceCtr)
			for (auto repeat = 0; repeat < pattern->GetSequenceRepeat(sequenceCtr); ++repeat)
			{
				int time = pattern->GetSequenceStartTime(sequenceCtr);
				while (time < pattern->GetSequenceEndTime(sequenceCtr))
				{
					pattern->SetLights(time, dotStar);
					dotStar->Show();
					time += 10;
				}
			}
	}

#ifdef _WIN32
	void DriverRunner::Run(System::String ^fileName, IDotStar ^dotStar)
	{
		auto bytes = System::Text::Encoding::UTF8->GetBytes(fileName);
		cli::pin_ptr<unsigned char> fileNamePtr = &bytes[0];
		auto driver = Driver::Create((char*)fileNamePtr, DotStar::Create(dotStar));
		driver->Run();
	}
#endif
}

#ifndef _WIN32
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

	Shelfinator::Driver::Create(argv[1], dotStar)->Run();

	return 0;
}
#endif
