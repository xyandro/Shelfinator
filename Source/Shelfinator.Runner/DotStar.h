#pragma once

#include "IDotStar.h"

namespace Shelfinator
{
	namespace Runner
	{
		class DotStar : public IDotStar
		{
		public:
			typedef std::shared_ptr<DotStar> ptr;
			static ptr Create();
			void Show(int *lights, int count);
		private:
			spi_ioc_transfer xfer[3];
			int fd, bufsiz;
			DotStar();
			int GetBufSiz();
		};
	}
}
