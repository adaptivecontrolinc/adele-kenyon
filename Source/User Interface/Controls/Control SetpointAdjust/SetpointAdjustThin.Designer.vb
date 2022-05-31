<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SetpointAdjustThin
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
    Me.labelHeader = New System.Windows.Forms.Label()
    Me.ButtonDecrease = New System.Windows.Forms.Button()
    Me.ButtonIncrease = New System.Windows.Forms.Button()
    Me.labelStatus = New MimicControls.Label()
    Me.labelSetpoint = New MimicControls.Label()
    Me.PicBox = New MimicControls.PictureBox()
    Me.labelActual = New MimicControls.Label()
    Me.SuspendLayout()
    '
    'labelHeader
    '
    Me.labelHeader.BackColor = System.Drawing.Color.Cornsilk
    Me.labelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.labelHeader.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelHeader.ForeColor = System.Drawing.Color.Blue
    Me.labelHeader.Location = New System.Drawing.Point(0, 0)
    Me.labelHeader.Name = "labelHeader"
    Me.labelHeader.Size = New System.Drawing.Size(110, 25)
    Me.labelHeader.TabIndex = 5
    Me.labelHeader.Text = "Header Text"
    Me.labelHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'ButtonDecrease
    '
    Me.ButtonDecrease.BackColor = System.Drawing.SystemColors.ButtonFace
    Me.ButtonDecrease.BackgroundImage = Global.My.Resources.Resources.Decrease
    Me.ButtonDecrease.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
    Me.ButtonDecrease.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue
    Me.ButtonDecrease.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
    Me.ButtonDecrease.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.ButtonDecrease.Location = New System.Drawing.Point(84, 69)
    Me.ButtonDecrease.Name = "ButtonDecrease"
    Me.ButtonDecrease.Size = New System.Drawing.Size(26, 40)
    Me.ButtonDecrease.TabIndex = 4
    Me.ButtonDecrease.UseVisualStyleBackColor = False
    '
    'ButtonIncrease
    '
    Me.ButtonIncrease.BackColor = System.Drawing.SystemColors.ButtonFace
    Me.ButtonIncrease.BackgroundImage = Global.My.Resources.Resources.Increase
    Me.ButtonIncrease.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
    Me.ButtonIncrease.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue
    Me.ButtonIncrease.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow
    Me.ButtonIncrease.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.ButtonIncrease.Location = New System.Drawing.Point(84, 26)
    Me.ButtonIncrease.Name = "ButtonIncrease"
    Me.ButtonIncrease.Size = New System.Drawing.Size(26, 40)
    Me.ButtonIncrease.TabIndex = 3
    Me.ButtonIncrease.UseVisualStyleBackColor = False
    '
    'labelStatus
    '
    Me.labelStatus.AutoSize = False
    Me.labelStatus.BackColor = System.Drawing.Color.AliceBlue
    Me.labelStatus.Border.Style = MimicControls.BorderStyle.Flat
    Me.labelStatus.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelStatus.ForeColor = System.Drawing.Color.Black
    Me.labelStatus.Location = New System.Drawing.Point(0, 111)
    Me.labelStatus.Name = "labelStatus"
    Me.labelStatus.Size = New System.Drawing.Size(110, 28)
    Me.labelStatus.TabIndex = 13
    Me.labelStatus.Text = "Status"
    Me.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'labelSetpoint
    '
    Me.labelSetpoint.AutoSize = False
    Me.labelSetpoint.BackColor = System.Drawing.Color.White
    Me.labelSetpoint.Border.Style = MimicControls.BorderStyle.Flat
    Me.labelSetpoint.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelSetpoint.ForeColor = System.Drawing.Color.Black
    Me.labelSetpoint.Location = New System.Drawing.Point(0, 26)
    Me.labelSetpoint.Name = "labelSetpoint"
    Me.labelSetpoint.Size = New System.Drawing.Size(83, 40)
    Me.labelSetpoint.TabIndex = 9
    Me.labelSetpoint.Text = "Setpoint"
    Me.labelSetpoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'PicBox
    '
    Me.PicBox.Location = New System.Drawing.Point(128, -2)
    Me.PicBox.Name = "PicBox"
    Me.PicBox.Size = New System.Drawing.Size(16, 16)
    Me.PicBox.TabIndex = 7
    '
    'labelActual
    '
    Me.labelActual.AutoSize = False
    Me.labelActual.BackColor = System.Drawing.Color.White
    Me.labelActual.Border.Style = MimicControls.BorderStyle.Flat
    Me.labelActual.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelActual.ForeColor = System.Drawing.Color.Black
    Me.labelActual.Location = New System.Drawing.Point(0, 69)
    Me.labelActual.Name = "labelActual"
    Me.labelActual.Size = New System.Drawing.Size(83, 40)
    Me.labelActual.TabIndex = 10
    Me.labelActual.Text = "Actual"
    Me.labelActual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'SetpointAdjustThin
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
    Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.BackColor = System.Drawing.SystemColors.WindowText
    Me.Controls.Add(Me.ButtonIncrease)
    Me.Controls.Add(Me.ButtonDecrease)
    Me.Controls.Add(Me.labelStatus)
    Me.Controls.Add(Me.labelHeader)
    Me.Controls.Add(Me.labelSetpoint)
    Me.Controls.Add(Me.PicBox)
    Me.Controls.Add(Me.labelActual)
    Me.Name = "SetpointAdjustThin"
    Me.Size = New System.Drawing.Size(110, 140)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents ButtonIncrease As System.Windows.Forms.Button
  Friend WithEvents ButtonDecrease As System.Windows.Forms.Button
  Friend WithEvents labelHeader As System.Windows.Forms.Label
  Friend WithEvents PicBox As MimicControls.PictureBox
  Friend WithEvents labelSetpoint As MimicControls.Label
  Friend WithEvents labelActual As MimicControls.Label
  Friend WithEvents labelStatus As MimicControls.Label

End Class
