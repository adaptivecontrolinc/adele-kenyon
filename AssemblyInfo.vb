Imports System.Reflection
Imports System.Security.Permissions
Imports System.Runtime.InteropServices

<Assembly: ComVisible(False)> 
<Assembly: CLSCompliant(True)> 
<Assembly: SecurityPermission(SecurityAction.RequestMinimum, Execution:=True)>

<Assembly: AssemblyTitle("Adele Kenyon Frame")>
<Assembly: AssemblyCompany("Adele Knits")>
<Assembly: AssemblyCopyright("Portions Copyright 1998-2022 Adaptive Control.")>
<Assembly: AssemblyVersion("1.4.*")>
<Assembly: AssemblyFileVersion("1.4")>

'******************************************************************************************************
'****************                Code Version Notes -- Most Recent At Top              ****************
'****************                                                                      ****************
'****************     Note: Update File Version Above Upon new Release Compilation     ****************
'******************************************************************************************************

' Version 1.4 [2022.05.25] - DH
' Updates to IO Transport PLC1 read and write section

' Version 1.3 [2022.05.24] - DH
' Adding Transport PLC to replace Microspeeds
'   Tenter, TenterLeft, TenterRight, SelvageLeft, SelvageRight, OverfeedTop, OverfeedBottom, Padder1, Padder2



' Version 1.2 [2022.04.19] - DH
' Update mimic to match Adele's Kenyon Frame 2
' Use Percent text with keypad forms instead of % characters due to format issues


' Version 1.1 [2022.03.18] - DH
' Update to Control Configuration to enable setting all Air Temps from default procedure setpoint
'  - Added Supervisor button to control configuration that is used with interfaces

' Add User functionality for Supervisor User to change setpoints beyond procedure tolerances.  
'  - Requires update to IController interface and all locations of usage: ChangeSetpoint, Increase, Decrease, etc.
' Added Control Configuration buttons to change all group setpoints: Width, Air Temp, Fan Top, and Fan Bottom
'  - Adele run standard procedures but will adjust all oven temps or widths for reworks or quick batches



' Version 1.1 [2022.03.14] - DH
' Combine Overfeed Top and Bottom commands into OV
'

' Version 1.1 [2022.03.09] - DH
'  First version placed on PC with Fans, Microspeeds, and Honeywells communicating
'  More updates to come...


' Version 1.1 [2022.03.07] - DH
'  First cut of the code for Adele Knits Kenyon Frame.
'  Beginning with latest Shawmut Kenyon Range code version 1.8 with notes below
'  Pulling version 1.3.6 version with Microspeed controls


' *************************************** BELOW IS FROM GLEN RAVEN KENYON FRAME ***************************************
' Version 1.7 [2021.05.07] - DH
' Add alarms for Fan speed setpoints outside of program tolerances
' Add parameter to enable setpoint adjustment increment on setpoint control 'Setpoint Increment Enable'
'   operators were not paying attention to increment setting and making a change resulting excessive
'   changes.  HC wants increments to remain at 1

' Version 1.6 [2021.04.07] - DH
' Update to not send setpoints to control hardware unless procedure has active command

' Version 1.5 [2021.01.27] - DH

' Version 1.4 [2020.04.13] - DH
' Push to GitHub
' Update version to 2-digit format

'Version 1.3.6 [2018.03.06] - DH
' - Jason Brazington updates his Width Control hardware for more functionality resutling in the width
'   control I/O and class modules requiring changes for added properties and register reads
'
'Version 1.3.5 [2018.02.19] - DH
' Batch Control version 3.2.182
' - Glen Raven removed Microspeeds associated with Width Control and replaced with Jason's Smarty Hardware
'

'Version 1.3.4 [2016.02.06] - DH
' Batch Control version 3.2.166
' Issue with Histories not saving.  Updating Batch Control and Installed Fix No History Windows Update
'

'Version 1.2.8 [2013.10.23] - DH
' Adding Dancer Position Pressure Analog Output as Command setpoint
'   Included in Transport Control PLC Hardware
'   SMC Part#: ITV3050-31N3CS4-X15 (0-10vdc = 0.7 - 150psi)


