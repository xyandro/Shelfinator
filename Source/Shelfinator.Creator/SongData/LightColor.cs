using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Shelfinator.Creator.SongData
{
	class LightColor
	{
		readonly int minValue;
		readonly int maxValue;
		readonly List<IReadOnlyList<int>> colors = new List<IReadOnlyList<int>>();

		public LightColor(int minValue, int maxValue, List<IReadOnlyList<int>> colors)
		{
			this.minValue = minValue;
			this.maxValue = maxValue;
			this.colors = colors;
		}

		public LightColor(int color) : this(0, 0, new List<IReadOnlyList<int>> { new List<int> { color } }) { }

		public LightColor(params IReadOnlyList<int>[] colors) : this(0, 0, colors.ToList()) { }

		public LightColor(List<IReadOnlyList<int>> colors) : this(0, 0, colors) { }

		public LightColor(int minValue, int maxValue, params IReadOnlyList<int>[] colors) : this(minValue, maxValue, colors.ToList()) { }

		public void Save(BinaryWriter output)
		{
			output.Write(minValue);
			output.Write(maxValue);
			output.Write(colors.Count);
			foreach (var palette in colors)
			{
				output.Write(palette.Count);
				foreach (var color in palette)
					output.Write(color);
			}
		}

		public override string ToString() => $"Value: {minValue}-{maxValue}, {colors.Count} indexes";
	}
}
