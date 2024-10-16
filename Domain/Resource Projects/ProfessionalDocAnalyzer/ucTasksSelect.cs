using Atebion.Common;
using Atebion.Tasks;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProfessionalDocAnalyzer
{
    public partial class ucTasksSelect : UserControl
    {
        #region Constructor

        public ucTasksSelect()
        {
            StackTrace st = new StackTrace(false);
            InitializeComponent();
        }

        #endregion

        #region Properties

        // Declare delegate for when a workgroup has been selected
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when a Task is selected")]
        public event ProcessHandler TaskSelected;

        public string TaskName { get; private set; }
        public string Task { get; private set; }
        public string[] TaskSteps { get; private set; }
        public Manager TaskManager { get; private set; }
        public DataSet DsTasks;

        #endregion

        #region Public Methods

        public void LoadData()
        {
            Task = string.Empty;
            TaskName = string.Empty;
            TaskSteps = null;
            chkbShowDetails.Visible = false;

            lblInformation.Text = string.Empty; // clear from last display
            lblInstructions.Text = "Select a Task and then click the 'Next' button";

            panTasks.Controls.Clear();

            var userName = AppFolders.IsLocal ? AppFolders.UserName : AppFolders.WorkgroupRootPath;
            TaskManager = new Manager(AppFolders.AppDataPath, userName);

            TaskManager.ValidateFix();
            TaskManager.CheckFixTaskCatalogue();

            DsTasks = TaskManager.GetTasks();
            if (DsTasks == null)
            {
                MessageBox.Show(TaskManager.ErrorMessage, "Unable to Get Tasks", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataView dv = new DataView(DsTasks.Tables[TaskCatalogue.TableName])
            {
                Sort = "SortOrder"
            };

            foreach (DataRowView row in dv)
            {
                if (Convert.ToBoolean(row[TaskCatalogue.ShowTask]))
                {
                    var name = row[TaskCatalogue.TaskName].ToString();
                    var description = row[TaskCatalogue.TaskDescription].ToString();

                    RadioButton taskButton = new RadioButton()
                    {
                        Text = name,
                        Height = 50,
                        Appearance = Appearance.Button,
                        Width = splitContainer1.Panel1.Width - 35,
                        Font = new Font("Segoe UI", 12, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    taskButton.Click += new EventHandler(TaskButton_Click);
                    taskButton.MouseEnter += new EventHandler(TaskButton_MouseEnter);
                    taskButton.MouseLeave += new EventHandler(TaskButton_MouseLeave);
                    taskButton.CheckedChanged += new EventHandler(TaskButton_CheckedChanged);

                    panTasks.Controls.Add(taskButton);
                }
            }

            var taskCount = panTasks.Controls.Count;
            if (taskCount == 0)
            {
                MessageBox.Show("Unable to find any tasks for the current Workgroup.", "No Tasks Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            lblTaskQty.Text = taskCount.ToString();
        }

        #endregion

        #region Private Methods

        private void DisplayTaskInformation()
        {
            lblInformation.Text = string.Empty;
            lblInstructions.Text = string.Empty;

            chkbShowDetails.Visible = true;

            if (TaskManager == null)
                return;

            TaskName = DataFunctions.ReplaceSingleQuote(TaskName);

            DataRow[] rows = DsTasks.Tables[TaskCatalogue.TableName].Select(TaskCatalogue.TaskName + " = '" + TaskName + "'");

            if (rows == null || !rows.Any())
                return;

            string information = rows[0][TaskCatalogue.TaskDescription].ToString();

            Task = rows[0][TaskCatalogue.Task].ToString();

            string summary = TaskManager.GetTaskSummary(Task);

            lblInformation.Text = chkbShowDetails.Checked ?
                $"{information}{Environment.NewLine}{Environment.NewLine}{summary}" : information;
            
            TaskSteps = TaskManager.GetTaskSteps(Task);
            if (TaskSteps == null)
                return;

            if (TaskSteps.Any())
            {
                lblInstructions.Text = $"Click on the 'Next' button to {TaskSteps[0]}";
            }

            TaskSelected?.Invoke();
        }

        private void chkbShowDetails_CheckedChanged(object sender, EventArgs e)
        {
            DisplayTaskInformation();
        }

        private void panTasks_SizeChanged(object sender, EventArgs e)
        {
            foreach (RadioButton button in panTasks.Controls)
            {
                button.Width = panTasks.VerticalScroll.Visible ? panTasks.Width - 35 : panTasks.Width - 10;
            }
        }

        private void txtInformation_TextChanged(object sender, EventArgs e)
        {
            txtInformation.Text = lblInformation.Text;
        }

        private void lblInformation_TextChanged(object sender, EventArgs e)
        {
            txtInformation.Text = lblInformation.Text;
        }

        #endregion
    }
}
