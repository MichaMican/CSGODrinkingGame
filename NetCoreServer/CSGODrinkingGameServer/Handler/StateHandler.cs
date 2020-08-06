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
        private IArduinoSerial arduino;
        public StateHandler(IArduinoSerial arduino)
        {
            this.arduino = arduino;
        }

        public void Handle(CsgoGameStateDto csgoGameState)
        {
            throw new NotImplementedException();
        }
    }
}
