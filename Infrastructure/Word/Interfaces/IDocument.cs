using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Word
{
    public interface IDocument
    {
        void Save();
        void SaveAs(string path);

        ITable InsertTable(int columns, int rows);

        IParagraph InsertParagraph(string text);
        IParagraph InsertParagraph(string text, IFormatting fomatting);

        IList<IProperty> GetProperties();
        IList<ITable> GetTables();

        void SetPropertyValue(string propertyName, string propertyValue);
    }
}
