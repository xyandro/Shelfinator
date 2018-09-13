﻿#include "Sockets.h"

#include <thread>

namespace Shelfinator
{
	namespace Runner
	{
		Sockets::Request::ptr Sockets::Request::Create(Pattern::ptr pattern)
		{
			return ptr(new Request(pattern));
		}

		Sockets::Request::Request(Pattern::ptr pattern)
		{
			this->pattern = pattern;
		}

		Sockets::ptr Sockets::Create()
		{
			return ptr(new Sockets());
		}

		Sockets::Sockets()
		{
			WSADATA wsaData = { 0 };
			WSAStartup(MAKEWORD(2, 2), &wsaData);

			std::thread(&Sockets::ServerThread, this).detach();
		}

		void Sockets::ServerThread()
		{
			auto sockfd = socket(AF_INET, SOCK_STREAM, 0);
			if (sockfd == INVALID_SOCKET)
				throw "Unable to create socket";

			int reuse = 1;
			if (setsockopt(sockfd, SOL_SOCKET, SO_REUSEADDR, (const char*)&reuse, sizeof(reuse)) == SOCKET_ERROR)
				throw "setsockopt(SO_REUSEADDR) failed";

#ifdef SO_REUSEPORT
			if (setsockopt(sockfd, SOL_SOCKET, SO_REUSEPORT, (const char*)&reuse, sizeof(reuse)) == SOCKET_ERROR)
				throw "setsockopt(SO_REUSEPORT) failed";
#endif

			sockaddr_in addr;
			memset(&addr, 0, sizeof(addr));
			addr.sin_family = AF_INET;
			addr.sin_addr.s_addr = INADDR_ANY;
			addr.sin_port = htons((u_short)7435);
			if (bind(sockfd, (sockaddr*)&addr, sizeof(addr)) == SOCKET_ERROR)
				throw "Failed to bind to socket";

			if (listen(sockfd, 10) == SOCKET_ERROR)
				throw "Failed to listen to socket";

			while (true)
			{
				int addrSize = sizeof(addr);
				auto client = accept(sockfd, (sockaddr*)&addr, &addrSize);
				if (client == INVALID_SOCKET)
					throw "Failed to accept socket";
				std::thread(&Sockets::ClientThread, this, client).detach();
			}
		}

		void Sockets::ClientThread(SOCKET socket)
		{
			try
			{
				char *buffer = nullptr;
				auto size = 0;
				auto used = 0;
				while (true)
				{
					if (used < size)
					{
						auto block = recv(socket, buffer + used, size - used, 0);
						if (block == 0)
							break;
						if (block == SOCKET_ERROR)
							throw "Failed to recv";
						used += block;
					}

					auto newSize = size;
					if (size == 0)
						newSize = sizeof(int);
					else if (used == size)
					{
						if (size == sizeof(int))
							newSize = *(int*)buffer;
						else
						{
							// Got message
							auto pattern = Pattern::Read(BufferFile::Create(buffer + sizeof(int), size - sizeof(int)));
							auto request = Request::Create(pattern);

							{
								std::unique_lock<std::mutex> lock(mutex);
								requests.push(request);
							}

							newSize = sizeof(int);
							used = 0;
						}
					}

					if (size != newSize)
					{
						buffer = (char*)realloc(buffer, newSize);
						size = newSize;
					}
				}
			}
			catch (...)
			{
			}
		}

		Sockets::Request::ptr Sockets::GetRequest()
		{
			std::unique_lock<std::mutex> lock(mutex);
			Request::ptr request;
			if (requests.size() != 0)
			{
				request = requests.front();
				requests.pop();
			}
			return request;
		}
	}
}