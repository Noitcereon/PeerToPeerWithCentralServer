using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace TCPPeerServer
{
    public class FileManagement
    {
        public List<string> GetAllFilesOnServer(string rootPath)
        {
            List<string> output = new List<string>();

            Directory.CreateDirectory(rootPath);
            string[] files = Directory.GetFiles(rootPath);
            output.AddRange(files);

            return output;
        }

        /// <summary>
        /// Returns filepath of the requested file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filesOnServer"></param>
        /// <returns></returns>
        public static string GetFile(string fileName, List<string> filesOnServer)
        {
            foreach (var filePath in filesOnServer)
            {
                // note: if multiple files have similar names, this only returns the first occurence.
                if (Path.GetFileNameWithoutExtension(filePath) == (fileName))
                {
                    return filePath;
                }
            }
            return "Couldn't find file.";
        }
    }
}
