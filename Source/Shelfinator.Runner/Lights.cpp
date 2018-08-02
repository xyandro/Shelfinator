#include "Lights.h"

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
			CheckOverage();
			dotStar->Show(lights, count);
		}

		void Lights::CheckOverage()
		{
			const int OutputLimit = 15 * 12750; // 15 amps

			static int errorLights[] =
			{
				32, 33, 34, 35, 36, 38, 39, 40, 41, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 54,
				55, 88, 89, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 102, 103, 104, 105, 107, 108, 109,
				110, 111, 144, 145, 146, 147, 148, 150, 151, 152, 153, 155, 156, 157, 158, 159, 160, 161, 162, 163,
				164, 166, 167, 200, 201, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 214, 215, 216, 217, 219,
				220, 221, 222, 223
			};

			auto total = 0;
			for (auto light = 0; light < count; ++light)
				total += (lights[light] >> 24 & 0xff) + (lights[light] >> 16 & 0xff) + (lights[light] >> 8 & 0xff);
			if (total < OutputLimit)
				return;

			Clear();
			for (auto ctr = 0; ctr < sizeof(errorLights) / sizeof(*errorLights); ++ctr)
				lights[errorLights[ctr]] = 0x100000ff;
		}
	}
}
