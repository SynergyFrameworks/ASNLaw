using System.Collections.Generic;
using System.IO;

namespace Excel.Transformers
{
    public interface IExcelTransformer
    {
        object ParseExcelFile(Stream file, Dictionary<string, object> data = null);
    }
}
