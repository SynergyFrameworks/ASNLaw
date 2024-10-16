using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using Domain.Common;

namespace Domain.ConceptAnalyzer
{
    // Responsibility Assignment Matrix
    public class ResponsAssigMatrix
    {
        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        public DataSet CreateTemplateDataSet()
        {
            DataTable dt = new DataTable(ExcelTemplateFields.RAM_TableName);

            dt.Columns.Add(ExcelTemplateFields.UID, typeof(string));

            dt.Columns.Add(ExcelTemplateFields.OrgTemplateFile, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.TemplateName, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.SheetName, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.ExportTempFor, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.Description, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.LocProjectName, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.LocAnalysisName, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.LocDocName, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.LocDate, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.LocYourName, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.DataRowStart, typeof(string));
         //   dt.Columns.Add(ExcelTemplateFields., typeof(string));
            dt.Columns.Add(ExcelTemplateFields.LocNumber, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.LocCaption, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.LocNoCaption, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.LocSegText, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.LocPageNo, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.LocDics, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.LocDicDefs, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.LocDicWeight, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.LocNotes, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.NotesEmbedded, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.FontName, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.FontSize, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.RAM_ModelName, typeof(string)); // Added for  Responsibility Assignment Matrix
            dt.Columns.Add(ExcelTemplateFields.DictionaryName, typeof(string)); // Added for  Responsibility Assignment Matrix
            dt.Columns.Add(ExcelTemplateFields.CreatedBy, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.CreatedDate, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.ModifiedDate, typeof(string));
            dt.Columns.Add(ExcelTemplateFields.ModifiedBy, typeof(string));


            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            DataTable dt2 = new DataTable(ResponsibilityAssMatrixFields.TableName);

            dt2.Columns.Add(ResponsibilityAssMatrixFields.UID, typeof(string));
            dt2.Columns.Add(ResponsibilityAssMatrixFields.Dictionary_Category_UID, typeof(int));
            dt2.Columns.Add(ResponsibilityAssMatrixFields.Role_Column, typeof(string));
            dt2.Columns.Add(ResponsibilityAssMatrixFields.Role_Notation, typeof(string));
            dt2.Columns.Add(ResponsibilityAssMatrixFields.Role_Name, typeof(string));
            dt2.Columns.Add(ResponsibilityAssMatrixFields.Role_Color, typeof(string));
            dt2.Columns.Add(ResponsibilityAssMatrixFields.Role_Description, typeof(string));
           

            ds.Tables.Add(dt2);


            return ds;
        }

        public string Get_DictionaryName_FromRAMTemplate(DataSet dsRAMTemplate, out string ModelName)
        {
            _ErrorMessage = string.Empty;

            ModelName = string.Empty;

            if (dsRAMTemplate == null)
            {
                _ErrorMessage = "RAM Template Configuration DataSet is null";
                return string.Empty;
            }

            try
            {
                string dictionaryName = dsRAMTemplate.Tables[0].Rows[0][ExcelTemplateFields.DictionaryName].ToString();
                ModelName = dsRAMTemplate.Tables[0].Rows[0][ExcelTemplateFields.RAM_ModelName].ToString();
                return dictionaryName;
            }
            catch (Exception ex)
            {
                _ErrorMessage = string.Concat("Unable to get the Dictionary Name from the RAM Template Configuration DataSet.     Error: ", ex.Message);
            }

            return string.Empty;
        }

