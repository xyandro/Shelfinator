#pragma once

#include <vcclr.h>
#include "../Shelfinator.Runner/IAudio.h"
#include "IAudio.h"

namespace Shelfinator
{
	namespace Interop
	{
		class Audio : public Runner::IAudio
		{
		public:
			typedef std::shared_ptr<Audio> ptr;
			static ptr Create(Interop::IAudio ^audio);
			void Open(std::string fileName);
			void Close();
			void Play(int time = 0);
			void Stop();
			int GetTime();
		private:
			gcroot<Interop::IAudio^> audio;
			Audio(Interop::IAudio ^audio);
		};
	}
}
