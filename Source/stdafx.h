#pragma once

#ifdef _WIN32
#include <vcclr.h>
#else
#include <linux/spi/spidev.h>
#include <sys/ioctl.h>
#include <fcntl.h>
#include <signal.h>
#endif

#include <memory>

#include "DotStar.h"
#include "Driver.h"
#include "Pattern.h"
