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
	const double Driver::multipliers[] = { -5, -3, -2, -1, -0.75, -0.5, -0.25, -0.125, 0, 0.125, 0.25, 0.5, 0.75, 1, 2, 3, 5 };
	const char *Driver::multiplierNames[] = { "-5", "-3", "-2", "-1", "-3/4", "-1/2", "-1/4", "-1/8", "A", "1/8", "1/4", "1/2", "3/4", "P", "2", "3", "5" };

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

	void Driver::AddIfPatternFile(std::string fileName)
	{
		std::transform(fileName.begin(), fileName.end(), fileName.begin(), tolower);
		if ((fileName.length() >= 4) && (fileName.substr(fileName.length() - 4, 4) == ".dat"))
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
		auto found = std::find(this->patternNumbers.begin(), this->patternNumbers.end(), patternNumber);
		if (found == this->patternNumbers.end())
			return;

		auto result = *found;
		this->patternNumbers.erase(found);
		this->patternNumbers.insert(this->patternNumbers.begin(), result);
	}

	bool Driver::HandleRemote()
	{
		auto lastMultiplierIndex = multiplierIndex;
		switch (remote->GetCode())
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
		default: return false;
		}

		if (lastMultiplierIndex != multiplierIndex)
			banner = Banner::Create(multiplierNames[multiplierIndex], 1000);

		return true;
	}

	void Driver::LoadPattern(bool startAtEnd)
	{
		while (patternIndex < 0)
			patternIndex += (int)patternNumbers.size();
		while (patternIndex >= patternNumbers.size())
			patternIndex -= (int)patternNumbers.size();

		auto fileName = patternsPath + std::to_string(patternNumbers[patternIndex]) + ".dat";
		pattern = Pattern::Read(fileName.c_str());
		time = startAtEnd ? pattern->GetLength() - 1 : 0;
	}

	void Driver::Run()
	{
		LoadPattern();
		std::shared_ptr<std::chrono::steady_clock::time_point> startTime, nextTime;
		while (true)
		{
			startTime = nextTime;
			nextTime.reset();

			if (HandleRemote())
				continue;

			if ((time < 0) || (time >= pattern->GetLength()))
			{
				patternIndex += time < 0 ? -1 : 1;
				LoadPattern(time < 0);
				continue;
			}

			pattern->SetLights(time, dotStar);

			if (banner)
				banner->SetLights(dotStar);

			dotStar->Show();
			nextTime = std::shared_ptr<std::chrono::steady_clock::time_point>(new std::chrono::steady_clock::time_point(std::chrono::steady_clock::now()));
			if (startTime)
			{
				auto elapsed = (int)std::chrono::duration_cast<std::chrono::milliseconds>(*nextTime - *startTime).count();
				time += (int)(elapsed * multipliers[multiplierIndex]);
				if (banner)
				{
					banner->AddElapsed(elapsed);
					if (banner->Expired())
						banner.reset();
				}
			}
		}
	}
}
