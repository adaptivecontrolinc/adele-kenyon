'******************************************************************************************************
'******                                 Mimic Setup Properties                         ****************
'******************************************************************************************************
'Frame Configuration Setup for Allen-Bradley VersaView 1550M screen
' touchscreen : 1024x768 at 96pixels/inch

'Mimic
'VSD page size: 10.65in x 6.82in 
'PNG size: screen resoloution / 10.65x6.82
'Mimic Size: 1029pixels x 658pixels (no scroll bar when in expert on bottom or side)
'******************************************************************************************************
'******************************************************************************************************
Public Class Mimic
  Public ControlCode As ControlCode
  Public Remoted As Boolean

  Public Sub New()
    DoubleBuffered = True  ' no flicker 
    InitializeComponent()
  End Sub

  Private Sub MimicTimer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
    Run()
  End Sub

  Private Sub Run()
    'There's an odd bug related to boxing the integer values that creates probelms with .ToString
    '  to get round this feature we divide the values by 1 which seems to force the values to be boxed correctly
    Try
      'Make sure we have a connection
      If ControlCode Is Nothing Then Exit Sub

      'Check to see if we are remoted (Plant Explorer / History)
      Remoted = Runtime.Remoting.RemotingServices.IsTransparentProxy(ControlCode)
      If Remoted Then
        ' disable (maybe make invisible) some buttons, etc
        'btTest1.Visible = False
      End If

      With ControlCode

        vlFabricSpeed.Value = .IO.TclSpeedActual / 10 ' .IO.TenterSpeed / 10  ' .Tenter.DisplayActual



        vlFanExhaust1.Value = .IO.FanExhaustActual(1)
        vlFanExhaust2.Value = .IO.FanExhaustActual(2)
        vlOverfeed.Value = .IO.OtSpeedActual / 10 ' .IO.OverfeedTopSpeed / 10    '  .IO.OtSpeedDesired / 10
        vlPadderSpeed1.Value = .IO.Pd1SpeedActual / 10 ' .IO.Padder1Speed / 10
        vlPadderSpeed2.Value = .IO.Pd2SpeedActual / 10 ' .IO.Padder2Speed / 10    ' .IO.PdSpeedDesired / 10


        '   vlPadDancerPosPV.Value = .IO.DancerPosition
        '   vlPadDancerPosSV.Value = .Padder(1).ActiveSetpointValue ' TODO Check .Padder.Prm_DncPosSv
        '   vlPadDancerPressPV.Value = .IO.DancerPressure / 10
        '   vlExtractor.Value = 0 'TODO .IO.ExtractorActual(1) / 10

        vlSelvageLeft.Value = .IO.SlSpeedActual / 10    '  .IO.SlSpeedDesired / 10
        vlSelvageRight.Value = .IO.SlSpeedActual / 10   '  .IO.SrSpeedDesired / 10
        vlTempActualZone1.Value = .IO.AirTempActual(1) / 10
        vlTempActualZone2.Value = .IO.AirTempActual(2) / 10
        vlTempActualZone3.Value = .IO.AirTempActual(3) / 10
        vlTempActualZone4.Value = .IO.AirTempActual(4) / 10
        vlTempActualZone5.Value = .IO.AirTempActual(5) / 10
        vlTempActualZone6.Value = .IO.AirTempActual(6) / 10
        vlTempActualZone7.Value = .IO.AirTempActual(7) / 10
        vlTempActualZone8.Value = .IO.AirTempActual(8) / 10

        vlTempDesiredZone1.Value = .IO.RemoteValue(1) / 10
        vlTempDesiredZone2.Value = .IO.RemoteValue(2) / 10
        vlTempDesiredZone3.Value = .IO.RemoteValue(3) / 10
        vlTempDesiredZone4.Value = .IO.RemoteValue(4) / 10
        vlTempDesiredZone5.Value = .IO.RemoteValue(5) / 10
        vlTempDesiredZone6.Value = .IO.RemoteValue(6) / 10
        vlTempDesiredZone7.Value = .IO.RemoteValue(7) / 10
        vlTempDesiredZone8.Value = .IO.RemoteValue(8) / 10

        vlWidth1.Value = .IO.WidthSetpoint(1) / 10
        vlWidth2.Value = .IO.WidthSetpoint(2) / 10
        vlWidth3.Value = .IO.WidthSetpoint(3) / 10
        vlWidthMain.Value = .IO.WidthSetpoint(4) / 10
        vlWidthRear.Value = .IO.WidthSetpoint(5) / 10


        vlTopFanZone1.Value = .IO.FanTopActual(1)
        vlTopFanZone2.Value = .IO.FanTopActual(2)
        vlTopFanZone3.Value = .IO.FanTopActual(3)
        vlTopFanZone4.Value = .IO.FanTopActual(4)
        vlTopFanZone5.Value = .IO.FanTopActual(5)
        vlTopFanZone6.Value = .IO.FanTopActual(6)
        vlTopFanZone7.Value = .IO.FanTopActual(7)
        vlTopFanZone8.Value = .IO.FanTopActual(8)


        vlBottomFanZone1.Value = .IO.FanBottomActual(1)
        vlBottomFanZone2.Value = .IO.FanBottomActual(2)
        vlBottomFanZone3.Value = .IO.FanBottomActual(3)
        vlBottomFanZone4.Value = .IO.FanBottomActual(4)
        vlBottomFanZone5.Value = .IO.FanBottomActual(5)
        vlBottomFanZone6.Value = .IO.FanBottomActual(6)
        vlBottomFanZone7.Value = .IO.FanBottomActual(7)
        vlBottomFanZone8.Value = .IO.FanBottomActual(8)

        vlFabricSpeed.Value = .IO.TclSpeedActual ' .IO.TenterSpeed                 ' .IO.TcSpeedDesired / 10
        vlFabricSpeedLimitLow.Value = .Tenter.Spt_LimitLower      ' .Pleva.SpeedFabricLimitLow / 10
        vlFabricSpeedLimitHigh.Value = .Tenter.Spt_LimitUpper     ' .Pleva.SpeedFabricLimitHigh / 10




