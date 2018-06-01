#include "stdafx.h"
#include "DotStar.h"

#ifndef _WIN32
#include <sys/ioctl.h>
#endif

namespace Shelfinator
{
#ifdef _WIN32

	DotStar::DotStar(IDotStar ^dotStar) { this->dotStar = dotStar; }
	DotStar::ptr DotStar::Create(IDotStar ^dotStar) { return ptr(new DotStar(dotStar)); }
	void DotStar::Clear() { return dotStar->Clear(); }
	void DotStar::SetPixelColor(int led, int color) { return dotStar->SetPixelColor(led, color); }
	void DotStar::Show() { return dotStar->Show(); }

#else

	DotStar::ptr DotStar::Create(int ledCount) { return ptr(new DotStar(ledCount)); }

	DotStar::DotStar(int ledCount)
	{
		numLEDs = ledCount;
		fd = -1;
		pixels = (int*)malloc(numLEDs * sizeof(int));

		Clear();

		if ((fd = open("/dev/spidev0.0", O_RDWR)) < 0)
		{
			puts("Can't open /dev/spidev0.0 (try 'sudo')");
			return;
		}

		char mode = SPI_MODE_0 | SPI_NO_CS;
		ioctl(fd, SPI_IOC_WR_MODE, &mode);

		// Get actual bitrate
		int bitrate = 8000000;
		ioctl(fd, SPI_IOC_WR_MAX_SPEED_HZ, &bitrate);

		int bufsiz = 0;

		if (auto fp = fopen("/sys/module/spidev/parameters/bufsiz", "r"))
		{
			int n;
			if (fscanf(fp, "%d", &n) == 1)
				bufsiz = n;
			fclose(fp);
		}

		// Header (zeros)
		xfer[0].bits_per_word = 8;
		xfer[0].cs_change = 0;
		xfer[0].delay_usecs = 0;
		xfer[0].len = 4;
		xfer[0].rx_buf = 0;
		xfer[0].speed_hz = bitrate;
		xfer[0].tx_buf = 0;

		// Color payload
		xfer[1].bits_per_word = 8;
		xfer[1].cs_change = 0;
		xfer[1].delay_usecs = 0;
		xfer[1].len = numLEDs * sizeof(int);
		xfer[1].rx_buf = 0;
		xfer[1].speed_hz = bitrate;
		xfer[1].tx_buf = (unsigned long)pixels;

		// Footer (zeros)
		xfer[2].bits_per_word = 8;
		xfer[2].cs_change = 0;
		xfer[2].delay_usecs = 0;
		xfer[2].len = (numLEDs + 15) / 16;
		xfer[2].rx_buf = 0;
		xfer[2].speed_hz = bitrate;
		xfer[2].tx_buf = 0;

		if ((xfer[0].len + xfer[1].len + xfer[2].len) > bufsiz)
		{
			puts("spidev.bufsiz too small; add spidev.bufsiz=xxxxx to /boot/cmdline.txt\n");
			return;
		}
	}

	DotStar::~DotStar()
	{
		free(pixels);
	}

	void DotStar::Clear()
	{
		for (int i = 0; i < numLEDs; ++i)
			pixels[i] = 0xff;
	}

	void DotStar::SetPixelColor(int led, int color)
	{
		if (led < numLEDs)
			pixels[led] = (color << 8) | 0xff;
	}

	void DotStar::Show()
	{
		ioctl(fd, SPI_IOC_MESSAGE(3), xfer);
	}

#endif
}
