namespace ThePregame
{
    partial class Form1
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
            this.t11 = new System.Windows.Forms.PictureBox();
            this.t13 = new System.Windows.Forms.PictureBox();
            this.t12 = new System.Windows.Forms.PictureBox();
            this.t23 = new System.Windows.Forms.PictureBox();
            this.t22 = new System.Windows.Forms.PictureBox();
            this.t21 = new System.Windows.Forms.PictureBox();
            this.scenes = new System.Windows.Forms.ListBox();
            this.export = new System.Windows.Forms.Button();
            this.scenestyle = new System.Windows.Forms.ComboBox();
            this.char21 = new System.Windows.Forms.ComboBox();
            this.char22 = new System.Windows.Forms.ComboBox();
            this.char23 = new System.Windows.Forms.ComboBox();
            this.char13 = new System.Windows.Forms.ComboBox();
            this.char12 = new System.Windows.Forms.ComboBox();
            this.char11 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.isChoice = new System.Windows.Forms.CheckBox();
            this.indexSuccess1 = new System.Windows.Forms.TextBox();
            this.indexFail1 = new System.Windows.Forms.TextBox();
            this.indexFail2 = new System.Windows.Forms.TextBox();
            this.indexSuccess2 = new System.Windows.Forms.TextBox();
            this.skillReq1 = new System.Windows.Forms.ComboBox();
            this.skillReq2 = new System.Windows.Forms.ComboBox();
            this.newScene = new System.Windows.Forms.Button();
            this.deleteScene = new System.Windows.Forms.Button();
            this.charPose11 = new System.Windows.Forms.ComboBox();
            this.charPos12 = new System.Windows.Forms.ComboBox();
            this.charPos13 = new System.Windows.Forms.ComboBox();
            this.charPos23 = new System.Windows.Forms.ComboBox();
            this.charPos22 = new System.Windows.Forms.ComboBox();
            this.charPos21 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.t11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.t13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.t12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.t23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.t22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.t21)).BeginInit();
            this.SuspendLayout();
            // 
            // t11
            // 
            this.t11.Location = new System.Drawing.Point(33, 58);
            this.t11.Name = "t11";
            this.t11.Size = new System.Drawing.Size(510, 839);
            this.t11.TabIndex = 1;
            this.t11.TabStop = false;
            // 
            // t13
            // 
            this.t13.Location = new System.Drawing.Point(574, 58);
            this.t13.Name = "t13";
            this.t13.Size = new System.Drawing.Size(510, 839);
            this.t13.TabIndex = 2;
            this.t13.TabStop = false;
            // 
            // t12
            // 
            this.t12.Location = new System.Drawing.Point(286, 58);
            this.t12.Name = "t12";
            this.t12.Size = new System.Drawing.Size(510, 839);
            this.t12.TabIndex = 3;
            this.t12.TabStop = false;
            // 
            // t23
            // 
            this.t23.Location = new System.Drawing.Point(1741, 58);
            this.t23.Name = "t23";
            this.t23.Size = new System.Drawing.Size(510, 839);
            this.t23.TabIndex = 5;
            this.t23.TabStop = false;
            this.t23.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // t22
            // 
            this.t22.Location = new System.Drawing.Point(1478, 58);
            this.t22.Name = "t22";
            this.t22.Size = new System.Drawing.Size(510, 839);
            this.t22.TabIndex = 6;
            this.t22.TabStop = false;
            // 
            // t21
            // 
            this.t21.Location = new System.Drawing.Point(1225, 58);
            this.t21.Name = "t21";
            this.t21.Size = new System.Drawing.Size(510, 839);
            this.t21.TabIndex = 4;
            this.t21.TabStop = false;
            // 
            // scenes
            // 
            this.scenes.FormattingEnabled = true;
            this.scenes.ItemHeight = 31;
            this.scenes.Location = new System.Drawing.Point(33, 1324);
            this.scenes.Name = "scenes";
            this.scenes.Size = new System.Drawing.Size(1899, 252);
            this.scenes.TabIndex = 7;
            // 
            // export
            // 
            this.export.Location = new System.Drawing.Point(1968, 1492);
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(307, 70);
            this.export.TabIndex = 10;
            this.export.Text = "Export .xml";
            this.export.UseVisualStyleBackColor = true;
            // 
            // scenestyle
            // 
            this.scenestyle.FormattingEnabled = true;
            this.scenestyle.Items.AddRange(new object[] {
            "solo",
            "duo",
            "trio",
            "quartet",
            "1v2",
            "2v1",
            "2v2",
            "1v3",
            "3v1",
            "2v3",
            "3v2",
            "3v3"});
            this.scenestyle.Location = new System.Drawing.Point(1010, 900);
            this.scenestyle.Name = "scenestyle";
            this.scenestyle.Size = new System.Drawing.Size(273, 39);
            this.scenestyle.TabIndex = 12;
            this.scenestyle.SelectedIndexChanged += new System.EventHandler(this.scenestyle_SelectedIndexChanged);
            // 
            // char21
            // 
            this.char21.Enabled = false;
            this.char21.FormattingEnabled = true;
            this.char21.Location = new System.Drawing.Point(1980, 926);
            this.char21.Name = "char21";
            this.char21.Size = new System.Drawing.Size(295, 39);
            this.char21.TabIndex = 14;
            this.char21.SelectedIndexChanged += new System.EventHandler(this.char21_SelectedIndexChanged);
            // 
            // char22
            // 
            this.char22.Enabled = false;
            this.char22.FormattingEnabled = true;
            this.char22.Location = new System.Drawing.Point(1981, 983);
            this.char22.Name = "char22";
            this.char22.Size = new System.Drawing.Size(295, 39);
            this.char22.TabIndex = 15;
            this.char22.SelectedIndexChanged += new System.EventHandler(this.char22_SelectedIndexChanged);
            // 
            // char23
            // 
            this.char23.Enabled = false;
            this.char23.FormattingEnabled = true;
            this.char23.Location = new System.Drawing.Point(1981, 1046);
            this.char23.Name = "char23";
            this.char23.Size = new System.Drawing.Size(295, 39);
            this.char23.TabIndex = 16;
            this.char23.SelectedIndexChanged += new System.EventHandler(this.char23_SelectedIndexChanged);
            // 
            // char13
            // 
            this.char13.Enabled = false;
            this.char13.FormattingEnabled = true;
            this.char13.Location = new System.Drawing.Point(34, 1046);
            this.char13.Name = "char13";
            this.char13.Size = new System.Drawing.Size(295, 39);
            this.char13.TabIndex = 19;
            this.char13.SelectedIndexChanged += new System.EventHandler(this.char13_SelectedIndexChanged);
            // 
            // char12
            // 
            this.char12.Enabled = false;
            this.char12.FormattingEnabled = true;
            this.char12.Location = new System.Drawing.Point(34, 983);
            this.char12.Name = "char12";
            this.char12.Size = new System.Drawing.Size(295, 39);
            this.char12.TabIndex = 18;
            this.char12.SelectedIndexChanged += new System.EventHandler(this.char12_SelectedIndexChanged);
            // 
            // char11
            // 
            this.char11.FormattingEnabled = true;
            this.char11.Location = new System.Drawing.Point(33, 926);
            this.char11.Name = "char11";
            this.char11.Size = new System.Drawing.Size(295, 39);
            this.char11.TabIndex = 17;
            this.char11.SelectedIndexChanged += new System.EventHandler(this.char11_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(425, 958);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1459, 127);
            this.textBox1.TabIndex = 20;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(425, 1140);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(1459, 119);
            this.textBox2.TabIndex = 21;
            // 
            // isChoice
            // 
            this.isChoice.AutoSize = true;
            this.isChoice.Location = new System.Drawing.Point(251, 1147);
            this.isChoice.Name = "isChoice";
            this.isChoice.Size = new System.Drawing.Size(170, 36);
            this.isChoice.TabIndex = 22;
            this.isChoice.Text = "Is Choice";
            this.isChoice.UseVisualStyleBackColor = true;
            // 
            // indexSuccess1
            // 
            this.indexSuccess1.Enabled = false;
            this.indexSuccess1.Location = new System.Drawing.Point(1890, 958);
            this.indexSuccess1.Name = "indexSuccess1";
            this.indexSuccess1.Size = new System.Drawing.Size(53, 38);
            this.indexSuccess1.TabIndex = 23;
            // 
            // indexFail1
            // 
            this.indexFail1.Enabled = false;
            this.indexFail1.Location = new System.Drawing.Point(1890, 1047);
            this.indexFail1.Name = "indexFail1";
            this.indexFail1.Size = new System.Drawing.Size(53, 38);
            this.indexFail1.TabIndex = 24;
            // 
            // indexFail2
            // 
            this.indexFail2.Enabled = false;
            this.indexFail2.Location = new System.Drawing.Point(1890, 1220);
            this.indexFail2.Name = "indexFail2";
            this.indexFail2.Size = new System.Drawing.Size(53, 38);
            this.indexFail2.TabIndex = 26;
            // 
            // indexSuccess2
            // 
            this.indexSuccess2.Enabled = false;
            this.indexSuccess2.Location = new System.Drawing.Point(1890, 1140);
            this.indexSuccess2.Name = "indexSuccess2";
            this.indexSuccess2.Size = new System.Drawing.Size(53, 38);
            this.indexSuccess2.TabIndex = 25;
            // 
            // skillReq1
            // 
            this.skillReq1.Enabled = false;
            this.skillReq1.FormattingEnabled = true;
            this.skillReq1.Items.AddRange(new object[] {
            "solo",
            "duo",
            "trio",
            "quartet",
            "1v1",
            "1v2",
            "2v1",
            "2v2",
            "1v3",
            "3v1",
            "2v3",
            "3v2",
            "3v3"});
            this.skillReq1.Location = new System.Drawing.Point(1010, 1091);
            this.skillReq1.Name = "skillReq1";
            this.skillReq1.Size = new System.Drawing.Size(273, 39);
            this.skillReq1.TabIndex = 27;
            // 
            // skillReq2
            // 
            this.skillReq2.Enabled = false;
            this.skillReq2.FormattingEnabled = true;
            this.skillReq2.Items.AddRange(new object[] {
            "solo",
            "duo",
            "trio",
            "quartet",
            "1v1",
            "1v2",
            "2v1",
            "2v2",
            "1v3",
            "3v1",
            "2v3",
            "3v2",
            "3v3"});
            this.skillReq2.Location = new System.Drawing.Point(1010, 1265);
            this.skillReq2.Name = "skillReq2";
            this.skillReq2.Size = new System.Drawing.Size(273, 39);
            this.skillReq2.TabIndex = 28;
            // 
            // newScene
            // 
            this.newScene.Location = new System.Drawing.Point(1968, 1324);
            this.newScene.Name = "newScene";
            this.newScene.Size = new System.Drawing.Size(307, 78);
            this.newScene.TabIndex = 29;
            this.newScene.Text = "Load Scene";
            this.newScene.UseVisualStyleBackColor = true;
            // 
            // deleteScene
            // 
            this.deleteScene.Location = new System.Drawing.Point(1969, 1408);
            this.deleteScene.Name = "deleteScene";
            this.deleteScene.Size = new System.Drawing.Size(307, 78);
            this.deleteScene.TabIndex = 30;
            this.deleteScene.Text = "Delete Scene";
            this.deleteScene.UseVisualStyleBackColor = true;
            // 
            // charPose11
            // 
            this.charPose11.FormattingEnabled = true;
            this.charPose11.Location = new System.Drawing.Point(34, 13);
            this.charPose11.Name = "charPose11";
            this.charPose11.Size = new System.Drawing.Size(207, 39);
            this.charPose11.TabIndex = 31;
            this.charPose11.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // charPos12
            // 
            this.charPos12.FormattingEnabled = true;
            this.charPos12.Location = new System.Drawing.Point(286, 13);
            this.charPos12.Name = "charPos12";
            this.charPos12.Size = new System.Drawing.Size(207, 39);
            this.charPos12.TabIndex = 32;
            this.charPos12.SelectedIndexChanged += new System.EventHandler(this.charPos12_SelectedIndexChanged);
            // 
            // charPos13
            // 
            this.charPos13.FormattingEnabled = true;
            this.charPos13.Location = new System.Drawing.Point(574, 12);
            this.charPos13.Name = "charPos13";
            this.charPos13.Size = new System.Drawing.Size(207, 39);
            this.charPos13.TabIndex = 33;
            this.charPos13.SelectedIndexChanged += new System.EventHandler(this.charPos13_SelectedIndexChanged);
            // 
            // charPos23
            // 
            this.charPos23.FormattingEnabled = true;
            this.charPos23.Location = new System.Drawing.Point(2044, 12);
            this.charPos23.Name = "charPos23";
            this.charPos23.Size = new System.Drawing.Size(207, 39);
            this.charPos23.TabIndex = 34;
            this.charPos23.SelectedIndexChanged += new System.EventHandler(this.charPos23_SelectedIndexChanged);
            // 
            // charPos22
            // 
            this.charPos22.FormattingEnabled = true;
            this.charPos22.Location = new System.Drawing.Point(1781, 12);
            this.charPos22.Name = "charPos22";
            this.charPos22.Size = new System.Drawing.Size(207, 39);
            this.charPos22.TabIndex = 35;
            this.charPos22.SelectedIndexChanged += new System.EventHandler(this.charPos22_SelectedIndexChanged);
            // 
            // charPos21
            // 
            this.charPos21.FormattingEnabled = true;
            this.charPos21.Location = new System.Drawing.Point(1528, 12);
            this.charPos21.Name = "charPos21";
            this.charPos21.Size = new System.Drawing.Size(207, 39);
            this.charPos21.TabIndex = 36;
            this.charPos21.SelectedIndexChanged += new System.EventHandler(this.charPos21_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(240F, 240F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(2318, 1612);
            this.Controls.Add(this.charPos21);
            this.Controls.Add(this.charPos22);
            this.Controls.Add(this.charPos23);
            this.Controls.Add(this.charPos13);
            this.Controls.Add(this.charPos12);
            this.Controls.Add(this.charPose11);
            this.Controls.Add(this.deleteScene);
            this.Controls.Add(this.newScene);
            this.Controls.Add(this.skillReq2);
            this.Controls.Add(this.skillReq1);
            this.Controls.Add(this.indexFail2);
            this.Controls.Add(this.indexSuccess2);
            this.Controls.Add(this.indexFail1);
            this.Controls.Add(this.indexSuccess1);
            this.Controls.Add(this.isChoice);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.char13);
            this.Controls.Add(this.char12);
            this.Controls.Add(this.char11);
            this.Controls.Add(this.char23);
            this.Controls.Add(this.char22);
            this.Controls.Add(this.char21);
            this.Controls.Add(this.scenestyle);
            this.Controls.Add(this.export);
            this.Controls.Add(this.scenes);
            this.Controls.Add(this.t21);
            this.Controls.Add(this.t22);
            this.Controls.Add(this.t23);
            this.Controls.Add(this.t13);
            this.Controls.Add(this.t12);
            this.Controls.Add(this.t11);
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "The Pregame";
            ((System.ComponentModel.ISupportInitialize)(this.t11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.t13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.t12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.t23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.t22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.t21)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox t11;
        private System.Windows.Forms.PictureBox t13;
        private System.Windows.Forms.PictureBox t12;
        private System.Windows.Forms.PictureBox t23;
        private System.Windows.Forms.PictureBox t22;
        private System.Windows.Forms.PictureBox t21;
        private System.Windows.Forms.ListBox scenes;
        private System.Windows.Forms.Button export;
        private System.Windows.Forms.ComboBox scenestyle;
        private System.Windows.Forms.ComboBox char21;
        private System.Windows.Forms.ComboBox char22;
        private System.Windows.Forms.ComboBox char23;
        private System.Windows.Forms.ComboBox char13;
        private System.Windows.Forms.ComboBox char12;
        private System.Windows.Forms.ComboBox char11;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckBox isChoice;
        private System.Windows.Forms.TextBox indexSuccess1;
        private System.Windows.Forms.TextBox indexFail1;
        private System.Windows.Forms.TextBox indexFail2;
        private System.Windows.Forms.TextBox indexSuccess2;
        private System.Windows.Forms.ComboBox skillReq1;
        private System.Windows.Forms.ComboBox skillReq2;
        private System.Windows.Forms.Button newScene;
        private System.Windows.Forms.Button deleteScene;
        private System.Windows.Forms.ComboBox charPose11;
        private System.Windows.Forms.ComboBox charPos12;
        private System.Windows.Forms.ComboBox charPos13;
        private System.Windows.Forms.ComboBox charPos23;
        private System.Windows.Forms.ComboBox charPos22;
        private System.Windows.Forms.ComboBox charPos21;
    }
}