#If 0 Then
        
        '   vlGasUsedCubicFeet.Value = .GasUsage.GasUseJob.TotalVolume
        '   lbGasUsedDecatherms.Text = "DecaTherms: " & (.GasUsage.GasUseJob.DecaTherm.ToString) & " Dth"
        '   vlGasFlowRate.Value = .GasUsage.GasUseJob.CubicFeetPerMinute
               
        vlPlevaTemp1.Value = .Graph_PlevaTemp1 / 10
        vlPlevaTemp2.Value = .Graph_PlevaTemp2 / 10
        vlPlevaTemp3.Value = .Graph_PlevaTemp3 / 10
        vlPlevaTemp4.Value = .Graph_PlevaTemp4 / 10


        'if pleva temp active then
        If .Parameters.PlevaTemperatureEnable <> 1 Then
          vlTempDesired.Visible = False
          vlTimeDesired.Visible = False
          lbTimeActual.Visible = False
          lbFabricDistanceAtTemp.Visible = False
        Else
          vlTempDesired.Visible = True
          vlTimeDesired.Visible = True
          lbTimeActual.Visible = True
          lbFabricDistanceAtTemp.Visible = True
        End If


        lbPlevaTempStatus.Text = .Pleva.StateStringPT
        vlTempDesired.Value = .Pleva.TempDesired / 10
        vlTimeDesired.Value = .Pleva.TimeAtTempDesired / 10

        If .Pleva.TempDesired > 0 AndAlso .Pleva.TimeAtTempDesired > 0 Then
          If Math.Abs(.Pleva.TimeError) < .Parameters.PlevaDwellTimeDeadband Then
            'Within the allowed Desired Time Deadband (seconds)
            lbTimeActual.Text = "Time Actual: " & .Pleva.TimeAtTempActualStr &
                                " [error = " & Math.Round(.Pleva.TimeError / 10, 1).ToString & "sec < " &
                                               Math.Round(.Parameters.PlevaDwellTimeDeadband / 10, 1).ToString & "sec]"
            lbTimeActual.ForeColor = Color.Blue
          Else
            lbTimeActual.Text = "Time Actual: " & .Pleva.TimeAtTempActualStr &
                        " [error = " & Math.Round(.Pleva.TimeError / 10, 1).ToString & "sec > " &
                                       Math.Round(.Parameters.PlevaDwellTimeDeadband / 10, 1).ToString & "sec]"
            lbTimeActual.ForeColor = Color.Red
          End If
        Else
          lbTimeActual.Text = "Time Actual: " & .Pleva.TimeAtTempActualStr
          lbTimeActual.ForeColor = Color.Black
        End If

#End If


#If 0 Then

        If .Parameters.PlevaHumidityEnable <> 1 Then
          lbHumidityActual.Visible = True
          vlHumidityDesired.Visible = True
        Else
          lbHumidityActual.Visible = False
          vlHumidityDesired.Visible = False
        End If

        lbPlevaHumidityStatus.Text = "" ' .Pleva.StateStringPH
        vlHumidityDesired.Value = 0


        If .Pleva.HumidityDesired > 0 Then
          If Math.Abs(.Pleva.HumidityError) < .Parameters.PlevaHumidityDeadband Then
            'Within the allowed Desired Humidity Deadband
            lbHumidityActual.Text = "Humidity Actual: " & (.Pleva.HumidityActual / 10).ToString & "g/kg " &
                                " [error = " & Math.Round(.Pleva.HumidityError / 10, 1).ToString & "g/kg < " &
                                               Math.Round(.Parameters.PlevaHumidityDeadband / 10, 1).ToString & "g/kg]"
            lbHumidityActual.ForeColor = Color.Blue
          Else
            lbHumidityActual.Text = "Humidity Actual: " & (.Pleva.HumidityActual / 10).ToString & "g/kg " &
                                " [error = " & Math.Round(.Pleva.HumidityError / 10, 1).ToString & "g/kg > " &
                                               Math.Round(.Parameters.PlevaHumidityDeadband / 10, 1).ToString & "g/kg]"
            lbHumidityActual.ForeColor = Color.Red
          End If
        End If

#End If

      End With

    Catch ex As Exception

    End Try
  End Sub

  Private Function Message(ByVal text As String) As Boolean
    MessageBox.Show(text.PadRight(64), "Adaptive Control", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
    Return False
  End Function

End Class
