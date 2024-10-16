using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{


    public partial class ucStart : UserControl
    {
        public ucStart()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when completed")]
        public event ProcessHandler Completed;

        private UserCardMgr _UserMgr = new UserCardMgr();
        private LicenseMgr _LicMgr = new LicenseMgr();

        private string _UserFile = string.Empty;

        private bool _SendOpenEmail = true;

        private int _days = 0;
        public int DaysLeft
        {
            get { return _days; }
        }

        private string _expirationDate = string.Empty;
        public string ExpirationDate
        {
            get {

                if (_expirationDate == string.Empty) 
                    CheckLicense();
                
                return _expirationDate; 
            }
        }

        public bool StartValidate()
        {
            
            SayCheckingSystem();
           // Thread.Sleep(4000);

            if (CheckLicense())
            {
                SayWelcome();
               // Thread.Sleep(4000);

                if (Completed != null)
                    Completed();
                return true;
            }

            SayWelcome();

            return false;
        }

        private bool CheckLicense()
        {
            butPurchase.Visible = false;
            butrRegister.Visible = false;

            ChkNoOpenEmails();

            if (_SendOpenEmail)
                GetUserInformation();

            if (_LicMgr.Validate() == "Invalid")
            {
               
                string[] users = _UserMgr.GetUserInforFiles4App();


                if (users.Length == 0) // New user
                {
                    
                    //	MessageBox.Show("If you have a License then enter it and your contact information on the next screen. \n\n If you want to purchase a user license for this application, please contact Atebion's Customer Relations at 540-535-8267 or via email at sales@atebionllc.com", "License is Required", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //frmPurchase purchase = new frmPurchase(msg);
                    //purchase.ShowDialog();

                    OpenUserInfor(true);

                    if (CheckLicense())
                    {
                        return true;
                    }
                    else
                    {
                        butPurchase.Visible = true;
                        butrRegister.Visible = true;

                        return false;
                    }
                }
                else
                {
                    

                    //MessageBox.Show("If you want to purchase a user license for this application, please contact Atebion's Customer Relations at 540-535-8267 or via email at sales@atebionllc.com", "Document Analyzer has Expired", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    string msg = string.Concat("Your Professional Document Analyzer license key has expired.", Environment.NewLine, Environment.NewLine, "To purchase user license key(s) for this application, please click the 'Purchase Online' button or contact Atebion's Customer Relations at 540-535-8267 or via email at sales@atebionllc.com", Environment.NewLine, Environment.NewLine, "If you have a License Key press the 'Register' button.");

                    lblNotice.Font = new System.Drawing.Font(lblNotice.Font.Name, 12F);
                    butPurchase.Visible = true;
                    butrRegister.Visible = true;

                    return false;

                  //  OpenUserInfor(false);
                }
                
            }
            else if (_LicMgr.Validate() == "Valid")
            {
                

                //string expirationDate = _lMgr.ExpirationDate;

                // //// For Trial
                //BetaTestCheck(expirationDate); // ToDo: Remove after Beta Testing

                // Added 08.21.2014
                DateTime? expirationDate = _LicMgr.dtExpirationDate;

                if (expirationDate != null)
                {
                    TimePeriodCheck((DateTime)expirationDate);
                }

                return true;
            }

            return true;
        }

        /// <summary>
        /// This function replaces BetaTestCheck()
        /// </summary>
        /// <param name="expirationDate"></param>
        private void TimePeriodCheck(DateTime expirationDate)
        {
            _days = DateFunctions.DateDiff(DateFunctions.DateInterval.Day, DateTime.Now, expirationDate);

            lblPrimary.Text = string.Concat("Expires in ", _days.ToString(), " Days on ", expirationDate.ToString("D"));

            _expirationDate = expirationDate.ToString("D");

            string s = string.Concat("Parse Engine: 3.3 -- Search Engine: 2.2.1");

            if (_days < 0)
            {
                MessageBox.Show("The Usage Period for this application has expired. If you want an extension, please contact Atebion, LLC at 540.535.8267 or via email at sales@AtebionLLC.com.", "Usage Period has Expired", MessageBoxButtons.OK, MessageBoxIcon.Information);

                OpenUserInfor(true);
            }
            else if (_days < 16) // Added 08.05.2015
            {
                if (_days < 5)
                {
                    lblPrimary.ForeColor = Color.Red;
                }
                else if (_days < 10)
                {
                    lblPrimary.ForeColor = Color.Yellow;
                }

            }
            else
            {
                lblPrimary.ForeColor = Color.White;
            }

            //Test code
            //butPurchase.Visible = true;
            //lblPurchase.Visible = true;
            //lblPurchase.ForeColor = Color.DarkRed;


        }

        private bool OpenUserInfor(bool isExpired)
        {

            string[] users = _UserMgr.GetUserInforFiles4App();

            frmUserCard user;

            if (users.Length == 0)
            {
                user = new frmUserCard();

                if (user.ShowDialog(this) == DialogResult.OK)
                {
                    return false; 
                }
                else
                {
                    if (_LicMgr.Validate() == "Invalid")
                    {
                        return false;
                    }
                }
            }
            else
            {
                //if (openIfFound)
                //{
                user = new frmUserCard(users[0]);

                if (user.ShowDialog(this) == DialogResult.OK)
                {
                    return true;
                }
                else
                {
                    if (isExpired)
                    {
                        return false;
                    }
                }
                //}
            }

            return true;

        }

        private void SayCheckingSystem()
        {
            this.lblNotice.Text = "Checking System...";
            this.Refresh();

            //System.Threading.Thread.Sleep(2000);
        }

        private void SayWelcome()
        {
            string[] users = _UserMgr.GetUserInforFiles4App();

            if (users.Length == 0) // New user
            {
                OpenUserInfor(true);
                return;
            }

            _UserMgr.ReadUserFile(users[0], false);

            string userName = string.Concat(_UserMgr.UserFirstName, " ", _UserMgr.UserLastName);


            AppFolders.UserName = userName;

            if (userName.Trim() != string.Empty)
            {
                string Greeting = string.Empty;

                if (DateTime.Now.Hour < 12)
                {
                    Greeting = string.Concat(_UserMgr.UserFirstName, ", ", "Good Morning...");
                }
                else if (DateTime.Now.Hour < 17)
                {
                    Greeting = string.Concat(_UserMgr.UserFirstName, ", ", "Good Afternoon...");
                }
                else
                {
                    Greeting = string.Concat(_UserMgr.UserFirstName, ", ", "Good Evening ...");
                }

                this.lblNotice.Text = Greeting;
                lblNotice.Refresh();
                this.Refresh();

                AppFolders.UserName = string.Concat(_UserMgr.UserFirstName, " ", _UserMgr.UserLastName);

                System.Threading.Thread.Sleep(3000);
                
            }

        }

        private void Chk4LastestRelease() // Added 8.2.2015
        {
            LastestRelease lastestRelease;

            try
            {
                lastestRelease = new LastestRelease();
            }
            catch
            {
                string msg = "Please check your internet connection or have your System Administrator check your company’s firewall to ensure Atebion’s website (http://www.atebionllc.com/) is White Listed.";
                string cap = "Unable to Check for the Newest Version of the Professional Document Analyzer";
                MessageBox.Show(this, msg, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (lastestRelease.IsLatestReleaseNewer())
            {
                string cureentRelease = lastestRelease.CurrentRelease;
                string latestRelease = lastestRelease.LatestRelease;

                string log = lastestRelease.ReleasesLog();

                string msg = string.Concat("Notice: The Professional Document Analyzer release you are using is older than the latest release.",
                    Environment.NewLine,
                    "Current Release: ", cureentRelease,
                    Environment.NewLine,
                    "Lastest Release:  ", latestRelease);

                IniFile inifile = new IniFile();
                inifile.Load(AppFolders.InIFile);
                string iniLatestRelease = inifile.GetKeyValue(CurrentSettings.configSecUserSettings, CurrentSettings.configKeyLatestRelease);


                bool showLastestReleaseInfo = false;
                if (latestRelease.ToString() != iniLatestRelease)
                {
                    showLastestReleaseInfo = true;
                }
                else
                {
                    string iniHideLastestReleaseInfo = inifile.GetKeyValue(CurrentSettings.configSecUserSettings, CurrentSettings.configKeyHideLastestRelease);
                    if (iniHideLastestReleaseInfo == "No")
                        showLastestReleaseInfo = true;
                }

                if (showLastestReleaseInfo)
                {
                    frmLastestReleaseInfo xfrmLastestReleaseInfo = new frmLastestReleaseInfo(msg, log, latestRelease.ToString(), true);
                    xfrmLastestReleaseInfo.ShowDialog();
                }

            }

        }


        /// <summary>
        /// Check to determine if Emails should be sent when the application opens
        /// </summary>
        private void ChkNoOpenEmails() // // Added 06.18.2014
        {
            string NoOpenEmails_FileName = "NoOpenEmails.key";
            string machineName = Environment.MachineName;
            string NoOpenEmails_DataPath = string.Concat(AppFolders.AppDataPath, @"\", NoOpenEmails_FileName);
            string NoOpenEmails_AppPath = string.Concat(Application.StartupPath, @"\", NoOpenEmails_FileName);
            string NoOpenEmails_Key = "NoOpenEmails555";
            string NoOpenEmails_EncryptResults = string.Empty;
            string NoOpenEmails_DecryptResults = string.Empty;

            if (File.Exists(NoOpenEmails_DataPath)) // Check for the Key in the Application Data Path
            {
                NoOpenEmails_EncryptResults = Files.ReadFile(NoOpenEmails_DataPath);
            }
            else if (File.Exists(NoOpenEmails_AppPath)) // Check for the Key in the Application Path
            {
                NoOpenEmails_EncryptResults = Files.ReadFile(NoOpenEmails_AppPath);
            }

            if (NoOpenEmails_EncryptResults.Length > 0)
            {
                NoOpenEmails_DecryptResults = SSTCryptographer.Decrypt(NoOpenEmails_EncryptResults, NoOpenEmails_Key);


                if (NoOpenEmails_DecryptResults.ToLower() == machineName.ToLower())
                {
                    _SendOpenEmail = false;
                }
            }

        }

        private void GetUserInformation()
        {

            string[] users = _UserMgr.GetUserInforFiles4App();

            if (users.Length != 0)
            {
                _UserFile = users[0];

                if (!_UserMgr.ReadUserFile(_UserFile, false))
                {
                    return;
                }

                StringBuilder sbUserInfor = new StringBuilder();

                sbUserInfor.AppendLine(string.Concat("Name: ", _UserMgr.UserPrefix, " ", _UserMgr.UserFirstName, " ", _UserMgr.UserMiddleName, " ", _UserMgr.UserLastName));
                sbUserInfor.AppendLine(string.Concat("Title: ", _UserMgr.UserTitle));
                sbUserInfor.AppendLine(" ");

                sbUserInfor.AppendLine(string.Concat("Company: ", _UserMgr.CompanyName));
                sbUserInfor.AppendLine(_UserMgr.CompanyStAddress);
                sbUserInfor.AppendLine(_UserMgr.CompanyCity);
                sbUserInfor.AppendLine(_UserMgr.CompanyZipCode);
                sbUserInfor.AppendLine(_UserMgr.CompanyState);

                sbUserInfor.AppendLine(" ");
                sbUserInfor.AppendLine(string.Concat("Web Site: ", _UserMgr.CompanyWebSite));

                sbUserInfor.AppendLine(" ");
                sbUserInfor.AppendLine(string.Concat("Email: ", _UserMgr.UserEmail));
                sbUserInfor.AppendLine(string.Concat("Phone: ", _UserMgr.UserPhone));


                sbUserInfor.AppendLine(" ");
                sbUserInfor.AppendLine(_LicMgr.GetLicense());

                sbUserInfor.AppendLine(" ");
                sbUserInfor.Append(string.Concat("DocAnalyzerVersion: ", Application.ProductVersion, Environment.NewLine)); // Added 08.21.2014
                sbUserInfor.Append(string.Concat("MachineName: ", Environment.MachineName, Environment.NewLine));
                sbUserInfor.Append(string.Concat("ApplicationPath: ", Application.StartupPath, Environment.NewLine));
                sbUserInfor.Append(string.Concat("ApplicationDataPath: ", AppFolders.AppDataPath, Environment.NewLine));
                sbUserInfor.Append(string.Concat("IE Version: ", Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Internet Explorer").GetValue("Version").ToString(), Environment.NewLine)); // Added 03.05.2013

                if (_SendOpenEmail) // If false then the user has a key to turn off the Send Open Email -- Added 06.18.2014
                {
                    SendMail(sbUserInfor.ToString());
                }

            }



        }

        private void SendMail(string userInfor)
        {

            EmailAtebion emailAtebion = new EmailAtebion();

            emailAtebion.Body = userInfor;

            emailAtebion.Subject = "Scion Analytics Professional Document Analyzer -- New Edition -- Opened Application";

            emailAtebion.SendEmail(true);



            // source: http://www.codeproject.com/KB/IP/SendMailUsingGmailAccount.aspx
        }


        private void ucStart_Paint(object sender, PaintEventArgs e)
        {
            if (this.ClientRectangle.Height == 0 && this.ClientRectangle.Width == 0) return;

            //get the graphics object of the control 
            Graphics g = e.Graphics;

            //The drawing gradient brush 
            LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, Color.DarkGreen, Color.Blue, 50);

            //Fill the client area with the gradient brush using the control's graphics object 
            g.FillRectangle(brush, this.ClientRectangle);
        }

        private void butPurchase_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"http://www.atebionllc.com/#pricing");
        }

        private void butPurchase_MouseEnter(object sender, EventArgs e)
        {
            butPurchase.BackColor = Color.Lime;
        }

        private void butPurchase_MouseLeave(object sender, EventArgs e)
        {
            butPurchase.BackColor = Color.DarkGreen;
        }

        private void butrRegister_MouseEnter(object sender, EventArgs e)
        {
            butrRegister.BackColor = Color.Blue;
        }

        private void butrRegister_MouseLeave(object sender, EventArgs e)
        {
            butrRegister.BackColor = Color.Navy;
        }

        private void butrRegister_Click(object sender, EventArgs e)
        {
            frmUserCard frm;

            if (_UserFile.Length == 0)
                frm = new frmUserCard();
            else
                frm = new frmUserCard(_UserFile);


            frm.ShowDialog(this);

            if (CheckLicense())
            {
                if (Completed != null)
                    Completed();
            }

        }
    }
}
