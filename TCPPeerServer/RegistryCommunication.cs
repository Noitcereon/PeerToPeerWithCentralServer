using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PeerToPeerLib;

namespace TCPPeerServer
{
    public static  class RegistryCommunication
    {
        private const string RegistryBaseUrl = "http://localhost:59022/api/powernap/";

        public static async Task ServerStartup(List<string> filesOnServer, FileEndPoint peer)
        {
            foreach (var filePath in filesOnServer)
            {
                await RegisterFileAsync(Path.GetFileName(filePath), peer);
            }
        }

        public static async Task<string> RegisterFileAsync(string fileName, FileEndPoint peer)
        {
            using HttpClient client = new HttpClient();

            string peerJson = JsonSerializer.Serialize(peer);
            StringContent content = new StringContent(peerJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(RegistryBaseUrl + fileName, content);

            return response.IsSuccessStatusCode ? "Registration succeeded" : "Registration failed";
        }

        public static void DeregisterFile(string fileName)
        {
            // TODO: Deregister file TCP Server
            throw new NotImplementedException("Deregister file is not implemented.");
        }
    }
}
