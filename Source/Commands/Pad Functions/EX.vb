<Command("Extractor", "Number: |1-2| Setpoint: |0-999|%  Deviance: |0-999|%", "", "", ""), Description("Extractor Setpoint Speed Percent."), Category("Pad Functions")>
Public Class EX : Inherits MarshalByRefObject : Implements ACCommand
  Private ReadOnly ControlCode As ControlCode

  Public Enum EState
    Off
    Active
  End Enum
  Property State As EState
  Public Zone As Integer
  Public Target As Integer
  Public TargetDeviance As Integer
  Public Targets(2) As Integer
  Public TargetDeviances(2) As Integer

  Public Sub New(ByVal controlCode As ControlCode)
    Me.ControlCode = controlCode
  End Sub

  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
  End Sub
  Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
    With ControlCode
      ' Verify command parameters
      If param.GetUpperBound(0) <> 3 Then Return True
      If param(1) < 0 OrElse param(1) > 2 Then Return True

      ' Array bounds already checked
      Zone = param(1)
      Target = param(2) * 10
      TargetDeviance = param(3) * 10

      ' Remember targets for each zone
      Targets(Zone) = Target
      TargetDeviances(Zone) = TargetDeviance

      If Zone = 0 Then
        For i As Integer = 0 To Targets.GetUpperBound(0)
          Targets(i) = Target
          TargetDeviances(i) = TargetDeviance
        Next i
      End If

      If .LockSetpoints Then
        ' Do Not Update the setpoint values due to batch parameter (LS) disabling
      Else
        ' Array bounds already checked
        .Setpoint_Extractor(Zone) = Targets(Zone)
        .Setpoint_ExtractorDeviance(Zone) = TargetDeviances(Zone)
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
