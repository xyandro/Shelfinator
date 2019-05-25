using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Shelfinator.Interop;

namespace Shelfinator.Creator
{
	partial class Emulator : IDotStar, IAudio
	{
		public static int TestPosition { get; set; }
		public static List<MidiNote> TestNotes { get; set; }

		readonly Controller controller;
		readonly Dictionary<int, List<int>> bufferPosition = new Dictionary<int, List<int>>();
		readonly WriteableBitmap bitmap;
		readonly uint[] buffer;
		bool playing = false, finished = true, edited = false;
		string normalFileName, editedFileName;

		public Emulator(bool small, List<int> songNumbers, bool startPaused)
		{
			InitializeComponent();

			var image = small ? "Shelfinator.Creator.Songs.Layout.DotStar-Small.png" : "Shelfinator.Creator.Songs.Layout.DotStar.png";
			bitmap = new WriteableBitmap(Helpers.GetEmbeddedBitmap(image));
			buffer = new uint[bitmap.PixelWidth * bitmap.PixelHeight];
			bitmap.CopyPixels(buffer, bitmap.BackBufferStride, 0);

			bufferPosition = new Dictionary<int, List<int>>();
			var index = -1;
			for (var y = 0; y < bitmap.PixelHeight; ++y)
				for (var x = 0; x < bitmap.PixelWidth; ++x)
				{
					++index;
					if ((buffer[index] & 0xff000000) != 0x01000000)
						continue;
					var light = (int)(buffer[index] & 0xffffff);
					if (!bufferPosition.ContainsKey(light))
						bufferPosition[light] = new List<int>();
					bufferPosition[light].Add(index);
				}

			if (small)
			{
				SizeToContent = SizeToContent.WidthAndHeight;
				dotStarBitmap.Width = bitmap.PixelWidth + 1;
				dotStarBitmap.Height = bitmap.PixelHeight + 1;
				RenderOptions.SetBitmapScalingMode(dotStarBitmap, BitmapScalingMode.NearestNeighbor);

				Loaded += (s, e) =>
				{
					var workArea = SystemParameters.WorkArea;
					Left = (workArea.Width - Width) / 2 + workArea.Left;
					Top = workArea.Height - Height + workArea.Top;
				};
			}
			else
			{
				WindowStyle = WindowStyle.None;
				WindowState = WindowState.Maximized;
			}
			dotStarBitmap.Source = bitmap;
			controller = new Controller(this, this, songNumbers, startPaused);
			mediaPlayer.MediaEnded += (s, e) => { Stop(); finished = true; };
			new Thread(() => controller.Run()).Start();
			new Thread(() => PlayTestNotes()).Start();
		}

		bool ControlDown => Keyboard.Modifiers.HasFlag(ModifierKeys.Control);
		bool ShiftDown => Keyboard.Modifiers.HasFlag(ModifierKeys.Shift);

		void PlayTestNotes()
		{
			using (var midi = new Midi())
			{
				var playing = new List<MidiNote>();
				while (true)
				{
					if (TestNotes?.Any() != true)
					{
						Thread.Sleep(100);
						continue;
					}

					var time = Time + 200 * mediaPlayer.SpeedRatio;
					var notes = TestNotes.Where(note => (time >= note.StartTime) && (time < note.EndTime)).ToList();
					foreach (var note in playing.Except(notes))
						midi.NoteOff(note.NoteValue);
					foreach (var note in notes.Except(playing))
						midi.NoteOn(note.NoteValue, 60);
					playing = notes;
					Thread.Sleep(10);
				}
			}
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			e.Handled = true;
			switch (e.Key)
			{
				case Key.Escape: controller.AddRemoteCode(RemoteCode.Play); break;
				case Key.Space: controller.AddRemoteCode(RemoteCode.Pause); break;
				case Key.Left: controller.AddRemoteCode(ControlDown ? RemoteCode.Previous : RemoteCode.Rewind); break;
				case Key.Right: controller.AddRemoteCode(ControlDown ? RemoteCode.Next : RemoteCode.FastForward); break;
				case Key.Enter: controller.AddRemoteCode(RemoteCode.Enter); break;
				case Key.D0: controller.AddRemoteCode(RemoteCode.D0); break;
				case Key.D1: controller.AddRemoteCode(RemoteCode.D1); break;
				case Key.D2: controller.AddRemoteCode(RemoteCode.D2); break;
				case Key.D3: controller.AddRemoteCode(RemoteCode.D3); break;
				case Key.D4: controller.AddRemoteCode(RemoteCode.D4); break;
				case Key.D5: controller.AddRemoteCode(RemoteCode.D5); break;
				case Key.D6: controller.AddRemoteCode(RemoteCode.D6); break;
				case Key.D7: controller.AddRemoteCode(RemoteCode.D7); break;
				case Key.D8: controller.AddRemoteCode(RemoteCode.D8); break;
				case Key.D9: controller.AddRemoteCode(RemoteCode.D9); break;
				case Key.I: controller.AddRemoteCode(RemoteCode.Info); break;
				case Key.E: controller.AddRemoteCode(RemoteCode.Edited); break;
				case Key.S: controller.Stop(); break;
				case Key.P: CopyPosition(); break;
				case Key.T: Time = TestPosition; break;
				default: e.Handled = false; break;
			}
			base.OnKeyDown(e);
		}

