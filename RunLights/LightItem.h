#pragma once

#include <memory>

class LightItem
{
public:
	typedef std::shared_ptr<LightItem> ptr;

	static ptr Create(int startTime, unsigned int startColor);
	static ptr Create(int startTime, int endTime, unsigned int startColor, unsigned int endColor);
	static ptr Read(FILE *file);

	int GetStartTime();
	int GetEndTime();
	unsigned int GetStartColor();
	unsigned int GetEndColor();
private:
	int startTime, endTime;
	unsigned int startColor, endColor;

	LightItem(int startTime, int endTime, unsigned int startColor, unsigned int endColor);
	void DoRead(FILE *file);
};
