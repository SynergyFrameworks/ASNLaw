namespace StorageProvider.Exceptions
{
    public class FileExistsException : FileSystemException
    {
        public string Path { get; }
        public string Prefix { get; }

        public FileExistsException(string path, string prefix) : base(GetMessage(path, prefix))
        {
            Path = path;
            Prefix = prefix;
        }

        private static string GetMessage(string path, string prefix)
        {
            return $"File at path '{path}' already exists in adapter with prefix '{prefix}'.";
        }
    }
}