using System;
using System.IO;

namespace Shelfinator.Creator.PatternData
{
	class SegmentItem
	{
		public enum SegmentItemType
		{
			Linear,
			VelocityBased,
		}
		public int SegmentIndex { get; set; }
		public SegmentItemType Type { get; set; }
		public int StartTime { get; set; }
		public int EndTime { get; set; }
		public int SegmentStartTime { get; set; }
		public int SegmentEndTime { get; set; }
		public int StartVelocity { get; set; }
		public int EndVelocity { get; set; }
		public int BaseVelocity { get; set; }
		public int Duration { get; set; }
		public int Repeat { get; set; }

		public SegmentItem(int segmentIndex, int segmentStartTime, int segmentEndTime, int duration, int repeat)
		{
			SegmentIndex = segmentIndex;
			Type = SegmentItemType.Linear;
			SegmentStartTime = segmentStartTime;
			SegmentEndTime = segmentEndTime;
			Duration = duration;
			Repeat = repeat;
		}

		public SegmentItem(int segmentIndex, int segmentStartTime, int segmentEndTime, int startVelocity, int endVelocity, int baseVelocity, int repeat)
		{
			if (startVelocity + endVelocity == 0)
				throw new ArgumentException($"{nameof(startVelocity)} + {nameof(endVelocity)} cannot be zero");

			SegmentIndex = segmentIndex;
			Type = SegmentItemType.VelocityBased;
			SegmentStartTime = segmentStartTime;
			SegmentEndTime = segmentEndTime;
			StartVelocity = startVelocity;
			EndVelocity = endVelocity;
			BaseVelocity = baseVelocity;
			Duration = 2 * (segmentEndTime - segmentStartTime) * baseVelocity / (endVelocity + startVelocity);
			Repeat = repeat;
		}

		public void Save(BinaryWriter output)
		{
			output.Write(SegmentIndex);
			output.Write((byte)Type);
			output.Write(SegmentStartTime);
			output.Write(SegmentEndTime);
			output.Write(StartVelocity);
			output.Write(EndVelocity);
			output.Write(BaseVelocity);
			output.Write(Duration);
			output.Write(Repeat);
		}

		public override string ToString()
		{
			switch (Type)
			{
				case SegmentItemType.Linear: return $"{Type}: Time: {SegmentStartTime}-{SegmentEndTime}, Duration: {Duration}, Repeat: {Repeat}";
				case SegmentItemType.VelocityBased: return $"{Type}: Time: {SegmentStartTime}-{SegmentEndTime}, Velocity: {StartVelocity}-{EndVelocity} ({BaseVelocity}), Repeat: {Repeat}";
			}
			throw new InvalidDataException();
		}
	}
}
