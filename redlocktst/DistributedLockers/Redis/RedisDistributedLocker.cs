using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RedLockNet.SERedis;
using redlocktst.Exceptions;

namespace redlocktst.DistributedLockers.Redis
{
    public class RedisDistributedLocker : IDistributedLocker
    {
        private readonly RedLockFactory _redLockFactory;
        private readonly RedLockOptions _options;

        public RedisDistributedLocker(RedLockFactory redLockFactory, IOptions<RedLockOptions> options)
        {
            _redLockFactory = redLockFactory;
            _options = options.Value;
        }
        
        public IDistributedLock AcquireLock(string resource)
        {
            var redLock = _redLockFactory.CreateLock(resource, _options.Expiry, _options.Wait, _options.Retry);
            return redLock.IsAcquired
                ? new RedLockDecorator(redLock)
                : throw new RedLockAcquiringException();
        }

        public async Task<IDistributedLock> AcquireLockAsync(string resource)
        {
            var redLock = await _redLockFactory.CreateLockAsync(resource, _options.Expiry, _options.Wait, _options.Retry);
            return redLock.IsAcquired
                ? new RedLockDecorator(redLock)
                : throw new RedLockAcquiringException();
        }
    }
}