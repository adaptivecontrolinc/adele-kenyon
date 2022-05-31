<Command("Pleva Humidity", "Setpoint: |0-999|g/Kg", "", "", "", CommandType.BatchParameter), _
Description("Set the desired Exhaust Humidity recorded by Pleva Humidity Sensor.  Used to control the Exhaust #1 Fan Speed within specified limits.  Set to '0' to disable control."), _
Category("Batch Parameter")> _
Public Class PH
  Inherits MarshalByRefObject
  Implements ACCommand

  Private ReadOnly ControlCode As ControlCode
  Public Sub New(ByVal controlCode As ControlCode)
    Me.ControlCode = controlCode
  End Sub

  Public Enum PH
    Off
    Active
  End Enum

  Public Sub Cancel() Implements ACCommand.Cancel
    state_ = PH.Off
  End Sub

  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
    With ControlCode

      'Check array bounds just to be on the safe side
      If param.GetUpperBound(0) >= 1 Then
        If param(1) > 0 Then
          desiredHumidity_ = param(1) * 10
          state_ = PH.Active
        Else : Cancel()
        End If
      Else  : Cancel()
      End If

    End With
  End Sub

  Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
    With ControlCode
     
      'Check array bounds just to be on the safe side
      If param.GetUpperBound(0) >= 1 Then
        If param(1) > 0 Then
          desiredHumidity_ = param(1) * 10
          state_ = PH.Active
        Else : Cancel()
        End If
      Else : Cancel()
      End If

    End With
    Return True
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
    With ControlCode
      Select Case state_
        Case PH.Off
        Case PH.Active
          If Not .Parent.IsProgramRunning Then Cancel()

      End Select
    End With
  End Function

#Region "Public Properties"
  Private state_ As PH
  Public ReadOnly Property State() As PH
    Get
      Return state_
    End Get
  End Property
  Public ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
    Get
      Return (state_ <> PH.Off)
    End Get
  End Property
  Private desiredHumidity_ As Integer
  Public ReadOnly Property DesiredHumidity() As Integer
    Get
      Return desiredHumidity_
    End Get
  End Property
#End Region

End Class
