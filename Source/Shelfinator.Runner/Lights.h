#pragma once

#include "IDotStar.h"

namespace Shelfinator
{
	namespace Runner
	{
		class Lights
		{
		public:
			typedef std::shared_ptr<Lights> ptr;
			static ptr Create(int count);
			~Lights();
			void Clear();
			void SetLight(int light, int value);
			void Show(IDotStar::ptr dotStar);
		private:
			int *lights, count;
			Lights(int count);
			void CheckOverage();
		};
	}
}
