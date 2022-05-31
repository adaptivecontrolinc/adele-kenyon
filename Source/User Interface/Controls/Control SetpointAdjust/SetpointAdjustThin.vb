Public Class SetpointAdjustThin
  Property IsSupervisor As Boolean

  Sub New()
    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    InitializeControl()
  End Sub

  Public Sub InitializeControl()
    'DoubleBuffered = True
  End Sub

  Public Sub RefreshValues(ByVal IsSupervisor As Boolean)
    If controller IsNot Nothing Then
      labelHeader.Text = controller.Description
      labelSetpoint.Text = controller.Setpoint
      labelActual.Text = controller.Actual
      labelStatus.Text = controller.Status
      Me.IsSupervisor = IsSupervisor
    Else
      labelHeader.Text = "Nothing"
      labelSetpoint.Text = "Setpoint"
      labelActual.Text = "0.0"
      labelStatus.Text = "0.0"
    End If
  End Sub

  Private increment_ As Integer
  Public Property Increment() As Integer
    Get
      Return increment_
    End Get
    Set(ByVal value As Integer)
      increment_ = value
    End Set
  End Property

  Private controller As IController
  Public Function Connect(ByVal controller As Object) As Boolean
    Try
      Me.controller = DirectCast(controller, IController)
      Return True
    Catch ex As Exception
      'Log error ?
    End Try
    Return False
  End Function

  Private index_ As Integer
  Public Property Index() As Integer
    Get
      Return index_
    End Get
    Set(ByVal value As Integer)
      index_ = value
    End Set
  End Property

  Private Sub ButtonIncrease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonIncrease.Click
    Try
      If controller.Increase(increment_, IsSupervisor) Then
        'setpoint was adjusted
      Else
        Message(controller.AdjustResult.ToString & Environment.NewLine & controller.AdjustString)
      End If
    Catch ex As Exception
      'log error
    End Try
  End Sub

  Private Sub ButtonDecrease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDecrease.Click
    Try
      If controller.Decrease(increment_, IsSupervisor) Then
        'setpoint was adjusted
      Else
        Message(controller.AdjustResult.ToString & Environment.NewLine & controller.AdjustString)
      End If
    Catch ex As Exception
      'log error
    End Try
  End Sub

  Private Function Message(ByVal text As String) As Boolean
    MessageBox.Show(text.PadRight(64), "Adaptive Control", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
    Return False
  End Function

  Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
    MyBase.OnPaint(e)
    Using g As Graphics = e.Graphics
      'g.DrawRectangle(New Pen(Color.Black), 2, 2, Me.Width - 4, Me.Height - 4)
    End Using
  End Sub

  Private Sub labelSetpoint_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles labelSetpoint.DoubleClick
    ChangeSetpoint()
  End Sub

  Private Sub labelActual_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles labelActual.DoubleClick
    ChangeSetpoint()
  End Sub

  Private Sub labelHeader_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles labelHeader.DoubleClick
    ChangeSetpoint()
  End Sub

  Private Sub ChangeSetpoint()
      If controller.ChangeSetpointEnabled Then
      Using newKeyPad As New FormKeypad(controller.Description, controller.SetpointMinimum, controller.SetpointMaximum, controller.SetpointCurrent, controller.Units, controller.SetpointFactor)
        newKeyPad.ShowDialog()
        If newKeyPad.WasConfirmed Then
          If controller.IChangeSetpoint(newKeyPad.NewSetpoint, IsSupervisor) Then
            'setpoint was adjusted
          Else
            Message(controller.AdjustResult.ToString & Environment.NewLine & controller.AdjustString)
          End If

          newKeyPad.Close()
        ElseIf newKeyPad.WasCancelled Then
          newKeyPad.Close()
        End If
      End Using
    Else
        Message("Double-Click Feature Not Currently Enabled")
      End If
  End Sub

End Class
