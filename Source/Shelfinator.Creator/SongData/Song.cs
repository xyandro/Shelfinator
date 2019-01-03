using System;
using System.Collections.Generic;
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

		public SegmentItem AddSegmentWithRepeat(Segment segment, int segmentStartTime, int segmentEndTime, int startTime, int? duration = null, int repeat = 1)
		{
			var segmentDuration = segmentEndTime - segmentStartTime;
			duration = duration ?? segmentDuration;
			return InsertSegment(new SegmentItem(GetSegmentIndex(segment), segmentStartTime, segmentEndTime, startTime, startTime + duration.Value * repeat, segmentDuration, segmentDuration, duration.Value));
		}

		public SegmentItem RepeatSegmentByVelocity(Segment segment, int segmentStartTime, int segmentEndTime, int startTime, int duration, int startVelocity, int endVelocity, int baseVelocity)
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

		public void Save(int songNumber)
		{
			using (var output = new BinaryWriter(File.Create(Path.Combine(Helpers.BuildDirectory, $"{songNumber}.pat"))))
				Save(output);

			var audioFile = Path.Combine(Helpers.BuildDirectory, songFileName);
			if (!File.Exists(audioFile))
				File.Copy(Path.Combine(Helpers.AudioDirectory, songFileName), audioFile, true);
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
