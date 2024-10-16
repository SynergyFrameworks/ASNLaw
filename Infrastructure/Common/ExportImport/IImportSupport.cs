using System;
using System.IO;
using System.Threading.Tasks;
using Infrastructure.Common;

namespace Infrastructure.ExportImport
{
    public interface IImportSupport
    {
        Task ImportAsync(Stream inputStream, ExportImportOptions options, Action<ExportImportProgressInfo> progressCallback, ICancellationToken cancellationToken);
    }
}
