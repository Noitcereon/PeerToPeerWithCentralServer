using System;
using System.Threading.Tasks;

namespace TCPPeerServer
{
    public class Program
    {
        private static void Main(string[] args)
        {
            ServerWorker worker = new ServerWorker();
            Task.Run(() => worker.Start(8001));
            Task.Run(() => worker.Start(8002));
            Task.Run(() => worker.Start(8003));
            Task.WaitAll();
            Console.ReadLine(); // keeps the server running
        }
    }
}
