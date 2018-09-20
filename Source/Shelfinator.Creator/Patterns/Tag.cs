using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Shelfinator.Creator.Patterns
{
	class Tag : IPattern
	{
		public int PatternNumber => 39;
		const int HeaderOffsetX = 32;
		const int HeaderOffsetY = -8;
		const int RunAwayDistance = 20;
		static Random rand = new Random(0xdec0de);
		Layout headerLayout, bodyLayout;

		class Player
		{
			public Player()
			{
				position = new Point(rand.Next(32) + HeaderOffsetX, rand.Next(8) + HeaderOffsetY);
			}

			int color;
			public int Color
			{
				get => Tagged ? 0 : color;
				set => color = value;
			}

			Point position;
			public Point Position => position;

			bool tagged = true;
			public bool Tagged
			{
				get => tagged;
				set
				{
					if (tagged == value)
						return;
					tagged = value;
					destination = null;
				}
			}

			List<Point> destination = null;

			public void SetupWait(int time) => destination = Enumerable.Repeat(Position, time).ToList();

			public void SetDestination(List<Point> destination) => this.destination = destination;

			public Point NextPosition => destination.First();

			public bool HasDestination => destination != null;

			public void AcceptNext()
			{
				position = NextPosition;
				destination.RemoveAt(0);
				if (!destination.Any())
					destination = null;
			}
		}

		void Setup()
		{
			headerLayout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Header.png");
			bodyLayout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");
		}

		int? GetLight(Point p) => p.Y < 0 ? headerLayout.TryGetPositionLight(new Point(p.X - HeaderOffsetX, p.Y - HeaderOffsetY)) : bodyLayout.TryGetPositionLight(p);

		List<Point> CalcPoint(List<Point> curPoints, Point end, bool[] used)
		{
			var start = curPoints[curPoints.Count - 1];
			if (start == end)
				return curPoints;

			var points = GetPoints(start, end);
			foreach (var point in points)
			{
				var light = GetLight(point);
				if ((!light.HasValue) || (used[light.Value]))
					continue;

				used[light.Value] = true;
				curPoints.Add(point);
				var result = CalcPoint(curPoints, end, used);
				if (result != null)
					return result;
				used[light.Value] = false;
				curPoints.RemoveAt(curPoints.Count - 1);
			}

			return null;
		}

		List<Point> GetPoints(Point start, Point end)
		{
			var check = new List<Vector>();
			var useX = Math.Abs(end.X - start.X) > Math.Abs(end.Y - start.Y);
			for (var pass = 0; pass < 2; ++pass)
			{
				Vector v1, v2;
				if (useX)
				{
					v1 = new Vector(-1, 0);
					v2 = new Vector(1, 0);
				}
				else
				{
					v1 = new Vector(0, -1);
					v2 = new Vector(0, 1);
				}

				var swap = ((useX) && (end.X > start.X)) || (!useX) && (end.Y > start.Y);
				if (swap)
				{
					var val = v1;
					v1 = v2;
					v2 = val;
				}

				var index = check.Any() ? 1 : 0;
				check.Insert(index, v2);
				check.Insert(index, v1);

				useX = !useX;
			}

			return check.Select(v => start + v).ToList();
		}

		List<Point> CalcPath(Point start, Point end)
		{
			var points = CalcPoint(new List<Point> { start }, end, new bool[2440]);
			if (points == null)
				throw new Exception("Unable to calculate path");
			points.RemoveAt(0);
			return points;
		}

		void SetupHunterPaths(List<Player> hunters, List<Player> players)
		{
			hunters.ForEach(hunter => hunter.SetDestination(new List<Point> { hunter.Position }));

			do
			{
				hunters = hunters.Where(hunter => !hunter.Tagged).ToList();
				players = players.Where(player => !player.Tagged).ToList();

				if ((!hunters.Any()) || (!players.Any()))
					break;

				var ideas = hunters.SelectMany(hunter => players.Select(player => Tuple.Create(hunter, player, (hunter.Position - player.Position).LengthSquared))).OrderBy(idea => idea.Item3).ToList();
				var tagged = ideas.Where(idea => idea.Item3 <= 1).Select(idea => idea.Item2).ToList();
				if (tagged.Any())
				{
					tagged.ForEach(player => player.Tagged = true);
					continue;
				}

				var busy = new HashSet<Player>();
				foreach (var hunter in hunters)
				{
					var close = ideas.Where(idea => idea.Item1 == hunter).Select(idea => idea.Item2).ToList();
					var closest = close.Where(player => !busy.Contains(player)).Concat(close).First();
					busy.Add(closest);
					hunter.SetDestination(CalcPath(hunter.Position, closest.Position));
				}
			} while (false);
		}

		void SetupPlayerPaths(List<Player> hunters, List<Player> players)
		{
			foreach (var player in players)
			{
				if (!player.Tagged)
				{
					var closest = hunters.Where(hunter => (!hunter.Tagged)).OrderBy(hunter => (player.Position - hunter.Position).LengthSquared).FirstOrDefault();
					if ((closest != null) && ((closest.Position - player.Position).Length < RunAwayDistance))
					{
						var point = GetPoints(player.Position, closest.Position).Where(p => (p.Y >= 0) && (GetLight(p).HasValue)).LastOrDefault();
						if (point != null)
							player.SetDestination(new List<Point> { point });
					}
				}

				if (!player.HasDestination)
				{
					while (true)
					{
						Point dest;

						if (!player.Tagged)
							dest = new Point(rand.Next(97), rand.Next(97));
						else if (player.Position.Y >= 0)
							dest = new Point(Math.Max(HeaderOffsetX, Math.Min(player.Position.X, 31 + HeaderOffsetX)), -1);
						else
							dest = new Point(rand.Next(32) + HeaderOffsetX, rand.Next(8) + HeaderOffsetY);

						if (dest == player.Position)
							continue;

						if (GetLight(dest) == null)
							continue;

						var destination = CalcPath(player.Position, dest);
						if (!destination.Any())
							continue;

						player.SetDestination(destination);
						break;
					}
				}
			}
		}

		bool GameIsOver(List<Player> hunters, List<Player> players)
		{
			if (hunters.Any(hunter => hunter.Tagged))
				return false;
			if (players.Any(player => (!player.Tagged) || (player.Position.Y >= 0)))
				return false;
			return true;
		}

		public int RenderGame(Pattern pattern, int numHunters, int numPlayers, int startTime)
		{
			const double Brightness = 1f / 16;

			const int TimeMultiplier = 5;
			const int HunterLockup = 800;
			const int PlayerLockup = 500;
			const int EndingDelay = 500;

			Setup();

			var color = new LightColor(0, 6, new List<int> { 0xffffff, 0x0000ff, 0xffff00, 0xff8000, 0xff0000, 0x80ff00, 0x00ff00 }.Multiply(Brightness).ToList());

			var hunters = Enumerable.Range(0, numHunters).Select(x => new Player()).ToList();
			var players = Enumerable.Range(0, numPlayers).Select(x => new Player { Color = rand.Next(5) + 2 }).ToList();

			var time = 0;
			var stopTime = default(int?);
			while (true)
			{
				if (time == HunterLockup)
					hunters.ForEach(hunter => hunter.Tagged = false);
				if (time == PlayerLockup)
					players.ForEach(player => player.Tagged = false);

				var draw = false;
				if (time % 4 == 0)
				{
					SetupHunterPaths(hunters, players);
					hunters.ForEach(hunter => hunter.AcceptNext());
					draw = true;
				}
				if (time % 6 == 0)
				{
					SetupPlayerPaths(hunters, players);
					players.ForEach(player => player.AcceptNext());
					draw = true;
				}

				if (draw)
				{
					if (GameIsOver(hunters, players))
						stopTime = stopTime ?? time + EndingDelay;

					pattern.Clear(startTime + time);
					players.ForEach(player => pattern.AddLight(GetLight(player.Position).Value, startTime + time, color, player.Tagged ? 1 : player.Color));
					hunters.ForEach(hunter => pattern.AddLight(GetLight(hunter.Position).Value, startTime + time, color, 0));
				}

				++time;
				if (time == stopTime)
					break;
			}

			pattern.AddLightSequence(startTime, startTime + time, time * TimeMultiplier);
			return startTime + time;
		}

		public Pattern Render()
		{
			var pattern = new Pattern();
			var startTime = 0;
			startTime = RenderGame(pattern, 1, 1, startTime);
			startTime = RenderGame(pattern, 1, 5, startTime);
			startTime = RenderGame(pattern, 2, 32, startTime);
			startTime = RenderGame(pattern, 8, 256, startTime);
			return pattern;
		}
	}
}
