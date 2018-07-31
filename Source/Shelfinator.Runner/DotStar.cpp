#include "stdafx.h"
#include "DotStar.h"

namespace Shelfinator
{
	namespace Runner
	{
		DotStar::ptr DotStar::Create()
		{
			return ptr(new DotStar());
		}

		DotStar::DotStar()
		{
			fd = -1;

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

			bufsiz = GetBufSiz();

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
			xfer[1].rx_buf = 0;
			xfer[1].speed_hz = bitrate;

			// Footer (zeros)
			xfer[2].bits_per_word = 8;
			xfer[2].cs_change = 0;
			xfer[2].delay_usecs = 0;
			xfer[2].rx_buf = 0;
			xfer[2].speed_hz = bitrate;
			xfer[2].tx_buf = 0;
		}

		int DotStar::GetBufSiz()
		{
			if (auto fp = fopen("/sys/module/spidev/parameters/bufsiz", "r"))
			{
				int n;
				if (fscanf(fp, "%d", &n) == 1)
					bufsiz = n;
				fclose(fp);
				return n;
			}
		}

		void DotStar::Show(int *lights, int count)
		{
			xfer[1].len = count * sizeof(int);
			xfer[1].tx_buf = (unsigned long)lights;
			xfer[2].len = (count + 15) / 16;

			if ((xfer[0].len + xfer[1].len + xfer[2].len) > bufsiz)
			{
				puts("spidev.bufsiz too small; add spidev.bufsiz=xxxxx to /boot/cmdline.txt\n");
				exit(0);
			}

			ioctl(fd, SPI_IOC_MESSAGE(3), xfer);
		}
	}
}
