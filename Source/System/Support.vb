Public Module TickCountModule
  Friend TickCount As UInt32
End Module

' Timer - down timer
Public Class Timer : Inherits MarshalByRefObject
  Private interval_, startTickCount_, pauseTickCount_ As UInt32, paused_, finished_ As Boolean

  Friend Property TimeRemainingMs() As Integer
    Get
      If finished_ Then Return 0
      Dim t As UInt32
      If paused_ Then
        t = pauseTickCount_
      Else
        t = TickCount
      End If
      ' Check to see if we have wrapped
      If t >= startTickCount_ Then
        Return Math.Max(CType(interval_ - (t - startTickCount_), Integer), 0)   ' Don't return a negative
      Else
        Return Math.Max(CType(interval_ - (t + (UInt32.MaxValue - startTickCount_)), Integer), 0)  ' Don't return a negative
      End If
    End Get
    Set(ByVal value As Integer)
      interval_ = CType(value, UInt32)
      startTickCount_ = TickCount
      paused_ = False : finished_ = False
    End Set
  End Property

  Public Property TimeRemaining() As Integer
    Get
      Return (TimeRemainingMs + 500) \ 1000
    End Get
    Set(ByVal value As Integer)
      TimeRemainingMs = value * 1000
    End Set
  End Property

  ' TimeRemainingMs and MilliSeconds are the same
  Friend Property Milliseconds As Integer
    Get
      Return TimeRemainingMs
    End Get
    Set(ByVal value As Integer)
      TimeRemainingMs = value
    End Set
  End Property

  ' TimeRemaining and Seconds are the same
  Friend Property Seconds As Integer
    Get
      Return TimeRemaining
    End Get
    Set(ByVal value As Integer)
      TimeRemaining = value
    End Set
  End Property

  Friend Property Minutes() As Integer
    Get
      Return (TimeRemainingMs + 30000) \ 60000
    End Get
    Set(ByVal value As Integer)
      TimeRemainingMs = value * 60000
    End Set
  End Property

  ReadOnly Property Finished() As Boolean
    Get
      ' This could get jammed if tick count wraps
      'If Not finished_ AndAlso Not paused_ AndAlso TickCount - startTickCount_ >= interval_ Then finished_ = True

      If Not finished_ AndAlso Not paused_ AndAlso TimeRemainingMs <= 0 Then finished_ = True
      Return finished_
    End Get
  End Property

  Public Sub Pause()
    'If we're already finshed or paused then ignore
    If finished_ OrElse paused_ Then Exit Sub
    paused_ = True : pauseTickCount_ = TickCount
  End Sub

  Public Sub Restart()
    'If we're not paused then ignore
    If finished_ OrElse Not paused_ Then Exit Sub
    Dim pauseTime As UInt32 = TickCount - pauseTickCount_
    startTickCount_ += pauseTime
    paused_ = False
  End Sub

  Public Sub Cancel()
    TimeRemainingMs = 0
  End Sub

  Public ReadOnly Property Paused() As Boolean
    Get
      Return paused_
    End Get
  End Property

  Public Overrides Function ToString() As String
    With TimeSpan.FromTicks(TimeRemainingMs * TimeSpan.TicksPerMillisecond)
      Select Case .TotalSeconds
        Case Is >= 86400
          Return .Days.ToString("00") & ":" & .Hours.ToString("00") & "h"
        Case 3600 To 86399
          Return .Hours.ToString("00") & ":" & .Minutes.ToString("00") & "m"
        Case 1 To 3599
          Return .Minutes.ToString("00") & ":" & .Seconds.ToString("00") & "s"
        Case Is <= 0
          Return "00:00s"
        Case Else
          Return "00:00s"
      End Select
    End With
  End Function
End Class

