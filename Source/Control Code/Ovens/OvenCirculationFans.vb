Public Class OvenCirculationFans : Inherits MarshalByRefObject
  Private ReadOnly controlcode As ControlCode

  ' PV = process variable (measured value), SP = setpoint (target value)
  Private ReadOnly topFanPV As Integer = 51             ' Aninp 51 - 58
  Private ReadOnly topFanSP As Integer = 23             ' Anout 23 - 30

  Private ReadOnly bottomFanPV As Integer = 35          ' Aninp 35-42
  Private ReadOnly bottomFanSP As Integer = 31          ' Anout 31-38

  Private ReadOnly fanMode As Integer = 9               ' Anout 9
  Private ReadOnly fanStart As Integer = 22             ' Anout 22
  Private ReadOnly fanStop As Integer = 21              ' Anout 21

  Public StartFan As Boolean
  Public StartFanTimer As New Timer

  Public StopFanPress As Boolean
  Public StopFanTimer As New Timer


  Sub New(controlcode As ControlCode)
    Me.controlcode = controlcode
    With controlcode
      ' Write enable the fan setpoints
      For i As Integer = 0 To 7
        Dim top = topFanSP + i
        Dim bottom = bottomFanSP + i
        ' .Setpoints(top).WriteEnabled = True
        ' .Setpoints(bottom).WriteEnabled = True
      Next

      ' .Setpoints(fanStart).WriteEnabled = True
      ' .Setpoints(fanStop).WriteEnabled = True
    End With
  End Sub

  Sub SetTopFanSpeed(zone As Integer, value As Integer)
    ' TODO check parameters
    With controlcode
      If zone < 0 OrElse zone > 8 Then Exit Sub
      If value < 0 OrElse value > 10000 Then Exit Sub  ' 0% to 100%

      Dim setpoint As Integer
      If zone = 0 Then
        For i As Integer = 0 To 7
          setpoint = topFanSP + i
          '   .Setpoints(setpoint).Write(value)
        Next i
      Else
        setpoint = topFanSP + (zone - 1)
        '  .Setpoints(setpoint).Write(value)
      End If

    End With
  End Sub

  Sub SetBottomFanSpeed(zone As Integer, value As Integer)
    ' TODO check parameters
    With controlcode
      If zone < 0 OrElse zone > 8 Then Exit Sub
      If value < 0 OrElse value > 10000 Then Exit Sub

      Dim setpoint As Integer
      If zone = 0 Then
        For i As Integer = 0 To 7
          setpoint = bottomFanSP + i
          '   .Setpoints(setpoint).Write(value)
        Next i
      Else
        setpoint = bottomFanSP + (zone - 1)
        '  .Setpoints(setpoint).Write(value)
      End If

    End With
  End Sub

  Sub SetFanMode(mode As Integer)

  End Sub

  ' Simulates start pushbutton press
  Sub StartFans()
    With controlcode
      StartFan = True
      StartFanTimer.Seconds = 4
      '  .Setpoints(fanStart).Write(1)
    End With
  End Sub

  Private Sub StartFansClear()
    With controlcode
      StartFan = False
      '  .Setpoints(fanStart).Write(0)
    End With
  End Sub

  ' Simulates stop pushbutton press
  Sub StopFans()
    With controlcode
      '  .Setpoints(fanStop).Write(0)
      StopFanPress = True
      StopFanTimer.Seconds = 4
    End With
  End Sub

  Private Sub StopFansClear()
    With controlcode
      StopFanPress = False
      '  .Setpoints(fanStop).Write(1)
    End With
  End Sub

  Sub Run()
    ' Release pushbuttons 
    If StartFan AndAlso StartFanTimer.Finished Then StartFansClear()
    If StopFanPress AndAlso StopFanTimer.Finished Then StopFansClear()
  End Sub

End Class
