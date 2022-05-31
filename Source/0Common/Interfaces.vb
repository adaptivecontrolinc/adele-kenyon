' Version 2022-05-17
' Add expert user to setpoint adjustment functions

Interface IController
  ReadOnly Property Setpoint() As String
  ReadOnly Property Actual() As String
  Property Description() As String
  ReadOnly Property Units() As String
  ReadOnly Property Status() As String
  ReadOnly Property OutputPercent As Integer
  Property Zone() As Integer

  ReadOnly Property SetpointCurrent() As Integer
  ReadOnly Property SetpointMinimum() As Integer
  ReadOnly Property SetpointMaximum() As Integer
  ReadOnly Property SetpointFactor As Integer

  ReadOnly Property AdjustResult() As EControllerAdjustResult
  ReadOnly Property AdjustString() As String

  Function Increase(ByVal increment As Integer, ByVal expert As Boolean) As Boolean
  Function Decrease(ByVal increment As Integer, ByVal expert As Boolean) As Boolean
  Function IChangeSetpoint(ByVal setpoint As Integer, ByVal expert As Boolean) As Boolean
  ReadOnly Property ChangeSetpointEnabled() As Boolean

End Interface

Public Enum EControllerAdjustResult
  OK
  SetpointExceedsLowLimit
  SetpointExceedsHighLimit
  SetpointChangeDisabled
End Enum