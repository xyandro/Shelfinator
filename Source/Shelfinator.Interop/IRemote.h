#pragma once

#include "RemoteCode.h"

namespace Shelfinator
{
	namespace Interop
	{
		public interface class IRemote
		{
			RemoteCode GetCode();
		};
	}
}