        public bool GenerateSampleExcelTemplate()
        {
            string s = AppFolders_CA.AppDataPathTools;
            s = AppFolders_CA.AppDataPathToolsExcelTemp;

            string pathFile = Path.Combine(AppFolders_CA.AppDataPathToolsExcelTempDicRAM, "RACI_Page.xml");

            if (File.Exists(pathFile))
            {
                return true;
            }

            pathFile = Path.Combine(AppFolders_CA.AppDataPathToolsExcelTempDicRAM, "RACI_Page.xlsx");

            if (!File.Exists(pathFile))
            {
                _ErrorMessage = string.Concat("Responsibility Assignment Matrix (RAM) Excel Template file was not found: ", pathFile);

                return false;
            }

            // ToDo - Check if the Excel template exists
            DataSet ds = CreateTemplateDataSet();

            DataRow row = ds.Tables[ExcelTemplateFields.RAM_TableName].NewRow();
            row[ExcelTemplateFields.UID] = 0;
            row[ExcelTemplateFields.OrgTemplateFile] = "RACI_Page.xlsx";
            row[ExcelTemplateFields.TemplateName] = "RACI_Page";
            row[ExcelTemplateFields.SheetName] = "RACI";
            row[ExcelTemplateFields.ExportTempFor] = "RAM";
            row[ExcelTemplateFields.Description] = "Sample Responsibility Assignment Matrix (RAM)";
            row[ExcelTemplateFields.DataRowStart] = "2";
            row[ExcelTemplateFields.LocNumber] = "A";
            row[ExcelTemplateFields.LocSegText] = "C";
            row[ExcelTemplateFields.LocPageNo] = "B";
            row[ExcelTemplateFields.FontName] = "Calibri";
            row[ExcelTemplateFields.FontSize] = "11";
            row[ExcelTemplateFields.RAM_ModelName] = "RACI";
            row[ExcelTemplateFields.DictionaryName] = "Ship";
            row[ExcelTemplateFields.CreatedBy] = "Professional Document Analyzer";
            row[ExcelTemplateFields.CreatedDate] = String.Format("{0:dddd, MMMM d, yyyy}", DateTime.Now);  // "Sunday, March 9, 2008"

            ds.Tables[ExcelTemplateFields.RAM_TableName].Rows.Add(row);

            DataRow row2 = ds.Tables[ResponsibilityAssMatrixFields.TableName].NewRow();
            row2[ResponsibilityAssMatrixFields.UID] = 0; 
            row2[ResponsibilityAssMatrixFields.Role_Name] = "Responsible";
            row2[ResponsibilityAssMatrixFields.Role_Notation] = "R";
            row2[ResponsibilityAssMatrixFields.Role_Color] = "Turquoise";
            row2[ResponsibilityAssMatrixFields.Dictionary_Category_UID] = 1; // Legal
            row2[ResponsibilityAssMatrixFields.Role_Column] = "H";
            ds.Tables[ResponsibilityAssMatrixFields.TableName].Rows.Add(row2);

            row2 = ds.Tables[ResponsibilityAssMatrixFields.TableName].NewRow();
            row2[ResponsibilityAssMatrixFields.UID] = 1; // Legal
            row2[ResponsibilityAssMatrixFields.Role_Name] = "Accountable";
            row2[ResponsibilityAssMatrixFields.Role_Notation] = "A";
            row2[ResponsibilityAssMatrixFields.Role_Color] = "Tomato";
            row2[ResponsibilityAssMatrixFields.Dictionary_Category_UID] = 1; // Legal
            row2[ResponsibilityAssMatrixFields.Role_Column] = "D"; 
            ds.Tables[ResponsibilityAssMatrixFields.TableName].Rows.Add(row2);

            row2 = ds.Tables[ResponsibilityAssMatrixFields.TableName].NewRow();
            row2[ResponsibilityAssMatrixFields.UID] = 2; 
            row2[ResponsibilityAssMatrixFields.Role_Name] = "Accountable";
            row2[ResponsibilityAssMatrixFields.Role_Notation] = "A";
            row2[ResponsibilityAssMatrixFields.Role_Color] = "Tomato";
            row2[ResponsibilityAssMatrixFields.Dictionary_Category_UID] = 1; // Legal
            row2[ResponsibilityAssMatrixFields.Role_Column] = "E";
            ds.Tables[ResponsibilityAssMatrixFields.TableName].Rows.Add(row2);

            row2 = ds.Tables[ResponsibilityAssMatrixFields.TableName].NewRow();
            row2[ResponsibilityAssMatrixFields.UID] = 3; 
            row2[ResponsibilityAssMatrixFields.Role_Name] = "Accountable";
            row2[ResponsibilityAssMatrixFields.Role_Notation] = "A";
            row2[ResponsibilityAssMatrixFields.Role_Color] = "Tomato";
            row2[ResponsibilityAssMatrixFields.Dictionary_Category_UID] = 1; // Legal
            row2[ResponsibilityAssMatrixFields.Role_Column] = "U";
            ds.Tables[ResponsibilityAssMatrixFields.TableName].Rows.Add(row2);

            row2 = ds.Tables[ResponsibilityAssMatrixFields.TableName].NewRow();
            row2[ResponsibilityAssMatrixFields.UID] = 4;
            row2[ResponsibilityAssMatrixFields.Role_Name] = "Consulted";
            row2[ResponsibilityAssMatrixFields.Role_Notation] = "C";
            row2[ResponsibilityAssMatrixFields.Role_Color] = "SpringGreen";
            row2[ResponsibilityAssMatrixFields.Dictionary_Category_UID] = 1; // Legal
            row2[ResponsibilityAssMatrixFields.Role_Column] = "O";
            ds.Tables[ResponsibilityAssMatrixFields.TableName].Rows.Add(row2);

            row2 = ds.Tables[ResponsibilityAssMatrixFields.TableName].NewRow();
            row2[ResponsibilityAssMatrixFields.UID] = 5;
            row2[ResponsibilityAssMatrixFields.Role_Name] = "Informed";
            row2[ResponsibilityAssMatrixFields.Role_Notation] = "I";
            row2[ResponsibilityAssMatrixFields.Role_Color] = "Gold";
            row2[ResponsibilityAssMatrixFields.Dictionary_Category_UID] = 1; // Legal
            row2[ResponsibilityAssMatrixFields.Role_Column] = "G";
            ds.Tables[ResponsibilityAssMatrixFields.TableName].Rows.Add(row2);

            pathFile = Path.Combine(AppFolders_CA.AppDataPathToolsExcelTempDicRAM, "RACI_Page.xml");

            GenericDataManger gDMgr = new GenericDataManger();
            gDMgr.SaveDataXML(ds, pathFile);

            if (!File.Exists(pathFile))
            {
                _ErrorMessage = string.Concat("Responsibility Assignment Matrix (RAM) Data Template file was not found: ", pathFile);

                return false;
            }


            return true;
        }

