﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ControlWorkListCurrent
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
    Me.SuspendLayout()
    '
    'listViewMain
    '
    Me.listViewMain.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.listViewMain.Dock = System.Windows.Forms.DockStyle.Fill
    Me.listViewMain.Location = New System.Drawing.Point(0, 0)
    Me.listViewMain.Name = "listViewMain"
    Me.listViewMain.Size = New System.Drawing.Size(700, 400)
    Me.listViewMain.TabIndex = 0
    Me.listViewMain.UseCompatibleStateImageBehavior = False
    '
    'ControlWorkListCurrent
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.listViewMain)
    Me.Name = "ControlWorkListCurrent"
    Me.Size = New System.Drawing.Size(700, 400)
    Me.ResumeLayout(False)

  End Sub
  Private WithEvents listViewMain As System.Windows.Forms.ListView

End Class
