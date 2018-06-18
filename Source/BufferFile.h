#pragma once

#include "Lights.h"

namespace Shelfinator
{
	class BufferFile
	{
	public:
		typedef std::shared_ptr<BufferFile> ptr;
		static ptr Open(const char *fileName);
		~BufferFile();
		bool GetBool();
		int GetInt();
	private:
		char buffer[65536];
		int bufPos = 0, bufUsed = 0;
		FILE *file;
		BufferFile(const char *fileName);
		void Copy(void *ptr, int size);
	};
}
