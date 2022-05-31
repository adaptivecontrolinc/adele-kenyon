<Command("Width Screw", "Screw: |1-5| Setpoint: |0-400|.|0-9| inches", "", "", ""),
Description("Width screw setpoints for width screws 1-5."),
Category("Width Functions")>
Public Class WS : Inherits MarshalByRefObject : Implements ACCommand
  Private ReadOnly ControlCode As ControlCode

  Public Hinge As Integer
  Public Target As Integer
  Public Targets(5) As Integer

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
      'Check array bounds just to be on the safe side
      If param.GetUpperBound(0) < 3 Then Return True
      If param(1) < 0 OrElse param(1) > 5 Then Return True

      If .LockSetpoints OrElse .LockSetpointsWidth Then
        'Do Not Update the setpoint values due to batch parameter (LS) disabling
      Else
        Hinge = param(1)
        Target = MinMax((param(2) * 10) + param(3), 0, 990) ' TODO Confirm Max width
        Targets(Hinge) = Target

        ' Rail 0 = set all rails to same width
        If Hinge = 0 Then
          For i As Integer = 0 To Targets.GetUpperBound(0) : Targets(i) = Target : Next i
          ' Update Setpoints
          For i As Integer = 1 To .Setpoint_WidthScrew.Length - 1
            .Setpoint_WidthScrew(i) = Target
          Next i
        Else
          ' Update specific Hinge
          .Setpoint_WidthScrew(Hinge) = Target
        End If
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
        Case Else
      End Select
    End With
  End Function

  Public Sub Cancel() Implements ACCommand.Cancel
    State = EState.Off
    ' Clear these on cancel so the IOWidth property will return 0 if the command is not active
    For i As Integer = 0 To Targets.GetUpperBound(0) : Targets(i) = 0 : Next i
  End Sub

  Public ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
    Get
      Return (State <> EState.Off)
    End Get
  End Property

End Class
