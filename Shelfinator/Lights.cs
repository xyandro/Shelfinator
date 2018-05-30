using System;
using System.Collections.Generic;
using System.IO;

namespace Shelfinator
{
	class Lights
	{
		readonly Dictionary<int, List<LightData>> lights = new Dictionary<int, List<LightData>>();
		public int Length { get; set; }

		List<LightData> GetLightDatas(int light)
		{
			if (!lights.ContainsKey(light))
				lights[light] = new List<LightData> { new LightData(0, int.MaxValue, 0x000000, 0x000000) };
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

			list.Add(new LightData(startTime, endTime, startColor.Value, endColor));
			if (endTime != int.MaxValue)
				list.Add(new LightData(endTime, int.MaxValue, endColor, endColor));
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

		public void Save(string fileName, int length, int numLights)
		{
			using (var output = new BinaryWriter(File.Create(fileName)))
				Save(output, length, numLights);
		}

		public void AdjustSpeed(double multiplier)
		{
			foreach (var pair in lights)
				foreach (var light in pair.Value)
					light.AdjustSpeed(multiplier);
			Length = (int)(Length * multiplier);
		}

		public void Save(BinaryWriter output, int length, int numLights)
		{
			output.Write(length);
			output.Write(numLights);
			for (var light = 0; light < numLights; ++light)
			{
				var lightDatas = GetLightDatas(light);
				output.Write(lightDatas.Count);
				foreach (var lightData in lightDatas)
					lightData.Write(output);
			}
		}

		public static Lights Load(string fileName)
		{
			using (var input = new BinaryReader(File.OpenRead(fileName)))
				return Load(input);
		}

		public static Lights Load(BinaryReader input)
		{
			var result = new Lights();
			result.Length = input.ReadInt32();
			var numLights = input.ReadInt32();
			for (var light = 0; light < numLights; ++light)
			{
				var lightDatasCount = input.ReadInt32();
				for (var lightData = 0; lightData < lightDatasCount; ++lightData)
				{
					if (!result.lights.ContainsKey(light))
						result.lights[light] = new List<LightData>();
					result.lights[light].Add(LightData.Read(input));
				}
			}
			return result;
		}
	}
}
