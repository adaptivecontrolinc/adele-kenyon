<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ControlWorkList
  Inherits System.Windows.Forms.UserControl

  'UserControl overrides dispose to clean up the component list.
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
    Me.components = New System.ComponentModel.Container
    Me.timerMain = New System.Windows.Forms.Timer(Me.components)
    Me.controlWorkListScheduled = New ControlWorkListScheduled
    Me.SuspendLayout()
    '
    'timerMain
    '
    Me.timerMain.Enabled = True
    Me.timerMain.Interval = 8000
    '
    'controlWorkListScheduled
    '
    Me.controlWorkListScheduled.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.controlWorkListScheduled.ControlCode = Nothing
    Me.controlWorkListScheduled.Location = New System.Drawing.Point(0, 0)
    Me.controlWorkListScheduled.Name = "controlWorkListScheduled"
    Me.controlWorkListScheduled.Size = New System.Drawing.Size(1022, 676)
    Me.controlWorkListScheduled.TabIndex = 0
    '
    'ControlWorkList
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.controlWorkListScheduled)
    Me.Name = "ControlWorkList"
    Me.Size = New System.Drawing.Size(1022, 676)
    Me.ResumeLayout(False)

  End Sub
  Private WithEvents controlWorkListScheduled As ControlWorkListScheduled
  Private WithEvents timerMain As System.Windows.Forms.Timer

End Class
