namespace Scion.FilesService.Contracts
{
    public class ScionStream : System.IO.MemoryStream
    {
        public ScionStream()
        {
        }

        public string SourceName {get; set;}
        public string ContentType { get; set; }
    }
}
