using System.Collections.Concurrent;

namespace Shelfinator
{
	public class RemoteEmulator : IRemote
	{
		readonly ConcurrentQueue<RefRemoteCode> queue = new ConcurrentQueue<RefRemoteCode>();
		public RefRemoteCode GetCode() => queue.TryDequeue(out var code) ? code : RefRemoteCode.None;
		public void Add(RefRemoteCode code) => queue.Enqueue(code);
	}
}
