using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkgroupMgr
{
    public static class RefResFields
    {
        public static string UID = "UID";
        public static string Name = "Name";
        public static string Description = "Description";
        public static string Path = "Path";
        public static string LocationType = "LocationType";

        // Value Options
        public static string LocationType_Internal = "Internal"; // Internal to a Workgroup
        public static string LocationType_Shared = "Shared"; // (Can be) Shared between Workgroup

        public static string TableName = "RefRes";

 
    }
}
