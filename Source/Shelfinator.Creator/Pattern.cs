﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Shelfinator.Creator
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
			public bool Intermediates { get; set; }

			public Light(int startTime, int endTime, int startColorIndex, int startColorValue, int endColorIndex, int endColorValue, bool intermediates)
			{
				StartTime = startTime;
				EndTime = OrigEndTime = endTime;
				StartColorIndex = startColorIndex;
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

			public override string ToString() => $"Time: {StartTime}-{EndTime} ({OrigEndTime}), Color: {StartColorIndex} ({StartColorValue})-{EndColorIndex} ({EndColorValue})";
		}

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
		readonly Dictionary<int, int> currentIndex = new Dictionary<int, int>();

		public Pattern()
		{
			paletteSequences.Add(new PaletteSequence(0, int.MaxValue, 0, 0));
		}

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

		public int AddLightSequence(int lightStartTime, int lightEndTime, int? duration = null, int repeat = 1)
		{
			var sequence = new LightSequence(lightStartTime, lightEndTime, duration ?? lightEndTime - lightStartTime, repeat);
			lightSequences.Add(sequence);
			return sequence.Duration * sequence.Repeat;
		}

		public int AddLightSequence(int lightStartTime, int lightEndTime, int startVelocity, int endVelocity, int baseVelocity, int repeat = 1)
		{
			var sequence = new LightSequence(lightStartTime, lightEndTime, startVelocity, endVelocity, baseVelocity, repeat);
			lightSequences.Add(sequence);
			return sequence.Duration * sequence.Repeat;
		}

		public int MaxLightSequenceTime() => lightSequences.Sum(s => s.Duration * s.Repeat);

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

		public void Transmit(string host)
		{
			byte[] data;
			using (var ms = new MemoryStream())
			{
				using (var output = new BinaryWriter(ms, Encoding.UTF8, true))
				{
					output.Write(default(int));
					Save(output);
					output.Flush();
					output.Seek(0, SeekOrigin.Begin);
					output.Write((int)ms.Length);
				}
				data = ms.ToArray();
			}

			using (var tcpClient = new TcpClient(host, 7435))
				tcpClient.GetStream().Write(data, 0, data.Length);
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
