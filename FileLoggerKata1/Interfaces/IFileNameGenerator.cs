namespace FileLoggerKata1
{
    public interface IFileNameGenerator
    {
        string GetFileName();
        string GetLastSaturdayFileName();
    }
}