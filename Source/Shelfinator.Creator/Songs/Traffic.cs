using System.Collections.Generic;
using System.Linq;
using System;
using Shelfinator.Creator.SongData;

namespace Shelfinator.Creator.Songs
{
	class Traffic : ISong
	{
		public int SongNumber => 45;

		class Car
		{
			public int X, Y, Width, Height, DX, DY, Color, Timer, Interval, Count;

			public bool Done => Count == 0;
			public Car CreateCar()
			{
				if (Done)
					return null;

				if (Timer-- != 0)
					return null;

				var car = new Car
				{
					X = X,
					Y = Y,
					Width = Width,
					Height = Height,
					DX = DX,
					DY = DY,
					Color = Color,
				};

				--Count;
				Timer = Interval - 1;
				Color = (Color + 1) % 6;
				return car;
			}

			public void Move()
			{
				X += DX;
				Y += DY;
			}
		}

		public Song Render()
		{
			const double Brightness = 1f / 16;
			const int Size = 5;
			const int Gap = 1;
			const int Interval = (Size + 2) * 2 + Gap * 2;
			const int CycleLength = Interval * 6;
			const int DisplayTime = 5000;

			var diff = 19 % Interval;
			var diff2 = diff - Interval;
			if (Math.Abs(diff2) < diff)
				diff = diff2;

			var segment = new Segment();
			var layout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");

			var color = new LightColor(0, 5,
				new List<int> { 0xffffff, 0xffffff, 0xffffff, 0xffffff, 0xffffff, 0xffffff }.Multiply(Brightness).ToList(),
				new List<int> { 0xda3369, 0xf36b5b, 0x6a8f1b, 0x966233, 0xf49d4e, 0x59b2c2 }.Multiply(Brightness).ToList(),
				Helpers.Rainbow6.Multiply(Brightness).ToList());

			var creators = new List<Car>();
			for (var ctr = 0; ctr < 6; ++ctr)
			{
				creators.Add(new Car { X = ctr * 19, Y = -Size + 1, Width = 2, Height = Size, DX = 0, DY = 1, Timer = ctr * diff, Interval = Interval, Count = 24, Color = ctr });
				creators.Add(new Car { X = -Size + 1, Y = ctr * 19, Width = Size, Height = 2, DX = 1, DY = 0, Timer = ctr * diff + Size + 2 + Gap, Interval = Interval, Count = 24, Color = ctr });
			}
			var minTimer = creators.Min(creator => creator.Timer);
			creators.ForEach(creator => creator.Timer -= minTimer);
			var cars = new List<Car>();

			var time = 0;
			while ((creators.Any()) || (cars.Any()))
			{
				segment.Clear(time);

				creators = creators.Where(creator => !creator.Done).ToList();
				cars.AddRange(creators.Select(creator => creator.CreateCar()).NonNull());

				for (var ctr = 0; ctr < cars.Count;)
				{
					var car = cars[ctr];
					var lights = layout.GetPositionLights(car.X, car.Y, car.Width, car.Height);
					if (!lights.Any())
						cars.RemoveAt(ctr);
					else
					{
						foreach (var light in lights)
							segment.AddLight(light, time, color, car.Color);
						car.Move();
						++ctr;
					}
				}

				++time;
			}

			var song = new Song();
			song.AddSegment(segment, 0, CycleLength * 2, DisplayTime * 2);
			song.AddSegment(segment, CycleLength * 2, CycleLength * 3, DisplayTime, 2);
			song.AddSegment(segment, CycleLength * 3, time, DisplayTime * (time - CycleLength * 3) / CycleLength);

			var sequenceTime = song.MaxSegmentTime();
			song.AddPaletteSequence(0, 0);
			song.AddPaletteSequence(sequenceTime * 1 / 3 - 500, sequenceTime * 1 / 3 + 500, null, 1);
			song.AddPaletteSequence(sequenceTime * 2 / 3 - 500, sequenceTime * 2 / 3 + 500, null, 2);

			return song;
		}
	}
}
