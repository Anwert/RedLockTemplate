using System;

namespace redlocktst.DistributedLockers
{
    public interface IDistributedLock : IDisposable, IAsyncDisposable
    {
    }
}