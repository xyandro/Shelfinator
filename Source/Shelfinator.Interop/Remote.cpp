#include "stdafx.h"
#include "Remote.h"

namespace Shelfinator
{
	namespace Interop
	{
		Remote::ptr Remote::Create(Interop::IRemote ^remote)
		{
			return ptr(new Remote(remote));
		}

		Remote::Remote(Interop::IRemote ^remote)
		{
			this->remote = remote;
		}

		Runner::RemoteCode Remote::GetCode()
		{
			return (Runner::RemoteCode)remote->GetCode();
		}
	}
}
