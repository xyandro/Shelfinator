#include "DotStar.h"

#include <fcntl.h>
#include <linux/spi/spidev.h>
#include <string.h>
#include <sys/ioctl.h>
#include <thread>
#include <unistd.h>

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
			std::thread(&DotStar::RunThread, this, "/dev/spidev0.0", 0, 936).detach();
			std::thread(&DotStar::RunThread, this, "/dev/spidev1.0", 936, 1504).detach();
		}

		void DotStar::Show(int *lights)
		{
			std::unique_lock<decltype(mutex)> lock(mutex);
			this->lights = lights;
			threadsWorking = threadCount;
			++iteration;
			condVar.notify_all();

			while (threadsWorking != 0)
				condVar.wait(lock);
		}

		void DotStar::RunThread(const char *device, int firstLight, int lightCount)
		{
			{
				std::unique_lock<decltype(mutex)> lock(mutex);
				++threadCount;
			}

			int fd = open(device, O_RDWR);
			if (fd < 0)
				throw "Can't open device (try 'sudo')";

			char mode = SPI_MODE_0 | SPI_NO_CS;
			ioctl(fd, SPI_IOC_WR_MODE, &mode);

			// Set bitrate
			int bitrate = 2500000;
			ioctl(fd, SPI_IOC_WR_MAX_SPEED_HZ, &bitrate);

			// This doesn't like living on the stack for some reason
			auto xfer = new spi_ioc_transfer[3];

			// Header (zeros)
			xfer[0].bits_per_word = 8;
			xfer[0].cs_change = 0;
			xfer[0].delay_usecs = 0;
			xfer[0].len = 4;
			xfer[0].rx_buf = 0;
			xfer[0].speed_hz = bitrate;
			xfer[0].tx_buf = (unsigned long)malloc(xfer[0].len);
			memset((void*)xfer[0].tx_buf, 0, xfer[0].len);

			// Color payload
			xfer[1].bits_per_word = 8;
			xfer[1].cs_change = 0;
			xfer[1].delay_usecs = 0;
			xfer[1].len = lightCount * sizeof(int);
			xfer[1].rx_buf = 0;
			xfer[1].speed_hz = bitrate;

			// Footer (zeros)
			xfer[2].bits_per_word = 8;
			xfer[2].cs_change = 0;
			xfer[2].delay_usecs = 0;
			xfer[2].len = (lightCount + 15) / 16;
			xfer[2].rx_buf = 0;
			xfer[2].speed_hz = bitrate;
			xfer[2].tx_buf = (unsigned long)malloc(xfer[2].len);
			memset((void*)xfer[2].tx_buf, 0, xfer[2].len);

			if ((xfer[0].len + xfer[1].len + xfer[2].len) > GetBufSiz())
				throw "spidev.bufsiz too small; add spidev.bufsiz=xxxxx to /boot/cmdline.txt\n";

			auto currentIteration = iteration;
			while (true)
			{
				{
					std::unique_lock<decltype(mutex)> lock(mutex);
					while (currentIteration == iteration)
						condVar.wait(lock);
					currentIteration = iteration;
				}

				xfer[1].tx_buf = (unsigned long)(lights + firstLight);
				ioctl(fd, SPI_IOC_MESSAGE(3), xfer);

				{
					std::unique_lock<decltype(mutex)> lock(mutex);
					--threadsWorking;
					condVar.notify_all();
				}
			}
		}

		int DotStar::GetBufSiz()
		{
			static int bufsiz = -1;
			if (bufsiz == -1)
			{
				auto fd = open("/sys/module/spidev/parameters/bufsiz", O_RDONLY);
				char buffer[1024];
				auto block = read(fd, buffer, sizeof(buffer));
				buffer[block] = 0;
				bufsiz = atoi(buffer);
				close(fd);
			}
			return bufsiz;
		}
	}
}
