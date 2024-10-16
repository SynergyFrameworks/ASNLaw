using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using WorkgroupMgr;
using Atebion.Outlook;
using MetroFramework.Forms;



namespace MatrixBuilder
{
    public partial class ucWorkGroups : UserControl
    {
        public ucWorkGroups()
        {
            InitializeComponent();
           
        }

        // Declare delegate for when a workgroup has been selected
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when a Workgroup is selected")]
        public event ProcessHandler WorkgroupSelected;

        WorkgroupMgr.Workgroups _workgroupMgr;
        WorkgroupMgr.ListMgr _listMgr;
        WorkgroupMgr.DocTypesMgr _docTypeMgr;
        WorkgroupMgr.RefResMgr _refResMgr;
        WorkgroupMgr.SBMgr _sbMgr;

        bool _isLocal = true;
        DataTable _dtWorkgroups;

        Atebion.Outlook.Email _EmailOutLook;

        private Modes _currentMode = Modes.Workgroups;
        private enum Modes
        {
            Workgroups = 0,
            Lists = 1,
            DocTypes = 2,
            RefResources = 3,
            Matrices = 4,
            Storyboards =5
        }

        List<string> _Members = new List<string>();

        private string _ListSelected = string.Empty;
        private string _DocTypeSelected = string.Empty;
        private string _RefResSelected = string.Empty;
        private string _RefResContentSelected = string.Empty;
        private string _MatrixSelected = string.Empty;
        private string _StoryboardSelected = string.Empty;

        private string _refRespath = string.Empty;
        private string _doctypePath = string.Empty;
        private string _listPath = string.Empty;
        private string _matrixTempPath = string.Empty;
        private string _storyboardPath = string.Empty;
        
        private string _matrixTempPathTemp = string.Empty;

        private string _workgroupRootPath = string.Empty;
        public string WorkgroupRootPath
        {
            get { return _workgroupRootPath; }
            set { _workgroupRootPath = value; }
        }

        private string _projectRootPath = string.Empty;
        public string ProjectRootPath
        {
            get { return _projectRootPath; }
        }

        private string _matrixTempPathTemplates = string.Empty;
        public string MatrixTempPathTemplates
        {
            get { return _matrixTempPathTemplates; }
        }

        public string ListPath
        {
            get { return _listPath; }
        }

        public string RefResPath
        {
            get { return _refRespath; }
        }

        public string StoryboardPath
        {
            get { return _storyboardPath; }
        }



        private string _WorkspaceCurrent = string.Empty;

        public string Workgroup
        {
            get { return _WorkspaceCurrent; }
            set
            {
                _WorkspaceCurrent = value;

                if (lstbWorkgroups.Items.Count > 0)
                {
                    lstbWorkgroups.SelectedIndex = lstbWorkgroups.FindString(_WorkspaceCurrent);
                }
            }
        }

