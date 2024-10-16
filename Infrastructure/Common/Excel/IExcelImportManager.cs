using System.Collections.Generic;
using System.IO;

namespace Excel
{
    public interface IExcelImportManager
    {        
        T ParseExcelFile<T>(Stream file, Dictionary<string,object> data = null);
        Dictionary<string, T> ParseExcelFiles<T>(Dictionary<string,Stream> files);
    }
}
