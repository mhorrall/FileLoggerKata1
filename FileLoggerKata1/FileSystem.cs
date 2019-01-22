using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLoggerKata1
{
    public class FileSystem: IFileSystem
    {
        public void CreateFileIfNotExists(string fileName)
        {
            using (File.CreateText(fileName)) { }
        }

        public DateTime GetCreationTime(string fileName)
        {
            var fi = new FileInfo(fileName);
            return fi.CreationTime;
        }

        public void MoveFile(string fileName, string destinationFileName)
        {
            File.Move(fileName, destinationFileName);
        }

        public void StringWriteLineToFile(string fileName, string message)
        {
            using (var writer = File.AppendText(fileName))
            {
                writer.WriteLine(message);
                writer.Flush();
            }
        }
    }
}
