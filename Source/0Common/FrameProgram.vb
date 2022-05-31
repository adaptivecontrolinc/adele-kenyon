' Version [2022-03-08]
' ADELE KNITS KENYON 2


Public Class FrameProgram

  Private lineFeed As String = Convert.ToChar(13) & Convert.ToChar(10)

  Public Group As String
  Public Number As Integer
  Public Name As String
  Public Notes As String

  Public AirTemp(8) As Integer
  Public AirTempSet(8) As Boolean
  Public Width(5) As Integer
  Public WidthSet(5) As Boolean
  Public TenterChain As Integer
  Public TenterChainSet As Boolean
  Public TenterChainLeft As Integer
  Public TenterChainLeftSet as boolean
  Public TenterChainRight As Integer
  Public TenterChainRightSet as boolean

  Public OverfeedTop As Integer
  Public OverfeedTopSet as boolean
  Public OverfeedBottom As Integer
  Public OverfeedBottomSet As Boolean
  Public SelvageLeft As Integer
  Public SelvageLeftSet As Boolean
  Public SelvageRight As Integer
  Public SelvageRightSet As Boolean
  Public Padder(2) As Integer
  Public PadderSet(2) as boolean
  '  Public PadPress As Integer
  '  Public Vacuum As Integer
  Public Conveyor As Integer
  Public ConveyorSet as boolean
  Public Stripper As Integer
  Public StripperSet As Boolean

  Public FanSpeedTop(8) As Integer
  Public FanSpeedTopSet(8) As Boolean
  Public FanSpeedBottom(8) As Integer
  Public FanSpeedBottomSet(8) As Boolean
  Public FanSpeedExhaust(2) As Integer
  Public FanSpeedExhaustSet(2) As Boolean

  Public LockSetpoints As Integer
  Public PlevaTemp As Integer
  Public PlevaTime As Integer
  Public PlevaHumidity As Integer

  Public ProgramSteps As New List(Of FrameProgramStep)

  Public Sub Load(ByVal program As System.Data.DataRow)
    Try
      'make sure we have data
      If program Is Nothing Then
        Exit Sub
      End If

      'Fill in program data 
      Group = program("ProgramGroup").ToString
      Number = Integer.Parse(program("ProgramNumber").ToString)
      Name = program("Name").ToString
      If Not program.IsNull("Notes") Then Notes = program("Notes").ToString

      'Make sure we have steps
      If program.IsNull("Steps") Then Exit Sub

      'Split the step string on line feed
      Dim steps() As String = program("Steps").ToString.Split(lineFeed.ToCharArray)

      'Loop through the array, create a program step for each step and add it to the collection
      For i As Integer = 0 To steps.GetUpperBound(0)
        Dim newProgramStep As New FrameProgramStep
        newProgramStep.Load(steps(i))
        ProgramSteps.Add(newProgramStep)
      Next i

      'Fill in AirTemps
      SetAirTemps()

      'Fill in widths
      SetWidths()

      'Fill In Tenter Chain
      SetTenterChain()

      'Fill In Overfeed
      SetOverfeed()

      'Fill In Selvage Left & Right
      SetSelvage()

      'Fill in Padder Roll
      SetPadder()

      ' Unused: 
      '  SetPadPress()
      '  SetVacuum()
      '  SetConveyor()

      ' Stripper Roll
      SetStripper()

      SetFanSpeeds()


      'Fill in Locked Setpoints
      SetLockSetpoints()

      'Fill in Pleva Temperature & Time
      '  SetPlevaTemp()

      'Fill in Pleva Humidity
      '  SetPlevaHumidity()

      ' Fill Stripper commands
      SetStripper()
      '  SetFoler()

    Catch ex As Exception
      'Log any errors ?
    End Try
  End Sub

  Private Sub SetAirTemps()
    For Each programStep As FrameProgramStep In ProgramSteps
      If programStep.Code.ToUpper = "AT" Then
        ' AT,Zone,Setpoint,Deviance
        Dim zone As Integer = CType(programStep.Parameters(0), Integer)
        Dim value As Integer = CType(programStep.Parameters(1), Integer)
        If zone = 0 Then
          For i As Integer = 0 To AirTemp.GetUpperBound(0)
            AirTemp(i) = value
            AirTempSet(i) = True
          Next i
        Else
          AirTemp(zone) = value
          AirTempSet(zone) = True
        End If
      End If
    Next
  End Sub

  Private Sub SetWidths()
    For Each programStep As FrameProgramStep In ProgramSteps
      If programStep.Code.ToUpper = "WS" Then
        'WS,Zone,Setpoint
        Dim zone As Integer = CType(programStep.Parameters(0), Integer)
        Dim value As Integer = CType(programStep.Parameters(1), Integer)
        If zone = 0 Then
          For i As Integer = 0 To Width.GetUpperBound(0)
            Width(i) = value
            WidthSet(i) = True
          Next i
        Else
          Width(zone) = value
          WidthSet(zone) = True
        End If

      End If
    Next
  End Sub

  Private Sub SetTenterChain()
    For Each programStep As FrameProgramStep In ProgramSteps
      If programStep.Code.ToUpper = "TC" Then
        'TC, Setpoint, Deviance
        Dim value As Integer = CType(programStep.Parameters(0), Integer)
        TenterChain = value
        TenterChainSet = True
      End If

      If programStep.Code.ToUpper = "TLR" Then
        'TLR, Left: |0-999|%  Right: |0-999|%  Deviance: |0-999|%
        TenterChainLeft = CType(programStep.Parameters(0), Integer)
        TenterChainRight = CType(programStep.Parameters(1), Integer)
        TenterChainLeftSet = True
        TenterChainRightSet = True
      End If
    Next
  End Sub

  Private Sub SetOverfeed()
    For Each programStep As FrameProgramStep In ProgramSteps
      If programStep.Code.ToUpper = "OV" Then
        Dim valueTop As Integer = CType(programStep.Parameters(0), Integer)
        Dim valueBottom As Integer = CType(programStep.Parameters(1), Integer)

        OverfeedTop = valueTop
        OverfeedTopSet = True
        OverfeedBottom = valueBottom
        OverfeedBottomSet = True

      End If
    Next
  End Sub

  Private Sub SetSelvage()
    For Each programStep As FrameProgramStep In ProgramSteps
      If programStep.Code.ToUpper = "SV" Then
        SelvageLeft = CType(programStep.Parameters(0), Integer)
        SelvageRight = CType(programStep.Parameters(1), Integer)
        SelvageLeftSet = True
        SelvageRightSet = True
      End If
    Next
  End Sub

  Private Sub SetPadder()
    For Each programStep As FrameProgramStep In ProgramSteps
      If programStep.Code.ToUpper = "PR" Then
        ' PR, Zone: |0-2| Setpoint: |35-175|% Deviance: |0-50|%
        Dim zone As Integer = CType(programStep.Parameters(0), Integer)
        Dim value As Integer = CType(programStep.Parameters(1), Integer)
        If zone = 0 Then
          For i As Integer = 0 To Padder.GetUpperBound(0)
            Padder(i) = value
            PadderSet(i) = True
          Next i
        Else
          Padder(zone) = value
          PadderSet(zone) = True
        End If

      End If
    Next
  End Sub

  'Private Sub SetVacuum()
  '  For Each programStep As FrameProgramStep In ProgramSteps
  '    If programStep.Code.ToUpper = "VA" Then
  '      Vacuum = CType(programStep.Parameters(0), Integer)
  '    End If
  '  Next
  'End Sub

  'Private Sub SetPadPress()
  '  For Each programStep As FrameProgramStep In ProgramSteps
  '    If programStep.Code.ToUpper = "DP" Then
  '      PadPress = CType(programStep.Parameters(0), Integer)
  '    End If
  '  Next
  'End Sub

  Private Sub SetConveyor()
    For Each programStep As FrameProgramStep In ProgramSteps
      If programStep.Code.ToUpper = "CV" Then
        Conveyor = CType(programStep.Parameters(0), Integer)
        ConveyorSet = True
      End If
    Next
  End Sub

  Private Sub SetStripper()
    For Each programStep As FrameProgramStep In ProgramSteps
      If programStep.Code.ToUpper = "ST" Then
        Stripper = CType(programStep.Parameters(0), Integer)
        StripperSet = True
      End If
    Next
  End Sub

  Private Sub SetLockSetpoints()
    For Each programStep As FrameProgramStep In ProgramSteps
      If programStep.Code.ToUpper = "LS" Then                                   'LS, All:|0-1|, Width:|0-1|
        LockSetpoints = CType(programStep.Parameters(0), Integer)
      End If
    Next
  End Sub

  Private Sub SetPlevaTemp()
    For Each programStep As FrameProgramStep In ProgramSteps
      If programStep.Code.ToUpper = "PT" Then                                   'PT, Temp:|0-450|F Time:|0-30|sec
        PlevaTemp = CType(programStep.Parameters(0), Integer)
        PlevaTime = CType(programStep.Parameters(1), Integer)
      End If
    Next
  End Sub

  Private Sub SetPlevaHumidity()
    For Each programStep As FrameProgramStep In ProgramSteps
      If programStep.Code.ToUpper = "PH" Then                                   'PH, Setpoint: |0-999|%
        PlevaHumidity = CType(programStep.Parameters(0), Integer)
      End If
    Next
  End Sub

