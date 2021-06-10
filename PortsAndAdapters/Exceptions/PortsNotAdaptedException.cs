using System;

namespace PortsAndAdapters.Exceptions
{
    public class PortNotAdaptedException : Exception
    {
        public PortNotAdaptedException(string message) : base(message)
        {
        }
    }
}
