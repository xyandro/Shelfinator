using System.Collections.Generic;
using System.Linq;

namespace Shelfinator.Patterns
{
	class Pattern
	{
		List<LightData> lights = new List<LightData>();
		public List<LightData> Lights => lights.OrderBy(light => light.StartTime).ThenBy(light => light.EndTime).ToList();
		Dictionary<int, Dictionary<int, List<LightData>>> lightsByPosition = new Dictionary<int, Dictionary<int, List<LightData>>>();

		public void AddLight(LightData lightData)
		{
			if (!Layout.IsValid(lightData.X, lightData.Y))
				return;

			lights.Add(lightData);
			if (!lightsByPosition.ContainsKey(lightData.X))
				lightsByPosition[lightData.X] = new Dictionary<int, List<LightData>>();
			if (!lightsByPosition[lightData.X].ContainsKey(lightData.Y))
				lightsByPosition[lightData.X][lightData.Y] = new List<LightData>();
			lightsByPosition[lightData.X][lightData.Y].Add(lightData);
		}
	}
}
