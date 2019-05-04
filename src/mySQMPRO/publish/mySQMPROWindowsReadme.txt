mySQMPRO Change Log

// mySQMPRO Windows Application
// Copyright RB Brown, 2017
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

// 0.0.1 06112017
// Initial release