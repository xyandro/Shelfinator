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
			~BufferFile();
			bool GetBool();
			int GetInt();
		private:
			char buffer[65536];
			int bufPos = 0, bufUsed = 0;
			FILE *file;
			BufferFile(std::string fileName);
			void Copy(void *ptr, int size);
		};
	}
}
