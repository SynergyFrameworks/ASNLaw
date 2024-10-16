using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure
{
    public class Application
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Group Group { get; set; }
        public Dictionary<string, object> ApplicationSettings { get; set; } 
    }
}