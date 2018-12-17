using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Shelfinator.Creator.PatternData;

namespace Shelfinator.Creator.Patterns
{
	class Tag : IPattern
	{
		public int PatternNumber => 39;
		const int RunAwayDistance = 20;
		static Random rand = new Random(0xdec0de);
		Layout headerLayout, bodyLayout;

		class Player
		{
			public Player()
			{
				Position = new Point(rand.Next(32), rand.Next(8) - 10);
			}

			public int Color { get; set; }
			public Point Position { get; set; }
			public Point? Destination { get; set; }

			bool tagged = true;
			public bool Tagged
			{
				get => tagged;
				set
				{
					if (tagged == value)
						return;
					tagged = value;
					Destination = null;
				}
			}
		}

		void Setup()
		{
			headerLayout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Header.png");
			bodyLayout = new Layout("Shelfinator.Creator.Patterns.Layout.Layout-Body.png");
		}

		int? GetLight(Point p, bool allowHeader = true)
		{
			int? light = null;
			if ((p.Y < 0) && (allowHeader))
				light = light ?? headerLayout.TryGetPositionLight(new Point(p.X, p.Y + 10));
			return light ?? bodyLayout.TryGetPositionLight(p);
		}

		Point NextLocation(Point location, Point dest, bool runAway = false)
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

		void MoveHunters(List<Player> hunters, List<Player> players)
		{
			while (true)
			{
				hunters = hunters.Where(hunter => !hunter.Tagged).ToList();
				players = players.Where(player => !player.Tagged).ToList();

				var ideas = hunters.SelectMany(hunter => players.Select(player => Tuple.Create(hunter, player, (hunter.Position - player.Position).LengthSquared))).OrderBy(idea => idea.Item3).ToList();
				var tagged = ideas.Where(idea => idea.Item3 <= 1).Select(idea => idea.Item2).ToList();
				if (tagged.Any())
				{
					tagged.ForEach(player => player.Tagged = true);
					continue;
				}

				var busy = new HashSet<Player>();
				foreach (var hunter in ideas.Select(idea => idea.Item1).Distinct())
				{
					var close = ideas.Where(idea => idea.Item1 == hunter).Select(idea => idea.Item2).ToList();
					var closest = close.Where(player => !busy.Contains(player)).Concat(close).First();
					busy.Add(closest);
					hunter.Position = NextLocation(hunter.Position, closest.Position);
				}

				break;
			}
		}

		void MovePlayers(List<Player> hunters, List<Player> players)
		{
			foreach (var player in players)
			{
				if (!player.Tagged)
				{
					var closest = hunters.Where(hunter => (!hunter.Tagged)).OrderBy(hunter => (player.Position - hunter.Position).LengthSquared).FirstOrDefault();
					if ((closest != null) && ((closest.Position - player.Position).Length < RunAwayDistance))
					{
						player.Position = NextLocation(player.Position, closest.Position, true);
						player.Destination = null;
						continue;
					}
				}

				if (player.Destination == null)
					while (true)
					{
						player.Destination = player.Tagged ? new Point(rand.Next(32), rand.Next(8) - 10) : new Point(rand.Next(97), rand.Next(97));
						if ((player.Destination.Value != player.Position) && (GetLight(player.Destination.Value) != null))
							break;
					}

				player.Position = NextLocation(player.Position, player.Destination.Value);
				if (player.Position == player.Destination.Value)
					player.Destination = null;
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
			var colors = new List<int> { 0xff0000, 0x00ff00, 0xffff00, 0xff00ff, 0xff8000 };

			const int TimeMultiplier = 5;
			const int HunterLockup = 1000;
			const int PlayerLockup = 500;
			const int EndingDelay = 500;

			Setup();

			var hunters = Enumerable.Range(0, numHunters).Select(x => new Player()).ToList();
			var players = Enumerable.Range(0, numPlayers).Select(x => new Player { Color = colors[x % colors.Count] }).ToList();

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
					MoveHunters(hunters, players);
					draw = true;
				}
				if (time % 6 == 0)
				{
					MovePlayers(hunters, players);
					draw = true;
				}

				if (draw)
				{
					if (GameIsOver(hunters, players))
						stopTime = stopTime ?? time + EndingDelay;

					pattern.Clear(startTime + time);
					players.ForEach(player => pattern.AddLight(GetLight(player.Position).Value, startTime + time, pattern.Absolute, Helpers.MultiplyColor(player.Tagged ? 0x0000ff : player.Color, Brightness)));
					hunters.ForEach(hunter => pattern.AddLight(GetLight(hunter.Position).Value, startTime + time, pattern.Absolute, Helpers.MultiplyColor(0xffffff, Brightness / (hunter.Tagged ? 3 : 1))));
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
