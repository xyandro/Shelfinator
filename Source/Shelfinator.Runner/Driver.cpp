#include "stdafx.h"
#include "Driver.h"

namespace Shelfinator
{
	namespace Runner
	{
		const double Driver::multipliers[] = { -4, -3, -2, -1, -0.75, -0.5, -0.25, -0.125, 0, 0.125, 0.25, 0.5, 0.75, 1, 2, 3, 4 };
		const char *Driver::multiplierNames[] = { "RRRR", "RRR", "RR", "R", "R3/4", "R1/2", "R1/4", "R1/8", "P", "1/8F", "1/4F", "1/2F", "3/4F", "F", "FF", "FFF", "FFFF" };

		Driver::ptr Driver::Create(int *patternNumbers, int patternNumberCount, IDotStar::ptr dotStar, IRemote::ptr remote)
		{
			return ptr(new Driver(patternNumbers, patternNumberCount, dotStar, remote));
		}

		Driver::Driver(int *patternNumbers, int patternNumberCount, IDotStar::ptr dotStar, IRemote::ptr remote)
		{
			lightsQueue = LightsQueue::Create(2);

			this->dotStar = dotStar;
			this->remote = remote;
			start = GetTime();

			SetupPatternsPath();
			SetupPatternNumbers();

			if (patternNumberCount == 0)
				MakeFirst(1); // Hello
			else
				for (auto ctr = patternNumberCount - 1; ctr >= 0; --ctr)
					MakeFirst(patternNumbers[ctr]);
		}

		void Driver::SetupPatternsPath()
		{
			char buf[1024];
#ifdef _WIN32
			GetModuleFileNameA(NULL, buf, sizeof(buf) / sizeof(*buf));
			*strrchr(buf, '\\') = 0;
			strcat(buf, "\\");
#else
			readlink("/proc/self/exe", buf, sizeof(buf) / sizeof(*buf));
			*strrchr(buf, '/') = 0;
			strcat(buf, "/");
#endif
			patternsPath = buf;
		}

		void Driver::AddIfPatternFile(std::string fileName)
		{
			std::transform(fileName.begin(), fileName.end(), fileName.begin(), tolower);
			if ((fileName.length() >= 4) && (fileName.substr(fileName.length() - 4, 4) == ".pat"))
			{
				fileName = fileName.substr(0, fileName.length() - 4);
				auto patternNumber = atoi(fileName.c_str());
				if (patternNumber != 0)
					patternNumbers.push_back(patternNumber);
			}
		}

		void Driver::SetupPatternNumbers()
		{
#ifdef _WIN32
			WIN32_FIND_DATAA findData;
			auto find = patternsPath + "*.*";
			auto hFind = FindFirstFileA(find.c_str(), &findData);
			if (hFind == INVALID_HANDLE_VALUE)
				return;

			do
			{
				if (findData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)
					continue;

				AddIfPatternFile(findData.cFileName);
			} while (FindNextFileA(hFind, &findData) != 0);

			FindClose(hFind);
#else
			DIR *dir;
			struct dirent *ent;
			if ((dir = opendir(patternsPath.c_str())) == nullptr)
				return;

			while ((ent = readdir(dir)) != nullptr)
			{
				AddIfPatternFile(ent->d_name);
			}

			closedir(dir);
#endif

			std::random_shuffle(patternNumbers.begin(), patternNumbers.end());
		}

		void Driver::MakeFirst(int patternNumber)
		{
			auto found = std::find(patternNumbers.begin(), patternNumbers.end(), patternNumber);
			if (found == patternNumbers.end())
				return;

			auto result = *found;
			patternNumbers.erase(found);
			patternNumbers.insert(patternNumbers.begin(), result);
		}

