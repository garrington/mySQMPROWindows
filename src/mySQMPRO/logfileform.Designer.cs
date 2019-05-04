namespace mySQMPRO
{
    partial class logfileform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(logfileform));
            this.label2 = new System.Windows.Forms.Label();
            this.Locationtxtbox = new System.Windows.Forms.TextBox();
            this.SetDirectoryBtn = new System.Windows.Forms.Button();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 73);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 84;
            this.label2.Text = "Location";
            // 
            // Locationtxtbox
            // 
            this.Locationtxtbox.Location = new System.Drawing.Point(15, 92);
            this.Locationtxtbox.Margin = new System.Windows.Forms.Padding(4);
            this.Locationtxtbox.Name = "Locationtxtbox";
            this.Locationtxtbox.ReadOnly = true;
            this.Locationtxtbox.Size = new System.Drawing.Size(477, 22);
            this.Locationtxtbox.TabIndex = 83;
            // 
            // SetDirectoryBtn
            // 
            this.SetDirectoryBtn.Location = new System.Drawing.Point(42, 20);
            this.SetDirectoryBtn.Margin = new System.Windows.Forms.Padding(4);
            this.SetDirectoryBtn.Name = "SetDirectoryBtn";
            this.SetDirectoryBtn.Size = new System.Drawing.Size(423, 31);
            this.SetDirectoryBtn.TabIndex = 81;
            this.SetDirectoryBtn.Text = "Click to set the folder where the logs will be stored";
            this.SetDirectoryBtn.UseVisualStyleBackColor = true;
            this.SetDirectoryBtn.Click += new System.EventHandler(this.SetDirectoryBtn_Click);
            // 
            // CloseBtn
            // 
            this.CloseBtn.Location = new System.Drawing.Point(400, 132);
            this.CloseBtn.Margin = new System.Windows.Forms.Padding(4);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(92, 33);
            this.CloseBtn.TabIndex = 79;
            this.CloseBtn.Text = "Close";
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // logfileform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 176);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Locationtxtbox);
            this.Controls.Add(this.SetDirectoryBtn);
            this.Controls.Add(this.CloseBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "logfileform";
            this.Text = "mySQMPRO";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.logfileform_FormClosing);
            this.Load += new System.EventHandler(this.logfileform_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Locationtxtbox;
        private System.Windows.Forms.Button SetDirectoryBtn;
        private System.Windows.Forms.Button CloseBtn;
    }
}