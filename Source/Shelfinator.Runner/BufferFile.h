#pragma once

#include <memory>
#include <string>

namespace Shelfinator
{
	namespace Runner
	{
		class BufferFile
		{
		public:
			typedef std::shared_ptr<BufferFile> ptr;
			static ptr Open(std::string fileName);
			static ptr Create(char *buffer, int size);
			~BufferFile();
			bool GetBool();
			unsigned char GetByte();
			int GetInt();
			std::string GetString();
		private:
			char *buffer;
			int bufPos = 0, bufUsed = 0, bufSize = 0;
			FILE *file = nullptr;
			BufferFile(std::string fileName);
			BufferFile(char *buffer, int size);
			void Copy(void *ptr, int size);
		};
	}
}
