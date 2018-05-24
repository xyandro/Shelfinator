#include "stdafx.h"
#include "LightItem.h"

LightItem::ptr LightItem::Create(int startTime, unsigned int startColor)
{
	return LightItem::ptr(new LightItem(startTime, INT_MAX, startColor, startColor));
}

LightItem::ptr LightItem::Create(int startTime, int endTime, unsigned int startColor, unsigned int endColor)
{
	return LightItem::ptr(new LightItem(startTime, endTime, startColor, endColor));
}

LightItem::LightItem(int startTime, int endTime, unsigned int startColor, unsigned int endColor) : startTime(startTime), endTime(endTime), startColor(startColor), endColor(endColor) { }

LightItem::ptr LightItem::Read(FILE *file)
{
	auto result = LightItem::Create(0, 0, 0, 0);
	result->DoRead(file);
	return result;
}

void LightItem::DoRead(FILE *file)
{
	fread_s(&startTime, sizeof(startTime), 1, sizeof(startTime), file);
	fread_s(&endTime, sizeof(endTime), 1, sizeof(endTime), file);
	fread_s(&startColor, sizeof(startColor), 1, sizeof(startColor), file);
	fread_s(&endColor, sizeof(endColor), 1, sizeof(endColor), file);
}

int LightItem::GetStartTime() { return startTime; }
int LightItem::GetEndTime() { return endTime; }
unsigned int LightItem::GetStartColor() { return startColor; }
unsigned int LightItem::GetEndColor() { return endColor; }
