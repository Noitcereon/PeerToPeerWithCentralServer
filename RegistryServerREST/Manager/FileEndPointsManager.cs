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
                {"testFile", new List<FileEndPoint>()
                {
                    new FileEndPoint("126.5.235.50", 4340),
                    new FileEndPoint("525.519.82.1", 9993)
                }},
                {"anotherFile", new List<FileEndPoint>()
                {
                    new FileEndPoint("129.53.2.513", 1234)
                }}
            };

        public string GetEndPointsThatHasFile(string fileName)
        {

            if (_endPointsByFile.ContainsKey(fileName))
            {
                _endPointsByFile.TryGetValue(fileName, out List<FileEndPoint> output);

                string serializedOutput = JsonSerializer.Serialize(output);

                return serializedOutput;
            }
            else
            {
                return "No endpoints have that file.";
            }
        }

        public int Register(string fileName)
        {
            throw new NotImplementedException();
        }

        public int Deregister()
        {
            throw new NotImplementedException();
        }
    }
}
