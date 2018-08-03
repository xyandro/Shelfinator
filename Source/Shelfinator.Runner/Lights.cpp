#include "Lights.h"

#include "Banner.h"

namespace Shelfinator
{
	namespace Runner
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

		void Lights::Show(IDotStar::ptr dotStar)
		{
			PreventOverage();
			dotStar->Show(lights, count);
		}

		void Lights::PreventOverage()
		{
			const int OutputLimit = 15 * 12750; // 15 amps

			auto total = 0;
			for (auto light = 0; light < count; ++light)
				total += (lights[light] >> 24 & 0xff) + (lights[light] >> 16 & 0xff) + (lights[light] >> 8 & 0xff);
			if (total < OutputLimit)
				return;

			Clear();
			Banner::Create(L"!!!", 0, 0, -1, 0x100000)->SetLights(shared_from_this());
		}
	}
}
