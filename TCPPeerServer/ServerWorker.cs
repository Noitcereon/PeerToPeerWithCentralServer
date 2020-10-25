﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPPeerServer
{
    public class ServerWorker
    {
        private enum ClientRequest { GetFile, UploadFile }
        private static List<string> _filesOnServer = new List<string>();

        public void Start()
        {
            TcpListener server = new TcpListener(IPAddress.Loopback, 8001);
            RegistryCommunication.ServerStartup();

            server.Start();
            LogMessage("Server ready");
            while (true)
            {
                TcpClient tempSocket = server.AcceptTcpClient();
                Task.Run(() => HandleClient(tempSocket));
            }
        }

        private void HandleClient(TcpClient tempSocket)
        {
            using NetworkStream ns = tempSocket.GetStream();
            StreamWriter sw = new StreamWriter(ns) { AutoFlush = true };
            StreamReader sr = new StreamReader(ns);

            try
            {
                ClientRequest clientRequest = (ClientRequest)Enum.Parse(typeof(ClientRequest), sr.ReadLine() ?? throw new InvalidOperationException());

                switch (clientRequest)
                {
                    case ClientRequest.GetFile:
                        sw.WriteLine("Enter name of the file you want to retrieve:");
                        string fileName = sr.ReadLine();

                        GetFile(fileName);
                        LogMessage($"GetFile({fileName}) called");

                        sw.WriteLine($"Oi, you tried to get a file named \"{fileName}\"");
                        break;
                    case ClientRequest.UploadFile:

                        break;

                    default:
                        sw.WriteLine("Request not understood by server");
                        break;
                }
            }
            catch (Exception e)
            {
                sw.WriteLine("Request failed.");
                Console.WriteLine(e);
            }
        }

        private static void GetFile(string fileName)
        {
            string path = @"\PeerServerFiles\" + fileName;
            throw new NotImplementedException();
        }

        private static void LogMessage(string message)
        {
            Console.WriteLine($"{DateTime.Now}: {message}");
        }
    }
}
