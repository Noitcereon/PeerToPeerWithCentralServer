using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PeerToPeerLib;
using RegistryServerREST.Manager;

namespace RegistryServerREST.Controllers
{
    [Route("api/powernap/")]
    [ApiController]
    public class FileEndPointsController : ControllerBase
    {
        
        private readonly FileEndPointsManager _manager = new FileEndPointsManager();

        [HttpGet("{fileName}")]
        public string GetEndPointsThatHasFile(string fileName)
        {
            return _manager.GetEndPointsThatHasFile(fileName);
        }

        [HttpPost("{fileName}")]
        public int Register([FromQuery] string fileName, [FromBody]FileEndPoint peer)
        {
            return _manager.Register(fileName, peer);
        }

        [HttpDelete("{fileName}")]
        public int Deregister([FromQuery] string fileName, [FromBody]FileEndPoint peer)
        {
            return _manager.Deregister(fileName, peer);
        }

    }
}
