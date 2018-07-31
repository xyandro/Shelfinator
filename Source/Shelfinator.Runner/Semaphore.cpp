#include "stdafx.h"
#include "Semaphore.h"

namespace Shelfinator
{
	namespace Runner
	{
#ifndef _WIN32
		namespace
		{
			union semun
			{
				int val;
				struct semid_ds *buf;
				unsigned short *array;
				struct seminfo *__buf;
			};
		}
#endif

		Semaphore::ptr Semaphore::Create(int count)
		{
			return ptr(new Semaphore(count));
		}

		Semaphore::Semaphore(int count)
		{
#ifdef _WIN32
			handle = CreateSemaphore(NULL, count, 1000, NULL);
			if (handle == NULL)
			{
				fprintf(stderr, "NULL result.");
				exit(0);
			}
#else
			semaphoreID = semget(IPC_PRIVATE, 1, 0666 | IPC_CREAT);

			semun sem_union_init;
			sem_union_init.val = count;
			semctl(semaphoreID, 0, SETVAL, sem_union_init);
#endif
		}

		Semaphore::~Semaphore()
		{
#ifdef _WIN32
			CloseHandle(handle);
#else
			semun sem_union_delete;
			semctl(semaphoreID, 0, IPC_RMID, sem_union_delete);
#endif
		}

		void Semaphore::Signal()
		{
#ifdef _WIN32
			ReleaseSemaphore(handle, 1, NULL);
#else
			sembuf semb;
			semb.sem_num = 0;
			semb.sem_op = 1;
			semb.sem_flg = SEM_UNDO;
			semop(semaphoreID, &semb, 1);
#endif
		}

		void Semaphore::Wait()
		{
#ifdef _WIN32
			WaitForSingleObject(handle, INFINITE);
#else
			sembuf semb;
			semb.sem_num = 0;
			semb.sem_op = -1;
			semb.sem_flg = SEM_UNDO;
			semop(semaphoreID, &semb, 1);
#endif
		}
	}
}
