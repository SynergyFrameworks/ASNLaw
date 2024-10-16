namespace LawUI.Domain.Model
{
    public class Document
    {
        public Dictionary<string, string> Fields { get; set; }

        public void ReplaceField(string key, string value)
        {
            if (Fields.ContainsKey(key))
            {
                Fields[key] = value;
            }
            else
            {
                Fields.Add(key, value);
            }
        }
    }
}
