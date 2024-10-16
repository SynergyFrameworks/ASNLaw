using System;

namespace Infrastructure.Settings
{
    public class ApplicationSetting
    {
        public Guid Id { get; set; }
        public String Key { get; set; }
        public String Value { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String DataType { get; set; }
        public String ValueList { get; set; }
    }
}
