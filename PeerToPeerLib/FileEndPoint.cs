using System;
using System.Collections.Generic;
using System.Text;

namespace PeerToPeerLib
{
    public class FileEndPoint
    {
        public string IpAddress { get; set; }
        public int PortNo { get; set; }


        public FileEndPoint()
        {
            
        }

        public FileEndPoint(string ipAddress, int portNo)
        {
            IpAddress = ipAddress;
            PortNo = portNo;
        }
    }
}
