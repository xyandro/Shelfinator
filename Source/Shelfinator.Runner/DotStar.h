#pragma once

#include "IDotStar.h"
#include <condition_variable>
#include <mutex>

namespace Shelfinator
{
	namespace Runner
	{
		class DotStar : public IDotStar
		{
		public:
			typedef std::shared_ptr<DotStar> ptr;
			static ptr Create();
			void Show(int *lights);
		private:
			int *lights, threadCount = 0, threadsWorking = 0, iteration = 0;
			std::condition_variable condVar;
			std::mutex mutex;

			DotStar();
			void RunThread(const char *device, int firstLight, int lightCount);
			static int GetBufSiz();
		};
	}
}
