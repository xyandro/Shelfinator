#pragma once

#include <memory>

namespace Shelfinator
{
	namespace Runner
	{
		class Lights
		{
		public:
			typedef std::shared_ptr<Lights> ptr;

			int count, *lights;

			static ptr Create(int count);
			~Lights();
			void Clear();
			void SetLight(int light, int value);
			void CheckOverage();
#ifndef INTEROP
		private:
			Lights(int count);
#endif
		};
	}
}
