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
        
        private FileEndPointsManager manager = new FileEndPointsManager();
        [HttpGet("{fileName}")]

        public string GetAllPeers(string fileName)
        {
            return manager.GetAll(fileName);

        }


    }
}
