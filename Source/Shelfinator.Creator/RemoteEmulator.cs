using System.Collections.Concurrent;
using Shelfinator.Interop;

namespace Shelfinator.Creator
{
	public class RemoteEmulator : IRemote
	{
		readonly ConcurrentQueue<RemoteCode> queue = new ConcurrentQueue<RemoteCode>();
		public RemoteCode GetCode() => queue.TryDequeue(out var code) ? code : RemoteCode.None;
		public void Add(RemoteCode code) => queue.Enqueue(code);
	}
}
