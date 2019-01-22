using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLoggerKata1
{
    public interface IFileSystem
    {
        void CreateFileIfNotExists(string fileName);
        void MoveFile(string fileName, string destinationFileName);
        void StringWriteLineToFile(string fileName, string message);
        DateTime GetCreationTime(string fileName);
    }
}