        public bool LoadData(string workgroupName)
        {
            
            if (workgroupName == string.Empty)
            {            
                _WorkspaceCurrent = "Local";
            }
            else
            {
                if (_WorkspaceCurrent == workgroupName)
                {
                    if (_workgroupMgr != null)
                        return true;
                }

                _WorkspaceCurrent = workgroupName;
            }

            if (_WorkspaceCurrent =="Local")
            {
                _isLocal = true;
                //MessageBox.Show("UserNamer=" + AppFolders.UserName); // ToDo Remove after Testing 
                //MessageBox.Show("UserInfoPathFile=" + AppFolders.UserInfoPathFile); // ToDo Remove after Testing 
                _workgroupMgr = new WorkgroupMgr.Workgroups(AppFolders.AppDataPath, AppFolders.UserName, AppFolders.UserInfoPathFile);
            }
            else
            {
                _isLocal = false;
                _workgroupMgr = new WorkgroupMgr.Workgroups(AppFolders.AppDataPath, _workgroupRootPath, AppFolders.UserName, AppFolders.UserInfoPathFile);
            }

            if (_workgroupMgr == null)
            {
                MessageBox.Show(_workgroupMgr.ErrorMessage, "Unable to Open Workgroup Manager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lblMessage.Text = _workgroupMgr.ErrorMessage;
                lblMessage.ForeColor = Color.Tomato;
                return false;
            }

            if (!_workgroupMgr.ValidateFix(_isLocal))
            {
                MessageBox.Show(_workgroupMgr.ErrorMessage, "Failed Workgroup Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lblMessage.Text = _workgroupMgr.ErrorMessage;
                lblMessage.ForeColor = Color.Tomato;
                return false;
            }

            _dtWorkgroups = _workgroupMgr.GetWorkGroupList();
            if (_dtWorkgroups == null)
            {
                MessageBox.Show(_workgroupMgr.ErrorMessage, "Unable to Get Workgroups", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lblMessage.Text = _workgroupMgr.ErrorMessage;
                lblMessage.ForeColor = Color.Tomato;
                return false;
            }

            if (_dtWorkgroups.Rows.Count == 0)
            {
                string msg = "No Workgroups were found!";
                MessageBox.Show(msg, "No Workgroups", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lblMessage.Text = msg;
                lblMessage.ForeColor = Color.Tomato;
                return false;
            }

     //       MessageBox.Show("_dtWorkgroups.Rows.Count=" + _dtWorkgroups.Rows.Count.ToString()); // ToDo Remove after Testing 
            LoadWorkgroups(_dtWorkgroups); // Load workgroups

     //       MessageBox.Show("Workgroups Loaded"); // ToDo Remove after Testing 

            Reset2DefaultView();

            return true;
        }

        public bool AddNew()
        {
         //   int index = -1;

            switch (_currentMode)
            {
                case Modes.Workgroups:
                    frmWorkGroup frmWorkgrp = new frmWorkGroup(AppFolders.AppDataPath, AppFolders.UserName, AppFolders.UserInfoPathFile);

                    if (frmWorkgrp.ShowDialog(this) == DialogResult.Cancel)
                        return false;

                    _dtWorkgroups = _workgroupMgr.GetWorkGroupList();
                    if (_dtWorkgroups == null)
                    {
                        MessageBox.Show(_workgroupMgr.ErrorMessage, "Unable to Get Workgroups", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        lblMessage.Text = _workgroupMgr.ErrorMessage;
                        lblMessage.ForeColor = Color.Tomato;
                        return false;
                    }

                    LoadWorkgroups(_dtWorkgroups);

                    break;

                case Modes.Lists:
                    //index = lstbLists.SelectedIndex;
                    //if (index == -1)
                    //    return false;

                    frmLists frm = new frmLists(_listMgr);
                    if (frm.ShowDialog(this) == DialogResult.Cancel)
                        return false;

                    LoadLists();

                    //try // Put try in case another user deleted a List while you were editing a List
                    //{
                    //    lstbLists.SelectedIndex = index;
                    //}
                    //catch
                    //{
                    //    return true;
                    //}
                    break;

                case Modes.DocTypes:
                    //index = lstbDocTypes.SelectedIndex;
                    //if (index == -1)
                    //    return false;

                    frmDocTypes frm2 = new frmDocTypes(_docTypeMgr);
                    if (frm2.ShowDialog(this) == DialogResult.Cancel)
                        return false;

                    LoadDocTypes();

                    //try // Put try in case another user deleted a List while you were editing a List
                    //{
                    //    lstbDocTypes.SelectedIndex = index;
                    //}
                    //catch
                    //{
                    //    return true;
                    //}

                    break;

                case Modes.RefResources:

                    frmRefRes frm3 = new frmRefRes(_refResMgr);

                    if (frm3.ShowDialog(this) == DialogResult.Cancel)
                        return false;

                    LoadRefRes();

                    break;

                case Modes.Matrices:
                    frmMatrixTemplate frm4 = new frmMatrixTemplate(_doctypePath, _listPath, _refRespath, _matrixTempPath, _matrixTempPathTemp, _matrixTempPathTemplates);

                    if (frm4.ShowDialog(this) == DialogResult.Cancel)
                        return false;

                     LoadMatrices();

                    break;

                case Modes.Storyboards:
                    frmSBTemplate frm5 = new frmSBTemplate(_storyboardPath, _matrixTempPathTemplates, _matrixTempPathTemp, true);

                    if (frm5.ShowDialog(this) == DialogResult.Cancel)
                        return false;

                    LoadSB();

                    break;


            }
            return true;
        }

        public bool Edit()
        {

            int index = -1;

            switch (_currentMode)
            {
                case Modes.Workgroups:
                    MessageBox.Show("You cannot edit a Workgroup, but you can modify workgroup components (e.g. Lists, Document Types, Reference Resources, Matrices, and Storyboards).", "Select a Workgroup Component", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                    break;

                case Modes.Lists:
                    index = lstbLists.SelectedIndex;
                    if (index == -1)
                        return false;

                    frmLists frm = new frmLists(_ListSelected, _listMgr);
                    if (frm.ShowDialog(this) == DialogResult.Cancel)
                        return false;

                    LoadLists();
                    try // Put try in case another user deleted a List while you were editing a List
                    {
                        lstbLists.SelectedIndex = index;
                    }
                    catch
                    {
                        return true;
                    }
                    


                    break;

                case Modes.DocTypes:
                    index = lstbLists.SelectedIndex;
                    if (index == -1)
                        return false;

                    frmDocTypes frm2 = new frmDocTypes(_DocTypeSelected, _docTypeMgr);
                    if (frm2.ShowDialog(this) == DialogResult.Cancel)
                        return false;

                    LoadDocTypes();
                    try // Put try in case another user deleted a List while you were editing a List
                    {
                        lstbDocTypes.SelectedIndex = index;
                    }
                    catch
                    {
                        return true;
                    }


                    break;

                case Modes.RefResources:
                    string refResEditMsg = string.Concat("Once a Reference Resource has been created, you cannot Edit it.", Environment.NewLine, Environment.NewLine, "You can remove a Reference Resource, but the content and assocated folders are not deleted.");
                    MessageBox.Show(refResEditMsg, "Reference Resources Cannot be Edited", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    break;

                case Modes.Matrices:

                    if (_MatrixSelected.Length == 0)
                        _MatrixSelected = lstMatrix.Text;

                    if (_MatrixSelected.Length == 0)
                        return false;

                    frmMatrixTemplate frm4 = new frmMatrixTemplate(_doctypePath, _listPath, _refRespath, _matrixTempPath, lblMatrixName.Text, _matrixTempPathTemp, _matrixTempPathTemplates);

                    if (frm4.ShowDialog(this) == DialogResult.Cancel)
                        return false;

                     LoadMatrices();

                    break;

                case Modes.Storyboards:
                    if (_StoryboardSelected.Length == 0)
                        _StoryboardSelected = lstSBs.Text;

                    if (_StoryboardSelected.Length == 0)
                        return false;

                    frmSBTemplate frm5 = new frmSBTemplate(_StoryboardSelected, _storyboardPath, _matrixTempPathTemplates);

                    if (frm5.ShowDialog(this) == DialogResult.Cancel)
                        return false;

                    LoadSB();

                    break;


            }
            return true;
        }

        public bool Delete()
        {
            int index = -1;
            string msg = string.Empty;

            switch (_currentMode)
            {
                case Modes.Workgroups:

                    if (_WorkspaceCurrent == "Local")
                    {
                        msg = "The Local Workgroup is your Default Workgroup on your computer.";
                        MessageBox.Show(msg, "Unable to remove your Local Workgroup.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }

                    msg = string.Concat("Are you sure you want to disconnect from Workgroup '", _WorkspaceCurrent, "' ?", Environment.NewLine, Environment.NewLine, "If you want, you can reconnect to this Workgroup again in the future.");

                    if (MessageBox.Show(msg, "Confirm Disconnection from Workgroup", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return false;

                    DataTable dt;

                    if (!_workgroupMgr.WorkgroupConnection_Remove(_WorkspaceCurrent, out dt))
                    {
                        MessageBox.Show(_workgroupMgr.ErrorMessage, "Unable to Disconnect from WorkGroup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (dt == null)
                    {
                        MessageBox.Show("Datatable was not returned", "Unable to Disconnect from WorkGroup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    LoadWorkgroups(dt);

                    if (lstbWorkgroups.Items.Count > 0)
                        lstbWorkgroups.SelectedIndex = 0;

                    break;

                case Modes.Lists:
                    index = lstbLists.SelectedIndex;
                    if (index == -1)
                        return false;

                    msg = string.Concat("Are you sure you want to Delete the selected List '", _ListSelected, "'?", Environment.NewLine, Environment.NewLine, "A Backup of this List will be generated.");
                    if (MessageBox.Show(msg, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return false;

                    lstbListItems.Items.Clear();

                    _ListSelected = lstbLists.Text;

                    _listMgr.DeleteList(_ListSelected);

                    _ListSelected = string.Empty;

                    LoadLists();

                    break;

                case Modes.DocTypes:
                    index = lstbDocTypes.SelectedIndex;
                    if (index == -1)
                        return false;

                    msg = string.Concat("Are you sure you want to Delete the selected Document Type '", _DocTypeSelected, "'?", Environment.NewLine, Environment.NewLine, "A Backup of this Document Type will be generated.");
                    if (MessageBox.Show(msg, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return false;

                    lstbListItems.Items.Clear();

                    _DocTypeSelected = lstbDocTypes.Text;

                    _docTypeMgr.DeleteDocType(_DocTypeSelected);

                    _DocTypeSelected = string.Empty;

                    LoadDocTypes();

                    break;

                case Modes.RefResources:
                    msg = string.Concat("Are you sure you want to Remove the selected Reference Resource '", _RefResSelected, "'?", Environment.NewLine, Environment.NewLine, "Reference Resource Content and assocated folders are Not deleted.");
                    if (MessageBox.Show(msg, "Confirm Reference Resource Connection Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return false;

                    if (!_refResMgr.DeleteRefRes(_RefResSelected))
                    {
                        if (_refResMgr.ErrorMessage.Length > 0)
                        {
                            MessageBox.Show(_refResMgr.ErrorMessage, "Reference Resource Connection was Not Removed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        LoadRefRes();
                    }
                    break;

                case Modes.Matrices:
                    
                    string matrixSelected = lstMatrix.Text;
                    if (_MatrixSelected.Length == 0)
                        return false;

                    msg = string.Format("Are you sure you want to Remove Matrix '{0}'?", matrixSelected);
                    if (MessageBox.Show(msg, "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return false;

                    string selectedTemplatePath = Path.Combine(_matrixTempPathTemplates, _MatrixSelected);

                    string prifix = "~";
                    string newPath = selectedTemplatePath;

                    while (Directory.Exists(newPath))
                    {
                        newPath = string.Concat(_matrixTempPathTemplates, @"\", prifix, _MatrixSelected);               
                        if (Directory.Exists(newPath))
                        {
                            prifix = "~" + prifix;
                        }
                        else
                        {
                            break;
                        }
                    }

                    try
                    {
                        Directory.Move(selectedTemplatePath, newPath);
                        lblMatrixName.Text = string.Empty;
                        lblMatrixDescription.Text = string.Empty;
                        lblSummaryText.Text = string.Empty;
                        reoGridControl2.Visible = false;
                        this.Refresh();

                    }
                    catch (Exception ex)
                    {
                        msg = string.Concat("An error occurred while attempting to remove a selected Matrix Template.", Environment.NewLine, Environment.NewLine, "Error: ", ex.Message);
                        MessageBox.Show(msg, "An Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    LoadMatrices();

                    break;

                case Modes.Storyboards:
                    _StoryboardSelected = lstSBs.Text;
                    if (_StoryboardSelected == string.Empty)
                        return false;

                    msg = string.Format("Are you sure you want to Remove Storyboard '{0}'?", _StoryboardSelected);
                    if (MessageBox.Show(msg, "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return false;

                    documentViewer1.CloseDocument();
                    documentViewer1.Visible = false;
                    reoGridControl1.Visible = false;

                    if (!_sbMgr.DeleteSBTemplate(_StoryboardSelected))
                    {
                        MessageBox.Show(_sbMgr.ErrorMessage, "An Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LoadSB(); // reload Storyboards anyway. The error may have occurred after the Data Row delete
                        return false;
                    }

                    LoadSB();

                    break;


            }
            return true;
        }

        private void Reset2DefaultView()
        {
            splitContLists.Visible = false;
            splitContLists.Dock = DockStyle.None;

            splitContWorkgroups.Visible = true;
            splitContWorkgroups.Dock = DockStyle.Fill;

            picDiagram.Visible = true;

            panMembers.Visible = false;
            tileList.Visible = false;
            tileDocTypes.Visible = false;
            tileRefResources.Visible = false;
            tileMatrixTemp.Visible = false;
            tileSBTemp.Visible = false;
        }

        private void ClearRefResSettings()
        {
            lstbRefRes.DataSource = null;                     
            lblRefResName.Text = string.Empty;

            ClearRefResContentSettings();
        }

        private void ClearRefResContentSettings()
        {
            lstbRefResContNames.DataSource = null;
            lblRefResContText.Text = string.Empty;

            
        }

        private int LoadMatrices() 
        {
            txtbSummaryText.Text = string.Empty;

            if (_isLocal)
            {
                _matrixTempPath = _workgroupMgr.PathLocalWorkgroupMatrixTemp;
                _matrixTempPathTemp = _workgroupMgr.PathLocalWorkgroupMatrixTempTemp;
                _matrixTempPathTemplates = _workgroupMgr.PathLocalWorkgroupMatrixTemplates;
            }
            else
            {
                _matrixTempPath = _workgroupMgr.PathWorkgroupMatrixTemp;
                _matrixTempPathTemp = _workgroupMgr.PathWorkgroupMatrixTempTemp;
                _matrixTempPathTemplates = _workgroupMgr.PathWorkgroupMatrixTemplates;
            }

            if (_matrixTempPath == string.Empty)
            {
                MessageBox.Show("Matrix Temple folder has not been set.", "Unable to Load Matrix Templates", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }

            lstMatrix.Items.Clear();
            txtbSummaryText.Text = string.Empty;
            reoGridControl2.Visible = false;
            int count = 0;
            string[] matrices = Directory.GetDirectories(_matrixTempPathTemplates);
            string matrix = string.Empty;

            foreach (string matrixFolder in matrices)
            {
                matrix = Directories.GetLastFolder(matrixFolder);
                if (matrix.IndexOf('~') == -1)
                {
                    lstMatrix.Items.Add(matrix);
                    count++;
                }
            }

            if (count == 0) // Added 02.13.2020
            {
                if (CopyDefaultMatrix())
                    count = 1;
            }
          
            if (count > 9)
                tileMatrixTemp.Text = string.Concat("Matrix Templates       ", count.ToString());
            else
                tileMatrixTemp.Text = string.Concat("Matrix Templates          ", count.ToString());

            if (count > 0)
                lstMatrix.SelectedIndex = 0;

            return count;
        }


        private bool CopyDefaultMatrix() // Added 02.13.2020
        {
            string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

            string appXRefPath = Path.Combine(appPath, "Settings", "Tools", "ExcelTemp", "XRef");

            if (!Directory.Exists(appXRefPath))
                return false;

            string xrefShipleyPath = Path.Combine(_matrixTempPathTemplates, "ShipleyComplianceMatrix");
            if (!Directory.Exists(xrefShipleyPath))
            {
                Directory.CreateDirectory(xrefShipleyPath);
            }

            if (!Directory.Exists(xrefShipleyPath))
                return false;

            string[] files = Directory.GetFiles(appXRefPath);
            string fileName = string.Empty;
            foreach (string file in files)
            {
                fileName = Files.GetFileName(file);
                Files.CopyFile2NewLocation(appXRefPath, xrefShipleyPath, fileName, fileName);
            }

            lstMatrix.Items.Add("ShipleyComplianceMatrix");

            return true;
        }

        private int LoadSB()
        {

            lblStoryboardCaption.Text = string.Empty;
            lblStoryboardName.Text = string.Empty;

            if (_isLocal)
                _storyboardPath = _workgroupMgr.PathLocalWorkgroupMatrixTempSB;
            else
                _storyboardPath = _workgroupMgr.PathWorkgroupMatrixTempSB;

            _sbMgr = new SBMgr(_storyboardPath);

            string[] sbNames = _sbMgr.GetSBNames();
            if (sbNames == null)
            {
                if (_sbMgr.ErrorMessage != string.Empty)
                {
                    MessageBox.Show(_sbMgr.ErrorMessage, "Unable to Load Storyboard List", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
                else
                {
                    MessageBox.Show("No Storyboard were found.", "Unable to Load Storyboard List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return 0;
                }
            }

            lstSBs.DataSource = sbNames;

            int count = sbNames.Length;

            if (count > 9)
                tileSBTemp.Text = string.Concat("Storyboards Templates   ", count.ToString());
            else
                tileSBTemp.Text = string.Concat("Storyboards Templates       ", count.ToString());



            return 0;
        }

        private int LoadRefRes()
        {
            int count = 0;

            ShowHideRefResContentButton(false);
            ClearRefResSettings();

            
            if (_isLocal)
                _refRespath = _workgroupMgr.PathLocalWorkgroupDataSources;
            else
                _refRespath = _workgroupMgr.PathWorkgroupDataSources;

            _refResMgr = new RefResMgr(_refRespath);


            string[] refResNames =  _refResMgr.GetRefResNames();
            if (refResNames == null)
            {
                if (_refResMgr.ErrorMessage.Length > 0)
                {
                    MessageBox.Show(_refResMgr.ErrorMessage, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return 0;
                }
            }

            if (refResNames.Length > 0)
            {
                lstbRefRes.DataSource = refResNames;

                count = refResNames.Length;
            }
            else
            {
                if (_refResMgr.ErrorMessage.Length > 0)
                {
                    MessageBox.Show(_refResMgr.ErrorMessage, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return 0;
                }
            }

            
            if (count > 9)
                tileRefResources.Text = string.Concat("Ref Resources       ", count.ToString());
            else
                tileRefResources.Text = string.Concat("Ref Resources           ", count.ToString());

            if (count > 0)
                ShowHideRefResContentButton(true);

            return count;
        }

        private int LoadDocTypes()
        {
            int count = 0;
            // Load Document Types
            if (_isLocal)
                _doctypePath = _workgroupMgr.PathLocalWorkgroupDocsType;
            else
                _doctypePath = _workgroupMgr.PathWorkgroupDocsType;

            _docTypeMgr = new DocTypesMgr(_doctypePath);

            lstbDocTypes.DataSource = null;
            dvgDocTypeItems.DataSource = null;

            string[] docTypes = _docTypeMgr.GetDocTypesNames();
            if (docTypes == null)
            {
                MessageBox.Show(_docTypeMgr.ErrorMessage, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (docTypes.Length > 0)
            {
                lstbDocTypes.DataSource = docTypes;

                count = docTypes.Length;
            }
            else
            {
                if (_docTypeMgr.ErrorMessage.Length > 0)
                {
                    MessageBox.Show(_docTypeMgr.ErrorMessage, "Notice!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return 0;
                }
            }

            if (count > 9)
                tileDocTypes.Text = string.Concat("Document Types     ", count.ToString());
            else
                tileDocTypes.Text = string.Concat("Document Types       ", count.ToString());

            return count;
        }

        private int LoadLists()
        {
            int count = 0;
            // Load Lists
            if (_isLocal)
                _listPath = _workgroupMgr.PathLocalWorkgroupList;
            else
                _listPath = _workgroupMgr.PathWorkgroupList;

            _listMgr = new ListMgr(_listPath);
            lstbLists.DataSource = null;
            lstbListItems.DataSource = null;

            string[] lists = _listMgr.GetListNames();
            if (lists == null)
            {
                MessageBox.Show(_listMgr.ErrorMessage, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (lists.Length > 0)
            {
                lstbLists.DataSource = lists;
                count = lists.Length;
            }
            else
            {
                if (_listMgr.ErrorMessage.Length > 0)
                {
                    MessageBox.Show(_listMgr.ErrorMessage, "Notice!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return 0;
                }
            }

            if (count > 9)
                tileList.Text = string.Concat("List           ", count.ToString());
            else
                tileList.Text = string.Concat("List              ", count.ToString());

            return count;
        }

        private int LoadWorkgroups(DataTable dt)
        {
        //    picWorkgroupType.Load("folder_puzzle.png"); // Causes an error

            int counter = 0;

            lstbWorkgroups.Items.Clear();

            //int uid;
            string workgroupName;
            //string description;
            //string rootPath;
            foreach (DataRow row in dt.Rows)
            {
                //uid = (int)row[WorkgroupMgr.WorkgroupCatalogueFields.UID];
                workgroupName = row[WorkgroupMgr.WorkgroupCatalogueFields.WorkgroupName].ToString();
                //description = row[WorkgroupMgr.WorkgroupCatalogueFields.WorkgroupDescription].ToString();
                //rootPath = row[WorkgroupMgr.WorkgroupCatalogueFields.WorkgroupRootPath].ToString();

                lstbWorkgroups.Items.Add(workgroupName);
            }

            counter++;


            //exListBox1.Items.Clear();

            //string applicationPath = Application.StartupPath; 

            //Image imglocal = Image.FromFile(Path.Combine(applicationPath, "home-variant.png"));
            //Image imgWorkgroup_good = Image.FromFile(@"folder_puzzle.png");
            //Image imgWorkgroup_bad = Image.FromFile(@"folder_lock.png");

            //int uid;
            //string workgroupName;
            //string description;
            //string rootPath;
            //foreach (DataRow row in dt.Rows)
            //{
            //   uid = (int) row[WorkgroupMgr.WorkgroupCatalogueFields.UID];
            //   workgroupName = row[WorkgroupMgr.WorkgroupCatalogueFields.WorkgroupName].ToString();
            //   description = row[WorkgroupMgr.WorkgroupCatalogueFields.WorkgroupDescription].ToString();
            //   rootPath = row[WorkgroupMgr.WorkgroupCatalogueFields.WorkgroupRootPath].ToString();

            //   if (uid == 0)
            //   {
            //       exListBox1.Items.Add(new exListBoxItem(uid, workgroupName, description, imglocal));
            //   }
            //   else
            //   {
            //       if (Directory.Exists(rootPath))
            //           exListBox1.Items.Add(new exListBoxItem(uid, workgroupName, description, imgWorkgroup_good));
            //       else
            //           exListBox1.Items.Add(new exListBoxItem(uid, workgroupName, description, imgWorkgroup_bad));
            //   }

            //   counter++;
            //}

            //exListBox1.Refresh();

            return counter;
        }


        private void ucWorkGroups_Load(object sender, EventArgs e)
        {
          //  if (DesignMode) return;
         //   LoadData(true);
 
        }

        public void Retrive()
        {
            throw new NotImplementedException();
        }

        private void metroTile4_Click(object sender, EventArgs e) // Matrix tile
        {
            splitContWorkgroups.Visible = false;
            splitContWorkgroups.Dock = DockStyle.None;

            splitContMatrix.Dock = DockStyle.Fill;
            splitContMatrix.Visible = true;

            _currentMode = Modes.Matrices;
        }

        public bool WorkgroupChanged()
        {
            picDiagram.Visible = false;
            panMembers.Visible = true;
            tileList.Visible = true;
            tileDocTypes.Visible = true;
            tileRefResources.Visible = true;
            tileMatrixTemp.Visible = true;
            tileSBTemp.Visible = true;

            lstbMembers.Items.Clear();
            lblMemberInfo.Text = string.Empty;

            int selectedIndex = lstbWorkgroups.SelectedIndex;

            if (selectedIndex == -1)
                return false;

            lblWorkgroupDescription.ForeColor = Color.White;

            DataRow row = _dtWorkgroups.Rows[selectedIndex];

            // uid = (int)row[WorkgroupMgr.WorkgroupCatalogueFields.UID];
            string workgroupName = row[WorkgroupMgr.WorkgroupCatalogueFields.WorkgroupName].ToString();
            string description = row[WorkgroupMgr.WorkgroupCatalogueFields.WorkgroupDescription].ToString();
            _workgroupRootPath = row[WorkgroupMgr.WorkgroupCatalogueFields.WorkgroupRootPath].ToString();

            lblWorkgroupPath.Visible = true;
            lblWorkgroupPath.Text = _workgroupRootPath;

            lblWorkgroupDescription.Visible = true;
            lblWorkgroupDescription.Text = description;

            lblWorkgroupName.Visible = true;
            lblWorkgroupName.Text = workgroupName;

            _WorkspaceCurrent = workgroupName;

            if (workgroupName == "Local")
            {
                //picWorkgroupType.Load("home-variant.png");
                lstbMembers.Items.Add(AppFolders.UserName);

                _isLocal = true;

                _workgroupMgr = new Workgroups(AppFolders.AppDataPath, AppFolders.UserName, AppFolders.UserInfoPathFile);
            }
            else
            {
                _isLocal = false;

                if (Directory.Exists(_workgroupRootPath))
                {
                    //picWorkgroupType.Load("folder_puzzle.png");

                    _workgroupMgr = null;

                    _workgroupMgr = new Workgroups(AppFolders.AppDataPath, _workgroupRootPath, AppFolders.UserName, AppFolders.UserInfoPathFile);

                    _Members.Clear();
                    _Members = _workgroupMgr.GetWorkgroupMembers();

                    if (_Members != null)
                    {

                        foreach (string member in _Members)
                        {
                            string[] prts = member.Split('|');
                            lstbMembers.Items.Add(prts[0]);
                        }
                    }
                    else
                    {
                        string errMsg = _workgroupMgr.ErrorMessage;
                        MessageBox.Show(errMsg, "Workgroup Users Not Displayed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    //picWorkgroupType.Load("appbar.alert.png");
                    lblWorkgroupDescription.Text = string.Concat("Unable to Connect to Workgroup '", lblWorkgroupName.Text, "'. Please check the connection or with your System Administrator.");
                    lblWorkgroupDescription.ForeColor = Color.LightSalmon;
                    System.Media.SystemSounds.Exclamation.Play();

                    // Clear List
                    lstbLists.DataSource = null;
                    lstbListItems.DataSource = null;
                    lstbListItems.Items.Clear();
                    lblListName.Text = string.Empty;
                    lblListDescription.Text = string.Empty;
                    tileList.Text = "List            X";

                    // Clear Document Types
                    lstbDocTypes.DataSource = null;
                    dvgDocTypeItems.DataSource = null;
                    lblDocTypeName.Text = string.Empty;
                    lblDocTypesDescription.Text = string.Empty;
                    tileDocTypes.Text = "Document Types       X";

                    // Clear Reference Resource
                    lstbRefRes.DataSource = null;
                    lblRefResName.Text = string.Empty;
                    lblRefResDescription.Text = string.Empty;
                    picRefResShared.Visible = false;
                    picRefResInternal.Visible = false;
                    tileRefResources.Text = "Ref Resources            X";

                    // Clear Matrix
                    lstMatrix.Items.Clear();
                    lblSummaryText.Text = string.Empty;
                    lblMatrixName.Text = string.Empty;
                    lblMatrixDescription.Text = string.Empty;
                    reoGridControl2.Visible = false; // Added 03.29.2020
                  //  webBrowser1.DocumentText = string.Empty;
                    tileMatrixTemp.Text = "Matrix Templates          X";

                    // Clear Storyboard -- Todo

                    return false;
                }
            }

            int listQty = LoadLists();    // Load Lists
            int docTypesQty = LoadDocTypes(); // Load Document Types
            LoadRefRes(); //Load Reference Resources
            LoadMatrices();
            LoadSB();

            _projectRootPath = _workgroupMgr.ProjectRootPath;


            if (WorkgroupSelected != null)
                WorkgroupSelected();

            return true;
        }

        private void lstbWorkgroups_SelectedIndexChanged(object sender, EventArgs e)
        {

            WorkgroupChanged();


           // picDiagram.Visible = false;
           // panMembers.Visible = true;
           // tileList.Visible = true;
           // tileDocTypes.Visible = true;
           // tileRefResources.Visible = true;
           // tileMatrixTemp.Visible = true;
           // tileSBTemp.Visible = true;

           // lstbMembers.Items.Clear();
           // lblMemberInfo.Text = string.Empty;

           // int selectedIndex = lstbWorkgroups.SelectedIndex;

           // if (selectedIndex == -1)
           //     return;

           // lblWorkgroupDescription.ForeColor = Color.White;

           // DataRow row = _dtWorkgroups.Rows[selectedIndex];

           //// uid = (int)row[WorkgroupMgr.WorkgroupCatalogueFields.UID];
           // string workgroupName = row[WorkgroupMgr.WorkgroupCatalogueFields.WorkgroupName].ToString();
           // string description = row[WorkgroupMgr.WorkgroupCatalogueFields.WorkgroupDescription].ToString();
           // _workgroupRootPath = row[WorkgroupMgr.WorkgroupCatalogueFields.WorkgroupRootPath].ToString();

           // lblWorkgroupPath.Visible = true;
           // lblWorkgroupPath.Text = _workgroupRootPath;

           // lblWorkgroupDescription.Visible = true;
           // lblWorkgroupDescription.Text = description;

           // lblWorkgroupName.Visible = true;
           // lblWorkgroupName.Text = workgroupName;

           // _WorkspaceCurrent = workgroupName;

           // if (workgroupName == "Local")
           // {
           //     //picWorkgroupType.Load("home-variant.png");
           //     lstbMembers.Items.Add(AppFolders.UserName);

           //     _isLocal = true;

           //     _workgroupMgr = new Workgroups(AppFolders.AppDataPath, AppFolders.UserName, AppFolders.UserInfoPathFile);
           // }
           // else
           // {
           //     _isLocal = false;

           //     if (Directory.Exists(_workgroupRootPath))
           //     {
           //         //picWorkgroupType.Load("folder_puzzle.png");

           //         _workgroupMgr = null;

           //         _workgroupMgr = new Workgroups(AppFolders.AppDataPath, _workgroupRootPath, AppFolders.UserName, AppFolders.UserInfoPathFile);

           //         _Members.Clear();
           //         _Members = _workgroupMgr.GetWorkgroupMembers();

           //         if (_Members != null)
           //         {

           //             foreach (string member in _Members)
           //             {
           //                 string[] prts = member.Split('|');
           //                 lstbMembers.Items.Add(prts[0]);
           //             }
           //         }
           //         else
           //         {
           //             string errMsg = _workgroupMgr.ErrorMessage;
           //             MessageBox.Show(errMsg, "Workgroup Users Not Displayed", MessageBoxButtons.OK, MessageBoxIcon.Error);
           //         }

           //     }
           //     else
           //     {
           //         //picWorkgroupType.Load("appbar.alert.png");
           //         lblWorkgroupDescription.Text = string.Concat("Unable to Connect to Workgroup '", lblWorkgroupName.Text, "'. Please check the connection or with your System Administrator.");
           //         lblWorkgroupDescription.ForeColor = Color.LightSalmon;
           //         System.Media.SystemSounds.Exclamation.Play();

           //         // Clear List
           //         lstbLists.DataSource = null;
           //         lstbListItems.DataSource = null;
           //         lstbListItems.Items.Clear();
           //         lblListName.Text = string.Empty;
           //         lblListDescription.Text = string.Empty;
           //         tileList.Text = "List            X";

           //         // Clear Document Types
           //         lstbDocTypes.DataSource = null;
           //         dvgDocTypeItems.DataSource = null;
           //         lblDocTypeName.Text = string.Empty;
           //         lblDocTypesDescription.Text = string.Empty;
           //         tileDocTypes.Text = "Document Types       X";

           //         // Clear Reference Resource
           //         lstbRefRes.DataSource = null;
           //         lblRefResName.Text = string.Empty;
           //         lblRefResDescription.Text = string.Empty;
           //         picRefResShared.Visible = false;
           //         picRefResInternal.Visible = false;
           //         tileRefResources.Text = "Ref Resources            X";

           //         // Clear Matrix
           //         lstMatrix.Items.Clear();
           //         lblSummaryText.Text = string.Empty;
           //         lblMatrixName.Text = string.Empty;
           //         lblMatrixDescription.Text = string.Empty;
           //         webBrowser1.DocumentText = string.Empty;
           //         tileMatrixTemp.Text = "Matrix Templates          X";

           //         // Clear Storyboard -- Todo

           //         return;
           //     }
           // }

           // int listQty = LoadLists();    // Load Lists
           // int docTypesQty = LoadDocTypes(); // Load Document Types
           // LoadRefRes(); //Load Reference Resources
           // LoadMatrices();
           // LoadSB();

           // _projectRootPath = _workgroupMgr.ProjectRootPath;
            

           // if (WorkgroupSelected != null)
           //     WorkgroupSelected();
  
        }

        private void lblSelectList_Click(object sender, EventArgs e)
        {

        }

        private void butBack_Lists_Click(object sender, EventArgs e)
        {
            splitContLists.Visible = false;
            splitContLists.Dock = DockStyle.None;

            splitContWorkgroups.Dock = DockStyle.Fill;
            splitContWorkgroups.Visible = true;

            _currentMode = Modes.Workgroups;

        }

        int FindMyStringInList(ListBox lb, string searchString, int startIndex)
        {
            for (int i = startIndex; i < lb.Items.Count; ++i)
            {
                string lbString = lb.Items[i].ToString();
                if (lbString.Contains(searchString))
                    return i;
            }
            return -1;
        }

        private void lstbLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSelect.Visible = false;
            lstbListItems.Dock = DockStyle.Fill;
            lstbListItems.Visible = true;

            if (lstbLists.Text == string.Empty && _ListSelected != string.Empty)
            {
                int index = FindMyStringInList(lstbLists, _ListSelected, 0);
                if (index == -1)
                    return;

                lstbLists.SelectedIndex = index;
            }
            _ListSelected = lstbLists.Text;
            lblListName.Text = _ListSelected;
            lblListName.Visible = true;

            lstbListItems.Items.Clear();
            string[] items = _listMgr.GetListItems(_ListSelected);
            if (items == null)
            {
                string error = _listMgr.ErrorMessage;
                if (error != string.Empty)
                {
                   // MessageBox.Show(string.Concat("An error has occurred while getting items for your selected List.", Environment.NewLine, Environment.NewLine, error), "Error has Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            foreach (string item in items)
            {
                lstbListItems.Items.Add(item);
            }
            

            string description = _listMgr.GetDescription(_ListSelected);
            lblListDescription.Text = description;
            lblListDescription.Visible = true;


        }

        private void tileList_Click(object sender, EventArgs e)
        {
            splitContWorkgroups.Visible = false;
            splitContWorkgroups.Dock = DockStyle.None;

            splitContLists.Dock = DockStyle.Fill;
            splitContLists.Visible = true;

            _currentMode = Modes.Lists;
        }


        private void tileDocTypes_Click(object sender, EventArgs e)
        {
            splitContWorkgroups.Visible = false;
            splitContWorkgroups.Dock = DockStyle.None;

            splitContDocTypes.Dock = DockStyle.Fill;
            splitContDocTypes.Visible = true;

            _currentMode = Modes.DocTypes;
        }

        private void butBack_DocTypes_Click(object sender, EventArgs e)
        {
            splitContDocTypes.Visible = false;
            splitContDocTypes.Dock = DockStyle.None;

            splitContWorkgroups.Dock = DockStyle.Fill;
            splitContWorkgroups.Visible = true;

            _currentMode = Modes.Workgroups;
        }

        private void lstbDocTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDocTypes_Definition.Visible = false;
            dvgDocTypeItems.Dock = DockStyle.Fill;
            dvgDocTypeItems.Visible = true;

            dvgDocTypeItems.DataSource = null;


            if (lstbDocTypes.Text == string.Empty && _DocTypeSelected != string.Empty)
            {
                int index = FindMyStringInList(lstbDocTypes, _DocTypeSelected, 0);
                if (index == -1)
                    return;

                lstbDocTypes.SelectedIndex = index;
            }
            else if (lstbDocTypes.Text == string.Empty && _DocTypeSelected == string.Empty)
            {
                return;
            }

            _DocTypeSelected = lstbDocTypes.Text;
            lblDocTypeName.Text = _DocTypeSelected;
            lblDocTypeName.Visible = true;

            DataTable dt = _docTypeMgr.GetDocTypeItems(_DocTypeSelected); // Get Document Type Items

            if (dt == null)
                return;

            dvgDocTypeItems.DataSource = dt;

            dvgDocTypeItems.Columns[0].Visible = false; // Hide UID column

            dvgDocTypeItems.ColumnHeadersVisible = false;
            dvgDocTypeItems.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dvgDocTypeItems.Columns[1].Width = 100;
            

            string description = _docTypeMgr.GetDescription(_DocTypeSelected); // Get Description
            lblDocTypesDescription.Text = description;
            lblDocTypesDescription.Visible = true;
        }

        private void butBack_RefRes_Click(object sender, EventArgs e)
        {
            splitConRefRec.Visible = false;
            splitConRefRec.Dock = DockStyle.None;

            splitContWorkgroups.Dock = DockStyle.Fill;
            splitContWorkgroups.Visible = true;

            _currentMode = Modes.Workgroups;
        }

        private void tileRefResources_Click(object sender, EventArgs e)
        {      
            splitContWorkgroups.Visible = false;
            splitContWorkgroups.Dock = DockStyle.None;

            splitConRefRec.Dock = DockStyle.Fill;
            splitConRefRec.Visible = true;

            _currentMode = Modes.RefResources;
        }

        private void lstbRefResContNames_SelectedIndexChanged(object sender, EventArgs e)
        {

           // ClearRefResContentSettings();

         //   ShowHideRefResContentButton(false);

            lblRefResContText.Text = string.Empty;

           _RefResContentSelected = lstbRefResContNames.Text;
           if (_RefResContentSelected == string.Empty)
           {
               return;
           }

           if (_refResMgr == null)
               return;

           lblRefResContText.Text = _refResMgr.GetRefResContentText(_RefResSelected, _RefResContentSelected);
           lblRefResContText.Visible = true;
        //   lblRefResContText.Refresh();

       //    ShowHideRefResContentButton(true);
            
        }

        private void ShowHideRefResContentButton(bool show)
        {
            butRefResContNew.Visible = show;
            butRefResContEdit.Visible = show;
            butRefResContDelete.Visible = show;
  
        }

        private void LoadResRefContent()
        {
         //   ShowHideRefResContentButton(false);

            lstbRefResContNames.DataSource = null;
            lblRefResName.Text = string.Empty;
            lblRefResName.Visible = true;
            lblRefResDescription.Text = string.Empty;
            lblRefResDescription.Visible = true;
            lblRefResContPath.Text = string.Empty;

            _RefResContentSelected = string.Empty;

            if (_refResMgr == null)
                return;

            _RefResSelected = lstbRefRes.Text; 

            if (_RefResSelected == string.Empty)
                return;

            lblRefResName.Text = _RefResSelected;

            string[] refResContentNames = _refResMgr.GetRefResContentNames(_RefResSelected);

            if (refResContentNames == null)
            {
                if (_refResMgr.ErrorMessage.Length > 0)
                {
                    MessageBox.Show(_refResMgr.ErrorMessage, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            lstbRefResContNames.DataSource = refResContentNames;
            DataRow row = _refResMgr.GetRefRes(_RefResSelected);
            if (row == null)
            {
                if (_refResMgr.ErrorMessage.Length > 0)
                {
                    MessageBox.Show(_refResMgr.ErrorMessage, "Unable to get Content", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return;
            }

            lblRefResDescription.Text = row[RefResFields.Description].ToString();

            string locationType = row[RefResFields.LocationType].ToString();

            if (locationType == RefResFields.LocationType_Internal)
            {
                locationType = string.Concat(locationType, " to this workgroup.");
                picRefResShared.Visible = false;
                picRefResInternal.Visible = true;
            }
            else
            {
                locationType = string.Concat(locationType, " with possible other workgroups.");
                picRefResInternal.Visible = false;
                picRefResShared.Visible = true;                
            }


            lblRefResContPath.Text = string.Concat(locationType, "   Path: ", _refResMgr.GetRefResContentPath(_RefResSelected));
            lblRefResContPath.Visible = true;

        }

        private void lstbRefRes_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadResRefContent();
        }

        private void butRefResContNew_Click(object sender, EventArgs e)
        {

            frmRefResContent frm = new frmRefResContent(_refResMgr, _RefResSelected);
            if (frm.ShowDialog(this) == DialogResult.Cancel)
                return;

            LoadResRefContent();
        }

        private void butRefResContEdit_Click(object sender, EventArgs e)
        {
            frmRefResContent frm = new frmRefResContent(_refResMgr, _RefResSelected, _RefResContentSelected, lblRefResContText.Text);
            if (frm.ShowDialog(this) == DialogResult.Cancel)
                return;

            LoadResRefContent();
        }

        private void butRefResContDelete_Click(object sender, EventArgs e)
        {
            string msg = string.Concat("Are you sure you want to Delete the selected Reference Resource Content '", _RefResContentSelected, "'?", Environment.NewLine, Environment.NewLine, "A Backup of this Content will be generated.");
            if (MessageBox.Show(msg, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            if (!_refResMgr.DeleteRefResContent(_RefResSelected, _RefResContentSelected))
            {
                if (_refResMgr.ErrorMessage.Length > 0)
                {
                    MessageBox.Show(_refResMgr.ErrorMessage, "Reference Resource Content was Not Deleted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            LoadResRefContent();
        }

        private void butMatrix_Back_Click(object sender, EventArgs e)
        {

            splitContMatrix.Visible = false;
            splitContMatrix.Dock = DockStyle.None;

            splitContWorkgroups.Dock = DockStyle.Fill;
            splitContWorkgroups.Visible = true;

            _currentMode = Modes.Workgroups;
        }

        private void lstMatrix_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbSummaryText.Text = string.Empty;
            lblMatrixName.Text = string.Empty;
            lblMatrixDescription.Text = string.Empty;
            lblSummaryText.Text = string.Empty;
            reoGridControl2.Visible = false;
            this.Refresh();

            _MatrixSelected = lstMatrix.Text;
            if (_MatrixSelected.Length == 0)
            {
                return;
            }

            lblMatrixName.Text = _MatrixSelected;
            lblMatrixName.Visible = true;

            string selectedTemplatePath = Path.Combine(_matrixTempPathTemplates, _MatrixSelected);

            string pathFile = Path.Combine(selectedTemplatePath, "Description.txt");
            if (File.Exists(pathFile))
            {
                lblMatrixDescription.Text = Files.ReadFile(pathFile);
                lblMatrixDescription.Visible = true;
            }

            pathFile = Path.Combine(selectedTemplatePath, "Summary.txt");
            if (File.Exists(pathFile))
            {              
                lblSummaryText.Text = Files.ReadFile(pathFile);
                txtbSummaryText.Visible = true;
            }

            pathFile = Path.Combine(selectedTemplatePath, "MatrixTemp.xlsx");
            if (File.Exists(pathFile))
            {
                reoGridControl2.Load(pathFile);
                reoGridControl2.Visible = true;

                //Zoom OUT
                //webBrowser1.Focus();
                //SendKeys.Send("^-"); // [CTRL]+[-]
               // SendKeys.Send("^-"); // [CTRL]+[-]
            }
        }

        private void txtbSummaryText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                if (sender != null)
                    ((TextBox)sender).SelectAll();
            }
        }

        private void lblSummaryText_TextChanged(object sender, EventArgs e)
        {
            txtbSummaryText.Text = lblSummaryText.Text;
        }

        private void txtbSummaryText_TextChanged(object sender, EventArgs e)
        {
            txtbSummaryText.Text = lblSummaryText.Text;
        }

        private void lblWorkgroupPath_Click(object sender, EventArgs e)
        {
            if (lblWorkgroupPath.Text == string.Empty)
                return;

            if (lblWorkgroupPath.Text == "Path:")
                return;

            System.Diagnostics.Process.Start("explorer.exe", lblWorkgroupPath.Text);
        }

      

        private void butSB_Back_Click(object sender, EventArgs e)
        {
            splitContSB.Visible = false;
            splitContSB.Dock = DockStyle.None;

            splitContWorkgroups.Dock = DockStyle.Fill;
            splitContWorkgroups.Visible = true;

            _currentMode = Modes.Workgroups;
        }

        private void lstSBs_SelectedIndexChanged(object sender, EventArgs e)
        {
            _StoryboardSelected = lstSBs.Text;
            if (_StoryboardSelected == string.Empty)
                return;

            documentViewer1.Visible = false; // Default
            reoGridControl1.Visible = false;  // Default

            DataRow row = _sbMgr.GetSBDataRow(_StoryboardSelected);

            if (row == null)
            {
                MessageBox.Show(_sbMgr.ErrorMessage, "Unable to Load Storyboard Template information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            string pathFile = _sbMgr.GetSBTempViewPathFile(_StoryboardSelected); // Load "View" SB Template to prevent locking errors
            if (pathFile == string.Empty)
            {
                if (_sbMgr.ErrorMessage != string.Empty)
                {
                    MessageBox.Show(_sbMgr.ErrorMessage, "Unable to Get the Storyboard Template MS Word Document", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MessageBox.Show("Unable to find the selected Storyboard Template MS Word Document file. ", "Storyboard Template Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (!File.Exists(pathFile))
            {

                string msg = string.Concat("Unable to find the selected Storyboard Template MS Word Document file: ", pathFile);
                MessageBox.Show(msg, "Storyboard Template Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);               
                return;

            }

            // code below doesn't work
            //docvSB.Preferences.FormatterSettings.SpreadSheet.ShowHeadings.Print = true;
            //docvSB.Preferences.FormatterSettings.SpreadSheet.ShowHeadings.View = true;

            documentViewer1.Visible = true;
            documentViewer1.LoadDocument(pathFile);

            string description = row[SBTempsFields.Description].ToString();
            string matrixTemplate = row[SBTempsFields.MatrixTemplate].ToString();

            DateTime cDT = (DateTime)row[SBTempsFields.CreationDate];
            string createdInfo = string.Concat("Storyboard Template created by: ", row[SBTempsFields.CreatedBy].ToString(), "  ", cDT.ToString("F"));

            bool hasFieldAdded = (bool) row[SBTempsFields.FieldsAdded];

            lblStoryboardDescription.Visible = true;
            lblStoryboardDescription.Text = description;

            lblStoryboardName.Visible = true;
            lblStoryboardName.Text = _StoryboardSelected;

            if (!hasFieldAdded)
            {
                lblStoryboardPath.Visible = true;
                lblStoryboardPath.ForeColor = Color.Yellow;
                lblStoryboardPath.Text = string.Concat("Insert Matrix fields into your selected Storyboard Template.  Click the 'Edit' button. -- ", createdInfo);
            }
            else
            {
                lblStoryboardPath.Visible = true;
                lblStoryboardPath.ForeColor = Color.White;
                lblStoryboardPath.Text = createdInfo;
            }

            string file = "MatrixTemp.xlsx";

            pathFile = Path.Combine(_matrixTempPathTemplates, matrixTemplate, file);

            if (!File.Exists(pathFile))
            {
                string msg = string.Concat("Unable to find the Storyboard Template’s associated Matrix Template: ", pathFile);
                MessageBox.Show(msg, "Matrix Template Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                reoGridControl1.Visible = false;
                return;
            }

            reoGridControl1.Visible = true;
            reoGridControl1.Load(pathFile);

            
        }

        private void tileSBTemp_Click(object sender, EventArgs e)
        {
            splitContWorkgroups.Visible = false;
            splitContWorkgroups.Dock = DockStyle.None;

            splitContSB.Dock = DockStyle.Fill;
            splitContSB.Visible = true;

            _currentMode = Modes.Storyboards;
        }

        private void butPrintSBTemplate_Click(object sender, EventArgs e)
        {
            string pathFile = _sbMgr.GetSBTempViewPathFile(_StoryboardSelected); // Load "View" SB Template to prevent locking errors
            if (pathFile != string.Empty)
            {
                ProcessStartInfo info = new ProcessStartInfo(pathFile); 
                info.Verb = "Print";
                info.CreateNoWindow = true;
                info.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(info);

            }
        }

        private void butEmailNewMembers_Click(object sender, EventArgs e)
        {
            if (lblWorkgroupPath.Text == string.Empty)
                return;

            if (_WorkspaceCurrent == "Local")
            {
                string msg = "The Local workgroup is on your computer and cannot be in a shared environment.";
                MessageBox.Show(msg, "Cannot Share your Local Workgroup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_EmailOutLook == null)
                _EmailOutLook = new Email();

            if (!_EmailOutLook.IsOutlookConnectable())
            {
                string msg = string.Concat("The Matrix Builder does not see MS Outlook installed on your computer. However, you can copy the Workgroup path below and send it to your co-worker(s).", Environment.NewLine, Environment.NewLine,  "Your co-worker(s) will require license subscriptions for Atebion LLC’s Professional Document Analyzer/Matrix Builder.");
                MessageBox.Show(msg, "MS Outlook Not Installed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Cursor.Current = Cursors.WaitCursor; // Waiting 

            //string exportedMatrixFile = _AllocationMgr.GetExportMatrix_New();
            //if (exportedMatrixFile == string.Empty)
            //{
            //    Cursor.Current = Cursors.Default;
            //    MessageBox.Show(_AllocationMgr.ErrorMessage, "Unable to get Excel Export due to an Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            List<string> sAttachments = new List<string>();

            //sAttachments.Add(exportedMatrixFile);

            //string file = Files.GetFileName(exportedMatrixFile);

            //_WorkspaceCurrent

            string subject = string.Concat("Join Workgroup '", _WorkspaceCurrent, "'");
            string body = string.Concat(Environment.NewLine, Environment.NewLine, "Please join this Workgroup: ", _WorkspaceCurrent, Environment.NewLine, Environment.NewLine,
                "Click on the 'New' button under the Workgroup panel. Next click the 'Connect to an existing Workgroup' button and copy this path: ", lblWorkgroupPath.Text,
                " into the 'Workgroup Folder' textbox.  Finally, click the 'Save' button to join this workgroup.", Environment.NewLine, Environment.NewLine,
                "To join this Workgroup requires a license subscription for Atebion LLC’s Professional Document Analyzer/Matrix Builder. For details, see Atebion’s website: ", @"http://www.atebionllc.com/");


            _EmailOutLook.OpenEmailWithAttachments(string.Empty, subject, body, sAttachments.ToArray());

            //_EmailOutLook.SendEmail("", 

            Cursor.Current = Cursors.Default;
        }

        private void lstbMembers_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selUser = lstbMembers.Text;

            if (selUser == string.Empty)
            {
                lblMemberInfo.Text = string.Empty;
                return;
            }

            lblMemberInfo.Text = _workgroupMgr.GetUserProfile(selUser);
        }

        private void butDownloadMatrix_Click(object sender, EventArgs e)
        {
            frmDownloadMatrix frm = new frmDownloadMatrix(_doctypePath, _listPath, _refRespath, _matrixTempPath, _matrixTempPathTemp, _matrixTempPathTemplates);
            if (frm.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            LoadMatrices();
        }

        private void butSBDownload_Click(object sender, EventArgs e)
        {
            frmDownloadMatrix frm = new frmDownloadMatrix(_doctypePath, _listPath, _refRespath, _storyboardPath, _matrixTempPathTemp, _storyboardPath);
            if (frm.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            LoadSB();
            
        }


    }
}
