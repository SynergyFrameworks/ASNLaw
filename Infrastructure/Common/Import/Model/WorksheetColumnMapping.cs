
namespace Infrastructure.Import.Model
{
    public class WorksheetColumnMapping
    {
        public int ColumnNumber { get; set; }
        public string PropertyName { get; set; }

        public WorksheetColumnMapping(int columnNumber, string propertyName)
        {
            ColumnNumber = columnNumber;
            PropertyName = propertyName;
        }
    }
}
