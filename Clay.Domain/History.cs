namespace Clay.Domain
{
    public class History
    {
        public IReadOnlyList<Log> Logs { get; set; }

        public IReadOnlyList<Log> GetLogs()
        {
            return Logs;
        }
    }
}
