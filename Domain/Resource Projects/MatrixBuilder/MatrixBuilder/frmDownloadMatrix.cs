using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using WorkgroupMgr;

namespace MatrixBuilder
{
    public partial class frmDownloadMatrix : MetroFramework.Forms.MetroForm
    {
        public frmDownloadMatrix(string docTypesPath, string listPath, string refResPath, string matrixPath, string matrixTempPath, string matrixTempPathTemplates)
        {
            InitializeComponent();

            _docTypesPath = docTypesPath;
            _listPath = listPath;
            _refResPath = refResPath;
            _matrixPath = matrixPath;
            _matrixTempPath = matrixTempPath;
            _matrixTempPathTemplates = matrixTempPathTemplates;

            _isNew = true;

            LoadData();
        }

        private string _docTypesPath = string.Empty;
        private string _listPath = string.Empty;
        private string _refResPath = string.Empty;
        private string _matrixPath = string.Empty;
        private string _matrixName = string.Empty;
        private string _matrixTempPath = string.Empty;
        private string _matrixTempPathTemplates = string.Empty;

        private DataSet _dsMatrixSettings;


        private bool _isNew = true;
        private bool _isNewExcelSelection = true;
        private bool _adjListCols = false;
        private bool _adjRefResCols = false;

        private string _SelectedExcelFile = string.Empty;

        private WorkgroupMgr.MatrixTemplate _matrixTemplate;


        private string _baseURL = string.Empty;

