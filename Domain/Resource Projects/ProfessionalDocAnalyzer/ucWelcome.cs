using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
//using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using Atebion.Common;
using Atebion.WorkGroups;
using Atebion.Outlook;
using Atebion.Tasks;
using Atebion.ConceptAnalyzer;
//using Atebion.QC;


namespace ProfessionalDocAnalyzer
{
    public partial class ucWelcome : UserControl
    {
        private string welcomeBG = "#000333";
        private string settingsColor = "#AAAAAA";
        private string resultsColor = "#00E5FF";
        private string startColor = "#00FFCA";
        private string leftBox = "#E5E5E5";

        public ucWelcome()
        {
            StackTrace st = new StackTrace(false);
            InitializeComponent();
            SetWelcomeScreenColors();
            cboWorkgroups.DrawMode = DrawMode.OwnerDrawVariable;
            pnlSetting.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width- this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            //lblScion.Location = new Point(0, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            //panel3.Location = new Point(-300, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            //picSettings.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - (this.Width+100), Screen.PrimaryScreen.WorkingArea.Height - this.Height);

        }

        /// <summary>
        /// Sets new colors for the Welcome Screen. 
        /// </summary>
        private void SetWelcomeScreenColors()
        {
            Color myColor = System.Drawing.ColorTranslator.FromHtml(welcomeBG);
            panHeader.BackColor = myColor;
            lblCaption.BackColor = myColor;
            panLeftPadding.BackColor = ColorTranslator.FromHtml(leftBox);
            lblWorkgroups.ForeColor = Color.Black;
            lblWorkgroupPath.ForeColor = Color.Black;
            lblMessage.ForeColor = Color.Black;
            panMembersHeader.BackColor = ColorTranslator.FromHtml(leftBox);
            panMembersHeader.ForeColor = Color.Black;

        }

