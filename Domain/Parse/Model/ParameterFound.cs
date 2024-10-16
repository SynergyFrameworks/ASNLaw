namespace Domain.Parse.Model
{
    public class ParameterFound 
    {
        public string Parameter { get; set; }
        public ParameterFound Parent { get; set; }
        public int Index { get; set; }
        public int StartLine { get; set; }
        public int ParameterLength { get; set; }
        public string Found { get; set; }
        public string Caption { get; set; }


    }
}