' -------------------------------------------------------------------------
' Timer - up timer
Public Class TimerUp : Inherits MarshalByRefObject
  Private startTickCount_, pauseInterval_ As UInt32, started_, paused_ As Boolean

  Public Sub Start()
    started_ = True
    startTickCount_ = TickCount
    paused_ = False
  End Sub

  Public Sub [Stop]()
    started_ = False
  End Sub

  ' TimeRemaining and Seconds are identical
  Public ReadOnly Property TimeElapsed() As Integer
    Get
      Return TimeElapsedMs \ 1000 ' return in seconds
    End Get
  End Property

  Friend ReadOnly Property Seconds As Integer
    Get
      Return TimeElapsed
    End Get
  End Property

  Friend ReadOnly Property TimeElapsedMs() As Integer
    Get
      If Not started_ Then Return 0
      If paused_ Then Return CType(pauseInterval_, Integer)
      Return CType(TickCount - startTickCount_, Integer)
    End Get
  End Property

  Public Sub Pause()
    'If we're already paused then ignore
    If Not paused_ Then paused_ = True : pauseInterval_ = TickCount - startTickCount_
  End Sub

  Public Sub Restart()
    'If we're not paused then ignore
    If Not paused_ Then Exit Sub

    startTickCount_ = TickCount - pauseInterval_
    paused_ = False
  End Sub

  Public Overloads Function ToString(padSpaces As Integer) As String
    If padSpaces > 0 Then
      Dim timerString As String = Me.ToString
      Return timerString.PadLeft(timerString.Length + padSpaces)
    End If
    Return Me.ToString
  End Function

  Public Overrides Function ToString() As String
    With TimeSpan.FromTicks(TimeElapsedMs * TimeSpan.TicksPerMillisecond)
      Select Case .TotalSeconds
        Case Is >= 86400
          Return .Days.ToString("00") & ":" & .Hours.ToString("00") & "h"
        Case 3600 To 86399
          Return .Hours.ToString("00") & ":" & .Minutes.ToString("00") & "m"
        Case 1 To 3599
          Return .Minutes.ToString("00") & ":" & .Seconds.ToString("00") & "s"
        Case Is <= 0
          Return "00:00s"
        Case Else
          Return "00:00s"
      End Select
    End With
  End Function
End Class

' -------------------------------------------------------------------------
#If 0 Then
Public Class Flasher
  Private startTime_ As UInt32

  Public Sub Flash(ByRef variable As Boolean, ByVal onMilliSeconds As Integer)
    Flash(variable, onMilliSeconds, onMilliSeconds)
  End Sub

  Public Sub Flash(ByRef variable As Boolean, ByVal onMilliSeconds As Integer, ByVal offMilliSeconds As Integer)
    ' Initialise StartTime
    Dim elapsed As UInt32
    If startTime_ = 0 Then
      startTime_ = TickCount
    Else
      elapsed = TickCount - startTime_
    End If

    ' Generate a number that ranges from 0 to the sum
    Dim x As UInt32 = elapsed Mod CType(onMilliSeconds + offMilliSeconds, UInt32)
    variable = (x < onMilliSeconds)
  End Sub
End Class
#End If

' -------------------------------------------------------------------------

' -------------------------------------------------------------------------
' Updated [2011-01-06 DH]
Public Class Smoothing

  ' Use a circular buffer of values
  Private values_() As Integer, count_, firstOfs_ As Integer, sum_ As Long
  Private returnValue_ As Integer

  Public Function Smooth(ByVal value As Integer, ByVal smoothing As Integer) As Integer
    If smoothing < 2 Then Return value ' no need to smooth

    ' Test that values_ has been created (smoothrate was 0) or the smoothrate has changed
    If values_ Is Nothing OrElse values_.Length <> smoothing Then
      ' Create a new values_ array based on the new smooth rate defined
      values_ = New Integer(smoothing - 1) {}
      ' Reset/Initialize variables
      count_ = 0
      firstOfs_ = 0
      sum_ = 0        ' This was not here before [2011-01-06 DH]
    End If

    ' Keep a correct sum at all times for performance
    sum_ += value

    ' Check to see that we're below the defined smooth array count
    If count_ < smoothing Then
      ' We are so update the current array value based on current count
      values_(count_) = value : count_ += 1
    Else
      ' We're greater than the defined smooth array, 
      '   subtract the old array value for the current count (fistOfs_ = count_)
      sum_ -= values_(firstOfs_)
      ' Update the current array with the current value at this address (firstOfs_)
      values_(firstOfs_) = value
      ' Increment the address for the next scan
      firstOfs_ += 1
      ' Test that upcoming address is below the defined smooth array count
      If firstOfs_ >= count_ Then
        firstOfs_ = 0
      End If
    End If

    returnValue_ = CType(sum_ \ count_, Integer)
    Return returnValue_
  End Function

  Public Function Smooth(ByVal value As Short, ByVal smoothing As Integer) As Short
    Return CType(Smooth(CType(value, Integer), smoothing), Short)
  End Function
