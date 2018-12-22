#include "Songs.h"

#include <algorithm>
#include <ctime>
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
		Songs::ptr Songs::Create()
		{
			return ptr(new Songs());
		}

		Songs::Songs()
		{
			path = Helpers::GetRunPath();
			SetupSongs();
			std::thread(&Songs::LoadSongsThread, this).detach();
		}

		void Songs::LoadSongsThread()
		{
			while (true)
			{
				int loadSong = -1;
				{
					std::unique_lock<decltype(mutex)> lock(mutex);

					for (auto itr = songCache.begin(); itr != songCache.end();)
					{
						if (std::find(songQueue.begin(), songQueue.end(), itr->first) == songQueue.end())
							songCache.erase(itr++);
						else
							itr++;
					}

					for (auto itr = songQueue.begin(); itr != songQueue.end(); ++itr)
						if (songCache.find(*itr) == songCache.end())
						{
							loadSong = *itr;
							break;
						}

					if (loadSong == -1)
					{
						auto oldQueueValue = queueValue;
						while (oldQueueValue == queueValue)
							condVar.wait(lock);
						continue;
					}
				}

				auto fileName = path + std::to_string(loadSong) + ".pat";
				auto result = SongData::Song::Read(fileName.c_str());

				std::unique_lock<decltype(mutex)> lock(mutex);
				songCache[loadSong] = result;
				condVar.notify_all();
			}
		}

		SongData::Song::ptr Songs::LoadSong(int index)
		{
			static const int fetchPositions[] = { 0,1,-1 };

			std::unique_lock<decltype(mutex)> lock(mutex);
			songQueue.clear();
			for (auto ctr = 0; ctr < sizeof(fetchPositions) / sizeof(*fetchPositions); ++ctr)
			{
				auto useIndex = index + fetchPositions[ctr];
				while (useIndex < 0)
					useIndex += (int)songNumbers.size();
				while (useIndex >= songNumbers.size())
					useIndex -= (int)songNumbers.size();
				songQueue.push_back(songNumbers[useIndex]);
			}
			++queueValue;
			condVar.notify_all();

			auto songNumber = songNumbers[index];
			while (songCache.find(songNumber) == songCache.end())
				condVar.wait(lock);

			return songCache[songNumber];
		}

		void Songs::AddIfSongFile(std::string fileName)
		{
			std::transform(fileName.begin(), fileName.end(), fileName.begin(), tolower);
			if ((fileName.length() >= 4) && (fileName.substr(fileName.length() - 4, 4) == ".pat"))
			{
				fileName = fileName.substr(0, fileName.length() - 4);
				auto songNumber = atoi(fileName.c_str());
				if (songNumber != 0)
					songNumbers.push_back(songNumber);
			}
		}

		void Songs::SetupSongs()
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

				AddIfSongFile(findData.cFileName);
			} while (FindNextFileA(hFind, &findData) != 0);

			FindClose(hFind);
#else
			DIR *dir;
			struct dirent *ent;
			if ((dir = opendir(path.c_str())) == nullptr)
				return;

			while ((ent = readdir(dir)) != nullptr)
			{
				AddIfSongFile(ent->d_name);
			}

			closedir(dir);
#endif

			//std::srand((unsigned)std::time(0));
			//std::random_shuffle(songNumbers.begin(), songNumbers.end());
		}

		void Songs::MakeFirst(int songNumber)
		{
			auto found = std::find(songNumbers.begin(), songNumbers.end(), songNumber);
			if (found == songNumbers.end())
				return;

			auto result = *found;
			songNumbers.erase(found);
			songNumbers.insert(songNumbers.begin(), result);
		}

		int Songs::GetValue(int index)
		{
			return songNumbers[index];
		}

		int Songs::GetIndex(int value)
		{
			auto found = std::find(songNumbers.begin(), songNumbers.end(), value);
			if (found != songNumbers.end())
				return (int)std::distance(songNumbers.begin(), found);
			return -1;
		}

		int Songs::Count()
		{
			return (int)songNumbers.size();
		}
	}
}
