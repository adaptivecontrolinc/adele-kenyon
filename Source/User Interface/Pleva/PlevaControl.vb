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
Public Class PlevaControl
  Private controlCode_ As ControlCode
  Public Property ControlCode() As ControlCode
    Get
      Return controlCode_
    End Get
    Set(ByVal value As ControlCode)
      controlCode_ = value
    End Set
  End Property

  Public Sub New(ByVal controlCode As ControlCode)
    DoubleBuffered = True  ' no flicker 

    controlCode_ = controlCode

    ' Add any initialization after the InitializeComponent() call.
    InitializeComponent()

  End Sub

  Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
    If controlCode_ Is Nothing Then Exit Sub

    If Runtime.Remoting.RemotingServices.IsTransparentProxy(ControlCode) Then
      ' disable (maybe make invisible) some buttons, etc

      btnPlevaTempEnabled.Visible = False
      btnAdjustTemp.Visible = False
      btnTempInc.Visible = False
      btnTempDec.Visible = False
      btnAdjustTime.Visible = False
      btnTimeInc.Visible = False
      btnTimeDec.Visible = False

      btnPlevaHumidityEnabled.Visible = False
      btnAdjustHumidity.Visible = False
      btnHumidityInc.Visible = False
      btnHumidityDec.Visible = False

    End If

    With controlCode_

      vlPlevaTemp1.Value = .Graph_PlevaTemp1 / 10
      vlPlevaTemp2.Value = .Graph_PlevaTemp2 / 10
      vlPlevaTemp3.Value = .Graph_PlevaTemp3 / 10
      vlPlevaTemp4.Value = .Graph_PlevaTemp4 / 10

      'if pleva temp active then
      If .Parameters.PlevaTemperatureEnable <> 1 Then
        btnPlevaTempEnabled.BackColor = Color.Red
        btnPlevaTempEnabled.Text = "Disabled"
      Else
        btnPlevaTempEnabled.BackColor = Color.Transparent
        btnPlevaTempEnabled.Text = "Enabled"
      End If

      lbPlevaTempStatus.Text = ""     ' .Pleva.StateStringPT
      vlTempDesired.Value = 0         ' .Pleva.TempDesired / 10
      vlTimeDesired.Value = 0         ' .Pleva.TimeAtTempDesired / 10

