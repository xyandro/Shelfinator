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

		void Audio::Play()
		{
			return audio->Play();
		}

		void Audio::Stop()
		{
			return audio->Stop();
		}

		int Audio::GetTime()
		{
			return audio->Time;
		}

		void Audio::SetTime(int time)
		{
			return audio->Time = time;
		}

		int Audio::GetVolume()
		{
			return audio->Volume;
		}

		void Audio::SetVolume(int volume)
		{
			return audio->Volume = volume;
		}

		bool Audio::Playing()
		{
			return audio->Playing();
		}

		bool Audio::Finished()
		{
			return audio->Finished();
		}
	}
}
