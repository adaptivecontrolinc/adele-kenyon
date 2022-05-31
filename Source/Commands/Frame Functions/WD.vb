<Command("Width Deviance", "Deviance: |0-99|.|0-9|", "", "", ""), _
Description("Width screw allowable deviance for all width controllers."), _
Category("Width Functions")>
Public Class WD : Inherits MarshalByRefObject : Implements ACCommand
  Private ReadOnly ControlCode As ControlCode

  Public Deviance As Integer

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
      ' Check parameter array
      If param.GetUpperBound(0) < 2 Then Return True
      If param(1) < 0 OrElse param(1) > 20 Then Return True


      If .LockSetpoints Then
        'Do Not Update the setpoint values due to batch parameter (LS) disabling
      Else

        'Check array bounds just to be on the safe side
        If param.GetUpperBound(0) >= 1 Then .Setpoint_WidthDeviance = param(1) * 10
        If param.GetUpperBound(0) >= 2 Then .Setpoint_WidthDeviance += param(2)

        ' Update width screw controllers
        For i As Integer = 1 To .Width_Screw.Length - 1
          '  .Width_Screw(i).Cmd_Deviance = .Setpoint_WidthDeviance
        Next

      End If

    End With
  End Function

  Public Sub Cancel() Implements ACCommand.Cancel
    State = EState.Off
    Deviance = 0
  End Sub

  Public Function Run() As Boolean Implements ACCommand.Run
  End Function

  Public ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
    Get
      Return (State <> EState.Off)
    End Get
  End Property

End Class
