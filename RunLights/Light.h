#pragma once

#include <memory>
#include <vector>

#include "LightItem.h"

class Light
{
public:
	typedef std::shared_ptr<Light> ptr;

	static ptr Create(int totalTime);
	static ptr Read(FILE *file, int totalTime);

	void Add(int startTime, int endTime, unsigned int startColor, unsigned int endColor);
	void Add(LightItem::ptr item);
private:
	int totalTime;

	Light(int totalTime);
	void DoRead(FILE *file);
	std::vector<LightItem::ptr> lightData;
	int GetTimeIndex(int time);
};
