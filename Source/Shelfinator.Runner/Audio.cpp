#include "Audio.h"

#include <alsa/asoundlib.h>
#include <thread>
#include "Helpers.h"

namespace Shelfinator
{
	namespace Runner
	{
		Audio::ptr Audio::Create()
		{
			return ptr(new Audio());
		}

		Audio::Audio()
		{
		}

		void Audio::Open(std::string normalFileName, std::string editedFileName)
		{
			Close();
			this->normalFileName = normalFileName;
			this->editedFileName = editedFileName;

			startTime = currentTime = 0;
			finished = false;
		}

		void Audio::ValidateHeader()
		{
#pragma pack(push, 1)
			struct WavChunk
			{
				char chunkID[4];
				int size;
			};
			struct WavChunkHeader : WavChunk
			{
				char format[4];
			};
			struct WavChunkFormat : WavChunk
			{
				short audioFormat;
				short numChannels;
				int sampleRate;
				int byteRate;
				short blockAlign;
				short bitsPerSample;
			};
#pragma pack(pop)

			char buffer[1024];
			auto used = 0;
			auto size = 0;
			auto totalSize = -1;
			while (true)
			{
				if (size == 0)
				{
					size = sizeof(WavChunk);
					used = 0;
				}

				while (used < size)
				{
					auto block = (int)fread(buffer + used, 1, size - used, file);
					if (block == 0)
						throw "Failed to read from audio file";
					used += block;
				}

				size = 0;
				auto wavChunk = (WavChunk&)buffer;
				if (strncmp(wavChunk.chunkID, "RIFF", 4) == 0)
				{
					if (used != sizeof(WavChunkHeader))
						size = sizeof(WavChunkHeader);
					else
					{
						auto wavChunkHeader = (WavChunkHeader&)buffer;
						if (strncmp(wavChunkHeader.format, "WAVE", 4) != 0)
							throw "Audio file lacks WAVE identifier";
						totalSize = wavChunkHeader.size;
					}
				}
				else if (strncmp(wavChunk.chunkID, "fmt ", 4) == 0)
				{
					if (used != sizeof(WavChunkFormat))
						size = sizeof(WavChunkFormat);
					else
					{
						auto wavChunkFormat = (WavChunkFormat&)buffer;
						if (wavChunkFormat.audioFormat != 1)
							throw "Invalid audio format";
						if (wavChunkFormat.numChannels != 2)
							throw "Invalid audio channel count";
						if (wavChunkFormat.sampleRate != 44100)
							throw "Invalid audio sample rate";
						if (wavChunkFormat.bitsPerSample != 16)
							throw "Invalid audio bits per sample";
					}
				}
				else if (strncmp(wavChunk.chunkID, "data", 4) == 0)
				{
					dataOffset = (int)ftell(file);
					if (dataOffset + wavChunk.size - totalSize - 8 != 0)
						throw "Audio has extra data";
					break;
				}
				else
					fseek(file, wavChunk.size, SEEK_CUR);
			}
		}

		void Audio::Close()
		{
			Stop();
			SetFile("");
			finished = true;
		}

		void Audio::SetFile(std::string fileName)
		{
			if (curFileName == fileName)
				return;
			curFileName = fileName;

			if (file != nullptr)
			{
				fclose(file);
				file = nullptr;
			}

			if (fileName.length() != 0)
			{
				file = fopen(curFileName.c_str(), "rb");
				if (file == nullptr)
					throw "Invalid audio file";
				ValidateHeader();
			}
		}

		void Audio::Play()
		{
			Stop();
			playing = IsPlaying;
			std::thread(&Audio::PlayWAVThread, this).detach();
		}

		void Audio::PlayWAVThread()
		{
			SetFile(edited ? editedFileName : normalFileName);

			const auto frameSize = (int)sizeof(short) * 2;
			fseek(file, dataOffset + startTime / 10 * 441 * frameSize, SEEK_SET);

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

		bool Audio::GetEdited()
		{
			return edited;
		}

		void Audio::SetEdited(bool edited)
		{
			auto playing = Playing();
			if (playing)
				Stop();
			this->edited = edited;
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
