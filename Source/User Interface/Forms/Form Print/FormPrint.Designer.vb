<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPrint
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
    Me.Button1 = New System.Windows.Forms.Button()
    Me.ButtonCancel = New System.Windows.Forms.Button()
    Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
    Me.SuspendLayout()
    '
    'Button1
    '
    Me.Button1.Location = New System.Drawing.Point(316, 109)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(56, 40)
    Me.Button1.TabIndex = 14
    Me.Button1.Text = "Accept"
    '
    'ButtonCancel
    '
    Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.ButtonCancel.Location = New System.Drawing.Point(260, 109)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(56, 40)
    Me.ButtonCancel.TabIndex = 13
    Me.ButtonCancel.Text = "Cancel"
    '
    'PrintDialog1
    '
    Me.PrintDialog1.UseEXDialog = True
    '
    'FormPrint
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.ButtonCancel
    Me.ClientSize = New System.Drawing.Size(384, 161)
    Me.ControlBox = False
    Me.Controls.Add(Me.Button1)
    Me.Controls.Add(Me.ButtonCancel)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FormPrint"
    Me.Text = "Confirm Print"
    Me.ResumeLayout(False)

  End Sub

  Friend WithEvents Button1 As Button
  Friend WithEvents ButtonCancel As Button
  Friend WithEvents PrintDialog1 As PrintDialog
End Class
