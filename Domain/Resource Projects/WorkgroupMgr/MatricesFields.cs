using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkgroupMgr
{
    public static class MatricesFields
    {
        // Metadata Table:      
        public static string Name = "Name";
        public static string Description = "Description";
        public static string CreatedBy = "CreatedBy"; // User who created the Workspace
        public static string CreationDate = "CreationDate"; // DateTime datatype

        // Tables:
        public static string MetadataTableName = "Metadata";
        public static string DocsAssociationTableName = "DocsAssociation";

        // DocsAssociation Table:
        public static string UID = "UID";
        public static string DocTypeItem = "DocTypeItem";
        public static string Column = "Column"; // e.g. A, B, C ... or Any - column assignment
        public static string DocTypeDescription = "DocTypeDescription";
        public static string DocTypeSource = "DocTypeSource"; // e.g. "Analysis Results" or "Deep Analysis Results" 
        public static string DocTypeContentType = "DocTypeContentType";
        public static string ProjectDocumentName = "ProjDocName";

    }
}
