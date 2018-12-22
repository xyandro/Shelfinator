using System;
using System.Windows;

namespace Shelfinator.Creator.Songs
{
	static class SongHelper
	{
		readonly static Layout headerLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Header.png");
		readonly static Layout bodyLayout = new Layout("Shelfinator.Creator.Songs.Layout.Layout-Body.png");

		public static int? GetLight(Point p, bool allowHeader = true)
		{
			int? light = null;
			if ((p.Y < 0) && (allowHeader))
				light = light ?? headerLayout.TryGetPositionLight(new Point(p.X, p.Y + 10));
			return light ?? bodyLayout.TryGetPositionLight(p);
		}

		public static Point NextLocation(Point location, Point dest, bool runAway = false)
		{
			if ((location.Y < 0) && (dest.Y >= 0))
				dest = new Point(((dest.X - 39) / 18 * 31).Round(), dest.Y);
			else if ((location.Y >= 0) && (dest.Y < 0))
				dest = new Point((dest.X / 31 * 18 + 39).Round(), dest.Y);

			if ((location.Y == 0) && (dest.Y < 0))
			{
				var p = new Point(((location.X - 39) / 18 * 31).Round(), -3);
				if (GetLight(p, !runAway) != null)
					return p;

				return new Point(location.X + (dest.X < location.X ? -1 : 1), location.Y);
			}
			else if ((location.Y == -3) && (dest.Y >= 0))
			{
				var p = new Point(((location.X) / 31 * 18 + 39).Round(), 0);
				if (GetLight(p, !runAway) != null)
					return p;

				return new Point(location.X + (dest.X < location.X ? -1 : 1), location.Y);
			}
			else if (Math.Abs(location.Y - dest.Y) >= Math.Abs(location.X - dest.X))
			{
				var next = new Point(location.X, location.Y + (location.Y <= dest.Y != runAway ? 1 : -1));
				if (GetLight(next, !runAway) != null)
					return next;
				var x = location.X - location.X % 19 + 1;
				var back = Math.Abs(x - dest.X) <= Math.Abs(x + 18 - dest.X);
				next = new Point(location.X + (back != runAway ? -1 : 1), location.Y);
				if (GetLight(next, !runAway) != null)
					return next;
				next = new Point(location.X + (back == runAway ? -1 : 1), location.Y);
				if (GetLight(next, !runAway) != null)
					return next;
			}
			else
			{
				var next = new Point(location.X + (location.X <= dest.X != runAway ? 1 : -1), location.Y);
				if (GetLight(next, !runAway) != null)
					return next;
				bool back;
				if (location.Y < 0)
					back = dest.Y < location.Y;
				else
				{
					var y = location.Y - location.Y % 19 + 1;
					back = Math.Abs(y - dest.Y) <= Math.Abs(y + 18 - dest.Y);
				}

				next = new Point(location.X, location.Y + (back != runAway ? -1 : 1));
				if (GetLight(next, !runAway) != null)
					return next;
				next = new Point(location.X, location.Y + (back == runAway ? -1 : 1));
				if (GetLight(next, !runAway) != null)
					return next;
			}

			throw new Exception("Cannot find next point");
		}
	}
}
