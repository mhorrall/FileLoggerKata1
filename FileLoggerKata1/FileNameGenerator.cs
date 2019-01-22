using System;

namespace FileLoggerKata1
{
    public class FileNameGenerator : IFileNameGenerator
    {
        private readonly IDateTime _dateTime;

        public FileNameGenerator(IDateTime dateTime)
        {
            _dateTime = dateTime;
        }
        public string GetFileName()
        {
            if (_dateTime.Today.DayOfWeek == DayOfWeek.Saturday || _dateTime.Today.DayOfWeek == DayOfWeek.Sunday)
            {
                return "weekend.txt";
            }
            return _dateTime.Today.ToString("yyyyMMdd") + ".txt";
        }

        public string GetLastSaturdayFileName()
        {
            DateTime candidate = _dateTime.Today.AddDays(-1);
            while (candidate.DayOfWeek != DayOfWeek.Saturday)
            {
                candidate = candidate.AddDays(-1);
            }
            return $"weekend-{candidate:yyyyMMdd}.txt";
        }
    }
}