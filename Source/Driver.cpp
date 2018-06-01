﻿#include "stdafx.h"
#include "Driver.h"

namespace Shelfinator
{
	Driver::ptr Driver::Create(char *fileName, DotStar::ptr dotStar)
	{
		return ptr(new Driver(fileName, dotStar));
	}

	Driver::Driver(char *fileName, DotStar::ptr dotStar)
	{
		lights = Lights::Read(fileName);
		this->dotStar = dotStar;
	}

	void Driver::Run()
	{
		int time = 0;
		while (time < lights->GetLength())
		{
			lights->SetLights(time, dotStar);
			dotStar->Show();
			time += 10;
		}
	}

	void DriverRunner::Run(System::String ^fileName, IDotStar ^dotStar)
	{
		auto bytes = System::Text::Encoding::UTF8->GetBytes(fileName);
		cli::pin_ptr<unsigned char> fileNamePtr = &bytes[0];
		auto driver = Driver::Create((char*)fileNamePtr, DotStar::Create(dotStar));
		driver->Run();
	}
}
