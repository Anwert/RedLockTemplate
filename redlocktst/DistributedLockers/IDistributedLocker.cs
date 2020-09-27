using System.Threading.Tasks;

namespace redlocktst.DistributedLockers
{
    public interface IDistributedLocker
    {
        IDistributedLock AcquireLock(string resource);
        
        Task<IDistributedLock> AcquireLockAsync(string resource);
    }
}