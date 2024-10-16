using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Word.Docx
{
    public class DocxProperty : IProperty
    {
        private CustomProperty _property;

        public DocxProperty(CustomProperty property)
        {
            _property = property;
        }

        public string Name
        {
            get { return _property.Name; }
        }

        public string Value
        {
            get { return _property.Value.ToString(); }
        }
    }
}
