#include "Pattern.h"

#include "Helpers.h"

namespace Shelfinator
{
	namespace Runner
	{
		Pattern::ptr Pattern::Read(std::string fileName)
		{
			return ptr(new Pattern(BufferFile::Open(fileName)));
		}

		Pattern::Pattern(BufferFile::ptr file)
		{
			ReadLights(file);
			lightSequences.Read(file, length);
			ReadColors(file);
			paletteSequences.Read(file);
		}

		void Pattern::ReadLights(BufferFile::ptr file)
		{
			lightData.resize(file->GetInt());
			for (auto ctr = 0; ctr < lightData.size(); ++ctr)
				lightData[ctr].Read(file);
		}

		void Pattern::Light::LightItem::Read(BufferFile::ptr file)
		{
			startTime = file->GetInt();
			endTime = file->GetInt();
			origEndTime = file->GetInt();
			startColorIndex = file->GetInt();
			startColorValue = file->GetInt();
			endColorIndex = file->GetInt();
			endColorValue = file->GetInt();
			intermediates = file->GetBool();
		}

		double Pattern::Light::LightItem::GetPercent(double time)
		{
			return (time - startTime) / (origEndTime - startTime);
		}

		double Pattern::Light::LightItem::GetSameIndexColorValue(double time)
		{
			return (time - startTime) * (endColorValue - startColorValue) / (origEndTime - startTime) + startColorValue;
		}

		void Pattern::Light::Read(BufferFile::ptr file)
		{
			lights.resize(file->GetInt());
			for (auto ctr = 0; ctr < lights.size(); ++ctr)
				lights[ctr].Read(file);
		}

		Pattern::Light::LightItem &Pattern::Light::LightAtTime(double lightTime)
		{
			while (lightTime < lights[current].startTime)
				--current;
			while (lightTime >= lights[current].endTime)
				++current;
			return lights[current];
		}

		void Pattern::LightSequences::LightSequence::Read(BufferFile::ptr file, int &length)
		{
			lightStartTime = file->GetInt();
			lightEndTime = file->GetInt();
			duration = file->GetInt();
			auto repeat = file->GetInt();
			startTime = length;
			endTime = length = length + duration * repeat;
		}

		double Pattern::LightSequences::LightSequence::GetLightTime(int time)
		{
			return Helpers::FPart(((double)time - startTime) / duration) * (lightEndTime - lightStartTime) + lightStartTime;
		}

		void Pattern::LightSequences::Read(BufferFile::ptr file, int &length)
		{
			lightSequences.resize(file->GetInt());
			length = 0;
			for (auto ctr = 0; ctr < lightSequences.size(); ++ctr)
				lightSequences[ctr].Read(file, length);
		}

		Pattern::LightSequences::LightSequence & Pattern::LightSequences::SequenceAtTime(int time)
		{
			while (time < lightSequences[current].startTime)
				--current;
			while (time >= lightSequences[current].endTime)
				++current;
			return lightSequences[current];
		}

		void Pattern::LightColor::Read(BufferFile::ptr file)
		{
			minValue = file->GetInt();
			maxValue = file->GetInt();
			colors.resize(file->GetInt());
			for (auto paletteCtr = 0; paletteCtr < colors.size(); ++paletteCtr)
			{
				colors[paletteCtr].resize(file->GetInt());
				for (auto colorCtr = 0; colorCtr < colors[paletteCtr].size(); ++colorCtr)
					colors[paletteCtr][colorCtr] = file->GetInt();
			}
		}

		void Pattern::LightColor::GradientColor(double value, int index, double &red, double &green, double &blue)
		{
			index = index % colors.size();
			Helpers::GradientColor(value, minValue, maxValue, colors[index].data(), (int)colors[index].size(), red, green, blue);
		}

		void Pattern::ReadColors(BufferFile::ptr file)
		{
			lightColors.resize(file->GetInt());
			for (auto ctr = 0; ctr < lightColors.size(); ++ctr)
				lightColors[ctr].Read(file);
		}

		void Pattern::PaletteSequences::Read(BufferFile::ptr file)
		{
			paletteSequences.resize(file->GetInt());
			for (auto ctr = 0; ctr < paletteSequences.size(); ++ctr)
				paletteSequences[ctr].Read(file);
		}

		void Pattern::PaletteSequences::PaletteSequence::Read(BufferFile::ptr file)
		{
			startTime = file->GetInt();
			endTime = file->GetInt();
			startPaletteIndex = file->GetInt();
			endPaletteIndex = file->GetInt();
		}

		double Pattern::PaletteSequences::PaletteSequence::GetPercent(int time)
		{
			return startPaletteIndex == endPaletteIndex ? 0 : ((double)time - startTime) / (endTime - startTime);
		}

		Pattern::PaletteSequences::PaletteSequence &Pattern::PaletteSequences::SequenceAtTime(int time)
		{
			while (time < paletteSequences[current].startTime)
				--current;
			while (time >= paletteSequences[current].endTime)
				++current;
			return paletteSequences[current];
		}

		Lights::ptr Pattern::GetLights(int time)
		{
			if (time < 0)
				return nullptr;

			auto lights = Lights::Create(2440);

			auto paletteSequence = paletteSequences.SequenceAtTime(time);
			auto palettePercent = paletteSequence.GetPercent(time);

			auto lightTime = lightSequences.SequenceAtTime(time).GetLightTime(time);

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

				lights->SetLight(lightCtr, Helpers::CombineColor(r, g, b));
			}

			return lights;
		}

		int Pattern::GetLength()
		{
			return length;
		}
	}
}
