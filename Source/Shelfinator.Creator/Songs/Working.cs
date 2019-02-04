using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Working : ISong
	{
		public int SongNumber => 8;

		readonly Layout bodyLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");

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

		public Song Render()
		{
			var song = new Song("orchestra.mp3");

			var squares = Squares();
			song.AddSegment(squares, 0, 360, 0, 1890, 10);

			return song;
		}
	}
}
