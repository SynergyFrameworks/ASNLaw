namespace ProfessionalDocAnalyzer
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.panHeader = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.butTest = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.butInformation = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.butPerson = new System.Windows.Forms.PictureBox();
            this.panFooter = new System.Windows.Forms.Panel();
            this.butImport = new System.Windows.Forms.Button();
            this.butDownload = new System.Windows.Forms.Button();
            this.butDelete = new System.Windows.Forms.Button();
            this.butNew = new System.Windows.Forms.Button();
            this.butEdit = new System.Windows.Forms.Button();
            this.butBackToWelcome = new System.Windows.Forms.Button();
            this.panFooterRight = new System.Windows.Forms.Panel();
            this.butBack = new System.Windows.Forms.Button();
            this.Butnext = new System.Windows.Forms.Button();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.ucStart1 = new ProfessionalDocAnalyzer.ucStart();
            this.ucDeepAnalyticsResults1 = new ProfessionalDocAnalyzer.ucDeepAnalyticsResults();
            this.ucAcroSeekerResults1 = new ProfessionalDocAnalyzer.ucAcroSeekerResults();
            this.ucDiffSxS1 = new ProfessionalDocAnalyzer.ucDiffSxS();
            this.ucResults1 = new ProfessionalDocAnalyzer.ucResults();
            this.ucResultsMultiDic1 = new ProfessionalDocAnalyzer.ucResultsMultiDic();
            this.ucResultsDic1 = new ProfessionalDocAnalyzer.ucResultsDic();
            this.ucResultsMultiConcepts1 = new ProfessionalDocAnalyzer.ucResultsMultiConcepts();
            this.ucDictionarySelect1 = new ProfessionalDocAnalyzer.ucDictionarySelect();
            this.ucResultsConcepts1 = new ProfessionalDocAnalyzer.ucResultsConcepts();
            this.ucProjectsDocs1 = new ProfessionalDocAnalyzer.ucProjectsDocs();
            this.ucSettings1 = new ProfessionalDocAnalyzer.ucSettings();
            this.ucAnalysisResults1 = new ProfessionalDocAnalyzer.ucAnalysisResults();
            this.ucKeywordsSelect1 = new ProfessionalDocAnalyzer.ucKeywordsSelect();
            this.ucTasksSelect1 = new ProfessionalDocAnalyzer.ucTasksSelect();
            this.ucWelcome1 = new ProfessionalDocAnalyzer.ucWelcome();
            this.ucQCAnalysisResults1 = new ProfessionalDocAnalyzer.ucQCAnalysisResults();
            this.ucTasks1 = new ProfessionalDocAnalyzer.ucTasks();
            this.butExport = new System.Windows.Forms.Button();
            this.panHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butInformation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.butPerson)).BeginInit();
            this.panFooter.SuspendLayout();
            this.panFooterRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.AutoSize = true;
            this.panHeader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panHeader.Controls.Add(this.pictureBox2);
            this.panHeader.Controls.Add(this.butTest);
            this.panHeader.Controls.Add(this.panel1);
            this.panHeader.Controls.Add(this.lblUser);
            this.panHeader.Controls.Add(this.butPerson);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 38);
            this.panHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(1463, 52);
            this.panHeader.TabIndex = 0;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(15, 6);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(232, 41);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // butTest
            // 
            this.butTest.Location = new System.Drawing.Point(500, 16);
            this.butTest.Margin = new System.Windows.Forms.Padding(4);
            this.butTest.Name = "butTest";
            this.butTest.Size = new System.Drawing.Size(100, 28);
            this.butTest.TabIndex = 4;
            this.butTest.Text = "Test";
            this.butTest.UseVisualStyleBackColor = true;
            this.butTest.Visible = false;
            this.butTest.Click += new System.EventHandler(this.butTest_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.butInformation);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(911, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(552, 52);
            this.panel1.TabIndex = 3;
            // 
            // butInformation
            // 
            this.butInformation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butInformation.Image = global::ProfessionalDocAnalyzer.Properties.Resources.appbar_information_circle;
            this.butInformation.Location = new System.Drawing.Point(4, 4);
            this.butInformation.Margin = new System.Windows.Forms.Padding(4);
            this.butInformation.Name = "butInformation";
            this.butInformation.Size = new System.Drawing.Size(51, 47);
            this.butInformation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.butInformation.TabIndex = 4;
            this.butInformation.TabStop = false;
            this.butInformation.Click += new System.EventHandler(this.butInformation_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(57, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(389, 32);
            this.label1.TabIndex = 3;
            this.label1.Text = "Professional Document Analyzer";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblUser.Location = new System.Drawing.Point(323, 16);
            this.lblUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(97, 19);
            this.lblUser.TabIndex = 2;
            this.lblUser.Text = "Tom Lipscomb";
            // 
            // butPerson
            // 
            this.butPerson.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butPerson.Image = global::ProfessionalDocAnalyzer.Properties.Resources.appbar1;
            this.butPerson.Location = new System.Drawing.Point(268, 5);
            this.butPerson.Margin = new System.Windows.Forms.Padding(4);
            this.butPerson.Name = "butPerson";
            this.butPerson.Size = new System.Drawing.Size(45, 43);
            this.butPerson.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.butPerson.TabIndex = 1;
            this.butPerson.TabStop = false;
            this.butPerson.Click += new System.EventHandler(this.butPerson_Click);
            // 
            // panFooter
            // 
            this.panFooter.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panFooter.Controls.Add(this.butExport);
            this.panFooter.Controls.Add(this.butImport);
            this.panFooter.Controls.Add(this.butDownload);
            this.panFooter.Controls.Add(this.butDelete);
            this.panFooter.Controls.Add(this.butNew);
            this.panFooter.Controls.Add(this.butEdit);
            this.panFooter.Controls.Add(this.butBackToWelcome);
            this.panFooter.Controls.Add(this.panFooterRight);
            this.panFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panFooter.Location = new System.Drawing.Point(0, 586);
            this.panFooter.Margin = new System.Windows.Forms.Padding(4);
            this.panFooter.Name = "panFooter";
            this.panFooter.Size = new System.Drawing.Size(1463, 64);
            this.panFooter.TabIndex = 1;
            this.panFooter.Visible = false;
            // 
            // butImport
            // 
            this.butImport.BackColor = System.Drawing.Color.Navy;
            this.butImport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butImport.ForeColor = System.Drawing.Color.White;
            this.butImport.Location = new System.Drawing.Point(839, 9);
            this.butImport.Margin = new System.Windows.Forms.Padding(0);
            this.butImport.Name = "butImport";
            this.butImport.Size = new System.Drawing.Size(116, 47);
            this.butImport.TabIndex = 9;
            this.butImport.Text = "Import";
            this.butImport.UseVisualStyleBackColor = false;
            this.butImport.Visible = false;
            this.butImport.Click += new System.EventHandler(this.butImport_Click);
            this.butImport.MouseEnter += new System.EventHandler(this.butImport_MouseEnter);
            this.butImport.MouseLeave += new System.EventHandler(this.butImport_MouseLeave);
            // 
            // butDownload
            // 
            this.butDownload.BackColor = System.Drawing.Color.Navy;
            this.butDownload.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butDownload.ForeColor = System.Drawing.Color.White;
            this.butDownload.Location = new System.Drawing.Point(685, 9);
            this.butDownload.Margin = new System.Windows.Forms.Padding(0);
            this.butDownload.Name = "butDownload";
            this.butDownload.Size = new System.Drawing.Size(131, 47);
            this.butDownload.TabIndex = 8;
            this.butDownload.Text = "Download";
            this.butDownload.UseVisualStyleBackColor = false;
            this.butDownload.Visible = false;
            this.butDownload.Click += new System.EventHandler(this.butDownload_Click);
            this.butDownload.MouseEnter += new System.EventHandler(this.butDownload_MouseEnter);
            this.butDownload.MouseLeave += new System.EventHandler(this.butDownload_MouseLeave);
            // 
            // butDelete
            // 
            this.butDelete.BackColor = System.Drawing.Color.DarkRed;
            this.butDelete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butDelete.ForeColor = System.Drawing.Color.White;
            this.butDelete.Location = new System.Drawing.Point(539, 9);
            this.butDelete.Margin = new System.Windows.Forms.Padding(0);
            this.butDelete.Name = "butDelete";
            this.butDelete.Size = new System.Drawing.Size(119, 47);
            this.butDelete.TabIndex = 7;
            this.butDelete.Text = "Delete";
            this.butDelete.UseVisualStyleBackColor = false;
            this.butDelete.Visible = false;
            this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
            this.butDelete.MouseEnter += new System.EventHandler(this.butDelete_MouseEnter);
            this.butDelete.MouseLeave += new System.EventHandler(this.butDelete_MouseLeave);
            // 
            // butNew
            // 
            this.butNew.BackColor = System.Drawing.Color.Navy;
            this.butNew.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butNew.ForeColor = System.Drawing.Color.White;
            this.butNew.Location = new System.Drawing.Point(403, 9);
            this.butNew.Margin = new System.Windows.Forms.Padding(0);
            this.butNew.Name = "butNew";
            this.butNew.Size = new System.Drawing.Size(113, 47);
            this.butNew.TabIndex = 6;
            this.butNew.Text = "New";
            this.butNew.UseVisualStyleBackColor = false;
            this.butNew.Visible = false;
            this.butNew.Click += new System.EventHandler(this.butNew_Click);
            this.butNew.MouseEnter += new System.EventHandler(this.butNew_MouseEnter);
            this.butNew.MouseLeave += new System.EventHandler(this.butNew_MouseLeave);
            // 
            // butEdit
            // 
            this.butEdit.BackColor = System.Drawing.Color.DarkGreen;
            this.butEdit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butEdit.ForeColor = System.Drawing.Color.White;
            this.butEdit.Location = new System.Drawing.Point(254, 9);
            this.butEdit.Margin = new System.Windows.Forms.Padding(0);
            this.butEdit.Name = "butEdit";
            this.butEdit.Size = new System.Drawing.Size(125, 47);
            this.butEdit.TabIndex = 5;
            this.butEdit.Text = "Edit";
            this.butEdit.UseVisualStyleBackColor = false;
            this.butEdit.Visible = false;
            this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
            this.butEdit.MouseEnter += new System.EventHandler(this.butEdit_MouseEnter);
            this.butEdit.MouseLeave += new System.EventHandler(this.butEdit_MouseLeave);
            // 
            // butBackToWelcome
            // 
            this.butBackToWelcome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(3)))), ((int)(((byte)(51)))));
            this.butBackToWelcome.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butBackToWelcome.ForeColor = System.Drawing.Color.White;
            this.butBackToWelcome.Location = new System.Drawing.Point(12, 9);
            this.butBackToWelcome.Margin = new System.Windows.Forms.Padding(0);
            this.butBackToWelcome.Name = "butBackToWelcome";
            this.butBackToWelcome.Size = new System.Drawing.Size(185, 47);
            this.butBackToWelcome.TabIndex = 4;
            this.butBackToWelcome.Text = "Back to Welcome";
            this.butBackToWelcome.UseVisualStyleBackColor = false;
            this.butBackToWelcome.Click += new System.EventHandler(this.butBackToWelcome_Click);
            this.butBackToWelcome.MouseEnter += new System.EventHandler(this.butBackToWelcome_MouseEnter);
            this.butBackToWelcome.MouseLeave += new System.EventHandler(this.butBackToWelcome_MouseLeave);
            // 
            // panFooterRight
            // 
            this.panFooterRight.Controls.Add(this.butBack);
            this.panFooterRight.Controls.Add(this.Butnext);
            this.panFooterRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panFooterRight.Location = new System.Drawing.Point(1074, 0);
            this.panFooterRight.Margin = new System.Windows.Forms.Padding(4);
            this.panFooterRight.Name = "panFooterRight";
            this.panFooterRight.Size = new System.Drawing.Size(389, 64);
            this.panFooterRight.TabIndex = 0;
            // 
            // butBack
            // 
            this.butBack.BackColor = System.Drawing.Color.Navy;
            this.butBack.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butBack.ForeColor = System.Drawing.Color.White;
            this.butBack.Location = new System.Drawing.Point(53, 9);
            this.butBack.Margin = new System.Windows.Forms.Padding(0);
            this.butBack.Name = "butBack";
            this.butBack.Size = new System.Drawing.Size(151, 47);
            this.butBack.TabIndex = 3;
            this.butBack.Text = "Back";
            this.butBack.UseVisualStyleBackColor = false;
            this.butBack.Click += new System.EventHandler(this.butBack_Click);
            this.butBack.MouseEnter += new System.EventHandler(this.butBack_MouseEnter);
            this.butBack.MouseLeave += new System.EventHandler(this.butBack_MouseLeave);
            // 
            // Butnext
            // 
            this.Butnext.BackColor = System.Drawing.Color.DarkGreen;
            this.Butnext.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Butnext.ForeColor = System.Drawing.Color.White;
            this.Butnext.Location = new System.Drawing.Point(220, 9);
            this.Butnext.Margin = new System.Windows.Forms.Padding(0);
            this.Butnext.Name = "Butnext";
            this.Butnext.Size = new System.Drawing.Size(151, 47);
            this.Butnext.TabIndex = 2;
            this.Butnext.Text = "Next";
            this.Butnext.UseVisualStyleBackColor = false;
            this.Butnext.Click += new System.EventHandler(this.Butnext_Click);
            this.Butnext.MouseEnter += new System.EventHandler(this.Butnext_MouseEnter);
            this.Butnext.MouseLeave += new System.EventHandler(this.Butnext_MouseLeave);
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // ucStart1
            // 
            this.ucStart1.BackColor = System.Drawing.Color.White;
            this.ucStart1.Location = new System.Drawing.Point(9, -642);
            this.ucStart1.Margin = new System.Windows.Forms.Padding(5);
            this.ucStart1.Name = "ucStart1";
            this.ucStart1.Size = new System.Drawing.Size(1693, 629);
            this.ucStart1.TabIndex = 21;
            this.ucStart1.Completed += new ProfessionalDocAnalyzer.ucStart.ProcessHandler(this.ucStart1_Completed);
            // 
            // ucDeepAnalyticsResults1
            // 
            this.ucDeepAnalyticsResults1.Location = new System.Drawing.Point(40, 98);
            this.ucDeepAnalyticsResults1.Margin = new System.Windows.Forms.Padding(5);
            this.ucDeepAnalyticsResults1.Name = "ucDeepAnalyticsResults1";
            this.ucDeepAnalyticsResults1.Size = new System.Drawing.Size(1693, 629);
            this.ucDeepAnalyticsResults1.TabIndex = 20;
            this.ucDeepAnalyticsResults1.Visible = false;
            // 
            // ucAcroSeekerResults1
            // 
            this.ucAcroSeekerResults1.BackColor = System.Drawing.Color.White;
            this.ucAcroSeekerResults1.Location = new System.Drawing.Point(107, 119);
            this.ucAcroSeekerResults1.Margin = new System.Windows.Forms.Padding(5);
            this.ucAcroSeekerResults1.Name = "ucAcroSeekerResults1";
            this.ucAcroSeekerResults1.Size = new System.Drawing.Size(1185, 625);
            this.ucAcroSeekerResults1.TabIndex = 19;
            this.ucAcroSeekerResults1.Visible = false;
            // 
            // ucDiffSxS1
            // 
            this.ucDiffSxS1.Location = new System.Drawing.Point(107, 97);
            this.ucDiffSxS1.Margin = new System.Windows.Forms.Padding(5);
            this.ucDiffSxS1.Name = "ucDiffSxS1";
            this.ucDiffSxS1.Size = new System.Drawing.Size(1703, 522);
            this.ucDiffSxS1.TabIndex = 18;
            this.ucDiffSxS1.Visible = false;
            // 
            // ucResults1
            // 
            this.ucResults1.BackColor = System.Drawing.Color.White;
            this.ucResults1.Location = new System.Drawing.Point(59, 137);
            this.ucResults1.Margin = new System.Windows.Forms.Padding(5);
            this.ucResults1.Name = "ucResults1";
            this.ucResults1.Size = new System.Drawing.Size(1252, 577);
            this.ucResults1.TabIndex = 16;
            this.ucResults1.Visible = false;
            this.ucResults1.ProjectSelected += new ProfessionalDocAnalyzer.ucResults.ProcessHandler(this.ucResults1_ProjectSelected);
            this.ucResults1.ProjectUnselected += new ProfessionalDocAnalyzer.ucResults.ProcessHandler(this.ucResults1_ProjectUnselected);
            this.ucResults1.AnalysisSelected += new ProfessionalDocAnalyzer.ucResults.ProcessHandler(this.ucResults1_AnalysisSelected);
            this.ucResults1.AnalysisUnselected += new ProfessionalDocAnalyzer.ucResults.ProcessHandler(this.ucResults1_AnalysisUnselected);
            this.ucResults1.RunDeepAnalysisResults += new ProfessionalDocAnalyzer.ucResults.ProcessHandler(this.ucResults1_RunDeepAnalysisResults);
            // 
            // ucResultsMultiDic1
            // 
            this.ucResultsMultiDic1.BackColor = System.Drawing.Color.Black;
            this.ucResultsMultiDic1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucResultsMultiDic1.Location = new System.Drawing.Point(80, 319);
            this.ucResultsMultiDic1.Margin = new System.Windows.Forms.Padding(5);
            this.ucResultsMultiDic1.Name = "ucResultsMultiDic1";
            this.ucResultsMultiDic1.Size = new System.Drawing.Size(1390, 724);
            this.ucResultsMultiDic1.TabIndex = 15;
            this.ucResultsMultiDic1.Visible = false;
            // 
            // ucResultsDic1
            // 
            this.ucResultsDic1.BackColor = System.Drawing.Color.Black;
            this.ucResultsDic1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucResultsDic1.Location = new System.Drawing.Point(133, 119);
            this.ucResultsDic1.Margin = new System.Windows.Forms.Padding(5);
            this.ucResultsDic1.Name = "ucResultsDic1";
            this.ucResultsDic1.Size = new System.Drawing.Size(1390, 770);
            this.ucResultsDic1.TabIndex = 14;
            this.ucResultsDic1.Visible = false;
            // 
            // ucResultsMultiConcepts1
            // 
            this.ucResultsMultiConcepts1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucResultsMultiConcepts1.Location = new System.Drawing.Point(313, 97);
            this.ucResultsMultiConcepts1.Margin = new System.Windows.Forms.Padding(5);
            this.ucResultsMultiConcepts1.Name = "ucResultsMultiConcepts1";
            this.ucResultsMultiConcepts1.Size = new System.Drawing.Size(1390, 465);
            this.ucResultsMultiConcepts1.TabIndex = 12;
            this.ucResultsMultiConcepts1.Visible = false;
            // 
            // ucDictionarySelect1
            // 
            this.ucDictionarySelect1.BackColor = System.Drawing.Color.White;
            this.ucDictionarySelect1.Location = new System.Drawing.Point(15, 224);
            this.ucDictionarySelect1.Margin = new System.Windows.Forms.Padding(5);
            this.ucDictionarySelect1.Name = "ucDictionarySelect1";
            this.ucDictionarySelect1.Size = new System.Drawing.Size(1236, 454);
            this.ucDictionarySelect1.TabIndex = 13;
            this.ucDictionarySelect1.Visible = false;
            this.ucDictionarySelect1.DicSelected += new ProfessionalDocAnalyzer.ucDictionarySelect.ProcessHandler(this.ucDictionarySelect1_DicSelected);
            // 
            // ucResultsConcepts1
            // 
            this.ucResultsConcepts1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucResultsConcepts1.Location = new System.Drawing.Point(528, 166);
            this.ucResultsConcepts1.Margin = new System.Windows.Forms.Padding(5);
            this.ucResultsConcepts1.Name = "ucResultsConcepts1";
            this.ucResultsConcepts1.Size = new System.Drawing.Size(1390, 770);
            this.ucResultsConcepts1.TabIndex = 9;
            this.ucResultsConcepts1.Visible = false;
            // 
            // ucProjectsDocs1
            // 
            this.ucProjectsDocs1.BackColor = System.Drawing.Color.White;
            this.ucProjectsDocs1.Location = new System.Drawing.Point(361, 274);
            this.ucProjectsDocs1.Margin = new System.Windows.Forms.Padding(5);
            this.ucProjectsDocs1.Name = "ucProjectsDocs1";
            this.ucProjectsDocs1.Size = new System.Drawing.Size(1023, 964);
            this.ucProjectsDocs1.TabIndex = 5;
            this.ucProjectsDocs1.Visible = false;
            this.ucProjectsDocs1.ProjectSelected += new ProfessionalDocAnalyzer.ucProjectsDocs.ProcessHandler(this.ucProjectsDocs1_ProjectSelected);
            this.ucProjectsDocs1.DocumentsSelected += new ProfessionalDocAnalyzer.ucProjectsDocs.ProcessHandler(this.ucProjectsDocs1_DocumentsSelected);
            this.ucProjectsDocs1.HasReset += new ProfessionalDocAnalyzer.ucProjectsDocs.ProcessHandler(this.ucProjectsDocs1_HasReset);
            // 
            // ucSettings1
            // 
            this.ucSettings1.Location = new System.Drawing.Point(963, 97);
            this.ucSettings1.Margin = new System.Windows.Forms.Padding(5);
            this.ucSettings1.Name = "ucSettings1";
            this.ucSettings1.Size = new System.Drawing.Size(1348, 689);
            this.ucSettings1.TabIndex = 8;
            this.ucSettings1.Visible = false;
            this.ucSettings1.MenuItemSelected += new ProfessionalDocAnalyzer.ucSettings.ProcessHandler(this.ucSettings1_MenuItemSelected);
            // 
            // ucAnalysisResults1
            // 
            this.ucAnalysisResults1.Location = new System.Drawing.Point(845, 119);
            this.ucAnalysisResults1.Margin = new System.Windows.Forms.Padding(5);
            this.ucAnalysisResults1.Name = "ucAnalysisResults1";
            this.ucAnalysisResults1.Size = new System.Drawing.Size(1369, 716);
            this.ucAnalysisResults1.TabIndex = 7;
            this.ucAnalysisResults1.Visible = false;
            this.ucAnalysisResults1.RunDeepAnalysisResults += new ProfessionalDocAnalyzer.ucAnalysisResults.ProcessHandler(this.ucAnalysisResults1_RunDeepAnalysisResults);
            // 
            // ucKeywordsSelect1
            // 
            this.ucKeywordsSelect1.BackColor = System.Drawing.Color.White;
            this.ucKeywordsSelect1.Location = new System.Drawing.Point(276, 394);
            this.ucKeywordsSelect1.Margin = new System.Windows.Forms.Padding(5);
            this.ucKeywordsSelect1.Name = "ucKeywordsSelect1";
            this.ucKeywordsSelect1.Padding = new System.Windows.Forms.Padding(40, 0, 40, 0);
            this.ucKeywordsSelect1.Size = new System.Drawing.Size(1189, 720);
            this.ucKeywordsSelect1.TabIndex = 6;
            this.ucKeywordsSelect1.Visible = false;
            this.ucKeywordsSelect1.KeywordLibSelected += new ProfessionalDocAnalyzer.ucKeywordsSelect.ProcessHandler(this.ucKeywordsSelect1_KeywordLibSelected);
            this.ucKeywordsSelect1.KeywordsNotSelected += new ProfessionalDocAnalyzer.ucKeywordsSelect.ProcessHandler(this.ucKeywordsSelect1_KeywordsNotSelected);
            // 
            // ucTasksSelect1
            // 
            this.ucTasksSelect1.BackColor = System.Drawing.Color.White;
            this.ucTasksSelect1.Location = new System.Drawing.Point(13, 224);
            this.ucTasksSelect1.Margin = new System.Windows.Forms.Padding(40, 4, 4, 4);
            this.ucTasksSelect1.Name = "ucTasksSelect1";
            this.ucTasksSelect1.Padding = new System.Windows.Forms.Padding(40, 0, 40, 0);
            this.ucTasksSelect1.Size = new System.Drawing.Size(1228, 593);
            this.ucTasksSelect1.TabIndex = 4;
            this.ucTasksSelect1.Visible = false;
            this.ucTasksSelect1.TaskSelected += new ProfessionalDocAnalyzer.ucTasksSelect.ProcessHandler(this.ucTasksSelect1_TaskSelected);
            // 
            // ucWelcome1
            // 
            this.ucWelcome1.AutoSize = true;
            this.ucWelcome1.BackColor = System.Drawing.Color.White;
            this.ucWelcome1.Location = new System.Drawing.Point(739, 166);
            this.ucWelcome1.Margin = new System.Windows.Forms.Padding(5);
            this.ucWelcome1.Name = "ucWelcome1";
            this.ucWelcome1.Size = new System.Drawing.Size(1392, 622);
            this.ucWelcome1.TabIndex = 2;
            this.ucWelcome1.Visible = false;
            this.ucWelcome1.WorkgroupSelected += new ProfessionalDocAnalyzer.ucWelcome.ProcessHandler(this.ucWelcome1_WorkgroupSelected);
            this.ucWelcome1.StartClicked += new ProfessionalDocAnalyzer.ucWelcome.ProcessHandler(this.ucWelcome1_StartClicked);
            this.ucWelcome1.ResultsClicked += new ProfessionalDocAnalyzer.ucWelcome.ProcessHandler(this.ucWelcome1_ResultsClicked);
            this.ucWelcome1.SettingsClicked += new ProfessionalDocAnalyzer.ucWelcome.ProcessHandler(this.ucWelcome1_SettingsClicked);
            // 
            // ucQCAnalysisResults1
            // 
            this.ucQCAnalysisResults1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucQCAnalysisResults1.Location = new System.Drawing.Point(29, 119);
            this.ucQCAnalysisResults1.Margin = new System.Windows.Forms.Padding(5);
            this.ucQCAnalysisResults1.Name = "ucQCAnalysisResults1";
            this.ucQCAnalysisResults1.Size = new System.Drawing.Size(1206, 616);
            this.ucQCAnalysisResults1.TabIndex = 17;
            this.ucQCAnalysisResults1.Visible = false;
            // 
            // ucTasks1
            // 
            this.ucTasks1.BackColor = System.Drawing.Color.White;
            this.ucTasks1.CurrentStep = -1;
            this.ucTasks1.CurrentTask = "";
            this.ucTasks1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucTasks1.Document = "";
            this.ucTasks1.Location = new System.Drawing.Point(0, 0);
            this.ucTasks1.Margin = new System.Windows.Forms.Padding(5);
            this.ucTasks1.Name = "ucTasks1";
            this.ucTasks1.Project = "";
            this.ucTasks1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ucTasks1.Size = new System.Drawing.Size(1463, 38);
            this.ucTasks1.Steps = null;
            this.ucTasks1.TabIndex = 3;
            this.ucTasks1.Visible = false;
            // 
            // butExport
            // 
            this.butExport.BackColor = System.Drawing.Color.Navy;
            this.butExport.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.butExport.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.butExport.Location = new System.Drawing.Point(974, 9);
            this.butExport.Name = "butExport";
            this.butExport.Size = new System.Drawing.Size(116, 47);
            this.butExport.TabIndex = 9;
            this.butExport.Text = "Export";
            this.butExport.UseVisualStyleBackColor = false;
            this.butExport.Visible = false;
            this.butExport.Click += new System.EventHandler(this.butExport_Click);
            this.butExport.MouseEnter += new System.EventHandler(this.butExport_MouseEnter);
            this.butExport.MouseLeave += new System.EventHandler(this.butExport_MouseLeave);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1463, 650);
            this.Controls.Add(this.ucStart1);
            this.Controls.Add(this.ucDeepAnalyticsResults1);
            this.Controls.Add(this.ucAcroSeekerResults1);
            this.Controls.Add(this.ucDiffSxS1);
            this.Controls.Add(this.ucResults1);
            this.Controls.Add(this.ucResultsMultiDic1);
            this.Controls.Add(this.ucResultsDic1);
            this.Controls.Add(this.ucResultsMultiConcepts1);
            this.Controls.Add(this.ucDictionarySelect1);
            this.Controls.Add(this.ucResultsConcepts1);
            this.Controls.Add(this.ucProjectsDocs1);
            this.Controls.Add(this.ucSettings1);
            this.Controls.Add(this.ucAnalysisResults1);
            this.Controls.Add(this.ucKeywordsSelect1);
            this.Controls.Add(this.ucTasksSelect1);
            this.Controls.Add(this.ucWelcome1);
            this.Controls.Add(this.ucQCAnalysisResults1);
            this.Controls.Add(this.panHeader);
            this.Controls.Add(this.ucTasks1);
            this.Controls.Add(this.panFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.Text = "Professional Document Analyzer - New Edition";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butInformation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.butPerson)).EndInit();
            this.panFooter.ResumeLayout(false);
            this.panFooterRight.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Panel panFooter;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private System.Windows.Forms.PictureBox butPerson;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox butInformation;
        private System.Windows.Forms.Label label1;
        private ucWelcome ucWelcome1;
        private System.Windows.Forms.Panel panFooterRight;
        private ucTasks ucTasks1;
        private System.Windows.Forms.Button butBackToWelcome;
        private System.Windows.Forms.Button butBack;
        private System.Windows.Forms.Button Butnext;
        private ucTasksSelect ucTasksSelect1;
        private ucProjectsDocs ucProjectsDocs1;
        private ucKeywordsSelect ucKeywordsSelect1;
        private ucAnalysisResults ucAnalysisResults1;
        private ucSettings ucSettings1;
        private System.Windows.Forms.Button butDelete;
        private System.Windows.Forms.Button butNew;
        private System.Windows.Forms.Button butEdit;
        private System.Windows.Forms.Button butDownload;
        private System.Windows.Forms.Button butImport;
        private ucResultsConcepts ucResultsConcepts1;
        private ucResultsMultiConcepts ucResultsMultiConcepts1;
        private ucDictionarySelect ucDictionarySelect1;
        private ucResultsDic ucResultsDic1;
        private ucResultsMultiDic ucResultsMultiDic1;
        private ucResults ucResults1;
        private System.Windows.Forms.Button butTest;
        private ucQCAnalysisResults ucQCAnalysisResults1;
        private ucDiffSxS ucDiffSxS1;
        private ucAcroSeekerResults ucAcroSeekerResults1;
        private ucDeepAnalyticsResults ucDeepAnalyticsResults1;
        private ucStart ucStart1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button butExport;
    }
}

