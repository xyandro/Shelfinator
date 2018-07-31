#pragma once

#ifndef _WIN32
#include <sys/sem.h>
#endif

namespace Shelfinator
{
	namespace Runner
	{
		class Semaphore
		{
		public:
			typedef std::shared_ptr<Semaphore> ptr;
			static ptr Create(int count);
			~Semaphore();
			void Signal();
			void Wait();
		private:
#ifdef _WIN32
			HANDLE handle;
#else
			int semaphoreID;
#endif

			Semaphore(int count);
		};
	}
}
