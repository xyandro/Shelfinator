using System;

namespace Shelfinator.Patterns
{
	class LightData
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int StartTime { get; set; }
		public int EndTime { get; set; }
		public int Duration { get => EndTime - StartTime; set => EndTime = StartTime + value; }
		public int StartColor { get; set; }
		public int EndColor { get; set; }

		public int ColorAtTime(int time)
		{
			if (time < StartTime)
				throw new Exception("Time < StartTime");
			if (time >= EndTime)
				throw new Exception("Time >= EndTime");

			var percent2 = (time - StartTime) / (double)Duration;
			var percent1 = 1 - percent2;

			var R1 = (StartColor & 0xff0000) >> 16;
			var G1 = (StartColor & 0x00ff00) >> 8;
			var B1 = (StartColor & 0x0000ff) >> 0;

			var R2 = (EndColor & 0xff0000) >> 16;
			var G2 = (EndColor & 0x00ff00) >> 8;
			var B2 = (EndColor & 0x0000ff) >> 0;

			var R = (byte)Math.Max(0, Math.Min(R1 * percent1 + R2 * percent2, 255));
			var G = (byte)Math.Max(0, Math.Min(G1 * percent1 + G2 * percent2, 255));
			var B = (byte)Math.Max(0, Math.Min(B1 * percent1 + B2 * percent2, 255));

			return (R << 16) + (B << 8) + (G << 0);
		}

		public override string ToString() => $"{X},{Y}: {StartColor:X6}-{EndColor:X6}, {StartTime}-{EndTime}";
	}
}
