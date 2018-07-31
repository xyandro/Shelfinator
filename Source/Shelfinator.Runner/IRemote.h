#pragma once

#include "RemoteCode.h"

namespace Shelfinator
{
	namespace Runner
	{
		class IRemote
		{
		public:
			typedef std::shared_ptr<IRemote> ptr;
			virtual RemoteCode GetCode() = 0;
		};
	}
}
