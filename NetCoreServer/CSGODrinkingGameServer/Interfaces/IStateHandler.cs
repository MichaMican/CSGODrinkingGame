using CSGODrinkingGameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGODrinkingGameServer.Interfaces
{
    public interface IStateHandler
    {
        void Handle(CsgoGameStateDto csgoGameState);
    }
}
