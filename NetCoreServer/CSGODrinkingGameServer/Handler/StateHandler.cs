using CSGODrinkingGameServer.Interfaces;
using CSGODrinkingGameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGODrinkingGameServer.Handler
{
    public class StateHandler : IStateHandler
    {
        private IArduinoSerial _arduino;
        public StateHandler(IArduinoSerial arduino)
        {
            this._arduino = arduino;
        }

        public void Handle(CsgoGameStateDto csgoGameState)
        {
            _arduino.writeToArduino("1");

            throw new NotImplementedException();
        }
    }
}
