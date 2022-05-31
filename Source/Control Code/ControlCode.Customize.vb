Partial Public Class ControlCode

  Private Sub CustomizeOperatorToolStrip()
    'Create custom Operator tool strip - bottom tool strip
    With Parent

      Dim buttonWorkList As New Windows.Forms.ToolStripButton("Work List", My.Resources.WorkList16x16)

      '.AddStandardButton(StandardButton.WorkList, ButtonPosition.Operator, "WorkList")
      .AddButton(New ToolStripButton("Worklist", My.Resources.WorkList16x16), ButtonPosition.Operator, New ControlWorkList(Me))

      .AddStandardButton(StandardButton.Program, ButtonPosition.Operator, "Status")
      .AddButton(New ToolStripButton("Configuration", My.Resources.Gridlines16x16), ButtonPosition.Operator, New ControlConfiguration(Me))
      .AddStandardButton(StandardButton.Mimic, ButtonPosition.Operator)
      '   .AddButton(New ToolStripButton("Pleva", My.Resources.Gridlines16x16), ButtonPosition.Operator, New PlevaControl(Me))
      '   .AddButton(New ToolStripButton("Utilities", My.Resources.Gridlines16x16), ButtonPosition.Operator, New UtilitiesControl(Me))
      .AddStandardButton(StandardButton.Graph, ButtonPosition.Operator)
      .AddStandardButton(StandardButton.History, ButtonPosition.Operator)

    End With
  End Sub


  Private Sub CustomizeExpertToolStrip()
    'Create custom Expert tool strip - shown after expert button press
    With Parent

      .AddStandardButton(StandardButton.IO, ButtonPosition.Expert)
      .AddStandardButton(StandardButton.Variables, ButtonPosition.Expert)
      .AddStandardButton(StandardButton.Parameters, ButtonPosition.Expert)
      .AddStandardButton(StandardButton.Programs, ButtonPosition.Expert)

    End With
  End Sub

End Class
