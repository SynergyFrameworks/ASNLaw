using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Diagnostics;
using Atebion.Outlook;

//using System.Runtime.InteropServices;


namespace ProfessionalDocAnalyzer
{
    public partial class frmUserCard : MetroFramework.Forms.MetroForm
    {
        public frmUserCard()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();

            this.AcceptButton = this.butOK;
            this.CancelButton = this.butCancel;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            //this.MaximizeBox = false;
            //this.MinimizeBox = false;
            //this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            btnActivateTrial.Visible = true;

            isNew = true;
            LoadLists();

            LoadMessagebox();
        }

        public frmUserCard(string userFile)
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();

            this.AcceptButton = this.butOK;
            this.CancelButton = this.butCancel;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            //this.MaximizeBox = false;
            //this.MinimizeBox = false;
            //this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            btnActivateTrial.Visible = false;

            isNew = false;
            LoadLists();

            _UserFile = userFile;

            PopulateForm();

            LoadMessagebox();
        }


        private bool isNew = false;
        private string _Message = string.Empty;
        private string _UserFile = string.Empty;
        private bool _EmailSent = false;
        private string AutoTrialMessage = string.Empty;

        private LicenseMgr _LicMgr = new LicenseMgr();

        private UserCardMgr _UserMgr = new UserCardMgr();

        private void LoadMessagebox()
        {
            lblMessage.Text = "To purchase user license key(s) for this application, please click the Purchase Online button or contact Scion Analytics' Customer Relations at 540-535-8267 or via email at Sales@ScionAnalytics.com";

            // Add a link to the lnkAtebion.
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = "http://www.ScionAnalytics.com/";
            lnkAtebion.Links.Add(link);
        }

        private void LoadLists()
        {
            populateCountries();

            cboUserPrefix.Items.Clear();
            string[] prefix = 
            {
                "Mr.",
                "Ms."
            };

            cboUserPrefix.DataSource = prefix;

            cboCompanyState.Items.Clear();

            string[] states = 
            { 
                "Alabama",
                "Alaska",
                "American Samoa",
                "Arizona",
                "Arkansas",
                "California",
                "Colorado",
                "Connecticut",
                "Delaware",
                "District of Columbia",
                "Florida",
                "Georgia",
                "Guam",
                "Hawaii",
                "Idaho",
                "Illinois",
                "Indiana",
                "Iowa",
                "Kansas",
                "Kentucky",
                "Louisiana",
                "Maine",
                "Maryland",
                "Massachusetts",
                "Michigan",
                "Minnesota",
                "Mississippi",
                "Missouri",
                "Montana",
                "Nebraska",
                "Nevada",
                "New Hampshire",
                "New Jersey",
                "New Mexico",
                "New York",
                "North Carolina",
                "North Dakota",
                "Northern Marianas Islands",
                "Ohio",
                "Oklahoma",
                "Oregon",
                "Pennsylvania",
                "Puerto Rico",
                "Rhode Island",
                "South Carolina",
                "South Dakota",
                "Tennessee",
                "Texas",
                "Utah",
                "Vermont",
                "Virginia",
                "Virgin Islands",
                "Washington",
                "West Virginia",
                "Wisconsin",
                "Wyoming"
            };

            cboCompanyState.DataSource = states;

        }

        private bool PopulateForm()
        {
            if (!_UserMgr.ReadUserFile(_UserFile, false))
            {
                _Message = _UserMgr.Message;
                MessageBox.Show(_Message, "Error: Unable to Open User File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            txtCompanyCity.Text = _UserMgr.CompanyCity;
            txtCompanyName.Text = _UserMgr.CompanyName;
            txtCompanyStAddress.Text = _UserMgr.CompanyStAddress;
            this.cboCompanyState.Text = _UserMgr.CompanyState;
            txtCompanyWebSite.Text = _UserMgr.CompanyWebSite;
            txtCompanyZipCode.Text = _UserMgr.CompanyZipCode;
            txtUserEmail.Text = _UserMgr.UserEmail;
            txtUserPhone.Text = _UserMgr.UserPhone;
            txtbUserFirstName.Text = _UserMgr.UserFirstName;
            txtbUserMiddleName.Text = _UserMgr.UserMiddleName;
            txtbUserLastName.Text = _UserMgr.UserLastName;
            _UserMgr.UserMiddleName = txtbUserMiddleName.Text;
            this.cboUserPrefix.Text = _UserMgr.UserPrefix;
            txtUserTitle.Text = _UserMgr.UserTitle;

            txtbLicense.Text = _LicMgr.GetLicense();

            int intCountry = 0;
            string country = _UserMgr.Country;
            if (country.Length == 0)
            {
                intCountry = this.cboCountry.FindStringExact("United States");
            }
            else
            {
                intCountry = this.cboCountry.FindStringExact(country);
            }
            cboCountry.SelectedIndex = intCountry;


            return true;
        }


        private bool Validate()
        {

            if (!ChkTextbox(this.txtbUserFirstName, "First Name"))
                return false;

            if (!ChkTextbox(this.txtbUserLastName, "Last Name"))
                return false;

            if (!ChkTextbox(this.txtCompanyCity, "Company's City"))
                return false;

            if (!ChkTextbox(this.txtCompanyName, "Company Name"))
                return false;

            if (!ChkTextbox(this.txtCompanyName, "Company Street Address"))
                return false;

            if (!ChkTextbox(this.txtCompanyZipCode, "Company's Zip Code"))
                return false;

            if (!ChkTextbox(this.txtCompanyZipCode, "Company's Zip Code"))
                return false;

            if (!ChkTextbox(this.txtUserEmail, "Your Email"))
                return false;

            if (cboCountry.Text.Length == 0)
            {
                MessageBox.Show("Please select a Country.", "Invalid Email Address", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (!Atebion.Common.DataFunctions.IsValidEmail(this.txtUserEmail.Text))
            {
                _Message = "Please enter a valid email.";
                MessageBox.Show(_Message, "Invalid Email Address", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (!ChkTextbox(this.txtUserPhone, "Your Phone Number"))
                return false;

            if (!ChkTextbox(this.txtUserTitle, "Your Title"))
                return false;

            if (!ChkTextbox(this.cboCompanyState, "Company State"))
                return false;

            if (!ChkTextbox(this.txtbLicense, "Your License Information"))
                return false;

            return true;
            
        }

        private bool ChkTextbox(Control txtbox, string title)
        {
            if (txtbox.Text.Trim() == string.Empty)
            {
                _Message = string.Concat("Please enter your ",title);
                MessageBox.Show(_Message, string.Concat(title, " Required"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            return true;
        }

        private void populateCountries()
        {
            if (_UserMgr == null)
                _UserMgr = new UserCardMgr();

            this.cboCountry.Items.Clear();

         //   this.cboCountry.DataSource = _UserMgr.CountryList();
            this.cboCountry.DataSource = _UserMgr.CountryList2(); // Added 10.13.2014

            int countryIndex = cboCountry.FindString("United States");
            if (countryIndex != -1)
                cboCountry.SelectedIndex = countryIndex;

            
        }

        private void populateUserMgr()
        {
            if (_UserMgr == null)
                _UserMgr = new UserCardMgr();

            _UserMgr.CompanyCity = txtCompanyCity.Text;
            _UserMgr.CompanyName = txtCompanyName.Text;
            _UserMgr.CompanyStAddress = txtCompanyStAddress.Text;
            _UserMgr.CompanyState = this.cboCompanyState.SelectedValue.ToString();
            _UserMgr.CompanyWebSite = txtCompanyWebSite.Text;
            _UserMgr.CompanyZipCode = txtCompanyZipCode.Text;
            _UserMgr.Country = cboCountry.Text;
            _UserMgr.UserEmail = txtUserEmail.Text;
            _UserMgr.UserPhone = txtUserPhone.Text;
            _UserMgr.UserFirstName = txtbUserFirstName.Text;
            _UserMgr.UserLastName = txtbUserLastName.Text;
            _UserMgr.UserMiddleName = txtbUserMiddleName.Text;
            _UserMgr.UserPrefix = this.cboUserPrefix.SelectedValue.ToString();
            _UserMgr.UserTitle = txtUserTitle.Text;

            
        }

        private string populateEmailContent()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(string.Concat("Release: ", Application.ProductVersion, Environment.NewLine));
            sb.Append(string.Concat("UserPrefix: ", this.cboUserPrefix.SelectedValue.ToString(), Environment.NewLine));
            sb.Append(string.Concat("UserFirstName: ", txtbUserFirstName.Text, Environment.NewLine));
            sb.Append(string.Concat("UserMiddleName: ", txtbUserMiddleName.Text, Environment.NewLine));
            sb.Append(string.Concat("UserLastName: ", txtbUserLastName.Text, Environment.NewLine));
            sb.Append(string.Concat("UserEmail: ", this.txtUserEmail.Text, Environment.NewLine));
            sb.Append(string.Concat("UserPhone: ", this.txtUserPhone.Text, Environment.NewLine));
            sb.Append(string.Concat("UserTitle: ", this.txtUserTitle.Text, Environment.NewLine));
            sb.Append(string.Concat("CompanyName: ", this.txtCompanyName.Text, Environment.NewLine));
            sb.Append(string.Concat("CompanyStAddress: ", this.txtCompanyStAddress.Text, Environment.NewLine));
            sb.Append(string.Concat("CompanyCity: ", this.txtCompanyCity.Text, Environment.NewLine));
            sb.Append(string.Concat("CompanyState: ", this.cboCompanyState.SelectedValue.ToString(), Environment.NewLine));
            sb.Append(string.Concat("CompanyZipCode: ", this.txtCompanyZipCode.Text, Environment.NewLine));
            sb.Append(string.Concat("Country: ", this.cboCountry.Text, Environment.NewLine));
            sb.Append(string.Concat("CompanyWebSite: ", this.txtCompanyWebSite.Text, Environment.NewLine));
            sb.Append(string.Concat("MachineName: ", Environment.MachineName, Environment.NewLine));
            sb.Append(string.Concat("ApplicationPath: ", Application.StartupPath, Environment.NewLine));
            sb.Append(string.Concat("ApplicationDataPath: ", AppFolders.AppDataPath, Environment.NewLine));
            sb.Append(string.Concat("IE Version: ", Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Internet Explorer").GetValue("Version").ToString(), Environment.NewLine)); // Added 03.05.2013
    //        System.OperatingSystem.
            sb.Append(string.Concat("Notes: ", AutoTrialMessage, Environment.NewLine));
            sb.Append(string.Concat("UserLicense: ", txtbLicense.Text, Environment.NewLine));

            return sb.ToString();

        }

        private bool UpdateUserInformation()
        {
            _Message = string.Empty;
            populateUserMgr();

            if (!_UserMgr.UpdateUserFile(_UserFile, false))
            {
                _Message = _UserMgr.Message;
                return false;
            }

            return true;
        }


        private bool CreateNewUser()
        {
            bool returnValue = false;

            _Message = string.Empty;
            populateUserMgr();

            if (!_UserMgr.CreateNewUser())
                _Message = _UserMgr.Message;
            else
                returnValue = true;

            return returnValue;

        }

        private void SendMail(string bodyContent)
        {

            EmailAtebion emailAtebion = new EmailAtebion();

            if (isNew)
                emailAtebion.Subject = "Scion Analytics Doc Analyzer -- New User Information";
            else
                emailAtebion.Subject = "Scion Analytics Doc Analyzer -- Update User Information";

            emailAtebion.Body = bodyContent;

            if (emailAtebion.SendEmail(true))
            {
                MessageBox.Show("Your registration information was sent to Scion Analytics.", "Registration Information Sent to Scion Analytics", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Your registration information was NOT sent to Scion Analytics." + "  " + emailAtebion.ErrorMessage, "Registration Information Not Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
 
            }



            //// source: http://www.codeproject.com/KB/IP/SendMailUsingGmailAccount.aspx

            ////Builed The MSG
            //System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            //msg.To.Add("Tom_w_Lipscomb@yahoo.com");
            //msg.To.Add("tlipscomb@hotmail.com");
            //msg.To.Add("tlipscomb@atebionllc.com");
            //msg.From = new MailAddress("tlipscome555@gmail.com", "Atebion Doc Analyzer", System.Text.Encoding.UTF8);
            //if (isNew)
            //    msg.Subject = "Atebion Doc Analyzer -- New User Information";
            //else
            //    msg.Subject = "Atebion Doc Analyzer -- Update User Information";

            //msg.SubjectEncoding = System.Text.Encoding.UTF8;
            //msg.Body = bodyContent;
            //msg.BodyEncoding = System.Text.Encoding.UTF8;
            //msg.IsBodyHtml = false;
            //msg.Priority = MailPriority.High;

            ////Add the Creddentials
            //SmtpClient client = new SmtpClient();
            //client.Credentials = new System.Net.NetworkCredential
            //    ("tlipscome555@gmail.com", "az511t26l59**");
            //client.Port = 587;//or use 587            
            //client.Host = "smtp.gmail.com";
            //client.EnableSsl = true;
            //client.SendCompleted += new SendCompletedEventHandler
            //    (client_SendCompleted);
            //object userState = msg;

            
            //try
            //{

            //    Atebion.Outlook.Email email2 = new Email();
            //    bool emailSent = email2.SendEmail("tlipscomb@AtebionLLC.com", msg.Subject, msg.Body + Environment.NewLine + "Notice: Sent via Outlook");
            //    if (emailSent)
            //    {
            //        MessageBox.Show("Your registration information was sent to Atebion, LLC via your MS Outlook configuration. See information sent from your Outlook Sent folder.", "Registration Information Sent to Atebion, LLC", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
 
            //}
            //catch (Exception ex)
            //{
            //    try
            //    {
            //        //you can also call client.Send(msg)
            //        if (!sendEmailViaYahoo(msg.Subject, bodyContent + Environment.NewLine + "Notice: Sent via Yahoo"))
            //        {
            //            client.SendAsync(msg, userState);
            //        }
            //        MessageBox.Show("Your registration information was sent to Atebion, LLC.", "Registration Information Sent to Atebion, LLC", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
            //    catch (Exception ex2)
            //    {
            //        MessageBox.Show("Unable to send your registration information to Atebion, LLC." + Environment.NewLine + "Please check your internet connection.", "Registration Information Not Sent", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        return;
            //    }

            //}

            //client.SendAsync(msg, userState);
        }

        private bool sendEmailViaYahoo(string subject, string body)
        {
            string smtpAddress = "smtp.mail.yahoo.com";
            int portNumber = 587;
            bool enableSSL = true;

            string emailFrom = "atebion.lipscomb@yahoo.com";
            string password = "Today=35";
            string emailTo = "tlipscomb@atebionllc.com";

            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

                return true;
            }
            catch (Exception exYahoo)
            {
                return false;
            }
        }

        private void client_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MailMessage mail = (MailMessage)e.UserState;
            string subject = mail.Subject;

            if (e.Cancelled)
            {
                string cancelled = string.Format("[{0}] Send canceled.", subject);
             //   MessageBox.Show(cancelled, "Cancelled -- Message Not Sent", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _EmailSent = false;
            }
            if (e.Error != null)
            {
                string error = String.Format("[{0}] {1}", subject, e.Error.ToString());
           //     MessageBox.Show(error, "Error -- Message Not Sent", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _EmailSent = false;
            }
            else
            {
                _EmailSent = true;
            }

        }

        private void butOK_Click(object sender, EventArgs e)
        {
            if (!Validate())
                return;

            if (isNew)
            {
                if (!CreateNewUser())
                {
                    MessageBox.Show(_Message, "Error: Unable to Create New User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string emailContent = populateEmailContent();

                SendMail(emailContent);

                if (!_EmailSent)
                {
                    // MessageBox.Show("Unable ")
                }

                bool isValid = _LicMgr.SaveLicense(txtbLicense.Text);
                if (isValid)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please enter the correct License Information or contact Scion Analytics for another License.", "License key has expired or is Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

            }
            else // Update
            {
                if (!UpdateUserInformation())
                {
                    MessageBox.Show(_Message, "Error: Unable to Your Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string emailContent = populateEmailContent();

                SendMail(emailContent);

                if (!_EmailSent)
                {
                    // MessageBox.Show("Unable ")
                }
                bool isValid = _LicMgr.SaveLicense(txtbLicense.Text);
                if (isValid)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please enter the correct License Information or contact Scion Analytics for another License.", "The License Information is Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            if (_LicMgr.Validate() == "Invalid")
            {
                MessageBox.Show("Please enter the correct License Information or contact Scion Analytics for another License.", "The License Information is Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                Application.Exit();
            }
            if (txtbLicense.Text.Trim() == string.Empty)
            {
                Application.Exit();
            }
        }

        private void btnActivateTrial_Click(object sender, EventArgs e)
        {
            LicenseMgr lMgr = new LicenseMgr();
            txtbLicense.Text = lMgr.ActivateTrial(7); // Changed from 7 to 30 days

            AutoTrialMessage = "User selected 7 Day Trial"; // Changed from 7 to 30 days
        }

        private void lblMessage_TextChanged(object sender, EventArgs e)
        {
            this.txtMessage.Text = lblMessage.Text;
        }

        private void txtbLicense_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            this.txtMessage.Text = lblMessage.Text;
        }

        private void cboCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCountry.SelectedItem.ToString().IndexOf("United States") > -1)
            {
                cboCompanyState.Visible = true;
                txtState.Visible = false;
            }
            else
            {
                cboCompanyState.Visible = false;
                txtState.Visible = true;
            }
        }

        private void butPurchaseOnline_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://www.scionanalytics.com/");
        }

        private void lnkAtebion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }

  

  


    }
}
