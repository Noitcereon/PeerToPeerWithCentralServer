using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TCPPeerServer
{
    public static class RegistryCommunication
    {

        public static void ServerStartup(List<string> filesOnServer)
        {
            
        }

        public static void RegisterFile(string fileName)
        {
            HttpClient client = new HttpClient();
        }

        public static void DeregisterFile(string fileName)
        {

        }
    }
}
