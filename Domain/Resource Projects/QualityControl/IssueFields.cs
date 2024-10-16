using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atebion.QC
{
    public static class IssueFields
    {
        public static string UID = "UID";
        public static string ParseSeg_UID = "ParseSegUID";
        public static string Blank = "Blank"; // Use to store a narrow column of the Issue Color
        public static string IssueQty = "IssueQty";
        public static string IssueCat = "Category";
        public static string Issue = "Issue";
        public static string IssueColor = "IssueColor";
        public static string Flag = "Flag";
        public static string Weight = "Weight";


        public static string TableName = "IssuesFound";
    }
}
