using System;

namespace TCPPeerServer
{
    public class Program
    {
        private static void Main(string[] args)
        {
            ServerWorker worker = new ServerWorker();
            worker.Start();
        }
    }
}
