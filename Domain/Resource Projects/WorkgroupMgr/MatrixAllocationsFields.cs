using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkgroupMgr
{
    public static class MatrixAllocationsFields
    {

        public static string UID = "UID";
        public static string SourceType = "SourceType"; // e.g. Document Type, List, Refernece Resource
        public static string SourceName = "SourceName"; // Name of the source
        public static string SourceUID = "SourceUID"; // Sources item UID

        public static string Column = "Column"; // Column of the allocation
        public static string Cell = "Cell"; // Cell location of allocation
        public static string DocTypeContentType = "DTContentType";
        public static string DocTypeSource = "DTSource"; // Either Analysis Results or Deep Analysis
        public static string Text = "Text"; // Allocated Text
        public static string Content = "Content"; // Used when Number and/or Caption is the Content Type, holds the actual text assocated with the Number and Caption -- Added 12.11.2017

        public static string AllocatedBy = "AllocatedBy"; // User who made the allocation
        public static string AllocatedDateTime = "AllocatedDateTime"; // DateTime of the allocation

        public static string TableName = "Allocations";


        public static string SourceType_DocType = "DocType"; 
        public static string SourceType_List = "List";
        public static string SourceType_RefRes = "RefRes";
    }
}
