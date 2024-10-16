using System;
using System.IO;

namespace Excel.EPPlus
{
    public class EPPlusAdapter : IExcelAdapter
    {
        public IWorkbook OpenWorkbook(FileInfo file)
        {
            try
            {
                return new EPPlusWorkbook(file);
            }
            catch (Exception)
            {
                throw new InvalidDataException("Invalid Excel file specified");
            }
        }

        public IWorkbook OpenWorkbook(Stream fileStream)
        {
            try
            {
                return new EPPlusWorkbook(fileStream);
            }
            catch (Exception)
            {
                throw new InvalidDataException("Invalid Excel file specified");
            }
        }
    }
}

