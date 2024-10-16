using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using WorkgroupMgr;


namespace MatrixBuilder
{
    public partial class frmMain : Form
    {
        public frmMain() 
        {
            InitializeComponent();
        }

        public frmMain(string[] args) 
        {
            InitializeComponent();

  
            _currentMode = Modes.Start;
            ModeAdjustments();

            string arg = string.Empty;
            string[] argSplit;
            string argParameter = string.Empty;
            string argValue = string.Empty;

            string action = string.Empty;
            string workgroup = string.Empty;
            string project = string.Empty;
            string matrix = string.Empty;
            string workgroupRootPath = string.Empty;

            if (args.Length > 0)
            {
                for (var i = 0; i < args.Length; i++)
                {
                    arg = args[i];
                    if (arg.IndexOf("=") != -1)
                    {
                        argSplit = arg.Split('=');
                        argParameter = argSplit[0];
                        argValue = argSplit[1];

                        // command-line argument actions
                        switch (argParameter)
                        {
                            case "Action":
                                action = argValue;
                                break;

                            case "Workgroup":
                                workgroup = argValue;
                                break;

                            case "Project":
                                project = argValue;
                                break;

                            case "Matrix":
                                matrix = argValue;
                                break;

                            case "WorkgroupRootPath":
                                workgroupRootPath = argValue;
                                break;

                        }

                    }
                }

                lblClickHere2Start.Visible = false;

                if (workgroup.Length > 0)
                {
                    ucWorkGroups1.Workgroup = workgroup;

                    if (action == "GoTo_Workgroup")
                    {
                        _currentStep++;
                        _currentMode = Modes.Workgroups;
                        ModeAdjustments();

                    }
                    else
                    {
                        if (project.Length > 0)
                        {
                            ucProjects1.Project = project;
                            if (action == "GoTo_Project")
                            {
                                _currentStep++;
                                _currentStep++;
                                _currentMode = Modes.Projects;
                                ModeAdjustments();
                            }
                            else if (action == "GoTo_CreateMatrix")
                            {
                                string sx = AppFolders.AppDataPath;

                                LoadData();

                                // Start Panel
                                _currentStep = 0;
                                _currentMode = (Modes)_currentStep;
                                ModeAdjustments();
                                this.Refresh();
                                Application.DoEvents();

                            //    MessageBox.Show(_currentMode.ToString());

                                // Workgroup
                                _currentStep++;
                                _currentMode = (Modes)_currentStep;
                                ModeAdjustments();
                                this.Refresh();
                                Application.DoEvents();

                               // AppFolders.WorkgroupCurrent = workgroup;
                                ucWorkGroups1.Workgroup = workgroup;
                                ucWorkGroups1.WorkgroupChanged();

                              //  MessageBox.Show(_currentMode.ToString());

                                // Go to Project
                                _currentStep++;
                                _currentMode = Modes.Projects;
                                _projectRootPath = ucWorkGroups1.ProjectRootPath;
                            //    MessageBox.Show("ProjectsRootPath=" + _projectRootPath);
                                ModeAdjustments();
                                ucProjects1.Project = project;
                                ucProjects1.ProjectSelectionUpdated();
                            //    MessageBox.Show("At Projects");

                                // Go to Matrices
                                _currentStep++;
                                _currentMode = Modes.Matrices;
                                ModeAdjustments();
                                this.Refresh();
                          //      MessageBox.Show("At Matrices");

                                ucMatrices1.AddNew();
                               
                            }
                            else if (action == "GoTo_Matrices")
                            {
                                string sx = AppFolders.AppDataPath;

                                LoadData();

                                // Start Panel
                                _currentStep = 0;
                                _currentMode = (Modes)_currentStep;
                                ModeAdjustments();
                                this.Refresh();
                                Application.DoEvents();

                                //    MessageBox.Show(_currentMode.ToString());

                                // Workgroup
                                _currentStep++;
                                _currentMode = (Modes)_currentStep;
                                ModeAdjustments();
                                this.Refresh();
                                Application.DoEvents();

                                // AppFolders.WorkgroupCurrent = workgroup;
                                ucWorkGroups1.Workgroup = workgroup;
                                ucWorkGroups1.WorkgroupChanged();

                                //  MessageBox.Show(_currentMode.ToString());

                                // Go to Project
                                _currentStep++;
                                _currentMode = Modes.Projects;
                                _projectRootPath = ucWorkGroups1.ProjectRootPath;
                                //    MessageBox.Show("ProjectsRootPath=" + _projectRootPath);
                                ModeAdjustments();
                                ucProjects1.Project = project;
                                ucProjects1.ProjectSelectionUpdated();
                                //    MessageBox.Show("At Projects");

                                // Go to Matrices
                                _currentStep++;
                                _currentMode = Modes.Matrices;
                                ModeAdjustments();
                                this.Refresh();
                                //      MessageBox.Show("At Matrices");
                            }
                            else if (action == "GoTo_Matrix")
                            {
                                _currentStep++;
                                _currentStep++;
                                _currentStep++;
                                _currentMode = Modes.Matrices;
                                ModeAdjustments();
                                if (ucMatrices1.SelectMatrix(matrix))
                                {
                                    _currentStep++;
                                    ModeAdjustments();
                                }

                            }

                        }
                    }

                }


            }

            //if (args.Length > 0)
            //{
            //    if (args.Length == 1)
            //    {
            //        _currentStep++;
            //        _currentMode = Modes.Workgroups;
            //        ModeAdjustments();

            //        ucWorkGroups1.Workgroup = args[0];

            //        ucSteps1.Step = 1;

            //    }
            //    else
            //    {
            //        _currentStep++;
            //        _currentMode = Modes.Workgroups;
            //        ModeAdjustments();

            //        ucWorkGroups1.Workgroup = args[0];

            //        _currentStep++;
            //        _currentMode = Modes.Projects;
            //        ModeAdjustments();

            //        ucProjects1.Project = args[1];

            //        ucSteps1.Step = 2;

            //    }

            //    lblClickHere2Start.Visible = false;

            //    this.Refresh();
           // }
 

        }

