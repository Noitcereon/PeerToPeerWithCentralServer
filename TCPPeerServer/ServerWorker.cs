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
            FileEndPoint thisPeer = new FileEndPoint(serverIPAddress, portNo);
            Task.Run(() => RegistryCommunication.ServerStartup(_filesOnServer, thisPeer));

            server.Start();
            LogMessage("Server ready", portNo);
            while (true)
            {
                TcpClient tempSocket = server.AcceptTcpClient();
                LogMessage("Client connected", portNo);
                Task.Run(() => HandleClient(tempSocket, thisPeer));
            }
        }

        private void HandleClient(TcpClient tempSocket, FileEndPoint thisPeer)
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
                        string newFileName = sr.ReadLine();
                        // TODO: Maybe add file creation here... 
                        // FileManagement.CreateFile(fileName) or something :thinking:
                        _filesOnServer.Add(newFileName); // only adds the name of the file, since there is no file.
                        Task.Run(() => RegistryCommunication.RegisterFileAsync(newFileName, thisPeer));
                        
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
                HandleClient(tempSocket, thisPeer);
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
