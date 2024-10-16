using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Atebion.Common;

using unvell.ReoGrid;
using unvell.ReoGrid.Events;
using OfficeOpenXml;
using System.Xml;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Diagnostics;
using OfficeOpenXml.Style;


namespace ProfessionalDocAnalyzer
{
    public partial class ucResults_ExcelPreview : UserControl
    {
        public ucResults_ExcelPreview()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        private string _ExcelPathFile = string.Empty;
        private string _Subject = string.Empty;
        private Atebion.Outlook.Email _EmailOutLook;



        public bool LoadData(string ExcelPathFile, string Subject)
        {
            DoubleBuffered = true;

            reoGridControl1.Visible = false;
            butDelete.Visible = false;
            butEmail.Visible = false;
            butOpen.Visible = false;

            if (!File.Exists(ExcelPathFile))
            {
                string msg = string.Concat("Unable to find Excel file: ", ExcelPathFile);
                MessageBox.Show(msg, "Excel File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            _ExcelPathFile = ExcelPathFile;

            Cursor.Current = Cursors.WaitCursor; // Waiting

            try
            {
                //get the directory name the file is in
                string sourceDirectory = Path.GetDirectoryName(ExcelPathFile);

                //get the file name without extension
                string filenameWithoutExtension = Path.GetFileNameWithoutExtension(ExcelPathFile);

                //get the file extension
                string fileExtension = Path.GetExtension(ExcelPathFile);

                // 
                FileInfo ExcelPathFileFileInfo = new FileInfo(ExcelPathFile);
                bool worksheetSummary = false;
                using (ExcelPackage package = new ExcelPackage(ExcelPathFileFileInfo))
                {
                    // Grab the sheet with the template.
                    //ExcelWorksheet sheet = package.Workbook.Worksheets[SheetName];
                    var worksheetX = package.Workbook.Worksheets.SingleOrDefault(x => x.Name == "Summary");
                    if (worksheetX != null)
                    {
                        worksheetSummary = true;
                    }
                }
                if (worksheetSummary)
                {
                    //get the new file name
                    string DestFileName = Path.Combine(sourceDirectory, filenameWithoutExtension + "_ReoReport" + fileExtension);

                    File.Copy(ExcelPathFile, DestFileName, true);

                    FileInfo cleanedXL = new FileInfo(DestFileName);

                    // Create Excel EPPlus Package based on template stream
                    using (ExcelPackage package = new ExcelPackage(cleanedXL))
                    {
                        // Grab the sheet with the template.
                        //ExcelWorksheet sheet = package.Workbook.Worksheets[SheetName];
                        var worksheetX = package.Workbook.Worksheets.SingleOrDefault(x => x.Name == "Summary");
                        package.Workbook.Worksheets.Delete(worksheetX);
                        package.Save();
                    }

                    reoGridControl1.Load(DestFileName);
                }
                else
                {
                    reoGridControl1.Load(ExcelPathFile);
                }

                var worksheet = reoGridControl1.CurrentWorksheet;

                worksheet.ScaleFactor = 0.75f;

                worksheet.SetSettings(WorksheetSettings.Edit_Readonly, true);

                reoGridControl1.Visible = true;
                butDelete.Visible = true;
            }
            catch (Exception ex)
            {
                string msg = string.Concat("Click the Open button to see the Excel file.", Environment.NewLine, Environment.NewLine, ex.Message);
                MessageBox.Show(msg, "Unable to Display Excel File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
            butOpen.Visible = true;

            _EmailOutLook = new Atebion.Outlook.Email();
            if (OutLookMgr.isOutLookRunning())
            {
                if (_EmailOutLook.IsOutlookConnectable())
                    butEmail.Visible = true;
            }


            Cursor.Current = Cursors.Default;


            return true;
        }

        private void butOpen_Click(object sender, EventArgs e)
        {
            Process.Start(_ExcelPathFile);
        }

        private void butEmail_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            List<string> sAttachments = new List<string>();

            sAttachments.Add(_ExcelPathFile);

            string file = Files.GetFileName(_ExcelPathFile);

            string subject = string.Concat("Excel file for ", _Subject);
            string body = string.Concat(Environment.NewLine, Environment.NewLine + "Please see the attached file: ", file);

            _EmailOutLook.OpenEmailWithAttachments(string.Empty, subject, body, sAttachments.ToArray());

            Cursor.Current = Cursors.Default;
        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            string msg = string.Concat("Are you sure you want to delete the selected Excel file: ", _ExcelPathFile, " ?");
            if (MessageBox.Show(msg, "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            reoGridControl1.Visible = false;

            try
            {
                File.Delete(_ExcelPathFile);
            }
            catch
            {
                msg = string.Concat("Unable to deleted the selected Excel file.", Environment.NewLine, Environment.NewLine, "Most likely the Excel is still opened. Please close the Excel file and retry.");
                MessageBox.Show(msg, "Unable to Delete the Excel File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reoGridControl1.Visible = true;
            }

        }

        private void butPrint_Click(object sender, EventArgs e)
        {
            var session = reoGridControl1.Worksheets[0].CreatePrintSession();
            // var session = worksheet.CreatePrintSession();
            session.Print();
        }
    }
}
