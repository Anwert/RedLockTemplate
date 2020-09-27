using System.Threading.Tasks;
using RedLockNet;

namespace redlocktst.DistributedLockers.Redis
{
    public class RedLockDecorator : IDistributedLock
    {
        private readonly IRedLock _redLock;

        public RedLockDecorator(IRedLock redLock)
        {
            _redLock = redLock;
        }

        public void Dispose()
        {
            _redLock.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            _redLock.Dispose();
            return default;
        }
    }
}