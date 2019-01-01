﻿#include "Audio.h"

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

		void Audio::Play(std::string fileName)
		{
			return audio->Play(gcnew System::String(fileName.c_str()));
		}

		void Audio::Stop()
		{
			return audio->Stop();
		}

		int Audio::GetTime()
		{
			return audio->GetTime();
		}

		void Audio::SetTime(int time)
		{
			return audio->SetTime(time);
		}

	}
}