#pragma once

namespace Shelfinator
{
	namespace Interop
	{
		public interface class IAudio
		{
			void Open(System::String ^fileName);
			void Close();
			void Play(int time);
			void Stop();
			int GetTime();
		};
	}
}
