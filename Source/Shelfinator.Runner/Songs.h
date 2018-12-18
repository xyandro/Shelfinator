#pragma once

#include <map>
#ifndef __CLR_VER
#include <condition_variable>
#include <mutex>
#endif
#include <string>
#include <vector>
#include "Song.h"

namespace Shelfinator
{
	namespace Runner
	{
		class Songs
		{
		public:
			typedef std::shared_ptr<Songs> ptr;
			static ptr Create();
			void MakeFirst(int songNumber);
			int GetValue(int index);
			int GetIndex(int value);
			int Count();
			SongData::Song::ptr LoadSong(int index);
		private:
			std::string path;
			std::vector<int> songNumbers;
			std::vector<int> songQueue;
			std::map<int, SongData::Song::ptr> songCache;
#ifndef __CLR_VER
			std::mutex mutex;
			std::condition_variable condVar;
#endif
			int queueValue = 0;
			Songs();
			void AddIfSongFile(std::string fileName);
			void SetupSongs();
			void LoadSongsThread();
		};
	}
}
