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
        private Dictionary<string, List<FileEndPoint>> _endPointsByFile = 
            new Dictionary<string, List<FileEndPoint>>() { 
                {"fileName", new List<FileEndPoint>()
                {
                    new FileEndPoint("126.5.235.50", 4340)
                }},
                {"anotherFile", new List<FileEndPoint>()
                {
                    new FileEndPoint("129.53.2.513", 1234)
                }}
            };

        public string GetAll(string fileName)
        {
            List<FileEndPoint> output = new List<FileEndPoint>();

            if (_endPointsByFile.ContainsKey(fileName))
            {
                foreach (var fileEndPoint in _endPointsByFile.Values)
                {
                    
                }
            }

            string serializedOutput = JsonSerializer.Serialize(output);

            return serializedOutput;
        }

        //public int Register(string fileName)
        //{
        //    foreach (var fileEndPoint in fileEndPoints)
        //    {
        //        if (fileEndPoint.FilesInEndPoint.ContainsKey(fileName))
        //        {
        //            return 0;
        //        }
                
        //    }
        //    fileEndPoints.Add(new FileEndPoint());
            
        //}
    }
}
