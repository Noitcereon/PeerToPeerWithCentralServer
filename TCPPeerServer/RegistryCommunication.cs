using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PeerToPeerLib;

namespace TCPPeerServer
{
    public static class RegistryCommunication
    {
        private const string RegistryBaseUrl = "http://localhost:59022/api/powernap/";
        public static void ServerStartup(List<string> filesOnServer)
        {

        }

        public static async Task<string> RegisterFile(string fileName, FileEndPoint peer)
        {
            using HttpClient client = new HttpClient();

            string peerJson = JsonSerializer.Serialize(peer);
            StringContent content = new StringContent(peerJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(RegistryBaseUrl + fileName, content);

            return response.IsSuccessStatusCode ? "Registration succeeded" : "Registration failed";
        }

        public static void DeregisterFile(string fileName)
        {

        }
    }
}
