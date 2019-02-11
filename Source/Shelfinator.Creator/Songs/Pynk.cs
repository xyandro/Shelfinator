using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Pynk : ISong
	{
		public int SongNumber => 8;

		readonly Layout bodyLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");

		List<Func<Point, Point>> Rotations = new List<Func<Point, Point>>
		{
			point => new Point(point.X, point.Y),
			point => new Point(-point.Y, point.X),
			point => new Point(-point.X, -point.Y),
			point => new Point(point.Y, -point.X),
		};


		Segment Intro()
		{
			const int SnapTime = 198;
			const int SnapFadeTime = 99;
			const int BeatSize = 20;
			const int NoteSize = 9;
			var segment = new Segment();

			var innerLights = bodyLayout.GetPositionLights(38, 38, 21, 21).Except(bodyLayout.GetPositionLights(40, 40, 17, 17)).ToList();
			var middleLights = bodyLayout.GetPositionLights(19, 19, 59, 59).Except(bodyLayout.GetPositionLights(21, 21, 55, 55)).ToList();
			var outerLights = bodyLayout.GetPositionLights(0, 0, 97, 97).Except(bodyLayout.GetPositionLights(2, 2, 93, 93)).ToList();

			var snapColor = new LightColor(0, 1000, new List<int> { 0x000010, 0x000001 });
			for (var time = 0; time <= 58806; time += 1188)
				if ((time != 20196) && (time != 58212))
					foreach (var light in innerLights)
					{
						var colorValue = (((bodyLayout.GetLightPosition(light) - Helpers.Center).Length - 9) * 194.47172793051).Round();
						segment.AddLight(light, time - colorValue * SnapFadeTime / 1000, snapColor, colorValue);
						segment.AddLight(light, time - colorValue * SnapFadeTime / 1000 + SnapTime, 0x000000);
					}

			var beats = new List<MidiNote>
			{
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 1782, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 1980, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 2178, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 2376, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 2673, 297),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 4158, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 4356, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 4554, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 5049, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 5643, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 6237, 297),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 6534, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 6732, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 6930, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 7128, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 7425, 297),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 8910, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 9108, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 9306, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 9801, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 10395, 297),
				new MidiNote(MidiNote.PianoNote.BFlat, 2, 10890, 297),

				new MidiNote(MidiNote.PianoNote.C, 3, 11286, 198),
				new MidiNote(MidiNote.PianoNote.C, 4, 11484, 198),
				new MidiNote(MidiNote.PianoNote.C, 5, 11682, 198),
				new MidiNote(MidiNote.PianoNote.C, 4, 11880, 297),
				new MidiNote(MidiNote.PianoNote.C, 3, 12177, 297),

				new MidiNote(MidiNote.PianoNote.AFlat, 2, 13662, 198),
				new MidiNote(MidiNote.PianoNote.AFlat, 3, 13860, 198),
				new MidiNote(MidiNote.PianoNote.AFlat, 4, 14058, 198),
				new MidiNote(MidiNote.PianoNote.G, 5, 14256, 297),
				new MidiNote(MidiNote.PianoNote.AFlat, 2, 14553, 297),
				new MidiNote(MidiNote.PianoNote.AFlat, 2, 15147, 297),
				new MidiNote(MidiNote.PianoNote.BFlat, 2, 15642, 297),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 16038, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 16236, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 16434, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 16929, 297),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 18414, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 18612, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 18810, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 19008, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 19305, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 19899, 297),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 20790, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 20988, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 21186, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 6, 21384, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 21681, 297),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 23166, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 23364, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 23562, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 24057, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 24651, 297),
				new MidiNote(MidiNote.PianoNote.G, 2, 25146, 297),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 25542, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 25740, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 25938, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 6, 26136, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 26433, 297),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 27918, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 28116, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 28314, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 28809, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 29403, 297),
				new MidiNote(MidiNote.PianoNote.F, 4, 29700, 198),
				new MidiNote(MidiNote.PianoNote.BFlat, 2, 29898, 198),
				new MidiNote(MidiNote.PianoNote.F, 4, 30096, 198),

				new MidiNote(MidiNote.PianoNote.C, 3, 30294, 198),
				new MidiNote(MidiNote.PianoNote.C, 4, 30492, 198),
				new MidiNote(MidiNote.PianoNote.C, 5, 30690, 198),
				new MidiNote(MidiNote.PianoNote.C, 4, 30888, 297),
				new MidiNote(MidiNote.PianoNote.C, 3, 31185, 297),

				new MidiNote(MidiNote.PianoNote.AFlat, 2, 32670, 198),
				new MidiNote(MidiNote.PianoNote.AFlat, 3, 32868, 198),
				new MidiNote(MidiNote.PianoNote.C, 5, 33066, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 33264, 297),
				new MidiNote(MidiNote.PianoNote.AFlat, 2, 33561, 297),
				new MidiNote(MidiNote.PianoNote.AFlat, 2, 34155, 297),
				new MidiNote(MidiNote.PianoNote.BFlat, 2, 34650, 297),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 35046, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 35244, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 35442, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 6, 35640, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 35937, 297),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 37422, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 37620, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 37818, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 38313, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 38907, 297),
				new MidiNote(MidiNote.PianoNote.G, 2, 39501, 297),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 39798, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 39996, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 40194, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 40392, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 40689, 297),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 42174, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 42372, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 42570, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 43065, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 43659, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 44253, 297),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 44550, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 44748, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 44946, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 45144, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 45441, 297),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 46926, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 47124, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 47322, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 47817, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 48411, 297),
				new MidiNote(MidiNote.PianoNote.BFlat, 2, 48906, 297),

				new MidiNote(MidiNote.PianoNote.C, 3, 49302, 198),
				new MidiNote(MidiNote.PianoNote.C, 4, 49500, 198),
				new MidiNote(MidiNote.PianoNote.C, 5, 49698, 198),
				new MidiNote(MidiNote.PianoNote.C, 4, 49896, 297),
				new MidiNote(MidiNote.PianoNote.C, 3, 50193, 297),

				new MidiNote(MidiNote.PianoNote.AFlat, 2, 51678, 198),
				new MidiNote(MidiNote.PianoNote.AFlat, 3, 51876, 198),
				new MidiNote(MidiNote.PianoNote.AFlat, 4, 52074, 198),
				new MidiNote(MidiNote.PianoNote.G, 5, 52272, 297),
				new MidiNote(MidiNote.PianoNote.AFlat, 2, 52569, 297),
				new MidiNote(MidiNote.PianoNote.AFlat, 2, 53163, 297),
				new MidiNote(MidiNote.PianoNote.BFlat, 2, 53658, 297),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 54054, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 54252, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 54450, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 54945, 297),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 56430, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 56628, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 56826, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 57024, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 57321, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 57915, 297),
			};

			var minBeatValue = beats.Min(beat => beat.NoteValue);
			var maxBeatValue = beats.Max(beat => beat.NoteValue);
			var beatDist = (89d - BeatSize) / (maxBeatValue - minBeatValue);
			foreach (var beat in beats)
			{
				var minVal = (beatDist * (beat.NoteValue - minBeatValue)).Round() + 4;
				var maxVal = minVal + BeatSize - 1;
				foreach (var light in outerLights)
				{
					var pos = bodyLayout.GetLightPosition(light);
					var show = false;

					if ((pos.Y < 2) && (pos.X >= minVal) && (pos.X <= maxVal))
						show = true;
					if ((pos.Y >= 95) && ((96 - pos.X) >= minVal) && ((96 - pos.X) <= maxVal))
						show = true;
					if ((pos.X < 2) && ((96 - pos.Y) >= minVal) && ((96 - pos.Y) <= maxVal))
						show = true;
					if ((pos.X >= 95) && (pos.Y >= minVal) && (pos.Y <= maxVal))
						show = true;

					if (show)
					{
						segment.AddLight(light, beat.StartTime, beat.StartTime + 198, 0x001000, 0x000400);
						segment.AddLight(light, beat.EndTime, 0x000000);
					}
				}
			}

			foreach (var light in middleLights)
			{
				segment.AddLight(light, 16038, 20790, 0x040102, 0x401020);
				segment.AddLight(light, 20790, 0x000000);
			}

			var notes = new List<MidiNote>
			{
				new MidiNote(MidiNote.PianoNote.G, 5, 20790, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 21384, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 21681, 297),
				new MidiNote(MidiNote.PianoNote.AFlat, 5, 21978, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 22275, 297),
				new MidiNote(MidiNote.PianoNote.F, 5, 22572, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 22869, 594),
				new MidiNote(MidiNote.PianoNote.C, 5, 24354, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 24948, 594),

				new MidiNote(MidiNote.PianoNote.G, 5, 25542, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 26136, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 26433, 297),
				new MidiNote(MidiNote.PianoNote.AFlat, 5, 26730, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 27027, 297),
				new MidiNote(MidiNote.PianoNote.F, 5, 27324, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 27621, 594),
				new MidiNote(MidiNote.PianoNote.C, 5, 29106, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 29700, 594),

				new MidiNote(MidiNote.PianoNote.G, 5, 30294, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 30888, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 31185, 297),
				new MidiNote(MidiNote.PianoNote.AFlat, 5, 31482, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 31779, 297),
				new MidiNote(MidiNote.PianoNote.F, 5, 32076, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 32373, 594),
				new MidiNote(MidiNote.PianoNote.C, 5, 33858, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 34452, 594),

				new MidiNote(MidiNote.PianoNote.G, 5, 35046, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 35640, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 35937, 297),
				new MidiNote(MidiNote.PianoNote.F, 5, 36234, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 36531, 297),
				new MidiNote(MidiNote.PianoNote.D, 5, 36828, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 37125, 594),

				new MidiNote(MidiNote.PianoNote.G, 5, 39798, 594),     new MidiNote(MidiNote.PianoNote.EFlat, 5, 39798, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 40392, 297),     new MidiNote(MidiNote.PianoNote.EFlat, 5, 40392, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 40689, 297),     new MidiNote(MidiNote.PianoNote.EFlat, 5, 40689, 297),
				new MidiNote(MidiNote.PianoNote.AFlat, 5, 40986, 297), new MidiNote(MidiNote.PianoNote.F, 5, 40986, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 41283, 297),     new MidiNote(MidiNote.PianoNote.EFlat, 5, 41283, 297),
				new MidiNote(MidiNote.PianoNote.F, 5, 41580, 297),     new MidiNote(MidiNote.PianoNote.D, 5, 41580, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 41877, 594), new MidiNote(MidiNote.PianoNote.C, 5, 41877, 594),
				new MidiNote(MidiNote.PianoNote.C, 5, 43362, 594),     new MidiNote(MidiNote.PianoNote.AFlat, 4, 43362, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 43956, 594),     new MidiNote(MidiNote.PianoNote.EFlat, 5, 43956, 594),

				new MidiNote(MidiNote.PianoNote.G, 5, 44550, 594),     new MidiNote(MidiNote.PianoNote.EFlat, 5, 44550, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 45144, 297),     new MidiNote(MidiNote.PianoNote.EFlat, 5, 45144, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 45441, 297),     new MidiNote(MidiNote.PianoNote.EFlat, 5, 45441, 297),
				new MidiNote(MidiNote.PianoNote.AFlat, 5, 45738, 297), new MidiNote(MidiNote.PianoNote.F, 5, 45738, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 46035, 297),     new MidiNote(MidiNote.PianoNote.EFlat, 5, 46035, 297),
				new MidiNote(MidiNote.PianoNote.F, 5, 46332, 297),     new MidiNote(MidiNote.PianoNote.D, 5, 46332, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 46629, 594), new MidiNote(MidiNote.PianoNote.C, 5, 46629, 594),
				new MidiNote(MidiNote.PianoNote.C, 5, 48114, 594),     new MidiNote(MidiNote.PianoNote.AFlat, 4, 48114, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 48708, 594),     new MidiNote(MidiNote.PianoNote.EFlat, 5, 48708, 594),

				new MidiNote(MidiNote.PianoNote.G, 5, 49302, 594),     new MidiNote(MidiNote.PianoNote.EFlat, 5, 49302, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 49896, 297),     new MidiNote(MidiNote.PianoNote.EFlat, 5, 49896, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 50193, 297),     new MidiNote(MidiNote.PianoNote.EFlat, 5, 50193, 297),
				new MidiNote(MidiNote.PianoNote.AFlat, 5, 50490, 297), new MidiNote(MidiNote.PianoNote.F, 5, 50490, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 50787, 297),     new MidiNote(MidiNote.PianoNote.EFlat, 5, 50787, 297),
				new MidiNote(MidiNote.PianoNote.F, 5, 51084, 297),     new MidiNote(MidiNote.PianoNote.D, 5, 51084, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 51381, 594), new MidiNote(MidiNote.PianoNote.C, 5, 51381, 594),
				new MidiNote(MidiNote.PianoNote.C, 5, 52866, 594),     new MidiNote(MidiNote.PianoNote.AFlat, 4, 52866, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 53460, 594),     new MidiNote(MidiNote.PianoNote.EFlat, 5, 53460, 594),

				new MidiNote(MidiNote.PianoNote.G, 5, 54054, 594),     new MidiNote(MidiNote.PianoNote.EFlat, 5, 54054, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 54648, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 54945, 297),
				new MidiNote(MidiNote.PianoNote.F, 5, 55242, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 55539, 297),
				new MidiNote(MidiNote.PianoNote.D, 5, 55836, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 56133, 297),
			};

			var minNoteValue = notes.Min(note => note.NoteValue);
			var maxNoteValue = notes.Max(note => note.NoteValue);
			var noteDist = (51d - NoteSize) / (maxNoteValue - minNoteValue);

			foreach (var note in notes)
			{
				var minVal = (noteDist * (note.NoteValue - minNoteValue)).Round() + 23;
				var maxVal = minVal + NoteSize - 1;
				foreach (var light in middleLights)
				{
					var pos = bodyLayout.GetLightPosition(light);
					var show = false;

					if ((pos.Y <= 20) && (pos.X >= minVal) && (pos.X <= maxVal))
						show = true;
					if ((pos.Y >= 76) && ((96 - pos.X) >= minVal) && ((96 - pos.X) <= maxVal))
						show = true;
					if ((pos.X <= 20) && ((96 - pos.Y) >= minVal) && ((96 - pos.Y) <= maxVal))
						show = true;
					if ((pos.X >= 76) && (pos.Y >= minVal) && (pos.Y <= maxVal))
						show = true;

					if (show)
					{
						segment.AddLight(light, note.StartTime, note.StartTime + 198, 0x100408, 0x040102);
						segment.AddLight(light, note.EndTime, 0x000000);
					}
				}
			}

			var endTimes = new List<int> { 58212, 58361, 58509, 58658 };
			for (int i = 0; i < endTimes.Count; i++)
				foreach (var light in bodyLayout.GetAllLights())
					segment.AddLight(light, endTimes[i], i % 2 == 0 ? 0x080204 : 0x040102);

			return segment;
		}

		Segment SineSquares()
		{
			const int NumSquares = 5;
			const double MinRadius = 8.999;
			const double MaxRadius = 48.001;
			const double MaxVariance = 30;
			const double EvenSize = (MaxRadius - MinRadius) / NumSquares;

			var segment = new Segment();
			var color = new LightColor(0, NumSquares - 1,
				new List<int> { 0x080204, 0x080204, 0x080204, 0x080204, 0x080204 },
				new List<int> { 0x080204, 0x000000, 0x080204, 0x000000, 0x080204 },
				new List<int> { 0x100010, 0x000000, 0x001010, 0x000000, 0x100010 },
				new List<int> { 0x100000, 0x000000, 0x001000, 0x000000, 0x000010 },
				new List<int> { 0x100000, 0x101000, 0x001000, 0x000010, 0x090010 });
			for (var angle = 0; angle < 360; ++angle)
			{
				segment.Clear(angle);
				var variance = Math.Pow(MaxVariance, Math.Sin(angle * Math.PI / 180));
				var multValue = Math.Pow(variance, 1d / (NumSquares - 1));
				var firstLen = (MaxRadius - MinRadius) / ((Math.Pow(multValue, NumSquares) - multValue) / (multValue - 1) + 1);
				foreach (var light in bodyLayout.GetAllLights())
				{
					var point = bodyLayout.GetLightPosition(light);
					var distance = Math.Max(Math.Abs(point.X - 48), Math.Abs(point.Y - 48));

					int square;
					if (double.IsNaN(firstLen))
						square = (int)Math.Floor((distance - MinRadius) / EvenSize);
					else
						square = (int)Math.Floor(Math.Log(((distance - MinRadius) / firstLen - 1) * (multValue - 1) / multValue + 1) / Math.Log(multValue) + 1);

					if ((square >= 0) && (square < NumSquares))
						segment.AddLight(light, angle, color, square);
				}
			}

			return segment;
		}

		int? GetSquare(Point point, bool horiz)
		{
			var x = point.X.Round();
			var y = point.Y.Round();
			if ((x < 0) || (y < 0) || (x > 96) || (y > 96))
				return null;
			if ((!horiz) && (x % 19 < 2) && (y % 19 < 2))
				return null;
			if ((x == 0) || (y == 0) || (x == 96) || (y == 96))
				return 0;
			return (y - 1) / 19 * 5 + (x - 1) / 19 + 1;
		}

		Segment Wavy()
		{
			const int TotalTime = 600;
			var segment = new Segment();
			var color = new LightColor(0, 1000, Helpers.Rainbow6);
			var lines = new List<Tuple<Point, Size, int, int, bool>>
			{
				Tuple.Create(new Point(0, 0), new Size(2, 97), -97, 12, false),
				Tuple.Create(new Point(19, 0), new Size(2, 97), 97, 16, false),
				Tuple.Create(new Point(38, 0), new Size(2, 97), 97, 14, false),
				Tuple.Create(new Point(57, 0), new Size(2, 97), -97, 17, false),
				Tuple.Create(new Point(76, 0), new Size(2, 97), -97, 20, false),
				Tuple.Create(new Point(95, 0), new Size(2, 97), 97, 13, false),
				Tuple.Create(new Point(0, 0), new Size(97, 2), -97, 15, true),
				Tuple.Create(new Point(0, 19), new Size(97, 2), -97, 12, true),
				Tuple.Create(new Point(0, 38), new Size(97, 2), 97, 10, true),
				Tuple.Create(new Point(0, 57), new Size(97, 2), -97, 15, true),
				Tuple.Create(new Point(0, 76), new Size(97, 2), 97, 17, true),
				Tuple.Create(new Point(0, 95), new Size(97, 2), 97, 13, true),
			};
			var colors = new List<int> { 0x101010, 0x100000, 0x100800, 0x101000, 0x001000, 0x000010, 0x050008, 0x09000d };
			var squareColor = "0/1&2&6/3&7&11/4&8&12&16/5&9&13&17&21/10&14&18&22/15&19&23/20&24&25".Split('/').SelectMany((l, index) => l.Split('&').Select(s => new { square = int.Parse(s), color = colors[index] })).ToDictionary(obj => obj.square, obj => obj.color);
			for (var time = 0; time < TotalTime; ++time)
			{
				segment.Clear(time);
				foreach (var line in lines)
				{
					var amplitude = line.Item3 * Math.Pow(Math.E, -Math.Pow(time * 2.4 / TotalTime, 2d)) * Math.Cos(line.Item4 * 2d * Math.PI * time / TotalTime);
					foreach (var light in bodyLayout.GetPositionLights(line.Item1, line.Item2))
					{
						var point = bodyLayout.GetLightPosition(light);
						if (line.Item5)
							point.X += amplitude;
						else
							point.Y += amplitude;
						var square = GetSquare(point, line.Item5);
						if (!square.HasValue)
							continue;
						segment.AddLight(light, time, squareColor[square.Value]);
					}
				}
			}
			return segment;
		}

		Segment MoveBoxes(out int time)
		{
			const int BoxDelay = 10;
			var moves = new List<Tuple<int, int>> { Tuple.Create(8, 0), Tuple.Create(8, 2), Tuple.Create(8, 16), Tuple.Create(8, 14), Tuple.Create(10, 2), Tuple.Create(10, 4), Tuple.Create(10, 18), Tuple.Create(10, 16), Tuple.Create(12, 4), Tuple.Create(12, 6), Tuple.Create(12, 20), Tuple.Create(12, 18), Tuple.Create(22, 14), Tuple.Create(22, 16), Tuple.Create(22, 30), Tuple.Create(22, 28), Tuple.Create(24, 16), Tuple.Create(24, 18), Tuple.Create(24, 32), Tuple.Create(24, 30), Tuple.Create(26, 18), Tuple.Create(26, 20), Tuple.Create(26, 34), Tuple.Create(26, 32), Tuple.Create(36, 28), Tuple.Create(36, 30), Tuple.Create(36, 44), Tuple.Create(36, 42), Tuple.Create(38, 30), Tuple.Create(38, 32), Tuple.Create(38, 46), Tuple.Create(38, 44), Tuple.Create(40, 32), Tuple.Create(40, 34), Tuple.Create(40, 48), Tuple.Create(40, 46) };
			var segment = new Segment();
			var distance = bodyLayout.GetAllLights().ToDictionary(light => light, light => (((bodyLayout.GetLightPosition(light) - Helpers.Center).Length - 9) * 16.9830463869911).Round());
			var color = new LightColor(0, 1000, new List<int> { 0x000010, 0x001000, 0x000010, 0x001000 }, new List<int> { 0x100010, 0x001010, 0x100010, 0x001010 });
			time = 0;
			for (var ctr = 0; ctr <= 38; ++ctr)
			{
				segment.Clear(time);
				foreach (var move in moves)
				{
					var fromSquare = move.Item1;
					var toSquare = move.Item2;

					var offset = 0;
					if (ctr >= 19)
					{
						Helpers.Swap(ref fromSquare, ref toSquare);
						offset = 19;
					}

					var fromPoint = new Point(fromSquare % 7 * 19 - 18, fromSquare / 7 * 19 - 18);
					var toPoint = new Point(toSquare % 7 * 19 - 18, toSquare / 7 * 19 - 18);
					var point = fromPoint + (toPoint - fromPoint) / 19 * (ctr - offset);
					foreach (var light in bodyLayout.GetPositionLights(point, 19, 19).Except(bodyLayout.GetPositionLights(point.X + 1, point.Y + 1, 17, 17)))
						segment.AddLight(light, time, color, distance[light]);
				}
				if (ctr % 19 != 0)
					++time;
				else if (ctr != 19)
					time += BoxDelay / 2;
				else
					time += BoxDelay;
			}
			return segment;
		}

		Segment RotateBoxes(out int time)
		{
			const int Delay = 10;
			var squares = Enumerable.Range(1, 7).SelectMany(y => Enumerable.Range(1, 7).Select(x => y * 9 + x)).ToList();
			var paths = new List<List<Tuple<int, int, int>>>();
			paths.Add(squares.Select(square => Tuple.Create(square, square + 1, square % 2)).ToList());
			paths.Add(squares.Select(square => Tuple.Create(square, square + 9, (square + 1) % 2)).ToList());
			paths.Add(squares.Select(square => Tuple.Create(square, square - 1, square % 2)).ToList());
			paths.Add(squares.Select(square => Tuple.Create(square, square - 9, (square + 1) % 2)).ToList());

			time = 0;
			var color = new LightColor(0, 1,
				new List<int> { 0x101010, 0x100000 },
				new List<int> { 0x100408, 0x091009 });
			var segment = new Segment();
			foreach (var path in paths)
				for (var ctr = 0; ctr <= 19; ++ctr)
				{
					var percent = ctr / 19d;
					foreach (var part in path)
					{
						var fromPoint = new Point(part.Item1 % 9 * 19 - 37, part.Item1 / 9 * 19 - 37);
						var toPoint = new Point(part.Item2 % 9 * 19 - 37, part.Item2 / 9 * 19 - 37);
						var point = fromPoint + (toPoint - fromPoint) * percent;
						foreach (var light in bodyLayout.GetPositionLights(point, 19, 19))
							segment.AddLight(light, time, color, part.Item3);
					}
					if (ctr % 19 == 0)
						time += Delay;
					else
						++time;
				}

			foreach (var light in bodyLayout.GetPositionLights(0, 0, 97, 97).Except(bodyLayout.GetPositionLights(1, 1, 95, 95)))
				segment.AddLight(light, 0, 0x000000);

			return segment;
		}

		enum SlideSquaresDirection { Up, Right, Down, Left, None }
		Segment SlideSquares(out int outTime)
		{
			const int Delay = 10;

			var dist = new Dictionary<int, List<Tuple<Vector, int>>>();
			for (var square = 0; square < 25; ++square)
			{
				dist[square] = new List<Tuple<Vector, int>>();
				var squarePoint = new Point(square % 5 * 19 + 1, square / 5 * 19 + 1);
				for (var y = 0; y < 19; ++y)
					for (var x = 0; x < 19; ++x)
					{
						var diff = new Vector(x, y);
						dist[square].Add(Tuple.Create(diff, (((squarePoint + diff - Helpers.Center).Length - 9) * 16.9830463869911).Round()));
					}
			}
			var color = new LightColor(0, 1000, Helpers.Rainbow6);
			var positions = new List<Point> { new Point(1, 20), new Point(20, 1), new Point(39, 1), new Point(77, 1), new Point(77, 20), new Point(1, 39), new Point(1, 1), new Point(39, 20), new Point(58, 1), new Point(77, 39), new Point(1, 58), new Point(20, 20), new Point(39, 58), new Point(58, 20), new Point(77, 58), new Point(1, 77), new Point(20, 39), new Point(39, 77), new Point(58, 39), new Point(77, 77), new Point(20, 77), new Point(20, 58), new Point(58, 77), new Point(58, 58) };
			var moves = new List<SlideSquaresDirection> { SlideSquaresDirection.Down, SlideSquaresDirection.Down, SlideSquaresDirection.Right, SlideSquaresDirection.Right, SlideSquaresDirection.Up, SlideSquaresDirection.Up, SlideSquaresDirection.Up, SlideSquaresDirection.Up, SlideSquaresDirection.Left, SlideSquaresDirection.Down, SlideSquaresDirection.Down, SlideSquaresDirection.Down, SlideSquaresDirection.Down, SlideSquaresDirection.Left, SlideSquaresDirection.Up, SlideSquaresDirection.Up, SlideSquaresDirection.Up, SlideSquaresDirection.Up, SlideSquaresDirection.Left, SlideSquaresDirection.Down, SlideSquaresDirection.Down, SlideSquaresDirection.Down, SlideSquaresDirection.Down, SlideSquaresDirection.Left, SlideSquaresDirection.Up, SlideSquaresDirection.Up, SlideSquaresDirection.Up, SlideSquaresDirection.Up, SlideSquaresDirection.None, SlideSquaresDirection.None, SlideSquaresDirection.None, SlideSquaresDirection.None };

			var time = 0;
			var segment = new Segment();

			Action drawBoard = () =>
			{
				segment.Clear(time);

				for (var square = 0; square < positions.Count; ++square)
					foreach (var tuple in dist[square])
						foreach (var light in bodyLayout.GetPositionLights(positions[square] + tuple.Item1, 1, 1))
							segment.AddLight(light, time, color, tuple.Item2);
			};

			var empty = new Point(39, 39);
			foreach (var move in moves)
			{
				drawBoard();
				time += Delay;

				var direction = new Vector(0, 0);
				switch (move)
				{
					case SlideSquaresDirection.Up: direction = new Vector(0, -1); break;
					case SlideSquaresDirection.Down: direction = new Vector(0, 1); break;
					case SlideSquaresDirection.Left: direction = new Vector(-1, 0); break;
					case SlideSquaresDirection.Right: direction = new Vector(1, 0); break;
				}

				empty -= direction * 19;
				var square = positions.Select((point, index) => new { point, index }).OrderBy(obj => (obj.point - empty).LengthSquared).Select(obj => obj.index).First();
				for (var ctr = 0; ctr < 19; ++ctr)
				{
					positions[square] += direction;
					drawBoard();
					++time;
				}
			}
			outTime = time;
			return segment;
		}

		Segment Window()
		{
			const double FakeSize = 25;
			const int Step = 5;

			var xDiff = new Vector(19, 0);
			var yDiff = new Vector(0, 19);
			var segment = new Segment();
			var colors = new List<IReadOnlyList<int>> { new List<int> { 0x101010, 0x010101 }, new List<int> { 0x100000, 0x000010 }, new List<int> { 0x100010, 0x001010 }, Helpers.Rainbow6 };
			var color = new LightColor(0, 720, colors.Select(list => list.Concat(list).Concat(list.Take(1)).ToList()).ToList());
			var distances = bodyLayout.GetAllLights().ToDictionary(light => light, light => 360 - (((bodyLayout.GetLightPosition(light) - Helpers.Center).Length - 9) * 6.11389669931678).Round());
			for (var time = 0; time < 360; time += Step)
			{
				segment.Clear(time);

				var horizSize = new Size(Math.Max(0, (FakeSize / 2 * Math.Cos(time * Math.PI / 180) + 21d / 2d).Round()), 2);
				var horizPoint = new Point((21 - horizSize.Width) / 2, 0);
				var vertSize = new Size(2, Math.Max(0, (FakeSize / 2 * Math.Cos((time + 180) * Math.PI / 180) + 21d / 2d).Round()));
				var vertPoint = new Point(0, (21 - vertSize.Height) / 2);

				var data = new List<Tuple<Point, Size>>
				{
					Tuple.Create(horizPoint, horizSize),
					Tuple.Create(vertPoint, vertSize),
				};

				for (var y = 0; y < 6; ++y)
					for (var x = 0; x < 6; ++x)
						foreach (var datum in data)
							foreach (var light in bodyLayout.GetPositionLights(datum.Item1 + xDiff * x + yDiff * y, datum.Item2))
								segment.AddLight(light, time, time + Step, color, time + distances[light], color, time + distances[light] + Step, true);
			}
			return segment;
		}

		Segment SineWaves()
		{
			var color = new LightColor(0, 5, new List<int> { 0x100000, 0x001000, 0x000010, 0x101000, 0x100010, 0x001010 });
			var offsets = new List<double> { -90, -36.86989765, -11.53695903, 11.53695903, 36.86989765, 90 };
			var segment = new Segment();
			for (var angle = 0; angle < 360; angle += 2)
			{
				segment.Clear(angle);
				for (var y = 0; y < 6; ++y)
				{
					var point = new Point(47.5 * Math.Sin((angle / 180 * 360 + angle % 180 + offsets[y]) * Math.PI / 180), -47.5 + y * 19);

					foreach (var rotation in Rotations)
					{
						var newPoint = rotation(point);
						foreach (var light in bodyLayout.GetPositionLights(newPoint.X + 47.5, newPoint.Y + 47.5, 2, 2))
							segment.AddLight(light, angle, color, y);
					}
				}
			}

			return segment;
		}

		Segment Circles()
		{
			const double TravelRadius = 10;
			const double CircleRadius1 = 97d / 2 - TravelRadius;
			const double CircleRadius2 = 30;
			const double Factor2 = 1.5;
			const double CircleRadius3 = CircleRadius2 - (CircleRadius1 - CircleRadius2);
			const double Factor3 = 3;

			var segment = new Segment();
			var centerAdjust = new Vector(48, 48);
			for (var angle = 0; angle < 720; angle += 2)
			{
				var useAngle = angle * Math.PI / 180;
				var center1 = new Point(TravelRadius * Math.Cos(useAngle), TravelRadius * Math.Sin(useAngle));
				var center2 = center1 + new Vector((CircleRadius1 - CircleRadius2) * Math.Cos(useAngle * Factor2), (CircleRadius1 - CircleRadius2) * Math.Sin(useAngle * Factor2));
				var center3 = center2 + new Vector((CircleRadius2 - CircleRadius3) * Math.Cos(useAngle * Factor3), (CircleRadius2 - CircleRadius3) * Math.Sin(useAngle * Factor3));

				segment.Clear(angle);
				foreach (var light in bodyLayout.GetAllLights())
				{
					var lightLocation = bodyLayout.GetLightPosition(light);
					if ((lightLocation - center3 - centerAdjust).LengthSquared <= CircleRadius3 * CircleRadius3)
						segment.AddLight(light, angle, 0x000010);
					else if ((lightLocation - center2 - centerAdjust).LengthSquared <= CircleRadius2 * CircleRadius2)
						segment.AddLight(light, angle, 0x001000);
					else if ((lightLocation - center1 - centerAdjust).LengthSquared <= CircleRadius1 * CircleRadius1)
						segment.AddLight(light, angle, 0x100000);
				}
			}
			return segment;
		}

		Segment RingPulse()
		{
			var segment = new Segment();
			var color1 = new LightColor(0, 2000, new List<int> { 0x000000, 0x100408, 0x000000, 0x100408, 0x000000, 0x100408, 0x000000, 0x100408, 0x000000 });
			var color2 = new LightColor(0, 2000, new List<int> { 0x000000, 0x001010, 0x000000, 0x100010, 0x000000, 0x001010, 0x000000, 0x100010, 0x000000 });
			var middleLights = bodyLayout.GetPositionLights(19, 19, 59, 59).Except(bodyLayout.GetPositionLights(21, 21, 55, 55)).ToList();
			foreach (var light in bodyLayout.GetAllLights())
			{
				var distance = middleLights.Select(middleLight => (bodyLayout.GetLightPosition(light) - bodyLayout.GetLightPosition(middleLight)).LengthSquared).OrderBy(len => len).Select(len => (Math.Sqrt(len) * 37.216146378239344).Round()).First();

				// The 250 added to all the numbers is for the snaps, which are on the 2nd and 4th beats
				segment.AddLight(light, 0, color1, 250);
				segment.AddLight(light, 250 + distance, 1250, color1, 0, color1, 1000 - distance, true);
				segment.AddLight(light, 1250, 2250, color1, 1000 - distance, color1, 2000 - distance, true);
				segment.AddLight(light, 2250, 2250 + distance, color1, 2000 - distance, color1, 2000, true);
				segment.AddLight(light, 2250 + distance, 3250, color2, 0, color2, 1000 - distance, true);
				segment.AddLight(light, 3250, 4250, color2, 1000 - distance, color2, 2000 - distance, true);
			}
			return segment;
		}

		Segment SquarePath(out int time)
		{
			const int Length = 18;
			const string data =
"0,0/1,0/2,0/3,0/4,0/5,0/6,0/7,0/8,0/9,0/10,0/11,0/12,0/13,0/14,0/15,0/16,0/17,0/18,0/19,0/19,1/19,2/19,3/19,4/19,5/19,6/19,7/19,8/19,9/19,10/19,11/19,12/19,13/19,14/19,15/19,16/19,17/19,18/19,19/20,19/21,19/22,19/23,19/24,19/25,19/26,19/27,19/28,19/29,19/30,19/31,19/32,19/33,19/34,19/35,19/36,19/37,19/38,19/38,20/38,21/38,22/38,23/38,24/38,25/38,26/38,27/38,28/38,29/38,30/38,31/38,32/38,33/38,34/38,35/38,36/38,37/38,38/39,38/40,38/41,38/42,38/43,38/44,38/45,38/46,38/47,38/48,38/49,38/50,38/51,38" +
"/52,38/53,38/54,38/55,38/56,38/57,38/57,39/57,40/57,41/57,42/57,43/57,44/57,45/57,46/57,47/57,48/57,49/57,50/57,51/57,52/57,53/57,54/57,55/57,56/57,57/58,57/59,57/60,57/61,57/62,57/63,57/64,57/65,57/66,57/67,57/68,57/69,57/70,57/71,57/72,57/73,57/74,57/75,57/76,57/76,58/76,59/76,60/76,61/76,62/76,63/76,64/76,65/76,66/76,67/76,68/76,69/76,70/76,71/76,72/76,73/76,74/76,75/76,76/77,76/78,76/79,76/80,76/81,76/82,76/83,76/84,76/85,76/86,76/87,76/88,76/89,76/90,76/91,76/92,76/93,76/94,76/95,76/95,77/9" +
"5,78/95,79/95,80/95,81/95,82/95,83/95,84/95,85/95,86/95,87/95,88/95,89/95,90/95,91/95,92/95,93/95,94/95,95";
			var path = data.Split('/').Select(Point.Parse).ToList();

			var diff = new Vector(47.5, 47.5);
			var segment = new Segment();
			var color = new LightColor(0, path.Count - Length, new List<int> { 0x100000, 0x100800, 0x101000, 0x001000, 0x000010, 0x090010, 0x100000 }, new List<int> { 0x401020 });
			time = 0;
			while (true)
			{
				segment.Clear(time);
				var end = time + Length;
				if (end > path.Count)
					break;
				for (var ctr = time; ctr < end; ++ctr)
					foreach (var point in new List<Point> { path[ctr], new Point(path[ctr].Y, path[ctr].X) })
						foreach (var rotation in Rotations)
							foreach (var light in bodyLayout.GetPositionLights(rotation(point - diff) + diff, 2, 2))
								segment.AddLight(light, time, time + 1, color, time, color, time + 1, true);
				++time;
			}

			return segment;
		}

		public Song Render()
		{
			var song = new Song("pynk.mp3"); // First sound is at 750; Measures start at 2532, repeat every 2376, and stop at 240132. Beats appear quantized to 2376/24 = 99

			// Intro (750)
			var intro = Intro();
			song.AddSegment(intro, 0, 58806, 750);

			// SineSquares (59556)
			var sineSquares = SineSquares();
			song.AddSegment(sineSquares, 0, 360, 59556, 4752, 4);
			song.AddPaletteChange(59556, 0);
			song.AddPaletteChange(59556, 60744, 1);
			song.AddPaletteChange(63808, 64808, 2);
			song.AddPaletteChange(68560, 69560, 3);
			song.AddPaletteChange(73312, 74312, 4);
			song.AddPaletteChange(78564, 0);

			// Wavy (78564)
			var wavy = Wavy();
			song.AddSegment(wavy, 0, 600, 78564, 17820);
			song.AddSegment(wavy, 600, 600, song.MaxTime(), 1188);

			// MoveBoxes (97572)
			var moveBoxes = MoveBoxes(out var moveBoxesTime);
			song.AddSegment(moveBoxes, 0, moveBoxesTime, 97572, 2376, 4);
			song.AddPaletteChange(97572, 0);
			song.AddPaletteChange(101824, 102824, 1);
			song.AddPaletteChange(107076, 0);

			// RotateBoxes (107076)
			var rotateBoxes = RotateBoxes(out var rotateBoxesTime);
			song.AddSegment(rotateBoxes, 0, rotateBoxesTime, 107076, 4752, 4);
			song.AddPaletteChange(107076, 0);
			song.AddPaletteChange(116080, 117080, 1);
			song.AddPaletteChange(126084, 0);

			// Window (126084)
			var window = Window();
			song.AddSegment(window, 0, 360, 126084, 2376, 8);
			song.AddPaletteChange(126084, 0);
			song.AddPaletteChange(130336, 131336, 1);
			song.AddPaletteChange(135088, 136088, 2);
			song.AddPaletteChange(139840, 140840, 3);
			song.AddPaletteChange(145092, 0);

			// SlideSquares (145092)
			var slideSquares = SlideSquares(out var slideSquaresTime);
			song.AddSegment(slideSquares, 0, slideSquaresTime, 145092, 19008);

			// SineWaves (164100)
			var sineWaves = SineWaves();
			song.AddSegment(sineWaves, 0, 360, 164100, 4752, 4);

			// Circles (183108)
			var circles = Circles();
			song.AddSegment(circles, 0, 720, 183108, 4752, 4);

			// RingPulse (202116)
			var ringPulse = RingPulse();
			song.AddSegment(ringPulse, 0, 1000, 202116, 2376);
			song.AddSegment(ringPulse, 1000, 2000, song.MaxTime(), 2376, 4);
			song.AddSegment(ringPulse, 2000, 3000, song.MaxTime(), 2376);
			song.AddSegment(ringPulse, 3000, 4000, song.MaxTime(), 2376, 3);

			// SquarePath (223500)
			var squarePath = SquarePath(out var squarePathTime);
			song.AddSegment(squarePath, 0, squarePathTime, 223500, 4752, 3);
			song.AddSegment(squarePath, 0, squarePathTime / 2, song.MaxTime(), 2376);
			song.AddSegment(squarePath, squarePathTime / 2, squarePathTime / 2, song.MaxTime(), 2376);
			song.AddPaletteChange(223500, 0);
			song.AddPaletteChange(240132, 241320, 1);
			song.AddPaletteChange(242508, 0);

			// End (242508)

			return song;
		}
	}
}
