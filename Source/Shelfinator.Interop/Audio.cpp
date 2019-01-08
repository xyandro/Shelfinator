#include "Audio.h"

namespace Shelfinator
{
	namespace Interop
	{
		Audio::Audio(Interop::IAudio ^audio)
		{
			this->audio = audio;
		}

		Audio::ptr Audio::Create(Interop::IAudio ^audio)
		{
			return ptr(new Audio(audio));
		}

		void Audio::Open(std::string fileName)
		{
			return audio->Open(gcnew System::String(fileName.c_str()));
		}

		void Audio::Close()
		{
			return audio->Close();
		}

		void Audio::Play(int time)
		{
			return audio->Play(time);
		}

		void Audio::Stop()
		{
			return audio->Stop();
		}

		int Audio::GetTime()
		{
			return audio->GetTime();
		}
	}
}
