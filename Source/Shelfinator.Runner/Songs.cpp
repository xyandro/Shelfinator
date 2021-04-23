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
			index = WrapIndex(-1);
			std::thread(&Songs::LoadSongsThread, this).detach();
		}

		int Songs::CurrentSong()
		{
			return songNumbers[index];
		}

		void Songs::LoadSongsThread()
		{
			try
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
			catch (char const* str)
			{
				fprintf(stderr, "Songs::LoadSongsThread failed: %s\n", str);
				throw;
			}
			catch (...)
			{
				fprintf(stderr, "Songs::LoadSongsThread failed\n");
				throw;
			}
		}

		bool Songs::SetCurrent(int songNumber)
		{
			auto found = std::find(songNumbers.begin(), songNumbers.end(), songNumber);
			if (found == songNumbers.end())
				return false;
			SetIndex((int)std::distance(songNumbers.begin(), found));
			return true;
		}

		void Songs::Move(int offset)
		{
			SetIndex(index + offset);
		}

		int Songs::WrapIndex(int index)
		{
			while (index < 0)
				index += (int)songNumbers.size();
			while (index >= songNumbers.size())
				index -= (int)songNumbers.size();
			return index;
		}

		void Songs::SetIndex(int newIndex)
		{
			static const int fetchPositions[] = { 0, 1, -1 };

			index = WrapIndex(newIndex);

			std::unique_lock<decltype(mutex)> lock(mutex);
			songQueue.clear();
			for (auto ctr = 0; ctr < sizeof(fetchPositions) / sizeof(*fetchPositions); ++ctr)
				songQueue.push_back(songNumbers[WrapIndex(index + fetchPositions[ctr])]);
			++queueValue;
			condVar.notify_all();
		}

		SongData::Song::ptr Songs::LoadSong()
		{
			auto songNumber = songNumbers[index];
			std::unique_lock<decltype(mutex)> lock(mutex);
			while (true)
			{
				auto itr = songCache.find(songNumber);
				if (itr != songCache.end())
					return itr->second;
				condVar.wait(lock);
			}
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

			std::sort(songNumbers.begin(), songNumbers.end());
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
	}
}
