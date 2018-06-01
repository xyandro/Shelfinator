using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Shelfinator
{
	class Lights
	{
		readonly Dictionary<int, List<Light>> lights = new Dictionary<int, List<Light>>();

		List<Light> GetLightDatas(int light)
		{
			if (!lights.ContainsKey(light))
				lights[light] = new List<Light> { new Light(0, int.MaxValue, 0x000000, 0x000000) };
			return lights[light];
		}

		public void Add(int light, int time, PixelColor color) => Add(light, time, int.MaxValue, color, color);

		public void Add(int light, int startTime, int endTime, PixelColor? startColor, PixelColor endColor)
		{
			if (startTime > endTime)
				throw new ArgumentException("startTime > endTime");

			startColor = startColor ?? GetColor(light, startTime);

			if (startTime == endTime)
				startColor = endColor;
			if (startColor.Value == endColor)
				endTime = int.MaxValue;

			var list = GetLightDatas(light);
			var index = 0;
			while ((index < list.Count) && (startTime >= list[index].EndTime))
				++index;
			list.RemoveRange(index + 1, list.Count - index - 1);

			list[index].EndColor = list[index].ColorAtTime(startTime);
			list[index].EndTime = startTime;
			if (list[index].Duration == 0)
				list.RemoveAt(index);

			list.Add(new Light(startTime, endTime, startColor.Value, endColor));
			if (endTime != int.MaxValue)
				list.Add(new Light(endTime, int.MaxValue, endColor, endColor));
		}

		public PixelColor GetColor(int light, int time)
		{
			var list = GetLightDatas(light);
			var index = 0;
			while ((index < list.Count) && (time >= list[index].EndTime))
				++index;
			if (index == list.Count)
				return 0;
			return list[index].ColorAtTime(time);
		}

		public void Save(BinaryWriter output)
		{
			var numLights = lights.Keys.Max() + 1;
			output.Write(numLights);
			for (var light = 0; light < numLights; ++light)
			{
				var lightDatas = GetLightDatas(light);
				output.Write(lightDatas.Count);
				foreach (var lightData in lightDatas)
					lightData.Write(output);
			}
		}

		public int GetMaxTime() => lights.Max(pair => pair.Value.Max(lightData => lightData.GetMaxTime()));
	}
}
