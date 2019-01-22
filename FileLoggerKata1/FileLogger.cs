using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FileLoggerKata1
{
    public class FileLogger
    {
        private readonly IFileNameGenerator _fileNameGenerator;
        private readonly IDateTime _dateTime;
        private readonly IFileSystem _fileSystem;

        public FileLogger(IFileNameGenerator fileNameGenerator, IDateTime dateTime, IFileSystem fileSystem)
        {
            _fileNameGenerator = fileNameGenerator;
            _dateTime = dateTime;
            _fileSystem = fileSystem;
        }

        public void Log(string message)
        {
            var fileName = _fileNameGenerator.GetFileName();

            _fileSystem.CreateFileIfNotExists(fileName);

            if (fileName == "weekend.txt")
            {
                if (_fileSystem.GetCreationTime(fileName) < _dateTime.Today.AddDays(-2))
                {
                    _fileSystem.MoveFile(fileName, _fileNameGenerator.GetLastSaturdayFileName());
                }
            }

            _fileSystem.StringWriteLineToFile(fileName, message);
        }
    }
}
