' Version 1.1 [2022-05-17] DH
' Adele Knits - Kenyon Frame Transport Control Application
'  Add Expert user function to change setpoints outside the procedure deviation
' Automation Direct DL260 w/ H2-ECOM100 Ethernet module
' Each SpeedController class module represents one motor control within Transport PLC (Tenter, Overfeed Top, Overfeed Bottom, Selvage L & R, Padder, etc)

Public Class TenterController
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

  Public Enum EModeState 'TODO - Use with enable parameter?
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

  Private prm_SetpointValue1_ As Integer
  Public Property Prm_SetpointValue1 As Integer
    Get
      Return prm_SetpointValue1_
    End Get
    Set(value As Integer)
      prm_SetpointValue1_ = value
    End Set
  End Property

  Private prm_SetpointValue2_ As Integer
  Public Property Prm_SetpointValue2 As Integer
    Get
      Return prm_SetpointValue2_
    End Get
    Set(value As Integer)
      prm_SetpointValue2_ = value
    End Set
  End Property

  Private prm_SetpointValue3_ As Integer
  Public Property Prm_SetpointValue3 As Integer
    Get
      Return prm_SetpointValue3_
    End Get
    Set(value As Integer)
      prm_SetpointValue3_ = value
    End Set
  End Property

  Private prm_SetpointValue4_ As Integer
  Public Property Prm_SetpointValue4 As Integer
    Get
      Return prm_SetpointValue4_
    End Get
    Set(value As Integer)
      prm_SetpointValue4_ = value
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

  Private plc_SpeedError_ As Integer
  Public Property Plc_SpeedError As Integer
    Get
      Return plc_SpeedError_
    End Get
    Set(value As Integer)
      plc_SpeedError_ = value
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

  Private plc_SetpointChActive_ As Integer           '1-4
  Public Property Plc_SetpointChActive As Integer
    Get
      Return plc_SetpointChActive_
    End Get
    Set(value As Integer)
      plc_SetpointChActive_ = value
    End Set
  End Property

  Private plc_SetpointCh2Req_ As Integer              '0-1
  Public Property Plc_SetpointCh2Request As Integer
    Get
      Return plc_SetpointCh2Req_
    End Get
    Set(value As Integer)
      plc_SetpointCh2Req_ = value
    End Set
  End Property

  'Boolean Flags
  Private plc_AtSpeed_ As Boolean
  Public Property Plc_AtSpeed As Boolean
    Get
      Return plc_AtSpeed_
    End Get
    Set(value As Boolean)
      plc_AtSpeed_ = value
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
      'Not requesting a new setpoint, update setpoint desired with remove value (may have changed elsewhere)
      If setpointStatus_ <> EUpdateState.Request Then
        If setpointDesired_ = value Then setpointStatus_ = EUpdateState.Idle '20220511
        setpointDesired_ = setpointActual_
      End If
    End Set
  End Property

  Public ReadOnly Property SetpointStatusDisplay() As String
    Get
      If setpointStatus_ = EUpdateState.Offline Then
        Return "Offline"
      ElseIf setpointStatus_ = EUpdateState.Verified Then
        Return "Verified"
      ElseIf setpointStatus_ = EUpdateState.Sent Then
        Return "Sent"
      ElseIf setpointStatus_ = EUpdateState.Request Then
        Return "Requested"
      Else
        Return "Idle"
      End If
    End Get
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
    setpointStatus_ = EUpdateState.Sent
  End Sub

#End Region

#Region "CONTROL SETPOINT VARIABLES"   'Based on active procedure's command parameters 
  '   NOTE 1: not currently stored on the PLC - Transport PLC can override these values, as Microspeeds could as well
  '   NOTE 2: May add to PLC for more setpoint control in future release

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

  Private spt_DevianceHigh_ As Integer
  Public Property Spt_DevianceHigh() As Integer
    Get
      Return spt_DevianceHigh_
    End Get
    Set(ByVal value As Integer)
      spt_DevianceHigh_ = MinMax(value, prm_SetpointMin_, prm_SetpointMax_)
    End Set
  End Property

  Private spt_DevianceLow_ As Integer
  Public Property Spt_DevianceLow() As Integer
    Get
      Return spt_DevianceLow_
    End Get
    Set(ByVal value As Integer)
      spt_DevianceLow_ = MinMax(value, prm_SetpointMin_, prm_SetpointMax_)
    End Set
  End Property

#End Region

