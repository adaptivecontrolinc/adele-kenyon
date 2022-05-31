' Version 1.0.1 [2022-05-20] DH
' Kenyon Frame Transport Control Application
' Automation Direct DL262 w/ H2-ECOM100 Ethernet module
' Each SpeedController class module represents one motor control within Transport PLC (Tenter, Overfeed Top, Overfeed Bottom, Selvage L & R, Padder, etc)

Public Class SpeedController
  Inherits MarshalByRefObject
  Implements IController

  'Keep a local reference to the control code object for convenience
  Private ReadOnly controlCode As ControlCode
  Public Sub New(ByVal controlCode As ControlCode)
    Me.controlCode = controlCode
  End Sub

  Public Enum EUpdateState
    Idle
    Request
    Sent            'Coms ok response from Write method
    Verified        'desired setpoint read from device corresponds to what we wrote
    Offline
  End Enum

  Public Enum EModeState
    Manual
    Automatic
  End Enum

#Region " PLC RAW PARAMETER VALUES - FILLED IN DURING I/O READS"

  Private prm_AutoEnable_ As Integer
  Public Property Prm_AutoEnable As Integer
    Get
      Return prm_AutoEnable_
    End Get
    Set(value As Integer)
      prm_AutoEnable_ = value
    End Set
  End Property

  Private prm_SpeedMaxRange_ As Integer
  Public Property Prm_SpeedMaxRange As Integer
    Get
      Return prm_SpeedMaxRange_
    End Get
    Set(value As Integer)
      prm_SpeedMaxRange_ = value
    End Set
  End Property

  Private prm_SetpointMin_ As Integer
  Public Property Prm_SetpointMin() As Integer
    Get
      Return prm_SetpointMin_
    End Get
    Set(ByVal value As Integer)
      prm_SetpointMin_ = value
    End Set
  End Property

  Private prm_SetpointMax_ As Integer
  Public Property Prm_SetpointMax() As Integer
    Get
      Return prm_SetpointMax_
    End Get
    Set(ByVal value As Integer)
      prm_SetpointMax_ = value
    End Set
  End Property

  Private prm_GainRamp_ As Integer
  Public Property Prm_GainRamp As Integer
    Get
      Return prm_GainRamp_
    End Get
    Set(value As Integer)
      prm_GainRamp_ = value
    End Set
  End Property

  Private prm_GainAtSpd_ As Integer
  Public Property Prm_GainAtSpd As Integer
    Get
      Return prm_GainAtSpd_
    End Get
    Set(value As Integer)
      prm_GainAtSpd_ = value
    End Set
  End Property

  Private prm_GainMaxAdj_ As Integer
  Public Property Prm_GainMaxAdj As Integer
    Get
      Return prm_GainMaxAdj_
    End Get
    Set(value As Integer)
      prm_GainMaxAdj_ = value
    End Set
  End Property

  Private prm_SpdErrorAllow_ As Integer
  Public Property Prm_SpdErrorAllow As Integer
    Get
      Return prm_SpdErrorAllow_
    End Get
    Set(value As Integer)
      prm_SpdErrorAllow_ = value
    End Set
  End Property

#End Region

#Region " PLC RAW VARIABLE VALUES - FILLED IN DURING I/O READS"

  Private plc_SpeedActual_ As Integer
  Public Property Plc_SpeedActual As Integer
    Get
      Return plc_SpeedActual_
    End Get
    Set(value As Integer)
      plc_SpeedActual_ = value
    End Set
  End Property

  Private plc_SpeedDesired_ As Integer
  Public Property Plc_SpeedDesired As Integer
    Get
      Return plc_SpeedDesired_
    End Get
    Set(value As Integer)
      plc_SpeedDesired_ = value
    End Set
  End Property

  Private plc_SpeedOffset_ As Integer
  Public Property Plc_SpeedOffset As Integer
    Get
      Return plc_SpeedOffset_
    End Get
    Set(value As Integer)
      plc_SpeedOffset_ = value
    End Set
  End Property

  Private plc_SpeedError_ As Integer
  Public Property Plc_SpeedError As Integer
    Get
      Return plc_SpeedError_
    End Get
    Set(value As Integer)
      plc_SpeedError_ = value
    End Set
  End Property

  Private plc_OutputGainAdj_ As Integer
  Public Property Plc_OutputGainAdj As Integer
    Get
      Return plc_OutputGainAdj_
    End Get
    Set(value As Integer)
      plc_OutputGainAdj_ = value
    End Set
  End Property

  Private plc_OutputPercent_ As Integer
  Public Property Plc_OutputPercent As Integer
    Get
      Return plc_OutputPercent_
    End Get
    Set(value As Integer)
      plc_OutputPercent_ = value
    End Set
  End Property

