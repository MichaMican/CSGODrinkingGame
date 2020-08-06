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
        private IStateHandler stateHandler;
        public GamestateEndpointController(IStateHandler stateHandler)
        {
            this.stateHandler = stateHandler;
        }

        [HttpPost]
        public ActionResult HandlePost([FromBody] CsgoGameStateDto csgoGameState)
        {

            stateHandler.Handle(csgoGameState);

            Console.WriteLine("Received");

            return StatusCode(501);
        }
    }
}