#Region "INTERFACES"

  Private ReadOnly Property ISetpointMaximum() As Integer Implements IController.SetpointMaximum
    Get
      Return prm_SetpointMax_
    End Get
  End Property

  Private ReadOnly Property ISetpointMinimum() As Integer Implements IController.SetpointMinimum
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
  Private ReadOnly Property ISetpointFactor As Integer Implements IController.SetpointFactor
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

  Private ReadOnly Property ISetpointCurrent As Integer Implements IController.SetpointCurrent
    Get
      Return SetpointDesired
    End Get
  End Property

  ' TODO
  Public Function IIncrease(increment As Integer, expert As Boolean) As Boolean Implements IController.Increase
    Try

      Dim newSetpoint As Integer = Prm_SetpointValue4 + increment

      'Test to make sure that new setpoint remains within the deviance limits and max/min values
      If newSetpoint <= prm_SetpointMax_ Then
        setpointDesired_ = newSetpoint
        setpointStatus_ = EUpdateState.Request
        Return True
      Else
        adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
        adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & displayUnitsSetpoint_ &
                          " Exceeds Max Setpoint Limit: " & (prm_SetpointMax_ / 10).ToString & displayUnitsSetpoint_
      End If

    Catch ex As Exception
      'log error
    End Try
    Return False
  End Function

  ' TODO
  Public Function IDecrease(increment As Integer, expert As Boolean) As Boolean Implements IController.Decrease
    Try

      Dim newSetpoint As Integer = Prm_SetpointValue4 + increment

      'Test to make sure that new setpoint remains within the deviance limits and max/min values
      If newSetpoint >= prm_SetpointMin_ Then
        setpointDesired_ = newSetpoint
        setpointStatus_ = EUpdateState.Request
        Return True
      Else
        adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
        adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & " " & IUnits &
                         " Below Min Setpoint Limit: " & (prm_SetpointMin_ / 10).ToString & " " & IUnits
      End If

    Catch ex As Exception
      'log error
    End Try
  End Function

  Private changeSetpointEnable_ As Boolean
  Public ReadOnly Property IChangeSetpointEnabled As Boolean Implements IController.ChangeSetpointEnabled
    Get
      changeSetpointEnable_ = (controlCode.Parameters.TenterAdjustEnable = 1)
      ' changeSetpointEnable_ = (controlCode.Parameters.MsSetpointAdjustEnable = 1)
      Return changeSetpointEnable_
    End Get
  End Property

  Friend Function IChangeSetpoint(setpoint As Integer, expert As Boolean) As Boolean Implements IController.IChangeSetpoint
    Try
      If Not controlCode.FirstScanDone Then Exit Function

      Dim newSetpoint As Integer = setpoint
      If newSetpoint = Prm_SetpointValue4 Then Exit Function

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
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & " " & IUnits &
                          " Below Lower Setpoint Limit: " & (prm_SetpointMin_ / 10).ToString & " " & IUnits

        ElseIf newSetpoint > prm_SetpointMax_ Then
          adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & " " & IUnits &
                          " Exceeds Max Setpoint Limit: " & (prm_SetpointMax_ / 10).ToString & " " & IUnits

        End If
      End If

    Catch ex As Exception
      'log error
    End Try
  End Function

  Private idescription_ As String
  Public Property IDescription() As String Implements IController.Description
    Get
      Return idescription_
    End Get
    Set(ByVal value As String)
      idescription_ = value
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
  Friend Property IDisplayUnits As String
    Get
      Return displayUnits_
    End Get
    Set(value As String)
      displayUnits_ = value
    End Set
  End Property

  Public ReadOnly Property IDisplayActual As String Implements IController.Actual
    Get
      '    Return (Plc_SpeedActual / ISetpointFactor).ToString & displayUnits_
      Select Case Plc_SetpointChActive
        Case 2 : Return (Prm_SetpointValue2 / ISetpointFactor).ToString & displayUnits_
        Case 3 : Return (Prm_SetpointValue3 / ISetpointFactor).ToString & displayUnits_
        Case 4 : Return (Prm_SetpointValue4 / ISetpointFactor).ToString & displayUnits_
        Case Else : Return (Prm_SetpointValue1 / ISetpointFactor).ToString & displayUnits_
      End Select
    End Get
  End Property
  Friend ReadOnly Property IDisplaySetpoint As String Implements IController.Setpoint
    Get
      ' Return (Plc_SpeedDesired / 10).ToString & displayUnitsSetpoint_
      Select Case Plc_SetpointChActive
        Case 2 : Return (Prm_SetpointValue2 / ISetpointFactor).ToString & displayUnitsSetpoint_
        Case 3 : Return (Prm_SetpointValue3 / ISetpointFactor).ToString & displayUnitsSetpoint_
        Case 4 : Return (Prm_SetpointValue4 / ISetpointFactor).ToString & displayUnitsSetpoint_
        Case Else : Return (Prm_SetpointValue1 / ISetpointFactor).ToString & displayUnitsSetpoint_
      End Select
    End Get
  End Property

  Public ReadOnly Property DisplayStatus As String Implements IController.Status
    Get
      Dim returnText As String = ""
      returnText = "SP" & Plc_SetpointChActive.ToString ' & " - Output: " & (Plc_OutputPercent / 10).ToString & "%"

      Return returnText
    End Get
  End Property

  Friend ReadOnly Property IOutputPct As Integer Implements IController.OutputPercent
    Get
      Return Plc_OutputPercent
    End Get
  End Property

  Private izone_ As Integer = 0
  Private Property IZone As Integer Implements IController.Zone
    Get
      Return izone_
    End Get
    Set(value As Integer)
      izone_ = value
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

End Class
