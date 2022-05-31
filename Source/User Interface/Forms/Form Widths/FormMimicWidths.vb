Public Class FormMimicWidths
  Private Labels(5) As LabelSetpoint

  Sub New()
    ' This call is required by the designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Labels(1) = LabelWidth1
    Labels(2) = LabelWidth2
    Labels(3) = LabelWidth3
    Labels(4) = LabelWidth4
    Labels(5) = LabelWidth5
  End Sub

  <Category("Adaptive")>
  Property Units As String
    Get
      Return LabelWidth1.Units
    End Get
    Set(value As String)
      For i As Integer = 1 To 5 : Labels(i).Units = value : Next i
    End Set
  End Property

  <Category("Adaptive")>
  Property Value(index As Integer) As String
    Get
      If index < 1 OrElse index > 5 Then Return Nothing
      Return Labels(index).NewValue
    End Get
    Set(newValue As String)
      If index < 1 OrElse index > 5 Then Exit Property
      Labels(index).CurrentValue = newValue
    End Set
  End Property

  <Category("Adaptive")>
  Property ValueInteger(index As Integer) As Integer
    Get
      If index < 1 OrElse index > 5 Then Return -1
      Return Labels(index).NewValueInteger
    End Get
    Set(newValue As Integer)
      If index < 1 OrElse index > 5 Then Exit Property
      Labels(index).CurrentValue = (newValue).ToString("#0.0")
    End Set
  End Property

  <Category("Adaptive")>
  Property ValueDouble(index As Integer) As Double
    Get
      If index < 1 OrElse index > 5 Then Return -1
      Return Labels(index).NewValueDouble
    End Get
    Set(newValue As Double)
      If index < 1 OrElse index > 5 Then Exit Property
      Labels(index).CurrentValue = (newValue / 10).ToString("#0.0")
    End Set
  End Property

  <Category("Adaptive")>
  Property ValueChecked(index As Integer) As Boolean
    Get
      If index < 1 OrElse index > 5 Then Return False
      Select Case index
        Case 1
          Return CheckBoxWidth1.Checked
        Case 2
          Return CheckBoxWidth2.Checked
        Case 3
          Return CheckBoxWidth3.Checked
        Case 4
          Return CheckBoxWidth4.Checked
        Case 5
          Return CheckBoxWidth5.Checked
      End Select
    End Get
    Set(value As Boolean)
      If index < 1 OrElse index > 5 Then Exit Property
      Select Case index
        Case 1
          CheckBoxWidth1.Checked = value
        Case 2
          CheckBoxWidth2.Checked = value
        Case 3
          CheckBoxWidth3.Checked = value
        Case 4
          CheckBoxWidth4.Checked = value
        Case 5
          CheckBoxWidth5.Checked = value
      End Select
    End Set
  End Property

  <Category("Adaptive")>
  Public MinValue(5) As Integer

  <Category("Adaptive")>
  Public MaxValue(5) As Integer 

  'For convenience
  Sub Setup(text As String, units As String, width1 As Integer, width2 As Integer, width3 As Integer, width4 As Integer, width5 As Integer)
    LabelWidth1.CurrentValue = (width1 / 10).ToString("#0.0")
    LabelWidth2.CurrentValue = (width2 / 10).ToString("#0.0")
    LabelWidth3.CurrentValue = (width3 / 10).ToString("#0.0")
    LabelWidth4.CurrentValue = (width4 / 10).ToString("#0.0")
    LabelWidth5.CurrentValue = (width5 / 10).ToString("#0.0")
  End Sub

  Sub SetupLimits(index As Integer, minValue As Integer, maxValue As Integer)
    If index < 0 OrElse index > Me.MinValue.GetUpperBound(0) Then Exit Sub

    Me.MinValue(index) = minValue
    Me.MaxValue(index) = maxValue
  End Sub


  Private Sub KeyPad_KeyClick(text As String) Handles KeyPad.KeyClick
    If LabelWidth1.Selected Then KeyPadUpdate(LabelWidth1, text)
    If LabelWidth2.Selected Then KeyPadUpdate(LabelWidth2, text)
    If LabelWidth3.Selected Then KeyPadUpdate(LabelWidth3, text)
    If LabelWidth4.Selected Then KeyPadUpdate(LabelWidth4, text)
    If LabelWidth5.Selected Then KeyPadUpdate(LabelWidth5, text)
  End Sub

  Private Sub KeyPadUpdate(labelTarget As LabelSetpoint, text As String)
    With labelTarget
      Select Case text
        Case "{CLEAR}"
          .NewValue = Nothing
        Case "."
          If .NewValue IsNot Nothing AndAlso .NewValue.Contains(".") Then Exit Select  ' There can be only one
          .NewValue &= text
        Case Else
          .NewValue &= text
      End Select
    End With
  End Sub

  Private Sub KeyCancel_KeyClick(sender As Object, text As String) Handles KeyCancel.KeyClick
    DialogResult = DialogResult.Cancel
    Close()
  End Sub

  Private Sub KeyOK_KeyClick(sender As Object, text As String) Handles KeyOK.KeyClick
    Dim message = CheckValues()
    If String.IsNullOrEmpty(message) Then
      DialogResult = DialogResult.OK
      Close()
    Else
      MessageBox.Show(message, "Adaptive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End If
  End Sub

  Private Sub KeyCancel_Click(sender As Object, e As EventArgs) Handles KeyCancel.Click
    DialogResult = DialogResult.Cancel
    Close()
  End Sub

  Private Function CheckValues() As String
    Dim valuesEntered As Boolean = False
    For i As Integer = 1 To 5
      If ValueInteger(i) <> -1 Then
        If ValueInteger(i) < MinValue(i) Then Return "Hinge " & i & " value is too low, it must be greater than or equal to " & MinValue(i).ToString & "."
        If ValueInteger(i) > MaxValue(i) Then Return "Hinge " & i & " value is too high, it must be less than or equal to " & MaxValue(i).ToString & "."
        valuesEntered = True
      End If
    Next
    If Not valuesEntered Then Return "Please enter a value for one of the hinges"

    Return Nothing
  End Function


  Private Sub LabelWidth1_Click(sender As Object, e As EventArgs) Handles LabelWidth1.Click
    LabelWidth1.Selected = True : CheckBoxWidth1.Checked = True
    LabelWidth2.Selected = False
    LabelWidth3.Selected = False
    LabelWidth4.Selected = False
    LabelWidth5.Selected = False
  End Sub

  Private Sub LabelWidth2_Click(sender As Object, e As EventArgs) Handles LabelWidth2.Click
    LabelWidth1.Selected = False
    LabelWidth2.Selected = True : CheckBoxWidth2.Checked = True
    LabelWidth3.Selected = False
    LabelWidth4.Selected = False
    LabelWidth5.Selected = False
  End Sub

  Private Sub LabelWidth3_Click(sender As Object, e As EventArgs) Handles LabelWidth3.Click
    LabelWidth1.Selected = False
    LabelWidth2.Selected = False
    LabelWidth3.Selected = True : CheckBoxWidth3.Checked = True
    LabelWidth4.Selected = False
    LabelWidth5.Selected = False
  End Sub

  Private Sub LabelWidth4_Click(sender As Object, e As EventArgs) Handles LabelWidth4.Click
    LabelWidth1.Selected = False
    LabelWidth2.Selected = False
    LabelWidth3.Selected = False
    LabelWidth4.Selected = True : CheckBoxWidth4.Checked = True
    LabelWidth5.Selected = False
  End Sub

  Private Sub LabelWidth5_Click(sender As Object, e As EventArgs) Handles LabelWidth5.Click
    LabelWidth1.Selected = False
    LabelWidth2.Selected = False
    LabelWidth3.Selected = False
    LabelWidth4.Selected = False
    LabelWidth5.Selected = True : CheckBoxWidth5.Checked = True
  End Sub

  Private Sub CheckBoxWidth1_Click(sender As Object, e As EventArgs) Handles CheckBoxWidth1.Click
    '    CheckBoxWidth1.Checked = Not CheckBoxWidth1.Checked
  End Sub

  Private Sub CheckBoxWidth2_Click(sender As Object, e As EventArgs) Handles CheckBoxWidth2.Click
    '    CheckBoxWidth2.Checked = Not CheckBoxWidth2.Checked
  End Sub

  Private Sub CheckBoxWidth3_Click(sender As Object, e As EventArgs) Handles CheckBoxWidth3.Click
    '    CheckBoxWidth3.Checked = Not CheckBoxWidth3.Checked
  End Sub

  Private Sub CheckBoxWidth4_Click(sender As Object, e As EventArgs) Handles CheckBoxWidth4.Click
    '    CheckBoxWidth4.Checked = Not CheckBoxWidth4.Checked
  End Sub

  Private Sub CheckBoxWidth5_Click(sender As Object, e As EventArgs) Handles CheckBoxWidth5.Click
    '    CheckBoxWidth5.Checked = Not CheckBoxWidth5.Checked
  End Sub

  'Must Set FormKeypad Property "KeyPreview" to True for this to work properly
  Private Sub FormKeypad_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Select Case e.KeyCode
      Case Keys.D0
        KeyPad_KeyClick("0")
        '  Value = Value & "0"
      Case Keys.D1
        KeyPad_KeyClick("1")
        '  Value = Value & "1"
      Case Keys.D2
        KeyPad_KeyClick("2")
        '  Value = Value & "2"
      Case Keys.D3
        KeyPad_KeyClick("3")
        '  Value = Value & "3"
      Case Keys.D4
        KeyPad_KeyClick("4")
        '  Value = Value & "4"
      Case Keys.D5
        KeyPad_KeyClick("5")
        '  Value = Value & "5"
      Case Keys.D6
        KeyPad_KeyClick("6")
        '  Value = Value & "6"
      Case Keys.D7
        KeyPad_KeyClick("7")
        '  Value = Value & "7"
      Case Keys.D8
        KeyPad_KeyClick("8")
        '  Value = Value & "8"
      Case Keys.D9
        KeyPad_KeyClick("9")
        '  Value = Value & "9"
      Case Keys.NumPad0
        KeyPad_KeyClick("0")
        '  Value = Value & "0"
      Case Keys.NumPad1
        KeyPad_KeyClick("1")
        '  Value = Value & "1"
      Case Keys.NumPad2
        KeyPad_KeyClick("2")
        '  Value = Value & "2"
      Case Keys.NumPad3
        KeyPad_KeyClick("3")
        '  Value = Value & "3"
      Case Keys.NumPad4
        KeyPad_KeyClick("4")
        '  Value = Value & "4"
      Case Keys.NumPad5
        KeyPad_KeyClick("5")
        '  Value = Value & "5"
      Case Keys.NumPad6
        KeyPad_KeyClick("6")
        '  Value = Value & "6"
      Case Keys.NumPad7
        KeyPad_KeyClick("7")
        '  Value = Value & "7"
      Case Keys.NumPad8
        KeyPad_KeyClick("8")
        '  Value = Value & "8"
      Case Keys.NumPad9
        KeyPad_KeyClick("9")
        '  Value = Value & "9"
      Case Keys.Enter
        Dim message = CheckValues()
        If String.IsNullOrEmpty(message) Then
          DialogResult = DialogResult.OK
          Close()
        Else
          MessageBox.Show(message, "Adaptive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
      Case Keys.Escape
        DialogResult = DialogResult.Cancel
        Close()
    End Select
  End Sub

End Class