using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkgroupMgr
{
    public static class MatrixSBFields
    {
        public static string SB_UID = "SB_UID";
        public static string Name = "SBName";
        public static string SBTemplateName = "SBTemplateName";
        public static string Description = "Description";
        public static string CreatedBy = "CreatedBy"; // User who generated SB
        public static string CreatedDateTime = "CreatedDateTime"; // DateTime of the SB generation

        public static string TableName_SBs = "Storyboards";
        public static string TableName_SBMatrixRows = "SBMatrix";

        public static string SBMatrix_UID = "SBMatrix_UID";
        public static string MatrixRow = "MatrixRow";

    }
}
