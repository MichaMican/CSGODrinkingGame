using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSGODrinkingGameServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CSGODrinkingGameServer.Controllers
{
    [ApiController]
    [Route("")]
    public class GamestateEndpointController : ControllerBase
    {

        public GamestateEndpointController()
        {
        }

        [HttpPost]
        public ActionResult HandlePost([FromBody] CsgoGameStateDto csgoGameState)
        {

            Console.WriteLine("Received");

            return StatusCode(501);
        }
    }
}