#End Region

#Region "SETPOINT CONTROL"

  Private setpointDesired_ As Integer
  Public ReadOnly Property SetpointDesired() As Integer
    Get
      Return setpointDesired_
    End Get
  End Property

  Private setpointActual_ As Integer
  Public Property SetpointActual As Integer
    Get
      Return setpointActual_
    End Get
    Set(value As Integer)
      setpointActual_ = value
      'Not requesting a new setpoint, update setpoint desired with remote value (may have changed elsewhere)
      If (setpointStatus_ <> EUpdateState.Request) Then setpointDesired_ = setpointActual_
    End Set
  End Property

  Private setpointStatus_ As EUpdateState
  Public ReadOnly Property SetpointStatus() As EUpdateState
    Get
      Return setpointStatus_
    End Get
  End Property

  Friend ReadOnly Property SetpointComplete As Boolean
    Get
      Select Case SetpointStatus
        Case EUpdateState.Idle, EUpdateState.Sent, EUpdateState.Verified, EUpdateState.Offline
          Return True
        Case EUpdateState.Request
          Return False
      End Select
    End Get
  End Property

  Public Sub SetpointStatusClear()
    'Clear the Setpoint Request
    setpointStatus_ = EUpdateState.Idle
  End Sub

#End Region

#Region "INTERFACES"

  Friend ReadOnly Property ISetpointMaximum() As Integer Implements IController.SetpointMaximum
    Get
      Return prm_SetpointMax_
    End Get
  End Property

  Friend ReadOnly Property ISetpointMinimum() As Integer Implements IController.SetpointMinimum
    Get
      Return prm_SetpointMin_
    End Get
  End Property

  'PLC Values Decimal 0-999 represent 0-999%.  115 represents 115% or 1.15
  Public Enum EDecimalPosition
    None = 1        '999 = 999
    One = 10        '999 = 99.9
    Two = 100       '999 = 9.99
  End Enum
  Private decimalPosition_ As EDecimalPosition = EDecimalPosition.One
  Friend ReadOnly Property ISetpointFactor As Integer Implements IController.SetpointFactor
    Get
      Return decimalPosition_
    End Get
  End Property

  Private adjustResult_ As EControllerAdjustResult
  Public ReadOnly Property IAdjustResult As EControllerAdjustResult Implements IController.AdjustResult
    Get
      Return adjustResult_
    End Get
  End Property

  Private adjustString_ As String
  Public ReadOnly Property IAdjustString() As String Implements IController.AdjustString
    Get
      Return adjustString_
    End Get
  End Property

  Friend ReadOnly Property ISetpointCurrent As Integer Implements IController.SetpointCurrent
    Get
      Return SetpointDesired
    End Get
  End Property

  ' TODO
  Public Function Increase(increment As Integer, expert As Boolean) As Boolean Implements IController.Increase
    Try

      Dim newSetpoint As Integer = setpointDesired_ + increment
      'Test to make sure that new setpoint remains within the deviance limits and max/min values
      If newSetpoint <= prm_SetpointMax_ Then
        setpointDesired_ += increment
        setpointStatus_ = EUpdateState.Request
        Return True
      Else
        adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
        adjustString_ = " New Setpoint " & (newSetpoint / decimalPosition_).ToString & displayUnitsSetpoint_ &
                          " Exceeds Max Setpoint Limit: " & (prm_SetpointMax_ / decimalPosition_).ToString & displayUnitsSetpoint_
      End If

    Catch ex As Exception
      'log error
    End Try
    Return False
  End Function

  ' TODO
  Public Function Decrease(increment As Integer, expert As Boolean) As Boolean Implements IController.Decrease
    Try

      Dim newSetpoint As Integer = setpointDesired_ - increment
      'Test to make sure that new setpoint remains within the deviance limits and max/min values
      If newSetpoint >= prm_SetpointMin_ Then
        setpointDesired_ -= increment
        setpointStatus_ = EUpdateState.Request
        Return True
      Else
        adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
        adjustString_ = " New Setpoint " & (newSetpoint / ISetpointFactor).ToString & " " & IUnits &
                         " Below Min Setpoint Limit: " & (prm_SetpointMin_ / ISetpointFactor).ToString & " " & IUnits
      End If

    Catch ex As Exception
      'log error
    End Try
  End Function

  Private changeSetpointEnable_ As Boolean
  Public ReadOnly Property ChangeSetpointEnabled As Boolean Implements IController.ChangeSetpointEnabled
    Get
      changeSetpointEnable_ = (controlCode.Parameters.MsSetpointAdjustEnable = 1)
      Return changeSetpointEnable_
    End Get
  End Property

  ' TODO
  Friend Function IChangeSetpoint(setpoint As Integer, expert As Boolean) As Boolean Implements IController.IChangeSetpoint
    Try

      If Not controlCode.FirstScanDone Then Exit Function

      Dim newSetpoint As Integer = setpoint
      If newSetpoint = setpointDesired_ Then Exit Function

      'Test to make sure that new setpoint remains within the deviance limits and max/min values
      If (newSetpoint >= ISetpointMinimum) AndAlso (newSetpoint <= ISetpointMaximum) Then
        setpointDesired_ = newSetpoint
        setpointStatus_ = EUpdateState.Request
        adjustString_ = ""

        Return True
      Else
        'Outside of limits (which limit?)
        If newSetpoint < prm_SetpointMin_ Then
          adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
          adjustString_ = " New Setpoint " & (newSetpoint / ISetpointFactor).ToString & " " & IUnits &
                          " Below Lower Setpoint Limit: " & (prm_SetpointMin_ / ISetpointFactor).ToString & " " & IUnits

        ElseIf newSetpoint > prm_SetpointMax_ Then
          adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
          adjustString_ = " New Setpoint " & (newSetpoint / ISetpointFactor).ToString & " " & IUnits &
                          " Exceeds Max Setpoint Limit: " & (prm_SetpointMax_ / ISetpointFactor).ToString & " " & IUnits

        End If
      End If

    Catch ex As Exception
      'log error
    End Try
  End Function

  Private description_ As String
  Public Property IDescription() As String Implements IController.Description
    Get
      Return description_
    End Get
    Set(ByVal value As String)
      description_ = value
    End Set
  End Property

  Friend ReadOnly Property IUnits As String Implements IController.Units
    Get
      Return displayUnitsSetpoint_
    End Get
  End Property

  Private displayUnitsSetpoint_ As String = ""
  Friend Property IDisplayUnitsSetpoint As String
    Get
      Return displayUnitsSetpoint_
    End Get
    Set(value As String)
      displayUnitsSetpoint_ = value
    End Set
  End Property

  Private displayUnits_ As String = ""
  Friend Property DisplayUnits As String
    Get
      Return displayUnits_
    End Get
    Set(value As String)
      displayUnits_ = value
    End Set
  End Property

  Public ReadOnly Property IDisplayActual As String Implements IController.Actual
    Get
      Return (Plc_SpeedActual / 10).ToString & displayUnits_
    End Get
  End Property

  Public ReadOnly Property IDisplaySetpoint As String Implements IController.Setpoint
    Get
      Return (SetpointDesired / decimalPosition_).ToString & IDisplayUnitsSetpoint
    End Get
  End Property

  Friend ReadOnly Property IDisplaySetpointPercent As String
    Get
      Return "Setpoint: " & (SetpointDesired / decimalPosition_).ToString & IDisplayUnitsSetpoint
    End Get
  End Property

  Friend ReadOnly Property IDisplaySetpointUnits As String
    Get
      Return "Setpoint Speed: " & (Plc_SpeedDesired / 10).ToString & " " & DisplayUnits
    End Get
  End Property

  Public ReadOnly Property DisplayStatus As String Implements IController.Status
    Get
      Dim response As String = "Idle"
      'Select Case SetpointStatus
      '  Case EUpdateState.Idle, EUpdateState.Sent
      '    response = " Output: " & (IOutputPct / 10).ToString & "%" '  response = "Idle" & " Output: " & (IOutputPct / 10).ToString & "%"
      '  Case EUpdateState.Request
      '    response = "Requested"
      '  Case EUpdateState.Offline
      '    response = "Offline"
      'End Select
      response = " Output: " & (IOutputPct / 10).ToString & "%"
      Return response
    End Get
  End Property

  Friend ReadOnly Property IOutputPct As Integer Implements IController.OutputPercent
    Get
      Return Plc_OutputPercent
    End Get
  End Property

  Private zone_ As Integer
  Friend Property Zone As Integer Implements IController.Zone
    Get
      Return zone_
    End Get
    Set(value As Integer)
      zone_ = value
    End Set
  End Property

