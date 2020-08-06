using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGODrinkingGameServer.Models
{
    public class PlayerDto
    {
        public string steamid { get; set; }
        public string name { get; set; }
        public PlayerStateDto state { get; set; }
    }
}
