using System;

namespace redlocktst.DistributedLockers.Redis
{
    public class RedLockOptions
    {
        public TimeSpan Expiry { get; set; }
        
        public TimeSpan Wait { get; set; }
        
        public TimeSpan Retry { get; set; }
    }
}