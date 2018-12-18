using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace Shelfinator.Creator.SongData
{
	class Segment
	{
		static public LightColor Absolute { get; } = new LightColor(0x000000);

		readonly Dictionary<int, List<Light>> lights = new Dictionary<int, List<Light>>();
		readonly List<LightColor> colors = new List<LightColor>();
		readonly Dictionary<LightColor, int> colorLookup = new Dictionary<LightColor, int>();
		readonly Dictionary<int, int> currentIndex = new Dictionary<int, int>();

		List<Light> GetLightList(int light)
		{
			if (!lights.ContainsKey(light))
				lights[light] = new List<Light> { new Light(0, int.MaxValue, -1, 0x000000, -1, 0x000000, false) };
			return lights[light];
		}

		public void AddLight(int light, int time, LightColor color) => DoAddLight(light, time, int.MaxValue, GetColorIndex(color), 0, GetColorIndex(color).Value, 0, false);
		public void AddLight(int light, int time, LightColor color, int colorValue) => DoAddLight(light, time, int.MaxValue, GetColorIndex(color), colorValue, GetColorIndex(color).Value, colorValue, false);
		public void AddLight(int light, int startTime, int endTime, LightColor startColor, LightColor endColor, bool intermediates = false) => DoAddLight(light, startTime, endTime, GetColorIndex(startColor), 0, GetColorIndex(endColor).Value, 0, intermediates);
		public void AddLight(int light, int startTime, int endTime, LightColor startColor, LightColor endColor, int endColorValue, bool intermediates = false) => DoAddLight(light, startTime, endTime, GetColorIndex(startColor), 0, GetColorIndex(endColor).Value, endColorValue, intermediates);
		public void AddLight(int light, int startTime, int endTime, LightColor startColor, int startColorValue, LightColor endColor, bool intermediates = false) => DoAddLight(light, startTime, endTime, GetColorIndex(startColor), startColorValue, GetColorIndex(endColor).Value, 0, intermediates);
		public void AddLight(int light, int startTime, int endTime, LightColor startColor, int startColorValue, LightColor endColor, int endColorValue, bool intermediates = false) => DoAddLight(light, startTime, endTime, GetColorIndex(startColor), startColorValue, GetColorIndex(endColor).Value, endColorValue, intermediates);

		void DoAddLight(int light, int startTime, int endTime, int? startColorIndex, int startColorValue, int endColorIndex, int endColorValue, bool intermediates)
		{
			if (startTime > endTime)
				throw new ArgumentException("startTime > endTime");

			if (startTime == endTime)
			{
				startColorIndex = endColorIndex;
				startColorValue = endColorValue;
			}

			var list = GetLightList(light);
			if (!currentIndex.ContainsKey(light))
				currentIndex[light] = 0;
			while (startTime < list[currentIndex[light]].StartTime)
				--currentIndex[light];
			while (startTime >= list[currentIndex[light]].EndTime)
				++currentIndex[light];
			var lastLight = list[currentIndex[light]];

			if (!startColorIndex.HasValue)
			{
				if (lastLight.StartColorIndex != lastLight.EndColorIndex)
					throw new Exception("Can't determine start color");
				startColorIndex = lastLight.StartColorIndex;
				var percent = (double)(startTime - lastLight.StartTime) / (lastLight.OrigEndTime - lastLight.StartTime);
				if (lastLight.StartColorIndex == -1)
					startColorValue = Helpers.GradientColor(lastLight.StartColorValue, lastLight.EndColorValue, percent);
				else if (lastLight.Intermediates)
					startColorValue = (int)(lastLight.StartColorValue * (1 - percent) + lastLight.EndColorValue * percent + 0.5);
				else if (lastLight.StartColorValue == lastLight.EndColorValue)
					startColorValue = lastLight.StartColorValue;
				else
					throw new Exception("Can't determine start color");
			}

			if (startColorIndex != endColorIndex)
				intermediates = false;

			if ((startColorIndex == endColorIndex) && (startColorValue == endColorValue))
				endTime = int.MaxValue;

			// Check for duplicates on solid colors
			if ((endTime == int.MaxValue) && (lastLight.EndTime == int.MaxValue) && (startColorIndex == lastLight.StartColorIndex) && (startColorValue == lastLight.StartColorValue))
				return;

			list.RemoveRange(currentIndex[light] + 1, list.Count - currentIndex[light] - 1);

			if (lastLight.StartTime == startTime)
				list.RemoveAt(currentIndex[light]);
			else
				lastLight.EndTime = startTime;

			list.Add(new Light(startTime, endTime, startColorIndex.Value, startColorValue, endColorIndex, endColorValue, intermediates));
			if (endTime != int.MaxValue)
				list.Add(new Light(endTime, int.MaxValue, endColorIndex, endColorValue, endColorIndex, endColorValue, false));
		}

		public void Clear(int time) => lights.Keys.ForEach(light => AddLight(light, time, Absolute, 0x000000));

		public int MaxLightTime() => lights.Max(pair => pair.Value.Max(lightData => lightData.EndTime == int.MaxValue ? lightData.StartTime : lightData.EndTime));

		int? GetColorIndex(LightColor color)
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

		public void Save(BinaryWriter output)
		{
			var numLights = lights.Keys.DefaultIfEmpty(0).Max() + 1;
			output.Write(numLights);
			for (var light = 0; light < numLights; ++light)
			{
				var lightList = GetLightList(light);
				output.Write(lightList.Count);
				foreach (var lightListItem in lightList)
					lightListItem.Save(output);
			}

			output.Write(colors.Count);
			foreach (var color in colors)
				color.Save(output);
		}
	}
}
