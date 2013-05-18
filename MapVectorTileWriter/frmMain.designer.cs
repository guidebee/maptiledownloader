namespace MapVectorTileWriter
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.picStoredMap = new System.Windows.Forms.PictureBox();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkLevel15 = new System.Windows.Forms.CheckBox();
            this.chkLevel17 = new System.Windows.Forms.CheckBox();
            this.chkLevel16 = new System.Windows.Forms.CheckBox();
            this.chkLevel14 = new System.Windows.Forms.CheckBox();
            this.chkLevel11 = new System.Windows.Forms.CheckBox();
            this.chkLevel13 = new System.Windows.Forms.CheckBox();
            this.chkLevel12 = new System.Windows.Forms.CheckBox();
            this.chkLevel10 = new System.Windows.Forms.CheckBox();
            this.chkLevel7 = new System.Windows.Forms.CheckBox();
            this.chkLevel5 = new System.Windows.Forms.CheckBox();
            this.chkLevel9 = new System.Windows.Forms.CheckBox();
            this.chkLevel8 = new System.Windows.Forms.CheckBox();
            this.chkLevel6 = new System.Windows.Forms.CheckBox();
            this.chkLevel4 = new System.Windows.Forms.CheckBox();
            this.chkLevel3 = new System.Windows.Forms.CheckBox();
            this.chkLevel2 = new System.Windows.Forms.CheckBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.cboMapType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEndY = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEndX = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtStartY = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStartX = new System.Windows.Forms.MaskedTextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStoredMap)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.picStoredMap);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(806, 798);
            this.panel1.TabIndex = 2;
            // 
            // picStoredMap
            // 
            this.picStoredMap.Location = new System.Drawing.Point(13, 14);
            this.picStoredMap.Name = "picStoredMap";
            this.picStoredMap.Size = new System.Drawing.Size(768, 768);
            this.picStoredMap.TabIndex = 2;
            this.picStoredMap.TabStop = false;
            // 
            // btnUp
            // 
            this.btnUp.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUp.Location = new System.Drawing.Point(951, 12);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(62, 33);
            this.btnUp.TabIndex = 3;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLeft.Location = new System.Drawing.Point(860, 56);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(62, 33);
            this.btnLeft.TabIndex = 4;
            this.btnLeft.Text = "Left";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnDown
            // 
            this.btnDown.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDown.Location = new System.Drawing.Point(951, 96);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(62, 33);
            this.btnDown.TabIndex = 5;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnRight
            // 
            this.btnRight.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRight.Location = new System.Drawing.Point(1039, 56);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(62, 33);
            this.btnRight.TabIndex = 6;
            this.btnRight.Text = "Right";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomIn.Location = new System.Drawing.Point(883, 135);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(86, 34);
            this.btnZoomIn.TabIndex = 7;
            this.btnZoomIn.Text = "Zoom In";
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomOut.Location = new System.Drawing.Point(992, 135);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(86, 34);
            this.btnZoomOut.TabIndex = 9;
            this.btnZoomOut.Text = "Zoom Out";
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(951, 56);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(62, 33);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkLevel15);
            this.groupBox2.Controls.Add(this.chkLevel17);
            this.groupBox2.Controls.Add(this.chkLevel16);
            this.groupBox2.Controls.Add(this.chkLevel14);
            this.groupBox2.Controls.Add(this.chkLevel11);
            this.groupBox2.Controls.Add(this.chkLevel13);
            this.groupBox2.Controls.Add(this.chkLevel12);
            this.groupBox2.Controls.Add(this.chkLevel10);
            this.groupBox2.Controls.Add(this.chkLevel7);
            this.groupBox2.Controls.Add(this.chkLevel5);
            this.groupBox2.Controls.Add(this.chkLevel9);
            this.groupBox2.Controls.Add(this.chkLevel8);
            this.groupBox2.Controls.Add(this.chkLevel6);
            this.groupBox2.Controls.Add(this.chkLevel4);
            this.groupBox2.Controls.Add(this.chkLevel3);
            this.groupBox2.Controls.Add(this.chkLevel2);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(833, 325);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(315, 171);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Map Level Selector";
            // 
            // chkLevel15
            // 
            this.chkLevel15.AutoSize = true;
            this.chkLevel15.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLevel15.Location = new System.Drawing.Point(91, 128);
            this.chkLevel15.Name = "chkLevel15";
            this.chkLevel15.Size = new System.Drawing.Size(45, 22);
            this.chkLevel15.TabIndex = 19;
            this.chkLevel15.Text = "15";
            this.chkLevel15.UseVisualStyleBackColor = true;
            this.chkLevel15.Click += new System.EventHandler(this.chkLevel15_Click);
            // 
            // chkLevel17
            // 
            this.chkLevel17.AutoSize = true;
            this.chkLevel17.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLevel17.Location = new System.Drawing.Point(219, 128);
            this.chkLevel17.Name = "chkLevel17";
            this.chkLevel17.Size = new System.Drawing.Size(45, 22);
            this.chkLevel17.TabIndex = 18;
            this.chkLevel17.Text = "17";
            this.chkLevel17.UseVisualStyleBackColor = true;
            this.chkLevel17.Click += new System.EventHandler(this.chkLevel17_Click);
            // 
            // chkLevel16
            // 
            this.chkLevel16.AutoSize = true;
            this.chkLevel16.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLevel16.Location = new System.Drawing.Point(155, 128);
            this.chkLevel16.Name = "chkLevel16";
            this.chkLevel16.Size = new System.Drawing.Size(45, 22);
            this.chkLevel16.TabIndex = 17;
            this.chkLevel16.Text = "16";
            this.chkLevel16.UseVisualStyleBackColor = true;
            this.chkLevel16.Click += new System.EventHandler(this.chkLevel16_Click);
            // 
            // chkLevel14
            // 
            this.chkLevel14.AutoSize = true;
            this.chkLevel14.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLevel14.Location = new System.Drawing.Point(27, 128);
            this.chkLevel14.Name = "chkLevel14";
            this.chkLevel14.Size = new System.Drawing.Size(45, 22);
            this.chkLevel14.TabIndex = 16;
            this.chkLevel14.Text = "14";
            this.chkLevel14.UseVisualStyleBackColor = true;
            this.chkLevel14.Click += new System.EventHandler(this.chkLevel14_Click);
            // 
            // chkLevel11
            // 
            this.chkLevel11.AutoSize = true;
            this.chkLevel11.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLevel11.Location = new System.Drawing.Point(91, 97);
            this.chkLevel11.Name = "chkLevel11";
            this.chkLevel11.Size = new System.Drawing.Size(45, 22);
            this.chkLevel11.TabIndex = 15;
            this.chkLevel11.Text = "11";
            this.chkLevel11.UseVisualStyleBackColor = true;
            this.chkLevel11.Click += new System.EventHandler(this.chkLevel11_Click);
            // 
            // chkLevel13
            // 
            this.chkLevel13.AutoSize = true;
            this.chkLevel13.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLevel13.Location = new System.Drawing.Point(219, 97);
            this.chkLevel13.Name = "chkLevel13";
            this.chkLevel13.Size = new System.Drawing.Size(45, 22);
            this.chkLevel13.TabIndex = 14;
            this.chkLevel13.Text = "13";
            this.chkLevel13.UseVisualStyleBackColor = true;
            this.chkLevel13.Click += new System.EventHandler(this.chkLevel13_Click);
            // 
            // chkLevel12
            // 
            this.chkLevel12.AutoSize = true;
            this.chkLevel12.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLevel12.Location = new System.Drawing.Point(155, 97);
            this.chkLevel12.Name = "chkLevel12";
            this.chkLevel12.Size = new System.Drawing.Size(45, 22);
            this.chkLevel12.TabIndex = 13;
            this.chkLevel12.Text = "12";
            this.chkLevel12.UseVisualStyleBackColor = true;
            this.chkLevel12.Click += new System.EventHandler(this.chkLevel12_Click);
            // 
            // chkLevel10
            // 
            this.chkLevel10.AutoSize = true;
            this.chkLevel10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLevel10.Location = new System.Drawing.Point(27, 97);
            this.chkLevel10.Name = "chkLevel10";
            this.chkLevel10.Size = new System.Drawing.Size(45, 22);
            this.chkLevel10.TabIndex = 12;
            this.chkLevel10.Text = "10";
            this.chkLevel10.UseVisualStyleBackColor = true;
            this.chkLevel10.Click += new System.EventHandler(this.chkLevel10_Click);
            // 
            // chkLevel7
            // 
            this.chkLevel7.AutoSize = true;
            this.chkLevel7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLevel7.Location = new System.Drawing.Point(91, 66);
            this.chkLevel7.Name = "chkLevel7";
            this.chkLevel7.Size = new System.Drawing.Size(36, 22);
            this.chkLevel7.TabIndex = 11;
            this.chkLevel7.Text = "7";
            this.chkLevel7.UseVisualStyleBackColor = true;
            this.chkLevel7.Click += new System.EventHandler(this.chkLevel7_Click);
            // 
            // chkLevel5
            // 
            this.chkLevel5.AutoSize = true;
            this.chkLevel5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLevel5.Location = new System.Drawing.Point(219, 35);
            this.chkLevel5.Name = "chkLevel5";
            this.chkLevel5.Size = new System.Drawing.Size(36, 22);
            this.chkLevel5.TabIndex = 10;
            this.chkLevel5.Text = "5";
            this.chkLevel5.UseVisualStyleBackColor = true;
            this.chkLevel5.Click += new System.EventHandler(this.chkLevel5_Click);
            // 
            // chkLevel9
            // 
            this.chkLevel9.AutoSize = true;
            this.chkLevel9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLevel9.Location = new System.Drawing.Point(219, 66);
            this.chkLevel9.Name = "chkLevel9";
            this.chkLevel9.Size = new System.Drawing.Size(36, 22);
            this.chkLevel9.TabIndex = 6;
            this.chkLevel9.Text = "9";
            this.chkLevel9.UseVisualStyleBackColor = true;
            this.chkLevel9.Click += new System.EventHandler(this.chkLevel9_Click);
            // 
            // chkLevel8
            // 
            this.chkLevel8.AutoSize = true;
            this.chkLevel8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLevel8.Location = new System.Drawing.Point(155, 66);
            this.chkLevel8.Name = "chkLevel8";
            this.chkLevel8.Size = new System.Drawing.Size(36, 22);
            this.chkLevel8.TabIndex = 5;
            this.chkLevel8.Text = "8";
            this.chkLevel8.UseVisualStyleBackColor = true;
            this.chkLevel8.Click += new System.EventHandler(this.chkLevel8_Click);
            // 
            // chkLevel6
            // 
            this.chkLevel6.AutoSize = true;
            this.chkLevel6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLevel6.Location = new System.Drawing.Point(27, 66);
            this.chkLevel6.Name = "chkLevel6";
            this.chkLevel6.Size = new System.Drawing.Size(36, 22);
            this.chkLevel6.TabIndex = 4;
            this.chkLevel6.Text = "6";
            this.chkLevel6.UseVisualStyleBackColor = true;
            this.chkLevel6.Click += new System.EventHandler(this.chkLevel6_Click);
            // 
            // chkLevel4
            // 
            this.chkLevel4.AutoSize = true;
            this.chkLevel4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLevel4.Location = new System.Drawing.Point(155, 35);
            this.chkLevel4.Name = "chkLevel4";
            this.chkLevel4.Size = new System.Drawing.Size(36, 22);
            this.chkLevel4.TabIndex = 3;
            this.chkLevel4.Text = "4";
            this.chkLevel4.UseVisualStyleBackColor = true;
            this.chkLevel4.Click += new System.EventHandler(this.chkLevel4_Click);
            // 
            // chkLevel3
            // 
            this.chkLevel3.AutoSize = true;
            this.chkLevel3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLevel3.Location = new System.Drawing.Point(91, 35);
            this.chkLevel3.Name = "chkLevel3";
            this.chkLevel3.Size = new System.Drawing.Size(36, 22);
            this.chkLevel3.TabIndex = 2;
            this.chkLevel3.Text = "3";
            this.chkLevel3.UseVisualStyleBackColor = true;
            this.chkLevel3.Click += new System.EventHandler(this.chkLevel3_Click);
            // 
            // chkLevel2
            // 
            this.chkLevel2.AutoSize = true;
            this.chkLevel2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLevel2.Location = new System.Drawing.Point(27, 35);
            this.chkLevel2.Name = "chkLevel2";
            this.chkLevel2.Size = new System.Drawing.Size(36, 22);
            this.chkLevel2.TabIndex = 1;
            this.chkLevel2.Text = "2";
            this.chkLevel2.UseVisualStyleBackColor = true;
            this.chkLevel2.Click += new System.EventHandler(this.chkLevel2_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(831, 508);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(317, 23);
            this.lblTotal.TabIndex = 13;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(834, 617);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(314, 129);
            this.txtMessage.TabIndex = 14;
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(833, 762);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(63, 34);
            this.btnStart.TabIndex = 15;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.Location = new System.Drawing.Point(918, 762);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(63, 34);
            this.btnPause.TabIndex = 16;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(1003, 762);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(63, 34);
            this.btnStop.TabIndex = 17;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(1088, 762);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(63, 34);
            this.btnExit.TabIndex = 18;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cboMapType
            // 
            this.cboMapType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMapType.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMapType.FormattingEnabled = true;
            this.cboMapType.Items.AddRange(new object[] {
                                                            "GOOGLEMAP",
                                                            "GOOGLESATELLITE",
                                                            "GOOGLEHYBRID",
                                                            "GOOGLECHINA",
                                                            "YAHOOMAP",
                                                            "YAHOOSATELLITE",
                                                            "YAHOOHYBRID",
                                                            "YAHOOINDIAMAP",
                                                            "YAHOOINDIAHYBRID",
                                                            "ASKDOTCOMMAP",
                                                            "ASKDOTCOMSATELLITE",
                                                            "ASKDOTCOMHYBRID",
                                                            "MICROSOFTMAP",
                                                            "MICROSOFTSATELLITE",
                                                            "MICROSOFTHYBRID",
                                                            "MICROSOFTCHINA",
                                                            "OPENSTREETMAP",
                                                            "MAPABCCHINA"});
            this.cboMapType.Location = new System.Drawing.Point(918, 543);
            this.cboMapType.Name = "cboMapType";
            this.cboMapType.Size = new System.Drawing.Size(229, 26);
            this.cboMapType.TabIndex = 19;
            this.cboMapType.SelectedIndexChanged += new System.EventHandler(this.cboMapType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(834, 543);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 23);
            this.label1.TabIndex = 20;
            this.label1.Text = "Map Type";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtEndY);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtEndX);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtStartY);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtStartX);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(833, 184);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 135);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Map Tile Selector";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(173, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 18);
            this.label5.TabIndex = 10;
            this.label5.Text = "Y";
            // 
            // txtEndY
            // 
            this.txtEndY.Location = new System.Drawing.Point(199, 88);
            this.txtEndY.Mask = "9";
            this.txtEndY.Name = "txtEndY";
            this.txtEndY.Size = new System.Drawing.Size(65, 26);
            this.txtEndY.TabIndex = 9;
            this.txtEndY.Text = "0";
            this.txtEndY.TextChanged += new System.EventHandler(this.txtEndY_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(173, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 18);
            this.label6.TabIndex = 8;
            this.label6.Text = "X";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(173, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 18);
            this.label7.TabIndex = 7;
            this.label7.Text = "EndIndex";
            // 
            // txtEndX
            // 
            this.txtEndX.AccessibleDescription = "txtEndY";
            this.txtEndX.Location = new System.Drawing.Point(199, 56);
            this.txtEndX.Mask = "9";
            this.txtEndX.Name = "txtEndX";
            this.txtEndX.Size = new System.Drawing.Size(65, 26);
            this.txtEndX.TabIndex = 6;
            this.txtEndX.Text = "0";
            this.txtEndX.TextChanged += new System.EventHandler(this.txtEndX_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 18);
            this.label4.TabIndex = 5;
            this.label4.Text = "Y";
            // 
            // txtStartY
            // 
            this.txtStartY.Location = new System.Drawing.Point(50, 85);
            this.txtStartY.Mask = "9";
            this.txtStartY.Name = "txtStartY";
            this.txtStartY.Size = new System.Drawing.Size(65, 26);
            this.txtStartY.TabIndex = 4;
            this.txtStartY.Text = "0";
            this.txtStartY.TextChanged += new System.EventHandler(this.txtStartY_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "StartIndex";
            // 
            // txtStartX
            // 
            this.txtStartX.Location = new System.Drawing.Point(50, 53);
            this.txtStartX.Mask = "9";
            this.txtStartX.Name = "txtStartX";
            this.txtStartX.Size = new System.Drawing.Size(65, 26);
            this.txtStartX.TabIndex = 0;
            this.txtStartX.Text = "0";
            this.txtStartX.TextChanged += new System.EventHandler(this.txtStartX_TextChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(837, 588);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(310, 23);
            this.progressBar1.TabIndex = 21;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 822);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboMapType);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnZoomOut);
            this.Controls.Add(this.btnZoomIn);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Guidebee Map Tile Downloader   http://www.guidebee.biz";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picStoredMap)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picStoredMap;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkLevel15;
        private System.Windows.Forms.CheckBox chkLevel17;
        private System.Windows.Forms.CheckBox chkLevel16;
        private System.Windows.Forms.CheckBox chkLevel14;
        private System.Windows.Forms.CheckBox chkLevel11;
        private System.Windows.Forms.CheckBox chkLevel13;
        private System.Windows.Forms.CheckBox chkLevel12;
        private System.Windows.Forms.CheckBox chkLevel10;
        private System.Windows.Forms.CheckBox chkLevel7;
        private System.Windows.Forms.CheckBox chkLevel5;
        private System.Windows.Forms.CheckBox chkLevel9;
        private System.Windows.Forms.CheckBox chkLevel8;
        private System.Windows.Forms.CheckBox chkLevel6;
        private System.Windows.Forms.CheckBox chkLevel4;
        private System.Windows.Forms.CheckBox chkLevel3;
        private System.Windows.Forms.CheckBox chkLevel2;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ComboBox cboMapType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox txtEndY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox txtEndX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox txtStartY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox txtStartX;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}