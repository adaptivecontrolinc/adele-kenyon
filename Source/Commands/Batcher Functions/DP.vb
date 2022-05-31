<Command("Dancer Pressure", "Setpoint: |0-999|psi Deviance: |0-999|psi", "", "", ""),
  Description("Dancer Pressure E/P Setpoint."), Category("Speed Functions")>
Public Class DP : Inherits MarshalByRefObject : Implements ACCommand
  Private ReadOnly ControlCode As ControlCode

  Public Enum EState
    Off
    Active
  End Enum
  Property State As EState

  Public Sub New(ByVal controlCode As ControlCode)
    Me.ControlCode = controlCode
  End Sub

  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
  End Sub

  Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
    With ControlCode
      ' Verify command parameters
      If param.GetUpperBound(0) <> 2 Then Return True

      If .LockSetpoints Then
        ' Do Not Update the setpoint values due to batch parameter (LS) disabling
      Else
        ' Array bounds already checked
        .Setpoint_DancerPress = param(1) * 10
        .Setpoint_DancerPressDeviance = param(2) * 10
      End If

      State = EState.Active
      Return True
    End With
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
    With ControlCode
      Select Case State

        Case EState.Off

        Case EState.Active

      End Select
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
