#include "LightItem.h"

namespace Shelfinator
{
	namespace Runner
	{
		namespace PatternData
		{
			void LightItem::Read(BufferFile::ptr file)
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

			double LightItem::GetPercent(double time)
			{
				return (time - startTime) / (origEndTime - startTime);
			}

			double LightItem::GetSameIndexColorValue(double time)
			{
				return (time - startTime) * (endColorValue - startColorValue) / (origEndTime - startTime) + startColorValue;
			}
		}
	}
}
