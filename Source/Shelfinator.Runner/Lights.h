#pragma once

#include "IDotStar.h"

namespace Shelfinator
{
	namespace Runner
	{
		class Lights : public std::enable_shared_from_this<Lights>
		{
		public:
			typedef std::shared_ptr<Lights> ptr;
			static ptr Create();
			void Clear();
			void SetLight(int light, int value);
			void Show(IDotStar::ptr dotStar);
		private:
			int lights[2440];
			Lights();
			void PreventOverage();
		};
	}
}
