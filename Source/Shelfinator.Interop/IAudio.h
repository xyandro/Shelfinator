#pragma once

namespace Shelfinator
{
	namespace Interop
	{
		public interface class IAudio
		{
			void Play(System::String ^fileName);
			void Stop();
			int GetTime();
			void SetTime(int time);
		};
	}
}
