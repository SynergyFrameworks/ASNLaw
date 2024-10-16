using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atebion.Tasks
{
    public static class Attributes
    {
        public static string UID = "UID";
        public static string TaskFlow_UID = "TaskFlow_UID"; // Foreign Key
        public static string Process = "Process";  // Foreign Key
        public static string Attribute_Value = "Attribute_Value";
        public static string Attribute_Name = "Attribute_Name";
        public static string Attribute_Caption = "Attribute_Caption";
        public static string Attribute_Instructions = "Attribute_Instructions";
        public static string Attribute_ValueOptions = "Attribute_ValueOptions"; // List of delimted '|' values 

        public static string TableName = "Attributes";
        
    }
}
