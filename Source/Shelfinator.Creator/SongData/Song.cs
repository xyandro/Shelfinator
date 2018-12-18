﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Shelfinator.Creator.SongData
{
	class Song
	{
		readonly string songFileName;
		readonly List<Segment> segments = new List<Segment>();
		readonly List<SegmentItem> segmentItems = new List<SegmentItem>();
		readonly List<PaletteSequence> paletteSequences = new List<PaletteSequence>();
		readonly Dictionary<int, int> currentIndex = new Dictionary<int, int>();

		public Song(string songFileName = null)
		{
			this.songFileName = songFileName;
			paletteSequences.Add(new PaletteSequence(0, int.MaxValue, 0, 0));
		}

		int GetSegmentIndex(Segment segment)
		{
			if (!segments.Contains(segment))
				segments.Add(segment);
			return segments.IndexOf(segment);
		}

		public int AddSegment(Segment segment, int segmentStartTime, int segmentEndTime, int? duration = null, int repeat = 1)
		{
			var sequence = new SegmentItem(GetSegmentIndex(segment), segmentStartTime, segmentEndTime, duration ?? segmentEndTime - segmentStartTime, repeat);
			segmentItems.Add(sequence);
			return sequence.Duration * sequence.Repeat;
		}

		public int AddSegment(Segment segment, int segmentStartTime, int segmentEndTime, int startVelocity, int endVelocity, int baseVelocity, int repeat = 1)
		{
			var sequence = new SegmentItem(GetSegmentIndex(segment), segmentStartTime, segmentEndTime, startVelocity, endVelocity, baseVelocity, repeat);
			segmentItems.Add(sequence);
			return sequence.Duration * sequence.Repeat;
		}

		public int MaxSegmentTime() => segmentItems.Sum(s => s.Duration * s.Repeat);

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
			output.Write(songFileName ?? "");

			output.Write(segments.Count);
			foreach (var segment in segments)
				segment.Save(output);

			output.Write(segmentItems.Count);
			foreach (var segmentItem in segmentItems)
				segmentItem.Save(output);

			output.Write(paletteSequences.Count);
			foreach (var paletteSequence in paletteSequences)
				paletteSequence.Save(output);
		}
	}
}
