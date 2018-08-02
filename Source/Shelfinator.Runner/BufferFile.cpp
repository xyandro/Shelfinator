#include "BufferFile.h"

namespace Shelfinator
{
	namespace Runner
	{
		BufferFile::ptr BufferFile::Open(std::string fileName)
		{
			return ptr(new BufferFile(fileName));
		}

		BufferFile::BufferFile(std::string fileName)
		{
			file = fopen(fileName.c_str(), "rb");
		}

		BufferFile::~BufferFile()
		{
			fclose(file);
		}

		bool BufferFile::GetBool()
		{
			bool value;
			Copy(&value, sizeof(value));
			return value;
		}

		int BufferFile::GetInt()
		{
			int value;
			Copy(&value, sizeof(value));
			return value;
		}

		void BufferFile::Copy(void *ptr, int size)
		{
			while (bufPos + size > bufUsed)
			{
				if (bufPos != 0)
				{
					memmove(buffer, buffer + bufPos, bufUsed - bufPos);
					bufUsed -= bufPos;
					bufPos = 0;
				}

				auto block = fread(buffer + bufUsed, 1, sizeof(buffer) / sizeof(*buffer) - bufUsed, file);
				if (block == 0)
					throw "Failed to read";
				bufUsed += (int)block;
			}

			memcpy(ptr, buffer + bufPos, size);
			bufPos += size;
		}
	}
}