		bool Driver::HandleRemote()
		{
			auto result = true;
			auto useSelectedNumber = false;
			auto now = Millis();
			if ((selectedNumberTime != -1) && (now - selectedNumberTime >= 1000))
				useSelectedNumber = true;

			auto lastMultiplierIndex = multiplierIndex;
			auto code = remote->GetCode();
			switch (code)
			{
			case Play: multiplierIndex = 13; break;
			case Pause: multiplierIndex = 8; break;
			case Rewind:
				if (multiplierIndex > 0)
					--multiplierIndex;
				break;
			case FastForward:
				if (multiplierIndex < sizeof(multipliers) / sizeof(*multipliers) - 1)
					++multiplierIndex;
				break;
			case Previous:
				if ((multipliers[multiplierIndex] == 0) || (time / multipliers[multiplierIndex] < 2000))
				{
					--patternIndex;
					LoadPattern();
				}
				else
					time = 0;
				break;
			case Next:
				++patternIndex;
				LoadPattern();
				break;
			case Enter: useSelectedNumber = true; break;
			case D0:
			case D1:
			case D2:
			case D3:
			case D4:
			case D5:
			case D6:
			case D7:
			case D8:
			case D9:
				selectedNumberTime = now;
				selectedNumber = selectedNumber * 10 + code - D0;
				if (selectedNumber >= 10000)
				{
					selectedNumber = 0;
					selectedNumberTime = -1;
				}
				else
					banner = Banner::Create(std::to_string(selectedNumber), 1000, 1);
				break;
			case Info: banner = Banner::Create(std::to_string(patternNumbers[patternIndex]), 1000, 1); break;
			default: result = false; break;
			}

			if (lastMultiplierIndex != multiplierIndex)
				banner = Banner::Create(multiplierNames[multiplierIndex], 1000);

			if (useSelectedNumber)
			{
				auto found = std::find(patternNumbers.begin(), patternNumbers.end(), selectedNumber);
				if (found != patternNumbers.end())
				{
					patternIndex = (int)std::distance(patternNumbers.begin(), found);
					LoadPattern();
				}

				selectedNumber = 0;
				selectedNumberTime = -1;
			}

			return result;
		}

		void Driver::LoadPattern(bool startAtEnd)
		{
			while (patternIndex < 0)
				patternIndex += (int)patternNumbers.size();
			while (patternIndex >= patternNumbers.size())
				patternIndex -= (int)patternNumbers.size();

			auto fileName = patternsPath + std::to_string(patternNumbers[patternIndex]) + ".pat";
			pattern = Pattern::Read(fileName.c_str());
			time = startAtEnd ? pattern->GetLength() - 1 : 0;
		}

		void Driver::RunUIThread()
		{
			while (true)
			{
				auto lights = lightsQueue->Get();
				if (!lights)
				{
					std::unique_lock<decltype(stopMutex)> lock(stopMutex);
					finished = true;
					stopCondVar.notify_all();
					break;
				}
				dotStar->Show(lights->lights, lights->count);
			}
		}

		int frameCount = 0;
		void Driver::Run()
		{
			std::thread(&Driver::RunUIThread, this).detach();

			auto startLoad = Millis();
			LoadPattern();
			auto loadTime = Millis() - startLoad;
			auto startDraw = Millis();
			int startTime, nextTime = -1;
			while (running)
			{
				startTime = nextTime;
				nextTime = -1;

				if (HandleRemote())
					continue;

				if ((time < 0) || (time >= pattern->GetLength()))
				{
					patternIndex += time < 0 ? -1 : 1;
					LoadPattern(time < 0);
					continue;
				}

				auto lights = Lights::Create(2440);
				pattern->SetLights(time, lights);
				if (banner)
					banner->SetLights(lights);
				lights->CheckOverage();
				lightsQueue->Add(lights);

				nextTime = Millis();
				if (startTime != -1)
				{
					auto elapsed = nextTime - startTime;
					time += (int)(elapsed * multipliers[multiplierIndex]);
					if (banner)
					{
						banner->AddElapsed(elapsed);
						if (banner->Expired())
							banner.reset();
					}
				}

				if (++frameCount == 300)
				{
					auto drawTime = Millis() - startDraw;
					auto fps = (double)frameCount / drawTime * 1000;
					fprintf(stderr, "Load time was %i. FPS is %f.\n", loadTime, fps);
				}
			}

			lightsQueue->Add(Lights::Create(2440));
			lightsQueue->Add(nullptr);

			std::unique_lock<decltype(stopMutex)> lock(stopMutex);
			while (!finished)
				stopCondVar.wait(lock);
		}

		void Driver::Stop()
		{
			running = false;
			std::unique_lock<decltype(stopMutex)> lock(stopMutex);
			while (!finished)
				stopCondVar.wait(lock);
		}

		std::shared_ptr<std::chrono::steady_clock::time_point> Driver::GetTime()
		{
			return std::shared_ptr<std::chrono::steady_clock::time_point>(new std::chrono::steady_clock::time_point(std::chrono::steady_clock::now()));
		}

		int Driver::Millis()
		{
			return Millis(GetTime());
		}

		int Driver::Millis(std::shared_ptr<std::chrono::steady_clock::time_point> atTime)
		{
			if (!atTime)
				atTime = GetTime();
			return (int)std::chrono::duration_cast<std::chrono::milliseconds>(*atTime - *start).count();
		}
	}
}
