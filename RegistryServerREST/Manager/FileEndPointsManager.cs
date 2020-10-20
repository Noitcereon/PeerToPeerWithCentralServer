using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using PeerToPeerLib;

namespace RegistryServerREST.Manager
{
    public class FileEndPointsManager
    {
        private List<FileEndPoint> fileEndPoints = new List<FileEndPoint>
        {
            new FileEndPoint("123.3.15.312", 4646, true),
            new FileEndPoint("123.32.51.1", 4000, true)
        };
        public string GetAll(string fileName)
        {
            List<FileEndPoint> output = new List<FileEndPoint>();

            foreach (var fileEndPoint in fileEndPoints)
            {
                if (fileEndPoint.FilesInEndPoint.ContainsKey(fileName))
                {
                    output.Add(fileEndPoint);
                }
            }

            string serializedOutput = JsonSerializer.Serialize(output);

            return serializedOutput;
        }
    }
}
