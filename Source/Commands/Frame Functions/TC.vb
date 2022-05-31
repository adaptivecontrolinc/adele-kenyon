<Command("Tenter Chain", "Setpoint: |0-125|.|0-9|yd/min  Deviance High: |0-99|.|0-9|yd/min  Deviance Low: |0-99|.|0-9|yd/min", "", "", ""), _
Description("Tenter Chain speed setpoint, in yards/minute, including the allowable deviance above and below for operator adjust."),
Category("Speed Functions")>
Public Class TC : Inherits MarshalByRefObject : Implements ACCommand

  Private ReadOnly ControlCode As ControlCode
  Public Sub New(ByVal controlCode As ControlCode)
    Me.ControlCode = controlCode
  End Sub

  Public Enum EState
    Off
    Active
  End Enum
  Property State As EState

  Public Sub Cancel() Implements ACCommand.Cancel
    State = EState.Off
  End Sub

  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
  End Sub

  Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
    With ControlCode

      If .LockSetpoints Then
        'Do Not Update the setpoint values due to batch parameter (LS) disabling

      Else
        'Check array bounds just to be on the safe side
        If param.GetUpperBound(0) >= 1 Then .Setpoint_TenterChain = param(1) * 10
        If param.GetUpperBound(0) >= 2 Then .Setpoint_TenterChain += param(2)

        If param.GetUpperBound(0) >= 3 Then .Setpoint_TenterDevianceHigh = param(3) * 10
        If param.GetUpperBound(0) >= 4 Then .Setpoint_TenterDevianceHigh += param(4)

        'Setpoint_TenterDevianceLow added 2010-03-17 due to Bruce Dabbs testing Pleva System
        '  Differentiate Pleva setpoint High/Low limits to enable more flexible range for control
        If param.GetUpperBound(0) >= 5 Then
          .Setpoint_TenterDevianceLow = param(5) * 10
          If param.GetUpperBound(0) >= 6 Then .Setpoint_TenterDevianceLow += param(6)
        Else
          'Initial programs only had one Deviance, so use the existing setpoint for both deviance values
          '[2013-07-2013] Bruce & Chad want to allow TC to slow to SetpointMin if deviancelow set to 0.0
          .Setpoint_TenterDevianceLow = 0
        End If

      End If

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
