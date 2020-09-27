using System;

namespace redlocktst.Exceptions
{
    public class RedLockAcquiringException : Exception
    {
        public RedLockAcquiringException() : base("Lock acquiring failed")
        {
        }
    }
}