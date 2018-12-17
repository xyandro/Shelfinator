#pragma once

#ifndef __CLR_VER
#include <mutex>
#endif
#include <queue>
#ifdef _WIN32
#include <Winsock2.h>
#else
typedef int SOCKET;
#endif
#include "Pattern.h"

namespace Shelfinator
{
	namespace Runner
	{
		class Sockets
		{
		public:
			class Request
			{
			public:
				typedef std::shared_ptr<Request> ptr;
				PatternData::Pattern::ptr pattern;
				static ptr Create(PatternData::Pattern::ptr pattern);
			private:
				Request(PatternData::Pattern::ptr pattern);
			};
			typedef std::shared_ptr<Sockets> ptr;
			static Sockets::ptr Create();
			Request::ptr GetRequest();
		private:
#ifndef __CLR_VER
			std::mutex mutex;
#endif
			std::queue<Request::ptr> requests;
			Sockets();
			void ServerThread();
			void ClientThread(SOCKET socket);
		};
	}
}
