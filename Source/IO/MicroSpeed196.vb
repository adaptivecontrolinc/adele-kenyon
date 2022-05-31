Namespace Ports
  ' www.electro-sensors.com/documents/microspeed196.pdf
  Public Class MicroSpeed196
    Private ReadOnly stm_ As Stream
    Private tx_ As Data, txb_(13 - 1) As Byte, rxb_(13 - 1) As Byte, rx_ As Data, _
            state_ As State, asyncResult_ As IAsyncResult, _
            result_ As Result, oks_, faults_, hwFaults_ As Integer

    ' Convenient for our callers
    Public Enum Variable
      MaxSpeedRpm = 1
      UserUnitsAtMaxSpeed
      PPR
      MaximumLeadRPM
      LeadPPR
      UserUnityRatio = 23
      NewSetPoint = 49
      SerialStatus
      MasterOrFollowerMode
      ClosedOrOpenLoop
      ForwardOrReverse
      MasterSetPoint1
      MasterSetPoint2
      MasterSetPoint3
      MasterSetPoint4
      FollowerSetPoint1
      FollowerSetPoint2
      FollowerSetPoint3
      FollowerSetPoint4
      Display
      AlarmStatus
    End Enum

    Public Enum EReadWriteState
      Read
      SetDisable
      WaitForDisable
      Write
    End Enum
    Public ReadWriteState As EReadWriteState

    ' Always 8 data, 1 stop, no parity
    Public Sub New(ByVal stm As System.IO.Stream)
      stm.ReadTimeout = 200  ' should allow for greatest possible time to receive data
      stm_ = New Stream(stm)
      txb_(0) = 2 ' stx
      txb_(1) = 0 + 48  ' device type
      txb_(12) = 3 ' etx
    End Sub

    Public Function ReadVariable(ByVal node As Integer, ByVal variable As Variable, ByRef data As Integer, ByRef decimalLocation As Integer) As Result
      Dim proposedTx As New Data(node, Message.ReadVariable, variable, 0, 0)
      If IsIdle Then BeginWriteAndRead(proposedTx)
      RunStateMachine(proposedTx)
      If Not IsIdle Then Return Result.Busy
      If result_ = Result.OK Then data = rx_.Data : decimalLocation = rx_.DecimalLocation
      Return result_
    End Function

    Public Function WriteVariable(ByVal node As Integer, ByVal variable As Variable, ByVal data As Integer, ByVal decimalLocation As Integer) As Result
      Return PerformAction(New Data(node, Message.WriteVariable, variable, data, decimalLocation))
    End Function
    Public Function Start(ByVal node As Integer) As Result
      Return PerformAction(New Data(node, Message.Command, Command.Start, 0, 0))
    End Function
    Public Function [Stop](ByVal node As Integer) As Result
      Return PerformAction(New Data(node, Message.Command, Command.Stop, 0, 0))
    End Function
    Public Function EStop(ByVal node As Integer) As Result
      Return PerformAction(New Data(node, Message.Command, Command.EStop, 0, 0))
    End Function
    Public Function SetToFactoryDefaults(ByVal node As Integer) As Result
      Return PerformAction(New Data(node, Message.Command, Command.SetToFactoryDefaults, 0, 0))
    End Function
    Public Function ChangeSetPoint(ByVal node As Integer, ByVal value As Integer, ByVal decimalLocation As Integer) As Result
      Return PerformAction(New Data(node, Message.Command, Command.ChangeSetPoint, value, decimalLocation))
    End Function
    Public Function LoadMasterSetPoint(ByVal node As Integer, ByVal value As Integer, ByVal decimalLocation As Integer) As Result
      Return PerformAction(New Data(node, Message.Command, Command.LoadMasterSetPoint, value, decimalLocation))
    End Function
    Public Function ExecuteLoadedMasterSetPoint(ByVal node As Integer, ByVal activeSetpoint As Integer) As Result
      Return PerformAction(New Data(node, Message.Command, Command.ExecuteLoadedMasterSetPoint, activeSetpoint, 0))
    End Function
    Public Function LoadFollowerSetPoint(ByVal node As Integer, ByVal value As Integer, ByVal decimalLocation As Integer) As Result
      Return PerformAction(New Data(node, Message.Command, Command.LoadFollowerSetPoint, value, decimalLocation))
    End Function
    Public Function ExecuteLoadedFollowerSetPoint(ByVal node As Integer, ByVal activeSetpoint As Integer) As Result
      Return PerformAction(New Data(node, Message.Command, Command.ExecuteLoadedFollowerSetPoint, 0, 0))
    End Function

    Private Function PerformAction(ByVal proposedTx As Data) As Result
      If IsIdle Then BeginWriteAndRead(proposedTx)
      RunStateMachine(proposedTx)
      If Not IsIdle Then Return Result.Busy
      Return result_
    End Function

    Private Sub BeginWriteAndRead(ByVal tx As Data)
      tx_ = tx : tx.CopyTo(txb_)
      stm_.Flush()
      asyncResult_ = stm_.BeginWrite(txb_, 0, txb_.Length, Nothing, Nothing)
      state_ = State.Tx
    End Sub

    Private Sub RunStateMachine(ByVal proposedTx As Data)
      Select Case state_
        Case State.Tx
          ' Wait for the tx to complete
          If Not asyncResult_.IsCompleted Then Exit Sub
          stm_.EndWrite(asyncResult_)  ' tidy up
          ' Start reading
          asyncResult_ = stm_.BeginRead(rxb_, 0, rxb_.Length, Nothing, Nothing)
          state_ = State.Rx : GoTo stateRx ' go straight on to the next state

        Case State.Rx
