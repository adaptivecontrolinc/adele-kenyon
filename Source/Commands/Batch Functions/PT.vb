<Command("Pleva Temperature", "Temp: |0-450|F Time: |0-150|.|0-9| sec", "", "", "", CommandType.BatchParameter), _
Description("Designed to adjust the fabric speed to reach a desired fabric temp for a desired time. Used to control the Tenter Chain Speed within specified limits.  Set to '0' to disable control."), _
Category("Batch Parameter")> _
Public Class PT
  Inherits MarshalByRefObject
  Implements ACCommand

  Private ReadOnly ControlCode As ControlCode
  Public Sub New(ByVal controlCode As ControlCode)
    Me.ControlCode = controlCode
  End Sub

  Public Enum PT
    Off
    Active
  End Enum

  Public Sub Cancel() Implements ACCommand.Cancel
    desiredTemp_ = 0
    desiredTime_ = 0
    state_ = PT.Off
  End Sub

  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
    With ControlCode
      If param(1) > 0 Then

        'Check array bounds just to be on the safe side
        If param.GetUpperBound(0) >= 1 Then desiredTemp_ = param(1) * 10 '450F = 4500
        If param.GetUpperBound(0) >= 2 Then desiredTime_ = param(2) * 10 '150sec = 1500ms
        If param.GetUpperBound(0) >= 3 Then desiredTime_ += param(3) '1500 + 9 = 1509ms

        'Set remote values in Pleva Class Module at point of change
        .Pleva.TempDesired = desiredTemp_
        .Pleva.TimeAtTempDesired = desiredTime_

        state_ = PT.Active
      Else
        'Set remote values in Pleva Class Module at point of change
        .Pleva.TempDesired = 0
        .Pleva.TimeAtTempDesired = 0
      End If
    End With
  End Sub

  Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
    With ControlCode
      If param(1) > 0 Then

        'Check array bounds just to be on the safe side
        If param.GetUpperBound(0) >= 1 Then desiredTemp_ = param(1) * 10
        If param.GetUpperBound(0) >= 2 Then desiredTime_ = param(2) * 10
        If param.GetUpperBound(0) >= 3 Then desiredTime_ += param(3) '1500 + 9 = 1509ms

        'Set remote values in Pleva Class Module at point of change
        .Pleva.TempDesired = desiredTemp_
        .Pleva.TimeAtTempDesired = desiredTime_

        state_ = PT.Active
      Else
        'Set remote values in Pleva Class Module at point of change
        .Pleva.TempDesired = 0
        .Pleva.TimeAtTempDesired = 0
      End If
    End With
    Return True
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
    With ControlCode
      Select Case state_
        Case PT.Off

        Case PT.Active
          If Not .Parent.IsProgramRunning Then Cancel()

      End Select
    End With
  End Function

#Region "Public Properties"
  Private state_ As PT
  Public ReadOnly Property State() As PT
    Get
      Return state_
    End Get
  End Property
  Public ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
    Get
      Return (state_ <> PT.Off)
    End Get
  End Property

  Private desiredTemp_ As Integer
  Public Property DesiredTemp() As Integer
    Get
      Return desiredTemp_
    End Get
    Set(ByVal value As Integer)
      desiredTemp_ = value
    End Set
  End Property

  Private desiredTime_ As Integer
  Public Property DesiredTime() As Integer
    Get
      Return desiredTime_
    End Get
    Set(ByVal value As Integer)
      desiredTime_ = value
    End Set
  End Property

#End Region

End Class
