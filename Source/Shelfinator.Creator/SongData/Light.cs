using System;
using System.IO;

namespace Shelfinator.Creator.SongData
{
	class Light
	{
		public const int AbsoluteColor = -1;

		public int StartTime { get; set; }
		public int EndTime { get; set; }
		public int OrigEndTime { get; set; }
		public int? NullableStartColorIndex { get; set; }
		public int StartColorIndex { get => NullableStartColorIndex.Value; set => NullableStartColorIndex = value; }
		public int StartColorValue { get; set; }
		public int EndColorIndex { get; set; }
		public int EndColorValue { get; set; }
		public bool Intermediates { get; set; }

		public Light(int startTime, int endTime, int? nullableStartColorIndex, int startColorValue, int endColorIndex, int endColorValue, bool intermediates)
		{
			StartTime = startTime;
			EndTime = OrigEndTime = endTime;
			NullableStartColorIndex = nullableStartColorIndex;
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

		public void GetColor(int time, out int colorIndex, out int colorValue)
		{
			if ((time < StartTime) || (time > OrigEndTime))
				throw new ArgumentOutOfRangeException("Time not in interval");

			if (StartColorIndex != EndColorIndex)
				throw new Exception("Can't get color between indexes");

			colorIndex = StartColorIndex;
			var percent = (double)(time - StartTime) / (OrigEndTime - StartTime);
			if (StartColorIndex == Light.AbsoluteColor)
				colorValue = Helpers.GradientColor(StartColorValue, EndColorValue, percent);
			else if (Intermediates)
				colorValue = (StartColorValue * (1 - percent) + EndColorValue * percent).Round();
			else if (StartColorValue == EndColorValue)
				colorValue = StartColorValue;
			else
				throw new Exception("Can't determine previous end color");
		}

		public bool DoJoin(Light newLight)
		{
			if (EndTime != newLight.StartTime)
				return false;
			if (EndTime != OrigEndTime)
				return false;
			if (newLight.EndTime != newLight.OrigEndTime)
				return false;
			if (StartColorIndex != EndColorIndex)
				return false;
			if (newLight.StartColorIndex != newLight.EndColorIndex)
				return false;
			if (StartColorIndex != newLight.StartColorIndex)
				return false;
			if (EndColorValue != newLight.StartColorValue)
				return false;
			if (Intermediates != newLight.Intermediates)
				return false;

			if ((StartColorValue != EndColorValue) || (newLight.StartColorValue != newLight.EndColorValue) || (StartColorValue != newLight.StartColorValue))
				if (newLight.StartColorIndex == AbsoluteColor)
					return false;
				else if (!Intermediates)
					return false;
				else if ((EndColorValue - StartColorValue) * (newLight.EndTime - newLight.StartTime) != (newLight.EndColorValue - newLight.StartColorValue) * (EndTime - StartTime))
					return false;

			EndTime = newLight.EndTime;
			OrigEndTime = newLight.OrigEndTime;
			EndColorValue = newLight.EndColorValue;
			return true;
		}

		public override string ToString() => $"Time: {StartTime}-{EndTime}{(OrigEndTime != EndTime ? $" ({OrigEndTime})" : "")}, Color: {StartColorIndex} ({StartColorValue}) -> {EndColorIndex} ({EndColorValue}){(Intermediates ? " (Intermediates)" : "")}";
	}
}
