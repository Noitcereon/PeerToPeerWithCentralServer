using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace TCPPeerServer
{
    public class FileManagement
    {
        public static List<string> GetAllFilesOnServer(string rootPath)
        {
            List<string> output = new List<string>();

            Directory.CreateDirectory(rootPath);
            string[] files = Directory.GetFiles(rootPath);
            output.AddRange(files);

            return output;
        }

        public void GetFile(string fileName)
        {

        }
    }
}
