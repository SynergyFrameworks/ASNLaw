﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel
{
    public interface IExcelManager
    {
        IWorkbook OpenWorkbook(string uri);
        IWorkbook OpenWorkbook(FileInfo file);
        IWorkbook OpenWorkbook(Stream stream);
    }
}
