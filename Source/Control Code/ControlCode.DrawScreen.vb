Partial Class ControlCode
  ' Up to 15 rows (1-15) on one screen on an 800x600 display
  '   Use \ to get an integer value (no tenths)
  '   Use / to get a double and follow with .ToString("#0.0") to show one decimal place

  <ScreenButton("Main", 1, ButtonImage.Information),
  ScreenButton("Width", 2, ButtonImage.Dial),
  ScreenButton("Ovens 1-4", 3, ButtonImage.Thermometer),
  ScreenButton("Ovens 5-8", 4, ButtonImage.Thermometer),
  ScreenButton("Data", 5, ButtonImage.Information)>
  Public Sub DrawScreen(ByVal screen As Integer, ByVal row() As String) Implements ACControlCode.DrawScreen

    Select Case screen
      Case 1 : DrawScreenMain(row)
      Case 2 : DrawScreenWidths(row)
      Case 3 : DrawScreenOvensEntrance(row)
      Case 4 : DrawScreenOvensExit(row)
      Case 5 : DrawScreenData(row)
    End Select
  End Sub

  Sub DrawScreenMain(ByVal row() As String)
    With Tenter
      row(1) = .IDescription & (" ") & .DisplayStatus
      row(2) = "- Setpoint: " & .SetpointStatusDisplay
      row(3) = "- Actual: " & .IDisplayActual
    End With
    row(4) = ""
    With SelvageLeft
      row(5) = .IDescription & (" ") & .DisplayStatus
      row(6) = "- Setpoint: " & .IDisplaySetpoint
      row(7) = "- Actual: " & .IDisplayActual
    End With
    row(8) = ""
    With SelvageRight
      row(9) = .IDescription & (" ") & .DisplayStatus
      row(10) = "- Setpoint: " & .IDisplaySetpoint
      row(11) = "- Actual: " & .IDisplayActual
    End With
    row(12) = ""
    With OverfeedTop
      row(13) = .IDescription & (" ") & .DisplayStatus
      row(14) = "- Setpoint: " & .IDisplaySetpoint
      row(15) = "- Actual: " & .IDisplayActual
    End With
    row(16) = ""
    With OverfeedBot
      row(17) = .IDescription & (" ") & .DisplayStatus
      row(18) = "- Setpoint: " & .IDisplaySetpoint
      row(19) = "- Actual: " & .IDisplayActual
    End With
    row(20) = ""
    With Padder(1)
      row(21) = .IDescription & (" ") & .DisplayStatus
      row(22) = "- Setpoint: " & .IDisplaySetpoint
      row(23) = "- Actual: " & .IDisplayActual
    End With
    row(24) = ""
    With Padder(2)
      row(25) = .IDescription & (" ") & .DisplayStatus
      row(26) = "- Setpoint: " & .IDisplaySetpoint
      row(27) = "- Actual: " & .IDisplayActual
    End With
  End Sub

  Sub DrawScreenWidths(ByVal row() As String)
    With Width_Screw(1)
      row(1) = .Description & ": " & .SetpointStatus & " (" & .Coms_ScanInterval.ToString & "ms)"
      row(2) = "- Setpoint: " & .DisplaySetpoint
      row(3) = "- Actual: " & .DisplayActual
    End With
    row(4) = ""
    With Width_Screw(2)
      row(5) = .Description & ": " & .SetpointStatus & " (" & .Coms_ScanInterval.ToString & "ms)"
      row(6) = "- Setpoint: " & .DisplaySetpoint
      row(7) = "- Actual: " & .DisplayActual
    End With
    row(8) = ""
    With Width_Screw(3)
      row(9) = .Description & ": " & .SetpointStatus & " (" & .Coms_ScanInterval.ToString & "ms)"
      row(10) = "- Setpoint: " & .DisplaySetpoint
      row(11) = "- Actual: " & .DisplayActual
    End With
    row(12) = ""
    With Width_Screw(4)
      row(13) = .Description & ": " & .SetpointStatus & " (" & .Coms_ScanInterval.ToString & "ms)"
      row(14) = "- Setpoint: " & .DisplaySetpoint
      row(15) = "- Actual: " & .DisplayActual
    End With
    row(16) = ""
    With Width_Screw(5)
      row(17) = .Description & ": " & .SetpointStatus & " (" & .Coms_ScanInterval.ToString & "ms)"
      row(18) = "- Setpoint: " & .DisplaySetpoint
      row(19) = "- Actual: " & .DisplayActual
    End With
  End Sub

  Sub DrawScreenOvensEntrance(ByVal row() As String)
    ' TEMPS 1-4
    With AirTemp_Zone(1)
      row(1) = "Honeywell 1: " & .DisplayStatus & " (" & .Coms_ScanInterval.ToString & "ms)"
      row(2) = "- Setpoint: " & .DisplaySetpoint
      row(3) = "- Actual: " & .DisplayActual
    End With
    row(4) = ""
    With AirTemp_Zone(2)
      row(5) = "Honeywell 2: " & .DisplayStatus & " (" & .Coms_ScanInterval.ToString & "ms)"
      row(6) = "- Setpoint: " & .DisplaySetpoint
      row(7) = "- Actual: " & .DisplayActual
    End With
    row(8) = ""
    With AirTemp_Zone(3)
      row(9) = "Honeywell 3: " & .DisplayStatus & " (" & .Coms_ScanInterval.ToString & "ms)"
      row(10) = "- Setpoint: " & .DisplaySetpoint
      row(11) = "- Actual: " & .DisplayActual
    End With
    row(12) = ""
    With AirTemp_Zone(4)
      row(13) = "Honeywell 4: " & .DisplayStatus & " (" & .Coms_ScanInterval.ToString & "ms)"
      row(14) = "- Setpoint: " & .DisplaySetpoint
      row(15) = "- Actual: " & .DisplayActual
    End With
    row(16) = ""
    row(17) = ""
  End Sub

  Sub DrawScreenOvensExit(ByVal row() As String)
    ' TEMPS 5-8
    With AirTemp_Zone(5)
      row(1) = "Honeywell 5: " & .DisplayStatus & " (" & .Coms_ScanInterval.ToString & "ms)"
      row(2) = "- Setpoint: " & .DisplaySetpoint
      row(3) = "- Actual: " & .DisplayActual
    End With
    row(4) = ""
    With AirTemp_Zone(6)
      row(5) = "Honeywell 6: " & .DisplayStatus & " (" & .Coms_ScanInterval.ToString & "ms)"
      row(6) = "- Setpoint: " & .DisplaySetpoint
      row(7) = "- Actual: " & .DisplayActual
    End With
    row(8) = ""
    With AirTemp_Zone(7)
      row(9) = "Honeywell 7: " & .DisplayStatus & " (" & .Coms_ScanInterval.ToString & "ms)"
      row(10) = "- Setpoint: " & .DisplaySetpoint
      row(11) = "- Actual: " & .DisplayActual
    End With
    row(12) = ""
    With AirTemp_Zone(8)
      row(13) = "Honeywell 8: " & .DisplayStatus & " (" & .Coms_ScanInterval.ToString & "ms)"
      row(14) = "- Setpoint: " & .DisplaySetpoint
      row(15) = "- Actual: " & .DisplayActual
    End With
    row(16) = ""
    row(17) = "Average (6/7/8): " & (Graph_ZoneTempAvg / 10).ToString.PadLeft(3) & "F"
  End Sub

  Sub DrawScreenFabric(ByVal row() As String)
    ' Future Integration with Existing Siemens system discussed
    ' Not Pleva system

    row(1) = "" ' "Zones 2-3: " & (Graph_FabricTemp1 / 10).ToString.PadLeft(5) & "F (" & (IO.PlevaTemp1 / 10).ToString.PadLeft(5) & "C)"
    row(2) = "" '"Zones 3-4: " & (Graph_FabricTemp2 / 10).ToString.PadLeft(5) & "F (" & (IO.PlevaTemp2 / 10).ToString.PadLeft(5) & "C)"
    row(3) = "" '"Zones 4-5: " & (Graph_FabricTemp3 / 10).ToString.PadLeft(5) & "F (" & (IO.PlevaTemp3 / 10).ToString.PadLeft(5) & "C)"
    row(4) = "" '"Zones 5-6: " & (Graph_FabricTemp4 / 10).ToString.PadLeft(5) & "F (" & (IO.PlevaTemp4 / 10).ToString.PadLeft(5) & "C)"

    'Pleva Temperature : Tenter Chain Speed Control
    row(6) = "" ' Pleva.StateStringPT
    row(7) = "" '"Temp Desired: " & (Pleva.TempDesired / 10).ToString.PadLeft(5) & "F (" & (Pleva.TimeAtTempDesired / 10).ToString("#0.0") & " sec)"
    row(8) = "" '"Actual Dwell: " & Pleva.DistanceAtTempStr & " " & Pleva.TimeAtTempActualStr

    row(10) = "" ' "Rate Of Rise Per Zone (" & (Parameters.PlevaZoneLength / 10).ToString & "yds per zone)"
    row(11) = "" '"P1-P2: " & ((Graph_FabricTemp2 - Graph_FabricTemp1) / 10).ToString.PadLeft(5) & "F > " & Math.Round((Graph_FabricTemp2 - Graph_FabricTemp1) \ Parameters.PlevaZoneLength, 2).ToString.PadLeft(3) & "F/yd"
    row(12) = "" ' "P2-P3: " & ((Graph_FabricTemp3 - Graph_FabricTemp2) / 10).ToString.PadLeft(5) & "F > " &                               Math.Round((Graph_FabricTemp3 - Graph_FabricTemp2) \ Parameters.PlevaZoneLength, 2).ToString.PadLeft(3) & "F/yd"
    row(13) = "" ' "P3-P4: " & ((Graph_FabricTemp4 - Graph_FabricTemp3) / 10).ToString.PadLeft(5) & "F > " & Math.Round((Graph_FabricTemp4 - Graph_FabricTemp3) \ Parameters.PlevaZoneLength, 2).ToString.PadLeft(3) & "F/yd"

    'Pleva Humidity : Fan Exhaust 1 Speed Control
    '  row(16) = Pleva.StateStringPH
    '  row(17) = "Exhaust Humidity: " & (Graph_PlevaHumidity / 10).ToString.PadLeft(4) & "g/Kg"
    '  row(18) = "Humidity Desired: " & (Pleva.HumidityDesired / 10).ToString.PadLeft(4) & "g/Kg"

  End Sub

  Sub DrawScreenData(ByVal row() As String)
    If Parent.IsProgramRunning Then
      row(1) = "Program " & Parent.ProgramNumber.ToString & " Running"
      row(2) = "  " & Parent.ProgramName
      If Delay <> DelayValue.NormalRunning Then
        row(4) = "Delayed: " & DelayString
      Else
        row(4) = "Normal Running "
      End If
      row(6) = "Start Time: " & StartTime
      row(7) = "End Time: " & EndTime
      row(8) = "Cycle Time " & TimerString(ProgramRunTimer.Seconds)
      row(10) = "" '"Gas Usage " & (GasUsage.GasUseJob.TotalVolume).ToString.PadLeft(4) & "CF "
      row(11) = "" ' "Gas DecaTherms " & (GasUsage.GasUseJob.DecaTherm.ToString) & "Dth "
      row(12) = "" ' "Gas Flowrate " & (GasUsage.GasUseJob.CubicFeetPerMinute.ToString) & "CFM "
    Else
      row(1) = "Machine Idle: " & ProgramStoppedTimer.ToString
      row(2) = ""
      row(3) = ""
      row(4) = "Previous Cycle Time " & TimerString(LastProgramCycleTime)
      row(5) = ""
      row(6) = ""
      row(7) = "" ' "Gas Usage " & (GasUsage.GasUseDay.TotalVolume).ToString.PadLeft(4) & "CF /Day "
      row(8) = "" ' "Gas DecaTherms " & (GasUsage.GasUseDay.DecaTherm.ToString) & "Dth /Day "
      row(9) = ""
      row(10) = ""
    End If
  End Sub

End Class
