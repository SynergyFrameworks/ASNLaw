using Scion.Infrastructure.Modularity;

namespace Scion.Infrastructure.Web.Modularity
{
    public class ModuleBackgroundJobOptions
    {
        public ModuleAction Action { get; set; }
        public ModuleDescriptor[] Modules { get; set; }
    }
}
