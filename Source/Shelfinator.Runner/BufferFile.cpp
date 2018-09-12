#include "BufferFile.h"

#include <string.h>

namespace Shelfinator
{
	namespace Runner
	{
		BufferFile::ptr BufferFile::Open(std::string fileName)
		{
			return ptr(new BufferFile(fileName));
		}

		BufferFile::ptr BufferFile::Create(char *buffer, int size)
		{
			return ptr(new BufferFile(buffer, size));
		}

		BufferFile::BufferFile(std::string fileName)
		{
			bufSize = 65536;
			buffer = (char*)malloc(bufSize);
			file = fopen(fileName.c_str(), "rb");
		}

		BufferFile::BufferFile(char *buffer, int size)
		{
			this->buffer = buffer;
			bufSize = bufUsed = size;
		}

		BufferFile::~BufferFile()
		{
			if (file != nullptr)
			{
				free(buffer);
				fclose(file);
			}
		}

		bool BufferFile::GetBool()
		{
			bool value;
			Copy(&value, sizeof(value));
			return value;
		}

		unsigned char BufferFile::GetByte()
		{
			unsigned char value;
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

				auto block = fread(buffer + bufUsed, 1, bufSize - bufUsed, file);
				if (block == 0)
					throw "Failed to read";
				bufUsed += (int)block;
			}

			memcpy(ptr, buffer + bufPos, size);
			bufPos += size;
		}
	}
}
