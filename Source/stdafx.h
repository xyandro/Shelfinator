#pragma once

#ifdef _WIN32
#include <vcclr.h>
#include <Windows.h>
#else
#include <linux/spi/spidev.h>
#include <sys/ioctl.h>
#include <fcntl.h>
#include <signal.h>
#include <queue>
#include <mutex>
#include <lirc/lirc_client.h>
#include <thread>
#include <dirent.h>
#include <string.h>
#include <unistd.h>
#endif

#include <algorithm>
#include <chrono>
#include <memory>
#include <string>
#include <vector>
