using System.IO;

namespace Shelfinator
{
	class Sequence
	{
		public int LightStartTime { get; set; }
		public int LightEndTime { get; set; }
		public int Repeat { get; set; }
		public int Duration { get; set; }

		public Sequence()
		{
		}

		public Sequence(int startTime, int endTime, int repeat = 1, int? duration = null)
		{
			LightStartTime = startTime;
			LightEndTime = endTime;
			Repeat = repeat;
			Duration = duration ?? LightEndTime - LightStartTime;
		}

		public void Save(BinaryWriter output)
		{
			output.Write(LightStartTime);
			output.Write(LightEndTime);
			output.Write(Duration);
			output.Write(Repeat);
		}
	}
}
