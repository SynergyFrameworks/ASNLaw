using Infrastructure.Modularity;

namespace Infrastructure.Modularity
{
    public class ModuleBackgroundJobOptions
    {
        public ModuleAction Action { get; set; }
        public ModuleDescriptor[] Modules { get; set; }
    }
}
