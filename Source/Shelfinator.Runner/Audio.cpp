#include "Audio.h"

#include <alsa/asoundlib.h>
#include <thread>
#include "Helpers.h"

namespace Shelfinator
{
	namespace Runner
	{
		const unsigned char Audio::Header[] = { 82, 73, 70, 70, 0, 0, 0, 0, 87, 65, 86, 69, 102, 109, 116, 32, 16, 0, 0, 0, 1, 0, 2, 0, 68, 172, 0, 0, 16, 177, 2, 0, 4, 0, 16, 0, 76, 73, 83, 84, 26, 0, 0, 0, 73, 78, 70, 79, 73, 83, 70, 84, 14, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 100, 97, 116, 97 };

		Audio::ptr Audio::Create()
		{
			return ptr(new Audio());
		}

		void Audio::Open(std::string fileName)
		{
			Close();

			file = fopen(fileName.c_str(), "rb");
			if (file == nullptr)
				throw "Invalid audio file";
			ValidateHeader();

			startTime = currentTime = 0;
			finished = false;
		}

		void Audio::ValidateHeader()
		{
			unsigned char buffer[sizeof(Header)];
			auto used = 0;
			fseek(file, 0, SEEK_SET);
			while (used < sizeof(Header))
			{
				auto block = (int)fread(buffer + used, 1, sizeof(Header) - used, file);
				if (block == 0)
					throw "Invalid audio file";
				used += block;
			}

			for (auto ctr = 0; ctr < sizeof(Header); ++ctr)
				if (((ctr < 4) || (ctr >= 8)) && ((ctr < 56) || (ctr >= 70)) && (Header[ctr] != buffer[ctr]))
					throw "Invalid audio file";
		}

		void Audio::Close()
		{
			Stop();
			if (file != nullptr)
			{
				fclose(file);
				file = nullptr;
			}
			finished = true;
		}

		void Audio::Play()
		{
			Stop();
			playing = IsPlaying;
			std::thread(&Audio::PlayWAVThread, this).detach();
		}

		void Audio::PlayWAVThread()
		{
			const auto frameSize = (int)sizeof(short) * 2;
			fseek(file, sizeof(Header) + startTime / 10 * 441 * frameSize, SEEK_SET);

			snd_pcm_t *pcm_handle;
			snd_pcm_open(&pcm_handle, "default", SND_PCM_STREAM_PLAYBACK, 0);

			snd_pcm_hw_params_t *hw_params;
			snd_pcm_hw_params_alloca(&hw_params);
			snd_pcm_hw_params_any(pcm_handle, hw_params);
			snd_pcm_hw_params_set_access(pcm_handle, hw_params, SND_PCM_ACCESS_RW_INTERLEAVED);
			snd_pcm_hw_params_set_format(pcm_handle, hw_params, SND_PCM_FORMAT_S16_LE);
			snd_pcm_hw_params_set_channels(pcm_handle, hw_params, 2);
			snd_pcm_hw_params_set_rate(pcm_handle, hw_params, 44100, 0);
			snd_pcm_hw_params(pcm_handle, hw_params);

			snd_pcm_sw_params_t *sw_params;
			snd_pcm_sw_params_alloca(&sw_params);
			snd_pcm_sw_params_current(pcm_handle, sw_params);
			snd_pcm_sw_params_set_tstamp_mode(pcm_handle, sw_params, SND_PCM_TSTAMP_ENABLE);
			snd_pcm_sw_params_set_tstamp_type(pcm_handle, sw_params, SND_PCM_TSTAMP_TYPE_MONOTONIC_RAW);
			snd_pcm_sw_params(pcm_handle, sw_params);

			snd_pcm_uframes_t frames;
			int dir;
			snd_pcm_hw_params_get_period_size(hw_params, &frames, &dir);

			snd_pcm_status_t *status;
			snd_pcm_status_alloca(&status);

			auto bufferSize = frames * frameSize;
			auto buffer = (short*)malloc(bufferSize);
			while (playing == IsPlaying)
			{
				auto readCount = 0;
				while (readCount < bufferSize)
				{
					auto block = fread(buffer + readCount, 1, bufferSize - readCount, file);
					if (block == 0)
						break;
					readCount += (int)block;
				}

				readCount /= frameSize;

				if (readCount == 0)
				{
					finished = true;
					break;
				}

				snd_pcm_status(pcm_handle, status);
				snd_htimestamp_t ts;
				snd_pcm_status_get_audio_htstamp(status, &ts);
				currentTime = startTime + ts.tv_sec * 1000 + ts.tv_nsec / 1000000;

				auto writeCount = snd_pcm_writei(pcm_handle, buffer, readCount);
				if (writeCount == -EPIPE)
				{
					fprintf(stderr, "Underrun!\n");
					snd_pcm_prepare(pcm_handle);
				}
				else if (writeCount < 0)
					fprintf(stderr, "Error writing to PCM device: %s\n", snd_strerror(writeCount));
				else if (writeCount != readCount)
					fprintf(stderr, "PCM write difffers from PCM read.\n");
			}

			free(buffer);
			snd_pcm_drain(pcm_handle);
			snd_pcm_close(pcm_handle);

			std::unique_lock<std::mutex> lock(mutex);
			playing = IsStopped;
			condVar.notify_all();
		}

		void Audio::Stop()
		{
			std::unique_lock<std::mutex> lock(mutex);
			if (playing != IsStopped)
			{
				playing = IsStopping;
				while (playing != IsStopped)
					condVar.wait(lock);
			}
			SetTime(currentTime);
		}

		int Audio::GetTime()
		{
			return currentTime;
		}

		void Audio::SetTime(int time)
		{
			auto playing = Playing();
			if (playing)
				Stop();
			startTime = currentTime = std::max(0, time - time % 10);
			if (playing)
				Play();
		}

		bool Audio::Playing()
		{
			return playing != IsStopped;
		}

		bool Audio::Finished()
		{
			return finished;
		}
	}
}
