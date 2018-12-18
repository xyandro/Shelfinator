using System.IO;

namespace Shelfinator.Creator.SongData
{
	class Light
	{
		public int StartTime { get; set; }
		public int EndTime { get; set; }
		public int OrigEndTime { get; set; }
		public int StartColorIndex { get; set; }
		public int StartColorValue { get; set; }
		public int EndColorIndex { get; set; }
		public int EndColorValue { get; set; }
		public bool Intermediates { get; set; }

		public Light(int startTime, int endTime, int startColorIndex, int startColorValue, int endColorIndex, int endColorValue, bool intermediates)
		{
			StartTime = startTime;
			EndTime = OrigEndTime = endTime;
			StartColorIndex = startColorIndex;
			StartColorValue = startColorValue;
			EndColorIndex = endColorIndex;
			EndColorValue = endColorValue;
			Intermediates = intermediates;
		}

		public void Save(BinaryWriter output)
		{
			output.Write(StartTime);
			output.Write(EndTime);
			output.Write(OrigEndTime);
			output.Write(StartColorIndex);
			output.Write(StartColorValue);
			output.Write(EndColorIndex);
			output.Write(EndColorValue);
			output.Write(Intermediates);
		}

		public override string ToString() => $"Time: {StartTime}-{EndTime} ({OrigEndTime}), Color: {StartColorIndex} ({StartColorValue})-{EndColorIndex} ({EndColorValue})";
	}
}
