#pragma once

#include <memory>
#include <vector>

#include "Light.h"

class Lights
{
public:
	typedef std::shared_ptr<Lights> ptr;

	static ptr Read(const char *fileName);
	static ptr Read(FILE *file);

	const char *GetName();
private:
	std::vector<Light::ptr> lightsData;
	Lights();
	void DoRead(FILE *file);
	std::shared_ptr<char[]> name;
	int totalTime;
};
