#include "stdafx.h"
#include "Lights.h"

Lights::Lights()
{
}

void Lights::DoRead(FILE *file)
{
	int data;
	fread_s(&data, sizeof(data), 1, sizeof(data), file);
	name = std::shared_ptr<char[]>(new char[data + 1]);
	fread_s(name.get(), data, 1, data, file);
	name[data] = 0;

	fread_s(&totalTime, sizeof(totalTime), 1, sizeof(totalTime), file);

	fread_s(&data, sizeof(data), 1, sizeof(data), file);
	for (auto light = 0; light < data; ++light)
		lightsData.push_back(Light::Read(file, totalTime));
}

const char* Lights::GetName()
{
	return name.get();
}

Lights::ptr Lights::Read(const char *fileName)
{
	FILE *file;
	fopen_s(&file, fileName, "rb");
	auto result = Read(file);
	fclose(file);
	return result;
}

Lights::ptr Lights::Read(FILE *file)
{
	ptr result(new Lights);
	result->DoRead(file);
	return result;
}