End Class


' -------------------------------------------------------------------------
''' <summary>A class that raises an event if there has not been mouse or keyboard activity for this application for a while.</summary>
Public Class InactiveTimeout : Implements System.Windows.Forms.IMessageFilter, IDisposable
  Public Event Timeout As EventHandler
  Private reLoad_, remaining_ As Integer
  Private timer_ As New Threading.Timer(AddressOf OnTimer, Nothing, 1000, 1000)

  Public Sub New()
    System.Windows.Forms.Application.AddMessageFilter(Me)
  End Sub

  Public Sub Dispose() Implements IDisposable.Dispose
    If timer_ IsNot Nothing Then
      timer_.Dispose() : timer_ = Nothing
      System.Windows.Forms.Application.RemoveMessageFilter(Me)
    End If
  End Sub

  Public Sub SetTimeout(ByVal seconds As Integer)
    reLoad_ = seconds : remaining_ = seconds
  End Sub

  Private Sub OnTimer(ByVal state As Object)
    Static inside_ As Boolean : If inside_ Then Exit Sub
    inside_ = True
    If remaining_ > 0 Then
      remaining_ -= 1
      If remaining_ = 0 Then RaiseEvent Timeout(Me, EventArgs.Empty)
    End If
    inside_ = False
  End Sub

  Private Function PreFilterMessage(ByRef m As System.Windows.Forms.Message) As Boolean Implements System.Windows.Forms.IMessageFilter.PreFilterMessage
    Select Case m.Msg
      Case &H201 To &H20D, &HA0 To &HAD, &H100 To &H109  ' all mouse and keyboard messages (except WM_MOUSEMOVE)
        If remaining_ > 0 Then remaining_ = reLoad_
    End Select
    Return False
  End Function
End Class

' ------------------------------------------------------------------------------
' Support subs and functions
Module Support

  ''' <summary>Return default value if value is empty or null.</summary>
  ''' <param name="value">The value to check.</param>
  ''' <param name="defaultValue">The value to return if value is empty or null.</param>
  Public Function DefaultSetting(value As String, defaultValue As String) As String
    If String.IsNullOrEmpty(value) Then Return defaultValue
    Return value
  End Function

  Public Function MulDiv(ByVal value As Integer, ByVal multiply As Integer, ByVal divide As Integer) As Integer
    If divide = 0 Then Return 0 ' no divide by zero error please
    Return CType((CType(value, Long) * multiply) \ divide, Integer)
  End Function

  Public Function MulDiv(ByVal value As Short, ByVal multiply As Integer, ByVal divide As Integer) As Short
    If divide = 0 Then Return 0 ' no divide by zero error please
    Return CType((CType(value, Long) * multiply) \ divide, Short)
  End Function

  ''' <summary>Returns a rescaled value.</summary>
  ''' <param name="value"></param>
  ''' <param name="inMin"></param>
  ''' <param name="inMax"></param>
  ''' <param name="outMin"></param>
  ''' <param name="outMax"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function ReScale(ByVal value As Integer, ByVal inMin As Integer, ByVal inMax As Integer, ByVal outMin As Integer, ByVal outMax As Integer) As Integer
    If inMin = inMax Then Return 0 ' avoid division by zero
    If value < inMin Then value = inMin
    If value > inMax Then value = inMax
    Return MulDiv(value - inMin, outMax - outMin, inMax - inMin) + outMin
  End Function

  Public Function ReScale(ByVal value As Short, ByVal inMin As Integer, ByVal inMax As Integer, ByVal outMin As Integer, ByVal outMax As Integer) As Short
    Return CType(ReScale(CType(value, Integer), inMin, inMax, outMin, outMax), Short)
  End Function

  Public Function Reverse(ByVal value As Short) As Short
    Return CType(-1 * (value - 1000), Short)
  End Function

  ''' <summary>Returns the given value constrained to lie beneath the min and the max.</summary>
  ''' <param name="value">The value to check.</param>
  ''' <param name="min">The least value that can be returned.</param>
  ''' <param name="max">The greatest value that can be returned.</param>
  Public Function MinMax(ByVal value As Integer, ByVal min As Integer, ByVal max As Integer) As Integer
    If value < min Then Return min
    If value > max Then Return max
    Return value
  End Function


  ''' <summary>Returns the given value constrained to lie beneath the min and the max.</summary>
  ''' <param name="value">The value to check.</param>
  ''' <param name="min">The least value that can be returned.</param>
  ''' <param name="max">The greatest value that can be returned.</param>
  Public Function MinMax(ByVal value As Double, ByVal min As Double, ByVal max As Double) As Double
    If value < min Then Return min
    If value > max Then Return max
    Return value
  End Function


  Public Function DoublePrecision(value As Double, decimalPlaces As Integer) As Double
    Dim formatString As String
    If decimalPlaces <= 0 Then
      formatString = "#0"
    Else
      formatString = "#0.00000000".Substring(0, decimalPlaces + 3)
    End If
    Return Double.Parse(value.ToString(formatString))
  End Function

  Public Function DisplayAmount(amount As Double, units As String) As String
    Return amount.ToString & " " & units
  End Function

  Public Function DisplayAmount(amount As Double, units As String, decimalPlaces As Integer) As String
    Dim formatString As String
    If decimalPlaces <= 0 Then
      formatString = "#0"
    Else
      formatString = "#0.00000000".Substring(0, decimalPlaces + 3) ' no more than 8 decimal places
    End If
    Return amount.ToString(formatString) & " " & units
  End Function

  Public Function SameJob(dyelot1 As String, reDye1 As Integer, dyelot2 As String, reDye2 As Integer) As Boolean
    Return (dyelot1 = dyelot2) AndAlso (reDye1 = reDye2)
  End Function


  Public Function TimerString(ByVal secs As Integer) As String
    Dim hours As Integer = secs \ 3600, minutes As Integer = (secs Mod 3600) \ 60, 
        seconds As Integer = (secs Mod 60)

    ' Hours and minutes
    If hours > 0 Then Return hours.ToString("00", Globalization.CultureInfo.InvariantCulture) & ":" _
                             & minutes.ToString("00", Globalization.CultureInfo.InvariantCulture) & "m"
    ' Minutes and seconds
    Return minutes.ToString("00", Globalization.CultureInfo.InvariantCulture) & ":" _
             & seconds.ToString("00", Globalization.CultureInfo.InvariantCulture) & "s"
  End Function

  ''' <summary>Gets the percent (actually in tenths of a percent, so a number between 0 and 1000),
  ''' that the given value represents between the given minimum and maximum bounds.</summary>
  Public Function GetPercent(ByVal value As Integer, ByVal minValue As Integer, ByVal maxValue As Integer) As Integer
    Dim range = maxValue - minValue
    If range = 0 Then Return -1 ' avoid a division by zero error, and return a special value
    Dim ret = ((value - minValue) * 1000) \ range
    Return Math.Min(Math.Max(ret, 0), 1000)
  End Function

  Public Function WordWrap(ByVal strText As String, ByVal iWidth As Integer) As String

    If strText.Length <= iWidth Then Return strText ' dont need to do anything 

    Dim sResult As String = strText
    Dim sChar As String             ' temp holder for current string char 
    Dim iEn As Integer
    Dim iLineNO As Integer = iWidth
    Do While sResult.Length >= iLineNO
      For iEn = iLineNO To 1 Step -1         ' work backwards from the max len to 1 looking for a space 
        sChar = sResult.Chars(iEn)
        If sChar = " " Then             ' found a space 
          sResult = sResult.Remove(iEn, 1)     ' Remove the space 
          sResult = sResult.Insert(iEn, Environment.NewLine)     ' insert a line feed here, 
          iLineNO += iWidth             ' increment 
          Exit For
        End If
      Next
    Loop
    Return sResult
  End Function

  Public Function WordWrap(ByVal strText As String, ByVal iWidth As Integer, ByVal strSpecifier As String) As String

    If strText.Length <= iWidth Then Return strText ' dont need to do anything 

    Dim sResult As String = strText
    Dim sChar As String             ' temp holder for current string char 
    Dim iEn As Integer
    Dim iLineNO As Integer = iWidth
    Do While sResult.Length >= iLineNO
      For iEn = iLineNO To 1 Step -1         ' work backwards from the max len to 1 looking for a space or specifier (,) 
        sChar = sResult.Chars(iEn)
        If (sChar = " ") OrElse (sChar = strSpecifier) Then      ' found a space or specifier
          sResult = sResult.Remove(iEn, 1)                       ' Remove the space or specifier
          sResult = sResult.Insert(iEn, Environment.NewLine)     ' insert a line feed here, 
          iLineNO += iWidth                                      ' increment 
          Exit For

        End If
      Next
    Loop
    Return sResult
  End Function

  Public Function FindGreater(ByVal value1 As Integer, ByVal value2 As Integer) As Integer
    If value1 > value2 Then
      Return value1
    Else
      Return value2
    End If
  End Function

  Public Function FindLesser(ByVal value1 As Integer, ByVal value2 As Integer) As Integer
    If value1 < value2 Then
      Return value1
    Else
      Return value2
    End If
  End Function

  Public Function Summer(ByVal value1 As Integer, ByVal value2 As Integer) As Integer
    Return value1 + value2
  End Function

  Public Function GetDispenseInformation(ByVal dispenseinformation As String, ByVal calloff As Integer) As String
    Try
      'These separators are used by programs and prefixed steps
      Dim LineFeed As String = Convert.ToChar(13) & Convert.ToChar(10)
      Dim separator1 As String = Convert.ToChar(255)
      Dim separator2 As String = ","

      'strings
      Dim steps() As String
      Dim StepDetailSplit() As String
      Dim stepnumbersplit() As String
      Dim NumberOfPrepsFound As Integer = 0
      Dim stepnumber As String = ""
      Dim scaleInfo As String = ""
      'Split the program string into an array of program steps - one step per array element
      steps = dispenseinformation.Split(LineFeed.ToCharArray, StringSplitOptions.RemoveEmptyEntries)
      'Make sure we've have something to check
      If steps.GetUpperBound(0) <= 0 Then Return ""

      For i = 1 To steps.GetUpperBound(0)
        StepDetailSplit = steps(i).Split(CChar(separator2))
        If StepDetailSplit(1) Like "Fnc=Prep*" Then
          NumberOfPrepsFound += 1
          If NumberOfPrepsFound = calloff Then
            stepnumbersplit = StepDetailSplit(0).Split(CChar("="))
            stepnumber = stepnumbersplit(1)
            scaleInfo = StepDetailSplit(3).Substring(StepDetailSplit(3).Length - 5, 5)
            Return stepnumber & "," & scaleInfo
          End If
        End If
      Next

      Return ""
    Catch ex As Exception
      Return ""
    End Try

  End Function

  Public Function BcdToDecimal(valueBCD As Integer) As Integer
    Dim ret As Integer
    Dim mult = 1

    Do While valueBCD <> 0
      ret = ret + (valueBCD Mod 16) * mult
      mult = mult * 10
      valueBCD = valueBCD \ 16
    Loop
    Return ret
  End Function


End Module