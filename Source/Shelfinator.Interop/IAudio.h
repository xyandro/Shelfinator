#pragma once

namespace Shelfinator
{
	namespace Interop
	{
		public interface class IAudio
		{
			void Open(System::String ^fileName);
			void Close();
			void Play();
			void Stop();
			int GetTime();
			void SetTime(int time);
			bool Playing();
			bool Finished();
		};
	}
}
