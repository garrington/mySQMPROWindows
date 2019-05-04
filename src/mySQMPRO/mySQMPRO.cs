using System;
using System.IO;
using System.IO.Ports;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Globalization;
using System.Threading;

// mySQMPRO Windows Application
// Copyright RB Brown, 2016-2018
// All Rights Reserved
//
// The schematic, code and ideas are released into the public domain. Users are free to implement these but 
// may NOT sell projects based on this project for commercial gain without express written permission
// granted from the author.
//
// Schematics, Code, Firmware, Ideas, Software Applications (Windows), Layout are protected by
// Copyright Law. Permission is NOT granted to any person to redistribute, market, manufacture or 
// sell for commercial gain the mySQM products, ideas, circuits, builds, variations and units as described, 
// discussed and shown.
//
// Permission is granted for personal and Academic/Educational use only.

// GUID e6e63c3f-e671-470c-be96-7c4e4151d69b

// 022 26012018
// Bug fixes for logging

// 021 22012018
// Correction to field spacing for Cloud SensorII Log file

// 020 21012018
// Fix for Sample Cloud SensorII logging format
// Fix for GPSEnabled

// 018 18012018
// Fix error in graph for Ambient Temp BME280

// 017 18012018
// BME280 pressure moved to different graph and minor fixes

// 016 04012018
// Sample Cloud SensorII logging format

// 014 05122017
// Bug fix for datalogging

// 013 04122017
// Add header line to datalog file

// 012 03122017
// Fix skystate not updating when getall
// Add skystate to data log file

// 011 02122017
// Rewrite using eng-us locale 

// 010 30112017
// Make datalogging values to csv one method instead of manual update and auto update
// Add gps fix and get all gpsdata button

// 009 30112017
// Fix errors in logging of value when Locale is not Eng US
// Fix data types not matching firmware changes

// 008 28112017
// Fix logging of value when Locale is not Eng US

// 007 13112017
// Add GPS

// 006 12112017
// Handle different windows locale settings

// 005 11112017
// Logging interval now remembered
// Log actual values rather than text box entries
// Remove redundant code
// Clean up error messages
// Rearrange data log file fields
// Disable buttons when auto-updating

// 004 07112017
// Add handling of inf for SQM returned values and nan for bme280
// Any errors in returned values and parameter is set to -1.0

/* Logfile format
0	Date
1	Time
2	SQM
3	Lux
4	IRAmbient
5	IRObject
6	Irradiance
7	Frequency
8	Firmware
9	SetPoint1
10	SetPoint2
11	RainSensor - SkyState
12	RainVoltage
13	NELM
14	bmeTemp
15	bmeHumidity
16	bmeDew
17	bmePressure
18	gpsdate
19 	gpstime
20	gpslongitude
21	gpslatitude
22	gpssatelittes
23	gpsaltitude
24  skystate
*/

namespace mySQMPRO
{
    public partial class mySQMPRO : Form
    {
        public mySQMPRO()
        {
            InitializeComponent();
            // insert code here to prevent window resizing
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            // this.MinimizeBox = false;  // do not as user cannot minimize form!!!
        }

        public string myVersion;
        public string RxString;                     // received buffer string (response) from serial port
        public string CmdString;
        public char recchar;                        // received character from serial port
        public char cmdterminator = '#';            // the character that means end of command string
        public string ComPortName;                  // hold comport name of last used comport
        public int ComPortBaudRate;
        public bool automaterunning = false;
        private string CopyrightStr = "\n© Copyright Robert Brown 2014-2018. All Rights Reserved.\n\n" +
"The schematic, code and ideas are released into the public domain. Users are free to implement these but " +
"may NOT sell projects based on this project for commercial gain without express written permission " +
"granted from the author.\n\nSchematics, Code, Firmware, Ideas, Software Applications, Layout are protected by " +
"Copyright Law. Permission is NOT granted to any person to redistribute, market, manufacture or " +
"sell for commercial gain the mySQMPRO products, ideas, circuits, builds, variations and units as described, discussed and shown. " +
"\nPermission is granted for personal use only.\n\n";
        public string ArduinoFirmwareRev;
        public int arduinoversion;
        public string firmwarefilename;
        public string Datalogfilename;              // name of data logging file
        public string Errorlogfilename;
        public string CSlogfilename;

        public logfileform logpathfrm;              // logging form to set folder/path
        public Graph GraphForm;                     // for display of temperature in real time in graph form
        public GetCloudSensorIILogFilename CloudSensorForm;
        public static Boolean CSFormActive;

        public Boolean Raining;
        public int RainVout;

        public double sqmval;
        public double lux;
        public double nelm;

        public double irradiance;
        public double frequency;

        public double IRObjectTemp;
        public double IRAmbientTemp;

        public double bme280humidity;
        public double bme280temperature;
        public double bme280dewpoint;
        public UInt32 bme280pressure;

        public Int32 setpoint1;
        public Int32 setpoint2;
        public Int32 skystate;

        public string gpsaltitude;
        public string gpssatelittes;
        public string gpsdate;
        public string gpstime;
        public string gpslatitude;
        public string gpslongitude;
        public string gpsfix;

        public CultureInfo thisCulture;
        public CultureInfo thisICCulture;
        public CultureInfo newCulture;
        public CultureInfo newICCulture;

        public String HeaderLine = "Date,Time,SQM,Lux,IRAmbient,IRObject,Irradiance,Frequency,Firmware,Setpoint1,Setpoint2,Raining,RainVolts,NELM,bmetemp,bmehumidity,bmedew,bmepressure,gpsdate,gpstime,gpslong,gpslat,gpssats,gpsalt,skystate";

