using System;
using System.IO;

namespace Shelfinator
{
	class LightData
	{
		public int StartTime { get; set; }
		public int EndTime { get; set; }
		public int Duration { get => EndTime - StartTime; set => EndTime = StartTime + value; }
		public PixelColor StartColor { get; set; }
		public PixelColor EndColor { get; set; }

		public LightData(int startTime, int endTime, PixelColor startColor, PixelColor endColor)
		{
			StartTime = startTime;
			EndTime = endTime;
			StartColor = startColor;
			EndColor = endColor;
		}

		public LightData()
		{
		}

		public PixelColor ColorAtTime(int time)
		{
			if (time < StartTime)
				throw new Exception("Time < StartTime");
			if (time >= EndTime)
				throw new Exception("Time >= EndTime");

			return PixelColor.MixColor(StartColor, EndColor, (time - StartTime) / (double)Duration);
		}

		public override string ToString() => $"{StartTime}-{EndTime}, {StartColor:X6}-{EndColor:X6}";

		public void Write(BinaryWriter output)
		{
			output.Write(StartTime);
			output.Write(EndTime);
			output.Write(StartColor.Color);
			output.Write(EndColor.Color);
		}

		public static LightData Read(BinaryReader input)
		{
			var result = new LightData();
			result.StartTime = input.ReadInt32();
			result.EndTime = input.ReadInt32();
			result.StartColor = new PixelColor(input.ReadInt32());
			result.EndColor = new PixelColor(input.ReadInt32());
			return result;
		}

		public void AdjustSpeed(double multiplier)
		{
			if (StartTime != int.MaxValue)
				StartTime = (int)(StartTime * multiplier);
			if (EndTime != int.MaxValue)
				EndTime = (int)(EndTime * multiplier);
		}

		public int GetMaxTime() => EndTime != int.MaxValue ? EndTime : StartTime;
	}
}