		void CopyPosition()
		{
			var str = (ShiftDown ? DateTime.Now.TimeOfDay : mediaPlayer.Position).TotalMilliseconds.Round().ToString();
			if (ControlDown)
				str = Clipboard.GetText() + "," + str;
			Clipboard.SetText(str);
		}

		byte MapColor(byte color)
		{
			const int Diff = 10;
			if (color == 0)
				return 0;
			return (byte)Math.Max(0, Math.Min(255 + (color - 16) * Diff, 255));
		}

		uint GetColor(int color)
		{
			var r = MapColor((byte)((color >> 24) & 0xff));
			var g = MapColor((byte)((color >> 16) & 0xff));
			var b = MapColor((byte)((color >> 8) & 0xff));
			return 0xff000000 | (uint)(r << 16) | (uint)(g << 8) | b;
		}

		public void Show(int[] lights)
		{
			for (var light = 0; light < lights.Length; ++light)
				if (bufferPosition.ContainsKey(light))
					foreach (var position in bufferPosition[light])
						buffer[position] = GetColor(lights[light]);
			Dispatcher.Invoke(() => bitmap.WritePixels(new Int32Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight), buffer, bitmap.BackBufferStride, 0));
		}

		void SetupInput(int? pos = null)
		{
			Dispatcher.Invoke(() =>
			{
				//mediaPlayer.SpeedRatio = 0.8;
				var usePos = pos ?? mediaPlayer.Position.TotalMilliseconds.Round();
				mediaPlayer.Source = new Uri(Path.Combine(Helpers.PatternDirectory, Edited ? editedFileName : normalFileName));
				mediaPlayer.Position = TimeSpan.FromMilliseconds(usePos);
			});
		}

		public void Open(string normalFileName, string editedFileName)
		{
			Close();

			this.normalFileName = normalFileName;
			this.editedFileName = editedFileName;
			SetupInput(0);

			finished = false;
		}

		public new void Close()
		{
			Stop();
			Dispatcher.Invoke(() => mediaPlayer.Source = null);
			finished = true;
		}

		public void Play()
		{
			Dispatcher.Invoke(() => mediaPlayer.Play());
			playing = true;
		}

		public void Stop()
		{
			Dispatcher.Invoke(() => mediaPlayer.Pause());
			playing = false;
		}

		public int Time
		{
			get => Dispatcher.Invoke(() => mediaPlayer.Position.TotalMilliseconds.Round());
			set => Dispatcher.Invoke(() => mediaPlayer.Position = TimeSpan.FromMilliseconds(value));
		}

		public int Volume
		{
			get => Dispatcher.Invoke(() => (mediaPlayer.Volume * 10).Round());
			set => Dispatcher.Invoke(() => mediaPlayer.Volume = value / 10d);
		}

		public bool Edited
		{
			get => edited;
			set { edited = value; SetupInput(); }
		}

		public bool Playing() => playing;
		public bool Finished() => finished;
	}
}