#End Region

#Region " COMMUNICATIONS "

  Private scanLast_ As Date
  Public Property Coms_ScanLast() As Date
    Get
      Return scanLast_
    End Get
    Set(ByVal value As Date)
      commsTimer_.Seconds = 10
      scanInterval_ = CInt((value - Coms_ScanLast).TotalMilliseconds)
      scanLast_ = value
    End Set
  End Property

  Private scanInterval_ As Integer
  Public ReadOnly Property Coms_ScanInterval() As Integer
    Get
      Return scanInterval_
    End Get
  End Property

  Private commsTimer_ As New Timer
  Public ReadOnly Property Coms_Timeout() As Boolean
    Get
      If commsTimer_.Finished Then
        ClearValues()
        Return True
      Else
        Return False
      End If
    End Get
  End Property

  Public Sub ClearValues()
    plc_SpeedActual_ = 0
    plc_SpeedDesired_ = 0
    plc_OutputPercent_ = 0
  End Sub

#End Region

#Region " CONTROL VARIABLES "   'Based on active procedure's command parameters 
  '   NOTE 1: not currently stored on the PLC - Transport PLC can override these values, as Microspeeds could as well
  '   NOTE 2: May add to PLC for more setpoint control in future release

  Private spt_DevianceHigh_ As Integer
  Public Property Spt_DevianceHigh As Integer
    Get
      Return spt_DevianceHigh_
    End Get
    Set(value As Integer)
      spt_DevianceHigh_ = MinMax(value, prm_SetpointMin_, prm_SetpointMax_)
    End Set
  End Property

  Private spt_DevianceLow_ As Integer
  Public Property Spt_DevianceLow As Integer
    Get
      Return spt_DevianceLow_
    End Get
    Set(value As Integer)
      spt_DevianceLow_ = MinMax(value, prm_SetpointMin_, prm_SetpointMax_)
    End Set
  End Property

  Private spt_LimitUpper_ As Integer
  Public Property Spt_LimitUpper As Integer
    Get
      Return spt_LimitUpper_
    End Get
    Set(value As Integer)
      spt_LimitUpper_ = MinMax(value, Prm_SetpointMin + 100, Prm_SetpointMax)
    End Set
  End Property

  Private spt_LimitLower_ As Integer
  Public Property Spt_LimitLower As Integer
    Get
      Return spt_LimitLower_
    End Get
    Set(value As Integer)
      spt_LimitLower_ = MinMax(value, Prm_SetpointMin, Prm_SetpointMax)
    End Set
  End Property

#End Region



End Class
