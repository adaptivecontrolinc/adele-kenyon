<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormAddJob
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
    Me.listBoxPrograms = New System.Windows.Forms.ListBox()
    Me.buttonOK = New System.Windows.Forms.Button()
    Me.buttonCancel = New System.Windows.Forms.Button()
    Me.TextBoxProgramNumber = New System.Windows.Forms.TextBox()
    Me.TextBoxJob = New System.Windows.Forms.TextBox()
    Me.ListViewComparison = New System.Windows.Forms.ListView()
    Me.TimerMain = New System.Windows.Forms.Timer(Me.components)
    Me.checkBoxLock = New System.Windows.Forms.CheckBox()
    Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.TabControlPrograms = New System.Windows.Forms.TabControl()
    Me.TabPage1 = New System.Windows.Forms.TabPage()
    Me.TabPage2 = New System.Windows.Forms.TabPage()
    Me.ListBoxRework = New System.Windows.Forms.ListBox()
    Me.checkBoxLockWidth = New System.Windows.Forms.CheckBox()
    Me.buttonKeyboard = New System.Windows.Forms.Button()
    Me.TabControlPrograms.SuspendLayout()
    Me.TabPage1.SuspendLayout()
    Me.TabPage2.SuspendLayout()
    Me.SuspendLayout()
    '
    'listBoxPrograms
    '
    Me.listBoxPrograms.Dock = System.Windows.Forms.DockStyle.Fill
    Me.listBoxPrograms.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.listBoxPrograms.FormattingEnabled = True
    Me.listBoxPrograms.ItemHeight = 16
    Me.listBoxPrograms.Location = New System.Drawing.Point(3, 3)
    Me.listBoxPrograms.Name = "listBoxPrograms"
    Me.listBoxPrograms.Size = New System.Drawing.Size(270, 617)
    Me.listBoxPrograms.TabIndex = 8
    '
    'buttonOK
    '
    Me.buttonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.buttonOK.Location = New System.Drawing.Point(768, 625)
    Me.buttonOK.Name = "buttonOK"
    Me.buttonOK.Size = New System.Drawing.Size(100, 28)
    Me.buttonOK.TabIndex = 10
    Me.buttonOK.Text = "OK"
    Me.buttonOK.UseVisualStyleBackColor = True
    '
    'buttonCancel
    '
    Me.buttonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.buttonCancel.Location = New System.Drawing.Point(874, 625)
    Me.buttonCancel.Name = "buttonCancel"
    Me.buttonCancel.Size = New System.Drawing.Size(100, 28)
    Me.buttonCancel.TabIndex = 9
    Me.buttonCancel.Text = "Cancel"
    Me.buttonCancel.UseVisualStyleBackColor = True
    '
    'TextBoxProgramNumber
    '
    Me.TextBoxProgramNumber.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.TextBoxProgramNumber.Location = New System.Drawing.Point(301, 623)
    Me.TextBoxProgramNumber.Name = "TextBoxProgramNumber"
    Me.TextBoxProgramNumber.Size = New System.Drawing.Size(242, 26)
    Me.TextBoxProgramNumber.TabIndex = 38
    Me.TextBoxProgramNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    Me.TextBoxProgramNumber.Visible = False
    '
    'TextBoxJob
    '
    Me.TextBoxJob.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.TextBoxJob.Location = New System.Drawing.Point(301, 591)
    Me.TextBoxJob.Name = "TextBoxJob"
    Me.TextBoxJob.Size = New System.Drawing.Size(454, 26)
    Me.TextBoxJob.TabIndex = 39
    '
    'ListViewComparison
    '
    Me.ListViewComparison.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ListViewComparison.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.ListViewComparison.HideSelection = False
    Me.ListViewComparison.Location = New System.Drawing.Point(301, 26)
    Me.ListViewComparison.Name = "ListViewComparison"
    Me.ListViewComparison.Size = New System.Drawing.Size(673, 507)
    Me.ListViewComparison.TabIndex = 40
    Me.ListViewComparison.UseCompatibleStateImageBehavior = False
    '
    'TimerMain
    '
    Me.TimerMain.Interval = 1000
    '
    'checkBoxLock
    '
    Me.checkBoxLock.AutoSize = True
    Me.checkBoxLock.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.checkBoxLock.Location = New System.Drawing.Point(318, 565)
    Me.checkBoxLock.Name = "checkBoxLock"
    Me.checkBoxLock.Size = New System.Drawing.Size(209, 20)
    Me.checkBoxLock.TabIndex = 41
    Me.checkBoxLock.Text = "Lock Current Transport Settings"
    Me.checkBoxLock.UseVisualStyleBackColor = True
    '
    'ContextMenuStrip1
    '
    Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
    Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
    '
    'TabControlPrograms
    '
    Me.TabControlPrograms.Controls.Add(Me.TabPage1)
    Me.TabControlPrograms.Controls.Add(Me.TabPage2)
    Me.TabControlPrograms.Location = New System.Drawing.Point(11, 4)
    Me.TabControlPrograms.Name = "TabControlPrograms"
    Me.TabControlPrograms.SelectedIndex = 0
    Me.TabControlPrograms.Size = New System.Drawing.Size(284, 649)
    Me.TabControlPrograms.TabIndex = 43
    '
    'TabPage1
    '
    Me.TabPage1.Controls.Add(Me.listBoxPrograms)
    Me.TabPage1.Location = New System.Drawing.Point(4, 22)
    Me.TabPage1.Name = "TabPage1"
    Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage1.Size = New System.Drawing.Size(276, 623)
    Me.TabPage1.TabIndex = 0
    Me.TabPage1.Text = "Production"
    Me.TabPage1.UseVisualStyleBackColor = True
    '
    'TabPage2
    '
    Me.TabPage2.Controls.Add(Me.ListBoxRework)
    Me.TabPage2.Location = New System.Drawing.Point(4, 22)
    Me.TabPage2.Name = "TabPage2"
    Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage2.Size = New System.Drawing.Size(276, 561)
    Me.TabPage2.TabIndex = 1
    Me.TabPage2.Text = "Reworks"
    Me.TabPage2.UseVisualStyleBackColor = True
    '
    'ListBoxRework
    '
    Me.ListBoxRework.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ListBoxRework.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.ListBoxRework.FormattingEnabled = True
    Me.ListBoxRework.ItemHeight = 16
    Me.ListBoxRework.Location = New System.Drawing.Point(3, 3)
    Me.ListBoxRework.Name = "ListBoxRework"
    Me.ListBoxRework.Size = New System.Drawing.Size(270, 555)
    Me.ListBoxRework.TabIndex = 9
    '
    'checkBoxLockWidth
    '
    Me.checkBoxLockWidth.AutoSize = True
    Me.checkBoxLockWidth.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.checkBoxLockWidth.Location = New System.Drawing.Point(318, 539)
    Me.checkBoxLockWidth.Name = "checkBoxLockWidth"
    Me.checkBoxLockWidth.Size = New System.Drawing.Size(183, 20)
    Me.checkBoxLockWidth.TabIndex = 57
    Me.checkBoxLockWidth.Text = "Lock Current Width Settngs"
    Me.checkBoxLockWidth.UseVisualStyleBackColor = True
    '
    'buttonKeyboard
    '
    Me.buttonKeyboard.BackgroundImage = Global.My.Resources.Resources.Edit16x16
    Me.buttonKeyboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
    Me.buttonKeyboard.Location = New System.Drawing.Point(948, 593)
    Me.buttonKeyboard.Name = "buttonKeyboard"
    Me.buttonKeyboard.Size = New System.Drawing.Size(26, 26)
    Me.buttonKeyboard.TabIndex = 58
    Me.buttonKeyboard.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
    Me.buttonKeyboard.UseVisualStyleBackColor = True
    '
    'FormAddJob
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(984, 661)
    Me.ControlBox = False
    Me.Controls.Add(Me.buttonKeyboard)
    Me.Controls.Add(Me.checkBoxLockWidth)
    Me.Controls.Add(Me.ListViewComparison)
    Me.Controls.Add(Me.TabControlPrograms)
    Me.Controls.Add(Me.checkBoxLock)
    Me.Controls.Add(Me.TextBoxProgramNumber)
    Me.Controls.Add(Me.TextBoxJob)
    Me.Controls.Add(Me.buttonOK)
    Me.Controls.Add(Me.buttonCancel)
    Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Name = "FormAddJob"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "New Job"
    Me.TabControlPrograms.ResumeLayout(False)
    Me.TabPage1.ResumeLayout(False)
    Me.TabPage2.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Private WithEvents listBoxPrograms As System.Windows.Forms.ListBox
  Private WithEvents buttonOK As System.Windows.Forms.Button
  Private WithEvents buttonCancel As System.Windows.Forms.Button
  Friend WithEvents TextBoxJob As System.Windows.Forms.TextBox
  Friend WithEvents TimerMain As System.Windows.Forms.Timer
  Private WithEvents TextBoxProgramNumber As System.Windows.Forms.TextBox
  Private WithEvents checkBoxLock As System.Windows.Forms.CheckBox
  Private WithEvents ListViewComparison As System.Windows.Forms.ListView
  Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents TabControlPrograms As System.Windows.Forms.TabControl
  Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
  Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
  Private WithEvents ListBoxRework As System.Windows.Forms.ListBox
  Private WithEvents checkBoxLockWidth As CheckBox
  Friend WithEvents buttonKeyboard As Button
End Class
