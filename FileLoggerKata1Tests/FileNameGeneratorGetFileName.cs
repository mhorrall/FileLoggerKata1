using System;
using Moq;
using Xunit;

namespace FileLoggerKata1.Tests
{
    public class FileNameGeneratorGetFileName
    {
        [Theory]
        [InlineData(2019, 1, 7)]
        [InlineData(2019, 1, 8)]
        [InlineData(2019, 1, 9)]
        [InlineData(2019, 1, 10)]
        [InlineData(2019, 1, 11)]
        public void ReturnsDateStringFilenameForWeekdays(int year, int month, int day)
        {
            var date = new DateTime(year, month, day);
            var mockDateTime = new Mock<IDateTime>();
            mockDateTime.Setup(d => d.Today).Returns(date);

            var generator = new FileNameGenerator(mockDateTime.Object);

            var fileName = generator.GetFileName();

            var expected = date.ToString("yyyyMMdd") + ".txt";

            Assert.Equal(expected, fileName);
        }

        [Theory]
        [InlineData(2019, 1, 12)]
        [InlineData(2019, 1, 13)]
        public void ReturnsWeekendFilenameForWeekend(int year, int month, int day)
        {
            var date = new DateTime(year, month, day);
            var mockDateTime = new Mock<IDateTime>();
            mockDateTime.Setup(d => d.Today).Returns(date);

            var generator = new FileNameGenerator(mockDateTime.Object);

            var fileName = generator.GetFileName();

            var expected = "weekend.txt";

            Assert.Equal(expected, fileName);
        }
    }
}