        public bool Generate_Default_RAM_Models ()
        {
            Create_RACI_Model();
            Create_RASCI_Model();
            Create_RASI_Model();
            Create_RACIQ_Model();
            Create_RACI_VS_Model();
            Create_RACIO_Model();
            Create_DACI_Model();
            Create_RAPID_Model();
            Create_RATSI_Model();
            Create_DRASCI_Model();
            Create_PDQA_Model();

            return true;
        }

        private void Create_RACI_Model()
        {
            string file = "RACI.ram";
            string pathFile = Path.Combine(AppFolders_CA.AppDataPathToolsRAMDefs, file);

            if (!File.Exists(pathFile))
            {
                GenericDataManger gDmgr = new GenericDataManger();

                // RACI
                DataSet ds = new DataSet();
                DataTable dt = CreateTable_Defs();

                DataRow row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 0;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Responsible";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "R";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Turquoise";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those who will do the work to complete the task";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 1;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Accountable";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "A";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Tomato";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those answerable for the correct and thorough completion of the deliverable or task";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 2;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Consulted";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "C";
                row[ResponsibilityAssMatrixFields.Role_Color] = "SpringGreen";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those whose opinions are sought, typically Subject Matter Experts (SMEs)";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 3;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Informed";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "I";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Gold";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those who are kept up-to-date on progress, often only on completion of the task or deliverable";
                dt.Rows.Add(row);

                ds.Tables.Add(dt);

                gDmgr.SaveDataXML(ds, pathFile);

            }

        }



        private void Create_RASCI_Model()
        {
            // RASCI
 
            string file = "RASCI.ram";
            string pathFile = Path.Combine(AppFolders_CA.AppDataPathToolsRAMDefs, file);

            if (!File.Exists(pathFile))
            {
                GenericDataManger gDmgr = new GenericDataManger();

                DataSet ds = new DataSet();
                DataTable dt = CreateTable_Defs();

                DataRow row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 0;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Responsible";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "R";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Turquoise";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those who will do the work to complete the task";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 1;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Accountable";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "A";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Tomato";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those answerable for the correct and thorough completion of the deliverable or task";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 2;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Support";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "S";
                row[ResponsibilityAssMatrixFields.Role_Color] = "HotPink";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those whose helps complete the task";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 3;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Consulted";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "C";
                row[ResponsibilityAssMatrixFields.Role_Color] = "SpringGreen";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those whose opinions are sought, typically Subject Matter Experts (SMEs)";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 4;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Informed";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "I";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Gold";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those who are kept up-to-date on progress, often only on completion of the task or deliverable";
                dt.Rows.Add(row);

                ds.Tables.Add(dt);

                gDmgr.SaveDataXML(ds, pathFile);

            }
        }

