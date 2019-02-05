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

		Segment Intro()
		{
			const int SnapTime = 297;
			const int SnapFadeTime = 150;
			const int BeatSize = 20;
			const int NoteSize = 9;
			var segment = new Segment();

			var innerLights = bodyLayout.GetPositionLights(38, 38, 21, 21).Except(bodyLayout.GetPositionLights(40, 40, 17, 17)).ToList();
			var middleLights = bodyLayout.GetPositionLights(19, 19, 59, 59).Except(bodyLayout.GetPositionLights(21, 21, 55, 55)).ToList();
			var outerLights = bodyLayout.GetPositionLights(0, 0, 97, 97).Except(bodyLayout.GetPositionLights(2, 2, 93, 93)).ToList();

			var center = new Point(48, 48);
			var snapColor = new LightColor(0, 1000, new List<int> { 0x000010, 0x000001 });
			for (var time = 750; time <= 59556; time += 1188)
				if ((time != 20946) && (time != 58962))
					foreach (var light in innerLights)
					{
						var colorValue = (((bodyLayout.GetLightPosition(light) - center).Length - 9) * 194.47172793051).Round();
						segment.AddLight(light, time - colorValue * SnapFadeTime / 1000, snapColor, colorValue);
						segment.AddLight(light, time - colorValue * SnapFadeTime / 1000 + SnapTime, 0x000000);
					}

			var beats = new List<MidiNote>
			{
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 2532, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 2730, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 2928, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 3126, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 3423, 1485),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 4908, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 5106, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 5304, 495),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 5799, 594),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 6393, 594),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 6987, 297),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 7284, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 7482, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 7680, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 7878, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 8175, 1485),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 9660, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 9858, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 10056, 495),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 10551, 594),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 11145, 495),
				new MidiNote(MidiNote.PianoNote.BFlat, 2, 11640, 396),

				new MidiNote(MidiNote.PianoNote.C, 3, 12036, 198),
				new MidiNote(MidiNote.PianoNote.C, 4, 12234, 198),
				new MidiNote(MidiNote.PianoNote.C, 5, 12432, 198),
				new MidiNote(MidiNote.PianoNote.C, 4, 12630, 297),
				new MidiNote(MidiNote.PianoNote.C, 3, 12927, 1485),

				new MidiNote(MidiNote.PianoNote.AFlat, 2, 14412, 198),
				new MidiNote(MidiNote.PianoNote.AFlat, 3, 14610, 198),
				new MidiNote(MidiNote.PianoNote.AFlat, 4, 14808, 198),
				new MidiNote(MidiNote.PianoNote.G, 5, 15006, 297),
				new MidiNote(MidiNote.PianoNote.AFlat, 2, 15303, 594),
				new MidiNote(MidiNote.PianoNote.AFlat, 2, 15897, 495),
				new MidiNote(MidiNote.PianoNote.BFlat, 2, 16392, 396),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 16788, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 16986, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 17184, 495),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 17679, 1485),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 19164, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 19362, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 19560, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 19758, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 20055, 594),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 20649, 891),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 21540, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 21738, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 21936, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 6, 22134, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 22431, 1485),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 23916, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 24114, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 24312, 495),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 24807, 594),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 25401, 495),
				new MidiNote(MidiNote.PianoNote.G, 2, 25896, 396),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 26292, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 26490, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 26688, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 6, 26886, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 27183, 1485),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 28668, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 28866, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 29064, 495),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 29559, 594),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 30153, 297),
				new MidiNote(MidiNote.PianoNote.F, 4, 30450, 198),
				new MidiNote(MidiNote.PianoNote.BFlat, 2, 30648, 198),
				new MidiNote(MidiNote.PianoNote.F, 4, 30846, 198),

				new MidiNote(MidiNote.PianoNote.C, 3, 31044, 198),
				new MidiNote(MidiNote.PianoNote.C, 4, 31242, 198),
				new MidiNote(MidiNote.PianoNote.C, 5, 31440, 198),
				new MidiNote(MidiNote.PianoNote.C, 4, 31638, 297),
				new MidiNote(MidiNote.PianoNote.C, 3, 31935, 1485),

				new MidiNote(MidiNote.PianoNote.AFlat, 2, 33420, 198),
				new MidiNote(MidiNote.PianoNote.AFlat, 3, 33618, 198),
				new MidiNote(MidiNote.PianoNote.C, 5, 33816, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 34014, 297),
				new MidiNote(MidiNote.PianoNote.AFlat, 2, 34311, 594),
				new MidiNote(MidiNote.PianoNote.AFlat, 2, 34905, 495),
				new MidiNote(MidiNote.PianoNote.BFlat, 2, 35400, 396),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 35796, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 35994, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 36192, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 6, 36390, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 36687, 1485),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 38172, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 38370, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 38568, 495),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 39063, 594),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 39657, 594),
				new MidiNote(MidiNote.PianoNote.G, 2, 40251, 297),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 40548, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 40746, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 40944, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 41142, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 41439, 1485),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 42924, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 43122, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 43320, 495),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 43815, 594),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 44409, 594),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 45003, 297),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 45300, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 45498, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 45696, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 45894, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 46191, 1485),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 47676, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 47874, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 48072, 495),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 48567, 594),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 49161, 495),
				new MidiNote(MidiNote.PianoNote.BFlat, 2, 49656, 396),

				new MidiNote(MidiNote.PianoNote.C, 3, 50052, 198),
				new MidiNote(MidiNote.PianoNote.C, 4, 50250, 198),
				new MidiNote(MidiNote.PianoNote.C, 5, 50448, 198),
				new MidiNote(MidiNote.PianoNote.C, 4, 50646, 297),
				new MidiNote(MidiNote.PianoNote.C, 3, 50943, 1485),

				new MidiNote(MidiNote.PianoNote.AFlat, 2, 52428, 198),
				new MidiNote(MidiNote.PianoNote.AFlat, 3, 52626, 198),
				new MidiNote(MidiNote.PianoNote.AFlat, 4, 52824, 198),
				new MidiNote(MidiNote.PianoNote.G, 5, 53022, 297),
				new MidiNote(MidiNote.PianoNote.AFlat, 2, 53319, 594),
				new MidiNote(MidiNote.PianoNote.AFlat, 2, 53913, 495),
				new MidiNote(MidiNote.PianoNote.BFlat, 2, 54408, 396),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 54804, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 55002, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 55200, 495),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 55695, 1485),

				new MidiNote(MidiNote.PianoNote.EFlat, 3, 57180, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 57378, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 57576, 198),
				new MidiNote(MidiNote.PianoNote.EFlat, 4, 57774, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 58071, 594),
				new MidiNote(MidiNote.PianoNote.EFlat, 3, 58665, 594),
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
				segment.AddLight(light, 16788, 21540, 0x040102, 0x401020);
				segment.AddLight(light, 21540, 0x000000);
			}

			var notes = new List<MidiNote>
			{
				new MidiNote(MidiNote.PianoNote.G, 5, 21540, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 22134, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 22431, 297),
				new MidiNote(MidiNote.PianoNote.AFlat, 5, 22728, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 23025, 297),
				new MidiNote(MidiNote.PianoNote.F, 5, 23322, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 23619, 1485),
				new MidiNote(MidiNote.PianoNote.C, 5, 25104, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 25698, 594),

				new MidiNote(MidiNote.PianoNote.G, 5, 26292, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 26886, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 27183, 297),
				new MidiNote(MidiNote.PianoNote.AFlat, 5, 27480, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 27777, 297),
				new MidiNote(MidiNote.PianoNote.F, 5, 28074, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 28371, 1485),
				new MidiNote(MidiNote.PianoNote.C, 5, 29856, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 30450, 594),

				new MidiNote(MidiNote.PianoNote.G, 5, 31044, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 31638, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 31935, 297),
				new MidiNote(MidiNote.PianoNote.AFlat, 5, 32232, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 32529, 297),
				new MidiNote(MidiNote.PianoNote.F, 5, 32826, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 33123, 1485),
				new MidiNote(MidiNote.PianoNote.C, 5, 34608, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 35202, 594),

				new MidiNote(MidiNote.PianoNote.G, 5, 35796, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 36390, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 36687, 297),
				new MidiNote(MidiNote.PianoNote.F, 5, 36984, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 37281, 297),
				new MidiNote(MidiNote.PianoNote.D, 5, 37578, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 37875, 2673),

				new MidiNote(MidiNote.PianoNote.G, 5, 40548, 594),      new MidiNote(MidiNote.PianoNote.EFlat, 5, 40548, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 41142, 297),      new MidiNote(MidiNote.PianoNote.EFlat, 5, 41142, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 41439, 297),      new MidiNote(MidiNote.PianoNote.EFlat, 5, 41439, 297),
				new MidiNote(MidiNote.PianoNote.AFlat, 5, 41736, 297),  new MidiNote(MidiNote.PianoNote.F, 5, 41736, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 42033, 297),      new MidiNote(MidiNote.PianoNote.EFlat, 5, 42033, 297),
				new MidiNote(MidiNote.PianoNote.F, 5, 42330, 297),      new MidiNote(MidiNote.PianoNote.D, 5, 42330, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 42627, 1485), new MidiNote(MidiNote.PianoNote.C, 5, 42627, 1485),
				new MidiNote(MidiNote.PianoNote.C, 5, 44112, 594),      new MidiNote(MidiNote.PianoNote.AFlat, 4, 44112, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 44706, 594),      new MidiNote(MidiNote.PianoNote.EFlat, 5, 44706, 594),

				new MidiNote(MidiNote.PianoNote.G, 5, 45300, 594),      new MidiNote(MidiNote.PianoNote.EFlat, 5, 45300, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 45894, 297),      new MidiNote(MidiNote.PianoNote.EFlat, 5, 45894, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 46191, 297),      new MidiNote(MidiNote.PianoNote.EFlat, 5, 46191, 297),
				new MidiNote(MidiNote.PianoNote.AFlat, 5, 46488, 297),  new MidiNote(MidiNote.PianoNote.F, 5, 46488, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 46785, 297),      new MidiNote(MidiNote.PianoNote.EFlat, 5, 46785, 297),
				new MidiNote(MidiNote.PianoNote.F, 5, 47082, 297),      new MidiNote(MidiNote.PianoNote.D, 5, 47082, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 47379, 1485), new MidiNote(MidiNote.PianoNote.C, 5, 47379, 1485),
				new MidiNote(MidiNote.PianoNote.C, 5, 48864, 594),      new MidiNote(MidiNote.PianoNote.AFlat, 4, 48864, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 49458, 594),      new MidiNote(MidiNote.PianoNote.EFlat, 5, 49458, 594),

				new MidiNote(MidiNote.PianoNote.G, 5, 50052, 594),      new MidiNote(MidiNote.PianoNote.EFlat, 5, 50052, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 50646, 297),      new MidiNote(MidiNote.PianoNote.EFlat, 5, 50646, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 50943, 297),      new MidiNote(MidiNote.PianoNote.EFlat, 5, 50943, 297),
				new MidiNote(MidiNote.PianoNote.AFlat, 5, 51240, 297),  new MidiNote(MidiNote.PianoNote.F, 5, 51240, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 51537, 297),      new MidiNote(MidiNote.PianoNote.EFlat, 5, 51537, 297),
				new MidiNote(MidiNote.PianoNote.F, 5, 51834, 297),      new MidiNote(MidiNote.PianoNote.D, 5, 51834, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 52131, 1485), new MidiNote(MidiNote.PianoNote.C, 5, 52131, 1485),
				new MidiNote(MidiNote.PianoNote.C, 5, 53616, 594),      new MidiNote(MidiNote.PianoNote.AFlat, 4, 53616, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 54210, 594),      new MidiNote(MidiNote.PianoNote.EFlat, 5, 54210, 594),

				new MidiNote(MidiNote.PianoNote.G, 5, 54804, 594),      new MidiNote(MidiNote.PianoNote.EFlat, 5, 54804, 594),
				new MidiNote(MidiNote.PianoNote.G, 5, 55398, 297),
				new MidiNote(MidiNote.PianoNote.G, 5, 55695, 297),
				new MidiNote(MidiNote.PianoNote.F, 5, 55992, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 56289, 297),
				new MidiNote(MidiNote.PianoNote.D, 5, 56586, 297),
				new MidiNote(MidiNote.PianoNote.EFlat, 5, 56883, 297),
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

			var endTimes = new List<int> { 58962, 59111, 59259, 59408 };
			for (int i = 0; i < endTimes.Count; i++)
				foreach (var light in bodyLayout.GetAllLights())
					segment.AddLight(light, endTimes[i], i % 2 == 0 ? 0x040102 : 0x080204);

			return segment;
		}

		Segment Squares()
		{
			const int NumSquares = 6;
			const double MinRadius = 8.999;
			const double MaxRadius = 48.001;
			const double MaxVariance = 30;
			const double EvenSize = (MaxRadius - MinRadius) / NumSquares;

			var segment = new Segment();
			var color = new LightColor(0, NumSquares - 1, Helpers.Rainbow6);
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
			const int TotalTime = 1000;
			var segment = new Segment();
			var center = new Point(48, 48);
			var color = new LightColor(0, 1000, Helpers.Rainbow6);
			var lines = new List<Tuple<Point, Size, int, int, bool>>
			{
				Tuple.Create(new Point(0, 0), new Size(1, 97), -80, 12, false),
				Tuple.Create(new Point(1, 0), new Size(1, 97), 63, 17, false),
				Tuple.Create(new Point(19, 0), new Size(1, 97), -76, 16, false),
				Tuple.Create(new Point(20, 0), new Size(1, 97), 72, 17, false),
				Tuple.Create(new Point(38, 0), new Size(1, 97), -80, 14, false),
				Tuple.Create(new Point(39, 0), new Size(1, 97), 78, 15, false),
				Tuple.Create(new Point(57, 0), new Size(1, 97), -71, 17, false),
				Tuple.Create(new Point(58, 0), new Size(1, 97), 81, 19, false),
				Tuple.Create(new Point(76, 0), new Size(1, 97), -80, 20, false),
				Tuple.Create(new Point(77, 0), new Size(1, 97), 73, 10, false),
				Tuple.Create(new Point(95, 0), new Size(1, 97), -92, 13, false),
				Tuple.Create(new Point(96, 0), new Size(1, 97), 71, 17, false),
				Tuple.Create(new Point(0, 0), new Size(97, 1), -84, 15, true),
				Tuple.Create(new Point(0, 1), new Size(97, 1), 80, 17, true),
				Tuple.Create(new Point(0, 19), new Size(97, 1), -85, 12, true),
				Tuple.Create(new Point(0, 20), new Size(97, 1), 95, 12, true),
				Tuple.Create(new Point(0, 38), new Size(97, 1), -91, 10, true),
				Tuple.Create(new Point(0, 39), new Size(97, 1), 88, 16, true),
				Tuple.Create(new Point(0, 57), new Size(97, 1), -82, 15, true),
				Tuple.Create(new Point(0, 58), new Size(97, 1), 82, 19, true),
				Tuple.Create(new Point(0, 76), new Size(97, 1), -72, 17, true),
				Tuple.Create(new Point(0, 77), new Size(97, 1), 72, 11, true),
				Tuple.Create(new Point(0, 95), new Size(97, 1), -88, 13, true),
				Tuple.Create(new Point(0, 96), new Size(97, 1), 63, 14, true),
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

		public Song Render()
		{
			var song = new Song("pynk.mp3"); // First sound is at 750; Measures start at 2532, repeat every 2376, and stop at 240132. Beats appear quantized to 2376/24 = 99

			// Intro (0)
			var intro = Intro();
			song.AddSegment(intro, 0, 59556, 0);

			// Next (59556)

			//var squares = Squares();
			//song.AddSegment(squares, 0, 360, 0, 1890, 10);

			//var wavy = Wavy();
			//song.AddSegment(wavy, 0, 1000, 0, 1890 * 10);

			return song;
		}
	}
}
