#pragma once

#include <condition_variable>
#include "IAudio.h"

namespace Shelfinator
{
	namespace Runner
	{
		class Audio : public IAudio
		{
		public:
			typedef std::shared_ptr<Audio> ptr;
			static ptr Create();
			void Open(std::string fileName);
			void Close();
			void Play();
			void Stop();
			int GetTime();
			void SetTime(int time);
			bool Playing();
			bool Finished();
		private:
			static const unsigned char Header[];

			bool finished = true;
			enum { IsStopped, IsPlaying, IsStopping } playing = IsStopped;
			std::mutex mutex;
			std::condition_variable condVar;
			int startTime = 0, currentTime = 0;
			FILE *file;

			void ValidateHeader();
			void PlayWAVThread();
		};
	}
}
