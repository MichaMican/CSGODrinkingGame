using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGODrinkingGameServer.Interfaces
{
    public interface IArduinoSerial
    {
        void writeToArduino(string value);
    }
}