        private void Create_RASI_Model()
        {
            // RASI

            string file = "RASI.ram";
            string pathFile = Path.Combine(AppFolders_CA.AppDataPathToolsRAMDefs, file);

            if (!File.Exists(pathFile))
            {
                GenericDataManger gDmgr = new GenericDataManger();

                DataSet ds = new DataSet();
                DataTable dt = CreateTable_Defs();

                DataRow row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 0;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Responsible";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "R";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Turquoise";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those who will do the work to complete the task";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 1;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Accountable";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "A";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Tomato";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those answerable for the correct and thorough completion of the deliverable or task";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 2;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Support";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "S";
                row[ResponsibilityAssMatrixFields.Role_Color] = "HotPink";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those whose helps complete the task";
                dt.Rows.Add(row);


                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 3;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Informed";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "I";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Gold";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those who are kept up-to-date on progress, often only on completion of the task or deliverable";
                dt.Rows.Add(row);

                ds.Tables.Add(dt);

                gDmgr.SaveDataXML(ds, pathFile);

            }

        }

        private void Create_RACIQ_Model()
        {
            string file = "RACIQ.ram";
            string pathFile = Path.Combine(AppFolders_CA.AppDataPathToolsRAMDefs, file);

            if (!File.Exists(pathFile))
            {
                GenericDataManger gDmgr = new GenericDataManger();

                DataSet ds = new DataSet();
                DataTable dt = CreateTable_Defs();

                DataRow row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 0;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Responsible";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "R";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Turquoise";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those who will do the work to complete the task";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 1;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Accountable";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "A";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Tomato";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those answerable for the correct and thorough completion of the deliverable or task";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 2;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Consulted";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "C";
                row[ResponsibilityAssMatrixFields.Role_Color] = "SpringGreen";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those whose opinions are sought, typically Subject Matter Experts (SMEs)";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 3;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Informed";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "I";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Gold";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those who are kept up-to-date on progress, often only on completion of the task or deliverable";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 4;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Quality Review";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "Q";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Violet";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those who check whether the product/service meets the quality requirements";
                dt.Rows.Add(row);

                ds.Tables.Add(dt);
            }

        }

        private void Create_RACI_VS_Model()
        {
            string file = "RACI-VS.ram";
            string pathFile = Path.Combine(AppFolders_CA.AppDataPathToolsRAMDefs, file);

            if (!File.Exists(pathFile))
            {
                GenericDataManger gDmgr = new GenericDataManger();

                DataSet ds = new DataSet();
                DataTable dt = CreateTable_Defs();

                DataRow row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 0;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Responsible";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "R";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Turquoise";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those who will do the work to complete the task";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 1;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Accountable";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "A";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Tomato";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those answerable for the correct and thorough completion of the deliverable or task";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 2;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Consulted";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "C";
                row[ResponsibilityAssMatrixFields.Role_Color] = "SpringGreen";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those whose opinions are sought, typically Subject Matter Experts (SMEs)";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 3;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Informed";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "I";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Gold";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those who are kept up-to-date on progress, often only on completion of the task or deliverable";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 4;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Verifier";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "V";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Violet";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those who check whether the product meets the acceptance criteria set forth in the product description";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 5;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Signatory";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "S";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Yellow";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those who approve the verify decision and authorize the product hand-off. It seems to make sense that the signatory should be the party being accountable for its success";
                dt.Rows.Add(row);

                ds.Tables.Add(dt);
            }
        }

        private void Create_RACIO_Model()
        {
            string file = "RACIO.ram";
            string pathFile = Path.Combine(AppFolders_CA.AppDataPathToolsRAMDefs, file);

            if (!File.Exists(pathFile))
            {
                GenericDataManger gDmgr = new GenericDataManger();

                // RACI
                DataSet ds = new DataSet();
                DataTable dt = CreateTable_Defs();

                DataRow row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 0;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Responsible";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "R";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Turquoise";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those who will do the work to complete the task";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 1;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Accountable";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "A";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Tomato";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those answerable for the correct and thorough completion of the deliverable or task";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 2;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Consulted";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "C";
                row[ResponsibilityAssMatrixFields.Role_Color] = "SpringGreen";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those whose opinions are sought, typically Subject Matter Experts (SMEs)";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 3;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Informed";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "I";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Gold";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those who are kept up-to-date on progress, often only on completion of the task or deliverable";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 4;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Out of the Loop";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "O";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Yellow";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Designating individuals or groups who are specifically not part of the task. Specifying that a resource does not participate can be as beneficial to a task's completion as specifying those who do participate";
                dt.Rows.Add(row);

                ds.Tables.Add(dt);

                gDmgr.SaveDataXML(ds, pathFile);

            }

        }

