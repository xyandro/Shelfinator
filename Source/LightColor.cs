using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Shelfinator
{
	class LightColor
	{
		readonly int minValue;
		readonly int maxValue;
		readonly List<List<PixelColor>> colors = new List<List<PixelColor>>();

		public LightColor(int minValue, int maxValue, List<List<PixelColor>> colors)
		{
			this.minValue = minValue;
			this.maxValue = maxValue;
			this.colors = colors;
		}

		public LightColor(PixelColor color) : this(0, 0, new List<List<PixelColor>> { new List<PixelColor> { color } }) { }

		public LightColor(params List<PixelColor>[] colors) : this(0, 0, colors.ToList()) { }

		public LightColor(int minValue, int maxValue, params List<PixelColor>[] colors) : this(minValue, maxValue, colors.ToList()) { }

		public void Save(BinaryWriter output)
		{
			output.Write(minValue);
			output.Write(maxValue);
			output.Write(colors.Count);
			foreach (var pixelColors in colors)
			{
				output.Write(pixelColors.Count);
				foreach (var pixelColor in pixelColors)
					output.Write(pixelColor.Color);
			}
		}

		public override string ToString() => $"Value: {minValue}-{maxValue}, {colors.Count} indexes";
	}
}
