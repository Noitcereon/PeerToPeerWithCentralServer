using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TCPPeerServer
{
    public class ServerWorker
    {
        private enum ClientRequest { GetFile, UploadFile }

        private static List<string> _filesOnServer;

        public void Start(int portNo)
        {
            TcpListener server = new TcpListener(IPAddress.Loopback, portNo);
            DirectoryInfo dirInfo = new DirectoryInfo(@"F:\visual_studio_projects\repos\3_semester\PeerToPeerWithCentralServer\TCPPeerServer");
            _filesOnServer = FileManagement.GetAllFilesOnServer($"{dirInfo}\\PeerServerFiles\\{portNo}");
            RegistryCommunication.ServerStartup(_filesOnServer);

            server.Start();
            LogMessage("Server ready", portNo);
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
                sw.WriteLine("Commands: GetFile, UploadFile");
                ClientRequest clientRequest = (ClientRequest)Enum.Parse(typeof(ClientRequest), sr.ReadLine());

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
                Console.WriteLine(e.Message);
                HandleClient(tempSocket);
            }
        }

        private void GetFile(string fileName)
        {
            //if (_filesOnServer.Contains(fileName))
            //{
            //    File.ReadLines()
            //}
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
