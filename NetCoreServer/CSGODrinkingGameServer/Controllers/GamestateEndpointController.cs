using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSGODrinkingGameServer.Interfaces;
using CSGODrinkingGameServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CSGODrinkingGameServer.Controllers
{
    [ApiController]
    [Route("")]
    public class GamestateEndpointController : ControllerBase
    {
        private IStateHandler _stateHandler;
        public GamestateEndpointController(IStateHandler stateHandler)
        {
            this._stateHandler = stateHandler;
        }

        [HttpPost]
        public ActionResult HandlePost([FromBody] CsgoGameStateDto csgoGameState)
        {
            if (csgoGameState == null || csgoGameState.player.state == null || csgoGameState.map == null)
            {
                Console.WriteLine("Not ingame");
                return Ok();
            }

            Console.WriteLine(csgoGameState.map.phase);

            if (csgoGameState.map.phase == "live" || csgoGameState.map.phase == "gameover")
            {
                _stateHandler.Handle(csgoGameState);
            }
            else
            {
                Console.WriteLine("Not ingame");
            }

            return Ok();
        }
    }
}
