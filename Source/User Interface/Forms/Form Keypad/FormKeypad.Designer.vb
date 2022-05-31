<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormKeypad
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
    Me.components = New System.ComponentModel.Container()
    Me.ButtonAccept = New System.Windows.Forms.Button()
    Me.Button0 = New System.Windows.Forms.Button()
    Me.ButtonCancel = New System.Windows.Forms.Button()
    Me.Button9 = New System.Windows.Forms.Button()
    Me.Button8 = New System.Windows.Forms.Button()
    Me.Button7 = New System.Windows.Forms.Button()
    Me.Button6 = New System.Windows.Forms.Button()
    Me.Button5 = New System.Windows.Forms.Button()
    Me.Button4 = New System.Windows.Forms.Button()
    Me.Button3 = New System.Windows.Forms.Button()
    Me.Button2 = New System.Windows.Forms.Button()
    Me.Button1 = New System.Windows.Forms.Button()
    Me.ButtonClear = New System.Windows.Forms.Button()
    Me.vlSetpointNew = New MimicControls.ValueLabel()
    Me.vlSetpointCurrent = New MimicControls.ValueLabel()
    Me.vlLimitMaximum = New MimicControls.ValueLabel()
    Me.vlLimitMinimum = New MimicControls.ValueLabel()
    Me.labelLimits = New MimicControls.Label()
    Me.TimerMain = New System.Windows.Forms.Timer(Me.components)
    Me.SuspendLayout()
    '
    'ButtonAccept
    '
    Me.ButtonAccept.BackColor = System.Drawing.Color.Gainsboro
    Me.ButtonAccept.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.ButtonAccept.Location = New System.Drawing.Point(137, 459)
    Me.ButtonAccept.Margin = New System.Windows.Forms.Padding(4)
    Me.ButtonAccept.Name = "ButtonAccept"
    Me.ButtonAccept.Size = New System.Drawing.Size(126, 61)
    Me.ButtonAccept.TabIndex = 24
    Me.ButtonAccept.TabStop = False
    Me.ButtonAccept.Text = "Accept"
    Me.ButtonAccept.UseVisualStyleBackColor = False
    '
    'Button0
    '
    Me.Button0.BackColor = System.Drawing.Color.Gainsboro
    Me.Button0.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Button0.Location = New System.Drawing.Point(5, 390)
    Me.Button0.Margin = New System.Windows.Forms.Padding(4)
    Me.Button0.Name = "Button0"
    Me.Button0.Size = New System.Drawing.Size(81, 61)
    Me.Button0.TabIndex = 22
    Me.Button0.TabStop = False
    Me.Button0.Text = "0"
    Me.Button0.UseVisualStyleBackColor = False
    '
    'ButtonCancel
    '
    Me.ButtonCancel.BackColor = System.Drawing.Color.Gainsboro
    Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.ButtonCancel.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.ButtonCancel.Location = New System.Drawing.Point(5, 459)
    Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(4)
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.Size = New System.Drawing.Size(125, 61)
    Me.ButtonCancel.TabIndex = 23
    Me.ButtonCancel.TabStop = False
    Me.ButtonCancel.Text = "Cancel"
    Me.ButtonCancel.UseVisualStyleBackColor = False
    '
    'Button9
    '
    Me.Button9.BackColor = System.Drawing.Color.Gainsboro
    Me.Button9.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Button9.Location = New System.Drawing.Point(182, 322)
    Me.Button9.Margin = New System.Windows.Forms.Padding(4)
    Me.Button9.Name = "Button9"
    Me.Button9.Size = New System.Drawing.Size(81, 61)
    Me.Button9.TabIndex = 21
    Me.Button9.TabStop = False
    Me.Button9.Text = "9"
    Me.Button9.UseVisualStyleBackColor = False
    '
    'Button8
    '
    Me.Button8.BackColor = System.Drawing.Color.Gainsboro
    Me.Button8.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Button8.Location = New System.Drawing.Point(93, 322)
    Me.Button8.Margin = New System.Windows.Forms.Padding(4)
    Me.Button8.Name = "Button8"
    Me.Button8.Size = New System.Drawing.Size(81, 61)
    Me.Button8.TabIndex = 20
    Me.Button8.TabStop = False
    Me.Button8.Text = "8"
    Me.Button8.UseVisualStyleBackColor = False
    '
    'Button7
    '
    Me.Button7.BackColor = System.Drawing.Color.Gainsboro
    Me.Button7.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Button7.Location = New System.Drawing.Point(5, 322)
    Me.Button7.Margin = New System.Windows.Forms.Padding(4)
    Me.Button7.Name = "Button7"
    Me.Button7.Size = New System.Drawing.Size(81, 61)
    Me.Button7.TabIndex = 19
    Me.Button7.TabStop = False
    Me.Button7.Text = "7"
    Me.Button7.UseVisualStyleBackColor = False
    '
    'Button6
    '
    Me.Button6.BackColor = System.Drawing.Color.Gainsboro
    Me.Button6.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Button6.Location = New System.Drawing.Point(182, 253)
    Me.Button6.Margin = New System.Windows.Forms.Padding(4)
    Me.Button6.Name = "Button6"
    Me.Button6.Size = New System.Drawing.Size(81, 61)
    Me.Button6.TabIndex = 18
    Me.Button6.TabStop = False
    Me.Button6.Text = "6"
    Me.Button6.UseVisualStyleBackColor = False
    '
    'Button5
    '
    Me.Button5.BackColor = System.Drawing.Color.Gainsboro
    Me.Button5.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Button5.Location = New System.Drawing.Point(93, 253)
    Me.Button5.Margin = New System.Windows.Forms.Padding(4)
    Me.Button5.Name = "Button5"
    Me.Button5.Size = New System.Drawing.Size(81, 61)
    Me.Button5.TabIndex = 17
    Me.Button5.TabStop = False
    Me.Button5.Text = "5"
    Me.Button5.UseVisualStyleBackColor = False
    '
    'Button4
    '
    Me.Button4.BackColor = System.Drawing.Color.Gainsboro
    Me.Button4.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Button4.Location = New System.Drawing.Point(5, 253)
    Me.Button4.Margin = New System.Windows.Forms.Padding(4)
    Me.Button4.Name = "Button4"
    Me.Button4.Size = New System.Drawing.Size(81, 61)
    Me.Button4.TabIndex = 16
    Me.Button4.TabStop = False
    Me.Button4.Text = "4"
    Me.Button4.UseVisualStyleBackColor = False
    '
    'Button3
    '
    Me.Button3.BackColor = System.Drawing.Color.Gainsboro
    Me.Button3.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Button3.Location = New System.Drawing.Point(182, 184)
    Me.Button3.Margin = New System.Windows.Forms.Padding(4)
    Me.Button3.Name = "Button3"
    Me.Button3.Size = New System.Drawing.Size(81, 61)
    Me.Button3.TabIndex = 15
    Me.Button3.TabStop = False
    Me.Button3.Text = "3"
    Me.Button3.UseVisualStyleBackColor = False
    '
    'Button2
    '
    Me.Button2.BackColor = System.Drawing.Color.Gainsboro
    Me.Button2.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Button2.Location = New System.Drawing.Point(93, 184)
    Me.Button2.Margin = New System.Windows.Forms.Padding(4)
    Me.Button2.Name = "Button2"
    Me.Button2.Size = New System.Drawing.Size(81, 61)
    Me.Button2.TabIndex = 14
    Me.Button2.TabStop = False
    Me.Button2.Text = "2"
    Me.Button2.UseVisualStyleBackColor = False
    '
    'Button1
    '
    Me.Button1.BackColor = System.Drawing.Color.Gainsboro
    Me.Button1.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Button1.Location = New System.Drawing.Point(5, 184)
    Me.Button1.Margin = New System.Windows.Forms.Padding(4)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(81, 61)
    Me.Button1.TabIndex = 13
    Me.Button1.TabStop = False
    Me.Button1.Text = "1"
    Me.Button1.UseVisualStyleBackColor = False
    '
    'ButtonClear
    '
    Me.ButtonClear.BackColor = System.Drawing.Color.Gainsboro
    Me.ButtonClear.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.ButtonClear.Location = New System.Drawing.Point(94, 390)
    Me.ButtonClear.Margin = New System.Windows.Forms.Padding(4)
    Me.ButtonClear.Name = "ButtonClear"
    Me.ButtonClear.Size = New System.Drawing.Size(169, 61)
    Me.ButtonClear.TabIndex = 30
    Me.ButtonClear.TabStop = False
    Me.ButtonClear.Text = "Clear"
    Me.ButtonClear.UseVisualStyleBackColor = False
    '
    'vlSetpointNew
    '
    Me.vlSetpointNew.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlSetpointNew.ForeColor = System.Drawing.Color.Blue
    Me.vlSetpointNew.Format = "New Setpoint: 0.0"
    Me.vlSetpointNew.Location = New System.Drawing.Point(13, 145)
    Me.vlSetpointNew.Name = "vlSetpointNew"
    Me.vlSetpointNew.Size = New System.Drawing.Size(137, 19)
    Me.vlSetpointNew.TabIndex = 36
    '
    'vlSetpointCurrent
    '
    Me.vlSetpointCurrent.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlSetpointCurrent.ForeColor = System.Drawing.Color.Black
    Me.vlSetpointCurrent.Format = "Current Setpoint: 0.0"
    Me.vlSetpointCurrent.Location = New System.Drawing.Point(11, 13)
    Me.vlSetpointCurrent.Name = "vlSetpointCurrent"
    Me.vlSetpointCurrent.Size = New System.Drawing.Size(159, 19)
    Me.vlSetpointCurrent.TabIndex = 35
    '
    'vlLimitMaximum
    '
    Me.vlLimitMaximum.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlLimitMaximum.ForeColor = System.Drawing.Color.Black
    Me.vlLimitMaximum.Format = "Maximum: 0.0"
    Me.vlLimitMaximum.Location = New System.Drawing.Point(37, 110)
    Me.vlLimitMaximum.Name = "vlLimitMaximum"
    Me.vlLimitMaximum.Size = New System.Drawing.Size(112, 19)
    Me.vlLimitMaximum.TabIndex = 34
    '
    'vlLimitMinimum
    '
    Me.vlLimitMinimum.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlLimitMinimum.ForeColor = System.Drawing.Color.Black
    Me.vlLimitMinimum.Format = "Minimum: 0.0"
    Me.vlLimitMinimum.Location = New System.Drawing.Point(37, 83)
    Me.vlLimitMinimum.Name = "vlLimitMinimum"
    Me.vlLimitMinimum.Size = New System.Drawing.Size(127, 19)
    Me.vlLimitMinimum.TabIndex = 33
    Me.vlLimitMinimum.Value = 366.0R
    '
    'labelLimits
    '
    Me.labelLimits.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelLimits.ForeColor = System.Drawing.Color.Black
    Me.labelLimits.Location = New System.Drawing.Point(13, 56)
    Me.labelLimits.Margin = New System.Windows.Forms.Padding(4)
    Me.labelLimits.Name = "labelLimits"
    Me.labelLimits.Size = New System.Drawing.Size(62, 19)
    Me.labelLimits.TabIndex = 27
    Me.labelLimits.Text = "Limits -"
    '
    'TimerMain
    '
    '
    'FormKeypad
    '
    Me.AcceptButton = Me.ButtonAccept
    Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
    Me.CancelButton = Me.ButtonCancel
    Me.ClientSize = New System.Drawing.Size(266, 520)
    Me.ControlBox = False
    Me.Controls.Add(Me.vlSetpointNew)
    Me.Controls.Add(Me.vlSetpointCurrent)
    Me.Controls.Add(Me.vlLimitMaximum)
    Me.Controls.Add(Me.vlLimitMinimum)
    Me.Controls.Add(Me.ButtonClear)
    Me.Controls.Add(Me.labelLimits)
    Me.Controls.Add(Me.ButtonAccept)
    Me.Controls.Add(Me.Button0)
    Me.Controls.Add(Me.ButtonCancel)
    Me.Controls.Add(Me.Button9)
    Me.Controls.Add(Me.Button8)
    Me.Controls.Add(Me.Button7)
    Me.Controls.Add(Me.Button6)
    Me.Controls.Add(Me.Button5)
    Me.Controls.Add(Me.Button4)
    Me.Controls.Add(Me.Button3)
    Me.Controls.Add(Me.Button2)
    Me.Controls.Add(Me.Button1)
    Me.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
    Me.KeyPreview = True
    Me.Margin = New System.Windows.Forms.Padding(4)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FormKeypad"
    Me.ShowIcon = False
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Adjust Setpoint"
    Me.TopMost = True
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents ButtonAccept As System.Windows.Forms.Button
  Friend WithEvents Button0 As System.Windows.Forms.Button
  Friend WithEvents ButtonCancel As System.Windows.Forms.Button
  Friend WithEvents Button9 As System.Windows.Forms.Button
  Friend WithEvents Button8 As System.Windows.Forms.Button
  Friend WithEvents Button7 As System.Windows.Forms.Button
  Friend WithEvents Button6 As System.Windows.Forms.Button
  Friend WithEvents Button5 As System.Windows.Forms.Button
  Friend WithEvents Button4 As System.Windows.Forms.Button
  Friend WithEvents Button3 As System.Windows.Forms.Button
  Friend WithEvents Button2 As System.Windows.Forms.Button
  Friend WithEvents Button1 As System.Windows.Forms.Button
  Friend WithEvents labelLimits As MimicControls.Label
  Friend WithEvents ButtonClear As System.Windows.Forms.Button
  Friend WithEvents vlLimitMinimum As MimicControls.ValueLabel
  Friend WithEvents vlLimitMaximum As MimicControls.ValueLabel
  Friend WithEvents vlSetpointCurrent As MimicControls.ValueLabel
  Friend WithEvents vlSetpointNew As MimicControls.ValueLabel
  Friend WithEvents TimerMain As Windows.Forms.Timer
End Class
