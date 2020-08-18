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
        private IDrinkManager _drinkManager;
        private int lastHP;
        private List<int> hittedThresholds;
        private Settings _settings;
        private bool died;
        public StateManager(IOptions<Settings> options, IDrinkManager drinkManager)
        {
            _drinkManager = drinkManager;
            _settings = options.Value;
            //This is intentionaly to high so the first post will update the lastHP to the actual HP without triggering a reset
            lastHP = 200;
            hittedThresholds = new List<int>();
            died = false;
        }

        public void Handle(CsgoGameStateDto csgoGameState)
        {
            if (csgoGameState.map.phase == "gameover")
            {
                Console.WriteLine("Reset after gameover");
                lastHP = 200;
                hittedThresholds.Clear();
                died = false;
            }
            else
            {
                if (csgoGameState.provider.steamid == csgoGameState.player.steamid)
                {
                    var usersHp = csgoGameState.player.state.health;
                    if (lastHP >= usersHp)
                    {
                        foreach (var thresh in _settings.TriggerThresholds)
                        {
                            if (!hittedThresholds.Contains(thresh.hp) && thresh.hp >= usersHp)
                            {
                                _drinkManager.registerDrinkPenalty(thresh.pumpMultiplicator * _settings.MaxPumpDuration);
                                hittedThresholds.Add(thresh.hp);
                            }
                        }

                        lastHP = usersHp;

                    }
                    else
                    {
                        //reset
                        Console.WriteLine("Resetting");
                        lastHP = csgoGameState.player.state.health;
                        hittedThresholds.Clear();
                        died = false;
                    }
                }
                else
                {
                    if (!died)
                    {
                        foreach (var thresh in _settings.TriggerThresholds)
                        {
                            //Gives the player ALL remaining penalties
                            if (!hittedThresholds.Contains(thresh.hp))
                            {
                                _drinkManager.registerDrinkPenalty(thresh.pumpMultiplicator * _settings.MaxPumpDuration);
                                hittedThresholds.Add(thresh.hp);
                            }
                        }
                        died = true;
                        //This will make sure that the a reset is performed when the round restarts
                        lastHP = -1;
                    }
                }
            }
        }
    }
}
