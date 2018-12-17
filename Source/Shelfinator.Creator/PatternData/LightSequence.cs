using System;
using System.IO;

namespace Shelfinator.Creator.PatternData
{
	class LightSequence
	{
		public enum LightSequenceType
		{
			Linear,
			VelocityBased,
		}
		public LightSequenceType Type { get; set; }
		public int StartTime { get; set; }
		public int EndTime { get; set; }
		public int LightStartTime { get; set; }
		public int LightEndTime { get; set; }
		public int StartVelocity { get; set; }
		public int EndVelocity { get; set; }
		public int BaseVelocity { get; set; }
		public int Duration { get; set; }
		public int Repeat { get; set; }

		public LightSequence(int lightStartTime, int lightEndTime, int duration, int repeat)
		{
			Type = LightSequenceType.Linear;
			LightStartTime = lightStartTime;
			LightEndTime = lightEndTime;
			Duration = duration;
			Repeat = repeat;
		}

		public LightSequence(int lightStartTime, int lightEndTime, int startVelocity, int endVelocity, int baseVelocity, int repeat)
		{
			if (startVelocity + endVelocity == 0)
				throw new ArgumentException($"{nameof(startVelocity)} + {nameof(endVelocity)} cannot be zero");

			Type = LightSequenceType.VelocityBased;
			LightStartTime = lightStartTime;
			LightEndTime = lightEndTime;
			StartVelocity = startVelocity;
			EndVelocity = endVelocity;
			BaseVelocity = baseVelocity;
			Duration = 2 * (lightEndTime - lightStartTime) * baseVelocity / (endVelocity + startVelocity);
			Repeat = repeat;
		}

		public void Save(BinaryWriter output)
		{
			output.Write((byte)Type);
			output.Write(LightStartTime);
			output.Write(LightEndTime);
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
				case LightSequenceType.Linear: return $"{Type}: Time: {LightStartTime}-{LightEndTime}, Duration: {Duration}, Repeat: {Repeat}";
				case LightSequenceType.VelocityBased: return $"{Type}: Time: {LightStartTime}-{LightEndTime}, Velocity: {StartVelocity}-{EndVelocity} ({BaseVelocity}), Repeat: {Repeat}";
			}
			throw new InvalidDataException();
		}
	}
}
