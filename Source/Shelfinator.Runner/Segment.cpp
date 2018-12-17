#include "Segment.h"

#include "Helpers.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace PatternData
		{
			void Segment::Read(BufferFile::ptr file)
			{
				ReadLights(file);
				ReadColors(file);
			}

			void Segment::ReadLights(BufferFile::ptr file)
			{
				lightData.resize(file->GetInt());
				for (auto ctr = 0; ctr < lightData.size(); ++ctr)
					lightData[ctr].Read(file);
			}

			void Segment::ReadColors(BufferFile::ptr file)
			{
				lightColors.resize(file->GetInt());
				for (auto ctr = 0; ctr < lightColors.size(); ++ctr)
					lightColors[ctr].Read(file);
			}

			void Segment::SetLights(double lightTime, double brightness, Lights::ptr lights, PaletteSequence &paletteSequence, double palettePercent)
			{
				for (auto lightCtr = 0; lightCtr < lightData.size(); ++lightCtr)
				{
					auto light = lightData[lightCtr].LightAtTime(lightTime);

					auto sameIndexColorValue = light.GetSameIndexColorValue(lightTime);

					double scr, scg, scb;
					if (light.startColorIndex == -1)
						Helpers::SplitColor(light.startColorValue, scr, scg, scb);
					else
					{
						double spscr, spscg, spscb, epscr, epscg, epscb;
						auto value = light.intermediates ? sameIndexColorValue : light.startColorValue;
						lightColors[light.startColorIndex].GradientColor(value, paletteSequence.startPaletteIndex, spscr, spscg, spscb);
						lightColors[light.startColorIndex].GradientColor(value, paletteSequence.endPaletteIndex, epscr, epscg, epscb);
						Helpers::GradientColor(spscr, spscg, spscb, epscr, epscg, epscb, palettePercent, scr, scg, scb);
					}

					double ecr, ecg, ecb;
					if (light.endColorIndex == -1)
						Helpers::SplitColor(light.endColorValue, ecr, ecg, ecb);
					else
					{
						double specr, specg, specb, epecr, epecg, epecb;
						auto value = light.intermediates ? sameIndexColorValue : light.endColorValue;
						lightColors[light.endColorIndex].GradientColor(value, paletteSequence.startPaletteIndex, specr, specg, specb);
						lightColors[light.endColorIndex].GradientColor(value, paletteSequence.endPaletteIndex, epecr, epecg, epecb);
						Helpers::GradientColor(specr, specg, specb, epecr, epecg, epecb, palettePercent, ecr, ecg, ecb);
					}

					double r, g, b;
					Helpers::GradientColor(scr, scg, scb, ecr, ecg, ecb, light.GetPercent(lightTime), r, g, b);

					Helpers::MultiplyColor(r, g, b, brightness, r, g, b);

					lights->SetLight(lightCtr, Helpers::CombineColor(r, g, b));
				}
			}
		}
	}
}
