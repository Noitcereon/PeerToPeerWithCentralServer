using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RegistryServerREST.Controllers
{
    [ApiController]
    [Route("api/powernap/")]
    public class FileEndPointsController : ControllerBase
    {
        private List<FileEndPoint> test = new List<FileEndPoint>
        {
            new FileEndPoint("123.")
        };
        public string GetAllPeers()
        {
            
            
        }
    }
}
