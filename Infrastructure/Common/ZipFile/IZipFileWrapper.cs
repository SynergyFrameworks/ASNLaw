using System.IO.Compression;

namespace Infrastructure.ZipFile
{
    public interface IZipFileWrapper
    {
        ZipArchive OpenRead(string fileName);
        void Extract(string zipFile, string destinationDir);
    }
}
