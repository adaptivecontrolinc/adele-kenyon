' Version 2022-04-15
Imports Utilities.Sql

Public Class FormKeypad

  Public Sub New()
    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    InitializeForm()
  End Sub

  Public Sub New(ByVal Title As String, ByVal LowLimit As Integer, ByVal HighLimit As Integer, ByVal CurrentSetpoint As Integer, ByVal Units As String)
    InitializeComponent()

    limitLow_ = LowLimit
    limitHigh_ = HighLimit
    existingSetpoint_ = CurrentSetpoint
    If Units = "%" Then Units = "percent"
    units_ = Units

    ' Added text relavent to the control being adjusted
    If Title <> "" Then Me.Text = Title

    vlSetpointCurrent.Format = "Current Setpoint: 0.0 " & units_
    vlLimitMinimum.Format = "Minimum: 0.0 " & units_
    vlLimitMaximum.Format = "Maximum: 0.0 " & units_
    vlSetpointNew.Format = "New Setpoint: 0.0 " & units_

    InitializeForm()
  End Sub

  Public Sub New(ByVal Title As String, ByVal LowLimit As Integer, ByVal HighLimit As Integer, ByVal CurrentSetpoint As Integer, ByVal Units As String, ByVal NumberScale As Integer)
    InitializeComponent()

    ' Update local properties as defined in tenths (TenterChain: 50 = 5.0ypm)
    limitLow_ = LowLimit
    limitHigh_ = HighLimit
    existingSetpoint_ = CurrentSetpoint
    '  If Units = "%" Then Units = "\" & Units    ?? This worked on my laptop but not on controller - caused strange keypad value behavior??
    If Units = "%" Then
      Units = "percent"
      If NumberScale = 1 Then NumberScale = 10
    End If
    units_ = Units
    numberScale_ = NumberScale

    ' Added text relavent to the control being adjusted
    If Title <> "" Then Me.Text = Title

    vlSetpointCurrent.NumberScale = numberScale_
    vlLimitMinimum.NumberScale = numberScale_
    vlLimitMaximum.NumberScale = numberScale_
    vlSetpointNew.NumberScale = numberScale_

    If numberScale_ = 1 Then
      ' No decimal position, number is written as shown on Microspeed (TenterChain: 5 = 5ypm)
      ' All values are expected to be in tenths, so we need to keep the value label number scale in 10 (or more), but drop the decimal place in the string
      vlSetpointCurrent.Format = "Current Setpoint: 0 " & units_
      vlLimitMinimum.Format = "Minimum: 0 " & units_
      vlLimitMaximum.Format = "Maximum: 0 " & units_
      vlSetpointNew.Format = "New Setpoint: 0 " & units_
    Else
      vlSetpointCurrent.Format = "Current Setpoint: 0.0 " & units_
      vlLimitMinimum.Format = "Minimum: 0.0 " & units_
      vlLimitMaximum.Format = "Maximum: 0.0 " & units_
      vlSetpointNew.Format = "New Setpoint: 0.0 " & units_
    End If

    InitializeForm()
  End Sub

  Private Sub InitializeForm()

    'Setup Labels
    labelLimits.Text = "Limits - "

    vlSetpointCurrent.Value = existingSetpoint_
    vlLimitMinimum.Value = limitLow_
    vlLimitMaximum.Value = limitHigh_

    'Set timer interval and enable timer
    TimerMain.Interval = 1000
    TimerMain.Enabled = True
  End Sub

#Region "Properties"

  Private value_ As String
  Public Property Value() As String
    Get
      Return value_
    End Get
    Set(ByVal value As String)
      value_ = value
      newSetpoint_ = NullToZeroInteger(value_)
      vlSetpointNew.Value = newSetpoint_
    End Set
  End Property

  Private description_ As String = "Adjust Setpoint"
  Public ReadOnly Property Description As String
    Get
      Return description_
    End Get
  End Property

  Private limitLow_ As Integer
  Public ReadOnly Property LimitLow() As Integer
    Get
      Return limitLow_
    End Get
  End Property

  Private limitHigh_ As Integer
  Public ReadOnly Property LimitHigh() As Integer
    Get
      Return limitHigh_
    End Get
  End Property

  Private existingSetpoint_ As Integer
  Public ReadOnly Property ExistingSetpoint() As Integer
    Get
      Return existingSetpoint_
    End Get
  End Property

  Private units_ As String
  Public ReadOnly Property Units() As String
    Get
      Return units_
    End Get
  End Property

  Private numberScale_ As Integer
  Public ReadOnly Property NumberScale() As Integer
    Get
      Return numberScale_
    End Get
  End Property

  Private newSetpoint_ As Integer
  Public ReadOnly Property NewSetpoint() As Integer
    Get
      Return newSetpoint_
    End Get
  End Property

  Private wasCancelled_ As Boolean
  Public Property WasCancelled() As Boolean
    Get
      Return wasCancelled_
    End Get
    Set(ByVal value As Boolean)
      wasCancelled_ = value
    End Set
  End Property

  Private wasConfirmed_ As Boolean
  Public Property WasConfirmed() As Boolean
    Get
      Return wasConfirmed_
    End Get
    Set(ByVal value As Boolean)
      wasConfirmed_ = value
    End Set
  End Property

