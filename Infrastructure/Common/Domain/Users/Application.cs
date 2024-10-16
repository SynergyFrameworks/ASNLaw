using System;
using System.Collections.Generic;

namespace Infrastructure.Common.Domain.Users
{
    public class Application
    {
        public virtual Guid Id { get; set; }
        public virtual string Settings { get; set; }
        public virtual string Name { get; set; }
        public virtual Group Group { get; set; }
        public virtual Dictionary<string, object> ApplicationSettings { get; set; } 
    }
}
