using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

namespace ProfessionalDocAnalyzer
{
    public partial class ucTasks : UserControl
    {
        public ucTasks()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        private string _CurrentWG = string.Empty;

        private string _CurrentProj = string.Empty;
        public string Project
        {
            get { return _CurrentProj; }
            set { _CurrentProj = value; }
        }

        private string _CurrentTask = string.Empty;
        public string CurrentTask
        {
            get { return _CurrentTask; }
            set { _CurrentTask = value; }
        }

        private string _CurrentDocument = string.Empty;
        public string Document
        {
            get { return _CurrentDocument; }
            set { _CurrentDocument = value; }
        }

        private string[] _Steps;
        public string[] Steps
        {
            get { return _Steps; }
            set { _Steps = value; }
        }

        private int _CurrentStep = -1;
        public int CurrentStep
        {
            get { return _CurrentStep; }
            set { 
                _CurrentStep = value;
                if (_CurrentStep > -1)
                {
                   // _CurrentTask = _Steps[_CurrentStep];
                    UpdateStatus();
                }
            }
        }

        private string _Status = string.Empty;
        private string _ProcessStatus = string.Empty;

        public void UpdateProcessStatus(string Process, bool ShowWait)
        {
            if (ShowWait)
            {
                _ProcessStatus = "<b>Please Wait: </b> ";
            }
            else
            {
                _ProcessStatus = string.Empty; ;
            }

            _ProcessStatus = string.Concat(_ProcessStatus, Process);

            htmlLabel.Visible = false;

            htmlLabelStatus.Text = _ProcessStatus;

            htmlLabelStatus.Dock = DockStyle.Fill;

            htmlLabelStatus.Visible = true;

            Application.DoEvents();

            htmlLabelStatus.Refresh();
        }

        public void ShowTaskFlowStatus()
        {
            
            htmlLabelStatus.Visible = false;

            htmlLabel.Dock = DockStyle.Fill;

            htmlLabel.Visible = true;
        }

        public void LoadData(string CurrentWG, string CurrentTask, string CurrentProj, string CurrentDocument, string[] steps, int CurrentStep)
        {
            _CurrentWG = CurrentWG;
            _CurrentTask = CurrentTask;
            _CurrentProj = CurrentProj;
            _CurrentDocument = CurrentDocument;
            _Steps = steps;

            _CurrentStep = CurrentStep;

            UpdateStatus();
        }


        public void UpdateStatus()
        {
            if (_Steps == null && _CurrentTask.Length == 0)
            {
                _Status = string.Concat("<b>Workgroup: </b><font color=blue>", _CurrentWG, "</font>");
            }
            else
            {
                _Status = string.Concat("<b>WorkGroup: </b> ", _CurrentWG);
            }

            if (_CurrentTask.Length > 0)
            {
                if (_Steps == null || CurrentStep == -1)
                {
                    _Status = string.Concat(_Status, " - <b>Task: </b><font color=blue>", _CurrentTask, "</font>");
                }
                else
                {
                    _Status = string.Concat(_Status, " - <b>Task: </b>", _CurrentTask);
                }
            }
            if (_CurrentProj.Length > 0)
            {
                _Status = string.Concat(_Status, " - <b>Project: </b>", _CurrentProj);
            }
            if (_CurrentDocument.Length > 0)
            {
                _Status = string.Concat(_Status, " - <b>Document: </b>", _CurrentDocument);
            }

            if (_Steps != null)
            {
                int i = 0;
                foreach (string step in _Steps)
                {
                    if (i == 0)
                    {
                        _Status = string.Concat(_Status, " - ");
                    }

                    if (_CurrentStep == i)
                    {
                        _Status = string.Concat(_Status, " <font color=blue> >", step, "</font>");
                    }
                    else
                    {
                        _Status = string.Concat(_Status, " >", step);
                    }

                    i++;
                }
            }

            htmlLabel.Text = _Status;

            ShowTaskFlowStatus();

            htmlLabel.Refresh();


        }

        private void ucTasks_Load(object sender, EventArgs e)
        {

        }

        private void htmlLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