        private void Create_DACI_Model()
        {
            string file = "DACI.ram";
            string pathFile = Path.Combine(AppFolders_CA.AppDataPathToolsRAMDefs, file);

            if (!File.Exists(pathFile))
            {
                GenericDataManger gDmgr = new GenericDataManger();

                // RACI
                DataSet ds = new DataSet();
                DataTable dt = CreateTable_Defs();

                DataRow row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 0;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Driver";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "D";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Turquoise";
                row[ResponsibilityAssMatrixFields.Role_Description] = "A single driver of overall project like the person steering a car.";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 1;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Approver";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "A";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Tomato";
                row[ResponsibilityAssMatrixFields.Role_Description] = "One or more approvers who make most project decisions, and are responsible if it fails.";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 2;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Contributors";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "C";
                row[ResponsibilityAssMatrixFields.Role_Color] = "SpringGreen";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Are the worker-bees who are responsible for deliverables; and with whom there is two-way communication.";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 3;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Informed";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "I";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Gold";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those who are impacted by the project and are provided status and informed of decisions; and with whom there is one-way communication.";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 4;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Out of the Loop";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "O";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Yellow";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Designating individuals or groups who are specifically not part of the task. Specifying that a resource does not participate can be as beneficial to a task's completion as specifying those who do participate";
                dt.Rows.Add(row);

                ds.Tables.Add(dt);

                gDmgr.SaveDataXML(ds, pathFile);

            }
        }

        private void Create_RAPID_Model()
        {
            string file = "RAPID.ram";
            string pathFile = Path.Combine(AppFolders_CA.AppDataPathToolsRAMDefs, file);

            if (!File.Exists(pathFile))
            {
                GenericDataManger gDmgr = new GenericDataManger();

                // RAPID
                DataSet ds = new DataSet();
                DataTable dt = CreateTable_Defs();

                DataRow row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 0;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Recommend";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "R";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Turquoise";
                row[ResponsibilityAssMatrixFields.Role_Description] = "The Recommend role typically involves 80 percent of the work in a decision. The recommender gathers relevant input and proposes a course of action—sometimes alternative courses, complete with pros and cons so that the decision maker's choices are as clear, simple and timely as possible.";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 1;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Agree";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "A";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Tomato";
                row[ResponsibilityAssMatrixFields.Role_Description] = "The Agree role represents a formal approval of a recommendation. The 'A' and the 'R' should work together to come to a mutually satisfactory proposal to bring forward to the Decider. But not all decisions will need an Agree role, as this is typically reserved for those situations where some form of regulatory or compliance sign-off is required.";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 2;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Perform";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "P";
                row[ResponsibilityAssMatrixFields.Role_Color] = "SpringGreen";
                row[ResponsibilityAssMatrixFields.Role_Description] = "The Perform role defines who is accountable for executing or implementing the decision once it is made. Best-practice companies typically define P's and gather input from them early in the process";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 3;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Input";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "I";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Gold";
                row[ResponsibilityAssMatrixFields.Role_Description] = "The Input role provides relevant information and facts so that the Recommender and Decider can assess all the relevant facts to make the right decision. However, the 'I' role is strictly advisory. Recommenders should consider all input, but they don't have to reflect every point of view in the final recommendation.";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 4;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Decide";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "D";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Yellow";
                row[ResponsibilityAssMatrixFields.Role_Description] = "The Decide role is for the single person who ultimately is accountable for making the final decision, committing the group to action and ensuring the decision gets implemented.";
                dt.Rows.Add(row);

                ds.Tables.Add(dt);

                gDmgr.SaveDataXML(ds, pathFile);

            }
        }

        private void Create_RATSI_Model()
        {
            string file = "RATSI.ram";
            string pathFile = Path.Combine(AppFolders_CA.AppDataPathToolsRAMDefs, file);

            if (!File.Exists(pathFile))
            {
                GenericDataManger gDmgr = new GenericDataManger();

                DataSet ds = new DataSet();
                DataTable dt = CreateTable_Defs();

                DataRow row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 0;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Responsibility";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "R";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Turquoise";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Identify who is in charge of making sure the work is done.";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 1;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Authority";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "A";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Tomato";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Identify who has final decision power on the work.";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 2;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Task";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "T";
                row[ResponsibilityAssMatrixFields.Role_Color] = "SpringGreen";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Identify who actually does the work.";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 3;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Support";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "S";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Gold";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Identify who is involved to provide support to the work.";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 4;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Informed";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "I";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Yellow";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Identify who is informed that the work has been done (or will be started)";
                dt.Rows.Add(row);

                ds.Tables.Add(dt);

                gDmgr.SaveDataXML(ds, pathFile);

            }

        }

