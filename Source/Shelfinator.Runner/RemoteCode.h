#pragma once

namespace Shelfinator
{
	namespace Runner
	{
#define OPTIONS\
		None,\
		Play,\
		Pause,\
		Rewind,\
		FastForward,\
		Enter,\
		Previous,\
		Next,\
		D0,\
		D1,\
		D2,\
		D3,\
		D4,\
		D5,\
		D6,\
		D7,\
		D8,\
		D9,\
		Info,\

		enum RemoteCode { OPTIONS };

#ifdef _WIN32
		public enum class RefRemoteCode { OPTIONS };
#endif
	}
}
