using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGODrinkingGameServer.Exceptions
{
    public class PortClosedException : Exception
    {
        public PortClosedException() : base("Port is closed") { }
        public PortClosedException(string message) : base(message) { }
        public PortClosedException(string message, System.Exception inner) : base(message, inner) { }
    }
}
