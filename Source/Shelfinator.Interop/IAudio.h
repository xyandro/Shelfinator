#pragma once

namespace Shelfinator
{
	namespace Interop
	{
		public interface class IAudio
		{
			void Open(System::String ^normalFileName, System::String ^editedFileName);
			void Close();
			void Play();
			void Stop();
			property int Time;
			property int Volume;
			property bool Edited;
			bool Playing();
			bool Finished();
		};
	}
}
