using System.Linq;

namespace Shelfinator.Patterns
{
	static class Layout
	{
		public const int LIGHTSPERSQUARE = 19;
		public const int SQUARESX = 5;
		public const int SQUARESY = 5;
		public const int SQUARES = SQUARESX * SQUARESY;
		public const int SPACEBETWEENROWS = 0;

		public const int WIDTH = (LIGHTSPERSQUARE + SPACEBETWEENROWS) * SQUARESX + SPACEBETWEENROWS + 2;
		public const int HEIGHT = (LIGHTSPERSQUARE + SPACEBETWEENROWS) * SQUARESY + SPACEBETWEENROWS + 2;

		static readonly bool[,] Valid;

		static Layout()
		{
			Valid = new bool[WIDTH, HEIGHT];

			for (var squareX = 0; squareX < SQUARESX; ++squareX)
				for (var squareY = 0; squareY < SQUARESY; ++squareY)
				{
					GetSquare(squareX, squareY, out var x1, out var y1, out var x2, out var y2);
					for (var x = x1; x < x2; ++x)
						Valid[x, y1] = Valid[x, y2 - 1] = true;
					for (var y = y1; y < y2; ++y)
						Valid[x1, y] = Valid[x2 - 1, y] = true;
				}

			for (var x = 0; x < WIDTH; ++x)
				Valid[x, 0] = Valid[x, HEIGHT - 1] = true;
			for (var y = 0; y < HEIGHT; ++y)
				Valid[0, y] = Valid[WIDTH - 1, y] = true;
		}

		static public void GetSquare(int x, int y, out int x1, out int y1, out int x2, out int y2)
		{
			x1 = GetX1(x);
			y1 = GetY1(y);
			x2 = GetX2(x);
			y2 = GetY2(y);
		}

		static public int GetX1(int x) => (LIGHTSPERSQUARE + SPACEBETWEENROWS) * x + SPACEBETWEENROWS + 1;
		static public int GetY1(int y) => (LIGHTSPERSQUARE + SPACEBETWEENROWS) * y + SPACEBETWEENROWS + 1;
		static public int GetX2(int x) => (LIGHTSPERSQUARE + SPACEBETWEENROWS) * (x + 1) + 1;
		static public int GetY2(int y) => (LIGHTSPERSQUARE + SPACEBETWEENROWS) * (y + 1) + 1;


		static public bool IsValid(int x, int y)
		{
			if ((x < 0) || (y < 0) || (x >= WIDTH) || (y >= HEIGHT))
				return false;
			return Valid[x, y];
		}
	}
}
