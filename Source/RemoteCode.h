#pragma once

namespace Shelfinator
{
#define OPTIONS\
	None,\
	Play,\
	Pause,\

	enum RemoteCode { OPTIONS };

#ifdef _WIN32
	public enum class RefRemoteCode { OPTIONS };
#endif
}
