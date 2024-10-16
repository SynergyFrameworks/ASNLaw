namespace Infrastructure.Telemetry
{
    public class IgnoreSqlTelemetryOptions
    {
        public string[] QueryIgnoreSubstrings { get; set; } = new string[] { };
    }
}