'Version 1.2.7 [2013.09.27] - DH
' Updated Transport PLC to version 127 including:
'   - counter for digital inputs dropping out, read up as Debug_x0 through Debug X1 for histories
'   - counter rate negative feedback (sign bit) detection > reset counter
'   - Off Delay Timer for digital inputs to help with possible issue where running signal drops out
'       intermittently
' NOTE: was here 8/21 with issue where frame would stop intermittently.  appeared to be an issue with 
'       dry contacts on start interlock circuit.  Jason Brazington was also onsite later after leaving
'       and confirmed theory.  Glen Raven replaced TCR (Tenter Control Relay) in Tenter MCP.
'       Did not replace ATRR, TRR, ARSR relays as suggested.
'       While Onsite today (9/27) frame ran fine during morning while testing.  2nd shift arrived 
'       and tested the updates and were able to replicate the Run/Start Enable dropping out several times
'       Method to replicate: 
'       - Occasionally (1/10 or 1/15 attempts) when pressing the Start or Thread pushbuttons, the necessary 
'         relays (TKR,ATRR,TRR) would not latch.  When this happens, you repeat pressing the start/thread
'         PB (or holding the PB) until the relay latched, and the tenter started correctly.
'       - During this successfull start, you can switch the bead roll In/Out for the Left or Right side,
'         and the start enable will drop out.  If you were running in Run speed, the auxillary run speed
'         relay will remain on (if a proper stop or Estop occurred, this relay would have dropped out)
'       - Also appears that after multiple times replicating this stop issue with the bead motors, the
'         issue goes away possibly because the starters are warmed up and not drawing as much current?
'       - Ray says that he's going to replace all the starters in the MCP this weekend and let me know
'         how it runs afterward.
'         

'Version 1.2.6 [2013.07.19] - DH
' Command OV was defined as OB, not OV - so all OV commands were writing to OverfeedBottom (Not Top)
' Fixed Pleva.vb formKeypad use of decimalPosition with time

'Version 1.2.5 [2013.07.18] - DH
' Pleva.vb update us use Tenter.Var_LimitUpper/Lower as speed fabric max & min values
' Meet w/ Bruce & Chad Cusin to discuss Pleva
'   For TC DevianceHigh=0.0ypm > Want to use DevianceHigh as 0.0 instead of defaulting to standard value
'   For TC DevianceLow=0.0ypm > Want to use SetpointMin as LimitLower (allow to slow completly)
' Reload Pleva Setpoints if LockedSetpoints to ensure current program parameters are used

'Version 1.2.4 [2013.07.16] - DH

'Version 1.2.3 [2013.07.15] - DH
' Updating Pleva.vb for changes due to PLC Transport control 
'   > Active Production Setpoint now 4 instead of SP2
' Renaming PLC Parameters for Motor specific variables to include memory address 
'   > Param_Tc00AutoEnable = Tenter 'v6000' Auto Enable Address
' Updated Control Setpoint Adjust display to only display the Setpoint % for followers, 
'   and not % w/ Speed Desired.  Feedback from operators that 1xx% 120ypm was being trimmed due to length

'Version 1.2.2 [2013.07.12] - DH

'Version 1.2.1 [2013.07.08] - DH

'Version 1.2.0 [2013.07.01] - DH
'  Replacing transport microspeeds with Automation direct PLC w/ 260cpu and EBC-100 coms

'Version 1.1.6 [2013.03.04] - DH

'Version 1.1.4 [2013.03.01] - DH
'  Update [2013-03-01] - Need to further spilt the write update section to account for the active setpoint being used
'     while running (displayvalue>10), to use either the LoadMasterSetpoint or the WriteVariable command in each case
'  New Parameters: TenterMsRequestStopSec, TenterMsSentStopSec

'Version 1.1.2 [2013.02.21] - DH
'  New Parameters: TenterSetpoint3UpdateSec,TenterSetpoint4UpdateSec

