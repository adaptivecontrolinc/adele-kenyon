<Command("Vacuum", "Zone: |1-2| Setpoint: |0-100|%  Deviance: |0-99|%", "", "", ""),
Description("Vacuum system setpoint Percent. (0 to 100%"),
Category("Speed Functions")>
Public Class VA : Inherits MarshalByRefObject : Implements ACCommand
  Private ReadOnly ControlCode As ControlCode

  Public Zone As Integer
  Public Target As Integer
  Public Targets(2) As Integer

  Public Sub New(ByVal controlCode As ControlCode)
    Me.ControlCode = controlCode
  End Sub

  Public Enum EState
    Off
    Active
  End Enum
  Public State As EState

  Public Sub Cancel() Implements ACCommand.Cancel
    State = EState.Off
    ' Clear these on cancel so the speed property will return 0 if the command is not active
    For i As Integer = 0 To Targets.GetUpperBound(0) : Targets(i) = 0 : Next
  End Sub

  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
  End Sub

  Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
    With ControlCode

      If .LockSetpoints Then
        'Do Not Update the setpoint values due to batch parameter (LS) disabling
      Else
        'Check array bounds 
        If param.GetUpperBound(0) < 2 Then Return True
        If param(1) < 0 OrElse param(1) > 2 Then Return True ' two vacuum pumps

        ' Update values after checking arrays
        Zone = param(1)
        Target = MinMax(param(2) * 100, .Parameters.VacuumpPumpMin, .Parameters.VacuumPumpMax)

        ' Zone 0 = set all zone speeds the same speed
        Targets(Zone) = Target
        If Zone = 0 Then
          For i As Integer = 1 To Targets.GetUpperBound(0)
            Targets(i) = Target
            .Setpoint_Extractor(i) = MinMax(Targets(i), 0, 1000) '0% to 100%
          Next i
        Else
          .Setpoint_ExtractorDeviance(Zone) = Targets(Zone)
        End If
      End If

      State = EState.Active
      Return True
    End With
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
  End Function

  Public ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
    Get
      Return (State <> EState.Off)
    End Get
  End Property

End Class
