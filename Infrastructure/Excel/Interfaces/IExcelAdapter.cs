using System.IO;

namespace Excel
{
    public interface IExcelAdapter
    {
        IWorkbook OpenWorkbook(FileInfo file);
        IWorkbook OpenWorkbook(Stream fileStream);
    }
}
