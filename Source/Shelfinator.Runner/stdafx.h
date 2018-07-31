﻿#pragma once

#include <algorithm>
#include <queue>
#ifdef _WIN32
#include <chrono>
#include <string>
#include <vector>
#include <Windows.h>
#else
#include <dirent.h>
#include <fcntl.h>
#include <linux/spi/spidev.h>
#include <lirc/lirc_client.h>
#include <mutex>
#include <queue>
#include <signal.h>
#include <sys/ioctl.h>
#include <thread>
#endif
