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
			void Play(std::string fileName);
			void Stop();
			int GetTime();
			void SetTime(int time);
		private:
			enum { Stopped, Playing, Stopping } playing = Stopped;
			std::mutex mutex;
			std::condition_variable condVar;
			int time = -1;

			void PlayWAVThread(std::string fileName);
		};
	}
}
