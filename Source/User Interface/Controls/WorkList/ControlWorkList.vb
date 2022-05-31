Public Class ControlWorkList
  Private ReadOnly controlCode As ControlCode

  Sub New(controlCode As ControlCode)
    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    Me.ControlCode = controlCode

    ' Add any initialization after the InitializeComponent() call.
    InitializeControl()
  End Sub

  Public Sub InitializeControl()
    timerMain.Interval = 10000
    timerMain.Enabled = True

    Me.ResizeRedraw = True
  End Sub

  Private Sub timerMain_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerMain.Tick
    timerMain.Interval = 5000  'Run a bit faster once the control system is up
    timerMain.Enabled = False

    'Check to see if control code is set
    'If controlWorkListCurrent.ControlCode Is Nothing Then controlWorkListCurrent.ControlCode = Me.ControlCode
    If controlWorkListScheduled.ControlCode Is Nothing Then controlWorkListScheduled.ControlCode = Me.controlCode

    If controlCode IsNot Nothing Then
      If (controlCode.Parent.IsProgramRunning) Then
        timerMain.Interval = 10000  'Delay longer while running between requery
        controlWorkListScheduled.Requery()
      Else
        If Me.Visible Then
          controlWorkListScheduled.Requery()
        End If
      End If
    End If

    timerMain.Enabled = True
  End Sub

  Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
    MyBase.OnPaint(e)

    Exit Sub
    'Used to check drawing area in normal and expert mode
    e.Graphics.DrawLine(New Pen(Color.Black), 0, 675 - 200, 0, 675)
    e.Graphics.DrawLine(New Pen(Color.Black), 0, 675, 200, 675)

    e.Graphics.DrawLine(New Pen(Color.Black), 1023, 675 - 200, 1023, 675)
    e.Graphics.DrawLine(New Pen(Color.Black), 1023 - 200, 675, 1023, 675)
  End Sub

End Class