        private void LoadData()
        {
            _matrixTemplate = new MatrixTemplate(_docTypesPath, _listPath, _refResPath, _matrixPath, _matrixTempPathTemplates);

            //   string storeLocation = "C:\\dump";
            string fileName = string.Empty;


            _baseURL = @"https://www.atebionllc.com/Templates/Matrices/";

            lblHeader.Text = "Matrix Templates";
            lblNotice.Text = "Check Matrix Templates to Download.";

 //           lblNotice.Text = "Check Matrix Templates to Download. Any associated Storyboards will be automatically downloaded with selected Matrices.";


        //    WebClient r = new WebClient();

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                var webClient = new WebClient();

                string content = webClient.DownloadString(_baseURL);


                // string content = r.DownloadString(_baseURL);

                foreach (Match m in Regex.Matches(content, "<a href=\\\"[^\\.]+\\.xlsx\">"))
                {
                    fileName = Regex.Match(m.Value, "\\w+\\.xlsx").Value;
                    fileName = Files.GetFileNameWOExt(fileName);
                    chklstDownload.Items.Add(fileName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to Download Templates", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool SaveSettings(string matrixTemplateName, string pathFile_xml, string pathFile_xlsx, string matrixTemplateDescription, string matrixSummary)
        {

            DataSet dsMatrixTemplate = DataFunctions.LoadDatasetFromXml(pathFile_xml);

            bool result = _matrixTemplate.SaveMatrixTemplate(matrixTemplateName, dsMatrixTemplate, pathFile_xlsx, _isNewExcelSelection, matrixTemplateDescription, matrixSummary);

            if (!result)
            {
              //  string alertError = string.Concat(Environment.NewLine, Environment.NewLine, "**** Save Matrix Template Settings ****", Environment.NewLine, _matrixTemplate.ErrorMessage);             
              //  MessageBox.Show(_matrixTemplate.ErrorMessage, "An Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);

                string msg = string.Concat("Matrix Template '", matrixTemplateName, "' was Not saved due to an error.", Environment.NewLine, Environment.NewLine, _matrixTemplate.ErrorMessage);
                MessageBox.Show(msg, "Matrix Template Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return result;
        }


        private void butOK_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            int counter = 0;
            int alreadyExisted = 0;
            string storeLocation = _matrixTemplate.GetMatrixTemporaryPath();


            string fullFileName_xml = string.Empty;
            string storeLocation_xml = string.Empty;

            string storeLocation_xlsx = string.Empty;
            string fullFileName_xlsx = string.Empty;

            //string fullFileName_html = string.Empty;
            //string storeLocation_html = string.Empty;

            string fullFileName_desc = string.Empty;
            string storeLocation_desc = string.Empty;

            string fullFileName_sum = string.Empty;
            string storeLocation_sum = string.Empty;

            string htmlTxt = string.Empty;
            string descriptionTxt = string.Empty;
            string summaryTxt = string.Empty;

            bool isSave = false;

            WebClient r = new WebClient();
            foreach (var fileName in chklstDownload.CheckedItems)
            {
                fullFileName_xml = string.Concat(fileName.ToString(), ".xml");
                storeLocation_xml = Path.Combine(storeLocation, fullFileName_xml);
  
                fullFileName_xlsx = string.Concat(fileName.ToString(), ".xlsx");
                storeLocation_xlsx = Path.Combine(storeLocation, fullFileName_xlsx);

                //fullFileName_html = string.Concat(fileName.ToString(), ".html");
                //storeLocation_html = Path.Combine(storeLocation, fullFileName_html);

                fullFileName_desc = string.Concat(fileName.ToString(), ".desc");
                storeLocation_desc = Path.Combine(storeLocation, fullFileName_desc);

                fullFileName_sum = string.Concat(fileName.ToString(), ".sum");
                storeLocation_sum = Path.Combine(storeLocation, fullFileName_sum);

                lblSaved1.Text = string.Empty;
                lblSaved1.ForeColor = Color.Lime;

                // Excel Matrix Template file
                if (File.Exists(storeLocation_xlsx))
                {
                    try
                    {
                        File.Delete(storeLocation_xlsx);
                    }
                    catch (Exception ex1)
                    {
                        string msg = string.Concat("Unable to download Excel Matrix Template '", fileName, "' due to an error.", Environment.NewLine, Environment.NewLine, ex1.Message);
                        MessageBox.Show(msg, "Unable to Download", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblSaved1.ForeColor = Color.Red;
                        lblSaved1.Text = msg;
                        return;
                    }    
                }
 
                r.DownloadFile(_baseURL + fullFileName_xlsx, storeLocation_xlsx);
                

                Application.DoEvents();

                // XML Template Data file
                if (File.Exists(storeLocation_xml))
                {
                    try
                    {
                        File.Delete(storeLocation_xml);
                    }
                    catch (Exception ex1)
                    {
                        string msg = string.Concat("Unable to download XML Data Matrix Template '", fileName, "' due to an error.", Environment.NewLine, Environment.NewLine, ex1.Message);
                        MessageBox.Show(msg, "Unable to Download", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblSaved1.ForeColor = Color.Red;
                        lblSaved1.Text = msg;
                        return;
                    }
                }

                    r.DownloadFile(_baseURL + fullFileName_xml, storeLocation_xml);
                    Application.DoEvents();

                // Removed 03.29.2020
                // HTML preview Template file
                //if (File.Exists(storeLocation_html))
                //{
                //    try
                //    {
                //        File.Delete(storeLocation_html);
                //    }
                //    catch (Exception ex1)
                //    {
                //        string msg = string.Concat("Unable to download HTML preview Matrix Template file '", fileName, "' due to an error.", Environment.NewLine, Environment.NewLine, ex1.Message);
                //        MessageBox.Show(msg, "Unable to Download", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        lblSaved1.ForeColor = Color.Red;
                //        lblSaved1.Text = msg;
                //        return;
                //    }
                //}

                //r.DownloadFile(_baseURL + fullFileName_html, storeLocation_html);
                

                Application.DoEvents();

                // Template Description file
                if (File.Exists(storeLocation_desc))
                {
                    try
                    {
                        File.Delete(storeLocation_desc);
                    }
                    catch (Exception ex1)
                    {
                        string msg = string.Concat("Unable to download Matrix Template Description file '", fileName, "' due to an error.", Environment.NewLine, Environment.NewLine, ex1.Message);
                        MessageBox.Show(msg, "Unable to Download", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblSaved1.ForeColor = Color.Red;
                        lblSaved1.Text = msg;
                        return;
                    }
                }

                r.DownloadFile(_baseURL + fullFileName_desc, storeLocation_desc);
                

                Application.DoEvents();

                // Template Summary file
                if (File.Exists(storeLocation_sum))
                {
                    try
                    {
                        File.Delete(storeLocation_sum);
                    }
                    catch (Exception ex1)
                    {
                        string msg = string.Concat("Unable to download Matrix Template Summary file '", fileName, "' due to an error.", Environment.NewLine, Environment.NewLine, ex1.Message);
                        MessageBox.Show(msg, "Unable to Download", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblSaved1.ForeColor = Color.Red;
                        lblSaved1.Text = msg;
                        return;
                    }
                }

                r.DownloadFile(_baseURL + fullFileName_sum, storeLocation_sum);
                

                Application.DoEvents();

                // Removed 03.29.2020
                //if (File.Exists(storeLocation_html))
                //    storeLocation_html = Files.ReadFile(storeLocation_html);

                if (File.Exists(storeLocation_desc))
                    descriptionTxt = Files.ReadFile(storeLocation_desc);
                else
                    descriptionTxt = string.Empty;

                if (File.Exists(storeLocation_sum))
                    summaryTxt = Files.ReadFile(storeLocation_sum);
                else
                    summaryTxt = string.Empty;


                isSave = SaveSettings(fileName.ToString(), storeLocation_xml, storeLocation_xlsx, descriptionTxt, summaryTxt);

                counter++;
            }

            if (alreadyExisted == 0)
                lblSaved1.Text = string.Concat("Downloaded  ", counter.ToString());
            else
                lblSaved1.Text = string.Concat("Existing File(s) Not Replaced  ", alreadyExisted.ToString(), "  Downloaded  ", counter.ToString());

            Cursor.Current = Cursors.Default;
            lblSaved1.Visible = true;
            this.Refresh();
            System.Threading.Thread.Sleep(2000);
            lblSaved1.Visible = false;
            this.Refresh();

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }
    }
}
