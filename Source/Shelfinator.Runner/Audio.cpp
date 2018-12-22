#include "Audio.h"

#include <alsa/asoundlib.h>
#include <sndfile.h>
#include <thread>

namespace Shelfinator
{
	namespace Runner
	{
		Audio::ptr Audio::Create()
		{
			return ptr(new Audio());
		}

		void Audio::Play(std::string fileName)
		{
			Stop();
			time = 0;
			std::thread(&Audio::PlayWAVThread, this, fileName).detach();
		}

		void Audio::PlayWAVThread(std::string fileName)
		{
			snd_pcm_t *pcm_handle;
			snd_pcm_open(&pcm_handle, "default", SND_PCM_STREAM_PLAYBACK, 0);

			SF_INFO sf_info;
			auto infile = sf_open(fileName.c_str(), SFM_READ, &sf_info);
			snd_pcm_hw_params_t *hw_params;
			snd_pcm_hw_params_alloca(&hw_params);
			snd_pcm_hw_params_any(pcm_handle, hw_params);
			snd_pcm_hw_params_set_access(pcm_handle, hw_params, SND_PCM_ACCESS_RW_INTERLEAVED);
			snd_pcm_hw_params_set_format(pcm_handle, hw_params, SND_PCM_FORMAT_S16_LE);
			snd_pcm_hw_params_set_channels(pcm_handle, hw_params, sf_info.channels);
			snd_pcm_hw_params_set_rate(pcm_handle, hw_params, sf_info.samplerate, 0);
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

			auto buf = (short*)malloc(frames * sf_info.channels * sizeof(short));
			int readCount;
			playing = Playing;
			while ((playing == Playing) && ((readCount = sf_readf_short(infile, buf, frames)) > 0))
			{
				snd_pcm_status(pcm_handle, status);
				snd_htimestamp_t ts;
				snd_pcm_status_get_audio_htstamp(status, &ts);
				time = ts.tv_sec * 1000 + ts.tv_nsec / 1000000;

				auto writeCount = snd_pcm_writei(pcm_handle, buf, readCount);
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

			free(buf);
			sf_close(infile);
			snd_pcm_drain(pcm_handle);
			snd_pcm_close(pcm_handle);

			std::unique_lock<std::mutex> lock(mutex);
			playing = Stopped;
			condVar.notify_all();
			time = -1;
		}

		void Audio::Stop()
		{
			std::unique_lock<std::mutex> lock(mutex);
			if (playing == Stopped)
				return;
			playing = Stopping;
			while (playing != Stopped)
				condVar.wait(lock);
			time = -1;
		}

		int Audio::GetTime()
		{
			return time;
		}

		void Audio::SetTime(int time)
		{
		}
	}
}
