using System;
using System.IO;

namespace Shelfinator
{
	class LightData
	{
		public int StartTime { get; set; }
		public int EndTime { get; set; }
		public int Duration { get => EndTime - StartTime; set => EndTime = StartTime + value; }
		public int StartColor { get; set; }
		public int EndColor { get; set; }

		public LightData(int startTime, int endTime, int startColor, int endColor)
		{
			StartTime = startTime;
			EndTime = endTime;
			StartColor = startColor;
			EndColor = endColor;
		}

		public int ColorAtTime(int time)
		{
			if (time < StartTime)
				throw new Exception("Time < StartTime");
			if (time >= EndTime)
				throw new Exception("Time >= EndTime");

			if (StartColor == EndColor)
				return StartColor;

			var percent = (time - StartTime) / (double)Duration;

			var R1 = (StartColor & 0xff0000) >> 16;
			var G1 = (StartColor & 0x00ff00) >> 8;
			var B1 = (StartColor & 0x0000ff) >> 0;

			var R2 = (EndColor & 0xff0000) >> 16;
			var G2 = (EndColor & 0x00ff00) >> 8;
			var B2 = (EndColor & 0x0000ff) >> 0;

			var R = (byte)Math.Max(0, Math.Min(R1 * (1 - percent) + R2 * percent, 255));
			var G = (byte)Math.Max(0, Math.Min(G1 * (1 - percent) + G2 * percent, 255));
			var B = (byte)Math.Max(0, Math.Min(B1 * (1 - percent) + B2 * percent, 255));

			return (R << 16) + (B << 8) + (G << 0);
		}

		public override string ToString() => $"{StartTime}-{EndTime}, {StartColor:X6}-{EndColor:X6}";

		public void Write(BinaryWriter output)
		{
			output.Write(StartTime);
			output.Write(EndTime);
			output.Write(StartColor);
			output.Write(EndColor);
		}
	}
}
