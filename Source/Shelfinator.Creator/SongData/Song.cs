using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Shelfinator.Creator.SongData
{
	class Song
	{
		readonly string normalSongFileName, editSongFileName;
		readonly List<Segment> segments = new List<Segment>();
		readonly List<SegmentItem> segmentItems = new List<SegmentItem>();
		readonly List<PaletteSequence> paletteSequences = new List<PaletteSequence>();
		readonly Dictionary<int, int> currentIndex = new Dictionary<int, int>();

		public Song(string normalSongFileName, string editSongFileName = null)
		{
			this.normalSongFileName = normalSongFileName;
			this.editSongFileName = editSongFileName;
			paletteSequences.Add(new PaletteSequence(0, int.MaxValue, 0, 0));
		}

		int GetSegmentIndex(Segment segment)
		{
			if (!segments.Contains(segment))
				segments.Add(segment);
			return segments.IndexOf(segment);
		}

		public void AddSegment(Segment segment, int segmentStartTime, int segmentEndTime, int startTime, int? duration = null, int repeat = 1)
		{
			var segmentDuration = Math.Abs(segmentEndTime - segmentStartTime);
			duration = duration ?? segmentDuration;
			InsertSegment(new SegmentItem(GetSegmentIndex(segment), segmentStartTime, segmentEndTime, Math.Abs(segmentEndTime - segmentStartTime) * repeat, startTime, startTime + duration.Value * repeat, segmentDuration, segmentDuration, duration.Value));
		}

		public void AddSegmentByVelocity(Segment segment, int segmentStartTime, int segmentEndTime, int segmentTime, int startTime, int duration, int startVelocity, int endVelocity, int baseVelocity)
		{
			InsertSegment(new SegmentItem(GetSegmentIndex(segment), segmentStartTime, segmentEndTime, segmentTime, startTime, startTime + duration, startVelocity, endVelocity, baseVelocity));
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
			byte[] data;
			using (var ms = new MemoryStream())
			{
				using (var output = new BinaryWriter(ms, Encoding.UTF8, true))
					Save(output, songNumber);
				data = ms.ToArray();
			}

			var outputFile = Path.Combine(Helpers.PatternDirectory, $"{songNumber}.pat");
			var fi = new FileInfo(outputFile);
			var write = (!fi.Exists) || (fi.Length != data.Length);
			if (!write)
			{
				var existing = File.ReadAllBytes(outputFile);
				for (var ctr = 0; ctr < data.Length; ++ctr)
					if (existing[ctr] != data[ctr])
					{
						write = true;
						break;
					}
			}

			if (write)
				File.WriteAllBytes(outputFile, data);
		}

		void SaveAudio(string inputFileName, string outputFileName)
		{
			var inputFile = Path.Combine(Helpers.AudioDirectory, inputFileName);
			var outputFile = Path.Combine(Helpers.PatternDirectory, outputFileName);
			if (!File.Exists(outputFile))
			{
				var ffmpegs = new List<string> { @"C:\Users\rspackma\Documents\YouTubeDL\bin\ffmpeg.exe", @"C:\Documents\YouTubeDL\ffmpeg\bin\ffmpeg.exe", @"C:\Users\rspackma\Documents\YouTubeDL\ffmpeg\bin\ffmpeg.exe" };
				var ffmpeg = ffmpegs.Where(File.Exists).First();
				using (var process = Process.Start(ffmpeg, $@"-i ""{Path.Combine(Helpers.AudioDirectory, inputFile)}"" ""{Path.Combine(Helpers.PatternDirectory, outputFile)}"""))
					process.WaitForExit();
			}
		}

		public void Save(BinaryWriter output, int songNumber)
		{
			var normalFileName = $"{songNumber}.wav";
			var editFileName = normalFileName;

			SaveAudio(normalSongFileName, normalFileName);
			if (editSongFileName != null)
			{
				editFileName = $"{songNumber}e.wav";
				SaveAudio(editSongFileName, editFileName);
			}

			output.Write(normalFileName);
			output.Write(editFileName);

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