stateRx:
          If Not asyncResult_.IsCompleted Then Exit Sub ' it'll be completed soon

          Dim rxCount As Integer = rxb_.Length, red As Integer = stm_.EndRead(asyncResult_) : asyncResult_ = Nothing
          If red = -1 Then
            rx_ = Nothing : SetResult(Result.HwFault)
          ElseIf red <> rxCount Then
            rx_ = Nothing : SetResult(Result.Fault)
          Else
            rx_ = New Data(rxb_) : SetResult(Result.OK)
          End If
          state_ = State.Complete : GoTo stateComplete ' go straight on to the next state

        Case State.Complete
stateComplete:
          ' See if our caller will get the result
          If tx_.Equals(proposedTx) Then state_ = State.Idle ' the end of this job
      End Select
    End Sub


    Friend ReadOnly Property BaseStream() As Stream
      Get
        Return stm_
      End Get
    End Property

    Public ReadOnly Property OKs() As Integer
      Get
        Return oks_
      End Get
    End Property
    Public ReadOnly Property Faults() As Integer
      Get
        Return faults_
      End Get
    End Property
    Public ReadOnly Property HwFaults() As Integer
      Get
        Return hwFaults_
      End Get
    End Property
    Private Sub SetResult(ByVal value As Result)
      result_ = value
      Select Case value
        Case Result.OK : oks_ += 1
        Case Result.Fault : faults_ += 1
        Case Else : hwFaults_ += 1
      End Select
    End Sub

    ' ----------------------------------------------------------------
    Private Class Data
      Implements IEquatable(Of Data)

      Private ReadOnly node_ As Integer, message_ As Message, variable_, data_, decimalLocation_ As Integer

      Public Sub New(ByVal node As Integer, ByVal message As Message, ByVal variable As Integer,
                     ByVal data As Integer, ByVal decimalLocation As Integer)
        node_ = node : message_ = message : variable_ = variable : data_ = data : decimalLocation_ = decimalLocation
      End Sub

      Public Sub New(ByVal rx() As Byte)
        node_ = (rx(2) - 48) * 10 + (rx(3) - 48)
        message_ = CType(rx(4) - 48, Message)
        variable_ = (rx(5) - 48) * 10 + (rx(6) - 48)
        data_ = (rx(7) - 48) * 1000 + (rx(8) - 48) * 100 + (rx(9) - 48) * 10 + (rx(10) - 48)
        decimalLocation_ = rx(11) - 48
      End Sub

      Public Sub CopyTo(ByVal tx() As Byte)
        tx(0) = 2 ' stx
        tx(1) = 0 + 48  ' device type
        tx(2) = CType(node_ \ 10 + 48, Byte)
        tx(3) = CType(node_ Mod 10 + 48, Byte)
        tx(4) = CType(message_ + 48, Byte)
        tx(5) = CType(variable_ \ 10 + 48, Byte)
        tx(6) = CType(variable_ Mod 10 + 48, Byte)
        tx(7) = CType(data_ \ 1000 + 48, Byte)
        tx(8) = CType((data_ \ 100) Mod 10 + 48, Byte)
        tx(9) = CType((data_ \ 10) Mod 10 + 48, Byte)
        tx(10) = CType(data_ Mod 10 + 48, Byte)
        tx(11) = CType(decimalLocation_ + 48, Byte)
        tx(12) = 3 ' etx
      End Sub

      Public Overloads Function Equals(ByVal other As Data) As Boolean Implements IEquatable(Of Data).Equals
        Return node_ = other.node_ AndAlso message_ = other.message_ AndAlso variable_ = other.variable_
      End Function

      Public ReadOnly Property Node() As Integer
        Get
          Return node_
        End Get
      End Property
      Public ReadOnly Property Message() As Message
        Get
          Return message_
        End Get
      End Property
      Public ReadOnly Property Variable() As Integer
        Get
          Return variable_
        End Get
      End Property
      Public ReadOnly Property Data() As Integer
        Get
          Return data_
        End Get
      End Property
      Public ReadOnly Property DecimalLocation() As Integer
        Get
          Return decimalLocation_
        End Get
      End Property
    End Class

    Private Enum State
      Idle
      Tx
      Rx
      Complete
      Disabled
    End Enum

    Private goDisabled_ As Boolean
    Public ReadOnly Property Disabled() As Boolean
      Get
        Return state_ = State.Disabled
      End Get
    End Property
    Public Sub SetDisabled(ByVal value As Boolean)
      If value Then
        If state_ = State.Idle Then state_ = State.Disabled
      '  If state_ = State.Complete Then state_ = State.Disabled ' 2022-03-16
      Else
        If state_ = State.Disabled Then state_ = State.Idle
      End If
      goDisabled_ = value
    End Sub

    Private ReadOnly Property IsIdle() As Boolean
      Get
        If state_ <> State.Idle Then Return False
        If goDisabled_ Then state_ = State.Disabled : Return False
        Return True
      End Get
    End Property

    Public Enum Result
      Busy
      OK
      Fault
      HwFault
    End Enum

    Private Enum Message
      Command
      ReadVariable
      WriteVariable
      [Error]
    End Enum

    Private Enum Command
      Start
      [Stop]
      EStop
      SetToFactoryDefaults
      ChangeSetPoint
      LoadMasterSetPoint
      ExecuteLoadedMasterSetPoint
      LoadFollowerSetPoint
      ExecuteLoadedFollowerSetPoint
    End Enum
  End Class
End Namespace