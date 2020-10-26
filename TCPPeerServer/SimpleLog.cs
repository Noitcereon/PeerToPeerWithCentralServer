using System;
using System.Collections.Generic;
using System.Text;

namespace TCPPeerServer
{
    public static class SimpleLog
    {
        internal static void LogMessage(string message)
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss}: {message}");
        }
        internal static void LogMessage(string message, int portNo)
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss} {portNo}: {message}");
        }
    }
}
