#include "stdafx.h"
#include "Light.h"

Light::ptr Light::Create(int totalTime)
{
	return Light::ptr(new Light(totalTime));
}

Light::Light(int totalTime) : totalTime(totalTime)
{
	lightData.push_back(LightItem::Create(0, totalTime, 0xff000000, 0xff000000));
}

Light::ptr Light::Read(FILE *file, int totalTime)
{
	auto result = Create(totalTime);
	result->DoRead(file);
	return result;
}

int Light::GetTimeIndex(int time)
{
	for (auto itr = lightData.begin(); itr != lightData.end(); ++itr)
		if ((time >= (*itr)->GetStartTime()) && (time < (*itr)->GetEndTime()))
			return (int)(itr - lightData.begin());
	throw "Bad index";
}

void Light::Add(LightItem::ptr item)
{
	auto index = GetTimeIndex(item->GetStartTime());
}

void Light::DoRead(FILE *file)
{
	int count;
	fread_s(&count, sizeof(count), 1, sizeof(count), file);
	for (auto ctr = 0; ctr < count; ++ctr)
		Add(LightItem::Read(file));
}
