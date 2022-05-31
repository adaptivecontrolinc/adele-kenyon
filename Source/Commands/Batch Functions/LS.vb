<Command("Lock Setpoints", "Disable All:|0-1| Disable Widths:|0-1|", "", "", "", CommandType.BatchParameter),
Description("Batch Parameter used at point of unblocking to disable updating controllers with new setpoints.  " &
            "If 'Disable All' set to '1' application will not send any setpoints down to any controllers.  " &
            "If 'Disable Widths' set to '1' application will not send any setpoints down to width controllers."),
Category("Batch Parameter")>
Public Class LS : Inherits MarshalByRefObject : Implements ACCommand

  Private ReadOnly ControlCode As ControlCode
  Public Sub New(ByVal controlCode As ControlCode)
    Me.ControlCode = controlCode
  End Sub

  Public Enum EState
    Off
    Active
  End Enum
  Property State As EState

  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
  End Sub

  Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
    With ControlCode
      'Check array bounds just to be on the safe side
      If param.GetUpperBound(0) < 2 Then Return True

      .LockSetpoints = param(1) >= 1
      .LockSetpointsWidth = param(2) >= 1


      State = EState.Active
      Return True
    End With
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
    With ControlCode
    End With
  End Function

  Public Sub Cancel() Implements ACCommand.Cancel
    State = EState.Off
  End Sub

  Public ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
    Get
      Return (State <> EState.Off)
    End Get
  End Property

End Class
