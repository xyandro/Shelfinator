using System.IO;

namespace Shelfinator.Creator.SongData
{
	class PaletteSequence
	{
		public int StartTime { get; set; }
		public int EndTime { get; set; }
		public int StartPaletteIndex { get; set; }
		public int EndPaletteIndex { get; set; }

		public PaletteSequence(int startTime, int endTime, int startPaletteIndex, int endPaletteIndex)
		{
			StartTime = startTime;
			EndTime = endTime;
			StartPaletteIndex = startPaletteIndex;
			EndPaletteIndex = endPaletteIndex;
		}

		public void Save(BinaryWriter output)
		{
			output.Write(StartTime);
			output.Write(EndTime);
			output.Write(StartPaletteIndex);
			output.Write(EndPaletteIndex);
		}

		public override string ToString() => $"Time: {StartTime}-{EndTime}, Index: {StartPaletteIndex}-{EndPaletteIndex}";
	}
}
