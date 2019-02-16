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
			property int Time;
			property int Volume;
			bool Playing();
			bool Finished();
		};
	}
}
