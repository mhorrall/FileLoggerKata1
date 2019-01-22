using System;

namespace FileLoggerKata1
{
    public class SystemDateTime : IDateTime
    {
        DateTime IDateTime.Today => DateTime.Today;
    }
}