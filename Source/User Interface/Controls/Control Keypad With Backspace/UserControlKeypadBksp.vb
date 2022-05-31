Public Class UserControlKeypadBksp
  '    SendKeys.Send("{ENTER}")
  Public Shadows Event KeyPress(ByVal Key As String)

  Public ReadOnly Property Value() As String
    Get
      Value = TextBoxValue.Text
    End Get
  End Property

  Public Sub Clear()
    TextBoxValue.Text = ""
  End Sub

  Private Sub Button0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button0.Click
    TextBoxValue.Text = TextBoxValue.Text & "0"
    SendKeys.Send("0")
  End Sub
  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    TextBoxValue.Text = TextBoxValue.Text & "1"
    SendKeys.Send("1")
  End Sub
  Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    TextBoxValue.Text = TextBoxValue.Text & "2"
    SendKeys.Send("2")
  End Sub
  Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
    TextBoxValue.Text = TextBoxValue.Text & "3"
    SendKeys.Send("3")
  End Sub
  Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
    TextBoxValue.Text = TextBoxValue.Text & "4"
    SendKeys.Send("4")
  End Sub
  Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
    TextBoxValue.Text = TextBoxValue.Text & "5"
    SendKeys.Send("5")
  End Sub
  Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
    TextBoxValue.Text = TextBoxValue.Text & "6"
    SendKeys.Send("6")
  End Sub
  Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
    TextBoxValue.Text = TextBoxValue.Text & "7"
    SendKeys.Send("7")
  End Sub
  Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
    TextBoxValue.Text = TextBoxValue.Text & "8"
    SendKeys.Send("8")
  End Sub
  Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
    TextBoxValue.Text = TextBoxValue.Text & "9"
    SendKeys.Send("9")
  End Sub
  Private Sub ButtonClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClear.Click
    Clear()
  End Sub
  Private Sub ButtonDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDelete.Click
    TextBoxValue.Text = TextBoxValue.Text
    If TextBoxValue.Text.Length > 0 Then
      TextBoxValue.Text = TextBoxValue.Text.Substring(0, TextBoxValue.Text.Length - 1)
    End If
  End Sub


End Class
