using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Persistence.Azure.ErrorHandlers
{
    public class AzureTableStorageErrorHandler : IErrorHandler
    {
        public Exception HandleError(Exception ex)
        {
            return ex;
        }
    }
}
