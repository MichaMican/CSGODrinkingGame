using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGODrinkingGameServer.Models
{
    public class PlayerStateDto
    {
        public int health { get; set; }
        public int armor { get; set; }
        public bool helmet { get; set; }
        public int flashed { get; set; }
        public int smoked { get; set; }
        public int burning { get; set; }
        public int money { get; set; }
        public int round_kills { get; set; }
        public int round_killhs { get; set; }
        public int equip_value { get; set; }
    }
}
