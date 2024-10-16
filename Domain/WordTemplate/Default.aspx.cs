//using System;
//using System.Data;
//using System.Configuration;
//using System.Collections;
//using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
//using System.Collections.Generic;
//using MarketReadyInventions.Utilities;

//namespace WordTest
//{
//    public partial class _Default : System.Web.UI.Page
//    {
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            //Add some sample data (2 records)
//            DataTable dt = new DataTable();
//            dt.Columns.Add("FromName");
//            dt.Columns.Add("FromAddress");
//            dt.Columns.Add("FromCity");
//            dt.Columns.Add("FromState");
//            dt.Columns.Add("FromZip");
//            dt.Columns.Add("ToName");
//            dt.Columns.Add("ToTitle");
//            dt.Columns.Add("SchoolName");
//            dt.Columns.Add("ToAddress");
//            dt.Columns.Add("ToCity");
//            dt.Columns.Add("ToState");
//            dt.Columns.Add("ToZip");

//            DataRow dr = dt.NewRow();
//            dr["FromName"] = "Bob Jones";
//            dr["FromAddress"] = "123 My Street";
//            dr["FromCity"] = "My City";
//            dr["FromState"] = "AZ";
//            dr["FromZip"] = "85293";
//            dr["ToName"] = "Fred Roberts";
//            dr["ToTitle"] = "Dean of Students";
//            dr["SchoolName"] = "Cook College";
//            dr["ToAddress"] = "234 State Street";
//            dr["ToCity"] = "Robinson";
//            dr["ToState"] = "TX";
//            dr["ToZip"] = "15432";

//            dt.Rows.Add(dr);

//            dr = dt.NewRow();
//            dr["FromName"] = "Fred Flinstone";
//            dr["FromAddress"] = "456 Prehistoric Way";
//            dr["FromCity"] = "Bedrock";
//            dr["FromState"] = "MI";
//            dr["FromZip"] = "49890";
//            dr["ToName"] = "Alan Anderson";
//            dr["ToTitle"] = "Professor";
//            dr["SchoolName"] = "Cartoon College";
//            dr["ToAddress"] = "555 Any Street";
//            dr["ToCity"] = "Anytown";
//            dr["ToState"] = "AK";
//            dr["ToZip"] = "88876";

//            dt.Rows.Add(dr);

//            //Path to template file
//            string templateDoc = HttpContext.Current.Server.MapPath("/word/Template.docx");
            
//            //Path to output file
//            string filename = HttpContext.Current.Server.MapPath("/word/Output.docx");

//            //Create word file
//            WordDataMerge word = new WordDataMerge(templateDoc, filename);
//            word.DataSource = dt;
//            word.GenerateWordFile();
//        }
//    }
//}