#End Region

#Region " BUTTONS & KEYPAD "

  Public Shadows Event KeyPress(ByVal Key As String)
  Private Sub Button0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button0.Click
    Value = Value & "0"
    RaiseEvent KeyPress("0")
  End Sub
  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    Value = Value & "1"
    RaiseEvent KeyPress("1")
  End Sub
  Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    Value = Value & "2"
    RaiseEvent KeyPress("2")
  End Sub
  Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
    Value = Value & "3"
    RaiseEvent KeyPress("3")
  End Sub
  Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
    Value = Value & "4"
    RaiseEvent KeyPress("4")
  End Sub
  Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
    Value = Value & "5"
    RaiseEvent KeyPress("5")
  End Sub
  Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
    Value = Value & "6"
    RaiseEvent KeyPress("6")
  End Sub
  Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
    Value = Value & "7"
    RaiseEvent KeyPress("7")
  End Sub
  Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
    Value = Value & "8"
    RaiseEvent KeyPress("8")
  End Sub
  Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
    Value = Value & "9"
    RaiseEvent KeyPress("9")
  End Sub
  Private Sub ButtonClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClear.Click
    Value = ""
  End Sub
  Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
    Try
      Value = "0"
      wasCancelled_ = True
      RaiseEvent KeyPress("Cancel")
      Me.Close()
    Catch ex As Exception
    End Try
  End Sub
  Private Sub ButtonAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAccept.Click
    Try
      If CheckLimits() Then
        wasConfirmed_ = True
        Me.Close()
      End If
    Catch ex As Exception
    End Try
  End Sub

  'Must Set FormKeypad Property "KeyPreview" to True for this to work properly
  Private Sub FormKeypad_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Select Case e.KeyCode
      Case Keys.D0
        Value = Value & "0"
      Case Keys.D1
        Value = Value & "1"
      Case Keys.D2
        Value = Value & "2"
      Case Keys.D3
        Value = Value & "3"
      Case Keys.D4
        Value = Value & "4"
      Case Keys.D5
        Value = Value & "5"
      Case Keys.D6
        Value = Value & "6"
      Case Keys.D7
        Value = Value & "7"
      Case Keys.D8
        Value = Value & "8"
      Case Keys.D9
        Value = Value & "9"
      Case Keys.NumPad0
        Value = Value & "0"
      Case Keys.NumPad1
        Value = Value & "1"
      Case Keys.NumPad2
        Value = Value & "2"
      Case Keys.NumPad3
        Value = Value & "3"
      Case Keys.NumPad4
        Value = Value & "4"
      Case Keys.NumPad5
        Value = Value & "5"
      Case Keys.NumPad6
        Value = Value & "6"
      Case Keys.NumPad7
        Value = Value & "7"
      Case Keys.NumPad8
        Value = Value & "8"
      Case Keys.NumPad9
        Value = Value & "9"
      Case Keys.Enter
        If CheckLimits() Then
          wasConfirmed_ = True
          Me.Close()
        End If
      Case Keys.Escape
        Value = "0"
        wasCancelled_ = True
        Me.Close()
    End Select
  End Sub

#If 0 Then
  Private Sub FormKeypad_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs)Handles MyBase.PreviewKeyDown
    Exit Sub
    Dim messageBoxVB As New System.Text.StringBuilder()
    messageBoxVB.AppendFormat("{0} = {1}", "Alt", e.Alt)
    messageBoxVB.AppendLine()
    messageBoxVB.AppendFormat("{0} = {1}", "Control", e.Control)
    messageBoxVB.AppendLine()
    messageBoxVB.AppendFormat("{0} = {1}", "KeyCode", e.KeyCode)
    messageBoxVB.AppendLine()
    messageBoxVB.AppendFormat("{0} = {1}", "KeyValue", e.KeyValue)
    messageBoxVB.AppendLine()
    messageBoxVB.AppendFormat("{0} = {1}", "KeyData", e.KeyData)
    messageBoxVB.AppendLine()
    messageBoxVB.AppendFormat("{0} = {1}", "Modifiers", e.Modifiers)
    messageBoxVB.AppendLine()
    messageBoxVB.AppendFormat("{0} = {1}", "Shift", e.Shift)
    messageBoxVB.AppendLine()
    messageBoxVB.AppendFormat("{0} = {1}", "IsInputKey", e.IsInputKey)
    messageBoxVB.AppendLine()
    MessageBox.Show(messageBoxVB.ToString(), "PreviewKeyDown Event")
    End Sub
