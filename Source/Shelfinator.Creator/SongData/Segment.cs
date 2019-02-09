using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Shelfinator.Creator.SongData
{
	class Segment
	{
		readonly List<List<Light>> lights = new List<List<Light>>();
		readonly List<LightColor> colors = new List<LightColor>();
		readonly Dictionary<LightColor, int> colorLookup = new Dictionary<LightColor, int>();

		List<Light> GetLightList(int light)
		{
			while (lights.Count <= light)
				lights.Add(new List<Light> { new Light(0, int.MaxValue, Light.AbsoluteColor, 0x000000, Light.AbsoluteColor, 0x000000, false) });
			return lights[light];
		}

		public void AddLight(int light, int time, LightColor color, int colorValue) => DoAddLight(light, new Light(time, int.MaxValue, GetColorIndex(color), colorValue, GetColorIndex(color).Value, colorValue, false));
		public void AddLight(int light, int time, int color) => DoAddLight(light, new Light(time, int.MaxValue, Light.AbsoluteColor, color, Light.AbsoluteColor, color, false));
		public void AddLight(int light, int startTime, int endTime, LightColor startColor, int startColorValue, LightColor endColor, int endColorValue, bool intermediates = false) => DoAddLight(light, new Light(startTime, endTime, GetColorIndex(startColor), startColorValue, GetColorIndex(endColor).Value, endColorValue, intermediates));
		public void AddLight(int light, int startTime, int endTime, int startColor, LightColor endColor, int endColorValue) => DoAddLight(light, new Light(startTime, endTime, Light.AbsoluteColor, startColor, GetColorIndex(endColor).Value, endColorValue, false));
		public void AddLight(int light, int startTime, int endTime, LightColor startColor, int startColorValue, int endColor) => DoAddLight(light, new Light(startTime, endTime, GetColorIndex(startColor), startColorValue, Light.AbsoluteColor, endColor, false));
		public void AddLight(int light, int startTime, int endTime, int startColor, int endColor) => DoAddLight(light, new Light(startTime, endTime, Light.AbsoluteColor, startColor, Light.AbsoluteColor, endColor, false));

		void DoAddLight(int light, Light newLight)
		{
			if (newLight.StartTime > newLight.EndTime)
				throw new ArgumentException("startTime > endTime");

			var list = GetLightList(light);
			var removeIndex = list.Count;
			while ((removeIndex > 0) && (list[removeIndex - 1].StartTime >= newLight.StartTime))
				--removeIndex;
			list.RemoveRange(removeIndex, list.Count - removeIndex);

			if (!newLight.NullableStartColorIndex.HasValue)
			{
				if ((list.Count == 0) || (list[list.Count - 1].EndTime <= newLight.StartTime))
				{
					newLight.StartColorIndex = Light.AbsoluteColor;
					newLight.StartColorValue = 0x000000;
				}
				else
				{
					list[list.Count - 1].GetColor(newLight.StartTime, out var startColorIndexValue, out var startColorValueValue);
					newLight.StartColorIndex = startColorIndexValue;
					newLight.StartColorValue = startColorValueValue;
				}
			}

			if (list.Count != 0)
			{
				var lastLight = list[list.Count - 1];
				lastLight.EndTime = newLight.StartTime;
				if ((lastLight.StartColorIndex == lastLight.EndColorIndex) && (lastLight.StartColorValue == lastLight.EndColorValue))
					lastLight.OrigEndTime = newLight.StartTime;
			}

			if ((newLight.StartTime == newLight.EndTime) && ((newLight.StartColorIndex != newLight.EndColorIndex) || (newLight.StartColorValue != newLight.EndColorValue)))
				throw new ArgumentException("Must have time range to fade colors");

			if ((newLight.StartColorIndex != newLight.EndColorIndex) || (newLight.StartColorValue == newLight.EndColorValue))
				newLight.Intermediates = false;

			if ((newLight.StartColorIndex == newLight.EndColorIndex) && (newLight.StartColorValue == newLight.EndColorValue))
				newLight.EndTime = newLight.OrigEndTime = int.MaxValue;

			if (list.LastOrDefault()?.DoJoin(newLight) != true)
				list.Add(newLight);

			if (newLight.EndTime != int.MaxValue)
				list.Add(new Light(newLight.EndTime, int.MaxValue, newLight.EndColorIndex, newLight.EndColorValue, newLight.EndColorIndex, newLight.EndColorValue, false));
		}

		public void Clear(int time, int color = 0x000000) => lights.ForEach((value, light) => AddLight(light, time, color));

		public int MaxTime() => lights.Max((Func<List<Light>, int>)((List<Light> list) => list.Max((Func<Light, int>)(lightData => (int)(lightData.EndTime == int.MaxValue ? lightData.StartTime : lightData.EndTime)))));

		int? GetColorIndex(LightColor color)
		{
			if (color == null)
				return null;
			if (!colorLookup.ContainsKey(color))
			{
				colorLookup[color] = colors.Count;
				colors.Add(color);
			}
			return colorLookup[color];
		}

		public void Save(BinaryWriter output)
		{
			output.Write(lights.Count);
			for (var light = 0; light < lights.Count; ++light)
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
