<Command("Stripper Roll", "Setpoint: |50-150|%  Deviance: |0-999|%", "", "", ""),
Description("Stripper Roll speed setpoint Percent of Tenter Chain setpoint. (115% = 1.15 * TC_Setpoint)"),
Category("Speed Functions")>
Public Class ST : Inherits MarshalByRefObject : Implements ACCommand
  Private ReadOnly ControlCode As ControlCode

  Public Target As Integer
  Public Deviance As Integer

  Public Enum EState
    Off
    Adjust
    Active
  End Enum
  Property State As EState
  Property StateString As String

  Public Sub New(ByVal controlCode As ControlCode)
    Me.ControlCode = controlCode
  End Sub

  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
  End Sub

  Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
    With ControlCode
      ' Check parameter array
      If param.GetUpperBound(0) < 2 Then Return True
      If param(1) < 50 OrElse param(1) > 150 Then Return True

      If .LockSetpoints Then
        'Do Not Update the setpoint values due to batch parameter (LS) disabling
      Else
        'Check array bounds just to be on the safe side
        Target = MinMax(param(1) * 10, .Parameters.StripperSetpointMin, .Parameters.StripperSetpointMax)
        Deviance = param(2) * 10

        .Setpoint_Stripper = Target
        .Setpoint_StripperDeviance = Deviance

        If (.Parameters.StripperAdjustEnable > 0) Then
          State = EState.Adjust
        Else : State = EState.Active
        End If
      End If

      Return True
    End With
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
    With ControlCode
      Select Case State
        Case EState.Off
          StateString = ""

        Case EState.Adjust
          StateString = "Adust"
          If .Parameters.StripperAdjustEnable = 0 Then State = EState.Active

        Case EState.Active
          StateString = "Active"

      End Select
    End With
  End Function

  Public Sub Cancel() Implements ACCommand.Cancel
    State = EState.Off
    Target = 0
    Deviance = 0
  End Sub

  Public ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
    Get
      Return (State <> EState.Off)
    End Get
  End Property
  Public ReadOnly Property IsAdust() As Boolean
    Get
      Return (State = EState.Adjust)
    End Get
  End Property

End Class
