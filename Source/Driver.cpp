#include "stdafx.h"
#include "Driver.h"

#include <algorithm>
#include <string>
#include <chrono>
#ifdef _WIN32
#include <Windows.h>
#else
#include <unistd.h>
#include <dirent.h>
#include <string.h>
#endif

namespace Shelfinator
{
	Driver::ptr Driver::Create(int *patternNumbers, int patternNumberCount, DotStar::ptr dotStar, Remote::ptr remote)
	{
		return ptr(new Driver(patternNumbers, patternNumberCount, dotStar, remote));
	}

	Driver::Driver(int *patternNumbers, int patternNumberCount, DotStar::ptr dotStar, Remote::ptr remote)
	{
		this->dotStar = dotStar;
		this->remote = remote;
		SetupPatternsPath();
		SetupPatternNumbers();

		for (auto ctr = 0; ctr < patternNumberCount; ++ctr)
		{
			auto found = std::find(this->patternNumbers.begin(), this->patternNumbers.end(), patternNumbers[ctr]);
			if (found != this->patternNumbers.end())
			{
				auto result = *found;
				this->patternNumbers.erase(found);
				this->patternNumbers.insert(this->patternNumbers.begin() + ctr, result);
			}
		}
	}

	void Driver::SetupPatternsPath()
	{
		char buf[1024];
#ifdef _WIN32
		GetModuleFileNameA(NULL, buf, sizeof(buf) / sizeof(*buf));
		for (auto ctr = 0; ctr < 4; ++ctr)
			*strrchr(buf, '\\') = 0;
		strcat(buf, "\\Patterns\\");
#else
		readlink("/proc/self/exe", buf, sizeof(buf) / sizeof(*buf));
		for (auto ctr = 0; ctr < 2; ++ctr)
			*strrchr(buf, '/') = 0;
		strcat(buf, "/Patterns/");
#endif
		patternsPath = buf;
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

			std::string found = findData.cFileName;
#else
		DIR *dir;
		struct dirent *ent;
		if ((dir = opendir(patternsPath.c_str())) == nullptr)
			return;

		while ((ent = readdir(dir)) != nullptr)
		{
			std::string found = ent->d_name;
#endif

			std::transform(found.begin(), found.end(), found.begin(), tolower);
			if ((found.length() >= 4) && (found.substr(found.length() - 4, 4) == ".dat"))
			{
				found = found.substr(0, found.length() - 4);
				auto patternNumber = atoi(found.c_str());
				if (patternNumber != 0)
					patternNumbers.push_back(patternNumber);
			}
#ifdef _WIN32
		} while (FindNextFileA(hFind, &findData) != 0);

		FindClose(hFind);
#else
		}

		closedir(dir);
#endif

		std::random_shuffle(patternNumbers.begin(), patternNumbers.end());
	}

	void Driver::Run()
	{
		static const double multipliers[] = { -5, -3, -2, -1, -0.75, -0.5, -0.25, -0.125, 0, 0.125, 0.25, 0.5, 0.75, 1, 2, 3, 5 };
		static const char *multiplierNames[] = { "-5", "-3", "-2", "-1", "-3/4", "-1/2", "-1/4", "-1/8", "0", "1/8", "1/4", "1/2", "3/4", "1", "2", "3", "5" };
		int onPattern = 0, loadedPattern = 0, patternIndex = -1, time = 0;
		Pattern::ptr pattern;
		std::shared_ptr<std::chrono::steady_clock::time_point> lastTime;
		int multiplierIndex = 13;
		while (true)
		{
			auto code = remote->GetCode();
			if (code != None)
			{
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
				}
				continue;
			}

			if (onPattern == 0)
			{
				patternIndex++;
				while (patternIndex < 0)
					patternIndex += (int)patternNumbers.size();
				while (patternIndex >= patternNumbers.size())
					patternIndex -= (int)patternNumbers.size();
				onPattern = patternNumbers[patternIndex];
				time = 0;
				lastTime.reset();
				continue;
			}

			if (onPattern != loadedPattern)
			{
				auto fileName = patternsPath + std::to_string(onPattern) + ".dat";
				pattern = Pattern::Read(fileName.c_str());
				loadedPattern = onPattern;
				time = 0;
				lastTime.reset();
				continue;
			}

			if (time >= pattern->GetLength())
			{
				onPattern = 0;
				lastTime.reset();
				continue;
			}

			pattern->SetLights(time, dotStar);
			dotStar->Show();
			std::shared_ptr<std::chrono::steady_clock::time_point> now(new std::chrono::steady_clock::time_point(std::chrono::steady_clock::now()));
			if (lastTime)
				time += (int)(std::chrono::duration_cast<std::chrono::milliseconds>(*now - *lastTime).count() * multipliers[multiplierIndex]);
			lastTime = now;
		}
	}
}
