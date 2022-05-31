<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPassword
  Inherits System.Windows.Forms.Form

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.TextBoxPassword = New System.Windows.Forms.TextBox()
    Me.KeyPad = New KeysNumericClear()
    Me.ButtonOK = New System.Windows.Forms.Button()
    Me.ButtonCancel = New System.Windows.Forms.Button()
    Me.SuspendLayout()
    '
    'TextBoxPassword
    '
    Me.TextBoxPassword.Font = New System.Drawing.Font("Segoe UI", 11.25!)
    Me.TextBoxPassword.Location = New System.Drawing.Point(12, 12)
    Me.TextBoxPassword.Name = "TextBoxPassword"
    Me.TextBoxPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
    Me.TextBoxPassword.Size = New System.Drawing.Size(199, 27)
    Me.TextBoxPassword.TabIndex = 1
    Me.TextBoxPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.TextBoxPassword.UseSystemPasswordChar = True
    '
    'KeyPad
    '
    Me.KeyPad.Location = New System.Drawing.Point(12, 48)
    Me.KeyPad.Name = "KeyPad"
    Me.KeyPad.Size = New System.Drawing.Size(200, 200)
    Me.KeyPad.TabIndex = 2
    '
    'ButtonOK
    '
    Me.ButtonOK.Location = New System.Drawing.Point(12, 254)
    Me.ButtonOK.Name = "ButtonOK"
    Me.ButtonOK.Size = New System.Drawing.Size(90, 40)
    Me.ButtonOK.TabIndex = 3
    Me.ButtonOK.Text = "OK"
    Me.ButtonOK.UseVisualStyleBackColor = True
    '
    'ButtonCancel
    '
    Me.ButtonCancel.Location = New System.Drawing.Point(122, 253)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(90, 40)
    Me.ButtonCancel.TabIndex = 4
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'FormPassword
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(224, 305)
    Me.ControlBox = False
    Me.Controls.Add(Me.ButtonCancel)
    Me.Controls.Add(Me.ButtonOK)
    Me.Controls.Add(Me.KeyPad)
    Me.Controls.Add(Me.TextBoxPassword)
    Me.KeyPreview = True
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FormPassword"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Enter Password"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TextBoxPassword As System.Windows.Forms.TextBox
  Friend WithEvents KeyPad As KeysNumericClear
  Friend WithEvents ButtonOK As Button
  Friend WithEvents ButtonCancel As Button
End Class
