Public Class GasFlowmeter

  Private volumeFromPlc_ As Integer = 0

  Private previousVolume_ As Double       'Used to calculate a flow rate
  Private previousVolumeTime_ As Date     '

  'Set at start for an initial value
  Private volumeStart_ As Integer
  Public Property VolumeStart() As Integer
    Get
      Return volumeStart_
    End Get
    Set(ByVal value As Integer)
      volumeStart_ = value
    End Set
  End Property

  'Current value of counter
  Public Property VolumeFromPlc() As Integer
    Get
      Return volumeFromPlc_
    End Get
    Set(ByVal value As Integer)
      If CounterValid(value) Then
        'If the new value is less than the current value then the counter must have reset locally at PLC
        If value < volumeFromPlc_ Then
          totalVolumePriorWrap_ += (volumeFromPlc_ - VolumeStart) 'Store the current total volume up to this point
          CounterWraps += 1
          volumeStart_ = 0
        End If

        volumeFromPlc_ = value

        If Date.Now.Subtract(previousVolumeTime_).TotalSeconds > 5 Then
          CalculateFlowRate()
          previousVolumeTime_ = Date.Now
          previousVolume_ = TotalVolume
        End If

      End If

    End Set
  End Property

  'Total Volume
  Public ReadOnly Property TotalVolume() As Integer
    Get
      If counterWraps_ > 0 Then
        Return (volumeFromPlc_ - VolumeStart) + totalVolumePriorWrap_
      Else
        Return (volumeFromPlc_ - VolumeStart)
      End If
    End Get
  End Property

  'Total Volume Prior Wrap
  Private totalVolumePriorWrap_ As Integer
  Public ReadOnly Property TotalVolumePriorWrap() As Integer
    Get
      Return totalVolumePriorWrap_
    End Get
  End Property

  'Number of times the counter has wrapped:
  '  PLC is coded to wrap at a specific time (23hr:00min:01sec)
  Private counterWraps_ As Integer = 0
  Public Property CounterWraps() As Integer
    Get
      Return counterWraps_
    End Get
    Private Set(ByVal value As Integer)
      counterWraps_ = value
    End Set
  End Property

  Public Sub InitializeNewStart(ByVal volume As Integer)

    VolumeStart = volume
    CounterWraps = 0
    totalVolumePriorWrap_ = 0

    'Setup FlowRate Values for New Batch
    previousVolume_ = TotalVolume
    previousVolumeTime_ = Date.Now
  End Sub


#Region "Counter Valid"

  Private Function CounterValid(ByVal value As Integer) As Boolean
    'We seem to be getting some noise on the counter which is triggering a false counter wrap
    '   so keep track of the last few counter values and only accept the counter if the values are pretty close
    Try

      Static lastFourValues(3) As Integer

      'Log the attempted wrap...
      If value < VolumeFromPlc Then
        Dim message As String = "Counter wrap (" & value.ToString & ") ->"
        ' For i As Integer = 0 To 3 : message &= " (" & lastFourValues(i).ToString & ") " : Next
        'Utilities.Log.LogEvent(message)
        Return True
      End If

      Dim averageValue As Integer
      For i As Integer = 0 To 3 : averageValue += lastFourValues(i) : Next
      averageValue = averageValue \ 4

      If lastFourValues(0) <> value Then
        'If this is a new value shuffle the older values up and put the new one at index 0
        lastFourValues(3) = lastFourValues(2)
        lastFourValues(2) = lastFourValues(1)
        lastFourValues(1) = lastFourValues(0)
        lastFourValues(0) = value
      End If

      If Math.Abs(value - averageValue) < 10000 Then
        Return True
      End If

      Return False
    Catch ex As Exception
      'Ignore errors - if any
      Debug.Print(ex.Message, ex.StackTrace)
    End Try
    Return True
  End Function

#End Region

#Region "Gas Flow Rate"

  Private cubicFeetPerMinute_ As Double
  Public Property CubicFeetPerMinute() As Double
    Get
      Return Math.Round(cubicFeetPerMinute_, 3)
    End Get
    Private Set(ByVal value As Double)
      cubicFeetPerMinute_ = value
    End Set
  End Property

  Private Sub CalculateFlowRate()
    Try
      'Make sure we have actually recorded a previous amount
      If previousVolumeTime_ = Nothing Then Exit Sub

      'Calculate change in liters and time
      Dim deltaCubicFeet As Double = TotalVolume - previousVolume_
      Dim deltaSeconds As Double = Date.Now.Subtract(previousVolumeTime_).TotalSeconds
      Dim deltaMinutes As Double = deltaSeconds / 60

      If (deltaCubicFeet > 1) Or (deltaSeconds > 0.2) Then
        CubicFeetPerMinute = deltaCubicFeet / deltaMinutes
        previousVolume_ = TotalVolume
        previousVolumeTime_ = Date.Now

      End If
    Catch ex As Exception

    End Try
  End Sub

#End Region

#Region "DecaTherm Calculations"

  'http://tonto.eia.doe.gov/merquery/mer_data.asp?table=TA4
  'Also saved to Kenyon2.XLS in root directory  HEATCONTENTVALUE

  'From Wikipedia:
  ' The therm (symbol thm) is a non-SI unit of heat energy equal to 100,000 British thermal units (BTU).
  ' It is approximately the energy equivalent of burning 100 cubic feet (often referred to as 1 Ccf) of natural gas.
  'Therm (US) = 100,000 BTU 
  'Dth = 10 Therms = 1,000,000 BTU's
  'DecaTherm = (CubicFeet * HeatContentValue) / 1000000

  Private decaTherm_ As Double
  Public ReadOnly Property DecaTherm() As Double
    Get
      decaTherm_ = (TotalVolume * heatContent_) / 1000000
      Return Math.Round(decaTherm_, 3)
    End Get
  End Property

  Private heatContent_ As Integer
  Public Property HeatContent() As Integer
    Get
      Return heatContent_
    End Get
    Set(ByVal value As Integer)
      heatContent_ = value
    End Set
  End Property


#End Region

End Class