' Version 1.1.1 [2013.02.21] - DH
'Issues with tenter setpoint 4 (SP4) being set by Pleva to the calculated desired rate using the last
' update request for SP2, then the next scan using the ExecuteLoadedSetpoint, which changes the current
' SP4 value to the last SP2 value.
' Test for active setpoint to be 2 in Write "Sent" state logic for IO tenter chain.
'Also added more status properties for microspeeds in write section to help toubleshoot 
'  Coms_WriteLast, Coms_WriteLastValue, Coms_WriteLastCommand, Coms_WriteLastResult, etc
'Increase the Pleva.vb delay timer limits to allow for longer intervals between speed adjustments

' Version 1.1.0 [2013.02.20] - DH
'Pleva.vb - Pleva System updating Tenter Chain Setpoint 1 (Thread Speed) when attempting to control Running
'   due to the active setpoint.  correct and only adjust tenter speed if active setpoint = 2 (running)
'Add new parameters for Tenter Chain active setpoints 3 & 4
'   Ray wired Doffin counter to Setpoint 3/4 enable contact on tenter chain microspeed so that, when active
'     active setpoints switched between 1 & 2 to 3 & 4.
'   From Microspeed196.pdf page 7
'     Active Setpoint 1 (terminals 16/17 open)    - Thread Speed (Doffin counter contact open - terminals 7/9 open)
'     Active Setpoint 2 (terminals 16/17 closed)  - Active Speed (Doffin counter contact open - terminals 7/9 open)
'     Active Setpoint 3 (terminals 16/17 open)    - Active Speed (Doffin counter contact closed - terminals 7/9 closed)
'     Active Setpoint 4 (terminals 16/17 closed)  - Active Speed (Doffin counter contact closed - terminals 7/9 closed)
'Control code checks that parameter values haven't changed, and if so, update values at microspeeds
'Only send down thread/setpoint3/setpoint4 parameter values for microspeed setpoints (no controller adjustments)

'One concern is that, SP2 could be set to 30ypm and SP4 could be set to 70ypm - if doffin counter becomes active,
'   and setpoint changed to SP2 to SP4, then the tenter speed would go from 30 to 70.
'   Ray says that Jason has programmed the aux contact on the Doffin counter to only provide a closure if 
'     the speed is above the 70ypm limit.  in this way, i don't have to continuously monitor the last SP2 value
'     and adjust SP4 so that it's always the lower value (SP2 < P_Setpoint4 "Doffin Limit")

'Version 2011.05.20 - DH
' Bruce Needs the Pleva Temperature Control to allow the Tenter Setpoint to slow to the Parameter TenterSetpointMin
'   when no lower setpoint deviance is set.  Was set to the DevianceHigh parameter if the low parameter = 0

'Version 2010.11.29 - DH
'LockSetpoints does not lock Pleva parameters
'  Set up the programs to not lock in the previous programs PLEVA set-points when “lock current transport settings” is selected
'  Bruce Dabbs needs for the new programs PLEVA set-points to always be loaded with each new lot/program scheduled.
'  Email Sent: 11/24/2010 
'  Appears to be an issue with the additional command parameter for Pleva Temp in the form of the tenths place

'Version 2010.11.19 - DH
'Converted to MS Visual Studio 2010 w/ Backup
'Batch Control Version 3.2.79
'  Update Support.vb, MimicControls.vb, Control.vb to latest versions
'Add code to each command to test for parameter array bounds (Test for bad Programs)
'Update PT command, adding "Tenths" parameter to dwell time, now allowing for 0.0-0.9secs
'  "Temp:|0-450|F Time:|0-150|.|0-9|sec"
'Change Max Values for parameters: 
'  PlevaZoneLength - Was 30, now 100 (10.0yards)
'  PlevaSpeedAdjustFactor - was 300, now 1000
'  PlevaSpeedAdjustMax - was 200, now 1000


'Version 2010.07.23 - DH
'GasFlowmeter - fix totalVolumePriorWrap_ += (volumeFromPlc_ - VolumeStart) 
' instead of totalVolumePriorWrap_ += TotalVolume because, when the wrap occurs twice, the TotalVolume
' includes the last PriorWrap value, basicically doubling the usage.
'Fixed the UpdateGasDailyUsage to record the correct "previous" day of usage due to PM/AM shift