        private void Create_DRASCI_Model()
        {
            string file = "DRASCI.ram";
            string pathFile = Path.Combine(AppFolders_CA.AppDataPathToolsRAMDefs, file);

            if (!File.Exists(pathFile))
            {
                GenericDataManger gDmgr = new GenericDataManger();

                DataSet ds = new DataSet();
                DataTable dt = CreateTable_Defs();

                DataRow row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 0;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Driver";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "D";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Yellow";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Identify who is in charge of making sure the work is done.";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 1;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Responsibility";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "R";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Turquoise";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Identify who is in charge of making sure the work is done.";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 2;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Authority";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "A";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Tomato";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Identify who has final decision power on the work.";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 3;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Task";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "T";
                row[ResponsibilityAssMatrixFields.Role_Color] = "SpringGreen";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Identify who actually does the work.";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 4;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Support";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "S";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Gold";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Identify who is involved to provide support to the work.";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 4;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Informed";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "I";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Yellow";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Identify who is informed that the work has been done (or will be started)";
                dt.Rows.Add(row);

                ds.Tables.Add(dt);

                gDmgr.SaveDataXML(ds, pathFile);

            }

        }

        private void Create_PDQA_Model()
        {
            string file = "PDQA.ram";
            string pathFile = Path.Combine(AppFolders_CA.AppDataPathToolsRAMDefs, file);

            if (!File.Exists(pathFile))
            {
                GenericDataManger gDmgr = new GenericDataManger();

                DataSet ds = new DataSet();
                DataTable dt = CreateTable_Defs();

                DataRow row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 0;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Primary";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "P";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Turquoise";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Provides skill-based effort within capacity to complete scope and also manages dependencies through coordination.";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 1;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Decision";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "D";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Tomato";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Handles any decision, including scope acceptable and exception handling decisions leading to rework.(Does not generation nominal scope).";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 2;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Quality";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "Q";
                row[ResponsibilityAssMatrixFields.Role_Color] = "SpringGreen";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Reviews scope as it progresses to detect poor quality and escalates to decision maker as so. (Does not general nominal scope).";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 4;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Assist";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "A";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Gold";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Provides skill based effort with capacity to complete scope, in assistance to the primary. (Does not manage dependencies through coordination).";
                dt.Rows.Add(row);

                ds.Tables.Add(dt);

                gDmgr.SaveDataXML(ds, pathFile);

            }

        }

        private void Create_ARCI_Model()
        {
            string file = "ARCI.ram";
            string pathFile = Path.Combine(AppFolders_CA.AppDataPathToolsRAMDefs, file);

            if (!File.Exists(pathFile))
            {
                GenericDataManger gDmgr = new GenericDataManger();

                DataSet ds = new DataSet();
                DataTable dt = CreateTable_Defs();

                DataRow row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 0;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Accountable";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "A";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Turquoise";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Authorized to approve an answer to the decision.";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 1;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Responsible";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "R";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Tomato";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Responsible to recommend an answer to the decision.";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 2;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Consulted";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "C";
                row[ResponsibilityAssMatrixFields.Role_Color] = "SpringGreen";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those whose opinions are sought; and with whom there is two-way communication.";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[ResponsibilityAssMatrixFields.UID] = 4;
                row[ResponsibilityAssMatrixFields.Role_Name] = "Informed";
                row[ResponsibilityAssMatrixFields.Role_Notation] = "I";
                row[ResponsibilityAssMatrixFields.Role_Color] = "Gold";
                row[ResponsibilityAssMatrixFields.Role_Description] = "Those who are informed after the decision is made; and with whom there is one-way communication.";
                dt.Rows.Add(row);

                ds.Tables.Add(dt);

                gDmgr.SaveDataXML(ds, pathFile);

            }


        }


        public  DataTable CreateTable_Defs()
        {
            DataTable table = new DataTable(ResponsibilityAssMatrixFields.TableName);

            table.Columns.Add(ResponsibilityAssMatrixFields.UID, typeof(int));
            table.Columns.Add(ResponsibilityAssMatrixFields.Role_Name, typeof(string));
            table.Columns.Add(ResponsibilityAssMatrixFields.Role_Notation, typeof(string));
            table.Columns.Add(ResponsibilityAssMatrixFields.Role_Color, typeof(string));
            table.Columns.Add(ResponsibilityAssMatrixFields.Role_Description, typeof(string));

            return table;
        }

    }
}
