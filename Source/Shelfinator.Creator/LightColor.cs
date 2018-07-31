using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Shelfinator.Creator
{
	class LightColor
	{
		readonly int minValue;
		readonly int maxValue;
		readonly List<List<int>> colors = new List<List<int>>();

		public LightColor(int minValue, int maxValue, List<List<int>> colors)
		{
			this.minValue = minValue;
			this.maxValue = maxValue;
			this.colors = colors;
		}

		public LightColor(int color) : this(0, 0, new List<List<int>> { new List<int> { color } }) { }

		public LightColor(params List<int>[] colors) : this(0, 0, colors.ToList()) { }

		public LightColor(int minValue, int maxValue, params List<int>[] colors) : this(minValue, maxValue, colors.ToList()) { }

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
