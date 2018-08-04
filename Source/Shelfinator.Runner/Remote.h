#pragma once

#include <memory>
#include <mutex>
#include <queue>
#include "RemoteCode.h"

namespace Shelfinator
{
	namespace Runner
	{
		class Remote
		{
		public:
			typedef std::shared_ptr<Remote> ptr;
			static ptr Create();
			~Remote();
			RemoteCode GetCode();
		private:
			std::deque<RemoteCode> queue;
			std::mutex mutex;
			Remote();
			void RunThread();
			void AddCode(RemoteCode code);
		};
	}
}
