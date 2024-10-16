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

using Atebion.Tasks;
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public partial class ucTasksSettings : UserControl
    {
        public ucTasksSettings()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

               // Declare delegate for when a workgroup has been selected
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when a Task is selected")]
        public event ProcessHandler TaskSelected;

        private string _TaskName = string.Empty;
        public string TaskName
        {
            get { return _TaskName; }
        }

        private string _Task = string.Empty;
        public string Task
        {
            get { return _Task; }
        }

        private string _TaskDescription = string.Empty;
        public string TaskDescription
        {
            get { return _TaskDescription; }
        }


        private string[] _TaskSteps;
        public string[] TaskSteps
        {
            get { return _TaskSteps; }
        }

        private Atebion.Tasks.Manager _TaskManager;
        public Atebion.Tasks.Manager TaskManager
        {
            get { return _TaskManager; }
        }

        private DataSet _dsTasks;

        private bool _isReady4Edit = false;
        private bool _CheckedChanged = false;

        public bool LoadData()
        {
            _Task = string.Empty;
            _TaskName = string.Empty;
            _TaskSteps = null;

            _isReady4Edit = false;
            lstbTasks.Items.Clear();

            if (AppFolders.IsLocal)
            {
                _TaskManager = new Atebion.Tasks.Manager(AppFolders.AppDataPath, AppFolders.UserName);
            }
            else
            {
                _TaskManager = new Atebion.Tasks.Manager(AppFolders.AppDataPath, AppFolders.WorkgroupRootPath);
            }

            _TaskManager.ValidateFix();
            _TaskManager.CheckFixTaskCatalogue();

            _dsTasks = _TaskManager.GetTasks();
            if (_dsTasks == null)
            {
                MessageBox.Show(_TaskManager.ErrorMessage, "Unable to Get Tasks", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            SortOrder_CleanUp();

            DataView dv = new DataView(_dsTasks.Tables[Atebion.Tasks.TaskCatalogue.TableName]);
            dv.Sort = "SortOrder";



            string taskName = string.Empty;
            int tasksCount = 0;
            bool isVisible = false;
            // foreach (DataRow row in _dsTasks.Tables[Atebion.Tasks.TaskCatalogue.TableName].Rows)
            foreach (DataRowView row in dv)
            {
                isVisible = Convert.ToBoolean(row[Atebion.Tasks.TaskCatalogue.ShowTask]);
                
                taskName = row[Atebion.Tasks.TaskCatalogue.TaskName].ToString();
                lstbTasks.Items.Add(taskName, isVisible);
                tasksCount++;
                
            }

            lblInstructions.ForeColor = Color.Blue;
            lblInstructions.Text = "Unchecked Tasks are NOT displayed in the Task selection.";

            _isReady4Edit = true;

            return true;
        }

        public bool New()
        {
            frmTacksWorkflowConfig frm = new frmTacksWorkflowConfig();

            frm.LoadData(_TaskManager.TaskCatalogue_PathFile, AppFolders.AppDataPath_Local, AppFolders.WorkgroupRootPath);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                string taskName = frm.TaskName;

                LoadData();

                int index = lstbTasks.FindStringExact(taskName);
                lstbTasks.SelectedIndex = index;

                return true;
            }

            return false;
        }

        public bool Download()
        {
            frmDownLoad frm = new frmDownLoad();
            frm.LoadData(".tsk", _TaskManager.TasksPath, ContentTypes.Tasks_WorkFlow);

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                LoadData();
                return true;
            }

            return false;
        }

        public bool Edit()
        {
            if (_Task.Length == 0)
                return false;

            frmTacksWorkflowConfig frm = new frmTacksWorkflowConfig();

            string taskFile = string.Concat(_Task, ".tsk");
            string taskPathFile = Path.Combine(_TaskManager.TasksPath, taskFile);
            if (!File.Exists(taskPathFile))
            {
                string msg = string.Concat("Unable to find file: ", taskPathFile, "Task File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (!frm.LoadData(taskPathFile, _TaskManager.TaskCatalogue_PathFile, AppFolders.AppDataPath_Local, AppFolders.WorkgroupRootPath))
                return false;

            frm.TaskName = _TaskName;
            frm.TaskName_Short = _Task;
            frm.TaskDescription = _TaskDescription;


            if (frm.ShowDialog() == DialogResult.OK)
            {
                string taskName = frm.TaskName;

                LoadData();

                int index = lstbTasks.FindStringExact(taskName);
                lstbTasks.SelectedIndex = index;

                return true;
            }

            return false;
        }

        public bool Delete()
        {
            if (lstbTasks.Items.Count == 0)
                return false;

            string selectedTask = lstbTasks.Text;

            if (selectedTask.Length == 0)
                return false;

            string msg = string.Concat("Are you sure you want to remove task ", _TaskName, " ?");
            if (MessageBox.Show(msg, "Please Confirm Task Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return false;

            string file = string.Concat(_Task, ".tsk");
            string pathFile = Path.Combine(_TaskManager.TasksPath, file);

            Files.SetFileName2ObsoleteExtended(pathFile);

            LoadData();

            return true;
        }

        private bool SortOrder_CleanUp()
        {
          DataView dv = new DataView(_dsTasks.Tables[Atebion.Tasks.TaskCatalogue.TableName]);
            dv.Sort = "SortOrder";

            // foreach (DataRow row in _dsTasks.Tables[Atebion.Tasks.TaskCatalogue.TableName].Rows)
            int i = 0;
            foreach (DataRowView row in dv)
            {
                row.BeginEdit();
                row["SortOrder"] = i;
                row.EndEdit();
                i++;
            }

            DataTable dt = dv.ToTable(Atebion.Tasks.TaskCatalogue.TableName);

            _dsTasks.Tables.Remove(_dsTasks.Tables[Atebion.Tasks.TaskCatalogue.TableName]);

            _dsTasks.Tables.Add(dt);

                return true;
        }

        private void lstbTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblMessage.Text = string.Empty;

            _TaskName = lstbTasks.Text;

            _TaskName = DataFunctions.ReplaceSingleQuote(_TaskName);

            DataRow[] rows = _dsTasks.Tables[Atebion.Tasks.TaskCatalogue.TableName].Select(Atebion.Tasks.TaskCatalogue.TaskName + " = '" + _TaskName + "'");

            if (rows == null)
                return;

            if (rows.Length == 0)
                return;


            _Task = rows[0][Atebion.Tasks.TaskCatalogue.Task].ToString();

            _TaskName = rows[0][Atebion.Tasks.TaskCatalogue.TaskName].ToString();


            _TaskDescription = rows[0][Atebion.Tasks.TaskCatalogue.TaskDescription].ToString();

            if (_TaskManager == null)
                return;

            if (lstbTasks.Items.Count == 0)
                return;

            string summary = _TaskManager.GetTaskSummary(_Task);

            this.lblMessage.Text = string.Concat(_TaskDescription, Environment.NewLine, Environment.NewLine, summary);

            if (lstbTasks.GetItemCheckState(lstbTasks.SelectedIndex) == CheckState.Checked)
            {
                lblInstructions.ForeColor = Color.Blue;
                lblInstructions.Text = "This selected Task is shown in the Task List.";
            }
            else
            {
                lblInstructions.ForeColor = Color.Red;
                lblInstructions.Text = "This selected Task is NOT shown in the Task List.";
            }

            if (TaskSelected != null)
                TaskSelected();
        }

        private void txtbMessage_TextChanged(object sender, EventArgs e)
        {
            txtbMessage.Text = lblMessage.Text;
        }

        private void lblMessage_TextChanged(object sender, EventArgs e)
        {
            txtbMessage.Text = lblMessage.Text;
        }

        private void butSortUP_Click(object sender, EventArgs e)
        {
            int index = lstbTasks.SelectedIndex;


            if (index == -1)
                return;

            if (index == 0)
                return;

            int lastItem = lstbTasks.Items.Count - 1;
            //if (index == lastItem)
            //    return;

            DataView dv = new DataView(_dsTasks.Tables[Atebion.Tasks.TaskCatalogue.TableName]);
            dv.Sort = "SortOrder";

            dv.AllowEdit = true;

            dv[index - 1]["SortOrder"] = index;
            dv[index]["SortOrder"] = index - 1;
            

            DataTable dt = dv.ToTable(Atebion.Tasks.TaskCatalogue.TableName);

            _dsTasks.Tables.Remove(_dsTasks.Tables[Atebion.Tasks.TaskCatalogue.TableName]);

            _dsTasks.Tables.Add(dt);

            Atebion.Common.GenericDataManger gmgr = new GenericDataManger();
            gmgr.SaveDataXML(_dsTasks, _TaskManager.TaskCatalogue_PathFile);

            LoadData();

            lstbTasks.SelectedIndex = index - 1;
        }

        private void butSortDown_Click(object sender, EventArgs e)
        {
            int index = lstbTasks.SelectedIndex;
            

            if (index == -1)
                return;

            int lastItem = lstbTasks.Items.Count - 1;
            if (index == lastItem)
                return;

            DataView dv = new DataView(_dsTasks.Tables[Atebion.Tasks.TaskCatalogue.TableName]);
            dv.Sort = "SortOrder";

            dv.AllowEdit = true;
            
            dv[index]["SortOrder"] = index + 1;
            dv[index + 1]["SortOrder"] = index;

            DataTable dt = dv.ToTable(Atebion.Tasks.TaskCatalogue.TableName);

            _dsTasks.Tables.Remove(_dsTasks.Tables[Atebion.Tasks.TaskCatalogue.TableName]);

            _dsTasks.Tables.Add(dt);

            Atebion.Common.GenericDataManger gmgr = new GenericDataManger();
            gmgr.SaveDataXML(_dsTasks, _TaskManager.TaskCatalogue_PathFile);

            LoadData();

            lstbTasks.SelectedIndex = index + 1;

        }

        private void lstbTasks_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!_isReady4Edit)
                return;

            _CheckedChanged = true;

        }

        private void lstbTasks_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_CheckedChanged)
                return;

            DataView dv = new DataView(_dsTasks.Tables[Atebion.Tasks.TaskCatalogue.TableName]);
            dv.Sort = "SortOrder";

            // foreach (DataRow row in _dsTasks.Tables[Atebion.Tasks.TaskCatalogue.TableName].Rows)
            int i = 0;
            string isChecked = "false";
            foreach (DataRowView row in dv)
            {

                if (lstbTasks.GetItemCheckState(i) == CheckState.Checked)
                {
                    isChecked = "true";
                }
                else
                {
                    isChecked = "false";
                }

                row.BeginEdit();
                row["ShowTask"] = isChecked;
                row.EndEdit();
                i++;
            }

            DataTable dt = dv.ToTable(Atebion.Tasks.TaskCatalogue.TableName);

            _dsTasks.Tables.Remove(_dsTasks.Tables[Atebion.Tasks.TaskCatalogue.TableName]);

            _dsTasks.Tables.Add(dt);

            Atebion.Common.GenericDataManger gmgr = new GenericDataManger();
            gmgr.SaveDataXML(_dsTasks, _TaskManager.TaskCatalogue_PathFile);

            _CheckedChanged = false;
        }

        private void picHeader_Click(object sender, EventArgs e)
        {    
            if (_TaskManager.TasksPath.Length > 0)
                System.Diagnostics.Process.Start("explorer.exe", _TaskManager.TasksPath);
        }


    }
}
