#include "stdafx.h"
#include "Driver.h"

namespace Shelfinator
{
	Driver::ptr Driver::Create(char *fileName, DotStar::ptr dotStar, Remote::ptr remote)
	{
		return ptr(new Driver(fileName, dotStar, remote));
	}

	Driver::Driver(char *fileName, DotStar::ptr dotStar, Remote::ptr remote)
	{
		pattern = Pattern::Read(fileName);
		this->dotStar = dotStar;
		this->remote = remote;
	}

	void Driver::Run()
	{
		for (auto sequenceCtr = 0; sequenceCtr < pattern->GetNumSequences(); ++sequenceCtr)
			for (auto repeat = 0; repeat < pattern->GetSequenceRepeat(sequenceCtr); ++repeat)
			{
				int time = pattern->GetSequenceStartTime(sequenceCtr);
				while (time < pattern->GetSequenceEndTime(sequenceCtr))
				{
					auto code = remote->GetCode();
					if (code != None)
						fprintf(stderr, "Code is %i\n", code);
					pattern->SetLights(time, dotStar);
					dotStar->Show();
					time += 10;
				}
			}
	}
}
