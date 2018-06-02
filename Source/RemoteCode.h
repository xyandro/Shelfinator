#pragma once

namespace Shelfinator
{
#define OPTIONS\
	None,\
	Play,\
	Pause,\
	Rewind,\
	FastForward,\

	enum RemoteCode { OPTIONS };

#ifdef _WIN32
	public enum class RefRemoteCode { OPTIONS };
#endif
}
