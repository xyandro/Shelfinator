using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Shelfinator.Creator.Patterns
{
	class Stars : IPattern
	{
		public int PatternNumber => 36;

		class StarData
		{
			static Random rand = new Random(0x0badf00d);

			public Point Point { get; set; }
			public double Size { get; set; }
			public Vector Vector { get; set; }
			public double Brightness { get; set; }

			public StarData(bool randomizeDistance = false)
			{
				Point = new Point(48, 48);
				Size = 1;// rand.Next(50, 201) / 100.0;
				var angle = rand.Next(0, 36000) * Math.PI / 18000;
				Vector = new Vector(Math.Sin(angle), Math.Cos(angle));
				if (randomizeDistance)
					Point += Vector * rand.Next(0, 48);
				Vector *= rand.Next(100, 151) / 100.0;
				Brightness = rand.Next(192, 256) / 255.0;
			}

			public void Update()
			{
				Point += Vector;
				Brightness *= 0.99;
			}
		}

		public Pattern Render()
		{
			const double Brightness = 1f / 16;
			const int NumStars = 100;
			const int Duration = 500;
			var layout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");

			var pattern = new Pattern();

			var stars = Enumerable.Range(0, NumStars).Select(x => new StarData(true)).ToList();
			for (var time = 0; time < Duration; ++time)
			{
				RenderStars(layout, pattern, stars, time, Brightness);
				UpdateStars(stars);
			}

			pattern.AddLightSequence(0, Duration, 20000, 5);

			return pattern;
		}

		void RenderStars(Layout layout, Pattern pattern, List<StarData> stars, int time, double Brightness)
		{
			var board = new double[97, 97];
			stars.ForEach(star => RenderStar(board, star));
			for (var y = 0; y < 96; ++y)
				for (var x = 0; x < 96; ++x)
					foreach (var light in layout.GetPositionLights(x, y, 1, 1))
						pattern.AddLight(light, time, pattern.Absolute, Helpers.MultiplyColor(0xffffff, Math.Min(1, board[x, y]) * Brightness));
		}

		void RenderStar(double[,] board, StarData star)
		{
			var x = star.Point.X.Round();
			var y = star.Point.Y.Round();
			if ((x >= 0) && (y >= 0) && (x < board.GetLength(0)) && (y < board.GetLength(1)))
				++board[x, y];
		}

		void UpdateStars(List<StarData> stars)
		{
			for (var ctr = 0; ctr < stars.Count; ctr++)
				stars[ctr] = UpdateStar(stars[ctr]);
		}

		StarData UpdateStar(StarData star)
		{
			star.Update();
			if ((star.Point.X + star.Size < 0) || (star.Point.Y + star.Size < 0) || (star.Point.X >= 96) || (star.Point.Y >= 96))
				star = new StarData();
			return star;
		}
	}
}
