using Scion.FilesService.Contracts;

namespace Scion.FilesService.Contracts.Sql
{
    public class SqlProvider : ISourceProvider
    {
        public string Name { get; set; }
        public string Provider { get; set; } = "SQL.Client";

        public string SourceConnectionOrPath { get; set; }
        public string ServerPath { get; set; }
    }
}
