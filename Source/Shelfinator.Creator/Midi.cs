using System;
using System.Runtime.InteropServices;

namespace Shelfinator.Creator
{
	class Midi : IDisposable
	{
		IntPtr midi;

		public Midi()
		{
			if (Win32.midiOutOpen(out midi, 0, IntPtr.Zero, IntPtr.Zero, 0) != 0)
				throw new Exception("Failed to open MIDI");
		}

		public void Dispose()
		{
			if (midi == IntPtr.Zero)
				return;
			Win32.midiOutClose(midi);
			midi = IntPtr.Zero;
		}

		uint MakeDword(int a, int b, int c, int d) => ((uint)d << 24) + ((uint)c << 16) + ((uint)b << 8) + (uint)a;
		public void NoteOn(int note, int volume) => Win32.midiOutShortMsg(midi, MakeDword(144, note, volume, 0));
		public void NoteOff(int note) => Win32.midiOutShortMsg(midi, MakeDword(128, note, 0, 0));

		class Win32
		{
			[DllImport("winmm.dll")]
			public static extern uint midiOutOpen(out IntPtr midi, uint deviceID, IntPtr callback, IntPtr instance, uint flags);
			[DllImport("winmm.dll")]
			public static extern uint midiOutClose(IntPtr midi);
			[DllImport("winmm.dll")]
			public static extern uint midiOutShortMsg(IntPtr midi, uint msg);
		}
	}
}
