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

			var snapColor = new LightColor(0, 1000, new List<int> { 0x000010, 0x000001 });
			for (var time = 750; time <= 59556; time += 1188)
				if ((time != 20946) && (time != 58962))
					foreach (var light in innerLights)
					{
						var colorValue = (((bodyLayout.GetLightPosition(light) - Helpers.Center).Length - 9) * 194.47172793051).Round();
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
				new List<int> { 0x100010, 0x001010 },
				new List<int> { 0x100408, 0x000000 });
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

		enum SlideSquaresDirection { Up, Right, Down, Left, Fill, None }
		Segment SlideSquares(out int time)
		{
			var dist = new Dictionary<int, List<Tuple<Vector, int>>>();
			for (var square = 0; square < 25; ++square)
			{
				dist[square] = new List<Tuple<Vector, int>>();
				var squarePoint = new Point(square % 5 * 19 + 1, square / 5 * 19 + 1);
				foreach (var light in bodyLayout.GetPositionLights(squarePoint, 19, 19))
				{
					var lightPoint = bodyLayout.GetLightPosition(light);
					dist[square].Add(Tuple.Create(lightPoint - squarePoint, (((lightPoint - Helpers.Center).Length - 9) * 16.9830463869911).Round()));
				}
			}
			var color = new LightColor(0, 1000, Helpers.Rainbow6);
			var squares = new int?[5, 5] { { 10, 15, 20, 21, 22 }, { 16, 17, 12, 13, 23 }, { 5, 6, null, 8, 18 }, { 0, 11, 2, 7, 19 }, { 1, 3, 4, 9, 14 } };
			var moves = new List<SlideSquaresDirection> { SlideSquaresDirection.Up, SlideSquaresDirection.Right, SlideSquaresDirection.Down, SlideSquaresDirection.Down, SlideSquaresDirection.Left, SlideSquaresDirection.Left, SlideSquaresDirection.Up, SlideSquaresDirection.Up, SlideSquaresDirection.Up, SlideSquaresDirection.Right, SlideSquaresDirection.Right, SlideSquaresDirection.Right, SlideSquaresDirection.Down, SlideSquaresDirection.Down, SlideSquaresDirection.Down, SlideSquaresDirection.Down, SlideSquaresDirection.Left, SlideSquaresDirection.Left, SlideSquaresDirection.Left, SlideSquaresDirection.Left, SlideSquaresDirection.Up, SlideSquaresDirection.Up, SlideSquaresDirection.Up, SlideSquaresDirection.Up, SlideSquaresDirection.Right, SlideSquaresDirection.Right, SlideSquaresDirection.Right, SlideSquaresDirection.Down, SlideSquaresDirection.Down, SlideSquaresDirection.Down, SlideSquaresDirection.Left, SlideSquaresDirection.Left, SlideSquaresDirection.Up, SlideSquaresDirection.Up, SlideSquaresDirection.Right, SlideSquaresDirection.Down, SlideSquaresDirection.Down, SlideSquaresDirection.Left, SlideSquaresDirection.Up, SlideSquaresDirection.Up, SlideSquaresDirection.Right, SlideSquaresDirection.Right, SlideSquaresDirection.Down, SlideSquaresDirection.Down, SlideSquaresDirection.Down, SlideSquaresDirection.Left, SlideSquaresDirection.Left, SlideSquaresDirection.Left, SlideSquaresDirection.Up, SlideSquaresDirection.Up, SlideSquaresDirection.Up, SlideSquaresDirection.Up, SlideSquaresDirection.Right, SlideSquaresDirection.Right, SlideSquaresDirection.Right, SlideSquaresDirection.Right, SlideSquaresDirection.Down, SlideSquaresDirection.Down, SlideSquaresDirection.Down, SlideSquaresDirection.Down, SlideSquaresDirection.None, SlideSquaresDirection.Fill, SlideSquaresDirection.None, SlideSquaresDirection.None };
			time = 0;
			var segment = new Segment();
			var moveDict = new Dictionary<SlideSquaresDirection, Vector>
			{
				[SlideSquaresDirection.Up] = new Vector(0, -1),
				[SlideSquaresDirection.Down] = new Vector(0, 1),
				[SlideSquaresDirection.Left] = new Vector(-1, 0),
				[SlideSquaresDirection.Right] = new Vector(1, 0),
			};
			foreach (var move in moves)
			{
				segment.Clear(time);
				var empty = default(Point);
				for (var y = 0; y < 5; ++y)
					for (var x = 0; x < 5; ++x)
					{
						var point = new Point(x * 19 + 1, y * 19 + 1);
						if (squares[x, y].HasValue)
						{
							foreach (var pair in dist[squares[x, y].Value])
								segment.AddLight(bodyLayout.GetPositionLight(point + pair.Item1), time, color, pair.Item2);
						}
						else
							empty = new Point(x, y);
					}

				if (moveDict.ContainsKey(move))
				{
					var dest = empty + moveDict[move];
					squares[empty.X.Round(), empty.Y.Round()] = squares[dest.X.Round(), dest.Y.Round()];
					squares[dest.X.Round(), dest.Y.Round()] = null;
				}
				else if (move == SlideSquaresDirection.Fill)
					squares[empty.X.Round(), empty.Y.Round()] = empty.Y.Round() * 5 + empty.X.Round();

				++time;
			}
			return segment;
		}

		Segment Window()
		{
			const double FakeSize = 30;
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
			var rotations = new List<Func<Point, Point>>
			{
				point => new Point(point.X, point.Y),
				point => new Point(-point.Y, point.X),
				point => new Point(-point.X, -point.Y),
				point => new Point(point.Y, -point.X),
			};

			var color = new LightColor(0, 5, new List<int> { 0x100000, 0x001000, 0x000010, 0x101000, 0x100010, 0x001010 });
			var offsets = new List<double> { -90, -36.86989765, -11.53695903, 11.53695903, 36.86989765, 90 };
			var segment = new Segment();
			for (var angle = 0; angle < 360; angle += 2)
			{
				segment.Clear(angle);
				for (var y = 0; y < 6; ++y)
				{
					var point = new Point(47.5 * Math.Sin((angle / 180 * 360 + angle % 180 + offsets[y]) * Math.PI / 180), -47.5 + y * 19);

					foreach (var rotation in rotations)
					{
						var newPoint = rotation(point);
						foreach (var light in bodyLayout.GetPositionLights(newPoint.X + 47.5, newPoint.Y + 47.5, 2, 2))
							segment.AddLight(light, angle, color, y);
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

			// SlideSquares (126084)
			var slideSquares = SlideSquares(out var slideSquaresTime);
			song.AddSegment(slideSquares, 0, slideSquaresTime, 126084, 19008);

			// Window (145092)
			var window = Window();
			song.AddSegment(window, 0, 360, 145092, 2376, 8);
			song.AddPaletteChange(145092, 0);
			song.AddPaletteChange(149344, 150344, 1);
			song.AddPaletteChange(154096, 155096, 2);
			song.AddPaletteChange(158848, 159848, 3);
			song.AddPaletteChange(164100, 0);

			// SineWaves (164100)
			var sineWaves = SineWaves();
			song.AddSegment(sineWaves, 0, 360, 164100, 4752, 4);

			// Next (183108)

			return song;
		}
	}
}
