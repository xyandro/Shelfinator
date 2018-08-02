#include "Patterns.h"

#include <algorithm>
#include <Windows.h>

namespace Shelfinator
{
	namespace Runner
	{
		Patterns::ptr Patterns::Create(std::string path)
		{
			return ptr(new Patterns(path));
		}

		Patterns::Patterns(std::string path)
		{
			this->path = path;
			SetupPatterns();
		}

		Pattern::ptr Patterns::LoadPattern(int index)
		{
			auto fileName = path + std::to_string(patternNumbers[index]) + ".pat";
			return Pattern::Read(fileName.c_str());
		}

		void Patterns::AddIfPatternFile(std::string fileName)
		{
			std::transform(fileName.begin(), fileName.end(), fileName.begin(), tolower);
			if ((fileName.length() >= 4) && (fileName.substr(fileName.length() - 4, 4) == ".pat"))
			{
				fileName = fileName.substr(0, fileName.length() - 4);
				auto patternNumber = atoi(fileName.c_str());
				if (patternNumber != 0)
					patternNumbers.push_back(patternNumber);
			}
		}

		void Patterns::SetupPatterns()
		{
#ifdef _WIN32
			WIN32_FIND_DATAA findData;
			auto find = path + "*.*";
			auto hFind = FindFirstFileA(find.c_str(), &findData);
			if (hFind == INVALID_HANDLE_VALUE)
				return;

			do
			{
				if (findData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)
					continue;

				AddIfPatternFile(findData.cFileName);
			} while (FindNextFileA(hFind, &findData) != 0);

			FindClose(hFind);
#else
			DIR *dir;
			struct dirent *ent;
			if ((dir = opendir(path.c_str())) == nullptr)
				return;

			while ((ent = readdir(dir)) != nullptr)
			{
				AddIfPatternFile(ent->d_name);
			}

			closedir(dir);
#endif

			std::random_shuffle(patternNumbers.begin(), patternNumbers.end());
		}

		void Patterns::MakeFirst(int patternNumber)
		{
			auto found = std::find(patternNumbers.begin(), patternNumbers.end(), patternNumber);
			if (found == patternNumbers.end())
				return;

			auto result = *found;
			patternNumbers.erase(found);
			patternNumbers.insert(patternNumbers.begin(), result);
		}

		int Patterns::GetValue(int index)
		{
			return patternNumbers[index];
		}

		int Patterns::GetIndex(int value)
		{
			auto found = std::find(patternNumbers.begin(), patternNumbers.end(), value);
			if (found != patternNumbers.end())
				return (int)std::distance(patternNumbers.begin(), found);
			return -1;
		}

		int Patterns::Count()
		{
			return (int)patternNumbers.size();
		}
	}
}