        private void cboWorkgroups_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                Font drawFont = new Font("Agency FB", 16);
                var combo = sender as ComboBox;
                //combo.DropDownStyle = ComboBoxStyle.DropDownList;
                //combo.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)
                //            );

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 12, 64)), e.Bounds);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(1, 19, 99)), e.Bounds);//1, 19, 99
                }

                e.Graphics.DrawString(combo.Items[e.Index].ToString(),
                                              drawFont,
                                              new SolidBrush(Color.White),
                                              new Point(e.Bounds.X, e.Bounds.Y));
            }
            catch (Exception exception)
            {


                
            }

        }

        // Declare delegate for when a workgroup has been selected
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when a Workgroup is selected")]
        public event ProcessHandler WorkgroupSelected;

        [Category("Action")]
        [Description("Fires when a Start is clicked")]
        public event ProcessHandler StartClicked;

        [Category("Action")]
        [Description("Fires when a Results is clicked")]
        public event ProcessHandler ResultsClicked;

        [Category("Action")]
        [Description("Fires when a Settings is clicked")]
        public event ProcessHandler SettingsClicked;


        private string _WorkspaceCurrent = "Local";
        private bool _isLocal = true;

        private Atebion.Outlook.Email _EmailOutLook;

        private Atebion.WorkGroups.Manager _workgroupMgr;

        private Atebion.Tasks.Manager _TaskManager;
        public Atebion.Tasks.Manager TaskManager
        {
            get { return _TaskManager; }
        }

        private DataTable _dtWorkgroups;

        private string _workgroupRootPath = string.Empty;
        public string WorkgroupRootPath
        {
            get { return _workgroupRootPath; }
        }

        private string _workgroupDescription = string.Empty;

        List<string> _Members = new List<string>();

        public string Workgroup
        {
            get { return _WorkspaceCurrent; }
        }

        public bool LoadData(Atebion.Tasks.Manager TaskManager)
        {
            _TaskManager = TaskManager;

            _isLocal = true; // Local is the default workgroup

            _workgroupMgr = new Atebion.WorkGroups.Manager(AppFolders.AppDataPath, AppFolders.UserName, AppFolders.AppDataPathUser);

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

            int wgQty = LoadWorkgroups(_dtWorkgroups);

            if (wgQty > 0)
            {
                this.cboWorkgroups.SelectedIndex = 0;
            }


            // lblWorkgroups.Text = string.Concat("Workgroups [", wgQty.ToString(), "]");

            return true;

        }

        private int LoadWorkgroups(DataTable dt)
        {
            int counter = 0;
            //if(dt == null)
            //{
            //    return counter;
            //}

            //if (dt == null)
            //{
            //    _workgroupMgr = new Atebion.WorkGroups.Manager(AppFolders.AppDataPath, _workgroupRootPath, AppFolders.UserName, AppFolders.AppDataPathUser); // Added 08.20.2020

            //    _dtWorkgroups = _workgroupMgr.GetWorkGroupList(); // Added 7/12/2017

            //    LoadWorkgroups(_dtWorkgroups); // was using dt

            //    _dtWorkgroups = _workgroupMgr.GetWorkGroupList(); // Added 7/12/2017

            //    dt = _dtWorkgroups;
            //    MessageBox.Show("Nothing to sisplay workgroup is null sorry.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}

            cboWorkgroups.Items.Clear();

            string workgroupName;

            foreach (DataRow row in dt.Rows)
            {
                workgroupName = row[Atebion.WorkGroups.WorkgroupCatalogueFields.WorkgroupName].ToString();
                cboWorkgroups.Items.Add(workgroupName);
                counter++;
            }

            return counter;
        }

        private void picStart_MouseEnter(object sender, EventArgs e)
        {
            //butStart.BackColor = Color.Lime;

            string msg = string.Concat("Click the Start button to select a Task to run…", Environment.NewLine, Environment.NewLine, "To create a new Task or modify a Task, click the Settings button");
            lblButtonInfo.Text = msg;
             
        }

        private void picStart_MouseLeave(object sender, EventArgs e)
        {
            lblButtonInfo.Text = string.Empty;
            //picStart.Image = Image.FromFile(@"C:\Users\dbegley\Desktop\ColorChanges\picStart110.png");
            picStart.Image = Properties.Resources.ScionIcon110;
        }


        private void picResults_MouseEnter(object sender, EventArgs e)
        {
            //butResults.BackColor = Color.Blue;
            string msg = string.Concat("Click the Results button to access previous Analysis Results and associated artifacts/reports");
            lblButtonInfo.Text = msg;

        }

        private void picResults_MouseLeave(object sender, EventArgs e)
        {
            lblButtonInfo.Text = string.Empty;
            picResults.Image = Properties.Resources.resultsIcon110;
            //picResults.Image = Image.FromFile(@"C:\Users\dbegley\Desktop\ColorChanges\picResults110.png");            
        }

        private void picSettings_MouseEnter(object sender, EventArgs e)
        {
            //picSettings.BackColor = Color.DarkGray;
            string msg = string.Concat("Click the Settings button to create/modify Dictionaries, Keyword Groups, Readability QC parameters, Responsibility Assignment Matrix (RAM) Models, Tasks, and Excel Templates");
            lblButtonInfo.Text = msg;
            //Load Alternate Icons.
        }

        private void picSettings_MouseLeave(object sender, EventArgs e)
        {
            //picSettings.BackColor = Color.DimGray;
            lblButtonInfo.Text = string.Empty;
            picSettings.Image = Properties.Resources.settingsIcon97;
            //picSettings.Image = Image.FromFile(@"C:\Users\dbegley\Desktop\ColorChanges\settingsSmall.png");
        }

        private void cboWorkgroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            lblMessage.Text = "Please Wait: Connecting to the selected Workgroup and Checking/Updating Configurations.";

            butResults.Visible = false;
            butStart.Visible = false;
            butSettings.Visible = false;
            butLasr.Visible = false;

            lstbMembers.Items.Clear();
            lblMemberInfo.Text = string.Empty;

            lblMessage.Text = string.Empty;
            lblMessage.ForeColor = Color.Black;

            int selectedIndex = cboWorkgroups.SelectedIndex;

            if (selectedIndex == -1)
                return;

            lblMessage.ForeColor = Color.Black;

            DataRow row = _dtWorkgroups.Rows[selectedIndex];

            // uid = (int)row[WorkgroupMgr.WorkgroupCatalogueFields.UID];
            _WorkspaceCurrent = row[Atebion.WorkGroups.WorkgroupCatalogueFields.WorkgroupName].ToString();
            _workgroupDescription = row[Atebion.WorkGroups.WorkgroupCatalogueFields.WorkgroupDescription].ToString();
            _workgroupRootPath = row[Atebion.WorkGroups.WorkgroupCatalogueFields.WorkgroupRootPath].ToString();

            lblWorkgroupPath.Visible = true;
            lblWorkgroupPath.Text = _workgroupRootPath;

            //lblWorkgroupDescription.Visible = true;
            //lblWorkgroupDescription.Text = description;

            //lblWorkgroupName.Visible = true;
            //lblWorkgroupName.Text = workgroupName;

            // _WorkspaceCurrent = workgroupName;

            //picWorkgroupType.Visible = false;
            //picHome.Visible = false;
            //picAlert.Visible = false;

            if (_WorkspaceCurrent == "Local")
            {
                //  picHome.Visible = true;
                lstbMembers.Items.Add(AppFolders.UserName);

                _isLocal = true;

                _workgroupMgr = new Atebion.WorkGroups.Manager(AppFolders.AppDataPath, AppFolders.UserName, AppFolders.AppDataPathUser);
                _workgroupMgr.ApplicationPath = Application.StartupPath;

                AppFolders.SetRootFolder(string.Empty, true);

                _workgroupMgr.ValidateFix(true);

                string tasksFolder = Path.Combine(AppFolders.AppDataPath, "Tasks");
                if (Directory.Exists(tasksFolder))
                {
                    string[] tasksFiles = Directory.GetFiles(tasksFolder, "*.tsk");
                    if (tasksFiles.Length == 0)
                        _workgroupMgr.ImportDefaultSettingFiles();
                }

                //Analysis ConceptDicAnalyzer = new Analysis(AppFolders.AppDataPath);

                //ConceptDicAnalyzer.Generate_Default_RAM_Models();

                //ConceptDicAnalyzer.GenerateSampleRAMExcelTemplate();

            }
            else
            {
                _isLocal = false;

                if (Directory.Exists(_workgroupRootPath))
                {
                    // picWorkgroupType.Visible = true;
                    // picWorkgroupType.Load("folder_puzzle.png");

                    _workgroupMgr = null;

                    _workgroupMgr = new Atebion.WorkGroups.Manager(AppFolders.AppDataPath, _workgroupRootPath, AppFolders.UserName, AppFolders.AppDataPathUser);

                    //WorkgroupDescrip.txt
                    if (File.Exists(Path.Combine(_workgroupMgr.PathCurrent, "Workgroup", "WorkgroupDescrip.txt")))
                    {
                        lblMessage.Text = Files.ReadFile(Path.Combine(_workgroupMgr.PathCurrent, "Workgroup", "WorkgroupDescrip.txt"));
                    }
                    else
                    {
                        lblMessage.Text = string.Empty;
                    }

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

                    AppFolders.SetRootFolder(_workgroupRootPath, false);

                    _workgroupMgr.ValidateFix(false);

                    string tasksFolder = Path.Combine(_workgroupRootPath, "Tasks");
                    if (Directory.Exists(tasksFolder))
                    {
                        string[] tasksFiles = Directory.GetFiles(tasksFolder, "*.tsk");
                        if (tasksFiles.Length == 0)
                            _workgroupMgr.ImportDefaultSettingFiles();
                    }

                }
                else
                {
                    //picAlert.Visible = true;

                    lblMessage.Text = string.Concat("Unable to Connect to Workgroup '", _WorkspaceCurrent, "'. Please check the connection or with your System Administrator.");
                    lblMessage.ForeColor = Color.Red;
                    System.Media.SystemSounds.Exclamation.Play();
                    Cursor.Current = Cursors.Default;
                    return;

                }
            }

            butResults.Visible = true;
            butStart.Visible = true;
            butSettings.Visible = true;
            //     butLasr.Visible = true;

            if (_WorkspaceCurrent == "Local")
            {
                lblMessage.ForeColor = Color.Black;
                lblMessage.Text = "The Local workgroup is used for working alone.";
                panMembers.Visible = false;
                flowLayoutPanel1.Visible = true;
                //lblScion.Visible = true;
                _TaskManager = new Atebion.Tasks.Manager(AppFolders.AppDataPath, AppFolders.UserName);
            }
            else
            {
                lblMessage.ForeColor = Color.Black;
                // lblMessage.Text = string.Empty;
                panMembers.Visible = true;
                _TaskManager = new Atebion.Tasks.Manager(AppFolders.AppDataPath, _workgroupRootPath, AppFolders.UserName);

                Analysis ConceptDicAnalyzer = new Analysis(AppFolders.AppDataPath);

                ConceptDicAnalyzer.Generate_Default_RAM_Models();

                ConceptDicAnalyzer.GenerateSampleRAMExcelTemplate();
            }

            AppFolders.WorkgroupName = _WorkspaceCurrent;

            if (WorkgroupSelected != null)
            {
                WorkgroupSelected();
            }


            Cursor.Current = Cursors.Default;


        }

        private void butWorkgroup_Disconnect_Click(object sender, EventArgs e)
        {
            string _WorkspaceCurrentSecondAttempt = _WorkspaceCurrent;
            //Remove user file from wg
            string[] fileEntries = Directory.GetFiles(_workgroupRootPath + @"\Workgroup\Users\");
            foreach (string fileName in fileEntries)
            {
                if (fileName.Contains(AppFolders.UserName.Replace(" ", "_")))
                {
                    File.Delete(fileName);
                }
            }

            if (WorkgroupDelete(_WorkspaceCurrent))
            {
                WorkgroupDeletezsecondAttempt(_WorkspaceCurrentSecondAttempt);
            }           

        }

        private bool WorkgroupDelete(string _WorkspaceCurrentSource)
        {
            _WorkspaceCurrent = _WorkspaceCurrentSource;
            string msg = string.Empty;

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
            //this.Controls.Clear();
            //this.InitializeComponent();
            Atebion.Tasks.Manager _TaskManager = null;
            this.LoadData(_TaskManager);

            //this.Controls.Clear();
            //this.InitializeComponent();
            Atebion.Tasks.Manager _TaskManager1 = null;
            this.LoadData(_TaskManager1);

            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Location = new System.Drawing.Point(554, 135);
            this.Name = "ucWelcome1";
            this.Size = new System.Drawing.Size(1044, 505);
            Application.DoEvents();

            _workgroupMgr = null; // Added 08.20.2020

            AppFolders.IsLocal = true; // Added 08.20.2020

            _workgroupMgr = new Atebion.WorkGroups.Manager(AppFolders.AppDataPath, _workgroupRootPath, AppFolders.UserName, AppFolders.AppDataPathUser); // Added 08.20.2020

            _dtWorkgroups = _workgroupMgr.GetWorkGroupList(); // Added 7/12/2017

            LoadWorkgroups(_dtWorkgroups); // was using dt

            if (cboWorkgroups.Items.Count > 0)
                cboWorkgroups.SelectedIndex = 0;

            return true;
        }

        private bool WorkgroupDeletezsecondAttempt(string _WorkspaceCurrentSource)
        {
            _WorkspaceCurrent = _WorkspaceCurrentSource;
            string msg = string.Empty;

            if (_WorkspaceCurrent == "Local")
            {
                msg = "The Local Workgroup is your Default Workgroup on your computer.";
                MessageBox.Show(msg, "Unable to remove your Local Workgroup.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            msg = string.Concat("Are you sure you want to disconnect from Workgroup '", _WorkspaceCurrent, "' ?", Environment.NewLine, Environment.NewLine, "If you want, you can reconnect to this Workgroup again in the future.");

            //if (MessageBox.Show(msg, "Confirm Disconnection from Workgroup", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //    return false;

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
            //this.Controls.Clear();
            //this.InitializeComponent();
            Atebion.Tasks.Manager _TaskManager = null;
            this.LoadData(_TaskManager);

            //this.Controls.Clear();
            //this.InitializeComponent();
            Atebion.Tasks.Manager _TaskManager1 = null;
            this.LoadData(_TaskManager1);


            Application.DoEvents();

            _workgroupMgr = null; // Added 08.20.2020

            AppFolders.IsLocal = true; // Added 08.20.2020

            _workgroupMgr = new Atebion.WorkGroups.Manager(AppFolders.AppDataPath, _workgroupRootPath, AppFolders.UserName, AppFolders.AppDataPathUser); // Added 08.20.2020

            _dtWorkgroups = _workgroupMgr.GetWorkGroupList(); // Added 7/12/2017

            LoadWorkgroups(_dtWorkgroups); // was using dt

            if (cboWorkgroups.Items.Count > 0)
                cboWorkgroups.SelectedIndex = 0;

            return true;
        }

        private void butWorkgroup_Connect_Click(object sender, EventArgs e)
        {
            //this.Controls.Clear();
            //this.InitializeComponent();
            Atebion.Tasks.Manager _TaskManager = null;
            this.LoadData(_TaskManager);

            //this.Controls.Clear();
            //this.InitializeComponent();
            Atebion.Tasks.Manager _TaskManager1 = null;
            this.LoadData(_TaskManager1);

            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Location = new System.Drawing.Point(554, 135);
            this.Name = "ucWelcome1";
            this.Size = new System.Drawing.Size(1044, 505);
            frmWGConnect frm = new frmWGConnect(_workgroupMgr);

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                string selectedWGPath = frm.Workgroup_Selected;

                _workgroupMgr = null; // Added 08.20.2020

                _workgroupMgr = new Atebion.WorkGroups.Manager(AppFolders.AppDataPath, _workgroupRootPath, AppFolders.UserName, AppFolders.AppDataPathUser); // Added 08.20.2020

                _dtWorkgroups = _workgroupMgr.GetWorkGroupList(); // Added 7/12/2017

                int qty = LoadWorkgroups(_dtWorkgroups); // was using dt
                string workgroupRootPath = string.Empty;
                string workgroupName = string.Empty;
                if (qty > 0)
                {
                    foreach (DataRow row in _dtWorkgroups.Rows)
                    {
                        workgroupRootPath = row[Atebion.WorkGroups.WorkgroupCatalogueFields.WorkgroupRootPath].ToString();
                        if (selectedWGPath == workgroupRootPath)
                        {
                            workgroupName = row[Atebion.WorkGroups.WorkgroupCatalogueFields.WorkgroupName].ToString();
                            int index = cboWorkgroups.Items.IndexOf(workgroupName);
                            if (index > -1)
                            {
                                cboWorkgroups.SelectedIndex = index;
                            }
                        }
                    }
                }
            }
        }

        private void lblWorkgroupPath_Click(object sender, EventArgs e)
        {
            if (lblWorkgroupPath.Text == string.Empty)
                return;

            if (lblWorkgroupPath.Text == "Path:")
                return;

            System.Diagnostics.Process.Start("explorer.exe", lblWorkgroupPath.Text);
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

        private void butWorkgroup_Add_Click(object sender, EventArgs e)
        {


            //this.Controls.Clear();
            //this.InitializeComponent();
            Atebion.Tasks.Manager _TaskManager = null;
            this.LoadData(_TaskManager);

            //this.Controls.Clear();
            //this.InitializeComponent();
            Atebion.Tasks.Manager _TaskManager1 = null;
            this.LoadData(_TaskManager1);

            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Location = new System.Drawing.Point(554, 135);
            this.Name = "ucWelcome1";
            this.Size = new System.Drawing.Size(1044, 505);


            frmWGNew frm = new frmWGNew();

            frm.LoadData(_workgroupMgr);

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                string newWorkGroupName = frm.NewWorkgroupName;

                _workgroupMgr = null; // Added 08.20.2020

                _workgroupMgr = new Atebion.WorkGroups.Manager(AppFolders.AppDataPath, _workgroupRootPath, AppFolders.UserName, AppFolders.AppDataPathUser); // Added 08.20.2020


                string selectedWGPath = frm.Workgroup_Selected;
                _dtWorkgroups = _workgroupMgr.GetWorkGroupList(); // Added 7/12/2017

                int qty = LoadWorkgroups(_dtWorkgroups); // was using dt
                string workgroupRootPath = string.Empty;
                string workgroupName = string.Empty;
                if (qty > 0)
                {
                    foreach (DataRow row in _dtWorkgroups.Rows)
                    {
                        workgroupRootPath = row[Atebion.WorkGroups.WorkgroupCatalogueFields.WorkgroupRootPath].ToString();
                        if (selectedWGPath == workgroupRootPath)
                        {
                            workgroupName = row[Atebion.WorkGroups.WorkgroupCatalogueFields.WorkgroupName].ToString();
                            int index = cboWorkgroups.Items.IndexOf(workgroupName);
                            if (index > -1)
                            {
                                cboWorkgroups.SelectedIndex = index;
                            }
                        }
                    }
                }

                int x = cboWorkgroups.FindString(newWorkGroupName);
                if (x != -1)
                    cboWorkgroups.SelectedIndex = x;
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

            if (OutLookMgr.isOutLookRunning())
            {
                if (!_EmailOutLook.IsOutlookConnectable())
                {
                    string msg = string.Concat("The Professional Document Analyzer does not see MS Outlook installed on your computer. However, you can copy the Workgroup path below and send it to your co-worker(s).", Environment.NewLine, Environment.NewLine, "Your co-worker(s) will require license subscriptions for Scion Analytics Professional Document Analyzer/Matrix Builder.");
                    MessageBox.Show(msg, "MS Outlook Not Installed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
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
                "To join this Workgroup requires a license subscription for Scion Analytics Professional Document Analyzer/Matrix Builder. For details, see Scion Analytics website: ", @"http://www.scionanalytics.com/");


            _EmailOutLook.OpenEmailWithAttachments(string.Empty, subject, body, sAttachments.ToArray());

            //_EmailOutLook.SendEmail("", 

            Cursor.Current = Cursors.Default;
        }

        //************* New buttons *******************
        private void picStart_Click(object sender, EventArgs e)
        {
            if (StartClicked != null)
                StartClicked();
        }

        private void picResults_Click(object sender, EventArgs e)
        {
            string projectsFolder = AppFolders.Project;
            string[] projFolders = Directory.GetDirectories(projectsFolder);
            
            string[] subdirs = Directory.GetDirectories(projectsFolder)
                            .Select(Path.GetFileName)
                            .ToArray();
            int deletedFolder = 0;
            foreach (var dir in subdirs)
            {
                if (dir.Contains('~'))
                {
                    deletedFolder = deletedFolder + 1;
                }
            }
            
            if (projFolders.Length == 0  || ((deletedFolder > 0)&&(subdirs.Length== deletedFolder)))
            {
                string msg = string.Concat("You have no active or open projects ");
                MessageBox.Show(msg, "No Projects Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return ;
            }

            if (projFolders.Length > 1 && projFolders[0].Contains("~"))
            {
                string msg = string.Concat("You have no active or open projects ");
                MessageBox.Show(msg, "No Projects Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (ResultsClicked != null)
                ResultsClicked();
        }

        private void picSettings_Click(object sender, EventArgs e)
        {
            if (SettingsClicked != null)
                SettingsClicked();
        }

        //

        private void butResults_Click(object sender, EventArgs e)
        {
            if (ResultsClicked != null)
                ResultsClicked();
        }

        private void butSettings_Click(object sender, EventArgs e)
        {
            if (SettingsClicked != null)
                SettingsClicked();
        }

        private void butLasr_MouseEnter(object sender, EventArgs e)
        {
            butLasr.BackColor = Color.OrangeRed;
        }

        private void butLasr_MouseLeave(object sender, EventArgs e)
        {
            butLasr.BackColor = Color.Sienna;

        }

        /// <summary>
        /// Use for testing QC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butQCTest_Click(object sender, EventArgs e)
        {
            //Atebion.QC.Analysis qcAnalysis = new Atebion.QC.Analysis();
            //string xmlPath = @"F:\QCTest1\T-ADC(X) Ship RFP Section C 600\Current\ParseSec\XML";
            //string parseSegPath = @"F:\QCTest1\T-ADC(X) Ship RFP Section C 600\Current\ParseSec";
            //string lsqcPath = @"F:\QCTest1\T-ADC(X) Ship RFP Section C 600\Current\ParseSec\LSQC";
            //string modelPath = @"I:\Tom\Atebion\ProfessionalDocAnalyzer\ProfessionalDocAnalyzer\bin\Debug\Model";

            //Cursor.Current = Cursors.WaitCursor;

            //qcAnalysis.ComplexWords_SyllableCountGreaterThan = 4;
            //qcAnalysis.AnalyzeDocs(xmlPath, parseSegPath, lsqcPath, modelPath, string.Empty);

            //Cursor.Current = Cursors.Default; // Back to normal

        }

        private void panRight_Paint(object sender, PaintEventArgs e)
        {
            //if (this.ClientRectangle.Height == 0 && this.ClientRectangle.Width == 0) return; 

            ////get the graphics object of the control 
            //Graphics g = e.Graphics; 

            ////The drawing gradient brush 
            //LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, Color.Blue, Color.PowderBlue, 50); 

            ////Fill the client area with the gradient brush using the control's graphics object 
            //g.FillRectangle(brush, this.ClientRectangle);
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            //Dispose(true);
            //ucWelcome n = new ucWelcome();
            ///////////////////
            //ucWelcome objFrmGrafik = new ucWelcome();
            //this.Dispose();
            //objFrmGrafik.Show();
            //frmMain n = new frmMain();
            //n.Show();
            /////////////////////////////////
            ///
            this.Controls.Clear();
            this.InitializeComponent();
            Atebion.Tasks.Manager _TaskManager = null;
            this.LoadData(_TaskManager);

            this.Controls.Clear();
            this.InitializeComponent();
            Atebion.Tasks.Manager _TaskManager1 = null;
            this.LoadData(_TaskManager1);

        }

        private void metroToolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void panLeftPadding_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblScion_Click(object sender, EventArgs e)
        {

        }

        private void lblImagine_Click(object sender, EventArgs e)
        {

        }
    }
}
