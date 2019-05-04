namespace mySQMPRO
{
    partial class mySQMPRO
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mySQMPRO));
            this.myserialPort = new System.IO.Ports.SerialPort(this.components);
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Exitbtn = new System.Windows.Forms.Button();
            this.SQMTxtBx = new System.Windows.Forms.TextBox();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.DisconnectBtn = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alertPopupWhenRainingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableAlertPopoupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableAlertPopupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forceExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.luxValuesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetLogfilesPathMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rainingPollRateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RainingPollRate5sMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RainingPollRate30sMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RainingPollRate1mMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RainingPollRate5mMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RainingPollRate10mMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setCloudSensorIILogfilenameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getsqmBtn = new System.Windows.Forms.Button();
            this.getirradianceBtn = new System.Windows.Forms.Button();
            this.getfrequencyBtn = new System.Windows.Forms.Button();
            this.getfirmwareBtn = new System.Windows.Forms.Button();
            this.RefreshComPortBtn = new System.Windows.Forms.Button();
            this.irradiancetxtbox = new System.Windows.Forms.TextBox();
            this.frequencytxtbox = new System.Windows.Forms.TextBox();
            this.firmwaretxtbox = new System.Windows.Forms.TextBox();
            this.automatechkbox = new System.Windows.Forms.CheckBox();
            this.automate1m = new System.Windows.Forms.RadioButton();
            this.automate5m = new System.Windows.Forms.RadioButton();
            this.automate10m = new System.Windows.Forms.RadioButton();
            this.automategrpbox = new System.Windows.Forms.GroupBox();
            this.automate30m = new System.Windows.Forms.RadioButton();
            this.automate15m = new System.Windows.Forms.RadioButton();
            this.automate30s = new System.Windows.Forms.RadioButton();
            this.CloudSensorIILogFormat = new System.Windows.Forms.CheckBox();
            this.intervaltimer1 = new System.Windows.Forms.Timer(this.components);
            this.comportspeed = new System.Windows.Forms.ListBox();
            this.comportspeedlabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.statustxtbox = new System.Windows.Forms.TextBox();
            this.ClearStatusMsgTimer = new System.Windows.Forms.Timer(this.components);
            this.getallBtn = new System.Windows.Forms.Button();
            this.getLuxBtn = new System.Windows.Forms.Button();
            this.getRainSensorBtn = new System.Windows.Forms.Button();
            this.getMLX90614Btn = new System.Windows.Forms.Button();
            this.ObjectTempTxtBox = new System.Windows.Forms.TextBox();
            this.AmbientTempTxtBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.RainSensorTxtBox = new System.Windows.Forms.TextBox();
            this.RainVoltageTxtBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.luxTxtBox = new System.Windows.Forms.TextBox();
            this.IRSensorGrpBox = new System.Windows.Forms.GroupBox();
            this.SkyConditionLbl = new System.Windows.Forms.Label();
            this.SkyConditionLabel = new System.Windows.Forms.Label();
            this.getsetpointsBtn = new System.Windows.Forms.Button();
            this.setpoint2Btn = new System.Windows.Forms.Button();
            this.setpoint1Btn = new System.Windows.Forms.Button();
            this.RainSensorGrpBox = new System.Windows.Forms.GroupBox();
            this.GetNELMBtn = new System.Windows.Forms.Button();
            this.NELMtxtbox = new System.Windows.Forms.TextBox();
            this.RainTimer = new System.Windows.Forms.Timer(this.components);
            this.getBME280SensorBtn = new System.Windows.Forms.Button();
            this.bme280temptxtbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.bme280humiditytxtbox = new System.Windows.Forms.TextBox();
            this.bme280pressuretxtbox = new System.Windows.Forms.TextBox();
            this.bme280dewpointtxtbox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.bme280grpBox = new System.Windows.Forms.GroupBox();
            this.latitudetxtbox = new System.Windows.Forms.TextBox();
            this.longitudetxtbox = new System.Windows.Forms.TextBox();
            this.satelittestxtbox = new System.Windows.Forms.TextBox();
            this.timetxtbox = new System.Windows.Forms.TextBox();
            this.datetxtbox = new System.Windows.Forms.TextBox();
            this.getlatitudeBtn = new System.Windows.Forms.Button();
            this.getlongitudeBtn = new System.Windows.Forms.Button();
            this.getsatelittesBtn = new System.Windows.Forms.Button();
            this.gettimeBtn = new System.Windows.Forms.Button();
            this.getdateBtn = new System.Windows.Forms.Button();
            this.altitudetxtbox = new System.Windows.Forms.TextBox();
            this.getaltitudeBtn = new System.Windows.Forms.Button();
            this.gpsGroupBox = new System.Windows.Forms.GroupBox();
            this.ClearALLBtn = new System.Windows.Forms.Button();
            this.getGPSDataBtn = new System.Windows.Forms.Button();
            this.GraphLoggingPicture = new System.Windows.Forms.PictureBox();
            this.gpsFixChkBox = new System.Windows.Forms.CheckBox();
            this.gpspresentChkBox = new System.Windows.Forms.CheckBox();
            this.LogValuesChkBox = new System.Windows.Forms.CheckBox();
            this.LogErrorsChkBox = new System.Windows.Forms.CheckBox();
            this.SetPoint2TxtBox = new System.Windows.Forms.TextBox();
            this.SetPoint1TxtBox = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.automategrpbox.SuspendLayout();
            this.IRSensorGrpBox.SuspendLayout();
            this.RainSensorGrpBox.SuspendLayout();
            this.bme280grpBox.SuspendLayout();
            this.gpsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GraphLoggingPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(789, 58);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.MaxDropDownItems = 15;
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(109, 24);
            this.comboBox1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(832, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Com Port";
            // 
            // Exitbtn
            // 
            this.Exitbtn.Location = new System.Drawing.Point(795, 631);
            this.Exitbtn.Margin = new System.Windows.Forms.Padding(4);
            this.Exitbtn.Name = "Exitbtn";
            this.Exitbtn.Size = new System.Drawing.Size(100, 42);
            this.Exitbtn.TabIndex = 9;
            this.Exitbtn.Text = "Exit";
            this.Exitbtn.UseVisualStyleBackColor = true;
            this.Exitbtn.Click += new System.EventHandler(this.Exitbtn_Click);
            // 
            // SQMTxtBx
            // 
            this.SQMTxtBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SQMTxtBx.Location = new System.Drawing.Point(459, 264);
            this.SQMTxtBx.Margin = new System.Windows.Forms.Padding(4);
            this.SQMTxtBx.Name = "SQMTxtBx";
            this.SQMTxtBx.ReadOnly = true;
            this.SQMTxtBx.Size = new System.Drawing.Size(128, 41);
            this.SQMTxtBx.TabIndex = 11;
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Location = new System.Drawing.Point(799, 171);
            this.ConnectBtn.Margin = new System.Windows.Forms.Padding(4);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(99, 30);
            this.ConnectBtn.TabIndex = 32;
            this.ConnectBtn.Text = "Connect";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // DisconnectBtn
            // 
            this.DisconnectBtn.Location = new System.Drawing.Point(799, 209);
            this.DisconnectBtn.Margin = new System.Windows.Forms.Padding(4);
            this.DisconnectBtn.Name = "DisconnectBtn";
            this.DisconnectBtn.Size = new System.Drawing.Size(99, 30);
            this.DisconnectBtn.TabIndex = 33;
            this.DisconnectBtn.Text = "Disconnect";
            this.DisconnectBtn.UseVisualStyleBackColor = true;
            this.DisconnectBtn.Click += new System.EventHandler(this.DisconnectBtn_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(911, 28);
            this.menuStrip1.TabIndex = 49;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(45, 24);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alertPopupWhenRainingMenuItem,
            this.forceExitMenuItem,
            this.luxValuesMenuItem,
            this.resetLogfilesPathMenuItem,
            this.rainingPollRateMenuItem,
            this.setCloudSensorIILogfilenameMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // alertPopupWhenRainingMenuItem
            // 
            this.alertPopupWhenRainingMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.disableAlertPopoupMenuItem,
            this.enableAlertPopupMenuItem});
            this.alertPopupWhenRainingMenuItem.Name = "alertPopupWhenRainingMenuItem";
            this.alertPopupWhenRainingMenuItem.Size = new System.Drawing.Size(287, 26);
            this.alertPopupWhenRainingMenuItem.Text = "Alert Popup when raining";
            // 
            // disableAlertPopoupMenuItem
            // 
            this.disableAlertPopoupMenuItem.Name = "disableAlertPopoupMenuItem";
            this.disableAlertPopoupMenuItem.Size = new System.Drawing.Size(134, 26);
            this.disableAlertPopoupMenuItem.Text = "Disable";
            this.disableAlertPopoupMenuItem.Click += new System.EventHandler(this.disableAlertPopUpMenuItem_Click);
            // 
            // enableAlertPopupMenuItem
            // 
            this.enableAlertPopupMenuItem.Name = "enableAlertPopupMenuItem";
            this.enableAlertPopupMenuItem.Size = new System.Drawing.Size(134, 26);
            this.enableAlertPopupMenuItem.Text = "Enable";
            this.enableAlertPopupMenuItem.Click += new System.EventHandler(this.enableAlertPopUpMenuItem_Click);
            // 
            // forceExitMenuItem
            // 
            this.forceExitMenuItem.Name = "forceExitMenuItem";
            this.forceExitMenuItem.Size = new System.Drawing.Size(287, 26);
            this.forceExitMenuItem.Text = "Force Exit";
            this.forceExitMenuItem.Click += new System.EventHandler(this.forceExitMenuItem_Click);
            // 
            // luxValuesMenuItem
            // 
            this.luxValuesMenuItem.Name = "luxValuesMenuItem";
            this.luxValuesMenuItem.Size = new System.Drawing.Size(287, 26);
            this.luxValuesMenuItem.Text = "Lux Values";
            this.luxValuesMenuItem.Click += new System.EventHandler(this.luxValuesMenuItem_Click);
            // 
            // resetLogfilesPathMenuItem
            // 
            this.resetLogfilesPathMenuItem.Name = "resetLogfilesPathMenuItem";
            this.resetLogfilesPathMenuItem.Size = new System.Drawing.Size(287, 26);
            this.resetLogfilesPathMenuItem.Text = "Reset Logfiles path";
            this.resetLogfilesPathMenuItem.Click += new System.EventHandler(this.resetLogfilesPathMenuItem_Click);
            // 
            // rainingPollRateMenuItem
            // 
            this.rainingPollRateMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RainingPollRate5sMenuItem,
            this.RainingPollRate30sMenuItem,
            this.RainingPollRate1mMenuItem,
            this.RainingPollRate5mMenuItem,
            this.RainingPollRate10mMenuItem});
            this.rainingPollRateMenuItem.Name = "rainingPollRateMenuItem";
            this.rainingPollRateMenuItem.Size = new System.Drawing.Size(287, 26);
            this.rainingPollRateMenuItem.Text = "Raining Poll Rate";
            // 
            // RainingPollRate5sMenuItem
            // 
            this.RainingPollRate5sMenuItem.Name = "RainingPollRate5sMenuItem";
            this.RainingPollRate5sMenuItem.Size = new System.Drawing.Size(181, 26);
            this.RainingPollRate5sMenuItem.Text = "5 seconds";
            this.RainingPollRate5sMenuItem.Click += new System.EventHandler(this.RainingPollRate5sMenuItem_Click);
            // 
            // RainingPollRate30sMenuItem
            // 
            this.RainingPollRate30sMenuItem.Name = "RainingPollRate30sMenuItem";
            this.RainingPollRate30sMenuItem.Size = new System.Drawing.Size(181, 26);
            this.RainingPollRate30sMenuItem.Text = "30 seconds";
            this.RainingPollRate30sMenuItem.Click += new System.EventHandler(this.RainingPollRate30sMenuItem_Click);
            // 
            // RainingPollRate1mMenuItem
            // 
            this.RainingPollRate1mMenuItem.Name = "RainingPollRate1mMenuItem";
            this.RainingPollRate1mMenuItem.Size = new System.Drawing.Size(181, 26);
            this.RainingPollRate1mMenuItem.Text = "1 minute";
            this.RainingPollRate1mMenuItem.Click += new System.EventHandler(this.RainingPollRate1mMenuItem_Click);
            // 
            // RainingPollRate5mMenuItem
            // 
            this.RainingPollRate5mMenuItem.Name = "RainingPollRate5mMenuItem";
            this.RainingPollRate5mMenuItem.Size = new System.Drawing.Size(181, 26);
            this.RainingPollRate5mMenuItem.Text = "5 minutes";
            this.RainingPollRate5mMenuItem.Click += new System.EventHandler(this.RainingPollRate5mMenuItem_Click);
            // 
            // RainingPollRate10mMenuItem
            // 
            this.RainingPollRate10mMenuItem.Name = "RainingPollRate10mMenuItem";
            this.RainingPollRate10mMenuItem.Size = new System.Drawing.Size(181, 26);
            this.RainingPollRate10mMenuItem.Text = "10 minutes";
            this.RainingPollRate10mMenuItem.Click += new System.EventHandler(this.RainingPollRate10mMenuItem_Click);
            // 
            // setCloudSensorIILogfilenameMenuItem
            // 
            this.setCloudSensorIILogfilenameMenuItem.Name = "setCloudSensorIILogfilenameMenuItem";
            this.setCloudSensorIILogfilenameMenuItem.Size = new System.Drawing.Size(287, 26);
            this.setCloudSensorIILogfilenameMenuItem.Text = "Set CloudSensorII Logfilename";
            this.setCloudSensorIILogfilenameMenuItem.Click += new System.EventHandler(this.setCloudSensorIILogfilenameMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // getsqmBtn
            // 
            this.getsqmBtn.Location = new System.Drawing.Point(605, 264);
            this.getsqmBtn.Margin = new System.Windows.Forms.Padding(4);
            this.getsqmBtn.Name = "getsqmBtn";
            this.getsqmBtn.Size = new System.Drawing.Size(133, 43);
            this.getsqmBtn.TabIndex = 50;
            this.getsqmBtn.Text = "Get SQM";
            this.getsqmBtn.UseVisualStyleBackColor = true;
            this.getsqmBtn.Click += new System.EventHandler(this.getsqmBtn_Click);
            // 
            // getirradianceBtn
            // 
            this.getirradianceBtn.Location = new System.Drawing.Point(311, 80);
            this.getirradianceBtn.Margin = new System.Windows.Forms.Padding(4);
            this.getirradianceBtn.Name = "getirradianceBtn";
            this.getirradianceBtn.Size = new System.Drawing.Size(133, 31);
            this.getirradianceBtn.TabIndex = 57;
            this.getirradianceBtn.Text = "Irradiance";
            this.getirradianceBtn.UseVisualStyleBackColor = true;
            this.getirradianceBtn.Click += new System.EventHandler(this.getirradianceBtn_Click);
            // 
            // getfrequencyBtn
            // 
            this.getfrequencyBtn.Location = new System.Drawing.Point(458, 80);
            this.getfrequencyBtn.Margin = new System.Windows.Forms.Padding(4);
            this.getfrequencyBtn.Name = "getfrequencyBtn";
            this.getfrequencyBtn.Size = new System.Drawing.Size(133, 31);
            this.getfrequencyBtn.TabIndex = 58;
            this.getfrequencyBtn.Text = "Frequency";
            this.getfrequencyBtn.UseVisualStyleBackColor = true;
            this.getfrequencyBtn.Click += new System.EventHandler(this.getfrequencyBtn_Click);
            // 
            // getfirmwareBtn
            // 
            this.getfirmwareBtn.Location = new System.Drawing.Point(13, 80);
            this.getfirmwareBtn.Margin = new System.Windows.Forms.Padding(4);
            this.getfirmwareBtn.Name = "getfirmwareBtn";
            this.getfirmwareBtn.Size = new System.Drawing.Size(133, 31);
            this.getfirmwareBtn.TabIndex = 59;
            this.getfirmwareBtn.Text = "Firmware #";
            this.getfirmwareBtn.UseVisualStyleBackColor = true;
            this.getfirmwareBtn.Click += new System.EventHandler(this.getfirmwareBtn_Click);
            // 
            // RefreshComPortBtn
            // 
            this.RefreshComPortBtn.Location = new System.Drawing.Point(799, 247);
            this.RefreshComPortBtn.Margin = new System.Windows.Forms.Padding(4);
            this.RefreshComPortBtn.Name = "RefreshComPortBtn";
            this.RefreshComPortBtn.Size = new System.Drawing.Size(99, 30);
            this.RefreshComPortBtn.TabIndex = 60;
            this.RefreshComPortBtn.Text = "Refresh";
            this.RefreshComPortBtn.UseVisualStyleBackColor = true;
            this.RefreshComPortBtn.Click += new System.EventHandler(this.RefreshComPortBtn_Click);
            // 
            // irradiancetxtbox
            // 
            this.irradiancetxtbox.Location = new System.Drawing.Point(311, 48);
            this.irradiancetxtbox.Margin = new System.Windows.Forms.Padding(4);
            this.irradiancetxtbox.Name = "irradiancetxtbox";
            this.irradiancetxtbox.ReadOnly = true;
            this.irradiancetxtbox.Size = new System.Drawing.Size(132, 22);
            this.irradiancetxtbox.TabIndex = 67;
            // 
            // frequencytxtbox
            // 
            this.frequencytxtbox.Location = new System.Drawing.Point(458, 48);
            this.frequencytxtbox.Margin = new System.Windows.Forms.Padding(4);
            this.frequencytxtbox.Name = "frequencytxtbox";
            this.frequencytxtbox.ReadOnly = true;
            this.frequencytxtbox.Size = new System.Drawing.Size(132, 22);
            this.frequencytxtbox.TabIndex = 68;
            // 
            // firmwaretxtbox
            // 
            this.firmwaretxtbox.Location = new System.Drawing.Point(13, 48);
            this.firmwaretxtbox.Margin = new System.Windows.Forms.Padding(4);
            this.firmwaretxtbox.Name = "firmwaretxtbox";
            this.firmwaretxtbox.ReadOnly = true;
            this.firmwaretxtbox.Size = new System.Drawing.Size(132, 22);
            this.firmwaretxtbox.TabIndex = 69;
            // 
            // automatechkbox
            // 
            this.automatechkbox.AutoSize = true;
            this.automatechkbox.Location = new System.Drawing.Point(351, 587);
            this.automatechkbox.Margin = new System.Windows.Forms.Padding(4);
            this.automatechkbox.Name = "automatechkbox";
            this.automatechkbox.Size = new System.Drawing.Size(145, 21);
            this.automatechkbox.TabIndex = 70;
            this.automatechkbox.Text = "Automate Logging";
            this.automatechkbox.UseVisualStyleBackColor = true;
            this.automatechkbox.CheckedChanged += new System.EventHandler(this.automatechkbox_CheckedChanged);
            // 
            // automate1m
            // 
            this.automate1m.AutoSize = true;
            this.automate1m.Location = new System.Drawing.Point(8, 52);
            this.automate1m.Margin = new System.Windows.Forms.Padding(4);
            this.automate1m.Name = "automate1m";
            this.automate1m.Size = new System.Drawing.Size(48, 21);
            this.automate1m.TabIndex = 71;
            this.automate1m.TabStop = true;
            this.automate1m.Text = "1m";
            this.automate1m.UseVisualStyleBackColor = true;
            this.automate1m.CheckedChanged += new System.EventHandler(this.automate1m_CheckedChanged);
            // 
            // automate5m
            // 
            this.automate5m.AutoSize = true;
            this.automate5m.Location = new System.Drawing.Point(8, 80);
            this.automate5m.Margin = new System.Windows.Forms.Padding(4);
            this.automate5m.Name = "automate5m";
            this.automate5m.Size = new System.Drawing.Size(48, 21);
            this.automate5m.TabIndex = 72;
            this.automate5m.TabStop = true;
            this.automate5m.Text = "5m";
            this.automate5m.UseVisualStyleBackColor = true;
            this.automate5m.CheckedChanged += new System.EventHandler(this.automate5m_CheckedChanged);
            // 
            // automate10m
            // 
            this.automate10m.AutoSize = true;
            this.automate10m.Location = new System.Drawing.Point(74, 23);
            this.automate10m.Margin = new System.Windows.Forms.Padding(4);
            this.automate10m.Name = "automate10m";
            this.automate10m.Size = new System.Drawing.Size(56, 21);
            this.automate10m.TabIndex = 73;
            this.automate10m.TabStop = true;
            this.automate10m.Text = "10m";
            this.automate10m.UseVisualStyleBackColor = true;
            this.automate10m.CheckedChanged += new System.EventHandler(this.automate10m_CheckedChanged);
            // 
            // automategrpbox
            // 
            this.automategrpbox.Controls.Add(this.automate30m);
            this.automategrpbox.Controls.Add(this.automate15m);
            this.automategrpbox.Controls.Add(this.automate30s);
            this.automategrpbox.Controls.Add(this.automate10m);
            this.automategrpbox.Controls.Add(this.automate5m);
            this.automategrpbox.Controls.Add(this.automate1m);
            this.automategrpbox.Location = new System.Drawing.Point(763, 300);
            this.automategrpbox.Margin = new System.Windows.Forms.Padding(4);
            this.automategrpbox.Name = "automategrpbox";
            this.automategrpbox.Padding = new System.Windows.Forms.Padding(4);
            this.automategrpbox.Size = new System.Drawing.Size(136, 116);
            this.automategrpbox.TabIndex = 74;
            this.automategrpbox.TabStop = false;
            this.automategrpbox.Text = "Log Interval";
            // 
            // automate30m
            // 
            this.automate30m.AutoSize = true;
            this.automate30m.Location = new System.Drawing.Point(74, 80);
            this.automate30m.Margin = new System.Windows.Forms.Padding(4);
            this.automate30m.Name = "automate30m";
            this.automate30m.Size = new System.Drawing.Size(56, 21);
            this.automate30m.TabIndex = 76;
            this.automate30m.TabStop = true;
            this.automate30m.Text = "30m";
            this.automate30m.UseVisualStyleBackColor = true;
            this.automate30m.CheckedChanged += new System.EventHandler(this.automate30m_CheckedChanged);
            // 
            // automate15m
            // 
            this.automate15m.AutoSize = true;
            this.automate15m.Location = new System.Drawing.Point(74, 52);
            this.automate15m.Margin = new System.Windows.Forms.Padding(4);
            this.automate15m.Name = "automate15m";
            this.automate15m.Size = new System.Drawing.Size(56, 21);
            this.automate15m.TabIndex = 75;
            this.automate15m.TabStop = true;
            this.automate15m.Text = "15m";
            this.automate15m.UseVisualStyleBackColor = true;
            this.automate15m.CheckedChanged += new System.EventHandler(this.automate15m_CheckedChanged);
            // 
            // automate30s
            // 
            this.automate30s.AutoSize = true;
            this.automate30s.Location = new System.Drawing.Point(8, 23);
            this.automate30s.Margin = new System.Windows.Forms.Padding(4);
            this.automate30s.Name = "automate30s";
            this.automate30s.Size = new System.Drawing.Size(52, 21);
            this.automate30s.TabIndex = 74;
            this.automate30s.TabStop = true;
            this.automate30s.Text = "30s";
            this.automate30s.UseVisualStyleBackColor = true;
            this.automate30s.CheckedChanged += new System.EventHandler(this.automate30s_CheckedChanged);
            // 
            // CloudSensorIILogFormat
            // 
            this.CloudSensorIILogFormat.AutoSize = true;
            this.CloudSensorIILogFormat.Checked = global::mySQMPRO.Properties.Settings.Default.CloudSensorIILogFileFormat;
            this.CloudSensorIILogFormat.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::mySQMPRO.Properties.Settings.Default, "CloudSensorIILogFileFormat", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.CloudSensorIILogFormat.Location = new System.Drawing.Point(230, 587);
            this.CloudSensorIILogFormat.Name = "CloudSensorIILogFormat";
            this.CloudSensorIILogFormat.Size = new System.Drawing.Size(108, 21);
            this.CloudSensorIILogFormat.TabIndex = 77;
            this.CloudSensorIILogFormat.Text = "Log CSII File";
            this.CloudSensorIILogFormat.UseVisualStyleBackColor = true;
            // 
            // intervaltimer1
            // 
            this.intervaltimer1.Tick += new System.EventHandler(this.intervaltimer1_Tick);
            // 
            // comportspeed
            // 
            this.comportspeed.FormattingEnabled = true;
            this.comportspeed.ItemHeight = 16;
            this.comportspeed.Items.AddRange(new object[] {
            "4800",
            "9600",
            "14400",
            "19200",
            "28800",
            "38400",
            "57600",
            "115200"});
            this.comportspeed.Location = new System.Drawing.Point(789, 127);
            this.comportspeed.Margin = new System.Windows.Forms.Padding(4);
            this.comportspeed.Name = "comportspeed";
            this.comportspeed.Size = new System.Drawing.Size(108, 36);
            this.comportspeed.TabIndex = 76;
            this.comportspeed.SelectedIndexChanged += new System.EventHandler(this.comportspeed_SelectedIndexChanged);
            // 
            // comportspeedlabel
            // 
            this.comportspeedlabel.AutoSize = true;
            this.comportspeedlabel.Location = new System.Drawing.Point(787, 106);
            this.comportspeedlabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.comportspeedlabel.Name = "comportspeedlabel";
            this.comportspeedlabel.Size = new System.Drawing.Size(111, 17);
            this.comportspeedlabel.TabIndex = 75;
            this.comportspeedlabel.Text = "Com Port Speed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 631);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 17);
            this.label2.TabIndex = 78;
            this.label2.Text = "Status Messages";
            // 
            // statustxtbox
            // 
            this.statustxtbox.Location = new System.Drawing.Point(8, 651);
            this.statustxtbox.Name = "statustxtbox";
            this.statustxtbox.ReadOnly = true;
            this.statustxtbox.Size = new System.Drawing.Size(556, 22);
            this.statustxtbox.TabIndex = 77;
            this.statustxtbox.TextChanged += new System.EventHandler(this.statustxtbox_TextChanged);
            // 
            // ClearStatusMsgTimer
            // 
            this.ClearStatusMsgTimer.Interval = 3000;
            this.ClearStatusMsgTimer.Tick += new System.EventHandler(this.ClearStatusMsgTimer_Tick);
            // 
            // getallBtn
            // 
            this.getallBtn.Location = new System.Drawing.Point(605, 129);
            this.getallBtn.Margin = new System.Windows.Forms.Padding(4);
            this.getallBtn.Name = "getallBtn";
            this.getallBtn.Size = new System.Drawing.Size(133, 43);
            this.getallBtn.TabIndex = 83;
            this.getallBtn.Text = "Get ALL";
            this.getallBtn.UseVisualStyleBackColor = true;
            this.getallBtn.Click += new System.EventHandler(this.getallBtn_Click);
            // 
            // getLuxBtn
            // 
            this.getLuxBtn.Location = new System.Drawing.Point(163, 80);
            this.getLuxBtn.Margin = new System.Windows.Forms.Padding(4);
            this.getLuxBtn.Name = "getLuxBtn";
            this.getLuxBtn.Size = new System.Drawing.Size(133, 31);
            this.getLuxBtn.TabIndex = 84;
            this.getLuxBtn.Text = "Lux";
            this.getLuxBtn.UseVisualStyleBackColor = true;
            this.getLuxBtn.Click += new System.EventHandler(this.getLuxBtn_Click);
            // 
            // getRainSensorBtn
            // 
            this.getRainSensorBtn.Location = new System.Drawing.Point(249, 38);
            this.getRainSensorBtn.Margin = new System.Windows.Forms.Padding(4);
            this.getRainSensorBtn.Name = "getRainSensorBtn";
            this.getRainSensorBtn.Size = new System.Drawing.Size(133, 43);
            this.getRainSensorBtn.TabIndex = 85;
            this.getRainSensorBtn.Text = "Rain Sensor";
            this.getRainSensorBtn.UseVisualStyleBackColor = true;
            this.getRainSensorBtn.Click += new System.EventHandler(this.getRainSensorBtn_Click);
            // 
            // getMLX90614Btn
            // 
            this.getMLX90614Btn.Location = new System.Drawing.Point(249, 29);
            this.getMLX90614Btn.Margin = new System.Windows.Forms.Padding(4);
            this.getMLX90614Btn.Name = "getMLX90614Btn";
            this.getMLX90614Btn.Size = new System.Drawing.Size(133, 50);
            this.getMLX90614Btn.TabIndex = 86;
            this.getMLX90614Btn.Text = "MLX90614 Sensor";
            this.getMLX90614Btn.UseVisualStyleBackColor = true;
            this.getMLX90614Btn.Click += new System.EventHandler(this.getMLX90614Btn_Click);
            // 
            // ObjectTempTxtBox
            // 
            this.ObjectTempTxtBox.Location = new System.Drawing.Point(8, 26);
            this.ObjectTempTxtBox.Margin = new System.Windows.Forms.Padding(4);
            this.ObjectTempTxtBox.Name = "ObjectTempTxtBox";
            this.ObjectTempTxtBox.ReadOnly = true;
            this.ObjectTempTxtBox.Size = new System.Drawing.Size(132, 22);
            this.ObjectTempTxtBox.TabIndex = 87;
            // 
            // AmbientTempTxtBox
            // 
            this.AmbientTempTxtBox.Location = new System.Drawing.Point(8, 56);
            this.AmbientTempTxtBox.Margin = new System.Windows.Forms.Padding(4);
            this.AmbientTempTxtBox.Name = "AmbientTempTxtBox";
            this.AmbientTempTxtBox.ReadOnly = true;
            this.AmbientTempTxtBox.Size = new System.Drawing.Size(132, 22);
            this.AmbientTempTxtBox.TabIndex = 88;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(147, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 17);
            this.label3.TabIndex = 89;
            this.label3.Text = "Object Temp";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(147, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 17);
            this.label4.TabIndex = 90;
            this.label4.Text = "Ambient Temp";
            // 
            // RainSensorTxtBox
            // 
            this.RainSensorTxtBox.Location = new System.Drawing.Point(17, 29);
            this.RainSensorTxtBox.Margin = new System.Windows.Forms.Padding(4);
            this.RainSensorTxtBox.Name = "RainSensorTxtBox";
            this.RainSensorTxtBox.ReadOnly = true;
            this.RainSensorTxtBox.Size = new System.Drawing.Size(132, 22);
            this.RainSensorTxtBox.TabIndex = 91;
            // 
            // RainVoltageTxtBox
            // 
            this.RainVoltageTxtBox.Location = new System.Drawing.Point(17, 59);
            this.RainVoltageTxtBox.Margin = new System.Windows.Forms.Padding(4);
            this.RainVoltageTxtBox.Name = "RainVoltageTxtBox";
            this.RainVoltageTxtBox.ReadOnly = true;
            this.RainVoltageTxtBox.Size = new System.Drawing.Size(132, 22);
            this.RainVoltageTxtBox.TabIndex = 92;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(156, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 17);
            this.label6.TabIndex = 93;
            this.label6.Text = "Weather";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(161, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 17);
            this.label7.TabIndex = 94;
            this.label7.Text = "Vout";
            // 
            // luxTxtBox
            // 
            this.luxTxtBox.Location = new System.Drawing.Point(163, 48);
            this.luxTxtBox.Margin = new System.Windows.Forms.Padding(4);
            this.luxTxtBox.Name = "luxTxtBox";
            this.luxTxtBox.ReadOnly = true;
            this.luxTxtBox.Size = new System.Drawing.Size(132, 22);
            this.luxTxtBox.TabIndex = 95;
            // 
            // IRSensorGrpBox
            // 
            this.IRSensorGrpBox.Controls.Add(this.SkyConditionLbl);
            this.IRSensorGrpBox.Controls.Add(this.SkyConditionLabel);
            this.IRSensorGrpBox.Controls.Add(this.label4);
            this.IRSensorGrpBox.Controls.Add(this.label3);
            this.IRSensorGrpBox.Controls.Add(this.AmbientTempTxtBox);
            this.IRSensorGrpBox.Controls.Add(this.ObjectTempTxtBox);
            this.IRSensorGrpBox.Controls.Add(this.getMLX90614Btn);
            this.IRSensorGrpBox.Location = new System.Drawing.Point(13, 119);
            this.IRSensorGrpBox.Name = "IRSensorGrpBox";
            this.IRSensorGrpBox.Size = new System.Drawing.Size(391, 129);
            this.IRSensorGrpBox.TabIndex = 100;
            this.IRSensorGrpBox.TabStop = false;
            this.IRSensorGrpBox.Text = "IR Sensor";
            // 
            // SkyConditionLbl
            // 
            this.SkyConditionLbl.AutoSize = true;
            this.SkyConditionLbl.Location = new System.Drawing.Point(7, 93);
            this.SkyConditionLbl.Name = "SkyConditionLbl";
            this.SkyConditionLbl.Size = new System.Drawing.Size(94, 17);
            this.SkyConditionLbl.TabIndex = 101;
            this.SkyConditionLbl.Text = "Sky Condition";
            this.SkyConditionLbl.Click += new System.EventHandler(this.SkyConditionLbl_Click);
            // 
            // SkyConditionLabel
            // 
            this.SkyConditionLabel.AutoSize = true;
            this.SkyConditionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SkyConditionLabel.Location = new System.Drawing.Point(107, 86);
            this.SkyConditionLabel.Name = "SkyConditionLabel";
            this.SkyConditionLabel.Size = new System.Drawing.Size(114, 32);
            this.SkyConditionLabel.TabIndex = 100;
            this.SkyConditionLabel.Text = "CLEAR";
            // 
            // getsetpointsBtn
            // 
            this.getsetpointsBtn.Location = new System.Drawing.Point(483, 198);
            this.getsetpointsBtn.Name = "getsetpointsBtn";
            this.getsetpointsBtn.Size = new System.Drawing.Size(110, 30);
            this.getsetpointsBtn.TabIndex = 104;
            this.getsetpointsBtn.Text = "Get setpoints";
            this.getsetpointsBtn.UseVisualStyleBackColor = true;
            this.getsetpointsBtn.Click += new System.EventHandler(this.getsetpointsBtn_Click);
            // 
            // setpoint2Btn
            // 
            this.setpoint2Btn.Location = new System.Drawing.Point(483, 162);
            this.setpoint2Btn.Name = "setpoint2Btn";
            this.setpoint2Btn.Size = new System.Drawing.Size(110, 30);
            this.setpoint2Btn.TabIndex = 103;
            this.setpoint2Btn.Text = "Set setpoint2";
            this.setpoint2Btn.UseVisualStyleBackColor = true;
            this.setpoint2Btn.Click += new System.EventHandler(this.setpoint2Btn_Click);
            // 
            // setpoint1Btn
            // 
            this.setpoint1Btn.Location = new System.Drawing.Point(483, 128);
            this.setpoint1Btn.Name = "setpoint1Btn";
            this.setpoint1Btn.Size = new System.Drawing.Size(110, 30);
            this.setpoint1Btn.TabIndex = 102;
            this.setpoint1Btn.Text = "Set setpoint1";
            this.setpoint1Btn.UseVisualStyleBackColor = true;
            this.setpoint1Btn.Click += new System.EventHandler(this.setpoint1Btn_Click);
            // 
            // RainSensorGrpBox
            // 
            this.RainSensorGrpBox.Controls.Add(this.label7);
            this.RainSensorGrpBox.Controls.Add(this.label6);
            this.RainSensorGrpBox.Controls.Add(this.RainVoltageTxtBox);
            this.RainSensorGrpBox.Controls.Add(this.RainSensorTxtBox);
            this.RainSensorGrpBox.Controls.Add(this.getRainSensorBtn);
            this.RainSensorGrpBox.Location = new System.Drawing.Point(13, 254);
            this.RainSensorGrpBox.Name = "RainSensorGrpBox";
            this.RainSensorGrpBox.Size = new System.Drawing.Size(391, 100);
            this.RainSensorGrpBox.TabIndex = 101;
            this.RainSensorGrpBox.TabStop = false;
            this.RainSensorGrpBox.Text = "Rain Sensor";
            // 
            // GetNELMBtn
            // 
            this.GetNELMBtn.Location = new System.Drawing.Point(605, 315);
            this.GetNELMBtn.Margin = new System.Windows.Forms.Padding(4);
            this.GetNELMBtn.Name = "GetNELMBtn";
            this.GetNELMBtn.Size = new System.Drawing.Size(133, 43);
            this.GetNELMBtn.TabIndex = 102;
            this.GetNELMBtn.Text = "Get NELM";
            this.GetNELMBtn.UseVisualStyleBackColor = true;
            this.GetNELMBtn.Click += new System.EventHandler(this.GetNELMBtn_Click);
            // 
            // NELMtxtbox
            // 
            this.NELMtxtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NELMtxtbox.Location = new System.Drawing.Point(459, 317);
            this.NELMtxtbox.Margin = new System.Windows.Forms.Padding(4);
            this.NELMtxtbox.Name = "NELMtxtbox";
            this.NELMtxtbox.ReadOnly = true;
            this.NELMtxtbox.Size = new System.Drawing.Size(128, 41);
            this.NELMtxtbox.TabIndex = 103;
            // 
            // RainTimer
            // 
            this.RainTimer.Interval = 5000;
            this.RainTimer.Tick += new System.EventHandler(this.RainTimer_Tick);
            // 
            // getBME280SensorBtn
            // 
            this.getBME280SensorBtn.Location = new System.Drawing.Point(536, 27);
            this.getBME280SensorBtn.Margin = new System.Windows.Forms.Padding(4);
            this.getBME280SensorBtn.Name = "getBME280SensorBtn";
            this.getBME280SensorBtn.Size = new System.Drawing.Size(133, 43);
            this.getBME280SensorBtn.TabIndex = 95;
            this.getBME280SensorBtn.Text = "Barometric Sensor";
            this.getBME280SensorBtn.UseVisualStyleBackColor = true;
            this.getBME280SensorBtn.Click += new System.EventHandler(this.getBME280SensorBtn_Click);
            // 
            // bme280temptxtbox
            // 
            this.bme280temptxtbox.Location = new System.Drawing.Point(10, 24);
            this.bme280temptxtbox.Margin = new System.Windows.Forms.Padding(4);
            this.bme280temptxtbox.Name = "bme280temptxtbox";
            this.bme280temptxtbox.ReadOnly = true;
            this.bme280temptxtbox.Size = new System.Drawing.Size(132, 22);
            this.bme280temptxtbox.TabIndex = 95;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(149, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 17);
            this.label5.TabIndex = 95;
            this.label5.Text = "Temperature";
            // 
            // bme280humiditytxtbox
            // 
            this.bme280humiditytxtbox.Location = new System.Drawing.Point(10, 57);
            this.bme280humiditytxtbox.Margin = new System.Windows.Forms.Padding(4);
            this.bme280humiditytxtbox.Name = "bme280humiditytxtbox";
            this.bme280humiditytxtbox.ReadOnly = true;
            this.bme280humiditytxtbox.Size = new System.Drawing.Size(132, 22);
            this.bme280humiditytxtbox.TabIndex = 108;
            // 
            // bme280pressuretxtbox
            // 
            this.bme280pressuretxtbox.Location = new System.Drawing.Point(273, 22);
            this.bme280pressuretxtbox.Margin = new System.Windows.Forms.Padding(4);
            this.bme280pressuretxtbox.Name = "bme280pressuretxtbox";
            this.bme280pressuretxtbox.ReadOnly = true;
            this.bme280pressuretxtbox.Size = new System.Drawing.Size(132, 22);
            this.bme280pressuretxtbox.TabIndex = 109;
            // 
            // bme280dewpointtxtbox
            // 
            this.bme280dewpointtxtbox.Location = new System.Drawing.Point(273, 60);
            this.bme280dewpointtxtbox.Margin = new System.Windows.Forms.Padding(4);
            this.bme280dewpointtxtbox.Name = "bme280dewpointtxtbox";
            this.bme280dewpointtxtbox.ReadOnly = true;
            this.bme280dewpointtxtbox.Size = new System.Drawing.Size(132, 22);
            this.bme280dewpointtxtbox.TabIndex = 110;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(149, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 17);
            this.label8.TabIndex = 111;
            this.label8.Text = "Humidity";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(412, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 17);
            this.label9.TabIndex = 112;
            this.label9.Text = "Pressure hPa";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(412, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 17);
            this.label10.TabIndex = 113;
            this.label10.Text = "Dew Point";
            // 
            // bme280grpBox
            // 
            this.bme280grpBox.Controls.Add(this.label10);
            this.bme280grpBox.Controls.Add(this.label9);
            this.bme280grpBox.Controls.Add(this.label8);
            this.bme280grpBox.Controls.Add(this.bme280dewpointtxtbox);
            this.bme280grpBox.Controls.Add(this.bme280pressuretxtbox);
            this.bme280grpBox.Controls.Add(this.bme280humiditytxtbox);
            this.bme280grpBox.Controls.Add(this.label5);
            this.bme280grpBox.Controls.Add(this.bme280temptxtbox);
            this.bme280grpBox.Controls.Add(this.getBME280SensorBtn);
            this.bme280grpBox.Location = new System.Drawing.Point(13, 363);
            this.bme280grpBox.Name = "bme280grpBox";
            this.bme280grpBox.Size = new System.Drawing.Size(724, 102);
            this.bme280grpBox.TabIndex = 114;
            this.bme280grpBox.TabStop = false;
            this.bme280grpBox.Text = "Barometric Sensor";
            // 
            // latitudetxtbox
            // 
            this.latitudetxtbox.Location = new System.Drawing.Point(585, 16);
            this.latitudetxtbox.Margin = new System.Windows.Forms.Padding(4);
            this.latitudetxtbox.Name = "latitudetxtbox";
            this.latitudetxtbox.ReadOnly = true;
            this.latitudetxtbox.Size = new System.Drawing.Size(132, 22);
            this.latitudetxtbox.TabIndex = 124;
            // 
            // longitudetxtbox
            // 
            this.longitudetxtbox.Location = new System.Drawing.Point(441, 16);
            this.longitudetxtbox.Margin = new System.Windows.Forms.Padding(4);
            this.longitudetxtbox.Name = "longitudetxtbox";
            this.longitudetxtbox.ReadOnly = true;
            this.longitudetxtbox.Size = new System.Drawing.Size(132, 22);
            this.longitudetxtbox.TabIndex = 123;
            // 
            // satelittestxtbox
            // 
            this.satelittestxtbox.Location = new System.Drawing.Point(301, 16);
            this.satelittestxtbox.Margin = new System.Windows.Forms.Padding(4);
            this.satelittestxtbox.Name = "satelittestxtbox";
            this.satelittestxtbox.ReadOnly = true;
            this.satelittestxtbox.Size = new System.Drawing.Size(132, 22);
            this.satelittestxtbox.TabIndex = 122;
            // 
            // timetxtbox
            // 
            this.timetxtbox.Location = new System.Drawing.Point(157, 16);
            this.timetxtbox.Margin = new System.Windows.Forms.Padding(4);
            this.timetxtbox.Name = "timetxtbox";
            this.timetxtbox.ReadOnly = true;
            this.timetxtbox.Size = new System.Drawing.Size(132, 22);
            this.timetxtbox.TabIndex = 121;
            // 
            // datetxtbox
            // 
            this.datetxtbox.Location = new System.Drawing.Point(14, 16);
            this.datetxtbox.Margin = new System.Windows.Forms.Padding(4);
            this.datetxtbox.Name = "datetxtbox";
            this.datetxtbox.ReadOnly = true;
            this.datetxtbox.Size = new System.Drawing.Size(132, 22);
            this.datetxtbox.TabIndex = 120;
            // 
            // getlatitudeBtn
            // 
            this.getlatitudeBtn.Location = new System.Drawing.Point(585, 47);
            this.getlatitudeBtn.Margin = new System.Windows.Forms.Padding(4);
            this.getlatitudeBtn.Name = "getlatitudeBtn";
            this.getlatitudeBtn.Size = new System.Drawing.Size(133, 31);
            this.getlatitudeBtn.TabIndex = 119;
            this.getlatitudeBtn.Text = "Latitude";
            this.getlatitudeBtn.UseVisualStyleBackColor = true;
            this.getlatitudeBtn.Click += new System.EventHandler(this.getlatitudeBtn_Click);
            // 
            // getlongitudeBtn
            // 
            this.getlongitudeBtn.Location = new System.Drawing.Point(441, 47);
            this.getlongitudeBtn.Margin = new System.Windows.Forms.Padding(4);
            this.getlongitudeBtn.Name = "getlongitudeBtn";
            this.getlongitudeBtn.Size = new System.Drawing.Size(133, 31);
            this.getlongitudeBtn.TabIndex = 118;
            this.getlongitudeBtn.Text = "Longitude";
            this.getlongitudeBtn.UseVisualStyleBackColor = true;
            this.getlongitudeBtn.Click += new System.EventHandler(this.getlongitudeBtn_Click);
            // 
            // getsatelittesBtn
            // 
            this.getsatelittesBtn.Location = new System.Drawing.Point(301, 47);
            this.getsatelittesBtn.Margin = new System.Windows.Forms.Padding(4);
            this.getsatelittesBtn.Name = "getsatelittesBtn";
            this.getsatelittesBtn.Size = new System.Drawing.Size(133, 31);
            this.getsatelittesBtn.TabIndex = 117;
            this.getsatelittesBtn.Text = "Satellites";
            this.getsatelittesBtn.UseVisualStyleBackColor = true;
            this.getsatelittesBtn.Click += new System.EventHandler(this.getsatelittesBtn_Click);
            // 
            // gettimeBtn
            // 
            this.gettimeBtn.Location = new System.Drawing.Point(157, 47);
            this.gettimeBtn.Margin = new System.Windows.Forms.Padding(4);
            this.gettimeBtn.Name = "gettimeBtn";
            this.gettimeBtn.Size = new System.Drawing.Size(133, 31);
            this.gettimeBtn.TabIndex = 116;
            this.gettimeBtn.Text = "Time";
            this.gettimeBtn.UseVisualStyleBackColor = true;
            this.gettimeBtn.Click += new System.EventHandler(this.gettimeBtn_Click);
            // 
            // getdateBtn
            // 
            this.getdateBtn.Location = new System.Drawing.Point(14, 48);
            this.getdateBtn.Margin = new System.Windows.Forms.Padding(4);
            this.getdateBtn.Name = "getdateBtn";
            this.getdateBtn.Size = new System.Drawing.Size(133, 31);
            this.getdateBtn.TabIndex = 115;
            this.getdateBtn.Text = "Date";
            this.getdateBtn.UseVisualStyleBackColor = true;
            this.getdateBtn.Click += new System.EventHandler(this.getdateBtn_Click);
            // 
            // altitudetxtbox
            // 
            this.altitudetxtbox.Location = new System.Drawing.Point(729, 16);
            this.altitudetxtbox.Margin = new System.Windows.Forms.Padding(4);
            this.altitudetxtbox.Name = "altitudetxtbox";
            this.altitudetxtbox.ReadOnly = true;
            this.altitudetxtbox.Size = new System.Drawing.Size(132, 22);
            this.altitudetxtbox.TabIndex = 126;
            // 
            // getaltitudeBtn
            // 
            this.getaltitudeBtn.Location = new System.Drawing.Point(729, 48);
            this.getaltitudeBtn.Margin = new System.Windows.Forms.Padding(4);
            this.getaltitudeBtn.Name = "getaltitudeBtn";
            this.getaltitudeBtn.Size = new System.Drawing.Size(133, 31);
            this.getaltitudeBtn.TabIndex = 125;
            this.getaltitudeBtn.Text = "Altitude";
            this.getaltitudeBtn.UseVisualStyleBackColor = true;
            this.getaltitudeBtn.Click += new System.EventHandler(this.getaltitudeBtn_Click);
            // 
            // gpsGroupBox
            // 
            this.gpsGroupBox.Controls.Add(this.altitudetxtbox);
            this.gpsGroupBox.Controls.Add(this.getaltitudeBtn);
            this.gpsGroupBox.Controls.Add(this.latitudetxtbox);
            this.gpsGroupBox.Controls.Add(this.longitudetxtbox);
            this.gpsGroupBox.Controls.Add(this.satelittestxtbox);
            this.gpsGroupBox.Controls.Add(this.timetxtbox);
            this.gpsGroupBox.Controls.Add(this.datetxtbox);
            this.gpsGroupBox.Controls.Add(this.getlatitudeBtn);
            this.gpsGroupBox.Controls.Add(this.getlongitudeBtn);
            this.gpsGroupBox.Controls.Add(this.getsatelittesBtn);
            this.gpsGroupBox.Controls.Add(this.gettimeBtn);
            this.gpsGroupBox.Controls.Add(this.getdateBtn);
            this.gpsGroupBox.Location = new System.Drawing.Point(13, 468);
            this.gpsGroupBox.Name = "gpsGroupBox";
            this.gpsGroupBox.Size = new System.Drawing.Size(882, 92);
            this.gpsGroupBox.TabIndex = 127;
            this.gpsGroupBox.TabStop = false;
            this.gpsGroupBox.Text = "GPS";
            // 
            // ClearALLBtn
            // 
            this.ClearALLBtn.Location = new System.Drawing.Point(605, 185);
            this.ClearALLBtn.Margin = new System.Windows.Forms.Padding(4);
            this.ClearALLBtn.Name = "ClearALLBtn";
            this.ClearALLBtn.Size = new System.Drawing.Size(133, 43);
            this.ClearALLBtn.TabIndex = 129;
            this.ClearALLBtn.Text = "Clear ALL";
            this.ClearALLBtn.UseVisualStyleBackColor = true;
            this.ClearALLBtn.Click += new System.EventHandler(this.ClearALLBtn_Click);
            // 
            // getGPSDataBtn
            // 
            this.getGPSDataBtn.Location = new System.Drawing.Point(762, 567);
            this.getGPSDataBtn.Margin = new System.Windows.Forms.Padding(4);
            this.getGPSDataBtn.Name = "getGPSDataBtn";
            this.getGPSDataBtn.Size = new System.Drawing.Size(133, 43);
            this.getGPSDataBtn.TabIndex = 130;
            this.getGPSDataBtn.Text = "Get GPS Data";
            this.getGPSDataBtn.UseVisualStyleBackColor = true;
            this.getGPSDataBtn.Click += new System.EventHandler(this.getGPSDataBtn_Click);
            // 
            // GraphLoggingPicture
            // 
            this.GraphLoggingPicture.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GraphLoggingPicture.Image = global::mySQMPRO.Properties.Resources.Thermometer;
            this.GraphLoggingPicture.InitialImage = global::mySQMPRO.Properties.Resources.Thermometer;
            this.GraphLoggingPicture.Location = new System.Drawing.Point(674, 58);
            this.GraphLoggingPicture.Name = "GraphLoggingPicture";
            this.GraphLoggingPicture.Size = new System.Drawing.Size(64, 64);
            this.GraphLoggingPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GraphLoggingPicture.TabIndex = 107;
            this.GraphLoggingPicture.TabStop = false;
            this.GraphLoggingPicture.Click += new System.EventHandler(this.GraphLoggingPicture_Click);
            // 
            // gpsFixChkBox
            // 
            this.gpsFixChkBox.AutoSize = true;
            this.gpsFixChkBox.Enabled = false;
            this.gpsFixChkBox.Location = new System.Drawing.Point(658, 587);
            this.gpsFixChkBox.Name = "gpsFixChkBox";
            this.gpsFixChkBox.Size = new System.Drawing.Size(80, 21);
            this.gpsFixChkBox.TabIndex = 131;
            this.gpsFixChkBox.Text = "GPS Fix";
            this.gpsFixChkBox.UseVisualStyleBackColor = true;
            // 
            // gpspresentChkBox
            // 
            this.gpspresentChkBox.AutoSize = true;
            this.gpspresentChkBox.Checked = global::mySQMPRO.Properties.Settings.Default.GPSPresent;
            this.gpspresentChkBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::mySQMPRO.Properties.Settings.Default, "GPSPresent", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.gpspresentChkBox.Location = new System.Drawing.Point(520, 587);
            this.gpspresentChkBox.Name = "gpspresentChkBox";
            this.gpspresentChkBox.Size = new System.Drawing.Size(112, 21);
            this.gpspresentChkBox.TabIndex = 128;
            this.gpspresentChkBox.Text = "GPS Present";
            this.gpspresentChkBox.UseVisualStyleBackColor = true;
            // 
            // LogValuesChkBox
            // 
            this.LogValuesChkBox.AutoSize = true;
            this.LogValuesChkBox.Checked = global::mySQMPRO.Properties.Settings.Default.DataLogFileEnabled;
            this.LogValuesChkBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::mySQMPRO.Properties.Settings.Default, "DataLogFileEnabled", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LogValuesChkBox.Location = new System.Drawing.Point(123, 587);
            this.LogValuesChkBox.Name = "LogValuesChkBox";
            this.LogValuesChkBox.Size = new System.Drawing.Size(101, 21);
            this.LogValuesChkBox.TabIndex = 105;
            this.LogValuesChkBox.Text = "Log Values";
            this.LogValuesChkBox.UseVisualStyleBackColor = true;
            this.LogValuesChkBox.CheckedChanged += new System.EventHandler(this.LogValuesChkBox_CheckedChanged);
            // 
            // LogErrorsChkBox
            // 
            this.LogErrorsChkBox.AutoSize = true;
            this.LogErrorsChkBox.Checked = global::mySQMPRO.Properties.Settings.Default.ErrorLoggingEnabled;
            this.LogErrorsChkBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::mySQMPRO.Properties.Settings.Default, "ErrorLoggingEnabled", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LogErrorsChkBox.Location = new System.Drawing.Point(12, 587);
            this.LogErrorsChkBox.Name = "LogErrorsChkBox";
            this.LogErrorsChkBox.Size = new System.Drawing.Size(96, 21);
            this.LogErrorsChkBox.TabIndex = 104;
            this.LogErrorsChkBox.Text = "Log errors";
            this.LogErrorsChkBox.UseVisualStyleBackColor = true;
            this.LogErrorsChkBox.CheckedChanged += new System.EventHandler(this.LogErrorsChkBox_CheckedChanged);
            // 
            // SetPoint2TxtBox
            // 
            this.SetPoint2TxtBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::mySQMPRO.Properties.Settings.Default, "SetPoint2", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.SetPoint2TxtBox.Location = new System.Drawing.Point(426, 165);
            this.SetPoint2TxtBox.Name = "SetPoint2TxtBox";
            this.SetPoint2TxtBox.Size = new System.Drawing.Size(51, 22);
            this.SetPoint2TxtBox.TabIndex = 99;
            this.SetPoint2TxtBox.Text = global::mySQMPRO.Properties.Settings.Default.SetPoint2;
            this.SetPoint2TxtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SetPoint2TxtBox_KeyPress);
            // 
            // SetPoint1TxtBox
            // 
            this.SetPoint1TxtBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::mySQMPRO.Properties.Settings.Default, "SetPoint1", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.SetPoint1TxtBox.Location = new System.Drawing.Point(426, 133);
            this.SetPoint1TxtBox.Name = "SetPoint1TxtBox";
            this.SetPoint1TxtBox.Size = new System.Drawing.Size(51, 22);
            this.SetPoint1TxtBox.TabIndex = 96;
            this.SetPoint1TxtBox.Text = global::mySQMPRO.Properties.Settings.Default.SetPoint1;
            this.SetPoint1TxtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SetPoint1TxtBox_KeyPress);
            // 
            // mySQMPRO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 686);
            this.Controls.Add(this.CloudSensorIILogFormat);
            this.Controls.Add(this.gpsFixChkBox);
            this.Controls.Add(this.getGPSDataBtn);
            this.Controls.Add(this.ClearALLBtn);
            this.Controls.Add(this.gpspresentChkBox);
            this.Controls.Add(this.gpsGroupBox);
            this.Controls.Add(this.bme280grpBox);
            this.Controls.Add(this.getsetpointsBtn);
            this.Controls.Add(this.GraphLoggingPicture);
            this.Controls.Add(this.setpoint2Btn);
            this.Controls.Add(this.LogValuesChkBox);
            this.Controls.Add(this.setpoint1Btn);
            this.Controls.Add(this.LogErrorsChkBox);
            this.Controls.Add(this.SetPoint2TxtBox);
            this.Controls.Add(this.NELMtxtbox);
            this.Controls.Add(this.SetPoint1TxtBox);
            this.Controls.Add(this.GetNELMBtn);
            this.Controls.Add(this.RainSensorGrpBox);
            this.Controls.Add(this.IRSensorGrpBox);
            this.Controls.Add(this.luxTxtBox);
            this.Controls.Add(this.getLuxBtn);
            this.Controls.Add(this.getallBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.statustxtbox);
            this.Controls.Add(this.comportspeed);
            this.Controls.Add(this.comportspeedlabel);
            this.Controls.Add(this.automategrpbox);
            this.Controls.Add(this.automatechkbox);
            this.Controls.Add(this.firmwaretxtbox);
            this.Controls.Add(this.frequencytxtbox);
            this.Controls.Add(this.irradiancetxtbox);
            this.Controls.Add(this.RefreshComPortBtn);
            this.Controls.Add(this.getfirmwareBtn);
            this.Controls.Add(this.getfrequencyBtn);
            this.Controls.Add(this.getirradianceBtn);
            this.Controls.Add(this.getsqmBtn);
            this.Controls.Add(this.DisconnectBtn);
            this.Controls.Add(this.ConnectBtn);
            this.Controls.Add(this.SQMTxtBx);
            this.Controls.Add(this.Exitbtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "mySQMPRO";
            this.Text = "mySQMPRO (c) B Brown 2018";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mySQMGPS_FormClosing);
            this.Load += new System.EventHandler(this.mySQMGPS_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.automategrpbox.ResumeLayout(false);
            this.automategrpbox.PerformLayout();
            this.IRSensorGrpBox.ResumeLayout(false);
            this.IRSensorGrpBox.PerformLayout();
            this.RainSensorGrpBox.ResumeLayout(false);
            this.RainSensorGrpBox.PerformLayout();
            this.bme280grpBox.ResumeLayout(false);
            this.bme280grpBox.PerformLayout();
            this.gpsGroupBox.ResumeLayout(false);
            this.gpsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GraphLoggingPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort myserialPort;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Exitbtn;
        private System.Windows.Forms.TextBox SQMTxtBx;
        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.Button DisconnectBtn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button getsqmBtn;
        private System.Windows.Forms.Button getirradianceBtn;
        private System.Windows.Forms.Button getfrequencyBtn;
        private System.Windows.Forms.Button getfirmwareBtn;
        private System.Windows.Forms.Button RefreshComPortBtn;
        private System.Windows.Forms.TextBox irradiancetxtbox;
        private System.Windows.Forms.TextBox frequencytxtbox;
        private System.Windows.Forms.TextBox firmwaretxtbox;
        private System.Windows.Forms.CheckBox automatechkbox;
        private System.Windows.Forms.RadioButton automate1m;
        private System.Windows.Forms.RadioButton automate5m;
        private System.Windows.Forms.RadioButton automate10m;
        private System.Windows.Forms.GroupBox automategrpbox;
        private System.Windows.Forms.Timer intervaltimer1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forceExitMenuItem;
        private System.Windows.Forms.ListBox comportspeed;
        private System.Windows.Forms.Label comportspeedlabel;
        private System.Windows.Forms.RadioButton automate30m;
        private System.Windows.Forms.RadioButton automate15m;
        private System.Windows.Forms.RadioButton automate30s;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox statustxtbox;
        private System.Windows.Forms.Timer ClearStatusMsgTimer;
        private System.Windows.Forms.Button getallBtn;
        private System.Windows.Forms.Button getLuxBtn;
        private System.Windows.Forms.Button getRainSensorBtn;
        private System.Windows.Forms.Button getMLX90614Btn;
        private System.Windows.Forms.TextBox ObjectTempTxtBox;
        private System.Windows.Forms.TextBox AmbientTempTxtBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox RainSensorTxtBox;
        private System.Windows.Forms.TextBox RainVoltageTxtBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox luxTxtBox;
        private System.Windows.Forms.TextBox SetPoint1TxtBox;
        private System.Windows.Forms.TextBox SetPoint2TxtBox;
        private System.Windows.Forms.GroupBox IRSensorGrpBox;
        private System.Windows.Forms.GroupBox RainSensorGrpBox;
        private System.Windows.Forms.Label SkyConditionLabel;
        private System.Windows.Forms.Label SkyConditionLbl;
        private System.Windows.Forms.Button setpoint2Btn;
        private System.Windows.Forms.Button setpoint1Btn;
        private System.Windows.Forms.Button getsetpointsBtn;
        private System.Windows.Forms.Button GetNELMBtn;
        private System.Windows.Forms.TextBox NELMtxtbox;
        private System.Windows.Forms.ToolStripMenuItem resetLogfilesPathMenuItem;
        private System.Windows.Forms.CheckBox LogErrorsChkBox;
        private System.Windows.Forms.CheckBox LogValuesChkBox;
        private System.Windows.Forms.ToolStripMenuItem alertPopupWhenRainingMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableAlertPopoupMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableAlertPopupMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rainingPollRateMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RainingPollRate5sMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RainingPollRate30sMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RainingPollRate1mMenuItem;
        private System.Windows.Forms.Timer RainTimer;
        private System.Windows.Forms.ToolStripMenuItem RainingPollRate5mMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RainingPollRate10mMenuItem;
        private System.Windows.Forms.PictureBox GraphLoggingPicture;
        private System.Windows.Forms.Button getBME280SensorBtn;
        private System.Windows.Forms.TextBox bme280temptxtbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox bme280humiditytxtbox;
        private System.Windows.Forms.TextBox bme280pressuretxtbox;
        private System.Windows.Forms.TextBox bme280dewpointtxtbox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox bme280grpBox;
        private System.Windows.Forms.ToolStripMenuItem luxValuesMenuItem;
        private System.Windows.Forms.TextBox latitudetxtbox;
        private System.Windows.Forms.TextBox longitudetxtbox;
        private System.Windows.Forms.TextBox satelittestxtbox;
        private System.Windows.Forms.TextBox timetxtbox;
        private System.Windows.Forms.TextBox datetxtbox;
        private System.Windows.Forms.Button getlatitudeBtn;
        private System.Windows.Forms.Button getlongitudeBtn;
        private System.Windows.Forms.Button getsatelittesBtn;
        private System.Windows.Forms.Button gettimeBtn;
        private System.Windows.Forms.Button getdateBtn;
        private System.Windows.Forms.TextBox altitudetxtbox;
        private System.Windows.Forms.Button getaltitudeBtn;
        private System.Windows.Forms.GroupBox gpsGroupBox;
        private System.Windows.Forms.CheckBox gpspresentChkBox;
        private System.Windows.Forms.Button ClearALLBtn;
        private System.Windows.Forms.Button getGPSDataBtn;
        private System.Windows.Forms.CheckBox gpsFixChkBox;
        private System.Windows.Forms.CheckBox CloudSensorIILogFormat;
        private System.Windows.Forms.ToolStripMenuItem setCloudSensorIILogfilenameMenuItem;
    }
}

