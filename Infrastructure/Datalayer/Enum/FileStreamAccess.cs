using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datalayer.Enum
{
    public enum FileStreamAccess
    {
        Disable = 0,
        TransactSQLAccess = 1,
        TransactSQLAccessAndWindowsStreaming = 2
    }
}
