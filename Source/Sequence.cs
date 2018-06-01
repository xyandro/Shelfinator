using System.IO;

namespace Shelfinator
{
	class Sequence
	{
		public int StartTime { get; set; }
		public int EndTime { get; set; }
		public int Repeat { get; set; }

		public Sequence()
		{
		}

		public Sequence(int startTime, int endTime, int repeat = 1)
		{
			StartTime = startTime;
			EndTime = endTime;
			Repeat = repeat;
		}

		public void Save(BinaryWriter output)
		{
			output.Write(StartTime);
			output.Write(EndTime);
			output.Write(Repeat);
		}
	}
}
