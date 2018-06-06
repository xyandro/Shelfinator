#pragma once

namespace Shelfinator
{
	class Lights
	{
	public:
		typedef std::shared_ptr<Lights> ptr;

		int count, *lights;

		static ptr Create(int count);
		~Lights();
		void Clear();
		void SetLight(int light, int value);
		void CheckOverage();
	private:
		Lights(int count);
	};
}
