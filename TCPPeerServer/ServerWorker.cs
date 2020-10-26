using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PeerToPeerLib;

namespace TCPPeerServer
{
    public class ServerWorker
    {
        private enum ClientRequest { GetFile, UploadFile, List }

        private static List<string> _filesOnServer;

        public void Start(int portNo)
        {
            TcpListener server = new TcpListener(IPAddress.Loopback, portNo);
            DirectoryInfo dirInfo = new DirectoryInfo(@"F:\visual_studio_projects\repos\3_semester\PeerToPeerWithCentralServer\TCPPeerServer");
            _filesOnServer = FileManagement.GetAllFilesOnServer($"{dirInfo}\\PeerServerFiles\\{portNo}");
            string serverIPAddress = server.LocalEndpoint.ToString().Split(":").First();
            Task.Run(() => RegistryCommunication.ServerStartup(_filesOnServer, new FileEndPoint(serverIPAddress, portNo)));

            server.Start();
            LogMessage("Server ready", portNo);
            while (true)
            {
                TcpClient tempSocket = server.AcceptTcpClient();
                LogMessage("Client connected", portNo);
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
                sw.WriteLine("Commands: GetFile, UploadFile, List");
                ClientRequest clientRequest = (ClientRequest)Enum.Parse(typeof(ClientRequest), sr.ReadLine());

                switch (clientRequest)
                {
                    case ClientRequest.GetFile:
                        sw.WriteLine("Enter name of the file you want to retrieve:");
                        string fileName = sr.ReadLine();

                        string file = FileManagement.GetFile(fileName, _filesOnServer);
                        LogMessage($"GetFile({fileName}) called");

                        sw.WriteLine($"{file}");
                        break;
                    case ClientRequest.UploadFile:
                        // TODO: Upload/Register file
                        break;
                    case ClientRequest.List:
                        _filesOnServer.ForEach(x => sw.WriteLine(Path.GetFileName(x)));
                        break;

                    default:
                        sw.WriteLine("Request not understood by server");
                        break;
                }
            }
            catch (Exception e)
            {
                sw.WriteLine("Request failed.");
                Console.WriteLine($"Error thrown. Message: {e.Message}");
                HandleClient(tempSocket);
            }
        }

        private static void LogMessage(string message)
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss}: {message}");
        }
        private static void LogMessage(string message, int portNo)
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss} {portNo}: {message}");
        }
    }
}
