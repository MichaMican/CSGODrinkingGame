using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGODrinkingGameServer.Models
{
    public class CsgoGameStateDto
    {
        public ProviderDto provider { get; set; }
        public PlayerDto player { get; set; }
    }
}
