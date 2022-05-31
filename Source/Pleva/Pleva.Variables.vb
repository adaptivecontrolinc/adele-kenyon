Partial Class Pleva

#Region "Control Properties"

  Private statePT_ As EPlevaTemp
  Public ReadOnly Property StatePT() As EPlevaTemp
    Get
      Return statePT_
    End Get
  End Property

  Private statestringPT_ As String
  Public ReadOnly Property StateStringPT() As String
    Get
      Return statestringPT_
    End Get
  End Property

  Private statePH_ As EPlevaHumidty
  Public ReadOnly Property StatePH() As EPlevaHumidty
    Get
      Return statePH_
    End Get
  End Property

  Private statestringPH_ As String
  Public ReadOnly Property StateStringPH() As String
    Get
      Return statestringPH_
    End Get
  End Property

#End Region

#Region "Timers"

  Private adjustFabricSpeedTimer_ As New Timer
  Public ReadOnly Property AdjustFabricSpeedTimerDisplay As String
    Get
      Return adjustFabricSpeedTimer_.ToString
    End Get
  End Property

  Private adjustFanSpeedTimer_ As New Timer
  Public ReadOnly Property AdjustFanSpeedTimerDisplay As String
    Get
      Return adjustFanSpeedTimer_.ToString
    End Get
  End Property
#End Region

#Region "Pleva Temperature Variables"

  Private tempZone1_ As Integer                       'Pleva Zone 1 Temp (Zones 2-3)
  Public Property TempZone1() As Integer
    Get
      Return tempZone1_
    End Get
    Private Set(ByVal value As Integer)
      tempZone1_ = value
    End Set
  End Property

  Private tempZone2_ As Integer                       'Pleva Zone 2 Temp (Zones 3-4)
  Public Property TempZone2() As Integer
    Get
      Return tempZone2_
    End Get
    Private Set(ByVal value As Integer)
      tempZone2_ = value
    End Set
  End Property

  Private tempZone3_ As Integer                       'Pleva Zone 3 Temp (Zones 4-5)
  Public Property TempZone3() As Integer
    Get
      Return tempZone3_
    End Get
    Private Set(ByVal value As Integer)
      tempZone3_ = value
    End Set
  End Property

  Private tempZone4_ As Integer                       'Pleva Zone 4 Temp (Zones 5-6)
  Public Property TempZone4() As Integer
    Get
      Return tempZone4_
    End Get
    Private Set(ByVal value As Integer)
      tempZone4_ = value
    End Set
  End Property

  Private tempDesired_ As Integer
  Public Property TempDesired() As Integer
    Get
      Return tempDesired_
    End Get
    Set(ByVal value As Integer)
      tempDesired_ = MinMax(value, tempLimitLow_, tempLimitHigh_)
    End Set
  End Property

  Private tempLimitLow_ As Integer
  Public ReadOnly Property TempLimitLow() As Integer
    Get
      Return tempLimitLow_
    End Get
  End Property

  Private tempLimitHigh_ As Integer
  Public ReadOnly Property TempLimitHigh() As Integer
    Get
      Return tempLimitHigh_
    End Get
  End Property

  Private timeAtTempActual_ As Double
  Public ReadOnly Property TimeAtTempActual() As Double
    Get
      Return Math.Round(timeAtTempActual_, 3)
    End Get
  End Property

  Public ReadOnly Property TimeAtTempActualStr() As String
    Get
      Return Math.Round(timeAtTempActual_ / 10, 1).ToString & " sec"
    End Get
  End Property

  Private timeAtTempDesired_ As Integer
  Public Property TimeAtTempDesired() As Integer
    Get
      Return timeAtTempDesired_
    End Get
    Set(ByVal value As Integer)
      timeAtTempDesired_ = value
    End Set
  End Property

  Private timeError_ As Integer
  Public ReadOnly Property TimeError() As Integer
    Get
      Return timeError_
    End Get
  End Property

  Private speedFabricAdjust_ As Integer
  Public ReadOnly Property SpeedFabricAdjust() As Integer
    Get
      Return speedFabricAdjust_
    End Get
  End Property

  Private speedFabricAdjustCount_ As Integer
  Public ReadOnly Property SpeedFabricAdjustCount As Integer
    Get
      If speedFabricAdjustCount_ >= Integer.MaxValue - 10 Then speedFabricAdjustCount_ = 0
      Return speedFabricAdjustCount_
    End Get
  End Property

  Private speedFabricActual_ As Integer
  Public ReadOnly Property SpeedFabricActual() As Integer
    Get
      Return speedFabricActual_
    End Get
  End Property

  Private speedFabricDesired_ As Integer
  Public ReadOnly Property SpeedFabricDesired() As Integer
    Get
      Return speedFabricDesired_
    End Get
  End Property

  Private speedFabricLimitHigh_ As Integer
  Public ReadOnly Property SpeedFabricLimitHigh() As Integer
    Get
      Return speedFabricLimitHigh_
    End Get
  End Property

  Private speedFabricLimitLow_ As Integer
  Public ReadOnly Property SpeedFabricLimitLow() As Integer
    Get
      Return speedFabricLimitLow_
    End Get
  End Property

#End Region

#Region "Pleva Humidity Variables"

  Private humidityActual_ As Integer                       'Pleva Humidity Actual
  Public Property HumidityActual() As Integer
    Get
      Return humidityActual_
    End Get
    Private Set(ByVal value As Integer)
      humidityActual_ = value
    End Set
  End Property

  Private humidityDesired_ As Integer                       'Pleva Humidity Desired
  Public Property HumidityDesired() As Integer
    Get
      Return humidityDesired_
    End Get
    Set(ByVal value As Integer)
      humidityDesired_ = value
    End Set
  End Property

  Private humidityError_ As Integer
  Public ReadOnly Property HumidityError() As Integer
    Get
      Return humidityError_
    End Get
  End Property

  Private speedFanAdjust_ As Integer
  Public ReadOnly Property SpeedFanAjust() As Integer
    Get
      Return speedFanAdjust_
    End Get
  End Property

  Private speedFanAdjustCount_ As Integer
  Public ReadOnly Property SpeedFanAdjustCount As Integer
    Get
      If speedFanAdjustCount_ >= Integer.MaxValue - 10 Then speedFanAdjustCount_ = 0
      Return speedFanAdjustCount_
    End Get
  End Property

  Private speedFanActual_ As Integer
  Public ReadOnly Property SpeedFanActual() As Integer
    Get
      Return speedFanActual_
    End Get
  End Property

  Private speedFanDesired_ As Integer
  Public ReadOnly Property SpeedFanDesired() As Integer
    Get
      Return speedFanDesired_
    End Get
  End Property

  Private speedFanLimitHigh_ As Integer
  Public ReadOnly Property SpeedFanLimitHigh() As Integer
    Get
      Return speedFanLimitHigh_
    End Get
  End Property

  Private speedFanLimitLow_ As Integer
  Public ReadOnly Property SpeedFanLimitLow() As Integer
    Get
      Return speedFanLimitLow_
    End Get
  End Property

  Private speedAdjustRequested_ As Boolean
  Public ReadOnly Property SpeedAdjustRequested() As Boolean
    Get
      Return speedAdjustRequested_
    End Get
  End Property

#End Region

End Class
