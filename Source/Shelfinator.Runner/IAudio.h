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
			virtual void Play(std::string fileName) = 0;
			virtual void Stop() = 0;
			virtual int GetTime() = 0;
			virtual void SetTime(int time) = 0;
		};
	}
}
