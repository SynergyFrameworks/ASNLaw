using System.Collections.Generic;
using System.Linq;

namespace Excel.Transformers
{
    public static class TransformerHelper
    {
        public static T ConvertStringToType<T>(string refDescription, Dictionary<string, T> refs, string defaultDescription = "", bool nullable = true)
        {
            object defaultValue = null;
            if (defaultDescription != "")
            {
                defaultValue = refs[defaultDescription];
            }
            if (!nullable)
            {
                defaultValue = refs.FirstOrDefault();
            }

            return !string.IsNullOrEmpty(refDescription) && refs.ContainsKey(refDescription) ? refs[refDescription] : (T)defaultValue;
        }

        public static string ReadStringFromCell(IWorksheet worksheet, int rowIndex, int colIndex)
        {
            return worksheet.Cells[rowIndex, colIndex].Value == null ? null : worksheet.Cells[rowIndex, colIndex].Value.ToString().Trim();
        }
    }
}
