#include "Lights.h"

#include "Banner.h"

namespace Shelfinator
{
	namespace Runner
	{
		Lights::ptr Lights::Create()
		{
			return ptr(new Lights());
		}

		Lights::Lights()
		{
			Clear();
		}

		void Lights::Clear()
		{
			for (int i = 0; i < 2440; ++i)
				lights[i] = 0xff;
		}

		void Lights::SetLight(int light, int value)
		{
			if ((light < 0) || (light >= 2440))
				return;
			lights[light] = value << 8 | 0xff;
		}

		void Lights::Show(IDotStar::ptr dotStar)
		{
			PreventOverage();
			dotStar->Show(lights);
		}

		void Lights::PreventOverage()
		{
			const int OutputLimit = 15 * 12750; // 15 amps

			auto total = 0;
			for (auto light = 0; light < 2440; ++light)
				total += (lights[light] >> 24 & 0xff) + (lights[light] >> 16 & 0xff) + (lights[light] >> 8 & 0xff);
			if (total < OutputLimit)
				return;

			Clear();
			Banner::Create(L"!!!", 0, 0, -1, 0x100000)->SetLights(shared_from_this());
		}
	}
}
