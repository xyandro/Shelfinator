﻿#pragma once

#include <memory>
#include "Light.h"
#include "DotStar.h"

namespace Shelfinator
{
	class Lights
	{
	public:
		typedef std::shared_ptr<Lights> ptr;
		static ptr Read(char *fileName);
		~Lights();
		void SetLights(int time, DotStar::ptr dotStar);
		int GetLength();
	private:
		Light * light;
		int length, lightCount;
		Lights(char *fileName);
	};
}
