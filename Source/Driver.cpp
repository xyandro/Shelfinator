#include "stdafx.h"
#include "Driver.h"

#include <algorithm>
#include <string>
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
		int onPattern = 0, lastPattern = 0, onSequence = 0, patternIndex = 0, onRepeat = 0, time = -1;
		Pattern::ptr pattern;
		while (true)
		{
			auto code = remote->GetCode();
			if (code != None)
			{
				fprintf(stderr, "Code is %i\n", code);
				continue;
			}

			if (onPattern == 0)
			{
				onPattern = patternNumbers[patternIndex++];
				continue;
			}

			if (onPattern != lastPattern)
			{
				auto fileName = patternsPath + std::to_string(onPattern) + ".dat";
				pattern = Pattern::Read(fileName.c_str());
				lastPattern = onPattern;
				onSequence = onRepeat = 0;
				time = -1;
				continue;
			}

			if (time == -1)
			{
				time = pattern->GetSequenceStartTime(onSequence);
				continue;
			}

			if (time >= pattern->GetSequenceEndTime(onSequence))
			{
				++onRepeat;
				time = -1;
				continue;
			}

			if (onRepeat >= pattern->GetSequenceRepeat(onSequence))
			{
				++onSequence;
				onRepeat = 0;
				time = -1;
				continue;
			}

			pattern->SetLights(time, dotStar);
			dotStar->Show();
			time += 10;
		}
	}
}
