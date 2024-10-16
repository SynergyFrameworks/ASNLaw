using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProfessionalDocAnalyzer
{
    public partial class ucDeepAnalyticsFilterDisplay : UserControl
    {
        public ucDeepAnalyticsFilterDisplay()
        {
            InitializeComponent();
        }

        private Modes _CurrentMode;
        public enum Modes
        {
            All = 0,
            Search = 1,
            Keywords = 2
        }

        public Modes CurrentMode
        {
            get { return _CurrentMode; }
            set { _CurrentMode = value; }
        }

        private int _Total = 0;
        public int Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        private int _Count = 0;
        public int Count
        {
            get { return _Count; }
            set { _Count = value; }
        }

        public void UpdateStatusError(string ErrorMsg)
        {
            picPage.Visible = false;
            picKey.Visible = false;
            picKey.Visible = false;
            picFilter.Visible = false;
            picError.Visible = true;

            lblStatus.Text = string.Concat("Error: ", ErrorMsg);

        }

        public void UpdateStatusDisplay()
        {
            picPage.Visible = false;
            picKey.Visible = false;
            picSearch.Visible = false;
            picFilter.Visible = false;
            picError.Visible = false;

            switch (_CurrentMode)
            {
                case Modes.All:
                    picPage.Visible = true;
                  //  lblStatus.Text = string.Concat("All shown: ", _Total.ToString(), " of ", _Total.ToString());
                    lblStatus.Text = string.Concat("All shown: ", _Total.ToString());
                    break;

                case Modes.Keywords:
                    picFilter.Visible = true;
                    picKey.Visible = true;
                  //  lblStatus.Text = string.Concat("Filtered by Keywords ", _Count.ToString(), " of ", _Total.ToString());
                    lblStatus.Text = string.Concat("Filtered by Keywords   Found: ", _Count.ToString());
                    break;

                case Modes.Search:
                    picFilter.Visible = true;
                    picSearch.Visible = true;
               //     lblStatus.Text = string.Concat("Filtered by Search criteria ", _Count.ToString(), " of ", _Total.ToString());
                    lblStatus.Text = string.Concat("Filtered by Search criteria   Found: ", _Count.ToString());
                    break;

            }

        }

 

        



    }
}
