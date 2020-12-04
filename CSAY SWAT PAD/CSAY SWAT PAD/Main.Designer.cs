namespace CSAY_SWAT_PAD
{
    partial class FrmMain
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
            this.BtnExit = new System.Windows.Forms.Button();
            this.Lbltitle = new System.Windows.Forms.Label();
            this.BtnWeatherGenInput = new System.Windows.Forms.Button();
            this.BtnTheissenPolySubbasin = new System.Windows.Forms.Button();
            this.BtnParametersRecord = new System.Windows.Forms.Button();
            this.BtnIterationRecord = new System.Windows.Forms.Button();
            this.BtnAbout = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.White;
            this.BtnExit.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.BtnExit.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExit.Location = new System.Drawing.Point(17, 78);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(405, 40);
            this.BtnExit.TabIndex = 0;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = false;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // Lbltitle
            // 
            this.Lbltitle.AutoSize = true;
            this.Lbltitle.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbltitle.ForeColor = System.Drawing.Color.DarkOrange;
            this.Lbltitle.Location = new System.Drawing.Point(19, 22);
            this.Lbltitle.Name = "Lbltitle";
            this.Lbltitle.Size = new System.Drawing.Size(456, 26);
            this.Lbltitle.TabIndex = 29;
            this.Lbltitle.Text = "CSAY SWAT PAD (Preparation && Analysis of Data)";
            // 
            // BtnWeatherGenInput
            // 
            this.BtnWeatherGenInput.BackColor = System.Drawing.Color.White;
            this.BtnWeatherGenInput.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.BtnWeatherGenInput.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnWeatherGenInput.Location = new System.Drawing.Point(18, 25);
            this.BtnWeatherGenInput.Name = "BtnWeatherGenInput";
            this.BtnWeatherGenInput.Size = new System.Drawing.Size(404, 40);
            this.BtnWeatherGenInput.TabIndex = 30;
            this.BtnWeatherGenInput.Text = "Weather Data Generator (WGEN) File";
            this.BtnWeatherGenInput.UseVisualStyleBackColor = false;
            this.BtnWeatherGenInput.Click += new System.EventHandler(this.BtnWeatherGenInput_Click);
            // 
            // BtnTheissenPolySubbasin
            // 
            this.BtnTheissenPolySubbasin.BackColor = System.Drawing.Color.White;
            this.BtnTheissenPolySubbasin.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.BtnTheissenPolySubbasin.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnTheissenPolySubbasin.Location = new System.Drawing.Point(18, 73);
            this.BtnTheissenPolySubbasin.Name = "BtnTheissenPolySubbasin";
            this.BtnTheissenPolySubbasin.Size = new System.Drawing.Size(404, 40);
            this.BtnTheissenPolySubbasin.TabIndex = 31;
            this.BtnTheissenPolySubbasin.Text = "Theissen Polygon Wise Rainfall for Subbasins";
            this.BtnTheissenPolySubbasin.UseVisualStyleBackColor = false;
            this.BtnTheissenPolySubbasin.Click += new System.EventHandler(this.BtnTheissenPolySubbasin_Click);
            // 
            // BtnParametersRecord
            // 
            this.BtnParametersRecord.BackColor = System.Drawing.Color.White;
            this.BtnParametersRecord.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.BtnParametersRecord.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnParametersRecord.Location = new System.Drawing.Point(17, 27);
            this.BtnParametersRecord.Name = "BtnParametersRecord";
            this.BtnParametersRecord.Size = new System.Drawing.Size(404, 40);
            this.BtnParametersRecord.TabIndex = 32;
            this.BtnParametersRecord.Text = "Parameter Records";
            this.BtnParametersRecord.UseVisualStyleBackColor = false;
            this.BtnParametersRecord.Click += new System.EventHandler(this.BtnParametersRecord_Click);
            // 
            // BtnIterationRecord
            // 
            this.BtnIterationRecord.BackColor = System.Drawing.Color.White;
            this.BtnIterationRecord.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.BtnIterationRecord.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnIterationRecord.Location = new System.Drawing.Point(17, 73);
            this.BtnIterationRecord.Name = "BtnIterationRecord";
            this.BtnIterationRecord.Size = new System.Drawing.Size(404, 40);
            this.BtnIterationRecord.TabIndex = 33;
            this.BtnIterationRecord.Text = "Iteration Record";
            this.BtnIterationRecord.UseVisualStyleBackColor = false;
            this.BtnIterationRecord.Click += new System.EventHandler(this.BtnIterationRecord_Click);
            // 
            // BtnAbout
            // 
            this.BtnAbout.BackColor = System.Drawing.Color.White;
            this.BtnAbout.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.BtnAbout.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAbout.ForeColor = System.Drawing.Color.Teal;
            this.BtnAbout.Location = new System.Drawing.Point(17, 32);
            this.BtnAbout.Name = "BtnAbout";
            this.BtnAbout.Size = new System.Drawing.Size(405, 40);
            this.BtnAbout.TabIndex = 34;
            this.BtnAbout.Text = "About";
            this.BtnAbout.UseVisualStyleBackColor = false;
            this.BtnAbout.Click += new System.EventHandler(this.BtnAbout_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnParametersRecord);
            this.groupBox1.Controls.Add(this.BtnIterationRecord);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Green;
            this.groupBox1.Location = new System.Drawing.Point(24, 233);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 127);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Record";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BtnWeatherGenInput);
            this.groupBox2.Controls.Add(this.BtnTheissenPolySubbasin);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.groupBox2.Location = new System.Drawing.Point(24, 91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(442, 127);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WGEN";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.CadetBlue;
            this.label1.Location = new System.Drawing.Point(175, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 26);
            this.label1.TabIndex = 37;
            this.label1.Text = "Version : 1.2019";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BtnAbout);
            this.groupBox3.Controls.Add(this.BtnExit);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Teal;
            this.groupBox3.Location = new System.Drawing.Point(24, 366);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(442, 136);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Others";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(491, 515);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Lbltitle);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.Text = "CSAY Main Version 1.2019";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Label Lbltitle;
        private System.Windows.Forms.Button BtnWeatherGenInput;
        private System.Windows.Forms.Button BtnTheissenPolySubbasin;
        private System.Windows.Forms.Button BtnParametersRecord;
        private System.Windows.Forms.Button BtnIterationRecord;
        private System.Windows.Forms.Button BtnAbout;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

