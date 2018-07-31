#pragma once

namespace Shelfinator
{
	namespace Runner
	{
#ifdef _WIN32
		public interface class IDotStar
		{
			void Show(int *lights, int count);
		};
#endif

		class DotStar
		{
		public:
			typedef std::shared_ptr<DotStar> ptr;
			void Show(int *lights, int count);
#ifdef _WIN32
		public:
			static ptr Create(IDotStar^);
		private:
			gcroot<IDotStar^> dotStar;
			DotStar(IDotStar ^dotStar);
#else
		public:
			static ptr Create();
		private:
			spi_ioc_transfer xfer[3];
			int fd, bufsiz;
			DotStar();
			int GetBufSiz();
#endif
		};
	}
}
