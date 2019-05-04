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
    public partial class GetCloudSensorIILogFilename : Form
    {
        public GetCloudSensorIILogFilename()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            // this.MinimizeBox = false;  // do not as user cannot minimize form!!!
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        { 
            if (filename.Text == "")
                return;
            if (filename.Text.IndexOf('/') > 0)
            {
                return;
            }
            if (filename.Text.IndexOf('\\') > 0)
            {
                return;
            }
            if (filename.Text.IndexOf('.') > 0)
            {
                String txttmp = filename.Text;
                filename.Text = txttmp.Substring(0, filename.Text.IndexOf('.') - 1);
                filename.Update();
            } 
            this.Close();
        }

        private void GetCloudSensorIILogFilename_cs_FormClosing(object sender, FormClosingEventArgs e)
        {
            // repeat in case user closes form wthout using close() button
            if (filename.Text == "")
                return;
            if (filename.Text.IndexOf('/') > 0)
            {
                return;
            }
            if (filename.Text.IndexOf('\\') > 0)
            {
                return;
            } 
            if (filename.Text.IndexOf('.') > 0)
            {
                String txttmp = filename.Text;
                filename.Text = txttmp.Substring(0, filename.Text.IndexOf('.') - 1);
                filename.Update();
            }
            mySQMPRO.CSFormActive = false;
            Properties.Settings.Default.CloudSensorIIFormLocation = this.Location;
            Properties.Settings.Default.Save();
        }

        private void GetCloudSensorIILogFilename_cs_Load(object sender, EventArgs e)
        {
            this.Location = Properties.Settings.Default.CloudSensorIIFormLocation;
        }
    }
}