'Version 2010.06.25 - DH
'GasFlowmeter - set totalVolumePriorWrap_ = 0 in Public Sub InitializeNewStart(ByVal volume As Integer)
' else, continues to add previous wrap totals into the value

'Version 2010.06.21 - DH
'Update GasUsage.VolumeFromPlc so that, regardless of the value begin valid, if PM/AM shift occurs,
' update the daily gas used value.
'Update GasUsage.UpdateDailyTotal so that the Date and Time are always entered into database.
' date_ = Date.Now.ToShortDateString & " " & Date.Now.ToShortTimeString (ie, ##/##/####  00:00:00)
'New GasFlowmeter.vb object to calculate the total volume per batch (GasUsage.GasUsedJob) and 
' per day (GasUsage.GasUsedDay), which stores the previous volume at wrap for wrapped calculations
' does not use a max counter wrap due to plc configured to wrap at specific time (23hr:00min:01sec) 
' based on volumefromPLC (not raw count calculated) - should also work if volume value wraps as well
'
'Version 2010.06.02 - DH
'Adding more variables to Mimic for Display
'Add TotalVolumeBatchPrevious & TotalVolumePrevDay
'Add GasUsed (CF) to graph for history display
'-GasUsage.LastUpdateDailyTotal to be set when updating, and test within GasUsage to verify has be set in 
'   a reasonable amount of time.  Assist when powering up software to catch latest gasusage for daily
'   totals midday.
'-Pleva Humidity Control - add parameter to enable adjusting both exhaust fan speeds with each pleva 
'   humidity adjustment (same value to both controllers).  if set to '0', only exhaust fan 1 will adjust
'   - This was requested by Bruce Dabbs on 6/2/10
'   - Currently Only one fan speed adjustmen/limitlow/limithigh variable configuration based on the idea
'       that only the front exhaust fan will be adjusted and the assumption that if both fan's are being
'       set to the same new adjustment value, only use the deviation limits for the critical front fan.
'   - May need to add adjustment variables for the 2nd exhaust fan is different setpoints and limits will
'       be used.  (based on current programs, it looks like both ES commands are always the same value)

'Version 2010.03.17 - DH
' Pleva Configuration after initial production testing:
'   Parameters_PlevaSpeedInitializeDelay (15-60) - time once switching to "At Speed" state before first calculation
'   Parameters_PlevaSpedAdjustDelay (1-30) - time between next calculation if no adjustment made (within deadband)
'   Parameters_PlevaSpeedAccelDelay (3-20) - time after sending a new tenter setpoint before next calculation
'   Parameters_PlevaDwellTimeDeadband (2-20) - allowable error time, tenths (0.2-2.0), between actual and desired
'   Parameters_PlevaSpeedAdjustFactor (0-300) - error factor by which to adjust the fabric speed due to time error
'   Parameters_PlevaSpeedAdjustMax (0-200) - Max speed adjustment made, 20.0ypm to current setpoint.
'   TC command added High and Low Deviance to differentiate for Pleva to allow to slow down greater range
'     Bruce Dabbs requested this ability 2009-03-16, due to circumstances where due to wet fabric the speed may
'       need to slow down more than the existing 10ypm deviance, but needs to regulate the high speed.
'     ReImport type library at server and use Programs > Tools > Fix Programs to set the new command param values
'       to '0' else boundary exception will occur in TC command when attepmting to set the new param values
' PT max time allowable now 3 digits (150sec) due to 1% of production using special circumstances (Bruce Dabbs)

'Version 2010.03.16 - DH
'- Configure code to use 'mdf' sql database instead of 'sdf' sql compact database to provide access to a local
'   table for storing gas usage measurments for both batch runs as well as 24hour periods.
'- PC will require 1gb RAM memory upgrade (from 512mb) as well as Sql 2008 Express Installed for operation
'- Add I/O associated with 'First Out Indicator' PLC mounted in remote Burner Control Cabinet

