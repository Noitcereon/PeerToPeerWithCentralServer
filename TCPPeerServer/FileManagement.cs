using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace TCPPeerServer
{
    internal class FileManagement
    {
        public List<string> GetAllFilesOnServer(string basePath)
        {
            List<string> output = new List<string>();
            
            DirectoryInfo serverFileDirectory = new DirectoryInfo(basePath);
            serverFileDirectory.EnumerateFiles()
        }
    }
}
