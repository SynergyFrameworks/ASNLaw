using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scion.FilesService.Contracts
{
    /// <summary>
    /// Definition of a source, used by the manager to manage files.
    /// </summary>
    public interface ISourceProvider
    {
        /// <summary>
        /// Source's name.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Source provider (i.e. SQL, Azure, Mongo, Oracle, File System...etc.)
        /// </summary>
        string Provider { get; set; }
        /// <summary>
        /// The source's connection string or path.
        /// </summary>
        /// <returns></returns>
        string SourceConnectionOrPath { get; set; }

        /// <summary>
        /// Additional path to cover Hybrid scenarios (i.e SQL FileStream). Server Files Location.
        /// </summary>
        string ServerPath { get; set; }
    }
}
