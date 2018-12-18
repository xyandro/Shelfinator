using System.IO;

namespace Shelfinator.Creator.SongData
{
	class SegmentItem
	{
		public int SegmentIndex { get; set; }
		public int SegmentStartTime { get; set; }
		public int SegmentEndTime { get; set; }
		public int StartTime { get; set; }
		public int EndTime { get; set; }
		public int StartVelocity { get; set; }
		public int EndVelocity { get; set; }
		public int BaseVelocity { get; set; }
		public int Duration => EndTime - StartTime;

		public SegmentItem(int segmentIndex, int segmentStartTime, int segmentEndTime, int startTime, int endTime, int startVelocity, int endVelocity, int baseVelocity)
		{
			SegmentIndex = segmentIndex;
			SegmentStartTime = segmentStartTime;
			SegmentEndTime = segmentEndTime;
			StartTime = startTime;
			EndTime = endTime;
			StartVelocity = startVelocity;
			EndVelocity = endVelocity;
			BaseVelocity = baseVelocity;
		}

		public void Save(BinaryWriter output)
		{
			output.Write(SegmentIndex);
			output.Write(SegmentStartTime);
			output.Write(SegmentEndTime);
			output.Write(StartTime);
			output.Write(EndTime);
			output.Write(StartVelocity);
			output.Write(EndVelocity);
			output.Write(BaseVelocity);
		}

		public int SegmentTime(int time)
		{
			if (SegmentEndTime == SegmentStartTime)
				return SegmentEndTime;

			var useTime = (double)time - StartTime;
			var value = (useTime * useTime * (EndVelocity - StartVelocity) / BaseVelocity / (EndTime - StartTime) / 2 + useTime * StartVelocity / BaseVelocity) % (SegmentEndTime - SegmentStartTime) + SegmentStartTime;
			return value.Round();
		}
	}
}
