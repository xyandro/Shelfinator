//using System.Collections.Generic;
//using System.Linq;
//using System.Windows;
//using System;
//using Shelfinator.Creator.SongData;
//
//namespace Shelfinator.Creator.Songs
//{
//	class CornerRotate : ISong
//	{
//		public int SongNumber => 35;
//
//		public Song Render()
//		{
//			const double Brightness = 1f / 16;
//			var layout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");
//
//			var segment = new Segment();
//
//			var white = new List<int> { 0xffffff, 0xffffff, 0xff0000, 0xff0000, 0xffffff, 0xffffff, 0xff0000, 0xff0000, 0xffffff, 0xffffff, 0xff0000 }.Multiply(Brightness).ToList();
//			var pastel = new List<int> { 0x17b7ab, 0xbcd63d, 0xe71880, 0xf15a25, 0x17b7ab, 0xbcd63d, 0xe71880, 0xf15a25, 0x17b7ab, 0xbcd63d, 0xe71880 }.Multiply(Brightness).ToList();
//			var rainbow = Helpers.Rainbow6.Multiply(Brightness).ToList();
//			rainbow.InsertRange(0, rainbow.Skip(1).Reverse().ToList());
//
//			var color = new LightColor(-96, 96, white, pastel, rainbow);
//
//			var center = new Point(0, 0);
//			for (var angle = 0; angle < 90; ++angle)
//				foreach (var light in layout.GetAllLights())
//				{
//					var position = layout.GetLightPosition(light);
//					var vector = position - center;
//					var curAngle = Math.Atan2(vector.Y, vector.X) + angle * Math.PI / 180;
//					var newPosition = new Point(vector.Length * Math.Cos(curAngle), vector.Length * Math.Sin(curAngle));
//					segment.AddLight(light, angle, color, (int)newPosition.X);
//				}
//
//			center = new Point(96, 0);
//			for (var angle = 90; angle < 180; ++angle)
//				foreach (var light in layout.GetAllLights())
//				{
//					var position = layout.GetLightPosition(light);
//					var vector = position - center;
//					var curAngle = Math.Atan2(vector.Y, vector.X) + angle * Math.PI / 180;
//					var newPosition = new Point(vector.Length * Math.Cos(curAngle), vector.Length * Math.Sin(curAngle));
//					segment.AddLight(light, angle, color, (int)newPosition.X);
//				}
//
//			center = new Point(96, 96);
//			for (var angle = 180; angle < 270; ++angle)
//				foreach (var light in layout.GetAllLights())
//				{
//					var position = layout.GetLightPosition(light);
//					var vector = position - center;
//					var curAngle = Math.Atan2(vector.Y, vector.X) + angle * Math.PI / 180;
//					var newPosition = new Point(vector.Length * Math.Cos(curAngle), vector.Length * Math.Sin(curAngle));
//					segment.AddLight(light, angle, color, (int)newPosition.X);
//				}
//
//			center = new Point(0, 96);
//			for (var angle = 270; angle < 360; ++angle)
//				foreach (var light in layout.GetAllLights())
//				{
//					var position = layout.GetLightPosition(light);
//					var vector = position - center;
//					var curAngle = Math.Atan2(vector.Y, vector.X) + angle * Math.PI / 180;
//					var newPosition = new Point(vector.Length * Math.Cos(curAngle), vector.Length * Math.Sin(curAngle));
//					segment.AddLight(light, angle, color, (int)newPosition.X);
//				}
//
//			var song = new Song();
//
//			song.AddSegment(segment, 0, 360, 5000, 5);
//
//			song.AddPaletteSequence(0, 0);
//			song.AddPaletteSequence(5000, 10000, null, 1);
//			song.AddPaletteSequence(15000, 20000, null, 2);
//
//			return song;
//		}
//	}
//}
