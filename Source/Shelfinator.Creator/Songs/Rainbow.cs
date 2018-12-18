using System.Linq;
using System.Windows;
using System;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Rainbow : ISong
	{
		public int SongNumber => 46;

		public Song Render()
		{
			const double Brightness = 1f / 16;
			const int StartTime = 500;
			const int RotateTime = 2000;
			const int RotateIncrement = 10;
			const int RotateCount = 10;

			var segment = new Segment();
			var layout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");
			var lights = layout.GetAllLights();

			var colors = Helpers.Rainbow7.Multiply(Brightness).ToList();
			var useColors = new LightColor(0, 96, colors);

			var center = new Point(48, 48);
			var times = lights.Select(light => layout.GetLightPosition(light)).GetDistance(center).Scale(0, null, 0, StartTime).Round().ToList();
			var lightTimes = lights.ToDictionary(times);
			foreach (var pair in lightTimes)
				segment.AddLight(pair.Key, pair.Value, useColors, layout.GetLightPosition(pair.Key).X.Round());

			for (var angle = 0; angle <= 360; angle += RotateIncrement)
			{
				var time = StartTime + angle * RotateTime / 360;
				var nextTime = StartTime + (angle + RotateIncrement) * RotateTime / 360;
				var sin = Math.Sin(angle * Math.PI / 180);
				var cos = Math.Cos(angle * Math.PI / 180);
				foreach (var light in lights)
				{
					var location = layout.GetLightPosition(light);
					var newX = (location.X - 48) * cos - (location.Y - 48) * sin + 48;
					segment.AddLight(light, time, nextTime, null, 0, useColors, newX.Round());
				}
			}

			foreach (var pair in lightTimes)
				segment.AddLight(pair.Key, StartTime + RotateTime + StartTime - pair.Value, Segment.Absolute, 0x000000);

			var song = new Song("rainbow.wav");
			song.AddSegment(segment, 0, StartTime);
			song.AddSegment(segment, StartTime, StartTime + RotateTime, repeat: RotateCount);
			song.AddSegment(segment, StartTime + RotateTime, StartTime + RotateTime + StartTime);
			return song;
		}
	}
}
