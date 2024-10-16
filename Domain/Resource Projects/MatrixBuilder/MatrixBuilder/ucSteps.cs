using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MatrixBuilder
{
    public partial class ucSteps : UserControl
    {
        public ucSteps()
        {
            InitializeComponent();
        }

        private string _Status = string.Empty;
        public string Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                lblMessage.Text = _Status;
            }
    }

       // int Step0_Counter = 0;
        private int _Step = 0;
        public int Step
        {
            get {return _Step;}
            set 
            {
                _Step = value;
                lblStep0.Visible = false;
                picStep1.Visible = false;
                picStep2.Visible = false;
                picStep3.Visible = false;
                picStep4.Visible = false;

                switch (_Step)
                {
                    case 0:
                        lblStep0.Visible = true;
                        lblStep0.Text = "The Matrix Builder is typically used for building Compliance Matrices and Proposal Outlines. Documents must be parsed by the Document Analyzer prior to allocating to a Matrix.";
                        break;

                        lblWorkgroup.Text = string.Empty;
                        lblWorkgroup.Visible = false;
                        picHome.Visible = false;
                        picWorkgroupType.Visible = false;

                        lblProject.Text = string.Empty;
                        lblProject.Visible = false;
                        picProject.Visible = false;

                        lblMatrix.Text = string.Empty;
                        lblMatrix.Visible = false;
                        picMatrix.Visible = false;

                    case 1:
                        picStep1.Visible = true;

                        lblProject.Text = string.Empty;
                        lblProject.Visible = false;
                        picProject.Visible = false;

                        lblMatrix.Text = string.Empty;
                        lblMatrix.Visible = false;
                        picMatrix.Visible = false;
                        Refresh();
                        break;
                    case 2:
                        picStep2.Visible = true;
                                               
                        lblMatrix.Text = string.Empty;
                        lblMatrix.Visible = false;
                        picMatrix.Visible = false;
                        
                        Refresh();
                        break;
                    case 3: 
                        picStep3.Visible = true;
                        Refresh();
                        break;
                    case 4:
                        picStep4.Visible = true;
                        Refresh();
                        break;
                }
            
            }
        }
        
          
        private string _Workgroup = string.Empty;
        public string Workgroup
        {
            get { return _Workgroup; }
            set { 
                _Workgroup = value;
                if (_Workgroup.Length > 0)
                {
                    lblWorkgroup.Text =  _Workgroup;
                    lblWorkgroup.Visible = true;

                    if (_Workgroup == "Local")
                    {
                        picHome.Visible = true;
                        picWorkgroupType.Visible = false;
                    }
                    else
                    {
                        picWorkgroupType.Visible = true;
                        picHome.Visible = false;
                    }
                }
                else
                {
                    lblWorkgroup.Text = string.Empty;
                    lblWorkgroup.Visible = false;

                    picHome.Visible = false;
                    picWorkgroupType.Visible = false;
                }

                lblProject.Text = string.Empty;
                lblProject.Visible = false;
                picProject.Visible = false;

                lblMatrix.Text = string.Empty;
                lblMatrix.Visible = false;
                picMatrix.Visible = false;


            }
        }

        private string _Project = string.Empty;
        public string Project
        {
            get { return _Project; }
            set 
            { 
                _Project = value;
                if (_Project.Length > 0)
                {
                    lblProject.Text = _Project;
                    lblProject.Visible = true;
                    picProject.Visible = true;
                }
                else
                {
                    lblProject.Text = string.Empty;
                    lblProject.Visible = false;
                    picProject.Visible = false;
                }

                lblMatrix.Text = string.Empty;
                lblMatrix.Visible = false;
                picMatrix.Visible = false;
            }
        }


        private string _Matrix = string.Empty;
        public string Matrix
        {
            get { return _Matrix; }
            set
            {
                _Matrix = value;
                if (_Matrix.Length > 0)
                {
                    lblMatrix.Text = _Matrix;
                    lblMatrix.Visible = true;
                    picMatrix.Visible = true;
                }
                else
                {
                    lblMatrix.Text = string.Empty;
                    lblMatrix.Visible = false;
                    picMatrix.Visible = false;
                }

            }
        }
    }
}
