namespace Scion.FilesService.Contracts
{
    public class File
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string ContentType { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}