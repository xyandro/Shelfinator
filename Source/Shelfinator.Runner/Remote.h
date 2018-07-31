#pragma once

#include "RemoteCode.h"

namespace Shelfinator
{
	namespace Runner
	{
#ifdef _WIN32
		public interface class IRemote
		{
			RefRemoteCode GetCode();
		};
#endif

		class Remote
		{
		public:
			typedef std::shared_ptr<Remote> ptr;

			RemoteCode GetCode();
#ifdef _WIN32
		public:
			static ptr Create(IRemote ^remote);

		private:
			gcroot<IRemote^> remote;

			Remote(IRemote ^remote);
#else
		public:
			static ptr Create();
			~Remote();

		private:
			std::deque<RemoteCode> queue;
			std::mutex mutex;

			Remote();
			void RunThread();
			void AddCode(RemoteCode code);
#endif
		};
	}
}
