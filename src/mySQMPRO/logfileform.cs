using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mySQMPRO
{
    public partial class logfileform : Form
    {
        public logfileform()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            // this.MinimizeBox = false;  // do not as user cannot minimize form!!!
        }

        private void logfileform_Load(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
            this.Location = Properties.Settings.Default.LogFormLocation;

            Locationtxtbox.Text = Properties.Settings.Default.LogPathName;
            Locationtxtbox.Update();
        }

        private void logfileform_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.LogFormLocation = this.Location;

            if (Locationtxtbox.Text == "")
            {
                MessageBox.Show("Please specify a Folder", "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // cancel the form closing
                e.Cancel = true;
            }
            else
            {
                Properties.Settings.Default.LogPathName = Locationtxtbox.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            if (Locationtxtbox.Text == "")
            {
                MessageBox.Show("Please specify a Folder", "mySQGMGPS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Properties.Settings.Default.LogPathName = Locationtxtbox.Text;
                Properties.Settings.Default.Save();
                Close();
            }
        }

        private void SetDirectoryBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd2 = new FolderBrowserDialog();
            if (fd2.ShowDialog() == DialogResult.OK)
            {
                Locationtxtbox.Text = fd2.SelectedPath;
                Locationtxtbox.Update();
                Properties.Settings.Default.LogPathName = Locationtxtbox.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show("Folder not specified, default to C:\\", "No folder selected", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                Locationtxtbox.Text = "C:\\";
                Locationtxtbox.Update();

                Properties.Settings.Default.LogPathName = Locationtxtbox.Text;
                Properties.Settings.Default.Save();
            }
        }
    }
}
