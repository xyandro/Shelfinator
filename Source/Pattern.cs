using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Shelfinator
{
	class Pattern
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

			public Light(int startTime, int endTime, int startColorIndex, int startColorValue, int endColorIndex, int endColorValue)
			{
				StartTime = startTime;
				EndTime = OrigEndTime = endTime;
				StartColorIndex = startColorIndex;
				StartColorValue = startColorValue;
				EndColorIndex = endColorIndex;
				EndColorValue = endColorValue;
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
			}

			public override string ToString() => $"Time: {StartTime}-{EndTime} ({OrigEndTime}), Color: {StartColorIndex} ({StartColorValue})-{EndColorIndex} ({EndColorValue})";
		}

		class LightSequence
		{
			public int LightStartTime { get; set; }
			public int LightEndTime { get; set; }
			public int Duration { get; set; }

			public LightSequence(int startTime, int endTime, int? duration = null)
			{
				LightStartTime = startTime;
				LightEndTime = endTime;
				Duration = duration ?? LightEndTime - LightStartTime;
			}

			public void Save(BinaryWriter output)
			{
				output.Write(LightStartTime);
				output.Write(LightEndTime);
				output.Write(Duration);
			}

			public override string ToString() => $"Time: {LightStartTime}-{LightEndTime}, Duration: {Duration}";
		}

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

		public LightColor Absolute { get; } = new LightColor(0x000000);

		readonly Dictionary<int, List<Light>> lights = new Dictionary<int, List<Light>>();
		readonly List<LightSequence> lightSequences = new List<LightSequence>();
		readonly List<LightColor> colors = new List<LightColor>();
		readonly Dictionary<LightColor, int> colorLookup = new Dictionary<LightColor, int>();
		readonly List<PaletteSequence> paletteSequences = new List<PaletteSequence>();

		public Pattern()
		{
			paletteSequences.Add(new PaletteSequence(0, int.MaxValue, 0, 0));
		}

		List<Light> GetLightList(int light)
		{
			if (!lights.ContainsKey(light))
				lights[light] = new List<Light> { new Light(0, int.MaxValue, -1, 0x000000, -1, 0x000000) };
			return lights[light];
		}

		public void AddLight(int light, int time, LightColor color) => DoAddLight(light, time, int.MaxValue, GetColorIndex(color), 0, GetColorIndex(color).Value, 0);
		public void AddLight(int light, int time, LightColor color, int colorValue) => DoAddLight(light, time, int.MaxValue, GetColorIndex(color), colorValue, GetColorIndex(color).Value, colorValue);
		public void AddLight(int light, int startTime, int endTime, LightColor startColor, LightColor endColor) => DoAddLight(light, startTime, endTime, GetColorIndex(startColor), 0, GetColorIndex(endColor).Value, 0);
		public void AddLight(int light, int startTime, int endTime, LightColor startColor, LightColor endColor, int endColorValue) => DoAddLight(light, startTime, endTime, GetColorIndex(startColor), 0, GetColorIndex(endColor).Value, endColorValue);
		public void AddLight(int light, int startTime, int endTime, LightColor startColor, int startColorValue, LightColor endColor) => DoAddLight(light, startTime, endTime, GetColorIndex(startColor), startColorValue, GetColorIndex(endColor).Value, 0);
		public void AddLight(int light, int startTime, int endTime, LightColor startColor, int startColorValue, LightColor endColor, int endColorValue) => DoAddLight(light, startTime, endTime, GetColorIndex(startColor), startColorValue, GetColorIndex(endColor).Value, endColorValue);

		void DoAddLight(int light, int startTime, int endTime, int? startColorIndex, int startColorValue, int endColorIndex, int endColorValue)
		{
			if (startTime > endTime)
				throw new ArgumentException("startTime > endTime");

			if (startTime == endTime)
			{
				startColorIndex = endColorIndex;
				startColorValue = endColorValue;
			}

			var list = GetLightList(light);
			var index = 0;
			while (startTime >= list[index].EndTime)
				++index;

			if (!startColorIndex.HasValue)
			{
				if (list[index].StartColorIndex != list[index].EndColorIndex)
					throw new Exception("Can't determine start color");
				startColorIndex = list[index].StartColorIndex;
				var percent = (double)(startTime - list[index].StartTime) / (list[index].OrigEndTime - list[index].StartTime);
				if (list[index].StartColorIndex == -1)
					startColorValue = Helpers.GradientColor(list[index].StartColorValue, list[index].EndColorValue, percent);
				else
					startColorValue = (int)(list[index].StartColorValue * (1 - percent) + list[index].EndColorValue * percent + 0.5);
			}

			if ((startColorIndex == endColorIndex) && (startColorValue == endColorValue))
				endTime = int.MaxValue;

			// Check for duplicates on solid colors
			if ((endTime == int.MaxValue) && (list[index].EndTime == int.MaxValue) && (startColorIndex == list[index].StartColorIndex) && (startColorValue == list[index].StartColorValue))
				return;

			list.RemoveRange(index + 1, list.Count - index - 1);

			if (list[index].StartTime == startTime)
				list.RemoveAt(index);
			else
				list[index].EndTime = startTime;

			list.Add(new Light(startTime, endTime, startColorIndex.Value, startColorValue, endColorIndex, endColorValue));
			if (endTime != int.MaxValue)
				list.Add(new Light(endTime, int.MaxValue, endColorIndex, endColorValue, endColorIndex, endColorValue));
		}

		public void Clear(int time) => lights.Keys.ForEach(light => AddLight(light, time, Absolute, 0x000000));

		public int MaxLightTime() => lights.Max(pair => pair.Value.Max(lightData => lightData.EndTime == int.MaxValue ? lightData.StartTime : lightData.EndTime));

		public void AddLightSequence(int startTime, int endTime, int? duration = null, int repeat = 1)
		{
			var lightSequence = new LightSequence(startTime, endTime, duration ?? endTime - startTime);
			for (var ctr = 0; ctr < repeat; ++ctr)
				lightSequences.Add(lightSequence);
		}

		public int? GetColorIndex(LightColor color)
		{
			if (color == null)
				return null;
			if (color == Absolute)
				return -1;
			if (!colorLookup.ContainsKey(color))
			{
				colorLookup[color] = colors.Count;
				colors.Add(color);
			}
			return colorLookup[color];
		}

		public void AddPaletteSequence(int time, int paletteIndex) => AddPaletteSequence(time, time, paletteIndex, paletteIndex);

		public void AddPaletteSequence(int startTime, int endTime, int? startPaletteIndex, int endPaletteIndex)
		{
			if (startTime > endTime)
				throw new ArgumentException("startTime > endTime");

			if (startTime == endTime)
				startPaletteIndex = endPaletteIndex;

			var index = 0;
			while (startTime >= paletteSequences[index].EndTime)
				++index;

			if (!startPaletteIndex.HasValue)
			{
				if (paletteSequences[index].StartPaletteIndex != paletteSequences[index].EndPaletteIndex)
					throw new Exception("Can't determine start palette index");
				startPaletteIndex = paletteSequences[index].StartPaletteIndex;
			}

			if (startPaletteIndex == endPaletteIndex)
				endTime = int.MaxValue;

			paletteSequences.RemoveRange(index + 1, paletteSequences.Count - index - 1);

			if (paletteSequences[index].StartTime == startTime)
				paletteSequences.RemoveAt(index);
			else
				paletteSequences[index].EndTime = startTime;

			paletteSequences.Add(new PaletteSequence(startTime, endTime, startPaletteIndex.Value, endPaletteIndex));
			if (endTime != int.MaxValue)
				paletteSequences.Add(new PaletteSequence(endTime, int.MaxValue, endPaletteIndex, endPaletteIndex));
		}

		public void Save(string fileName)
		{
			using (var output = new BinaryWriter(File.Create(fileName)))
				Save(output);
		}

		public void Save(BinaryWriter output)
		{
			var numLights = lights.Keys.Max() + 1;
			output.Write(numLights);
			for (var light = 0; light < numLights; ++light)
			{
				var lightList = GetLightList(light);
				output.Write(lightList.Count);
				foreach (var lightListItem in lightList)
					lightListItem.Save(output);
			}

			output.Write(lightSequences.Count);
			foreach (var lightSequence in lightSequences)
				lightSequence.Save(output);

			output.Write(colors.Count);
			foreach (var color in colors)
				color.Save(output);

			output.Write(paletteSequences.Count);
			foreach (var paletteSequence in paletteSequences)
				paletteSequence.Save(output);
		}
	}
}
