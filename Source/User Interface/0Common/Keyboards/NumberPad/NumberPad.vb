Public Class NumberPad : Inherits BaseControl1

  Private WithEvents TextBoxValue As TextBox
  Private WithEvents Keys As KeysNumericDel

  Protected Overrides Sub AddControls()
    TextBoxValue = New TextBox
    With TextBoxValue
      .Location = New Point(0, 0)
      .Width = Width

      .Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
      .Font = New Font(Me.Font.FontFamily, 10)
    End With
    Controls.Add(TextBoxValue)

    Keys = New KeysNumericDel
    With Keys
      .Location = New Point(0, TextBoxValue.Height + 4)
      .Width = Width
      .Height = Height - TextBoxValue.Height - 4

      .Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
    End With
    Controls.Add(Keys)

  End Sub

  Private Sub Keys_KeyClick(text As String) Handles Keys.KeyClick

    If text = "{BACKSPACE}" Then
      If TextBoxValue.TextLength > 0 Then TextBoxValue.Text = TextBoxValue.Text.Substring(0, TextBoxValue.TextLength - 1)
    Else
      TextBoxValue.Text &= text
    End If
  End Sub

  ReadOnly Property Value As String
    Get
      Return TextBoxValue.Text
    End Get
  End Property
End Class