#If 0 Then
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


      If .Parameters.PlevaHumidityEnable <> 1 Then
        btnPlevaHumidityEnabled.BackColor = Color.Red
        btnPlevaHumidityEnabled.Text = "Disabled"
      Else
        btnPlevaHumidityEnabled.BackColor = Color.Transparent
        btnPlevaHumidityEnabled.Text = "Enabled"
      End If

      lbPlevaHumidityStatus.Text = .Pleva.StateStringPH

      vlHumidityDesired.Value = .Pleva.HumidityDesired / 10

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
      Else
        lbHumidityActual.Text = "Humidity Actual: " & (.Pleva.HumidityActual / 10).ToString & "g/kg " & _
                    " [error = " & Math.Round(.Pleva.HumidityError / 10, 1).ToString & "g/kg "
        lbHumidityActual.ForeColor = Color.Black
      End If

      vlFabricSpeed.Value = .IO.TcSpeedDesired / 10
      vlFabricSpeedLimitLow.Value = .Tenter.LimitLower    ' .Pleva.SpeedFabricLimitLow / 10
      vlFabricSpeedLimitHigh.Value = .Tenter.LimitUpper   ' .Pleva.SpeedFabricLimitHigh / 10
      lbFabricDistanceAtTemp.Text = "" ' "Fabric Distance at Desired Temp: " & .Pleva.DistanceAtTempStr

      vlFanExhaustSpeed1.Value = .IO.FanExhaustActual(1) / 10
      lbExhaustSpeed1Limits.Text = "Limit Low: " & (.FanExhaust_Speed(1).SetpointLimitLower / 10).ToString("#0.0") & "% & High: " & (.FanExhaust_Speed(1).SetpointLimitUpper / 10).ToString("#0.0") & "%"
      ' "Limit Low: " & (.Pleva.SpeedFanLimitLow / 10).ToString & "RPM " & "High: " & (.Pleva.SpeedFanLimitHigh / 10).ToString & "RPM"

      vlFanExhaustSpeed2.Value = .IO.FanExhaustActual(2) / 10
      lbExhaustSpeed2Limits.Text = "Limit Low: " & (.FanExhaust_Speed(2).SetpointLimitLower / 10).ToString("#0.0") & "% & High: " & (.FanExhaust_Speed(2).SetpointLimitUpper / 10).ToString("#0.0") & "%"
      'lbExhaustSpeed2Limits.Text = "Limit Low: " & (.Pleva.SpeedFanLimitLow / 10).ToString & "RPM " & "High: " & (.Pleva.SpeedFanLimitHigh / 10).ToString & "RPM"

      vlGasUsedCubicFeet.Value = 0      ' .GasUsage.GasUseJob.TotalVolume
      lbGasUsedDecatherms.Text = ""     ' "DecaTherms: " & (.GasUsage.GasUseJob.DecaTherm.ToString) & " Dth"
      vlGasFlowRate.Value = 0           ' .GasUsage.GasUseJob.CubicFeetPerMinute

      'Air Temp Zone Values
      lbAirZone1.Text = "Z1: " & (.IO.AirTempActual(1) / 10).ToString & " / " & (.IO.RemoteValue(1) / 10).ToString & "F"
      outputZone1.Value = .IO.WorkingOutput(1) / 1000

      lbAirZone2.Text = "Z2: " & (.IO.AirTempActual(2) / 10).ToString & " / " & (.IO.RemoteValue(2) / 10).ToString & "F"
      outputZone2.Value = .IO.WorkingOutput(2) / 1000

      lbAirZone3.Text = "Z3: " & (.IO.AirTempActual(3) / 10).ToString & " / " & (.IO.RemoteValue(3) / 10).ToString & "F"
      outputZone3.Value = .IO.WorkingOutput(3) / 1000

      lbAirZone4.Text = "Z4: " & (.IO.AirTempActual(4) / 10).ToString & " / " & (.IO.RemoteValue(4) / 10).ToString & "F"
      outputZone4.Value = .IO.WorkingOutput(4) / 1000

      lbAirZone5.Text = "Z5: " & (.IO.AirTempActual(5) / 10).ToString & " / " & (.IO.RemoteValue(5) / 10).ToString & "F"
      outputZone5.Value = .IO.WorkingOutput(5) / 1000

      lbAirZone6.Text = "Z6: " & (.IO.AirTempActual(6) / 10).ToString & " / " & (.IO.RemoteValue(6) / 10).ToString & "F"
      outputZone6.Value = .IO.WorkingOutput(6) / 1000

      PlevaTempProfile1.UpdateValues(.Graph_PlevaTemp1, .Graph_PlevaTemp2, .Graph_PlevaTemp3, .Graph_PlevaTemp4)

    End With
  End Sub

  Private Function Message(ByVal text As String) As Boolean
    MessageBox.Show(text.PadRight(64), "Adaptive Control", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
    Return False
  End Function

#Region "Buttons"

  Private Sub BtnPlevaTempEnabled_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlevaTempEnabled.Click
    Try
      With controlCode_
        If .Parameters.PlevaTemperatureEnable <> 1 Then
          .Parameters.PlevaTemperatureEnable = 1
        Else
          .Parameters.PlevaTemperatureEnable = 0
        End If
      End With
    Catch ex As Exception
    End Try
  End Sub

  Private Sub btnTempDec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTempDec.Click
    Try
      With controlCode_
        Dim newSetpoint As Integer = .Pleva.TempDesired - 10
        If newSetpoint >= .Pleva.TempLimitLow Then
          .Pleva.TempDesired -= 10
        Else
          Message("New Setpoint Exceeds Pleva Low Temp Limit: " & (.Pleva.TempLimitLow / 10).ToString & " F")
        End If
      End With
    Catch ex As Exception
    End Try
  End Sub

  Private Sub btnTempInc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTempInc.Click
    Try
      With controlCode_
        Dim newSetpoint As Integer = .Pleva.TempDesired + 10
        If newSetpoint <= .Pleva.TempLimitHigh Then
          .Pleva.TempDesired += 10
        Else
          Message("New Setpoint Exceeds Pleva High Temp Limit: " & (.Pleva.TempLimitHigh / 10).ToString & " F")
        End If
      End With
    Catch ex As Exception
    End Try
  End Sub

  Private Sub BtnAdjustTemp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdjustTemp.Click
    Try
      Using newKeyPad As New FormKeypad(controlCode_.Pleva.TempLimitLow, controlCode_.Pleva.TempLimitHigh, controlCode_.Pleva.TempDesired, "F", 10)
        newKeyPad.ShowDialog()
        If newKeyPad.WasConfirmed Then
          controlCode_.Pleva.TempDesired = newKeyPad.NewSetpoint
          newKeyPad.Close()
        ElseIf newKeyPad.WasCancelled Then
          newKeyPad.Close()
        End If
      End Using
    Catch ex As Exception
      'Some code
    End Try
  End Sub

  Private Sub btnTimeDec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTimeDec.Click
    Try
      With controlCode_
        Dim newSetpoint As Integer = MinMax(.Pleva.TimeAtTempDesired - 1, 0, 1500)
        .Pleva.TimeAtTempDesired = newSetpoint
      End With
    Catch ex As Exception
    End Try
  End Sub

  Private Sub btnTimeInc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTimeInc.Click
    Try
      With controlCode_
        Dim newSetpoint As Integer = MinMax(.Pleva.TimeAtTempDesired + 1, 0, 1500)
        .Pleva.TimeAtTempDesired = newSetpoint
      End With
    Catch ex As Exception
    End Try
  End Sub

  Private Sub BtnAdjustTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdjustTime.Click
    Try
      '150.0sec Limit On Pleva Time Desired (1% of the possible production may be at this dwell time: Bruce Dabbs 2010-03-16)
      Using newKeyPad As New FormKeypad(0, 1500, controlCode_.Pleva.TimeAtTempDesired, "sec")
        newKeyPad.ShowDialog()
        If newKeyPad.WasConfirmed Then
          controlCode_.Pleva.TimeAtTempDesired = newKeyPad.NewSetpoint
          newKeyPad.Close()
        ElseIf newKeyPad.WasCancelled Then
          newKeyPad.Close()
        End If
      End Using
    Catch ex As Exception
      'Some code
    End Try
  End Sub

  Private Sub btnPlevaHumidityEnabled_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlevaHumidityEnabled.Click
    Try
      With controlCode_
        If .Parameters.PlevaHumidityEnable <> 1 Then
          .Parameters.PlevaHumidityEnable = 1
        Else
          .Parameters.PlevaHumidityEnable = 0
        End If
      End With
    Catch ex As Exception
    End Try
  End Sub

  Private Sub btnAdjustHumidity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdjustHumidity.Click
    Try
      Using newKeyPad As New FormKeypad(0, controlCode_.Parameters.PlevaHumidityInputRange, controlCode_.Pleva.HumidityDesired, "g/kg")
        newKeyPad.ShowDialog()
        If newKeyPad.WasConfirmed Then
          controlCode_.Pleva.HumidityDesired = newKeyPad.NewSetpoint
        ElseIf newKeyPad.WasCancelled Then
          newKeyPad.Close()
        End If
      End Using
    Catch ex As Exception
      'Some Code
    End Try
  End Sub

  Private Sub btnHumidityDec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHumidityDec.Click
    Try
      With controlCode_
        Dim newSetpoint As Integer = MinMax(.Pleva.HumidityDesired - 1, 0, ControlCode.Parameters.PlevaHumidityInputRange)
        .Pleva.HumidityDesired = newSetpoint
      End With
    Catch ex As Exception
    End Try
  End Sub

  Private Sub btnHumidityInc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHumidityInc.Click
    Try
      With controlCode_
        Dim newSetpoint As Integer = MinMax(.Pleva.HumidityDesired + 1, 0, ControlCode.Parameters.PlevaHumidityInputRange)
        .Pleva.HumidityDesired = newSetpoint
      End With
    Catch ex As Exception
    End Try
  End Sub

#End Region

End Class
