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
#include "Song.h"

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
				SongData::Song::ptr song;
				static ptr Create(SongData::Song::ptr song);
			private:
				Request(SongData::Song::ptr song);
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
