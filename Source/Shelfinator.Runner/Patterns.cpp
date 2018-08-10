#include "Patterns.h"

#include <algorithm>
#ifdef _WIN32
#include <Windows.h>
#else
#include <dirent.h>
#endif
#include <thread>
#include "Helpers.h"

namespace Shelfinator
{
	namespace Runner
	{
		Patterns::ptr Patterns::Create()
		{
			return ptr(new Patterns());
		}

		Patterns::Patterns()
		{
			path = Helpers::GetRunPath();
			SetupPatterns();
			std::thread(&Patterns::LoadPatternsThread, this).detach();
		}

		void Patterns::LoadPatternsThread()
		{
			while (true)
			{
				int loadPattern = -1;
				{
					std::unique_lock<decltype(mutex)> lock(mutex);

					for (auto itr = patternCache.begin(); itr != patternCache.end();)
					{
						if (std::find(patternQueue.begin(), patternQueue.end(), itr->first) == patternQueue.end())
							patternCache.erase(itr++);
						else
							itr++;
					}

					for (auto itr = patternQueue.begin(); itr != patternQueue.end(); ++itr)
						if (patternCache.find(*itr) == patternCache.end())
						{
							loadPattern = *itr;
							break;
						}

					if (loadPattern == -1)
					{
						auto oldQueueValue = queueValue;
						while (oldQueueValue == queueValue)
							condVar.wait(lock);
						continue;
					}
				}

				auto fileName = path + std::to_string(loadPattern) + ".pat";
				auto result = Pattern::Read(fileName.c_str());

				std::unique_lock<decltype(mutex)> lock(mutex);
				patternCache[loadPattern] = result;
				condVar.notify_all();
			}
		}

		Pattern::ptr Patterns::LoadPattern(int index)
		{
			static const int fetchPositions[] = { 0,1,-1 };

			std::unique_lock<decltype(mutex)> lock(mutex);
			patternQueue.clear();
			for (auto ctr = 0; ctr < sizeof(fetchPositions) / sizeof(*fetchPositions); ++ctr)
			{
				auto useIndex = index + fetchPositions[ctr];
				while (useIndex < 0)
					useIndex += (int)patternNumbers.size();
				while (useIndex >= patternNumbers.size())
					useIndex -= (int)patternNumbers.size();
				patternQueue.push_back(patternNumbers[useIndex]);
			}
			++queueValue;
			condVar.notify_all();

			auto patternNumber = patternNumbers[index];
			while (patternCache.find(patternNumber) == patternCache.end())
				condVar.wait(lock);

			return patternCache[patternNumber];
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
