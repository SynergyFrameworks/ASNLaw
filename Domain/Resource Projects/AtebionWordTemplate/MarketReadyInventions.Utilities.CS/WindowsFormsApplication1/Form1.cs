using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Atebion.Word.Template;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Add some sample data (2 records)
            DataTable dt = new DataTable();
            dt.Columns.Add("FromName");
            dt.Columns.Add("FromAddress");
            dt.Columns.Add("FromCity");
            dt.Columns.Add("FromState");
            dt.Columns.Add("FromZip");
            dt.Columns.Add("ToName");
            dt.Columns.Add("ToTitle");
            dt.Columns.Add("SchoolName");
            dt.Columns.Add("ToAddress");
            dt.Columns.Add("ToCity");
            dt.Columns.Add("ToState");
            dt.Columns.Add("ToZip");
            dt.Columns.Add("Test.Me");
            dt.Columns.Add("TestMe2");

            DataRow dr = dt.NewRow();
            dr["FromName"] = "Bob Jones";
            dr["FromAddress"] = "123 My Street";
            dr["FromCity"] = "My City";
            dr["FromState"] = "AZ";
            dr["FromZip"] = "85293";
            dr["ToName"] = "Fred Roberts";
            dr["ToTitle"] = "Dean of Students";
            dr["SchoolName"] = "Cook College";
            dr["ToAddress"] = "234 State Street";
            dr["ToCity"] = "Robinson";
            dr["ToState"] = "TX";
            dr["ToZip"] = "15432";
            dr["Test.Me"] = "This is test 3.";

            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["FromName"] = "Fred Flinstone";
            dr["FromAddress"] = "456 Prehistoric Way";
            dr["FromCity"] = "Bedrock";
            dr["FromState"] = "MI";
            dr["FromZip"] = "49890";
            dr["ToName"] = "Alan Anderson";
            dr["ToTitle"] = "Professor";
            dr["SchoolName"] = "Cartoon College";
            dr["ToAddress"] = "555 Any Street";
            dr["ToCity"] = "Anytown";
            dr["ToState"] = "AK";
            dr["ToZip"] = "88876";
            dr["Test.Me"] = "This is test 4.";

            dt.Rows.Add(dr);

            //Path to template file
            string templateDoc = @"I:\Tom\Atebion\AtebionWordTemplate\AtebionWordTemplate\template3.docx";

            //Path to output file
            string filename = @"I:\Tom\Atebion\AtebionWordTemplate\AtebionWordTemplate\templateFill5.docx";

            //Create word file
            WordDataMerge word = new WordDataMerge(templateDoc, filename);
            word.DataSource = dt;
            word.GenerateWordFile();
        }


    }
}
