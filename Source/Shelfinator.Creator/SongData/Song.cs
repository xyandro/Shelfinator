﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

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

		public SegmentItem AddSegment(Segment segment, int segmentStartTime, int segmentEndTime, int startTime, int? duration = null, int repeat = 1)
		{
			var segmentDuration = segmentEndTime - segmentStartTime;
			duration = duration ?? segmentDuration;
			return InsertSegment(new SegmentItem(GetSegmentIndex(segment), segmentStartTime, segmentEndTime, startTime, startTime + duration.Value * repeat, segmentDuration, segmentDuration, duration.Value));
		}

		public SegmentItem AddSegmentByVelocity(Segment segment, int segmentStartTime, int segmentEndTime, int startTime, int duration, int startVelocity, int endVelocity, int baseVelocity)
		{
			return InsertSegment(new SegmentItem(GetSegmentIndex(segment), segmentStartTime, segmentEndTime, startTime, startTime + duration, startVelocity, endVelocity, baseVelocity));
		}

		public int GetDurationForRepeat(int segmentDuration, int startVelocity, int endVelocity, int baseVelocity, int repeat = 1)
		{
			return Math.Abs(segmentDuration * repeat * baseVelocity * 2 / (startVelocity + endVelocity));
		}

		SegmentItem InsertSegment(SegmentItem segmentItem)
		{
			if (segmentItem.StartTime < 0)
				throw new Exception("StartTime must be >= 0");

			var index = 0;
			while ((index < segmentItems.Count) && (segmentItem.StartTime >= segmentItems[index].StartTime))
				++index;

			if ((index != 0) && (segmentItems[index - 1].EndTime > segmentItem.StartTime))
				throw new Exception("Segments cannot overlap.");

			segmentItems.Add(segmentItem);
			return segmentItem;
		}

		public int MaxTime() => segmentItems.Select(s => s.EndTime).DefaultIfEmpty(0).Max();

		public void AddPaletteChange(int time, int paletteIndex) => AddPaletteChange(time, time, paletteIndex);

		public void AddPaletteChange(int startTime, int endTime, int endPaletteIndex)
		{
			if (startTime > endTime)
				throw new ArgumentException("startTime > endTime");

			var index = 0;
			while (startTime >= paletteSequences[index].EndTime)
				++index;

			int startPaletteIndex;
			if (startTime == endTime)
				startPaletteIndex = endPaletteIndex;
			else
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

			paletteSequences.Add(new PaletteSequence(startTime, endTime, startPaletteIndex, endPaletteIndex));
			if (endTime != int.MaxValue)
				paletteSequences.Add(new PaletteSequence(endTime, int.MaxValue, endPaletteIndex, endPaletteIndex));
		}

		public void Save(int songNumber)
		{
			Directory.CreateDirectory(Helpers.PatternDirectory);
			using (var output = new BinaryWriter(File.Create(Path.Combine(Helpers.PatternDirectory, $"{songNumber}.pat"))))
				Save(output);

			var audioFile = Path.Combine(Helpers.PatternDirectory, $"{songNumber}.wav");
			if (!File.Exists(audioFile))
			{
				var ffmpegs = new List<string> { @"C:\Users\rspackma\Documents\YouTubeDL\bin\ffmpeg.exe" };
				var ffmpeg = ffmpegs.Where(File.Exists).First();
				using (var process = Process.Start(ffmpeg, $@"-i ""{Path.Combine(Helpers.AudioDirectory, songFileName)}"" ""{audioFile}"""))
					process.WaitForExit();
			}
		}

		public void Save(BinaryWriter output)
		{
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