        // Write data to the data logfile
        public void LogDataToFile(string DataString)
        {
            String bufferdata = DataString + Environment.NewLine;
            try
            {
                File.AppendAllText(Datalogfilename, bufferdata);
            }
            catch (Exception)
            {
                MessageBox.Show("Error writing to data logfile: ", "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Write data to the error logfile
        public void LogMessageToErrorFile(string DataString)
        {
            String bufferdata = DataString + Environment.NewLine;
            try
            {
                File.AppendAllText(Errorlogfilename, bufferdata);
            }
            catch (Exception)
            {
                MessageBox.Show("Error writing to error logfile: " + System.Environment.NewLine + DataString, "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Write data to the cloudsensor logfile
        public void LogDataToCloudSensorFile(string DataString)
        {
            if (Properties.Settings.Default.CloudSensorIILogFileFormat == true)
                LogMessageToErrorFile("LogDataToCloudSensorFile: START ================================");

            // This text is added only once to the file.
            if (!File.Exists(CSlogfilename))
            {
                // Create a file to write to.
                try
                {
                    File.WriteAllText(CSlogfilename, DataString);
                }
                catch (ArgumentException)
                {
                    if (Properties.Settings.Default.CloudSensorIILogFileFormat == true)
                        LogMessageToErrorFile("LogDataToCloudSensorFile: " + CSlogfilename + " - Does not exist. path is a zero-length string, contains only white space, or contains one or more invalid characters");
                }
                catch (PathTooLongException)
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("LogDataToCloudSensorFile: The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters");

                }
                catch (DirectoryNotFoundException)
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("LogDataToCloudSensorFile: The specified path is invalid.");

                }
                catch (IOException)
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("LogDataToCloudSensorFile: An I/O error occurred while opening the file. ");
                }
                catch (UnauthorizedAccessException)
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("LogDataToCloudSensorFile: The caller does not have the required permission.");
                }
                catch (NotSupportedException)
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("LogDataToCloudSensorFile: path is in an invalid format.");
                }
            }
            else
            {
                // file exists
                try
                {
                    File.WriteAllText(CSlogfilename, DataString);
                }
                catch (ArgumentException)
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("LogDataToCloudSensorFile: Does not exist. path is a zero-length string, contains only white space, or contains one or more invalid characters");
                }
                catch (PathTooLongException)
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("LogDataToCloudSensorFile: The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters");
                }
                catch (DirectoryNotFoundException)
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("LogDataToCloudSensorFile: The specified path is invalid.");
                }
                catch (IOException)
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("LogDataToCloudSensorFile: An I/O error occurred while opening the file. ");
                }
                catch (UnauthorizedAccessException)
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("LogDataToCloudSensorFile: The caller does not have the required permission.");
                }
                catch (NotSupportedException)
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("LogDataToCloudSensorFile: path is in an invalid format.");
                }
            }

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("LogDataToCloudSensorFile: END ================================");
        }

        // this sends the command string to the controller and waits for a response
        private string CommandString(string Command, Boolean waitforresponse)
        {
            string cmd = Command;
            string recbuf;
            string retstring = "ok";

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("CommandString: START ================================");

            recbuf = "";

            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is open");
                try
                {
                    myserialPort.Write(Command);        // send the command
                }
                catch (Exception Ex) // for serialport.write
                {
                    MessageBox.Show("Exception error writing to serial port" + "\n" + Ex, "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (waitforresponse)
                {
                    try
                    {
                        recbuf = myserialPort.ReadTo("#");
                        if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                            LogMessageToErrorFile("Received string: " + recbuf);
                        char ch = recbuf[0];
                        switch (ch)
                        {
                            case 'A':      // get SQM Reading
                                statustxtbox.Text = "SQM: " + SQMTxtBx.Text;
                                statustxtbox.Update();
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get SQM response: " + recbuf); try
                                {
                                    sqmval = Double.Parse(recbuf.Substring(1, recbuf.Length - 1), newCulture);
                                    SQMTxtBx.Text = Convert.ToString(sqmval, newCulture);
                                    SQMTxtBx.Update();
                                    if (GraphForm != null)
                                    {
                                        // GraphForm.Show();
                                        try
                                        {
                                            GraphForm.AddToChart(sqmval, 2);
                                            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                                LogMessageToErrorFile("Add SQM value to chart: " + sqmval.ToString(newCulture));
                                        }
                                        catch (ObjectDisposedException)
                                        {
                                            // object was disposed because window form was closed with X
                                            // recreate object
                                            GraphForm = new Graph();
                                            GraphForm.Show();
                                            GraphForm.AddToChart(sqmval, 2);
                                        }
                                    }
                                }
                                catch (FormatException)
                                {
                                    statustxtbox.Text = "Format exception converting SQM value";
                                    statustxtbox.Update();
                                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                        LogMessageToErrorFile("Format exception converting SQM value: " + recbuf);
                                    sqmval = -1.0;
                                    SQMTxtBx.Text = Convert.ToString(sqmval, newCulture);
                                    SQMTxtBx.Update();
                                    retstring = "nok";
                                }
                                break;

                            case 'B':     // get frequency
                                statustxtbox.Text = "Frequency: " + recbuf.Substring(1, recbuf.Length - 1) + "Hz";
                                statustxtbox.Update();
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get Frequency response: " + recbuf);                                    //update the value
                                frequencytxtbox.Text = recbuf.Substring(1, recbuf.Length - 1);
                                frequencytxtbox.Update();
                                try
                                {
                                    frequency = Double.Parse(recbuf.Substring(1, recbuf.Length - 1), newCulture);
                                    frequencytxtbox.Text = Convert.ToString(frequency, newCulture);
                                    frequencytxtbox.Update();
                                }
                                catch (FormatException)
                                {
                                    statustxtbox.Text = "Format exception converting frequency value";
                                    statustxtbox.Update();
                                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                        LogMessageToErrorFile("Format exception converting frequency value: " + recbuf);
                                    frequency = 1.0;
                                    frequencytxtbox.Text = Convert.ToString(frequency, newCulture);
                                    frequencytxtbox.Update();
                                    retstring = "nok";
                                }
                                break;
                            case 'C':     // get irradiance
                                statustxtbox.Text = "Irradiance: " + recbuf.Substring(1, recbuf.Length - 1) + "Hz";
                                statustxtbox.Update();
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get irradiance response: " + recbuf);
                                try
                                {
                                    irradiance = Double.Parse(recbuf.Substring(1, recbuf.Length - 1), newCulture);
                                    irradiancetxtbox.Text = Convert.ToString(irradiance, newCulture);
                                    irradiancetxtbox.Update();
                                }
                                catch (FormatException)
                                {
                                    statustxtbox.Text = "Format exception converting irradiance value";
                                    statustxtbox.Update();
                                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                        LogMessageToErrorFile("Format exception converting irradiance value: " + recbuf);
                                    irradiance = 1.0;
                                    irradiancetxtbox.Text = Convert.ToString(irradiance, newCulture);
                                    irradiancetxtbox.Update();
                                    retstring = "nok";
                                }
                                break;
                            case 'D':     // get firmware version
                                // display in textbox
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get firmware response: " + recbuf);
                                ArduinoFirmwareRev = recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Text = "Controller version: " + recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Update();
                                firmwaretxtbox.Text = recbuf.Substring(1, recbuf.Length - 1);
                                firmwaretxtbox.Update();
                                break;
                            case 'E':     // get firmware filename
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get firmware filename response: " + recbuf);
                                firmwarefilename = recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Text = "Controller Filename: " + recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Update();
                                break;
                            case 'H':     // get date
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get date response: " + recbuf);
                                gpsdate = recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Text = gpsdate;
                                statustxtbox.Update();
                                datetxtbox.Text = gpsdate;
                                datetxtbox.Update();
                                break;
                            case 'I': // get time
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get time response: " + recbuf);
                                gpstime = recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Text = gpstime;
                                statustxtbox.Update();
                                timetxtbox.Text = gpstime;
                                timetxtbox.Update();
                                break;
                            case 'J':     // get longitude
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get longitude response: " + recbuf);
                                gpslongitude = recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Text = "Longitude: " + gpslongitude;
                                statustxtbox.Update();
                                longitudetxtbox.Text = gpslongitude;
                                longitudetxtbox.Update();
                                break;
                            case 'K':     // get latitude
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get latitude response: " + recbuf);
                                gpslatitude = recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Text = "Latitude: " + gpslatitude;
                                statustxtbox.Update();
                                latitudetxtbox.Text = gpslatitude;
                                latitudetxtbox.Update();
                                break;
                            case 'L':     // get altitude
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get altitude response: " + recbuf);
                                gpsaltitude = recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Text = "Altitude: " + gpsaltitude;
                                statustxtbox.Update();
                                altitudetxtbox.Text = gpsaltitude;
                                altitudetxtbox.Update();
                                break;
                            case 'M':     // get satelittes
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get satelittes response: " + recbuf);
                                gpssatelittes = recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Text = "Satelittes: " + gpssatelittes;
                                statustxtbox.Update();
                                satelittestxtbox.Text = gpssatelittes;
                                satelittestxtbox.Update();
                                break;
                            case 'N':     // get gps fix
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get gpsfix response: " + recbuf);
                                gpsfix = recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Text = "gps fix: " + gpsfix;
                                statustxtbox.Update();
                                if (gpsfix == "0")
                                {
                                    gpsFixChkBox.Checked = false;
                                }
                                else
                                {
                                    gpsFixChkBox.Checked = true;
                                }
                                break;
                            case 'R':     // get raining
                                statustxtbox.Text = "Raining = " + recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Update();
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get raining response: " + recbuf);
                                String tp = recbuf.Substring(1, recbuf.Length - 1);
                                if (tp == "1")
                                {
                                    RainSensorTxtBox.Text = "RAINING";
                                    Raining = true;
                                }
                                else
                                {
                                    RainSensorTxtBox.Text = "Not Raining";
                                    Raining = false;
                                }
                                RainSensorTxtBox.Update();
                                break;
                            case 'S':     // get raining Vout
                                statustxtbox.Text = "Raining Vout = " + recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Update();
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get raining Vout response: " + recbuf);
                                try
                                {
                                    RainVout = Convert.ToInt32(recbuf.Substring(1, recbuf.Length - 1));
                                    RainVoltageTxtBox.Text = RainVout.ToString();
                                    RainVoltageTxtBox.Update();
                                }
                                catch (FormatException)
                                {
                                    statustxtbox.Text = "Error converting value for Rain Vout";
                                    statustxtbox.Update();
                                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                        LogMessageToErrorFile("Invalid Rain Vout value: " + recbuf);
                                    retstring = "nok";
                                }
                                break;
                            case 'O':     // get IR Sensor Object Temp
                                statustxtbox.Text = "IR Object Temp = " + recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Update();
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get IR Object temperature response: " + recbuf);
                                try
                                {
                                    double TD1 = Double.Parse(recbuf.Substring(1, recbuf.Length - 1), newCulture);
                                    IRObjectTemp = Math.Round(TD1, 2);
                                    ObjectTempTxtBox.Text = Convert.ToString(IRObjectTemp, newCulture);
                                    ObjectTempTxtBox.Update();
                                    double tdiff = IRAmbientTemp - IRObjectTemp;
                                    if (GraphForm != null)
                                    {
                                        // GraphForm.Show();
                                        try
                                        {
                                            GraphForm.AddToChart(IRObjectTemp, 0);
                                            GraphForm.AddToChart(tdiff, 4);
                                        }
                                        catch (ObjectDisposedException)
                                        {
                                            // object was disposed because window form was closed with X
                                            // recreate object
                                            GraphForm = new Graph();
                                            GraphForm.Show();
                                            GraphForm.AddToChart(IRObjectTemp, 0);
                                            GraphForm.AddToChart(tdiff, 4);
                                        }
                                    }
                                }
                                catch (FormatException)
                                {
                                    statustxtbox.Text = "Error converting value for IR Object temperature";
                                    statustxtbox.Update();
                                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                        LogMessageToErrorFile("Invalid IR Object temperature: " + recbuf);
                                    IRObjectTemp = -1.0;
                                    ObjectTempTxtBox.Text = Convert.ToString(IRObjectTemp, newCulture);
                                    ObjectTempTxtBox.Update();
                                    retstring = "nok";
                                }
                                break;
                            case 'P':     // get IR Sensor Ambient Temp
                                statustxtbox.Text = "IR Ambient Temp = " + recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Update();
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get IR Ambient temperature response: " + recbuf);
                                try
                                {
                                    double TD1 = Double.Parse(recbuf.Substring(1, recbuf.Length - 1), newCulture);
                                    IRAmbientTemp = Math.Round(TD1, 2);
                                    AmbientTempTxtBox.Text = Convert.ToString(IRAmbientTemp, newCulture);
                                    AmbientTempTxtBox.Update();
                                    if (GraphForm != null)
                                    {
                                        // GraphForm.Show();
                                        try
                                        {
                                            GraphForm.AddToChart(IRAmbientTemp, 1);
                                        }
                                        catch (ObjectDisposedException)
                                        {
                                            // object was disposed because window form was closed with X
                                            // recreate object
                                            GraphForm = new Graph();
                                            GraphForm.Show();
                                            GraphForm.AddToChart(IRAmbientTemp, 1);
                                        }
                                    }
                                }
                                catch (FormatException)
                                {
                                    statustxtbox.Text = "Error converting value for IR Ambient temperature";
                                    statustxtbox.Update();
                                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                        LogMessageToErrorFile("Invalid IR ambient temperature: " + recbuf);
                                    IRAmbientTemp = -1.0;
                                    AmbientTempTxtBox.Text = Convert.ToString(IRAmbientTemp, newCulture);
                                    AmbientTempTxtBox.Update();
                                    retstring = "nok";
                                }
                                break;
                            case 'W':   // get setpoint1
                                statustxtbox.Text = "setpoint1 = " + recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Update();
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get setpoint1 response: " + recbuf);
                                try
                                {
                                    setpoint1 = Convert.ToInt32(recbuf.Substring(1, recbuf.Length - 1));
                                    SetPoint1TxtBox.Text = setpoint1.ToString();
                                    SetPoint1TxtBox.Update();
                                }
                                catch (FormatException)
                                {
                                    statustxtbox.Text = "Error converting value for setpoint1";
                                    statustxtbox.Update();
                                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                        LogMessageToErrorFile("Invalid setpoint1 value: " + recbuf);
                                    retstring = "nok";
                                }
                                break;
                            case 'X':   // get setpoint2
                                statustxtbox.Text = "setpoint2 = " + recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Update();
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get setpoint2 response: " + recbuf);
                                try
                                {
                                    setpoint2 = Convert.ToInt32(recbuf.Substring(1, recbuf.Length - 1));
                                    SetPoint2TxtBox.Text = setpoint2.ToString();
                                    SetPoint2TxtBox.Update();
                                }
                                catch (FormatException)
                                {
                                    statustxtbox.Text = "Error converting value for setpoint2";
                                    statustxtbox.Update();
                                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                        LogMessageToErrorFile("Invalid setpoint2 value: " + recbuf);
                                    retstring = "nok";
                                }
                                break;
                            case 'U':   // get Lux
                                statustxtbox.Text = "Lux = " + recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Update();
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get lux response: " + recbuf);
                                try
                                {
                                    lux = Double.Parse(recbuf.Substring(1, recbuf.Length - 1), newCulture);
                                    luxTxtBox.Text = lux.ToString(newCulture);
                                    luxTxtBox.Update();
                                    if (GraphForm != null)
                                    {
                                        // GraphForm.Show();
                                        try
                                        {
                                            GraphForm.AddToChart(lux, 3);
                                        }
                                        catch (ObjectDisposedException)
                                        {
                                            // object was disposed because window form was closed with X
                                            // recreate object
                                            GraphForm = new Graph();
                                            GraphForm.Show();
                                            GraphForm.AddToChart(lux, 3);
                                        }
                                    }
                                }
                                catch (FormatException)
                                {
                                    statustxtbox.Text = "Error converting value for lux";
                                    statustxtbox.Update();
                                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                        LogMessageToErrorFile("Invalid Lux value: " + recbuf);
                                    lux = 1.0;
                                    luxTxtBox.Text = lux.ToString(newCulture);
                                    luxTxtBox.Update();
                                    retstring = "nok";
                                }
                                break;
                            case 'V':     // get skystate, 0=clear, 1=partly cloudy, 2=cloudy
                                statustxtbox.Text = "Sky state = " + recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Update();
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get sky-state response: " + recbuf);
                                try
                                {
                                    skystate = Convert.ToInt32(recbuf.Substring(1, recbuf.Length - 1));
                                    switch (skystate)
                                    {
                                        case 0:
                                            SkyConditionLabel.Text = "CLEAR";
                                            SkyConditionLabel.Update();
                                            break;
                                        case 1:
                                            SkyConditionLabel.Text = "PARTLY CLOUDY";
                                            SkyConditionLabel.Update(); break;
                                        case 2:
                                            SkyConditionLabel.Text = "CLOUDY";
                                            SkyConditionLabel.Update(); break;
                                        default:
                                            SkyConditionLabel.Text = "UNKNOWN";
                                            SkyConditionLabel.Update();
                                            break;
                                    }
                                    statustxtbox.Text = "Skystate return value = " + skystate.ToString();
                                    statustxtbox.Update();
                                }
                                catch (FormatException)
                                {
                                    statustxtbox.Text = "Error converting value for sky state";
                                    statustxtbox.Update();
                                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                        LogMessageToErrorFile("Invalid sky state: " + recbuf);
                                    retstring = "nok";
                                }
                                break;
                            case 'Z':     // get Nelm
                                statustxtbox.Text = "NELM: " + recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Update();
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get NELM response: " + recbuf);
                                try
                                {
                                    nelm = Double.Parse(recbuf.Substring(1, recbuf.Length - 1), newCulture);
                                    NELMtxtbox.Text = Convert.ToString(nelm, newCulture);
                                    NELMtxtbox.Update();
                                }
                                catch (FormatException)
                                {
                                    statustxtbox.Text = "Error converting value for nelm";
                                    statustxtbox.Update();
                                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                        LogMessageToErrorFile("Invalid NELM value: " + recbuf);
                                    nelm = -1.0;
                                    NELMtxtbox.Text = Convert.ToString(nelm, newCulture);
                                    NELMtxtbox.Update();
                                    retstring = "nok";
                                }
                                break;
                            case 'a':     // get bme280 humidity
                                statustxtbox.Text = "BME280 Humidity: " + recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Update();
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get bme280 Humidity response: " + recbuf);
                                try
                                {
                                    bme280humidity = Double.Parse(recbuf.Substring(1, recbuf.Length - 1), newCulture);
                                    bme280humiditytxtbox.Text = Convert.ToString(bme280humidity, newCulture);
                                    bme280humiditytxtbox.Update();
                                    if (GraphForm != null)
                                    {
                                        // GraphForm.Show();
                                        try
                                        {
                                            GraphForm.AddToChart(bme280humidity, 6);
                                        }
                                        catch (ObjectDisposedException)
                                        {
                                            // object was disposed because window form was closed with X
                                            // recreate object
                                            GraphForm = new Graph();
                                            GraphForm.Show();
                                            GraphForm.AddToChart(bme280humidity, 6);
                                        }
                                    }
                                }
                                catch (FormatException)
                                {
                                    statustxtbox.Text = "Error converting value for bme280 humidity";
                                    statustxtbox.Update();
                                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                        LogMessageToErrorFile("Invalid bme280 humidity value: " + recbuf);
                                    bme280humidity = -1.0;
                                    bme280humiditytxtbox.Text = Convert.ToString(bme280humidity, newCulture);
                                    bme280humiditytxtbox.Update();
                                    retstring = "nok";
                                }
                                break;
                            case 'e':     // get bme280 pressure in hPa
                                string rawvalue = "BME280 pressure: " + recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Text = rawvalue;
                                statustxtbox.Update();
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("Get bme280 pressure response: " + rawvalue);
                                try
                                {
                                    bme280pressure = Convert.ToUInt32(recbuf.Substring(1, recbuf.Length - 1), newCulture);
                                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                        LogMessageToErrorFile("bme280 pressure parsed value: " + bme280pressure);
                                    bme280pressuretxtbox.Text = Convert.ToString(bme280pressure, newCulture);
                                    bme280pressuretxtbox.Update();
                                    if (GraphForm != null)
                                    {
                                        // GraphForm.Show();
                                        try
                                        {
                                            GraphForm.AddToChart((double)bme280pressure, 8);
                                        }
                                        catch (ObjectDisposedException)
                                        {
                                            // object was disposed because window form was closed with X
                                            // recreate object
                                            GraphForm = new Graph();
                                            GraphForm.Show();
                                            GraphForm.AddToChart((double)bme280pressure, 8);
                                        }
                                    }
                                }
                                catch (FormatException)
                                {
                                    statustxtbox.Text = "Error converting value for bme280 pressure";
                                    statustxtbox.Update();
                                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                        LogMessageToErrorFile("Invalid bme280 pressure value: " + recbuf);
                                    bme280pressure = 1000;
                                    bme280pressuretxtbox.Text = Convert.ToString(bme280pressure, newCulture);
                                    bme280pressuretxtbox.Update();
                                    retstring = "nok";
                                }
                                break;
                            case 'c':     // get bme280 ambient temperature
                                statustxtbox.Text = "BME280 Ambient Temp: " + recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Update();
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("BME280 Ambient temperature response: " + recbuf); try
                                {
                                    bme280temperature = Double.Parse(recbuf.Substring(1, recbuf.Length - 1), newCulture);
                                    bme280temptxtbox.Text = Convert.ToString(bme280temperature, newCulture);
                                    bme280temptxtbox.Update();
                                    if (GraphForm != null)
                                    {
                                        // GraphForm.Show();
                                        try
                                        {
                                            GraphForm.AddToChart(bme280temperature, 5);
                                        }
                                        catch (ObjectDisposedException)
                                        {
                                            // object was disposed because window form was closed with X
                                            // recreate object
                                            GraphForm = new Graph();
                                            GraphForm.Show();
                                            GraphForm.AddToChart(bme280temperature, 5);
                                        }
                                    }
                                }
                                catch (FormatException)
                                {
                                    statustxtbox.Text = "Error converting value for bme280 temperature";
                                    statustxtbox.Update();
                                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                        LogMessageToErrorFile("Invalid bme280 temperature value: " + recbuf);
                                    bme280temperature = -1.0;
                                    bme280temptxtbox.Text = Convert.ToString(bme280temperature, newCulture);
                                    bme280temptxtbox.Update();
                                    retstring = "nok";
                                }
                                break;
                            case 'd':     // get bme280 dewpoint
                                statustxtbox.Text = "BME280 Dewpoint: " + recbuf.Substring(1, recbuf.Length - 1);
                                statustxtbox.Update();
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                    LogMessageToErrorFile("BME280 Dewpoint response: " + recbuf); try
                                {
                                    bme280dewpoint = Double.Parse(recbuf.Substring(1, recbuf.Length - 1), newCulture);
                                    bme280dewpointtxtbox.Text = Convert.ToString(bme280dewpoint, newCulture);
                                    bme280dewpointtxtbox.Update();
                                    if (GraphForm != null)
                                    {
                                        // GraphForm.Show();
                                        try
                                        {
                                            GraphForm.AddToChart(bme280dewpoint, 7);
                                        }
                                        catch (ObjectDisposedException)
                                        {
                                            // object was disposed because window form was closed with X
                                            // recreate object
                                            GraphForm = new Graph();
                                            GraphForm.Show();
                                            GraphForm.AddToChart(bme280dewpoint, 7);
                                        }
                                    }
                                }
                                catch (FormatException)
                                {
                                    statustxtbox.Text = "Error converting value for bme280 dewpoint";
                                    statustxtbox.Update();
                                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                        LogMessageToErrorFile("Invalid bme280 dewpoint value: " + recbuf);
                                    bme280dewpoint = -1.0;
                                    bme280dewpointtxtbox.Text = Convert.ToString(bme280dewpoint, newCulture);
                                    bme280dewpointtxtbox.Update();
                                    retstring = "nok";
                                }
                                break;

                            //additional case statements for commands here

                            default:
                                {
                                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                        LogMessageToErrorFile("Unknown response: " + recbuf);
                                    retstring = "nok";
                                }
                                break;
                        }
                        // end of switch
                    }
                    catch (TimeoutException Ex)
                    {
                        retstring = "";
                        MessageBox.Show("Timeout exception reading controller response" + "\n" + Ex, "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception Ex)
                    {
                        retstring = "";
                        MessageBox.Show("Exception reading controller response" + "\n" + Ex, "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // no response needed as its a SET
                    // do nothing
                }
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("CommandString: END ================================");
            return retstring;
        }

        public void PauseForTime(int myseconds, int mymseconds)
        {
            // this is a generic wait that works with all versions of .NET
            System.DateTime ThisMoment = System.DateTime.Now;
            System.TimeSpan duration = new System.TimeSpan(0, 0, 0, myseconds, mymseconds);
            // System.TimeSpan( days, hrs, mins, secs, millisecs);
            System.DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = System.DateTime.Now;
            }
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("ExitBtn: START ================================");
            if (myserialPort.IsOpen)    // com port is open, so tidy up
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is open");
                MessageBox.Show("Disconnect the Serial Port before exiting", "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else                    // Port is closed, save settings and exit
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is not open");
                intervaltimer1.Enabled = false;
                Properties.Settings.Default.Save();
                // all tidy up is done in form_closing
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("ExitBtn: END ================================");
                this.Close();
            }
        }

        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            string tcmd;
            string ret;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
            {
                LogMessageToErrorFile("ConnectBtn: START ================================");
                LogMessageToErrorFile("Application version = " + myVersion);
            }

            statustxtbox.Text = "Checking if Com Port has been selected";
            statustxtbox.Update();
            // check that a comport is selected
            if (this.comboBox1.SelectedItem != null)
            {
                statustxtbox.Text = "Com Port has been selected";
                statustxtbox.Update();
                // if the comport is not open
                if (!myserialPort.IsOpen)
                {
                    statustxtbox.Text = "Com Port is not yet Opened";
                    statustxtbox.Update();
                    // update comport name with selected item and save it
                    ComPortName = comboBox1.GetItemText(comboBox1.SelectedItem);
                    Properties.Settings.Default.ComPortString = ComPortName;
                    Properties.Settings.Default.Save();

                    // MessageBox.Show("Comportname = " + ComPortName, "mySQMPRO", MessageBoxButtons.OK);
                    automatechkbox.Enabled = true;

                    myserialPort.PortName = comboBox1.GetItemText(comboBox1.SelectedItem);
                    myserialPort.BaudRate = Properties.Settings.Default.ComPortSpeed;
                    myserialPort.Parity = Parity.None;
                    myserialPort.StopBits = StopBits.One;
                    myserialPort.DataBits = 8;
                    myserialPort.DtrEnable = true;
                    myserialPort.RtsEnable = true;
                    myserialPort.WriteTimeout = 5000;
                    myserialPort.ReadTimeout = 20000;

                    // attempt to open com port
                    try
                    {
                        statustxtbox.Text = "Attempting to Open Com Port";
                        statustxtbox.Update();
                        myserialPort.Open();
                        statustxtbox.Text = myserialPort.PortName + " opened.";
                        statustxtbox.Update();

                        if (myserialPort.IsOpen)
                        {
                            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                LogMessageToErrorFile("Serial port is open");
                            statustxtbox.Text = "Connecting...." + myserialPort.PortName + "\nPlease wait....";
                            statustxtbox.Update();
                            myserialPort.DiscardInBuffer();
                        }
                        else
                        {
                            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                LogMessageToErrorFile("Serial port is not open");
                            statustxtbox.Text = "Connect Error: Comport=NOT connected";
                            statustxtbox.Update();
                            myserialPort.Close();
                        }
                    }
                    catch (UnauthorizedAccessException Ex)
                    {
                        String tmpstr = Ex.ToString();
                        MessageBox.Show("Connect: Access to the serial port denied: " + ComPortName, "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        myserialPort.Close();
                        statustxtbox.Text = "Connect: Access to the serial port denied:";
                        statustxtbox.Update();
                        return;
                    }
                    catch (ArgumentOutOfRangeException Ex)
                    {
                        String tmpstr = Ex.ToString();
                        MessageBox.Show("Connect: One or moore properties of serial port is invalid: " + ComPortName, "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        myserialPort.Close();
                        statustxtbox.Text = "Connect: One or more properties of serial port is invalid";
                        statustxtbox.Update();
                        return;
                    }
                    catch (ArgumentException Ex)
                    {
                        String tmpstr = Ex.ToString();
                        MessageBox.Show("Connect:  Port name does not begin with COM: " + ComPortName, "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        myserialPort.Close();
                        statustxtbox.Text = "Connect: Port name does not begin with COM";
                        statustxtbox.Update();
                        return;
                    }
                    catch (IOException Ex)
                    {
                        String tmpstr = Ex.ToString();
                        MessageBox.Show("Comport: IOException: " + ComPortName + "Port is in an invalid state or parameter is wrong", "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        myserialPort.Close();
                        statustxtbox.Text = "IOException: Port is in an invalid state";
                        statustxtbox.Update();
                        return;
                    }
                    catch (InvalidOperationException Ex)
                    {
                        String tmpstr = Ex.ToString();
                        MessageBox.Show("Comport: Comport already open:" + ComPortName, "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        myserialPort.Close();
                        statustxtbox.Text = "Exception: Com port was already open: Com Port now closed";
                        statustxtbox.Update();
                        return;
                    }
                    catch (Exception Ex)
                    {
                        String tmpstr = Ex.ToString();
                        MessageBox.Show("Comport Exception Error: Unspecified:" + ComPortName, "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        myserialPort.Close();
                        statustxtbox.Text = "Exception: Com port has been closed";
                        statustxtbox.Update();
                        return;
                    }

                    if (myserialPort.IsOpen)
                    {
                        if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                            LogMessageToErrorFile("Serial port is open");
                        statustxtbox.Text = myserialPort.PortName + " opened.";
                        statustxtbox.Update();

                        // wait forcontroller to start up
                        PauseForTime(3, 0); // wait specified seconds - give arduino time to reset

                        // we have connected, so GET all the default parameters to the controller
                        try
                        {
                            // get version number of controller
                            statustxtbox.Text = "Getting firmware version";
                            statustxtbox.Update();
                            tcmd = ":04#";
                            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                LogMessageToErrorFile("Calling command string with " + tcmd);
                            ret = CommandString(tcmd, true);
                            if (ret == "")
                            {
                                // beep to indicate connection made
                                SystemSounds.Exclamation.Play();
                                MessageBox.Show("Controller did not respond to request for firmware version.\nPort has been closed.\n\nCheck that the controller is connected via USB and that the right ComPort is being used.", "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                statustxtbox.Text = "No response to Get firmware version: disconnecting port now.";
                                statustxtbox.Update();
                                myserialPort.Close();
                                return;
                            }
                            // get firmware string of controller
                            statustxtbox.Text = "Getting firmware string";
                            statustxtbox.Update();
                            tcmd = ":05#";
                            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                LogMessageToErrorFile("Calling command string with " + tcmd);
                            ret = CommandString(tcmd, true);
                            if (ret == "")
                            {
                                // beep to indicate connection made
                                SystemSounds.Exclamation.Play();
                                MessageBox.Show("Controller did not respond to request for firmware string.\nPort has been closed.\n\nCheck that the controller is connected via USB and that the right ComPort is being used.", "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                statustxtbox.Text = "No response to Get firmware string: disconnecting port now.";
                                statustxtbox.Update();
                                myserialPort.Close();
                                return;
                            }

                            // beep to indicate connection made
                            SystemSounds.Beep.Play();
                            // disable controls that need connection to controller
                            DisconnectBtn.Enabled = true;
                            ConnectBtn.Enabled = false;
                            RefreshComPortBtn.Enabled = false;
                            getfirmwareBtn.Enabled = true;
                            getfrequencyBtn.Enabled = true;
                            getirradianceBtn.Enabled = true;
                            getsqmBtn.Enabled = true;
                            automategrpbox.Enabled = true;
                            automatechkbox.Enabled = true;
                            forceExitMenuItem.Enabled = true;
                            gpsGroupBox.Enabled = true;

                            // disable the checkboxes used for logging
                            // LogErrorsChkBox.Enabled = true;
                            LogValuesChkBox.Enabled = true;
                            IRSensorGrpBox.Enabled = true;
                            getsetpointsBtn.Enabled = true;
                            setpoint1Btn.Enabled = true;
                            setpoint2Btn.Enabled = true;
                            RainSensorGrpBox.Enabled = true;
                            bme280grpBox.Enabled = true;
                            getallBtn.Enabled = true;
                            getLuxBtn.Enabled = true;
                            GetNELMBtn.Enabled = true;
                            getGPSDataBtn.Enabled = true;

                            try
                            {
                                arduinoversion = Convert.ToInt32(ArduinoFirmwareRev);
                                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                {
                                    LogMessageToErrorFile("Arduino Controller Firmware: " + ArduinoFirmwareRev);
                                }

                                // SHOULD VALIDATE SETPOINT1 AND SETPOINT2 AS USER MAY NOT HAVE PRESSED ENTER KEY
                                if (validatesetpoints() == true)
                                {                        // send setpoint1
                                    tcmd = ":25";       // set setpoint1
                                    tcmd = tcmd + setpoint1.ToString() + "#";
                                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                        LogMessageToErrorFile("Calling command string with " + tcmd);
                                    statustxtbox.Text = "Sending setpoint1: " + setpoint1.ToString();
                                    statustxtbox.Update();
                                    CommandString(tcmd, false);

                                    // send setpoint2
                                    statustxtbox.Text = "Send setpoint2";
                                    statustxtbox.Update();
                                    tcmd = ":26" + setpoint2.ToString() + "#";
                                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                        LogMessageToErrorFile("Calling command string with " + tcmd);
                                    CommandString(tcmd, false);
                                }
                                PauseForTime(2, 0);
                                statustxtbox.Text = "Connected";
                                statustxtbox.Update();
                                if (Properties.Settings.Default.RainingAlert == true)
                                {
                                    RainTimer.Enabled = true;
                                    RainTimer.Start();
                                }
                                else
                                {
                                    RainTimer.Stop();
                                    RainTimer.Enabled = false;
                                }
                            }
                            catch (FormatException Ex)
                            {
                                // statustxtbox.Text = "Invalid conversion of Firmware version: " + ArduinoFirmwareRev;
                                // statustxtbox.Update();
                                // assume this version does not support NMEA mode
                                String tmpstr = Ex.ToString();
                            }
                        }
                        catch (TimeoutException Ex)
                        {
                            String tmpstr = Ex.ToString();
                            MessageBox.Show("Timeout Exception: No response from controller\nWrong COM Port?\n\nCOM Port has been disconnected.", "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            statustxtbox.Text = "Error: Serial Port timeout";
                            statustxtbox.Update();
                            myserialPort.Close();
                            return;
                        }
                        catch (Exception Ex)
                        {
                            String tmpstr = Ex.ToString();
                            MessageBox.Show("Exception occurred sending command to controller. Possible wrong port or controller OFF or disconnected. \n\nCOM Port has been disconnected.", "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            statustxtbox.Text = "Error: Serial Port Exception";
                            statustxtbox.Update();
                            myserialPort.Close();
                            return;
                        }
                    }
                }
                else
                {
                    statustxtbox.Text = myserialPort.PortName + " already open.";
                    statustxtbox.Update();
                }
            }
            else
            {
                statustxtbox.Clear();
                statustxtbox.Text = "ERR: No COM Port selected or there may be no port available.";
                statustxtbox.Update();
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("ConnectBtn: END ================================");
        }

        private void clearalltextboxes()
        {
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("clearalltextboxes: START ================================");
            statustxtbox.Clear();
            statustxtbox.Update();
            irradiancetxtbox.Text = "";
            irradiancetxtbox.Update();
            frequencytxtbox.Text = "";
            frequencytxtbox.Update();
            firmwaretxtbox.Text = "";
            firmwaretxtbox.Update();
            SQMTxtBx.Text = "";
            SQMTxtBx.Update();
            NELMtxtbox.Text = "";
            NELMtxtbox.Update();
            RainSensorTxtBox.Text = "";
            RainSensorTxtBox.Update();
            RainVoltageTxtBox.Text = "";
            RainVoltageTxtBox.Update();
            luxTxtBox.Text = "";
            luxTxtBox.Update();
            bme280dewpointtxtbox.Text = "";
            bme280dewpointtxtbox.Update();
            bme280humiditytxtbox.Text = "";
            bme280humiditytxtbox.Update();
            bme280pressuretxtbox.Text = "";
            bme280pressuretxtbox.Update();
            bme280temptxtbox.Text = "";
            bme280temptxtbox.Update();
            datetxtbox.Text = "";
            datetxtbox.Update();
            timetxtbox.Text = "";
            timetxtbox.Update();
            latitudetxtbox.Text = "";
            latitudetxtbox.Update();
            longitudetxtbox.Text = "";
            longitudetxtbox.Update();
            satelittestxtbox.Text = "";
            satelittestxtbox.Update();
            altitudetxtbox.Text = "";
            altitudetxtbox.Update();
            ObjectTempTxtBox.Text = "";
            ObjectTempTxtBox.Update();
            AmbientTempTxtBox.Text = "";
            AmbientTempTxtBox.Update();
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("clearalltextboxes: END ================================");
        }

        private void DisconnectBtn_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
            {
                LogMessageToErrorFile("DisconnectBtn: START ================================");
                LogMessageToErrorFile("Application version = " + myVersion);
            }

            statustxtbox.Clear();
            statustxtbox.Update();

            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is open");
                intervaltimer1.Enabled = false;
                automatechkbox.Checked = false;
                automatechkbox.Enabled = false;
                automatechkbox.Update();
                RainTimer.Enabled = false;
                myserialPort.Close();
                Properties.Settings.Default.Save();
                statustxtbox.Text = myserialPort.PortName + " closed.";
                statustxtbox.Update();
                ArduinoFirmwareRev = "Not connected.";

                // disable controls that need connection to controller
                DisconnectBtn.Enabled = false;
                ConnectBtn.Enabled = true;
                RefreshComPortBtn.Enabled = true;
                getfirmwareBtn.Enabled = false;
                getfrequencyBtn.Enabled = false;
                getirradianceBtn.Enabled = false;
                getsqmBtn.Enabled = false;
                automategrpbox.Enabled = false;
                automatechkbox.Enabled = false;
                forceExitMenuItem.Enabled = true;

                // disable the checkboxes used for logging
                // LogErrorsChkBox.Enabled = false;
                LogValuesChkBox.Enabled = false;
                clearalltextboxes();

                IRSensorGrpBox.Enabled = false;
                getsetpointsBtn.Enabled = false;
                setpoint1Btn.Enabled = false;
                setpoint2Btn.Enabled = false;
                RainSensorGrpBox.Enabled = false;
                bme280grpBox.Enabled = false;
                getallBtn.Enabled = false;
                getLuxBtn.Enabled = false;
                GetNELMBtn.Enabled = false;
                getGPSDataBtn.Enabled = false;
            }
            else
            {
                clearalltextboxes();
                // need to stop automate if running
                intervaltimer1.Enabled = false;
                statustxtbox.Text = "Com port is not connected.";
                statustxtbox.Update();
                RainTimer.Enabled = false;
                // ensure that we are disconnected
                DisconnectBtn.Enabled = false;
                ConnectBtn.Enabled = true;
                DisconnectBtn.Update();
                ConnectBtn.Update();
                RefreshComPortBtn.Enabled = true;
                getfirmwareBtn.Enabled = false;
                getfrequencyBtn.Enabled = false;
                getirradianceBtn.Enabled = false;
                getsqmBtn.Enabled = false;
                automategrpbox.Enabled = false;
                automatechkbox.Enabled = false;
                forceExitMenuItem.Enabled = true;

                // disable the checkboxes used for logging
                // LogErrorsChkBox.Enabled = false;
                LogValuesChkBox.Enabled = false;
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("DisconnectBtn: END ================================");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("exitToolStripMenuItem: START ================================");
            if (myserialPort.IsOpen)    // com port is open, so tidy up
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is open");
                MessageBox.Show("Disconnect the Serial Port before exiting", "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else                    // Port is closed, save settings and exit
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is not open");
                Properties.Settings.Default.Save();
                // all tidy up is done in form_closing
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("exitToolStripMenuItem: END ================================");
                this.Close();
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("exitToolStripMenuItem: END ================================");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("aboutToolStripMenuItem: START ================================");
            MessageBox.Show("mySQMPRO Application\nVersion=" + myVersion + CopyrightStr + "Firmware Version: " + ArduinoFirmwareRev, "mySQMPRO", MessageBoxButtons.OK);
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("aboutToolStripMenuItem: END ================================");
        }

        private void mySQMGPS_Load(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
            this.Location = Properties.Settings.Default.FormLocation;

            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                myVersion = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                this.Text = "mySQMPRO © R Brown 2018: " + myVersion;
            }
            else
            {
                myVersion = "mySQMPRO © R Brown 2018";
                this.Text = myVersion;
            }

            arduinoversion = 100;

            // get the path name for datalogfile and errorlogfile
            // all log files are in the same path
            // check to see if app setting for "LogPathName"
            logpathfrm = new logfileform();
            CloudSensorForm = new GetCloudSensorIILogFilename();

            // save threads culture
            newCulture = thisCulture = CultureInfo.CurrentCulture;
            newICCulture = thisICCulture = CultureInfo.CurrentUICulture;
            newCulture = CultureInfo.CreateSpecificCulture("en-US");
            newICCulture = CultureInfo.CreateSpecificCulture("en-US");

            String PathStr = Properties.Settings.Default.LogPathName;

            if (PathStr == "")
            {
                // its not set so activate form logfileform and get foldername
                // and save back into application setting
                logpathfrm.ShowDialog();
            }
            // in case it has changed, redo PathStr
            Properties.Settings.Default.Reload();
            PathStr = Properties.Settings.Default.LogPathName;

            // make up data logfilename
            String DateNow = DateTime.Now.ToString("ddMMyy-HHmmss");
            Datalogfilename = "mySQMPRO-DataLog-" + DateNow + ".csv";

            // make up error logfilename
            DateNow = DateTime.Now.ToString("ddMMyy-HHmmss");
            Errorlogfilename = "mySQMPRO-ErrorLog-" + DateNow + ".txt";
            CSlogfilename = Properties.Settings.Default.CloudSensorIILogFilename;
            // CSlogfilename = "mydatafile.txt";

            // remove any extension and then add extension .txt
            if (CSlogfilename.IndexOf('.') > 0)
            {
                CSlogfilename = CSlogfilename.Substring(0, CSlogfilename.IndexOf('.')) + ".txt";
            }
            else
                CSlogfilename = CSlogfilename + ".txt";

            // now work out absolute paths and save back into settings
            // get full pathname for datalog
            int pathlen = PathStr.Length;
            if (pathlen == 3)       // "D:\\" or "C:\\" - root directory
            {
                Datalogfilename = PathStr + Datalogfilename;
                Errorlogfilename = PathStr + Errorlogfilename;
                CSlogfilename = PathStr + CSlogfilename;
            }
            else
            {
                Datalogfilename = PathStr + "\\" + Datalogfilename;
                Errorlogfilename = PathStr + "\\" + Errorlogfilename;
                CSlogfilename = PathStr + "\\" + CSlogfilename;
            }

            this.comboBox1.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());

            automaterunning = false;
            intervaltimer1.Enabled = false;

            automatechkbox.Enabled = false;
            // set interval based on remembered setting
            switch (Properties.Settings.Default.Dataloggingrate)
            {
                case 30000:
                    automate30s.Checked = true;
                    break;
                case 60000:
                    automate1m.Checked = true;
                    break;
                case 60000 * 5:
                    automate5m.Checked = true;
                    break;
                case 60000 * 10:
                    automate10m.Checked = true;
                    break;
                case 60000 * 15:
                    automate15m.Checked = true;
                    break;
                case 60000 * 30:
                    automate30m.Checked = true;
                    break;
            }
            automatechkbox.Enabled = true;

            // always start with logging disabled by default
            // Properties.Settings.Default.DataLogFileEnabled = false;
            // LogValuesChkBox.Enabled = false;
            // Properties.Settings.Default.ErrorLoggingEnabled = false;
            // LogErrorsChkBox.Enabled = false;             // leave enabled so as to get start messages
            Properties.Settings.Default.Save();

            ComPortName = Properties.Settings.Default.ComPortString;
            int index = this.comboBox1.FindString(ComPortName);
            if (index < 0)	// < 0 or -1 if not found
            {
                // do nothing
            }
            else
            {
                this.comboBox1.SelectedIndex = index; // set the list to the found comport
            }

            // set up comport speed listbox
            // remove comport event handler
            comportspeed.SelectedIndexChanged -= new System.EventHandler(comportspeed_SelectedIndexChanged);
            comportspeed.Enabled = false;    // prevent change event from happening
            ComPortBaudRate = Properties.Settings.Default.ComPortSpeed;
            switch (ComPortBaudRate)
            {
                case 4800: comportspeed.SetSelected(0, true); break;
                case 9600: comportspeed.SetSelected(1, true); break;
                case 14400: comportspeed.SetSelected(2, true); break;
                case 19200: comportspeed.SetSelected(3, true); break;
                case 28800: comportspeed.SetSelected(4, true); break;
                case 38400: comportspeed.SetSelected(5, true); break;
                case 57600: comportspeed.SetSelected(6, true); break;
                case 115200: comportspeed.SetSelected(7, true); break;
                default: comportspeed.SetSelected(0, true); break;
            }
            comportspeed.Enabled = true;
            // restore handler
            comportspeed.SelectedIndexChanged += new System.EventHandler(comportspeed_SelectedIndexChanged);

            ArduinoFirmwareRev = "Not connected.";
            RxString = "";              // clear receive buffer;

            // disable controls that need connection to controller
            DisconnectBtn.Enabled = false;
            ConnectBtn.Enabled = true;
            RefreshComPortBtn.Enabled = true;
            getfirmwareBtn.Enabled = false;
            getfrequencyBtn.Enabled = false;
            getirradianceBtn.Enabled = false;
            getsqmBtn.Enabled = false;
            automategrpbox.Enabled = false;
            automatechkbox.Enabled = false;
            forceExitMenuItem.Enabled = true;
            IRSensorGrpBox.Enabled = false;
            getsetpointsBtn.Enabled = false;
            setpoint1Btn.Enabled = false;
            setpoint2Btn.Enabled = false;
            RainSensorGrpBox.Enabled = false;
            bme280grpBox.Enabled = false;
            getallBtn.Enabled = false;
            getLuxBtn.Enabled = false;
            GetNELMBtn.Enabled = false;
            gpsGroupBox.Enabled = false;
            getGPSDataBtn.Enabled = false;

            statustxtbox.Clear();
            statustxtbox.Update();

            SkyConditionLabel.Text = "UNKNOWN";
            SkyConditionLabel.Update();

            // set alert popup raining 
            if (Properties.Settings.Default.RainingAlert == true)
            {
                enableAlertPopupMenuItem.Checked = true;
                disableAlertPopoupMenuItem.Checked = false;
            }
            else
            {
                enableAlertPopupMenuItem.Checked = false;
                disableAlertPopoupMenuItem.Checked = true;
            }

            // rain timer
            Raining = false;
            RainTimer.Enabled = false;
            RainTimer.Interval = Properties.Settings.Default.RainingPollRate;
            switch (Properties.Settings.Default.RainingPollRate)
            {
                case 5000:
                    RainingPollRate5sMenuItem.Checked = true;
                    RainingPollRate30sMenuItem.Checked = false;
                    RainingPollRate1mMenuItem.Checked = false;
                    RainingPollRate5mMenuItem.Checked = false;
                    RainingPollRate10mMenuItem.Checked = false;
                    break;
                case 10000:
                    RainingPollRate5sMenuItem.Checked = false;
                    RainingPollRate30sMenuItem.Checked = true;
                    RainingPollRate1mMenuItem.Checked = false;
                    RainingPollRate5mMenuItem.Checked = false;
                    RainingPollRate10mMenuItem.Checked = false;
                    break;
                case 60000:
                    RainingPollRate5sMenuItem.Checked = false;
                    RainingPollRate30sMenuItem.Checked = false;
                    RainingPollRate1mMenuItem.Checked = true;
                    RainingPollRate5mMenuItem.Checked = false;
                    RainingPollRate10mMenuItem.Checked = false;
                    break;
                case 300000:
                    RainingPollRate5sMenuItem.Checked = false;
                    RainingPollRate30sMenuItem.Checked = false;
                    RainingPollRate1mMenuItem.Checked = false;
                    RainingPollRate5mMenuItem.Checked = true;
                    RainingPollRate10mMenuItem.Checked = false;
                    break;
                case 600000:
                    RainingPollRate5sMenuItem.Checked = false;
                    RainingPollRate30sMenuItem.Checked = false;
                    RainingPollRate1mMenuItem.Checked = false;
                    RainingPollRate5mMenuItem.Checked = false;
                    RainingPollRate10mMenuItem.Checked = true;
                    break;
                default:
                    Properties.Settings.Default.RainingPollRate = 60000;
                    Properties.Settings.Default.Save();
                    RainingPollRate5sMenuItem.Checked = false;
                    RainingPollRate30sMenuItem.Checked = false;
                    RainingPollRate1mMenuItem.Checked = true;
                    RainingPollRate5mMenuItem.Checked = false;
                    RainingPollRate10mMenuItem.Checked = false;
                    break;
            }

            gpsFixChkBox.Checked = false;
            Properties.Settings.Default.Save();
        }

        private void getsqmBtn_Click(object sender, EventArgs e)
        {
            string tcmd;
            string ret;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getsqmBtn: START ================================");

            statustxtbox.Clear();
            statustxtbox.Update();

            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is open");
                tcmd = ":01#";       // GR
                statustxtbox.Text = tcmd;
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value;
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is not open");
                statustxtbox.Text = "Com port is not connected.";
                statustxtbox.Update();
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getsqmBtn: END ================================");
        }


        private void getirradianceBtn_Click(object sender, EventArgs e)
        {
            string tcmd;
            string ret;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getirradianceBtn: START ================================");

            statustxtbox.Clear();
            statustxtbox.Update();

            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is open");
                tcmd = ":03#";  // GI
                statustxtbox.Text = tcmd;
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value;
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is not open");
                statustxtbox.Text = "Com port is not connected.";
                statustxtbox.Update();
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getirradianceBtn: END ================================");
        }

        private void getfrequencyBtn_Click(object sender, EventArgs e)
        {
            string tcmd;
            string ret;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getfrequencyBtn: START ================================");

            statustxtbox.Clear();
            statustxtbox.Update();

            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is open");
                tcmd = ":02#";      // GF
                statustxtbox.Text = tcmd;
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value;
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is not open");
                statustxtbox.Text = "Com port is not connected.";
                statustxtbox.Update();
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getfrequencyBtn: END ================================");
        }

        private void getfirmwareBtn_Click(object sender, EventArgs e)
        {
            string tcmd;
            string ret;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getfirmwareBtn: START ================================");

            statustxtbox.Clear();
            statustxtbox.Update();

            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is open");
                tcmd = ":04#";      // GV use controller specific command, not GV of moonlite
                statustxtbox.Text = tcmd;
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is not open");
                statustxtbox.Text = "Com port is not connected.";
                statustxtbox.Update();
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getfirmwareBtn: END ================================");
        }

        private void RefreshComPortBtn_Click(object sender, EventArgs e)
        {
            statustxtbox.Clear();
            statustxtbox.Update();

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("RefreshComPortBtn: START ================================");

            if (!myserialPort.IsOpen)
            {
                // erase list box items of comports
                this.comboBox1.Items.Clear();
                // reload from the system
                this.comboBox1.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
                RxString = "";              // clear receive buffer;
            }
            else
            {
                statustxtbox.Text = "Comport is already connected. Please click Disconnect first, then click Refresh";
                statustxtbox.Update();
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("RefreshComPortBtn: END ================================");
        }

        private void automatechkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is open");
                if (automatechkbox.Checked) // if checked then enable automate
                {
                    automaterunning = true;
                    //enable timer with interval
                    int timerinterval;
                    timerinterval = 60000;      // just set to a default in case

                    if (automate30s.Checked)
                        timerinterval = 30000;      // 30s
                    else if (automate1m.Checked)
                        timerinterval = 60000;      // 60s = 1m
                    else if (automate5m.Checked)
                        timerinterval = 60000 * 5;  // 5m
                    else if (automate10m.Checked)
                        timerinterval = 60000 * 10; // 10m
                    else if (automate15m.Checked)
                        timerinterval = 60000 * 15; // 15m
                    else if (automate30m.Checked)
                        timerinterval = 60000 * 30;   // 30m
                    else
                        timerinterval = 60000;     // in case of error set to 1m
                    Properties.Settings.Default.Dataloggingrate = timerinterval;
                    Properties.Settings.Default.Save();
                    intervaltimer1.Interval = timerinterval;
                    intervaltimer1.Enabled = true;
                }
                else
                {
                    automaterunning = false;
                    //disable timer
                    intervaltimer1.Enabled = false;
                }
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is not open");
                statustxtbox.Text = "Error: Serial port is not connected";
                statustxtbox.Update();
                automatechkbox.Checked = false;
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
        }

        // this is the data logging function called at regular intervals if enabled
        private void intervaltimer1_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("Interval timer START ================================");
            statustxtbox.Text = "Interval timer triggered";
            statustxtbox.Update();
            // get parameters from the controller and fill in the boxes
            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is open");
                // serial port is open so send commands one after the other

                // prevent other calls
                disableallbuttons();

                getallvaluesfromcontroller();

                // log data if enabled
                if (Properties.Settings.Default.DataLogFileEnabled == true)
                {
                    writedatavaluestodatafile();
                }

                // re-enable buttons
                enableeallbuttons();
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is not open");
                // serial port has been disconnected
                statustxtbox.Text = "Error: interval timer reports that serial port is not connected";
                statustxtbox.Update();
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("Interval timer END ==================================");
            statustxtbox.Text = "Interval timer end";
            statustxtbox.Update();
        }

        private void getallvaluesfromcontroller()
        {
            string ret;
            string tcmd;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getallvaluesfromcontroller START ================================");
            statustxtbox.Text = "getallvaluesfromcontroller triggered";
            statustxtbox.Update();
            // get parameters from the controller and fill in the boxes
            // assuming serial port is open

            tcmd = ":03#";      // get irradience
            statustxtbox.Text = tcmd;
            statustxtbox.Update();
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("Calling command string with " + tcmd);
            ret = CommandString(tcmd, true);

            tcmd = ":02#";      // get frequency
            statustxtbox.Text = tcmd;
            statustxtbox.Update();
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("Calling command string with " + tcmd);
            ret = CommandString(tcmd, true);

            tcmd = ":04#";      // get firmware
            statustxtbox.Text = tcmd;
            statustxtbox.Update();
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("Calling command string with " + tcmd);
            ret = CommandString(tcmd, true);

            tcmd = ":01#";      // get sqm reading
            statustxtbox.Text = tcmd;
            statustxtbox.Update();
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("Calling command string with " + tcmd);
            ret = CommandString(tcmd, true);

            tcmd = ":20#";      // get IR Ambient temp - always before object temp
            statustxtbox.Text = tcmd;
            statustxtbox.Update();
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("Calling command string with " + tcmd);
            ret = CommandString(tcmd, true);

            tcmd = ":19#";      // get IR Object temp
            statustxtbox.Text = tcmd;
            statustxtbox.Update();
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("Calling command string with " + tcmd);
            ret = CommandString(tcmd, true);

            tcmd = ":21#";      // get LUX
            statustxtbox.Text = tcmd;
            statustxtbox.Update();
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("Calling command string with " + tcmd);
            ret = CommandString(tcmd, true);

            tcmd = ":31#";      // get NELM
            statustxtbox.Text = tcmd;
            statustxtbox.Update();
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("Calling command string with " + tcmd);
            ret = CommandString(tcmd, true);

            tcmd = ":24#";      // get Raining Vout
            statustxtbox.Text = tcmd;
            statustxtbox.Update();
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("Calling command string with " + tcmd);
            ret = CommandString(tcmd, true);

            tcmd = ":23#";      // get Raining?
            statustxtbox.Text = tcmd;
            statustxtbox.Update();
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("Calling command string with " + tcmd);
            ret = CommandString(tcmd, true);

            if (Properties.Settings.Default.RainingAlert == true)
            {
                if (Raining == true)
                {
                    DialogResult dr;
                    RainTimer.Stop();
                    dr = MessageBox.Show("IT IS RAINING." + System.Environment.NewLine + "Click Cancel to Disable rain alert or OK to continue", "mySQMPRO", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    switch (dr)
                    {
                        case DialogResult.Cancel:      // disable
                            Properties.Settings.Default.RainingAlert = false;
                            Properties.Settings.Default.Save();
                            disableAlertPopoupMenuItem.Checked = true;
                            enableAlertPopupMenuItem.Checked = false;
                            RainTimer.Enabled = false;
                            break;
                        case DialogResult.OK:          // enable
                        default:
                            Properties.Settings.Default.RainingAlert = true;
                            Properties.Settings.Default.Save();
                            disableAlertPopoupMenuItem.Checked = false;
                            enableAlertPopupMenuItem.Checked = true;
                            RainTimer.Enabled = true;
                            RainTimer.Start();
                            break;
                    }
                }
            }
            // get BME820 humidity
            tcmd = ":32#";
            statustxtbox.Text = tcmd;
            statustxtbox.Update();
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("Calling command string with " + tcmd);
            ret = CommandString(tcmd, true);
            // ignore return value;

            // get BME820 pressure
            tcmd = ":33#";
            statustxtbox.Text = tcmd;
            statustxtbox.Update();
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("Calling command string with " + tcmd);
            ret = CommandString(tcmd, true);
            // ignore return value;

            // get BME820 temperature
            tcmd = ":34#";
            statustxtbox.Text = tcmd;
            statustxtbox.Update();
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("Calling command string with " + tcmd);
            ret = CommandString(tcmd, true);
            // ignore return value;

            // get BME280 dewpoint
            tcmd = ":35#";
            statustxtbox.Text = tcmd;
            statustxtbox.Update();
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("Calling command string with " + tcmd);
            ret = CommandString(tcmd, true);
            // ignore return value;

            tcmd = ":27#";      // get skystate
            statustxtbox.Text = tcmd;
            statustxtbox.Update();
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("Calling command string with " + tcmd);
            ret = CommandString(tcmd, true);

            // gpsdata
            if (gpspresentChkBox.Checked)
            {
                // get gpsdate
                tcmd = ":08#";
                statustxtbox.Text = tcmd;
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value;

                // get gpstime
                tcmd = ":09#";
                statustxtbox.Text = tcmd;
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value;

                // get gpslong
                tcmd = ":10#";
                statustxtbox.Text = tcmd;
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value;

                // get gps latitude
                tcmd = ":11#";
                statustxtbox.Text = tcmd;
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value;

                // get gps satelittes
                tcmd = ":13#";
                statustxtbox.Text = tcmd;
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value;

                // get gps altitude            
                tcmd = ":12#";
                statustxtbox.Text = tcmd;
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value;

            }


            // other get calls here

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getallvaluesfromcontroller END ================================");
            statustxtbox.Text = "getallvaluesfromcontroller end";
            statustxtbox.Update();
        }

        // this writes the data values to the logfile
        private void writedatavaluestodatafile()
        {
            String DataStr = "";
            String tmpString = "";
            // issue - writing as string to file causes locale issue - 21,95 is written as 21,95
            // and not 21.95

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("writedatavaluestodatafile START ================================");

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("writedatavaluestodatafile - writing data values");
            String DateNow = DateTime.Now.ToString("dd/MM/yy,HH:mm:ss");
            DataStr = DataStr + DateNow + ",";

            tmpString = Convert.ToString(sqmval, newCulture);
            DataStr = DataStr + tmpString + ",";                      // 2

            tmpString = Convert.ToString(lux, newCulture);
            DataStr = DataStr + tmpString + ",";                      // 3

            tmpString = Convert.ToString(IRAmbientTemp, newCulture);
            DataStr = DataStr + tmpString + ",";                      // 4

            tmpString = Convert.ToString(IRObjectTemp, newCulture);
            DataStr = DataStr + tmpString + ",";                      // 5

            tmpString = Convert.ToString(irradiance, newCulture);
            DataStr = DataStr + tmpString + ",";                      // 5

            tmpString = Convert.ToString(frequency, newCulture);
            DataStr = DataStr + tmpString + ",";                      // 7

            tmpString = firmwaretxtbox.Text;
            DataStr = DataStr + tmpString + ",";                      // 8

            DataStr = DataStr + setpoint1.ToString() + ",";           // 9
            DataStr = DataStr + setpoint2.ToString() + ",";           // 10
            DataStr = DataStr + RainSensorTxtBox.Text + ",";          // 11

            tmpString = RainVout.ToString();
            DataStr = DataStr + tmpString + ",";                      // 12

            tmpString = Convert.ToString(nelm, newCulture);
            DataStr = DataStr + tmpString + ",";                      // 13

            tmpString = Convert.ToString(bme280temperature, newCulture);
            DataStr = DataStr + tmpString + ",";                      // 14

            tmpString = Convert.ToString(bme280humidity, newCulture);
            DataStr = DataStr + tmpString + ",";                      // 15

            tmpString = Convert.ToString(bme280dewpoint, newCulture);
            DataStr = DataStr + tmpString + ",";                      // 16

            tmpString = Convert.ToString(bme280pressure, newCulture);
            DataStr = DataStr + tmpString + ",";                      // 17

            DataStr = DataStr + gpsdate + ",";                        // 18
            DataStr = DataStr + gpstime + ",";                        // 19
            DataStr = DataStr + gpslongitude + ",";                   // 20
            DataStr = DataStr + gpslatitude + ",";                    // 21
            DataStr = DataStr + gpssatelittes + ",";                  // 22
            DataStr = DataStr + gpsaltitude + ",";                    // 23

            DataStr = DataStr + SkyConditionLabel.Text;               // 24
            LogDataToFile(DataStr);
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("writedatavaluestodatafile - writing data values - done");

            if (CloudSensorIILogFormat.Checked == true)
            {
                LogMessageToErrorFile("writedatavaluestodatafile - writing cloud sensor file");

                // use cloud sensor II log file format
                DataStr = "";
                DateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff");
                DataStr = DataStr + DateNow;
                while (DataStr.Length < 23)
                    DataStr = DataStr + " ";

                // temperature is celcius 24
                DataStr = DataStr + "C";
                while (DataStr.Length < 25)
                    DataStr = DataStr + " ";

                // Wind velocity 26
                DataStr = DataStr + "m";
                while (DataStr.Length < 27)
                    DataStr = DataStr + " ";

                // sky temperature 28-34
                // F1 means float format with 1 place
                DataStr = DataStr + IRObjectTemp.ToString("F1");
                while (DataStr.Length < 34)
                    DataStr = DataStr + " ";

                // ambient temperature 35-40
                DataStr = DataStr + IRAmbientTemp.ToString("F1");
                while (DataStr.Length < 40)
                    DataStr = DataStr + " ";

                // sensor case temperature 41-47
                DataStr = DataStr + bme280temperature.ToString("F1");
                while (DataStr.Length < 48)
                    DataStr = DataStr + " ";

                // Wind Speed 49-54
                DataStr = DataStr + "-1";                   // still heating up
                while (DataStr.Length < 55)
                    DataStr = DataStr + " ";

                // Humidity 56-58
                int hum = Convert.ToInt32((int)bme280humidity);
                DataStr = DataStr + hum.ToString();
                while (DataStr.Length < 59)
                    DataStr = DataStr + " ";

                // Dew Point 60-65 (6 characters)
                DataStr = DataStr + bme280dewpoint.ToString("F1"); ;
                while (DataStr.Length < 66)
                    DataStr = DataStr + " ";

                // Heater % 67-69
                DataStr = DataStr + "0";
                while (DataStr.Length < 70)
                    DataStr = DataStr + " ";

                // Rain Flag 0-2 71
                if (Raining)
                    DataStr = DataStr + "2";
                else
                    DataStr = DataStr + "0";
                while (DataStr.Length < 72)
                    DataStr = DataStr + " ";

                // Wet Flag 0-2 73
                if (Raining)
                    DataStr = DataStr + "2";
                else
                    DataStr = DataStr + "0";
                while (DataStr.Length < 74)
                    DataStr = DataStr + " ";

                // Since 75-79 seconds since the last valid data
                // 00004
                // using the timerinterval tick
                // I use 30s (30) - 30m (1800)
                int timerinterval = intervaltimer1.Interval / 1000;
                DataStr = DataStr + timerinterval.ToString();
                while (DataStr.Length < 80)
                    DataStr = DataStr + " ";

                // Now() 81-92
                // date/time given as the VB6 Now() function result (in days) when Clarity II last
                // wrote this file
                // 038506.08846
                DataStr = DataStr + "038506.08846";
                while (DataStr.Length < 93)
                    DataStr = DataStr + " ";

                // c sky state 94
                // cloudUnknown = 0, cloudClear = 1, cloudCloudy = 2, cloudVeryCloudy = 3
                // skystate, 0=clear, 1=partly cloudy, 2=cloudy
                DataStr = DataStr + skystate.ToString();
                while (DataStr.Length < 95)
                    DataStr = DataStr + " ";

                // w wind condition 96
                // windUnknown = 0, windCalm = 1, windWindy = 2, windVeryWindy = 3
                DataStr = DataStr + "0";
                while (DataStr.Length < 97)
                    DataStr = DataStr + " ";

                // r rain condition 98
                // rainUnknown = 0, rainDry = 1, rainWet = 2, rainRain = 3
                if (Raining)
                    DataStr = DataStr + "3";
                else
                    DataStr = DataStr + "1";
                while (DataStr.Length < 99)
                    DataStr = DataStr + " ";

                // d daylight condition 100
                // dayUnknown = 0, dayDark = 1, dayLight = 2, dayVeryLight = 3
                if (lux < 3.0)
                    DataStr = DataStr + "1";
                else if (lux < 10000)
                    DataStr = DataStr + "2";
                else
                    DataStr = DataStr + "3";
                while (DataStr.Length < 101)
                    DataStr = DataStr + " ";

                // C roof close 102
                DataStr = DataStr + "0";        // not requested
                while (DataStr.Length < 103)
                    DataStr = DataStr + " ";

                // A Alert 104
                DataStr = DataStr + "0";              // not alerting
                LogDataToCloudSensorFile(DataStr);
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("writedatavaluestodatafile - writing cloud sensor file - done");
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("writedatavaluestodatafile END ================================");
        }

        private void mySQMGPS_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.FormLocation = this.Location;
            Properties.Settings.Default.Save();

            // if the comport is opened
            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is open");
                MessageBox.Show("Disconnect the Serial Port before exiting", "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
            else   // Serial port is disconnected
            {
                myserialPort.Dispose();
                // need to stop automate if running
                intervaltimer1.Stop();
                intervaltimer1.Enabled = false;
                Properties.Settings.Default.Save();  // save the application settings
                Application.Exit();
            }
        }

        private void forceExitMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;

            result = MessageBox.Show("Are you sure you want to force exit?", "mySQMPRO", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                // force exit
                // flush serial buffers
                // close serial port etc
                // stop interval timer
                // print message
                intervaltimer1.Enabled = false;
                if (myserialPort.IsOpen)
                    myserialPort.DiscardInBuffer();     // clear buffer
                RxString = "";              // clear receive buffer;
                CmdString = "";
                if (myserialPort.IsOpen)
                {
                    myserialPort.Close();
                    myserialPort.Dispose();
                }
                statustxtbox.Text = "Force Exit of mySQMPRO done.";
                statustxtbox.Update();
                Properties.Settings.Default.Save();
                Environment.Exit(0);
            }
            else if (result == DialogResult.No)
            {
                // do nothing
                // print message port not disconnected
                statustxtbox.Text = "Force Exit of mySQMPRO ignored.";
                statustxtbox.Update();
            }
            else if (result == DialogResult.Cancel)
            {
                // do nothing
                // print message Exit cancelled
                statustxtbox.Text = "Force Exit of mySQMPRO cancelled.";
                statustxtbox.Update();
            }
        }

        private void comportspeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            comportspeed.Enabled = false;
            // get the comportspeed from the listbox
            switch (comportspeed.SelectedIndex)
            {
                case 0: ComPortBaudRate = 4800;
                    break;
                case 1: ComPortBaudRate = 9600;
                    break;
                case 2: ComPortBaudRate = 14400;
                    break;
                case 3: ComPortBaudRate = 19200;
                    break;
                case 4: ComPortBaudRate = 28800;
                    break;
                case 5: ComPortBaudRate = 38400;
                    break;
                case 6: ComPortBaudRate = 57600;
                    break;
                case 7: ComPortBaudRate = 115200;
                    break;
                default: ComPortBaudRate = 9600;
                    break;
            }

            statustxtbox.Text = "Comport speed set to " + ComPortBaudRate.ToString();
            statustxtbox.Update();

            // save comportspeed
            Properties.Settings.Default.ComPortSpeed = ComPortBaudRate;
            Properties.Settings.Default.Save();
            comportspeed.Enabled = true;
        }

        private void ClearStatusMsgTimer_Tick(object sender, EventArgs e)
        {
            statustxtbox.Clear();
            statustxtbox.Update();
            ClearStatusMsgTimer.Stop();
        }

        private void statustxtbox_TextChanged(object sender, EventArgs e)
        {
            ClearStatusMsgTimer.Start();
        }

        private void SetPoint1TxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                // enter key has been pressed, so validate entry
                try
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("SetPoint1TxtBox: = " + SetPoint1TxtBox.Text);
                    try
                    {
                        if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                            LogMessageToErrorFile("Before conversion setpoint1: = " + SetPoint1TxtBox.ToString());
                        setpoint1 = Convert.ToInt32(SetPoint1TxtBox.Text);
                        if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                            LogMessageToErrorFile("After conversion setpoint1: = " + setpoint1.ToString());
                        if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                            LogMessageToErrorFile("Before Rounding setpoint1: = " + setpoint1.ToString());
                        if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                            LogMessageToErrorFile("After Rounding setpoint1: = " + setpoint1.ToString());
                        // update setpoint1 text box - this also updates the app setting
                        SetPoint1TxtBox.Text = setpoint1.ToString();
                        SetPoint1TxtBox.Update();
                    }
                    catch (Exception)
                    {
                        if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                            LogMessageToErrorFile("setpoint1: EXCEPTION CONVERTING SetPoint1TxtBox");
                        setpoint1 = 0;
                        // update setpoint1 text box - this also updates the app setting
                        SetPoint1TxtBox.Text = setpoint1.ToString();
                        SetPoint1TxtBox.Update();
                        statustxtbox.Text = "SetPoint1 value not correct syntax: Setting value to 2.0";
                        statustxtbox.Update();
                        if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                            LogMessageToErrorFile("Error in conversion - SetPoint1TxtBox: = " + SetPoint1TxtBox.Text);
                    }
                    Properties.Settings.Default.Save();
                    e.Handled = true;
                    return;
                }
                catch (Exception)
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("ERROR: EXCEPTION ERROR FOR SetPoint1TxtBox: " + SetPoint1TxtBox.Text);
                    statustxtbox.Text = "Invalid format for SetPoint1. Reset to 2";
                    statustxtbox.Update();
                    setpoint1 = 0;
                    // update setpoint1 text box - this also updates the app setting
                    SetPoint1TxtBox.Text = setpoint1.ToString();
                    SetPoint1TxtBox.Update();
                    Properties.Settings.Default.Save();
                    e.Handled = true;
                    return;
                }
            }
            // allow only numeric inout and + - and backspace (8)
            char ch = e.KeyChar;

            // only allow one '-'
            if (ch == 45 && SetPoint1TxtBox.Text.IndexOf('-') != -1)
            {
                e.Handled = true;
                return;
            }

            // only allow one '+'
            if (ch == 43 && SetPoint1TxtBox.Text.IndexOf('+') != -1)
            {
                e.Handled = true;
                return;
            }

            // handle digits, backspace, + and - and comma
            if (!Char.IsDigit(ch) && ch != 8 && ch != 43 && ch != 45)
            {
                e.Handled = true;
            }
        }

        private void SetPoint2TxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                // enter key has been pressed, so validate entry
                try
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("SetPoint2TxtBox: = " + SetPoint2TxtBox.Text);
                    try
                    {
                        if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                            LogMessageToErrorFile("Before conversion setpoint2: = " + SetPoint2TxtBox.ToString());
                        setpoint2 = Convert.ToInt32(SetPoint2TxtBox.Text);
                        if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                            LogMessageToErrorFile("After conversion setpoint2: = " + setpoint2.ToString());
                        if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                            LogMessageToErrorFile("Before Rounding setpoint2: = " + setpoint2.ToString());
                        if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                            LogMessageToErrorFile("After Rounding setpoint2: = " + setpoint2.ToString());
                        // update setpoint1 text box - this also updates the app setting
                        SetPoint2TxtBox.Text = setpoint2.ToString();
                        SetPoint2TxtBox.Update();
                    }
                    catch (Exception)
                    {
                        if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                            LogMessageToErrorFile("setpoint2: EXCEPTION CONVERTING SetPoint2TxtBox");
                        setpoint2 = 0;
                        // update setpoint2 text box - this also updates the app setting
                        SetPoint2TxtBox.Text = setpoint2.ToString();
                        SetPoint2TxtBox.Update();
                        statustxtbox.Text = "SetPoint2 value not correct syntax: Setting value to 22";
                        statustxtbox.Update();
                        if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                            LogMessageToErrorFile("Error in conversion - SetPoint2TxtBox: = " + SetPoint2TxtBox.Text);
                    }
                    Properties.Settings.Default.Save();
                    e.Handled = true;
                    return;
                }
                catch (Exception)
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("ERROR: EXCEPTION ERROR FOR SetPoint2TxtBox: " + SetPoint2TxtBox.Text);
                    statustxtbox.Text = "Invalid format for SetPoint1. Reset to 22";
                    statustxtbox.Update();
                    setpoint2 = 0;
                    // update setpoint2 text box - this also updates the app setting
                    SetPoint2TxtBox.Text = setpoint2.ToString();
                    SetPoint2TxtBox.Update();
                    Properties.Settings.Default.Save();
                    e.Handled = true;
                    return;
                }
            }
            // allow only numeric input and + - and backspace (8)
            char ch = e.KeyChar;

            // only allow one '-'
            if (ch == 45 && SetPoint2TxtBox.Text.IndexOf('-') != -1)
            {
                e.Handled = true;
                return;
            }

            // only allow one '+'
            if (ch == 43 && SetPoint2TxtBox.Text.IndexOf('+') != -1)
            {
                e.Handled = true;
                return;
            }

            // handle digits, backspace, + and - and comma
            if (!Char.IsDigit(ch) && ch != 8 && ch != 43 && ch != 45)
            {
                e.Handled = true;
            }
        }

        private void getRainSensorBtn_Click(object sender, EventArgs e)
        {
            // get rain data from controller
            string tcmd;
            string ret;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getRainSensorBtn: START ================================");
            statustxtbox.Clear();
            statustxtbox.Update();

            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is open");
                tcmd = ":23#";       // Get Raining
                statustxtbox.Text = "Get rain indicator " + tcmd;
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value;

                tcmd = ":24#";       // Get Rain Vout
                statustxtbox.Text = "Get rain vout " + tcmd;
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value;
                if (Raining == true)
                {
                    DialogResult dr;
                    RainTimer.Stop();
                    dr = MessageBox.Show("IT IS RAINING." + System.Environment.NewLine + "Click Cancel to Disable rain alert or OK to continue", "mySQMPRO", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    switch (dr)
                    {
                        case DialogResult.Cancel:      // disable
                            Properties.Settings.Default.RainingAlert = false;
                            Properties.Settings.Default.Save();
                            disableAlertPopoupMenuItem.Checked = true;
                            enableAlertPopupMenuItem.Checked = false;
                            RainTimer.Enabled = false;
                            break;
                        case DialogResult.OK:          // enable
                        default:
                            Properties.Settings.Default.RainingAlert = true;
                            Properties.Settings.Default.Save();
                            disableAlertPopoupMenuItem.Checked = false;
                            enableAlertPopupMenuItem.Checked = true;
                            RainTimer.Enabled = true;
                            RainTimer.Start();
                            break;
                    }
                }
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is not open");
                statustxtbox.Text = "Com port is not connected.";
                statustxtbox.Update();
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getRainSensorBtn: END ================================");
        }

        Boolean validatesetpoints()
        {
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("validatesetpoints: START ================================");

            // validate setpoint1
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("SetPoint1TxtBox: = " + SetPoint1TxtBox.Text);
            try
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Before conversion setpoint1: = " + SetPoint1TxtBox.ToString());
                setpoint1 = Convert.ToInt32(SetPoint1TxtBox.Text);
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("After conversion setpoint1: = " + setpoint1.ToString());
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Before Rounding setpoint1: = " + setpoint1.ToString());
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("After Rounding setpoint1: = " + setpoint1.ToString());
                // update setpoint1 text box - this also updates the app setting
                SetPoint1TxtBox.Text = setpoint1.ToString();
                SetPoint1TxtBox.Update();
                Properties.Settings.Default.Save();
            }
            catch (Exception)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("setpoint1: EXCEPTION CONVERTING SetPoint1TxtBox");
                setpoint1 = 2;
                // update setpoint1 text box - this also updates the app setting
                SetPoint1TxtBox.Text = setpoint1.ToString();
                SetPoint1TxtBox.Update();
                Properties.Settings.Default.Save();
                statustxtbox.Text = "SetPoint1 value not correct syntax: Setting value to 2";
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Error in conversion - SetPoint1TxtBox: = " + SetPoint1TxtBox.Text);
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("validatesetpoint: Error in setpoint1 value");
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("validatesetpoints: END");
                return false;
            }

            // validate setpoint2
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("SetPoint2TxtBox: = " + SetPoint2TxtBox.Text);
            try
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Before conversion setpoint2: = " + SetPoint2TxtBox.ToString());
                setpoint2 = Convert.ToInt32(SetPoint2TxtBox.Text);
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("After conversion setpoint2: = " + setpoint2.ToString());
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Before Rounding setpoint2: = " + setpoint2.ToString());
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("After Rounding setpoint2: = " + setpoint2.ToString());
                // update setpoint2 text box - this also updates the app setting
                SetPoint2TxtBox.Text = setpoint2.ToString();
                SetPoint2TxtBox.Update();
                Properties.Settings.Default.Save();
            }
            catch (Exception)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("setpoint1: EXCEPTION CONVERTING SetPoint2TxtBox");
                setpoint2 = 22;
                // update setpoint2 text box - this also updates the app setting
                SetPoint2TxtBox.Text = setpoint2.ToString();
                SetPoint2TxtBox.Update();
                Properties.Settings.Default.Save();
                statustxtbox.Text = "SetPoint2 value not correct syntax: Setting value to 22";
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Error in conversion - SetPoint2TxtBox: = " + SetPoint2TxtBox.Text);
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("validatesetpoint: Error in setpoint2 value");
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("validatesetpoints: END");
                return false;
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("validatesetpoints: END ================================");
            return true;
        }

        private void getMLX90614Btn_Click(object sender, EventArgs e)
        {
            // get MLX90614 sensor readings
            string tcmd;
            string ret;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getMLX90614Btn: START ================================");
            statustxtbox.Clear();
            statustxtbox.Update();

            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("getMLX90614Btn: Com port connected");
                // SHOULD VALIDATE SETPOINT1 AND SETPOINT2 AS USER MAY NOT HAVE PRESSED ENTER KEY
                if (validatesetpoints() == true)
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("getMLX90614Btn: Setpoints are valid");
                    tcmd = ":20#";       // Get IR Ambient first
                    statustxtbox.Text = "Get IR sensor Ambient temperature " + tcmd;
                    statustxtbox.Update();
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("Calling command string with " + tcmd);
                    ret = CommandString(tcmd, true);
                    PauseForTime(0, 250);
                    tcmd = ":19#";       // Get Object temperature
                    statustxtbox.Text = "Get IR sensor Object temperature " + tcmd;
                    statustxtbox.Update();
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("Calling command string with " + tcmd);
                    ret = CommandString(tcmd, true);
                    // get cloud state
                    tcmd = ":27#";
                    statustxtbox.Text = tcmd;
                    statustxtbox.Update();
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("Calling command string with " + tcmd);
                    ret = CommandString(tcmd, true);
                    // ignore return value;
                    PauseForTime(0, 200);
                }
                else
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("getMLX90614Btn: Setpoints are invalid. Call to get IR sensor values is ignored.");
                    statustxtbox.Text = "Setpoints are invalid. Call to get IR sensor values is ignored.";
                    statustxtbox.Update();
                }
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("getMLX90614Btn: Com port not connected");
                statustxtbox.Text = "Com port is not connected.";
                statustxtbox.Update();
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getMLX90614Btn: END ================================");
        }

        private void setpoint1Btn_Click(object sender, EventArgs e)
        {
            // sends setpoint1 to controller
            string tcmd;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("setpoint1Btn: START ================================");
            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("setpoint1Btn: Com port connected");
                // SHOULD VALIDATE SETPOINT1 AND SETPOINT2 AS USER MAY NOT HAVE PRESSED ENTER KEY
                if (validatesetpoints() == true)
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("setpoint1Btn: Setpoints are valid");
                    tcmd = ":25";       // set setpoint1
                    tcmd = tcmd + setpoint1.ToString() + "#";
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("Calling command string with " + tcmd);
                    statustxtbox.Text = "Sending setpoint1: " + setpoint1.ToString();
                    statustxtbox.Update();
                    CommandString(tcmd, false);
                }
                else
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("setpoint1Btn: a setpoint is invalid");
                    statustxtbox.Text = "A setpoint is not valid. Command ignored.";
                    statustxtbox.Update();
                }
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("setpoint1Btn: Com port not connected");
                statustxtbox.Text = "Com port is not connected.";
                statustxtbox.Update();
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("setpoint1Btn: END ================================");
        }

        private void setpoint2Btn_Click(object sender, EventArgs e)
        {
            // sends setpoint2 to controller
            string tcmd;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("setpoint2Btn: START ================================");
            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("setpoint2Btn: Com port connected");
                // SHOULD VALIDATE SETPOINT1 AND SETPOINT2 AS USER MAY NOT HAVE PRESSED ENTER KEY
                if (validatesetpoints() == true)
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("setpoint2Btn: Setpoints are valid");
                    tcmd = ":26";       // set setpoint2
                    tcmd = tcmd + setpoint2.ToString() + "#";
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("Calling command string with " + tcmd);
                    statustxtbox.Text = "Sending setpoint2: " + setpoint2.ToString();
                    statustxtbox.Update();
                    CommandString(tcmd, false);
                }
                else
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("setpoint2Btn: a setpoint is invalid");
                    statustxtbox.Text = "A setpoint is not valid. Command ignored.";
                    statustxtbox.Update();
                }
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("setpoint2Btn: Com port not connected");
                statustxtbox.Text = "Com port is not connected.";
                statustxtbox.Update();
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("setpoint2Btn: END ================================");
        }

        private void getsetpointsBtn_Click(object sender, EventArgs e)
        {
            string tcmd;
            string ret;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getpointsBtn: START ================================");
            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("getpointsBtn: Com port connected");

                // get setpoint1
                tcmd = ":28#";
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);

                PauseForTime(0, 250);
                // get setpoint2
                tcmd = ":29#";
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("getpointsBtn: Com port not connected");
                statustxtbox.Text = "Com port is not connected.";
                statustxtbox.Update();
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("setpoint2Btn: END ================================");
        }

        private void getLuxBtn_Click(object sender, EventArgs e)
        {
            string tcmd;
            string ret;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getLuxBtn: START ================================");
            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("getLuxBtn: Com port connected");

                // get Lux
                tcmd = ":21#";
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("getLuxBtn: Com port not connected");
                statustxtbox.Text = "Com port is not connected.";
                statustxtbox.Update();
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getLuxBtn: END ================================");
        }

        private void SkyConditionLbl_Click(object sender, EventArgs e)
        {
            string tcmd;
            string ret;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("SkyCondition: START ================================");
            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("SkyCondition: Com port connected");

                // get Sky State
                tcmd = ":27#";
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("SkyCondition: Com port not connected");
                statustxtbox.Text = "Com port is not connected.";
                statustxtbox.Update();
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("SkyCondition: END ================================");
        }

        private void GetNELMBtn_Click(object sender, EventArgs e)
        {
            string tcmd;
            string ret;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("GetNELMBtn: START ================================");
            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("GetNELMBtn: Com port connected");

                // get Sky State
                tcmd = ":31#";
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("GetNELMBtn: Com port not connected");
                statustxtbox.Text = "Com port is not connected.";
                statustxtbox.Update();
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("GetNELMBtn: END ================================");
        }

        private void resetLogfilesPathMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dgresult;

            // clear the data logfilename etc
            // then ask user to disconnect and exit app and restart

            // disable data logging
            Properties.Settings.Default.DataLogFileEnabled = false;
            // disable error logging
            Properties.Settings.Default.ErrorLoggingEnabled = false;
            Properties.Settings.Default.Save();

            String PathStr = Properties.Settings.Default.LogPathName;
            if (PathStr == "")
            {
                PathStr = "<null>";
            }

            dgresult = MessageBox.Show("Current path is " + PathStr + System.Environment.NewLine + "Do you want to reset the path?", "mySQMPRO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgresult == System.Windows.Forms.DialogResult.Yes)
            {
                Properties.Settings.Default.LogPathName = "";
                Properties.Settings.Default.Save();
                MessageBox.Show("Press enter to reset the path - Application needs to be restarted for changes to take effect", "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // invoke disconnect event handler
                this.Invoke(new EventHandler(DisconnectBtn_Click));
                Application.Exit();
            }
            else
            {
                // ignore
            }
        }

        private void LogValuesChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("LogValues: START ================================");
            if (LogValuesChkBox.Checked)
            {
                // create the file
                try
                {
                    if (!File.Exists(Datalogfilename))
                    {
                        FileStream fs = File.Create(Datalogfilename);
                        // MessageBox.Show("File created", "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        fs.Close();

                        // write header line to file 
                        try
                        {
                            File.AppendAllText(Datalogfilename, "// mySQMPRO" + System.Environment.NewLine);
                            File.AppendAllText(Datalogfilename, HeaderLine + System.Environment.NewLine);
                            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                LogMessageToErrorFile("LogValues: Data values written to values log file");
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show("Error writing data to file: " + Ex.Message, "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            statustxtbox.Text = "Exception Error - could not write to file";
                            statustxtbox.Update();
                            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                                LogMessageToErrorFile("LogValues: Exception Error appending values log file");
                        }
                    }
                    else
                    {
                        // MessageBox.Show("File Exists Already", "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Error creating log file. Cannot Access Location: " + Ex.Message, "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    statustxtbox.Text = "Exception Error creating log file";
                    statustxtbox.Update();
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("LogValues: Exception Error creating values log file");
                }
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("LogValues: END ================================");
        }

        private void getallBtn_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("GetAll: START ================================");
            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("GetALL: Com port connected");

                // prevent other calls
                disableallbuttons();

                getallvaluesfromcontroller();

                // log data if enabled
                if (Properties.Settings.Default.DataLogFileEnabled == true)
                {
                    writedatavaluestodatafile();
                }
                // re-enable buttons
                enableeallbuttons();
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("GetAll: Com port not connected");
                statustxtbox.Text = "Com port is not connected.";
                statustxtbox.Update();
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("GetAll: END ================================");
        }

        private void disableAlertPopUpMenuItem_Click(object sender, EventArgs e)
        {
            // alert pop up when raining disabled
            Properties.Settings.Default.RainingAlert = false;
            Properties.Settings.Default.Save();
            disableAlertPopoupMenuItem.Checked = true;
            enableAlertPopupMenuItem.Checked = false;
            if (myserialPort.IsOpen)
            {
                RainTimer.Enabled = false;
                RainTimer.Stop();
            }
            else
            {
                RainTimer.Enabled = false;
                RainTimer.Stop();
            }
            statustxtbox.Text = "Rain alert disabled";
            statustxtbox.Update();
        }

        private void enableAlertPopUpMenuItem_Click(object sender, EventArgs e)
        {
            // alert pop up when raining enabled
            Properties.Settings.Default.RainingAlert = true;
            Properties.Settings.Default.Save();
            disableAlertPopoupMenuItem.Checked = false;
            enableAlertPopupMenuItem.Checked = true;
            if (myserialPort.IsOpen)
            {
                RainTimer.Enabled = true;
                RainTimer.Start();
                statustxtbox.Text = "Rain alert enabled";
                statustxtbox.Update();
            }
            else
            {
                RainTimer.Enabled = false;
                RainTimer.Stop();
                statustxtbox.Text = "Rain alert disabled: Controller not connected";
                statustxtbox.Update();
            }
        }

        private void RainingPollRate5sMenuItem_Click(object sender, EventArgs e)
        {
            // 5 seconds
            Properties.Settings.Default.RainingPollRate = 5000;
            Properties.Settings.Default.Save();
            RainTimer.Interval = 5000;
            RainingPollRate5sMenuItem.Checked = true;
            RainingPollRate30sMenuItem.Checked = false;
            RainingPollRate1mMenuItem.Checked = false;
            RainingPollRate5mMenuItem.Checked = false;
            RainingPollRate10mMenuItem.Checked = false;
        }

        private void RainingPollRate30sMenuItem_Click(object sender, EventArgs e)
        {
            // 30 seconds
            Properties.Settings.Default.RainingPollRate = 30000;
            Properties.Settings.Default.Save();
            RainTimer.Interval = 30000;
            RainingPollRate5sMenuItem.Checked = false;
            RainingPollRate30sMenuItem.Checked = true;
            RainingPollRate1mMenuItem.Checked = false;
            RainingPollRate5mMenuItem.Checked = false;
            RainingPollRate10mMenuItem.Checked = false;
        }

        private void RainingPollRate1mMenuItem_Click(object sender, EventArgs e)
        {
            // 1 minute
            Properties.Settings.Default.RainingPollRate = 60000;
            Properties.Settings.Default.Save();
            RainTimer.Interval = 60000;
            RainingPollRate5sMenuItem.Checked = false;
            RainingPollRate30sMenuItem.Checked = false;
            RainingPollRate1mMenuItem.Checked = true;
            RainingPollRate5mMenuItem.Checked = false;
            RainingPollRate10mMenuItem.Checked = false;
        }

        private void RainTimer_Tick(object sender, EventArgs e)
        {
            // get rain data from controller
            string tcmd;
            string ret;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("RainTimer: START ================================");
            statustxtbox.Clear();
            statustxtbox.Update();

            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is open");
                tcmd = ":23#";       // Get Raining
                statustxtbox.Text = "Get rain indicator " + tcmd;
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value;
            }
            else
            {
                RainTimer.Stop();
                RainTimer.Enabled = false;
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is not open");
                statustxtbox.Text = "Com port is not connected.";
                statustxtbox.Update();
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("RainTimer: END ================================");
        }

        private void RainingPollRate5mMenuItem_Click(object sender, EventArgs e)
        {
            // 5 minutes
            Properties.Settings.Default.RainingPollRate = 300000;
            Properties.Settings.Default.Save();
            RainTimer.Interval = 300000;
            RainingPollRate5sMenuItem.Checked = false;
            RainingPollRate30sMenuItem.Checked = false;
            RainingPollRate1mMenuItem.Checked = false;
            RainingPollRate5mMenuItem.Checked = true;
            RainingPollRate10mMenuItem.Checked = false;
        }

        private void RainingPollRate10mMenuItem_Click(object sender, EventArgs e)
        {
            // 10 minutes
            Properties.Settings.Default.RainingPollRate = 600000;
            Properties.Settings.Default.Save();
            RainTimer.Interval = 600000;
            RainingPollRate5sMenuItem.Checked = false;
            RainingPollRate30sMenuItem.Checked = false;
            RainingPollRate1mMenuItem.Checked = false;
            RainingPollRate5mMenuItem.Checked = false;
            RainingPollRate10mMenuItem.Checked = true;
        }

        private void GraphLoggingPicture_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("GraphLogging: START ================================");
            if (GraphForm == null)
            {
                GraphForm = new Graph();
                GraphForm.Show();
            }
            else
            {
                try
                {
                    GraphForm.Show();
                }
                catch (ObjectDisposedException)
                {
                    // object was disposed because window form was closed with X
                    // recreate object
                    GraphForm = new Graph();
                    GraphForm.Show();
                }
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("GraphLogging: START ================================");
        }

        private void LogErrorsChkBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void getBME280SensorBtn_Click(object sender, EventArgs e)
        {
            // get barometric data from controller
            string tcmd;
            string ret;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getBME280SensorBtn: START ================================");
            statustxtbox.Clear();
            statustxtbox.Update();

            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is open");

                tcmd = ":34#";       // Get temperature
                statustxtbox.Text = "Get BME280 temperature " + tcmd;
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value;

                tcmd = ":32#";       // Get humidity
                statustxtbox.Text = "Get BME280 humidity " + tcmd;
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value;

                tcmd = ":33#";       // Get pressure
                statustxtbox.Text = "Get BME280 pressure " + tcmd;
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value;

                tcmd = ":35#";       // Get dewpoint
                statustxtbox.Text = "Get BME280 dewpoint " + tcmd;
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value;
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is not open");
                statustxtbox.Text = "Com port is not connected.";
                statustxtbox.Update();
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getBME280SensorBtn: END ================================");
        }

        private void luxValuesMenuItem_Click(object sender, EventArgs e)
        {
            // pop up a messagebox
            String txtstr = "0.0001\t\tMoonless, overcast night sky (starlight)" + System.Environment.NewLine +
                "0.002\t\tMoonless clear night sky with airglow" + System.Environment.NewLine +
                "0.05-0.36\t\tFull moon on a clear night" + System.Environment.NewLine +
                "3.4\t\tDark limit of civil twilight under a clear sky" + System.Environment.NewLine +
                "20-50\t\tPublic areas with dark surroundings" + System.Environment.NewLine +
                "50\t\tFamily living room lights" + System.Environment.NewLine +
                "80\t\tOffice building hallway" + System.Environment.NewLine +
                "100\t\tVery dark overcast day" + System.Environment.NewLine +
                "320-500\t\tOffice lighting" + System.Environment.NewLine +
                "400\t\tSunrise or sunset on a clear day" + System.Environment.NewLine +
                "1000\t\tOvercast day" + System.Environment.NewLine +
                "10,000-5,000\tFull daylight (not direct sun)" + System.Environment.NewLine +
                "32,000-100,000\tDirect sunlight" + System.Environment.NewLine +
                "Source: Wikipedia";

            MessageBox.Show(txtstr, "mySQMPro", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void automate30s_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Dataloggingrate = 30000;
            Properties.Settings.Default.Save();
        }

        private void automate1m_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Dataloggingrate = 60000;
            Properties.Settings.Default.Save();
        }

        private void automate5m_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Dataloggingrate = 60000 * 5;
            Properties.Settings.Default.Save();
        }

        private void automate10m_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Dataloggingrate = 60000 * 10;
            Properties.Settings.Default.Save();
        }

        private void automate15m_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Dataloggingrate = 60000 * 15;
            Properties.Settings.Default.Save();
        }

        private void automate30m_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Dataloggingrate = 60000 * 30;
            Properties.Settings.Default.Save();
        }

        private void disableallbuttons()
        {
            // called when autoupdate so user cannot query controller whilst update is happening
            getallBtn.Enabled = false;
            getsqmBtn.Enabled = false;
            GetNELMBtn.Enabled = false;
            getBME280SensorBtn.Enabled = false;
            getRainSensorBtn.Enabled = false;
            getMLX90614Btn.Enabled = false;
            getfirmwareBtn.Enabled = false;
            getfrequencyBtn.Enabled = false;
            getirradianceBtn.Enabled = false;
            getsetpointsBtn.Enabled = false;
            getLuxBtn.Enabled = false;
            setpoint1Btn.Enabled = false;
            setpoint2Btn.Enabled = false;
            Exitbtn.Enabled = false;
            DisconnectBtn.Enabled = false;
            gpsGroupBox.Enabled = false;
        }

        private void enableeallbuttons()
        {
            // called when autoupdate so user cannot query controller whilst update is happening
            getallBtn.Enabled = true;
            getsqmBtn.Enabled = true;
            GetNELMBtn.Enabled = true;
            getBME280SensorBtn.Enabled = true;
            getRainSensorBtn.Enabled = true;
            getMLX90614Btn.Enabled = true;
            getfirmwareBtn.Enabled = true;
            getfrequencyBtn.Enabled = true;
            getirradianceBtn.Enabled = true;
            getsetpointsBtn.Enabled = true;
            getLuxBtn.Enabled = true;
            setpoint1Btn.Enabled = true;
            setpoint2Btn.Enabled = true;
            Exitbtn.Enabled = true;
            DisconnectBtn.Enabled = true;
            gpsGroupBox.Enabled = true;
        }

        private void getdateBtn_Click(object sender, EventArgs e)
        {
            string tcmd;
            string ret;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getdateBtn: START ================================");

            statustxtbox.Clear();
            statustxtbox.Update();

            if (gpspresentChkBox.Checked == false)
                return;

            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is open");
                // get gps date
                tcmd = ":08#";
                statustxtbox.Text = "get gps date";
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value;
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is not open");
                statustxtbox.Text = "Com port is not connected.";
                statustxtbox.Update();
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getdateBtn: END ================================");
        }

        private void gettimeBtn_Click(object sender, EventArgs e)
        {
            string tcmd;
            string ret;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("gettimeBtn: START ================================");
            statustxtbox.Clear();
            statustxtbox.Update();

            if (gpspresentChkBox.Checked == false)
                return;

            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is open");
                // get gps time
                tcmd = ":09#";
                statustxtbox.Text = "Get gps time";
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value;
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is not open");
                statustxtbox.Text = "Com port is not connected.";
                statustxtbox.Update();
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("gettimeBtn: END ================================");
        }

        private void getsatelittesBtn_Click(object sender, EventArgs e)
        {
            string tcmd;
            string ret;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getsatelittesBtn: START ================================");

            statustxtbox.Clear();
            statustxtbox.Update();

            if (gpspresentChkBox.Checked == false)
                return;

            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is open");
                // get satelittes
                tcmd = ":13#";
                statustxtbox.Text = "Get satelittes";
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value;
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is not open");
                statustxtbox.Text = "Com port is not connected.";
                statustxtbox.Update();
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getsatelittesBtn: END ================================");
        }

        private void getlongitudeBtn_Click(object sender, EventArgs e)
        {
            string tcmd;
            string ret;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getlongitudeBtn: START ================================");

            statustxtbox.Clear();
            statustxtbox.Update();

            if (gpspresentChkBox.Checked == false)
                return;

            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is open");
                // get longitude
                tcmd = ":10#";
                statustxtbox.Text = "Get longitude";
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value;
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is not open");
                statustxtbox.Text = "Com port is not connected.";
                statustxtbox.Update();
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getlongitudeBtn: END ================================");
        }

        private void getlatitudeBtn_Click(object sender, EventArgs e)
        {
            string tcmd;
            string ret;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getlatitudeBtn: START ================================");

            statustxtbox.Clear();
            statustxtbox.Update();

            if (gpspresentChkBox.Checked == false)
                return;

            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is open");
                // get latitude
                tcmd = ":11#";
                statustxtbox.Text = "Get latitude";
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value;
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is not open");
                statustxtbox.Text = "Com port is not connected.";
                statustxtbox.Update();
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getlatitudeBtn: END ================================");
        }

        private void getaltitudeBtn_Click(object sender, EventArgs e)
        {
            string tcmd;
            string ret;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getaltitudeBtn: START ================================");

            statustxtbox.Clear();
            statustxtbox.Update();

            if (gpspresentChkBox.Checked == false)
                return;

            if (myserialPort.IsOpen)
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is open");
                // get altitude
                tcmd = ":12#";
                statustxtbox.Text = "Get altitude";
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Calling command string with " + tcmd);
                ret = CommandString(tcmd, true);
                // ignore return value;
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("Serial port is not open");
                statustxtbox.Text = "Com port is not connected.";
                statustxtbox.Update();
                this.Invoke(new EventHandler(DisconnectBtn_Click));
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getaltitudeBtn: END ================================");
        }

        private void ClearALLBtn_Click(object sender, EventArgs e)
        {
            // clear all values
            clearalltextboxes();
        }

        private void getGPSDataBtn_Click(object sender, EventArgs e)
        {
            string tcmd;
            string ret;

            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getGPSDataBtn: START ================================");

            if (gpspresentChkBox.Checked)
            {
                statustxtbox.Clear();
                statustxtbox.Update();
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("GPS is enabled - request to be actioned");

                if (myserialPort.IsOpen)
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("Serial port is open");


                    // get gps date
                    tcmd = ":08#";
                    statustxtbox.Text = "get gps date";
                    statustxtbox.Update();
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("Calling command string with " + tcmd);
                    ret = CommandString(tcmd, true);
                    // ignore return value;

                    // get gps time
                    tcmd = ":09#";
                    statustxtbox.Text = "Get gps time";
                    statustxtbox.Update();
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("Calling command string with " + tcmd);
                    ret = CommandString(tcmd, true);
                    // ignore return value;
                    // get satelittes
                    tcmd = ":13#";
                    statustxtbox.Text = "Get satelittes";
                    statustxtbox.Update();
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("Calling command string with " + tcmd);
                    ret = CommandString(tcmd, true);
                    // ignore return value;
                    // get longitude
                    tcmd = ":10#";
                    statustxtbox.Text = "Get longitude";
                    statustxtbox.Update();
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("Calling command string with " + tcmd);
                    ret = CommandString(tcmd, true);
                    // ignore return value;
                    // get latitude
                    tcmd = ":11#";
                    statustxtbox.Text = "Get latitude";
                    statustxtbox.Update();
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("Calling command string with " + tcmd);
                    ret = CommandString(tcmd, true);
                    // ignore return value;

                    // get altitude
                    tcmd = ":12#";
                    statustxtbox.Text = "Get altitude";
                    statustxtbox.Update();
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("Calling command string with " + tcmd);
                    ret = CommandString(tcmd, true);
                    // ignore return value;

                    // get gps fix
                    tcmd = ":16#";
                    statustxtbox.Text = "get gps date";
                    statustxtbox.Update();
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("Calling command string with " + tcmd);
                    ret = CommandString(tcmd, true);
                    // ignore return value;

                }
                else
                {
                    if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                        LogMessageToErrorFile("Serial port is not open");
                    statustxtbox.Text = "Com port is not connected.";
                    statustxtbox.Update();
                    this.Invoke(new EventHandler(DisconnectBtn_Click));
                }
            }
            else
            {
                if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                    LogMessageToErrorFile("GPS is disabled - request not actioned");
                statustxtbox.Text = "GPS is disabled - request not actioned";
                statustxtbox.Update();
            }
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("getGPSDataBtn: END ================================");
        }

        private void setCloudSensorIILogfilenameMenuItem_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("setCloudSensorIILogfilename: START ================================");

            CSFormActive = true;
            CloudSensorForm = new GetCloudSensorIILogFilename();
            CloudSensorForm.ShowDialog();
            while (CSFormActive == true)
                ;
            // now set filename path
            CSlogfilename = Properties.Settings.Default.CloudSensorIILogFilename;

            // now work out absolute paths and save back into settings
            // get full pathname for datalog
            String PathStr = Properties.Settings.Default.LogPathName;
            int pathlen = PathStr.Length;
            if (pathlen == 3)       // "D:\\" or "C:\\" - root directory
            {
                CSlogfilename = PathStr + CSlogfilename;
            }
            else
            {
                CSlogfilename = PathStr + "\\" + CSlogfilename;
            }
            // remove any extension and then add extension .txt
            if (CSlogfilename.IndexOf('.') > 0)
            {
                CSlogfilename = CSlogfilename.Substring(0, CSlogfilename.IndexOf('.')) + ".txt";
            }
            else
                CSlogfilename = CSlogfilename + ".txt";
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("setCloudSensorIILogfilename: CS log filename = " + CSlogfilename);
            if (Properties.Settings.Default.ErrorLoggingEnabled == true)
                LogMessageToErrorFile("setCloudSensorIILogfilename: END ================================");
        }
    }
}
