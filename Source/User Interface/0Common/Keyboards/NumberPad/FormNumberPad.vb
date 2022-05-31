Public Class FormNumberPad : Inherits BaseForm

  Private WithEvents NumberPad As NumberPad
  Private WithEvents ButtonOK As Button
  Private WithEvents ButtonCancel As Button

  Protected Overrides Sub AddControls()
    ClientSize = New Size(252, 300)
    Dim buttonWidth = 100
    Dim buttonHeight = 32

    NumberPad = New NumberPad
    With NumberPad
      .Location = New Point(8, 8)
      .Size = New Size(ClientSize.Width - 16, ClientSize.Height - (buttonHeight + 32))
      .Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

    End With
    Controls.Add(NumberPad)

    ButtonOK = New Button
    With ButtonOK
      .Size = New Size(buttonWidth, buttonHeight)
      .Location = New Point(8, ClientSize.Height - .Height - 8)
      .Anchor = AnchorStyles.Bottom Or AnchorStyles.Left

      .Text = "OK"
    End With
    Controls.Add(ButtonOK)

    ButtonCancel = New Button
    With ButtonCancel
      .Size = New Size(buttonWidth, buttonHeight)
      .Location = New Point(ClientSize.Width - .Width - 8, ClientSize.Height - .Height - 8)
      .Anchor = AnchorStyles.Bottom Or AnchorStyles.Left

      .Text = "Cancel"
    End With
    Controls.Add(ButtonCancel)
  End Sub

  Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
    If NumberPad.Value.Length > 0 Then
      Me.DialogResult = DialogResult.OK
      Close()
    End If
  End Sub

  Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
    Me.DialogResult = DialogResult.Cancel
    Close()
  End Sub

  ReadOnly Property Value As String
    Get
      Return NumberPad.Value
    End Get
  End Property

End Class
