using System;
using System.IO;

namespace Shelfinator
{
	class Light
	{
		public int StartTime { get; set; }
		public int EndTime { get; set; }
		public int Duration { get => EndTime - StartTime; set => EndTime = StartTime + value; }
		public PixelColor StartColor { get; set; }
		public PixelColor EndColor { get; set; }

		public Light(int startTime, int endTime, PixelColor startColor, PixelColor endColor)
		{
			StartTime = startTime;
			EndTime = endTime;
			StartColor = startColor;
			EndColor = endColor;
		}

		public Light()
		{
		}

		public PixelColor ColorAtTime(int time)
		{
			if (time < StartTime)
				throw new Exception("Time < StartTime");
			if (time >= EndTime)
				throw new Exception("Time >= EndTime");

			return PixelColor.Gradient(StartColor, EndColor, (time - StartTime) / (double)Duration);
		}

		public override string ToString() => $"{StartTime}-{EndTime}, {StartColor:X6}-{EndColor:X6}";

		public void Write(BinaryWriter output)
		{
			output.Write(StartTime);
			output.Write(EndTime);
			output.Write(StartColor.Color);
			output.Write(EndColor.Color);
		}

		public static Light Read(BinaryReader input)
		{
			var result = new Light();
			result.StartTime = input.ReadInt32();
			result.EndTime = input.ReadInt32();
			result.StartColor = new PixelColor(input.ReadInt32());
			result.EndColor = new PixelColor(input.ReadInt32());
			return result;
		}

		public int GetMaxTime() => EndTime != int.MaxValue ? EndTime : StartTime;
	}
}
