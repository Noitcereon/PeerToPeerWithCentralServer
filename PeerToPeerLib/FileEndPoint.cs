using System;
using System.Collections.Generic;
using System.Text;

namespace PeerToPeerLib
{
    public class FileEndPoint
    {
        private string _ipAddress;
        public string IpAddress { get; set; }

        private int _portNo;
        public int PortNo { get; set; }

        

        public FileEndPoint()
        {
            
        }

        public FileEndPoint(string ip, int portNo)
        {
            IpAddress = ip;
            portNo = portNo;
        }

        
    }
}