'- Create New Table 'Utilities' containing columns: <Date, DateTime, Null> <GasUsed, Float, Null>
'     "CREATE TABLE Utilities (Date DateTime, GasUsed float)"
'- Add <GasUsed, Float, Null> to dyelots table for batch utilities
'     "ALTER TABLE [dbo].[Dyelots] ADD [GasUsed][float]NULL"

'Version 2010.01.27 - DH
'Fix Worklist to display Program Name & Notes from Program Table by casting Dyelot.Program to be an integer,
' assuming that only one program is scheduled (and not several programs back to back - "1,2,3,...")
' sql string will fail if more than one program is scheduled due to caste function not working with comma's
'Initialize setpoints upon power up so that they have the current values at startup for adjustment capability
'Fix Form Keypad to clear the value and not cancel the form
'Add More Demo values for debugging the software

'Version 2010.01.15 - DH
'Add Pleva Tab (mimic) with new Pleva Temp Plot graph to display rate of rise for each pleva zone input
'Configure Mimic for total frame with generic inputs - will modify/improve 
'Add Double-Click feature to both Pleva TempDesired value on Pleva tab as well as configuration tab for 
'   current setpoints (if enabled through honeywell & microspeed setpointadjust parameters <P_=1>)
'New TenterChain Read Microspeed section to read up the MasterSetpoint 2 (Running Speed Setpoint) even if
'   in Threading (MS1) to be displayed on configuration tab such that operators always know what the Running
'   setpoint is for switching from Thread to Run (this is how the old system was and the operators want it 
'   this way for quick reference, Thread speed should always be 30ypm...)

'Version 2010.01.08 - DH
'Reconfigure Pleva Temperature inputs to be read directly into the IO.Temp range as Celcius values
'Add Pleva.vb Class Module
'On Pleva Control HMI - Parameters are configured as below:
' LengthDryer = 1848cm = 20.21yds
' LengthP1 = 1232cm = 13.47yds
' LengthP2 = 924cm = 10.10yds
' LengthP3 = 616cm = 6.74yds
' LengthP4 = 308cm = 3.37yds
'Begin Working on PlevaControl tab with mimic and pushbutton interaction as well as
'   attempting to design a new control that will simulate real time pleva temps on 
'   temp profile graph.  ToDo - Fix this control and make it work.


'Version 2009.12.17 - DH
'Batch Control Version 3.2.61
'Microspeed Communications Logic Detailed Below:


'Version 2009.09.16 - DH
'Batch Control Version 3.2.55
'Chuck signs off on current command parameter layout including OV/PR/SV % of TenterChain (TC) setpoint
'   ie, OV setpoint of 115% = 1.15 * Setpoint_TenterChain
'Tenter chain maximum speed is 100Yards/min and must be used with all speed conditions including deviance

'Pleva system was tested before, but never fully integrated.
' Goal of system is to determine moisture in exhaust vent stacks, and if:
'     too much moisture - not drying efficiently, slow down tenter chain
'                         must keep in mind that some styles must not slow down too much - use TC Deviance
'     lower moisture - drying more efficienctly, speed tenter chain up
'                         must use TC Deviance and P_MaxTenterSpeed to limit max speed
'     (some styles can run faster than others, and some can run slower than others - will need testing)

'******************************************************************************************************
'****************                     Reasonable Graph Windows Colors                  ****************
'******************************************************************************************************
' Maroon                Teal
' DarkRed               DarkCyan  
' Red                   CadetBlue
' Brown                 SteelBlue
' Firebrick             DodgerBlue
' OrangeRed             MidnightBlue
' Sienna                Blue
' SaddleBrown           DarkSlateBlue
' Peru                  Indigo
' DarkOrange            DarkOrchid
' DarkGoldenrod         DarkViolet
' Goldenrod             BlueViolet
' Olive                 DarkMagenta
' OliveDrab             Crimson              
' DarkOliveGreen        Purple
' ForestGreen           RoyalBlue
' DarkGreen
' Green
' LimeGreen
' Lime
'******************************************************************************************************