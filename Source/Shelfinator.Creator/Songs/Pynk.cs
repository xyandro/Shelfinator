using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Pynk : SongCreator
	{
		public override int SongNumber => 7;

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
			const int BeatFadeTime = 198;
			const int NoteSize = 9;
			const int NoteFadeTime = 198;
			var segment = new Segment();

			var innerLights = bodyLayout.GetPositionLights(38, 38, 21, 21).Except(bodyLayout.GetPositionLights(40, 40, 17, 17)).ToList();
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

			var beatStartColor = new LightColor(0, BeatSize - 1, new List<int> { 0x001010, 0x100010, 0x100010, 0x001010 });
			var beatEndColor = new LightColor(0, BeatSize - 1, new List<int> { 0x040004, 0x000404, 0x000404, 0x040004 });
			var minBeatValue = beats.Min(beat => beat.NoteValue);
			var maxBeatValue = beats.Max(beat => beat.NoteValue);
			var beatDist = (89d - BeatSize) / (maxBeatValue - minBeatValue);
			foreach (var beat in beats)
			{
				var pos = (beatDist * (beat.NoteValue - minBeatValue)).Round() + 4;
				for (var ctr = 0; ctr < BeatSize; ++ctr)
				{
					var lights = new List<int>();
					lights.AddRange(bodyLayout.GetPositionLights(ctr + pos, 0, 1, 2));
					lights.AddRange(bodyLayout.GetPositionLights(96 - ctr - pos, 95, 1, 2));
					lights.AddRange(bodyLayout.GetPositionLights(0, 96 - ctr - pos, 2, 1));
					lights.AddRange(bodyLayout.GetPositionLights(95, ctr + pos, 2, 1));
					foreach (var light in lights)
					{
						segment.AddLight(light, beat.StartTime, beat.StartTime + BeatFadeTime, beatStartColor, ctr, beatEndColor, ctr);
						segment.AddLight(light, beat.EndTime, 0x000000);
					}
				}
			}

			var startBrightLight = new LightColor(0, 1000, new List<int> { 0x0c0204, 0x060102 });
			var endBrightLight = new LightColor(0, 1000, new List<int> { 0x601020, 0x300810 });
			var middleLights = bodyLayout.GetPositionLights(19, 19, 59, 59).Except(bodyLayout.GetPositionLights(21, 21, 55, 55)).ToList();
			foreach (var light in middleLights)
			{
				var dist = (((bodyLayout.GetLightPosition(light) - Helpers.Center).Length - 28) / 13.0121933088198 * 1000).Round();
				segment.AddLight(light, 16038, 20790, startBrightLight, dist, endBrightLight, dist);
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

			var noteStartColor = new LightColor(0, NoteSize - 1, new List<int> { 0x100010, 0x180408, 0x180408, 0x100010 });
			var noteEndColor = new LightColor(0, NoteSize - 1, new List<int> { 0x040004, 0x0c0204, 0x0c0204, 0x040004 });
			var minNoteValue = notes.Min(note => note.NoteValue);
			var maxNoteValue = notes.Max(note => note.NoteValue);
			var noteDist = (51d - NoteSize) / (maxNoteValue - minNoteValue);
			foreach (var note in notes)
			{
				var pos = (noteDist * (note.NoteValue - minNoteValue)).Round() + 23;
				for (var ctr = 0; ctr < NoteSize; ++ctr)
				{
					var lights = new List<int>();
					lights.AddRange(bodyLayout.GetPositionLights(ctr + pos, 19, 1, 2));
					lights.AddRange(bodyLayout.GetPositionLights(96 - ctr - pos, 76, 1, 2));
					lights.AddRange(bodyLayout.GetPositionLights(19, 96 - ctr - pos, 2, 1));
					lights.AddRange(bodyLayout.GetPositionLights(76, ctr + pos, 2, 1));
					foreach (var light in lights)
					{
						segment.AddLight(light, note.StartTime, note.StartTime + NoteFadeTime, noteStartColor, ctr, noteEndColor, ctr);
						segment.AddLight(light, note.EndTime, 0x000000);
					}
				}
			}

			var endTimes = new List<int> { 58212, 58361, 58509, 58658 };
			for (int i = 0; i < endTimes.Count; i++)
				foreach (var light in middleLights)
					segment.AddLight(light, endTimes[i], i % 2 == 0 ? 0x0c0204 : 0x060102);

			return segment;
		}

		Segment ExplodeSquares()
		{
			const int Thickness = 5;
			const int StartRadius = 9;
			const int EndRadius = 136;
			const int LaunchTime = 60;
			var segment = new Segment();
			var addPoints = new List<List<Point>>
			{
				new List<Point> { new Point(48, 29) },
				new List<Point> { new Point(48, 67) },
				new List<Point> { new Point(29, 48) },
				new List<Point> { new Point(67, 48) },
				new List<Point> { new Point(29, 29) },
				new List<Point> { new Point(67, 67) },
				new List<Point> { new Point(67, 29) },
				new List<Point> { new Point(29, 67) },
				new List<Point> { new Point(48, 29), new Point(48, 67) },
				new List<Point> { new Point(29, 48), new Point(67, 48) },
				new List<Point> { new Point(29, 29), new Point(67, 67) },
				new List<Point> { new Point(67, 29), new Point(29, 67) },
				new List<Point> { new Point(48, 10), new Point(48, 86) },
				new List<Point> { new Point(10, 48), new Point(86, 48) },
				new List<Point> { new Point(10, 10), new Point(86, 10), new Point(10, 86), new Point(86, 86) },
			};
			var color = new LightColor(StartRadius - Thickness, EndRadius, new List<int> { 0x0c0204, 0x0c0204, 0x100000, 0x101000, 0x100010, 0x001000, 0x001010, 0x000010 });
			var addPointCtr = -1;
			var time = -1;
			var points = new List<Tuple<Point, int>>();
			while (time < 960)
			{
				++time;
				segment.Clear(time);

				// Move points, remove out of bounds
				points = points.Select(t => Tuple.Create(t.Item1, t.Item2 + 1)).Where(t => t.Item2 < EndRadius).ToList();

				if (time % LaunchTime == 0)
				{
					++addPointCtr;
					if (addPointCtr < addPoints.Count)
						points.AddRange(addPoints[addPointCtr].Select(p => Tuple.Create(p, StartRadius - Thickness)));
				}

				foreach (var light in bodyLayout.GetAllLights())
				{
					var minDist = default(double?);
					var pos = bodyLayout.GetLightPosition(light);
					foreach (var point in points)
					{
						var dist = (pos - point.Item1).Length;
						if ((dist >= point.Item2) && (dist <= point.Item2 + Thickness))
							minDist = Math.Min(minDist ?? dist, dist);
					}
					if (minDist.HasValue)
						segment.AddLight(light, time, color, minDist.Value.Round());
				}
			}
			return segment;
		}

		Segment RotateSections()
		{
			const int Sections = 3;

			var segment = new Segment();
			var color = new LightColor(0, 96, Helpers.Rainbow7);

			var center = new Point(48, 48);
			for (var angle = 0; angle < 360; ++angle)
			{
				var sin1 = Math.Sin(angle * Math.PI / 180);
				var cos1 = Math.Cos(angle * Math.PI / 180);
				var sin2 = Math.Sin(-angle * Math.PI / 180);
				var cos2 = Math.Cos(-angle * Math.PI / 180);
				segment.Clear(angle);
				foreach (var light in bodyLayout.GetAllLights())
				{
					var pos = bodyLayout.GetLightPosition(light) - center;
					var len = Math.Floor((pos.Length - 9) / 58.8822509939086 * Sections).Round();
					if (len % 2 == 0)
						segment.AddLight(light, angle, color, (pos.X * cos1 - pos.Y * sin1 + 48).Round());
					else
						segment.AddLight(light, angle, color, (pos.X * cos2 - pos.Y * sin2 + 48).Round());
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
			var color = new LightColor(0, 1000, new List<int> { 0x000010, 0x180408, 0x000010, 0x180408 }, new List<int> { 0x100010, 0x001010, 0x100010, 0x001010 });
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
				new List<int> { 0x091009, 0x180408 });
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

		enum SlideSquaresDirection { Up, Right, Down, Left }
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
			var positions = new List<Point> { new Point(39, 1), new Point(58, 39), new Point(77, 1), new Point(77, 20), new Point(58, 58), new Point(39, 20), new Point(20, 1), new Point(20, 20), new Point(58, 1), new Point(77, 58), new Point(1, 1), new Point(58, 20), new Point(58, 77), new Point(77, 39), new Point(39, 77), new Point(20, 39), new Point(1, 20), new Point(1, 77), new Point(39, 58), new Point(77, 77), new Point(1, 39), new Point(1, 58), new Point(20, 58), new Point(20, 77) };
			var moves = new List<Tuple<SlideSquaresDirection, int>> { Tuple.Create(SlideSquaresDirection.Up, 1), Tuple.Create(SlideSquaresDirection.Right, 1), Tuple.Create(SlideSquaresDirection.Down, 2), Tuple.Create(SlideSquaresDirection.Left, 2), Tuple.Create(SlideSquaresDirection.Up, 3), Tuple.Create(SlideSquaresDirection.Right, 3), Tuple.Create(SlideSquaresDirection.Down, 4), Tuple.Create(SlideSquaresDirection.Left, 4), Tuple.Create(SlideSquaresDirection.Up, 4), Tuple.Create(SlideSquaresDirection.Right, 4), Tuple.Create(SlideSquaresDirection.Down, 1), Tuple.Create(SlideSquaresDirection.Left, 4), Tuple.Create(SlideSquaresDirection.Down, 1), Tuple.Create(SlideSquaresDirection.Right, 4), Tuple.Create(SlideSquaresDirection.Down, 1), Tuple.Create(SlideSquaresDirection.Left, 4), Tuple.Create(SlideSquaresDirection.Down, 1), Tuple.Create(SlideSquaresDirection.Right, 4), Tuple.Create(SlideSquaresDirection.Up, 4), Tuple.Create(SlideSquaresDirection.Left, 1), Tuple.Create(SlideSquaresDirection.Down, 4), Tuple.Create(SlideSquaresDirection.Left, 1), Tuple.Create(SlideSquaresDirection.Up, 4), Tuple.Create(SlideSquaresDirection.Left, 1), Tuple.Create(SlideSquaresDirection.Down, 4), Tuple.Create(SlideSquaresDirection.Left, 1), Tuple.Create(SlideSquaresDirection.Up, 4), Tuple.Create(SlideSquaresDirection.Right, 4), Tuple.Create(SlideSquaresDirection.Down, 4), Tuple.Create(SlideSquaresDirection.Left, 4), Tuple.Create(SlideSquaresDirection.Up, 4), Tuple.Create(SlideSquaresDirection.Left, 0) };

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
				switch (move.Item1)
				{
					case SlideSquaresDirection.Up: direction = new Vector(0, -1); break;
					case SlideSquaresDirection.Down: direction = new Vector(0, 1); break;
					case SlideSquaresDirection.Left: direction = new Vector(-1, 0); break;
					case SlideSquaresDirection.Right: direction = new Vector(1, 0); break;
				}

				var squares = new List<int>();
				for (var ctr = 0; ctr < move.Item2; ++ctr)
				{
					empty -= direction * 19;
					var square = positions.Select((point, index) => new { point, index }).OrderBy(obj => (obj.point - empty).LengthSquared).Select(obj => obj.index).First();
					squares.Add(square);
				}
				for (var ctr = 0; ctr < 19; ++ctr)
				{
					foreach (var square in squares)
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
			var colors = new List<IReadOnlyList<int>> { new List<int> { 0x180408, 0x060102 }, new List<int> { 0x100000, 0x000010 }, new List<int> { 0x100010, 0x001010 }, Helpers.Rainbow6 };
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
			const double Factor3 = 2;

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
						segment.AddLight(light, angle, 0x180408);
					else if ((lightLocation - center2 - centerAdjust).LengthSquared <= CircleRadius2 * CircleRadius2)
						segment.AddLight(light, angle, 0x001010);
					else if ((lightLocation - center1 - centerAdjust).LengthSquared <= CircleRadius1 * CircleRadius1)
						segment.AddLight(light, angle, 0x100010);
				}
			}
			return segment;
		}

		Segment RingPulse()
		{
			var segment = new Segment();
			var useColors = new List<List<int>>
			{
				new List<int> { 0x180408 },
				new List<int> { 0x180408 },
				new List<int> { 0x000000, 0x180408, 0x000000, 0x180408, 0x000000 },
				new List<int> { 0x000000, 0x180408, 0x000000, 0x180408, 0x000000 },
				new List<int> { 0x000000, 0x180408, 0x000000, 0x180408, 0x000000 },
				new List<int> { 0x000000, 0x001010, 0x000000, 0x100010, 0x000000 },
				new List<int> { 0x000000, 0x001010, 0x000000, 0x100010, 0x000000 },
				new List<int> { 0x000000, 0x001010, 0x000000, 0x100010, 0x000000 },
			};
			var colors = Enumerable.Range(0, 3).Select(index => new LightColor(0, 1000, useColors.Skip(index).Take(useColors.Count - 2))).ToList();
			var middleLights = bodyLayout.GetPositionLights(19, 19, 59, 59).Except(bodyLayout.GetPositionLights(21, 21, 55, 55)).ToList();
			foreach (var light in bodyLayout.GetAllLights())
			{
				var distance = middleLights.Select(middleLight => (bodyLayout.GetLightPosition(light) - bodyLayout.GetLightPosition(middleLight)).LengthSquared).OrderBy(len => len).Select(len => (Math.Sqrt(len) * 37.216146378239344).Round()).First();
				for (var color = 0; color < colors.Count; ++color)
				{
					var startTime = Math.Max(0, Math.Min(distance - 1750 + color * 1000, 1000));
					var endTime = Math.Max(0, Math.Min(distance - 750 + color * 1000, 1000));
					var startColorValue = startTime - distance + 1750 - color * 1000;
					var endColorValue = endTime - distance + 1750 - color * 1000;
					segment.AddLight(light, startTime, endTime, colors[color], startColorValue, colors[color], endColorValue, true);
				}
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
			var color = new LightColor(0, Length, new List<int> { 0x100000, 0x180408, 0x100000 }, new List<int> { 0x100800, 0x180408, 0x100800 }, new List<int> { 0x101000, 0x180408, 0x101000 }, new List<int> { 0x001000, 0x180408, 0x001000 }, new List<int> { 0x000010, 0x180408, 0x000010 }, new List<int> { 0x090010, 0x180408, 0x090010 }, new List<int> { 0x180408 }, new List<int> { 0x601020 });
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
								segment.AddLight(light, time, color, ctr - time);
				++time;
			}

			return segment;
		}

		public override Song Render()
		{
			var song = new Song("pynk.ogg");

			song.AddMeasure(1000); // Empty lead-in time
			song.AddMeasure(1782); // Pickup
			song.AddMeasure(2376, 101); // Measures
			// Beats appear quantized to 1/24

			// Intro (1)
			var intro = Intro();
			song.AddSegmentByMeasure(intro, 0, 58806, 1, 25);

			// ExplodeSquares (26)
			var explodeSquares = ExplodeSquares();
			song.AddSegmentByMeasure(explodeSquares, 0, 960, 26, 8);

			// RotateSections (34)
			var rotateSections = RotateSections();
			song.AddSegmentByMeasure(rotateSections, 0, 360, 34, 1, 8);

			// MoveBoxes (42)
			var moveBoxes = MoveBoxes(out var moveBoxesTime);
			song.AddSegmentByMeasure(moveBoxes, 0, moveBoxesTime, 42, 1, 4);
			song.AddPaletteChangeByMeasure(42, 0);
			song.AddPaletteChangeByMeasure(43.8, 44.2, 1);
			song.AddPaletteChangeByMeasure(46, 0);

			// RotateBoxes (46)
			var rotateBoxes = RotateBoxes(out var rotateBoxesTime);
			song.AddSegmentByMeasure(rotateBoxes, 0, rotateBoxesTime, 46, 2, 4);
			song.AddPaletteChangeByMeasure(46, 0);
			song.AddPaletteChangeByMeasure(49.8, 50.2, 1);
			song.AddPaletteChangeByMeasure(54, 0);

			// Window (54)
			var window = Window();
			song.AddSegmentByMeasure(window, 0, 360, 54, 1, 8);
			song.AddPaletteChangeByMeasure(54, 0);
			song.AddPaletteChangeByMeasure(55.8, 56.2, 1);
			song.AddPaletteChangeByMeasure(57.8, 58.2, 2);
			song.AddPaletteChangeByMeasure(59.8, 60.2, 3);
			song.AddPaletteChangeByMeasure(62, 0);

			// SlideSquares (62)
			var slideSquares = SlideSquares(out var slideSquaresTime);
			song.AddSegmentByMeasure(slideSquares, 0, slideSquaresTime, 62, 8);

			// SineWaves (70)
			var sineWaves = SineWaves();
			song.AddSegmentByMeasure(sineWaves, 0, 360, 70, 2, 4);

			// Circles (78)
			var circles = Circles();
			song.AddSegmentByMeasure(circles, 0, 720, 78, 2, 4);

			// RingPulse (86)
			var ringPulse = RingPulse();
			song.AddSegmentByMeasure(ringPulse, 0, 1000, 86, 1, 9);
			song.AddPaletteChangeByMeasure(86, 0);
			song.AddPaletteChangeByMeasure(87, 1);
			song.AddPaletteChangeByMeasure(88, 2);
			song.AddPaletteChangeByMeasure(91, 3);
			song.AddPaletteChangeByMeasure(92, 4);
			song.AddPaletteChangeByMeasure(93, 5);
			song.AddPaletteChangeByMeasure(95, 0);

			// SquarePath (95)
			var squarePath = SquarePath(out var squarePathTime);
			song.AddSegmentByMeasure(squarePath, 0, squarePathTime, 95, 2, 3);
			song.AddSegmentByMeasure(squarePath, 0, squarePathTime / 2, 101, 1);
			song.AddSegmentByMeasure(squarePath, squarePathTime / 2, squarePathTime / 2, 102, 1);
			song.AddPaletteChangeByMeasure(95, 0);
			song.AddPaletteChangeByMeasure(95.2833333333333, 95.3833333333333, 1);
			song.AddPaletteChangeByMeasure(95.6166666666667, 95.7166666666667, 2);
			song.AddPaletteChangeByMeasure(95.95, 96.05, 3);
			song.AddPaletteChangeByMeasure(96.2833333333333, 96.3833333333333, 4);
			song.AddPaletteChangeByMeasure(96.6166666666667, 96.7166666666667, 5);
			song.AddPaletteChangeByMeasure(96.95, 97.05, 0);
			song.AddPaletteChangeByMeasure(97.2833333333333, 97.3833333333333, 1);
			song.AddPaletteChangeByMeasure(97.6166666666667, 97.7166666666667, 2);
			song.AddPaletteChangeByMeasure(97.95, 98.05, 3);
			song.AddPaletteChangeByMeasure(98.2833333333333, 98.3833333333333, 4);
			song.AddPaletteChangeByMeasure(98.6166666666667, 98.7166666666667, 5);
			song.AddPaletteChangeByMeasure(98.95, 99.05, 0);
			song.AddPaletteChangeByMeasure(99.2833333333333, 99.3833333333333, 1);
			song.AddPaletteChangeByMeasure(99.6166666666667, 99.7166666666667, 2);
			song.AddPaletteChangeByMeasure(99.95, 100.05, 3);
			song.AddPaletteChangeByMeasure(100.283333333333, 100.383333333333, 4);
			song.AddPaletteChangeByMeasure(100.616666666667, 100.716666666667, 5);
			song.AddPaletteChangeByMeasure(100.95, 101.05, 0);
			song.AddPaletteChangeByMeasure(101.283333333333, 101.383333333333, 1);
			song.AddPaletteChangeByMeasure(101.616666666667, 101.716666666667, 2);

			song.AddPaletteChangeByMeasure(101.95, 102.05, 6);
			song.AddPaletteChangeByMeasure(102.05, 102.5, 7);
			song.AddPaletteChangeByMeasure(103, 0);

			// End (103)

			return song;
		}
	}
}
