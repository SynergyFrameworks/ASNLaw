using Infrastructure.Modularity;

namespace Infrastructure.ExportImport
{
    public class ExportImportOptions
    {
        public ModuleIdentity ModuleIdentity { get; set; }

        /// <summary>
        /// Flag means the use of  binary data in export or import operations
        /// </summary>
        public bool HandleBinaryData { get; set; } = false;

    }
}
