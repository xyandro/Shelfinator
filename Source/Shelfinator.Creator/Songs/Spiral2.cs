//using System.Collections.Generic;
//using System.Linq;
//using System;
//using Shelfinator.Creator.SongData;
//
//namespace Shelfinator.Creator.Songs
//{
//	class Spiral2 : ISong
//	{
//		public int SongNumber => 30;
//
//		static double Mod(double value, double divisor) => (value %= divisor) < 0 ? value + divisor : value;
//
//		public Song Render()
//		{
//			const double Brightness = 1f / 16;
//			const double Radius = 5;
//			var layout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");
//			var points = layout.GetAllLights().Select(light => layout.GetLightPosition(light)).ToList();
//			var angles = points.Select(p => Mod(Math.Atan2(p.Y - 48, p.X - 48), 2 * Math.PI)).ToList();
//			var values = points.Select((p, index) => 2 * Math.PI * (int)((Math.Sqrt(Math.Pow(p.X - 48, 2) + Math.Pow(p.Y - 48, 2)) - angles[index] / 2 / Math.PI * Radius) / Radius) + angles[index]).ToList();
//			var minVal = values.Min();
//			var maxVal = values.Max();
//
//			var color = new List<LightColor>
//			{
//				new LightColor(0, 8, new List<List<int>> { new List<int> { 0x000000, 0xffffff, 0xff0000, 0xffffff, 0xff0000, 0xffffff, 0xff0000, 0x000000 }.Multiply(Brightness).ToList() }),
//				new LightColor(0, 8, new List<List<int>> { new List<int> { 0x000000, 0xff2712, 0x68b032, 0xfd9802, 0x0049ff, 0xfeff35, 0x8701b0, 0x000000 }.Multiply(Brightness).ToList() }),
//				new LightColor(0, 8, new List<List<int>> { new List<int> { 0x000000, 0x93d7fe, 0x289afb, 0x1163ed, 0x5c89da, 0x436ab9, 0x4485e3, 0x000000 }.Multiply(Brightness).ToList() }),
//				new LightColor(0, 8, new List<List<int>> { new List<int> { 0x000000 }.Concat(Helpers.Rainbow6).Concat(0x000000).Multiply(Brightness).ToList() }),
//			};
//			const int Length = 5000;
//			const int Delay = 2000;
//			const int ColorDuration = 4000;
//			var segment = new Segment();
//			for (var ctr = 0; ctr < points.Count; ctr++)
//			{
//				var light = layout.GetPositionLight(points[ctr]);
//				var time = (int)((values[ctr] - minVal) / (maxVal - minVal) * Length);
//
//				for (var pass = 0; pass < color.Count; ++pass)
//					segment.AddLight(light, time + pass * (Length + Delay), time + pass * (Length + Delay) + ColorDuration, color[pass], 0, color[pass], 8, true);
//			}
//
//			var song = new Song();
//			song.AddSegment(segment, 0, segment.MaxLightTime());
//			return song;
//		}
//	}
//}
