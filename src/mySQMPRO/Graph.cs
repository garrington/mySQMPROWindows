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
    public partial class Graph : Form
    {
        public string myTime;               // hold time at which temperature value is plotted
        public int numberofpointplots = 9;  // 9 display points on the graph
        public int ambientplotcount = 0;           // used to start deleting first value and thus scroll chart points
        public int objectplotcount = 0;
        public int luxplotcount = 0;
        public int sqmplotcount = 0;
        public int tempdiffplotcount = 0;
        public int bme280tplotcount = 0;
        public int bme280hplotcount = 0;
        public int bme280dplotcount = 0;
        public int bme280pplotcount = 0;

        public Graph()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            // this.MinimizeBox = false;  // do not as user cannot minimize form!!!
        }

        // add a temperature value to the chart
        public void AddToChart(double datavalue, int pos)
        {
            String TimeNow;
            String myTime = "";

            chart1.ChartAreas[0].RecalculateAxesScale();
            chart2.ChartAreas[0].RecalculateAxesScale();
            chart3.ChartAreas[0].RecalculateAxesScale();
            chart4.ChartAreas[0].RecalculateAxesScale();
            chart5.ChartAreas[0].RecalculateAxesScale();

            // Calculate current time as this needs to be plotted on Graph X
            TimeNow = DateTime.Now.TimeOfDay.ToString();       // get current time
            for (int lpval = 0; lpval <= 7; lpval++)           // hh:mm:ss.nnnnn
            {
                myTime += TimeNow[lpval];
            }
            myTime += "";                                       // hh.mm.ss

            // increment the number of plotted points and if exceeds number of points on the chart 
            // then delete the first point and thus the plotted points will scroll
            switch (pos)
            {
                case 0:     // object temp
                    objectplotcount++;
                    if (objectplotcount > numberofpointplots)
                    {
                        try
                        {
                            chart1.Series["ObjectTemp"].Points.RemoveAt(0);
                        }
                        catch (NullReferenceException)
                        {
                            // do nothing
                            return;
                        }
                    }
                    // add the point (temperature, time) to the chart
                    // chart1.Series["Temp"].XValueType = ChartValueType.Time;
                    try
                    {
                        chart1.Series["ObjectTemp"].Points.AddXY(myTime, datavalue);
                    }
                    catch (NullReferenceException)
                    {
                        // do nothing
                        return;
                    }
                    break;
                case 1:     // ambient temp
                    ambientplotcount++;
                    if (ambientplotcount > numberofpointplots)
                    {
                        try
                        {
                            chart1.Series["AmbientTemp"].Points.RemoveAt(0);
                        }
                        catch (NullReferenceException)
                        {
                            // do nothing
                            return;
                        }
                    }
                    // add the point (temperature, time) to the chart
                    // chart1.Series["Temp"].XValueType = ChartValueType.Time;
                    try
                    {
                        chart1.Series["AmbientTemp"].Points.AddXY(myTime, datavalue);
                    }
                    catch (NullReferenceException)
                    {
                        // do nothing
                        return;
                    }
                    break;
                case 2:     // sqm - chart3
                    sqmplotcount++;
                    if (sqmplotcount > numberofpointplots)
                    {
                        try
                        {
                            chart3.Series["SQM"].Points.RemoveAt(0);
                        }
                        catch (NullReferenceException)
                        {
                            // do nothing
                            return;
                        }
                    }
                    // add the point (temperature, time) to the chart
                    // chart3.Series["Temp"].XValueType = ChartValueType.Time;
                    try
                    {
                        chart3.Series["SQM"].Points.AddXY(myTime, datavalue);
                    }
                    catch (NullReferenceException)
                    {
                        // do nothing
                        return;
                    }
                    break;
                case 3:     // lux - chart2
                    luxplotcount++;
                    if (luxplotcount > numberofpointplots)
                    {
                        try
                        {
                            chart2.Series["Lux"].Points.RemoveAt(0);
                        }
                        catch (NullReferenceException)
                        {
                            // do nothing
                            return;
                        }
                    }
                    // add the point (temperature, time) to the chart
                    // chart2.Series["Temp"].XValueType = ChartValueType.Time;
                    try
                    {
                        chart2.Series["Lux"].Points.AddXY(myTime, datavalue);
                    }
                    catch (NullReferenceException)
                    {
                        // do nothing
                        return;
                    }
                    break;
                case 4:     // tempdiff
                    tempdiffplotcount++;
                    if (tempdiffplotcount > numberofpointplots)
                    {
                        try
                        {
                            chart1.Series["TempDiff"].Points.RemoveAt(0);
                        }
                        catch (NullReferenceException)
                        {
                            // do nothing
                            return;
                        }
                    }
                    // add the point (temperature, time) to the chart
                    // chart1.Series["Temp"].XValueType = ChartValueType.Time;
                    try
                    {
                        chart1.Series["TempDiff"].Points.AddXY(myTime, datavalue);
                    }
                    catch (NullReferenceException)
                    {
                        // do nothing
                        return;
                    }
                    break;
                case 5:     // bme280temperature
                    bme280tplotcount++;
                    if (bme280tplotcount > numberofpointplots)
                    {
                        try
                        {
                            chart4.Series["Ambient"].Points.RemoveAt(0);
                        }
                        catch (NullReferenceException)
                        {
                            // do nothing
                            return;
                        }
                    }
                    // add the point (temperature, time) to the chart
                    // chart1.Series["Temp"].XValueType = ChartValueType.Time;
                    try
                    {
                        chart4.Series["Ambient"].Points.AddXY(myTime, datavalue);
                    }
                    catch (NullReferenceException)
                    {
                        // do nothing
                        return;
                    }
                    break;
                case 6:     // bme280 humidity
                    bme280hplotcount++;
                    if (bme280hplotcount > numberofpointplots)
                    {
                        try
                        {
                            chart4.Series["Humidity"].Points.RemoveAt(0);
                        }
                        catch (NullReferenceException)
                        {
                            // do nothing
                            return;
                        }
                    }
                    // add the point (temperature, time) to the chart
                    // chart1.Series["Temp"].XValueType = ChartValueType.Time;
                    try
                    {
                        chart4.Series["Humidity"].Points.AddXY(myTime, datavalue);
                    }
                    catch (NullReferenceException)
                    {
                        // do nothing
                        return;
                    }
                    break;
                case 7:     // bme280 dewpoint
                    bme280dplotcount++;
                    if (bme280dplotcount > numberofpointplots)
                    {
                        try
                        {
                            chart4.Series["DewPoint"].Points.RemoveAt(0);
                        }
                        catch (NullReferenceException)
                        {
                            // do nothing
                            return;
                        }
                    }
                    // add the point (temperature, time) to the chart
                    // chart1.Series["Temp"].XValueType = ChartValueType.Time;
                    try
                    {
                        chart4.Series["DewPoint"].Points.AddXY(myTime, datavalue);
                    }
                    catch (NullReferenceException)
                    {
                        // do nothing
                        return;
                    }
                    break;
                case 8:     // bme280 pressure
                    bme280pplotcount++;
                    if (bme280pplotcount > numberofpointplots)
                    {
                        try
                        {
                            chart5.Series["Pressure"].Points.RemoveAt(0);
                        }
                        catch (NullReferenceException)
                        {
                            // do nothing
                            return;
                        }
                    }
                    // add the point (temperature, time) to the chart
                    // chart1.Series["Temp"].XValueType = ChartValueType.Time;
                    try
                    {
                        chart5.Series["Pressure"].Points.AddXY(myTime, (UInt32) datavalue);
                    }
                    catch (NullReferenceException)
                    {
                        // do nothing
                        return;
                    }
                    break;
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.GraphFormLocation = this.Location;
            Properties.Settings.Default.Save();
            this.Hide();
            return;
        }

        private void Graph_Load(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
            this.Location = Properties.Settings.Default.GraphFormLocation;
            Properties.Settings.Default.Save();

            tabControl.SelectedIndex = 0;                   // SQM tab
            // tabControl.SelectedTab = tabPage1;           // alternative way of selecting tab page
            numberofpointplots = 9;

            objectplotcount = 0;
            ambientplotcount = 0;
            sqmplotcount = 0;
            luxplotcount = 0;
            tempdiffplotcount = 0;
            bme280tplotcount = 0;
            bme280hplotcount = 0;
            bme280dplotcount = 0;
            bme280pplotcount = 0;
            chart1.Visible = false;
            chart2.Visible = false;
            chart3.Visible = true;
            chart4.Visible = false;
            chart5.Visible = false;

            // turn off the vertical lines in the chart
            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;    // IR sensor
            chart2.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;    // lux
            chart3.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;    // sqm
            chart4.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;    // bme280
            chart5.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;    // bme280 pressure

            // set labels on x axis - spacing between datapoints = 1 label per datapoint
            chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            chart3.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            chart4.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            chart5.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            // set label style x axis to 90 so they are vertical
            chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 90;
            chart2.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 90;
            chart3.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 90;
            chart4.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 90;
            chart5.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 90;
        }

        private void Graph_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.GraphFormLocation = this.Location;
            Properties.Settings.Default.Save();
            // do not allow form to close
            e.Cancel = true;
        }

        private void tabControl_Click(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0: // SQM
                    chart1.Visible = false;
                    chart2.Visible = false;
                    chart3.Visible = true;
                    chart4.Visible = false;
                    chart5.Visible = false;
                    break;
                case 1: // Lux
                    chart1.Visible = false;
                    chart2.Visible = true;
                    chart3.Visible = false;
                    chart4.Visible = false;
                    chart5.Visible = false;
                    break;
                case 2: // ambient and object temp
                    chart1.Visible = true;
                    chart2.Visible = false;
                    chart3.Visible = false;
                    chart4.Visible = false;
                    chart5.Visible = false;
                    break;
                case 3: // bme280 ambient, humidity, dewpoint
                    chart1.Visible = false;
                    chart2.Visible = false;
                    chart3.Visible = false;
                    chart4.Visible = true;
                    chart5.Visible = false;
                    break;
                case 4: // bme280 pressure
                    chart1.Visible = false;
                    chart2.Visible = false;
                    chart3.Visible = false;
                    chart4.Visible = false;
                    chart5.Visible = true;
                    break;
            }
        }

        private void ClearSQMDataBtn_Click(object sender, EventArgs e)
        {
            chart3.Series["SQM"].Points.Clear();
            sqmplotcount = 0;
        }

        private void ClearLuxDataPointsBtn_Click(object sender, EventArgs e)
        {
            chart2.Series["Lux"].Points.Clear();
            luxplotcount = 0;
        }

        private void ClearIRDataPointsBtn_Click(object sender, EventArgs e)
        {
            chart1.Series["ObjectTemp"].Points.Clear();
            chart1.Series["AmbientTemp"].Points.Clear();
            chart1.Series["TempDiff"].Points.Clear();
            objectplotcount = 0;
            ambientplotcount = 0;
            tempdiffplotcount = 0;
        }

        private void bme28ClearDataPointsBtn_Click(object sender, EventArgs e)
        {
            chart4.Series["Ambient"].Points.Clear();
            chart4.Series["DewPoint"].Points.Clear();
            chart4.Series["Humdity"].Points.Clear();
            bme280tplotcount = 0;
            bme280hplotcount = 0;
            bme280dplotcount = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart5.Series["Pressure"].Points.Clear(); 
            bme280pplotcount = 0;
        }
    }
}