#If 0 Then
  Private Sub SetFolder()
    For Each programStep As FrameProgramStep In ProgramSteps
      If programStep.Code.ToUpper = "FL" Then                                   'FL, Setpoint: |0-999|% Deviance: |0-999|%
        Folder = CType(programStep.Parameters(0), Integer)
        FolderSet = True
      End If
    Next
  End Sub
#End If


  Private Sub SetFanSpeeds()
    For Each programStep As FrameProgramStep In ProgramSteps
      If programStep.Code.ToUpper = "FS" Then
        'FS: Command("Fan Speed", "Zone: |0-8| Top: |0-100|%  Bottom: |0-100|%")
        Dim zone As Integer = CType(programStep.Parameters(0), Integer)
        Dim valueTop As Integer = CType(programStep.Parameters(1), Integer)
        Dim valueBottom As Integer = CType(programStep.Parameters(2), Integer)
        If zone = 0 Then
          For i As Integer = 0 To Width.GetUpperBound(0)
            FanSpeedTop(i) = valueTop
            FanSpeedTopSet(i) = True
            FanSpeedBottom(i) = valueBottom
            FanSpeedBottomSet(i) = True
          Next i
        Else
          FanSpeedTop(zone) = valueTop
          FanSpeedTopSet(zone) = True
          FanSpeedBottom(zone) = valueBottom
          FanSpeedBottomSet(zone) = True
        End If

      End If
    Next
  End Sub

End Class

Public Class FrameProgramStep

  Public Code As String
  Public Parameters(8) As String

  Public Sub Load(ByVal stepString As String)
    'Make sure we have data
    If stepString Is Nothing Then Exit Sub

    'Split on notes first
    Dim firstSplit() As String
    firstSplit = stepString.Split("!".ToCharArray)

    'Shouldn't happen but you never know
    If firstSplit Is Nothing Then Exit Sub

    'Split on parameters second
    Dim secondSplit() As String
    secondSplit = firstSplit(0).Split(",".ToCharArray)

    'Shouldn't happen but you never know
    If secondSplit Is Nothing Then Exit Sub

    'Set the step code
    Code = secondSplit(0)

    'Loop through parameters and store values
    For i As Integer = 0 To secondSplit.GetUpperBound(0)
      'Make sure we don't go beyond parameter array size
      '  string array is one off here because element 0 contains the command code
      If i > 0 And i <= Parameters.GetUpperBound(0) + 1 Then
        Parameters(i - 1) = secondSplit(i)
      End If
    Next

  End Sub

End Class