#End If


  Private Sub Button0_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button0.MouseDown
    Button0.BackColor = Color.Blue
  End Sub
  Private Sub Button0_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button0.MouseUp
    Button0.BackColor = Color.Gainsboro
  End Sub

  Private Sub Button1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button1.MouseDown
    Button1.BackColor = Color.Blue
  End Sub
  Private Sub Button1_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button1.MouseUp
    Button1.BackColor = Color.Gainsboro
  End Sub

  Private Sub Button2_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button2.MouseDown
    Button2.BackColor = Color.Blue
  End Sub
  Private Sub Button2_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button2.MouseUp
    Button2.BackColor = Color.Gainsboro
  End Sub

  Private Sub Button3_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button3.MouseDown
    Button3.BackColor = Color.Blue
  End Sub
  Private Sub Button3_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button3.MouseUp
    Button3.BackColor = Color.Gainsboro
  End Sub

  Private Sub Button4_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button4.MouseDown
    Button4.BackColor = Color.Blue
  End Sub
  Private Sub Button4_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button4.MouseUp
    Button4.BackColor = Color.Gainsboro
  End Sub

  Private Sub Button5_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button5.MouseDown
    Button5.BackColor = Color.Blue
  End Sub
  Private Sub Button5_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button5.MouseUp
    Button5.BackColor = Color.Gainsboro
  End Sub

  Private Sub Button6_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button6.MouseDown
    Button6.BackColor = Color.Blue
  End Sub
  Private Sub Button6_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button6.MouseUp
    Button6.BackColor = Color.Gainsboro
  End Sub

  Private Sub Button7_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button7.MouseDown
    Button7.BackColor = Color.Blue
  End Sub
  Private Sub Button7_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button7.MouseUp
    Button7.BackColor = Color.Gainsboro
  End Sub

  Private Sub Button8_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button8.MouseDown
    Button8.BackColor = Color.Blue
  End Sub
  Private Sub Button8_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button8.MouseUp
    Button8.BackColor = Color.Gainsboro
  End Sub

  Private Sub Button9_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button9.MouseDown
    Button9.BackColor = Color.Blue
  End Sub
  Private Sub Button9_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button9.MouseUp
    Button9.BackColor = Color.Gainsboro
  End Sub

  Private Sub ButtonClear_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ButtonClear.MouseDown
    ButtonClear.BackColor = Color.Blue
  End Sub
  Private Sub ButtonClear_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ButtonClear.MouseUp
    ButtonClear.BackColor = Color.Gainsboro
  End Sub
  Private Sub ButtonCancel_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ButtonCancel.MouseDown
    ButtonCancel.BackColor = Color.Blue
  End Sub
  Private Sub ButtonCancel_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ButtonCancel.MouseUp
    ButtonCancel.BackColor = Color.Gainsboro
  End Sub
  Private Sub ButtonAccept_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ButtonAccept.MouseDown
    ButtonAccept.BackColor = Color.Blue
  End Sub
  Private Sub ButtonAccept_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ButtonAccept.MouseUp
    ButtonAccept.BackColor = Color.Gainsboro
  End Sub

#End Region

  Private Function CheckLimits() As Boolean
    Try

      'Check New Setpoint against provided High/Low Limits
      If newSetpoint_ < limitLow_ Then
        Value = ""
        Return Message("Setpoint Low.  Please enter a valid number from " &
             (limitLow_ / 10).ToString & " " & units_ & " to " & (limitHigh_ / 10).ToString & " " & units_ & ".")
      ElseIf newSetpoint_ > limitHigh_ Then
        Value = ""
        Return Message("Setpoint High.  Please enter a valid number from " &
             (limitLow_ / 10).ToString & " " & units_ & " to " & (limitHigh_ / 10).ToString & " " & units_ & ".")
      Else
        Return True
      End If

    Catch ex As Exception
      'some code
    End Try
    Return False
  End Function

  Private Function Message(ByVal text As String) As Boolean
    MessageBox.Show(text.PadRight(64), "Adaptive Control", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
    Return False
  End Function

  Private Sub TimerMain_Tick(sender As Object, e As EventArgs) Handles TimerMain.Tick
    vlSetpointNew.Value = newSetpoint_
  End Sub

End Class