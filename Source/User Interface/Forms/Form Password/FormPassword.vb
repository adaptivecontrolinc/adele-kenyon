Public Class FormPassword

  Property Password As String

  Sub New()

    ' This call is required by the designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.ActiveControl = KeyPad

  End Sub

  Private Sub KeyPad_KeyClick(text As String) Handles KeyPad.KeyClick
    With TextBoxPassword
      Select Case text
        Case "{CLEAR}"
          .Text = Nothing
        Case "."
          ' Ignore
        Case Else
          .Text &= text
      End Select
    End With
  End Sub

  Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
    If TextBoxPassword.Text = Password Then
      DialogResult = DialogResult.OK
      Close()
    End If
  End Sub

  Private Sub ButtonCancel_Click_1(sender As Object, e As EventArgs) Handles ButtonCancel.Click
    DialogResult = DialogResult.Cancel
    Close()
  End Sub

#Region " BUTTONS & KEYPAD "

  'Must Set FormKeypad Property "KeyPreview" to True for this to work properly
  Private Sub FormKeypad_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    With TextBoxPassword

      Select Case e.KeyCode
        Case Keys.D0
          .Text = .Text & "0"
        Case Keys.D1
          .Text = .Text & "1"
        Case Keys.D2
          .Text = .Text & "2"
        Case Keys.D3
          .Text = .Text & "3"
        Case Keys.D4
          .Text = .Text & "4"
        Case Keys.D5
          .Text = .Text & "5"
        Case Keys.D6
          .Text = .Text & "6"
        Case Keys.D7
          .Text = .Text & "7"
        Case Keys.D8
          .Text = .Text & "8"
        Case Keys.D9
          .Text = .Text & "9"
        Case Keys.NumPad0
          .Text = .Text & "0"
        Case Keys.NumPad1
          .Text = .Text & "1"
        Case Keys.NumPad2
          .Text = .Text & "2"
        Case Keys.NumPad3
          .Text = .Text & "3"
        Case Keys.NumPad4
          .Text = .Text & "4"
        Case Keys.NumPad5
          .Text = .Text & "5"
        Case Keys.NumPad6
          .Text = .Text & "6"
        Case Keys.NumPad7
          .Text = .Text & "7"
        Case Keys.NumPad8
          .Text = .Text & "8"
        Case Keys.NumPad9
          .Text = .Text & "9"
        Case Keys.Enter
          If TextBoxPassword.Text = Password Then
            DialogResult = DialogResult.OK
            Close()
          End If
        Case Keys.Escape
          DialogResult = DialogResult.Cancel
          Close()
      End Select
    End With
  End Sub

#End Region

End Class