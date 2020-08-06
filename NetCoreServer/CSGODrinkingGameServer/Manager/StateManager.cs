using CSGODrinkingGameServer.Interfaces;
using CSGODrinkingGameServer.Models;
using CSGODrinkingGameServer.Models.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSGODrinkingGameServer.Manager
{
    public class StateManager : IStateHandler
    {
        private IArduinoSerial _arduino;
        private IDrinkManager _drinkManager;
        private int lastHP;
        private int lastTriggerId;
        private Settings _settings;
        public StateManager(IOptions<Settings> options, IArduinoSerial arduino, IDrinkManager drinkManager)
        {
            _drinkManager = drinkManager;
            _settings = options.Value;
            _arduino = arduino;
            //This is intentionaly to high so the first post will update the lastHP to the actual HP without triggering a reset
            lastHP = 200;
            lastTriggerId = -1;
        }

        public void Handle(CsgoGameStateDto csgoGameState)
        {

            if(csgoGameState.provider.steamid == csgoGameState.player.steamid)
            {
                var usersHp = csgoGameState.player.state.health;
                if (lastHP >= usersHp)
                {
                    for(int i = lastTriggerId + 1; i<_settings.TriggerThresholds.Count; i++)
                    {
                        var thresh = _settings.TriggerThresholds[i];
                        if(thresh.hp > usersHp)
                        {
                            _drinkManager.registerDrinkPenalty(thresh.pumpMultiplicator * _settings.MaxPumpDuration);
                            lastTriggerId++;
                        }
                    }
                } 
                else
                {
                    lastHP = csgoGameState.player.state.health;
                    lastTriggerId = -1;
                }
            } 
            else
            {
                //figure out if player is dead (RIP)
            }

            _arduino.writeToArduino("1");

        }
    }
}
