﻿namespace Shelfinator.Creator
{
	public class MidiNote
	{
		public enum PianoNote
		{
			C = 0,
			CSharp = 1,
			DFlat = 1,
			D = 2,
			DSharp = 3,
			EFlat = 3,
			E = 4,
			F = 5,
			FSharp = 6,
			GFlat = 6,
			G = 7,
			GSharp = 8,
			AFlat = 8,
			A = 9,
			ASharp = 10,
			BFlat = 10,
			B = 11,
		}

		public int NoteValue { get; set; } = 0;
		public PianoNote Note { get => (PianoNote)(NoteValue % 12); set => NoteValue = NoteValue / 12 * 12 + (int)value; }
		public int Octave { get => NoteValue / 12; set => NoteValue = value * 12 + NoteValue % 12; }
		public int StartTime { get; set; }
		public int EndTime { get => StartTime + Length; set => Length = value - StartTime; }
		public int Length { get; set; }

		public MidiNote(PianoNote note, int octave, int startTime, int length)
		{
			Note = note;
			Octave = octave;
			StartTime = startTime;
			Length = length;
		}

		public MidiNote(int noteValue, int startTime, int length)
		{
			NoteValue = noteValue;
			StartTime = startTime;
			Length = length;
		}
	}
}
