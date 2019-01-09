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
			bool SetCurrent(int songNumber);
			void Move(int offset);
			int CurrentSong();
			SongData::Song::ptr LoadSong();
		private:
			std::string path;
			std::vector<int> songNumbers;
			std::vector<int> songQueue;
			std::map<int, SongData::Song::ptr> songCache;
#ifndef __CLR_VER
			std::mutex mutex;
			std::condition_variable condVar;
#endif
			int queueValue = 0, index;
			Songs();
			void SetIndex(int newIndex);
			int WrapIndex(int index);
			void AddIfSongFile(std::string fileName);
			void SetupSongs();
			void LoadSongsThread();
		};
	}
}
