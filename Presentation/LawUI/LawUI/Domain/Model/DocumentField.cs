namespace LawUI.Domain.Model
{
    public class DocumentField
    {
        public string Key { get; set; }          // Placeholder name, e.g., [#FirstName#]
        public string Value { get; set; }        // User-provided value
        public string FieldType { get; set; }    // Can be "text", "email", "date"
        public bool IsRequired { get; set; }     // Determines if the field is required
        public int Position { get; set; }
    }
}
