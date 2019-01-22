using Xunit;
using FileLoggerKata1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Moq;

namespace FileLoggerKata1.Tests
{
    public class FileLoggerLog
    {
        private readonly FileLogger _fileLogger;
        private readonly Mock<IFileNameGenerator> _mockFileNameGenerator = new Mock<IFileNameGenerator>();
        private Mock<IDateTime> _mockDateTime = new Mock<IDateTime>();
        private Mock<IFileSystem> _mockFileSystem = new Mock<IFileSystem>();
        private string _testFileName;

        private string _testMessage = Guid.NewGuid().ToString();

        public FileLoggerLog()
        {
            _testFileName = DateTime.Today.ToString("yyyyMMdd") + ".txt";

            _fileLogger = new FileLogger(_mockFileNameGenerator.Object, _mockDateTime.Object, _mockFileSystem.Object);
        }

        [Fact()]
        public void CreatesLogTxtFileWhenRunFirstTime()
        {
            _testFileName = "20190122.txt";
            _mockFileNameGenerator.Setup(n => n.GetFileName()).Returns(_testFileName);

            _fileLogger.Log(_testMessage);

            _mockFileSystem.Verify(fs => fs.StringWriteLineToFile(_testFileName, _testMessage), Times.Once);
        }


        [Fact()]
        public void CreatesWeekendFileNameOnSaturday()
        {
            _testFileName = "weekend.txt";
            var testSaturday = new DateTime(2019, 1, 19);
            _mockFileNameGenerator.Setup(n => n.GetFileName()).Returns(_testFileName);
            _mockDateTime.Setup(d => d.Today).Returns(testSaturday);
            _mockFileSystem.Setup(fi => fi.GetCreationTime(_testFileName)).Returns(testSaturday);

            _fileLogger.Log(_testMessage);

            _mockFileSystem.Verify(fs => fs.StringWriteLineToFile(_testFileName, _testMessage), Times.Once);
        }

        [Fact()]
        public void CreatesWeekendFilenameFileOnSaturdayAndRenamesOldOneToLastSaturday()
        {
            _testFileName = "weekend.txt";
            var testSaturday = new DateTime(2019, 1, 19);
            var lastSaturdayFileName = "weekend-20190112.txt";

            _mockFileNameGenerator.Setup(n => n.GetFileName()).Returns(_testFileName);
            _mockFileNameGenerator.Setup(n => n.GetLastSaturdayFileName()).Returns(lastSaturdayFileName);
            _mockDateTime.Setup(d => d.Today).Returns(testSaturday);
            _mockFileSystem.Setup(fi => fi.GetCreationTime(_testFileName)).Returns(testSaturday.AddDays(-7));

            _fileLogger.Log(_testMessage);

            _mockFileSystem.Verify(fs => fs.MoveFile(_testFileName, lastSaturdayFileName), Times.Once);
            _mockFileSystem.Verify(fs => fs.StringWriteLineToFile(_testFileName, _testMessage), Times.Once);
        }
    }
}