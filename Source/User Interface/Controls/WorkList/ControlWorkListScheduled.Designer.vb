<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ControlWorkListScheduled
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
    Me.listViewMain = New System.Windows.Forms.ListView
    Me.toolStripMain = New System.Windows.Forms.ToolStrip
    Me.toolStripButtonNew = New System.Windows.Forms.ToolStripButton
    Me.toolStripButtonDelete = New System.Windows.Forms.ToolStripButton
    Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
    Me.toolStripButtonFirst = New System.Windows.Forms.ToolStripButton
    Me.toolStripButtonSooner = New System.Windows.Forms.ToolStripButton
    Me.toolStripButtonLater = New System.Windows.Forms.ToolStripButton
    Me.toolStripButtonLast = New System.Windows.Forms.ToolStripButton
    Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
    Me.toolStripButtonRefresh = New System.Windows.Forms.ToolStripButton
    Me.toolStripButtonBlocked = New System.Windows.Forms.ToolStripButton
    Me.groupBoxDyelot = New System.Windows.Forms.GroupBox
    Me.textBoxDyelot = New System.Windows.Forms.TextBox
    Me.labelDyelot = New System.Windows.Forms.Label
    Me.toolStripMain.SuspendLayout()
    Me.groupBoxDyelot.SuspendLayout()
    Me.SuspendLayout()
    '
    'listViewMain
    '
    Me.listViewMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.listViewMain.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.listViewMain.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.listViewMain.Location = New System.Drawing.Point(0, 28)
    Me.listViewMain.Name = "listViewMain"
    Me.listViewMain.Size = New System.Drawing.Size(700, 334)
    Me.listViewMain.TabIndex = 0
    Me.listViewMain.UseCompatibleStateImageBehavior = False
    '
    'toolStripMain
    '
    Me.toolStripMain.Dock = System.Windows.Forms.DockStyle.None
    Me.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
    Me.toolStripMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripButtonNew, Me.toolStripButtonDelete, Me.toolStripSeparator1, Me.toolStripButtonFirst, Me.toolStripButtonSooner, Me.toolStripButtonLater, Me.toolStripButtonLast, Me.ToolStripSeparator2, Me.toolStripButtonRefresh, Me.toolStripButtonBlocked})
    Me.toolStripMain.Location = New System.Drawing.Point(0, 0)
    Me.toolStripMain.Name = "toolStripMain"
    Me.toolStripMain.Size = New System.Drawing.Size(475, 25)
    Me.toolStripMain.TabIndex = 2
    Me.toolStripMain.Text = "ToolStripMain"
    '
    'toolStripButtonNew
    '
    Me.toolStripButtonNew.Image = Global.My.Resources.Resources.NewDocument16x16
    Me.toolStripButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.toolStripButtonNew.Name = "toolStripButtonNew"
    Me.toolStripButtonNew.Size = New System.Drawing.Size(51, 22)
    Me.toolStripButtonNew.Text = "New"
    '
    'toolStripButtonDelete
    '
    Me.toolStripButtonDelete.Image = Global.My.Resources.Resources.DeleteRed16x16
    Me.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.toolStripButtonDelete.Name = "toolStripButtonDelete"
    Me.toolStripButtonDelete.Size = New System.Drawing.Size(60, 22)
    Me.toolStripButtonDelete.Text = "Delete"
    Me.toolStripButtonDelete.ToolTipText = "Delete"
    '
    'toolStripSeparator1
    '
    Me.toolStripSeparator1.Name = "toolStripSeparator1"
    Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
    '
    'toolStripButtonFirst
    '
    Me.toolStripButtonFirst.Image = Global.My.Resources.Resources.DoubleLeftArrow16x16
    Me.toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.toolStripButtonFirst.Name = "toolStripButtonFirst"
    Me.toolStripButtonFirst.Size = New System.Drawing.Size(49, 22)
    Me.toolStripButtonFirst.Text = "First"
    '
    'toolStripButtonSooner
    '
    Me.toolStripButtonSooner.Image = Global.My.Resources.Resources.LeftArrow16x16
    Me.toolStripButtonSooner.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.toolStripButtonSooner.Name = "toolStripButtonSooner"
    Me.toolStripButtonSooner.Size = New System.Drawing.Size(64, 22)
    Me.toolStripButtonSooner.Text = "Sooner"
    '
    'toolStripButtonLater
    '
    Me.toolStripButtonLater.Image = Global.My.Resources.Resources.RightArrow16x16
    Me.toolStripButtonLater.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.toolStripButtonLater.Name = "toolStripButtonLater"
    Me.toolStripButtonLater.Size = New System.Drawing.Size(53, 22)
    Me.toolStripButtonLater.Text = "Later"
    '
    'toolStripButtonLast
    '
    Me.toolStripButtonLast.Image = Global.My.Resources.Resources.DoubleRightArrow16x16
    Me.toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.toolStripButtonLast.Name = "toolStripButtonLast"
    Me.toolStripButtonLast.Size = New System.Drawing.Size(48, 22)
    Me.toolStripButtonLast.Text = "Last"
    '
    'ToolStripSeparator2
    '
    Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
    Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
    '
    'toolStripButtonRefresh
    '
    Me.toolStripButtonRefresh.Image = Global.My.Resources.Resources.Refresh16x16
    Me.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.toolStripButtonRefresh.Name = "toolStripButtonRefresh"
    Me.toolStripButtonRefresh.Size = New System.Drawing.Size(66, 22)
    Me.toolStripButtonRefresh.Text = "Refresh"
    '
    'toolStripButtonBlocked
    '
    Me.toolStripButtonBlocked.Image = Global.My.Resources.Resources.Blocked16x16
    Me.toolStripButtonBlocked.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.toolStripButtonBlocked.Name = "toolStripButtonBlocked"
    Me.toolStripButtonBlocked.Size = New System.Drawing.Size(69, 22)
    Me.toolStripButtonBlocked.Text = "Blocked"
    '
    'groupBoxDyelot
    '
    Me.groupBoxDyelot.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.groupBoxDyelot.Controls.Add(Me.textBoxDyelot)
    Me.groupBoxDyelot.Controls.Add(Me.labelDyelot)
    Me.groupBoxDyelot.Location = New System.Drawing.Point(0, 354)
    Me.groupBoxDyelot.Name = "groupBoxDyelot"
    Me.groupBoxDyelot.Size = New System.Drawing.Size(700, 46)
    Me.groupBoxDyelot.TabIndex = 4
    Me.groupBoxDyelot.TabStop = False
    '
    'textBoxDyelot
    '
    Me.textBoxDyelot.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.textBoxDyelot.Location = New System.Drawing.Point(208, 14)
    Me.textBoxDyelot.Multiline = True
    Me.textBoxDyelot.Name = "textBoxDyelot"
    Me.textBoxDyelot.Size = New System.Drawing.Size(280, 24)
    Me.textBoxDyelot.TabIndex = 1
    '
    'labelDyelot
    '
    Me.labelDyelot.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labelDyelot.Location = New System.Drawing.Point(6, 12)
    Me.labelDyelot.Name = "labelDyelot"
    Me.labelDyelot.Size = New System.Drawing.Size(200, 24)
    Me.labelDyelot.TabIndex = 2
    Me.labelDyelot.Text = "Scan or enter dyelot to start:"
    Me.labelDyelot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'ControlWorkListScheduled
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.groupBoxDyelot)
    Me.Controls.Add(Me.toolStripMain)
    Me.Controls.Add(Me.listViewMain)
    Me.Name = "ControlWorkListScheduled"
    Me.Size = New System.Drawing.Size(700, 400)
    Me.toolStripMain.ResumeLayout(False)
    Me.toolStripMain.PerformLayout()
    Me.groupBoxDyelot.ResumeLayout(False)
    Me.groupBoxDyelot.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Private WithEvents listViewMain As System.Windows.Forms.ListView
  Private WithEvents toolStripMain As System.Windows.Forms.ToolStrip
  Private WithEvents toolStripButtonNew As System.Windows.Forms.ToolStripButton
  Private WithEvents toolStripButtonDelete As System.Windows.Forms.ToolStripButton
  Private WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
  Private WithEvents toolStripButtonFirst As System.Windows.Forms.ToolStripButton
  Private WithEvents toolStripButtonSooner As System.Windows.Forms.ToolStripButton
  Private WithEvents toolStripButtonLater As System.Windows.Forms.ToolStripButton
  Private WithEvents toolStripButtonLast As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
  Private WithEvents toolStripButtonBlocked As System.Windows.Forms.ToolStripButton
  Private WithEvents toolStripButtonRefresh As System.Windows.Forms.ToolStripButton
  Private WithEvents groupBoxDyelot As System.Windows.Forms.GroupBox
  Private WithEvents textBoxDyelot As System.Windows.Forms.TextBox
  Private WithEvents labelDyelot As System.Windows.Forms.Label

End Class
