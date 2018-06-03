#pragma once

namespace Shelfinator
{
#ifdef _WIN32
	public interface class IDotStar
	{
		void Clear();
		void SetPixelColor(int led, int color);
		void Show();
	};
#endif

	class DotStar
	{
	public:
		typedef std::shared_ptr<DotStar> ptr;
		void Clear();
		void SetPixelColor(int led, int color);
		void Show();
#ifdef _WIN32
	public:
		static ptr Create(IDotStar^);
	private:
		gcroot<IDotStar^> dotStar;
		DotStar(IDotStar ^dotStar);
#else
	public:
		static ptr Create(int ledCount);
		~DotStar();
	private:
		spi_ioc_transfer xfer[3];
		int numLEDs, fd;
		int *pixels;
		DotStar(int ledCount);
#endif
	};
}
