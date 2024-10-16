using System;
using System.IO;
using System.Threading.Tasks;
using Infrastructure.Common;

namespace Infrastructure.ExportImport
{
    public interface IExportSupport
    {
        Task ExportAsync(Stream outStream, ExportImportOptions options, Action<ExportImportProgressInfo> progressCallback, ICancellationToken cancellationToken);
    }
}
