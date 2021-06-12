using System;

namespace ServicesEnforcer.Exceptions
{
    public class ServiceNotEnforcedException : Exception
    {
        public ServiceNotEnforcedException(string message) : base(message)
        {
        }
    }
}
