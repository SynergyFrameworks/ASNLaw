using Infrastructure.Import.Model;
using Infrastructure.Common.Domain;
using Infrastructure.Common.Domain.Performance;
using System.Collections.Generic;

namespace Infrastructure.Import.TabularTransformer
{
    public interface ITabularTransformer
    {
        WorkbookModel Transform(DataUpload upload, int? headerColumnCount = null);
        Dictionary<string, IList<CodeDescription>> GenerateLookUpDataFromWorkbookModel<T, U>(WorkbookModel workbook);
    }
}
