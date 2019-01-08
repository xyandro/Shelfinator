#pragma once

#include <string>

namespace Shelfinator
{
	namespace Runner
	{
		class IAudio
		{
		public:
			typedef std::shared_ptr<IAudio> ptr;
			virtual void Open(std::string fileName) = 0;
			virtual void Close() = 0;
			virtual void Play() = 0;
			virtual void Stop() = 0;
			virtual int GetTime() = 0;
			virtual void SetTime(int time) = 0;
			virtual bool Playing() = 0;
			virtual bool Finished() = 0;
		};
	}
}
