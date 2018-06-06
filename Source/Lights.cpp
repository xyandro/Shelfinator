#include "stdafx.h"
#include "Lights.h"

namespace Shelfinator
{
	Lights::ptr Lights::Create(int count)
	{
		return ptr(new Lights(count));
	}

	Lights::Lights(int count)
	{
		this->count = count;
		lights = new int[count];
		Clear();
	}

	Lights::~Lights()
	{
		delete[] lights;
	}

	void Lights::Clear()
	{
		for (int i = 0; i < count; ++i)
			lights[i] = 0xff;
	}

	void Lights::SetLight(int light, int value)
	{
		if ((light < 0) || (light >= count))
			return;
		lights[light] = value << 8 | 0xff;
	}
}
