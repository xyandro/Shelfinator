#pragma once

#include <vcclr.h>
#include "../Shelfinator.Runner/IRemote.h"
#include "IRemote.h"

namespace Shelfinator
{
	namespace Interop
	{
		class Remote : public Runner::IRemote
		{
		public:
			typedef std::shared_ptr<Remote> ptr;
			static ptr Create(Interop::IRemote ^remote);
			Runner::RemoteCode GetCode();
		private:
			gcroot<Interop::IRemote^> remote;
			Remote(Interop::IRemote ^remote);
		};
	}
}
