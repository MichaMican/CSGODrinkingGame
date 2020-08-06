using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGODrinkingGameServer.Models.Settings
{
    public class Settings
    {
        public string ArduinoPort { get; set; }
        public List<TriggerThreshold> TriggerThresholds { get; set; }
        public double MaxPumpDuration { get; set; }
    }
}
