using System;
using Moq;
using Xunit;

namespace FileLoggerKata1.Tests
{
    public class FileNameGeneratorGetLastSaturdayFileName
    {
        [Theory]
        [InlineData(2019, 1, 6)]
        [InlineData(2019, 1, 7)]
        [InlineData(2019, 1, 8)]
        [InlineData(2019, 1, 9)]
        [InlineData(2019, 1, 10)]
        [InlineData(2019, 1, 11)]
        [InlineData(2019, 1, 12)]
        public void ReturnsLastSaturdayDateStringFilenameForWeekdays(int year, int month, int day)
        {
            var date = new DateTime(year, month, day);
            var expectedSaturday = new DateTime(2019, 1, 5);
            var mockDateTime = new Mock<IDateTime>();
            mockDateTime.Setup(d => d.Today).Returns(date);

            var generator = new FileNameGenerator(mockDateTime.Object);

            var fileName = generator.GetLastSaturdayFileName();

            var expected = $"weekend-{expectedSaturday:yyyyMMdd}.txt";

            Assert.Equal(expected, fileName);
        }
    }
}