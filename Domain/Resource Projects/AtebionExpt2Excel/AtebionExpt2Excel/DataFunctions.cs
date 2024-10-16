using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atebion.Excel.Output
{
    public static class DataFunctions
    {
        public static bool IsWholeNumber(string value)
        {
            if (!IsDouble(value))
            {
                return false;
            }

            double d = Convert.ToDouble(value);
            if ((d % 1) == 0)
            {
                return true;
            }

            return false;

        }

        public static bool IsDouble(string value)
        {
            try
            {
                double x = Convert.ToDouble(value);
            }
            catch
            {
                return false;
            }

            return true;

        }
    }
}
