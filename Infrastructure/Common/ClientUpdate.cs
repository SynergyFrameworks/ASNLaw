using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public class ClientUpdate
    {
        public string ObjectName { get; set; }
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string ModifierName { get; set; }
        public string UpdateTime { get; set; }
        public string Application { get; set; }
        public string Message { get; set; }
        public List<KeyValuePair<string,string>> Params { get; set; }
        public string LinkType { get; set; }
    }
}