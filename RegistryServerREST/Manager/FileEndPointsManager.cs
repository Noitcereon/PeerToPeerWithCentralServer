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
        private static Dictionary<string, List<FileEndPoint>> _endPointsByFile =
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
            try
            {
                if (!_endPointsByFile.ContainsKey(fileName)) return "No endpoints have that file.";
                _endPointsByFile.TryGetValue(fileName, out List<FileEndPoint> output);

                string serializedOutput = JsonSerializer.Serialize(output);

                return serializedOutput;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "An error occured during the request.";
            }

        }

        public int Register(string fileName, FileEndPoint peer)
        {
            try
            {
                if (_endPointsByFile.ContainsKey(fileName))
                {
                    _endPointsByFile.TryGetValue(fileName, out var peers);
                    if (peers?.Contains(peer) == false) return 0; // peer with that file already exists.
                    peers?.Add(peer);
                    return 1; // 1 = successfully added
                }

                _endPointsByFile.Add(fileName, new List<FileEndPoint> { peer });
                return 1; // 1 = successfully added
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1; // -1 = error
            }
        }

        public int Deregister(string fileName, FileEndPoint peer)
        {
            // TODO: Deregister on REST Server.
            try
            {
                if (!_endPointsByFile.ContainsKey(fileName)) return 0; // nothing to delete.
                _endPointsByFile.TryGetValue(fileName, out var peers);
                if (peers?.Contains(peer) == false) return 0; // nothing to delete.
                peers?.Remove(peer);
                if (peers?.Count == 0)
                {
                    _endPointsByFile.Remove(fileName);
                }
                return 1; // 1 = successfully removed

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1; // -1 = error
            }
        }
    }
}
