namespace Shelfinator
{
	class Light
	{
	public:
		class LightData
		{
		public:
			int startTime, endTime, startColor, endColor;
			int GetLight(int time);
		} *lightData = nullptr;
		int dataCount = 0;
		int currentData = 0;

		int GetLight(int time);
	};
}