        private string _currentWorkgroup = string.Empty;
        private string _currentProject = string.Empty;

        private string _projectRootPath = string.Empty;

        private string _currentMatrix = string.Empty;
        private string _currentMatrixPath = string.Empty;
        private string _listPath = string.Empty;
        private string _refResPath = string.Empty;
        private string _sbPath = string.Empty;

        private int _currentStep = 0;
        private Modes _PreviousMode;
        private Modes _currentMode;
        private enum Modes
        {
            Start = 0,
            Workgroups = 1,
            Projects = 2,
            Matrices = 3,
            Matrix = 4
        }

        


        private LicenseMgr _lMgr;
        private int _days = 0;

        private bool LoadData()
        {
            // ucSteps1.Workgroup = "Test";
            ucSteps1.Step = 0;

            // ToDo Fix
            string[] user = Directory.GetFiles(AppFolders.AppDataPathUser, "*.AUsr");
            if (user.Length == 0)
            {
                string msg = string.Concat("The Matrix Builder will close because it cannot operate without a registered user.", Environment.NewLine, Environment.NewLine, "Register via the Professional Document Analyzer.");
                MessageBox.Show(msg, "Unable to Find a Registered User", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
            }


            string fileName = Files.GetFileNameWOExt(user[0]);
            string memberName = string.Empty;
            string[] prts = fileName.Split('_');
            if (prts.Length > 1)
            {
                for (int i = 0; i < prts.Length - 1; i++)
                {
                    if (i == 0)
                    {
                        memberName = prts[0];
                    }
                    else
                    {
                        memberName = string.Concat(memberName, " ", prts[i]);
                    }
                }

                AppFolders.UserName = memberName;
                lblUserName.Text = AppFolders.UserName;
            }





            string pathFile = Path.Combine(AppFolders.AppDataPath, "MatrixBuilder.src");
            if (!File.Exists(pathFile))
            {
                WorkgroupMgr.Files.WriteStringToFile(Application.StartupPath, pathFile);
            }

            return true;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            LoadData();


     //      // ucSteps1.Workgroup = "Test";
     //       ucSteps1.Step = 0;

     //// ToDo Fix
     //       string[] user = Directory.GetFiles(AppFolders.AppDataPathUser, "*.AUsr");
     //       if (user.Length == 0)
     //       {
     //           string msg = string.Concat("The Matrix Builder will close because it cannot operate without a registered user.", Environment.NewLine, Environment.NewLine, "Register via the Professional Document Analyzer.");
     //           MessageBox.Show(msg, "Unable to Find a Registered User", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
     //           Application.Exit();
     //       }


     //       string fileName = Files.GetFileNameWOExt(user[0]);
     //       string memberName = string.Empty;
     //       string[] prts = fileName.Split('_');
     //       if (prts.Length > 1)
     //       {
     //           for (int i = 0; i < prts.Length - 1; i++)
     //           {
     //               if (i == 0)
     //               {
     //                   memberName = prts[0];
     //               }
     //               else
     //               {
     //                   memberName = string.Concat(memberName, " ", prts[i]);
     //               }
     //           }

     //           AppFolders.UserName = memberName;
     //           lblUserName.Text = AppFolders.UserName;
     //       }


       


     //       string pathFile = Path.Combine(AppFolders.AppDataPath, "MatrixBuilder.src");
     //       if (!File.Exists(pathFile))
     //       {
     //           WorkgroupMgr.Files.WriteStringToFile(Application.StartupPath, pathFile);
     //       }
        }

        private void TimePeriodCheck(DateTime expirationDate)
        {
            _days = DateFunctions.DateDiff(DateFunctions.DateInterval.Day, DateTime.Now, expirationDate);

            lblPrimary.Text = string.Concat(lblPrimary.Text, " -- Expires in ", _days.ToString(), " Days on ", expirationDate.ToString("D"));            

            if (_days < 0)
            {
                MessageBox.Show("The Subscription Period for the Professional Document Analyzer has expired. Please contact Atebion, LLC at 540.535.8267 or via email at sales@AtebionLLC.com.", "Subscription Period has Expired", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();

            }
            //else if (_days < 16) // Added 08.05.2015
            //{
            //    butPurchase.Visible = true;
            //    lblPurchase.Visible = true;
            //    if (_days < 5)
            //    {
            //        lblPurchase.ForeColor = Color.Red;
            //    }
            //    else if (_days < 10)
            //    {
            //        lblPurchase.ForeColor = Color.Yellow;
            //    }

            //}

        }


        private void HideAllPrimaryButtons()
        {
            //butResults.Visible = false;
            picNew.Visible = false;
            picEdit.Visible = false;
            picDelete.Visible = false;

            butBack.Visible = false;
            butNext.Visible = false;

            butAnalyze.Visible = false;

            lblAnalyze.Visible = false;
            lblPrevious.Visible = false;
            lblNext.Visible = false;
            lblNew.Visible = false;
            lblEdit.Visible = false;
            lblDelete.Visible = false;

            butDownload.Visible = false;
            lblDownload.Visible = false;


        }

        private void ModeEdit()
        {
            switch (_currentMode)
            {
                case Modes.Workgroups:
                    ucWorkGroups1.Edit();

                    break;

                case Modes.Projects:

                    break;

                case Modes.Matrices:

                    break;

                case Modes.Matrix:

                    break;

            }

        }

        private void ModeNew()
        {
            switch (_currentMode)
            {
                case Modes.Workgroups:
                    ucWorkGroups1.AddNew();

                    break;

                case Modes.Projects:

                    break;

                case Modes.Matrices:
                    ucMatrices1.AddNew();

                    break;

                case Modes.Matrix:

                    break;

            }

        }

        private void ModeDelete()
        {
            switch (_currentMode)
            {
                case Modes.Workgroups:
                    ucWorkGroups1.Delete();

                    break;

                case Modes.Projects:

                    break;

                case Modes.Matrices:

                    break;

                case Modes.Matrix:

                    break;

            }

        }

        private void ModeAdjustments()
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 

            HideAllPrimaryButtons();


            // Hide User controls
            ucWelcome1.Visible = false;
            ucWorkGroups1.Visible = false;
            ucProjects1.Visible = false;
            ucMatrices1.Visible = false;
            ucMatrixBuild1.Visible = false;
            
            butMinus.Visible = false;
            butPlus.Visible = false;
            butMinusBottom.Visible = false;
            butPlusBottom.Visible = false;

            //butNext.Visible = false;
            //lblNext.Visible = false;


            panHeader.Height = 115;
            panFooter.Height = 75;

            picMatrixBuilder.Visible = true;
            lblMatrixArchitect.Visible = true;

            ucSteps1.Status = string.Empty;


            switch (_currentMode)
            {

                case Modes.Start:

                    ucWelcome1.Location = new Point(250, 0);
                    ucWelcome1.Dock = DockStyle.Fill;
                    ucWelcome1.Visible = true;
                    ucSteps1.Step = 0;
                  
                    butNext.Visible = true;
                    lblNext.Visible = true;

                    break;

                case Modes.Workgroups:
                    if (AppFolders.WorkgroupCurrent.Length == 0)
                    {
                        AppFolders.WorkgroupCurrent = "Local";
                        
                    }

                    ucWorkGroups1.Location = new Point(250, 0);
                    ucWorkGroups1.Dock = DockStyle.Fill;
                    ucWorkGroups1.Visible = true;                  
                    ucWorkGroups1.LoadData(AppFolders.WorkgroupCurrent);     

                    ucSteps1.Step = 1;



                    lblPrevious.Visible = true;
                    butBack.Visible = true;

                    picNew.Visible = true;
                    lblNew.Visible = true;

                    picEdit.Visible = true;
                    lblEdit.Visible = true;

                    picDelete.Visible = true;
                    lblDelete.Visible = true;

                    ucSteps1.Status = "Maintain the selected Workgroup’s Resources && Templates";

                    this.Refresh();

                    break;

                case Modes.Projects:
                   
                    ucProjects1.LoadData(_projectRootPath);
                    ucProjects1.Visible = true;
                    ucProjects1.Dock = DockStyle.Fill;

                    ucSteps1.Step = 2;

                    lblPrevious.Visible = true;
                    butBack.Visible = true;

                    ucSteps1.Status = "Project’s Documents are shown for Analysis Status";

                    this.Refresh();

                    break;

                case Modes.Matrices:

                    ucMatrices1.LoadData(_projectRootPath, _currentProject, this.ucWorkGroups1.MatrixTempPathTemplates);
                    ucMatrices1.Location = new Point(250, 0);
                    ucMatrices1.Dock = DockStyle.Fill;
                    ucMatrices1.Visible = true;
                    Application.DoEvents();
                    
                    
                    lblPrevious.Visible = true;
                    butBack.Visible = true;

                    picNew.Visible = true;
                    lblNew.Visible = true;

                    ucSteps1.Status = "Multiple Matrices can be maintained for a Project (e.g. Compliance && Requirements Traceability) ";

                    ucSteps1.Step = 3;

                    break;

                case Modes.Matrix:

                    ucMatrixBuild1.Location = new Point(250, 0);
                    ucMatrixBuild1.Dock = DockStyle.Fill;
                    ucMatrixBuild1.Visible = true;
                    ucSteps1.Step = 4;

                    _listPath = ucWorkGroups1.ListPath;
                    _refResPath = ucWorkGroups1.RefResPath;
                    _sbPath = ucWorkGroups1.StoryboardPath;

                    ucMatrixBuild1.LoadData(_currentMatrixPath, _projectRootPath, _currentProject, _listPath, _refResPath, _sbPath); // ToDo change after testing

                    lblPrevious.Visible = true;
                    butBack.Visible = true;

                    butMinus.Visible = true;
                    butMinusBottom.Visible = true;

                    ucSteps1.Status = "Drag-&&-Drop content to the Matrix below";

                    break;

                   

            }

            Cursor.Current = Cursors.Default; // Back to normal

        }

        private void butNext_Click(object sender, EventArgs e)
        {
            lblClickHere2Start.Visible = false;

            //if (_currentMode == Modes.Workgroups) // For Testing only -- Remove after testing
            //{
            //    _currentMode = Modes.Matrix;
            //    ModeAdjustments();
            //    return;
            //}

            _currentStep++;

            _currentMode = (Modes)_currentStep;
            ModeAdjustments();
        }

        private void butBack_Click(object sender, EventArgs e)
        {
            _currentStep--;

            _currentMode = (Modes)_currentStep;
            ModeAdjustments();
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void picEdit_Click(object sender, EventArgs e)
        {
            ModeEdit();
        }

        private void picNew_Click(object sender, EventArgs e)
        {
            ModeNew();
        }

        private void picDelete_Click(object sender, EventArgs e)
        {
            ModeDelete();
        }

        private void ucProjects1_ProjectSelected()
        {
            _currentProject = ucProjects1.Project;
            ucSteps1.Project = _currentProject;
            ucSteps1.Step = 2;

            butNext.Visible = true;
            lblNext.Visible = true;
        }

        private void ucWorkGroups1_WorkgroupSelected()
        {
            _currentWorkgroup = ucWorkGroups1.Workgroup;
            ucSteps1.Workgroup = _currentWorkgroup;
            ucSteps1.Step = 1;
            _projectRootPath = ucWorkGroups1.ProjectRootPath;

            butNext.Visible = true;
            lblNext.Visible = true;
        }

        private void ucMatrices1_MatrixSelected()
        {
             _currentMatrix = ucMatrices1.CurrentMatrix;
             _currentMatrixPath = ucMatrices1.CurrentMatrixPath;

             ucSteps1.Matrix = _currentMatrix;

             butNext.Visible = true;
             lblNext.Visible = true;
        }

        private void butPlus_Click(object sender, EventArgs e)
        {
            panHeader.Height = 115;
            butPlus.Visible = false;
            butMinus.Visible = true;

            picMatrixBuilder.Visible = true;         
            lblMatrixArchitect.Visible = true;
            

            this.Refresh();
        }

        private void butMinus_Click(object sender, EventArgs e)
        {
            panHeader.Height = 20;
            butMinus.Visible = false;
            butPlus.Visible = true;

            picMatrixBuilder.Visible = false;
            lblMatrixArchitect.Visible = false;
            this.Refresh();
        }

        private void butPlusBottom_Click(object sender, EventArgs e)
        {
            panFooter.Height = 75;

            butMinusBottom.Visible = true;
            butPlusBottom.Visible = false;

            butBack.Visible = true;
            butClose.Visible = true;
            butHelp.Visible = true;
        }

        private void butMinusBottom_Click(object sender, EventArgs e)
        {
            panFooter.Height = 20;

            butMinusBottom.Visible = false;
            butPlusBottom.Visible = true;

            butBack.Visible = false;
            butClose.Visible = false;
            butHelp.Visible = false;
        }

        private void butHelp_Click(object sender, EventArgs e)
        {
            string helpFile = string.Empty;
            switch (_currentMode)
            {

                case Modes.Start:
                    helpFile = @"http://www.atebionllc.com/documentation/mb/#!/documenter_cover";
                    break;

                case Modes.Workgroups:
                    helpFile = @"http://www.atebionllc.com/documentation/mb/#!/workgroups";
                    break;

                case Modes.Projects:
                    helpFile = @"http://www.atebionllc.com/documentation/mb/#!/projects";
                    break;

                case Modes.Matrices:
                    helpFile = @"http://www.atebionllc.com/documentation/mb/#!/matrices";
                    break;

                case Modes.Matrix:
                    helpFile = @"http://www.atebionllc.com/documentation/mb/#!/matrix";
                    break;
       
            }

            if (helpFile.Length > 0)
                Process.Start(helpFile);
        }

        private void picUser_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Open the Professional Document Analyzer to change your Personal Information and License Key.", "Registration is via the Professional Document Analyzer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
