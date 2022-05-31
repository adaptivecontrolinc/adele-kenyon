Imports Utilities.Sql

Public Class GasUsage : Inherits MarshalByRefObject
  Private ReadOnly parent_ As ACParent
  Private ReadOnly controlCode_ As ControlCode

  Public Sub New(ByVal parent As ACParent, ByVal controlCode As ControlCode)
    parent_ = parent : controlCode_ = controlCode
  End Sub

  Public Sub InitializeNewBatch(ByVal volume As Integer)

    gasUseJob_.InitializeNewStart(volume)

  End Sub

  Public Sub VolumeFromPLC(ByVal volume As Integer)

    'Set Gas Used values
    GasUseJob.VolumeFromPlc = volume
    GasUseDay.VolumeFromPlc = volume

    ' Test to see that we've just past midnight and update the daily GasUsage value in Utilities Table
    If Date.Now.Hour > 12 Then
      isPM_ = True
    Else : isPM_ = False
    End If
    If Not isPM_ Then
      If wasPM_ Then
        UpdateDailyTotal(GasUseDay.TotalVolume)
      End If
    End If
    If Date.Now.Hour > 12 Then
      wasPM_ = True
    Else : wasPM_ = False
    End If

    'Test to see that we've updated the daily GasUsage value since powering up (lastDailyUpdate within last 2 days)
    '   as well as we're reading from the Gas Usage PLC and have a valid Volume total to use as a differential
    If (lastDailyUpdate_ < Date.Now.AddDays(-2)) AndAlso (volume > 0) Then
      InitializeDailyTotal(GasUseDay.TotalVolume)
    End If

  End Sub

  'Declare GasUsedJob as new GasFlowmeter Object
  Private gasUseJob_ As New GasFlowmeter
  Public Property GasUseJob() As GasFlowmeter
    Get
      Return gasUseJob_
    End Get
    Set(ByVal value As GasFlowmeter)
      gasUseJob_ = value
    End Set
  End Property

  'Declare GasUsedDay as new GasFlowmeter Object
  Private gasUseDay_ As New GasFlowmeter
  Public Property GasUseDay() As GasFlowmeter
    Get
      Return gasUseDay_
    End Get
    Set(ByVal value As GasFlowmeter)
      gasUseDay_ = value
    End Set
  End Property

  Private totalVolumePrevJob_ As Integer
  Public ReadOnly Property TotalVolumePrevJob() As Integer
    'Only set at UpdateDyelotGasUsage propery (Called At ProgramStop)
    Get
      Return totalVolumePrevJob_
    End Get
  End Property

  Private totalVolumePrevDay_ As Integer
  Public ReadOnly Property TotalVolumePrevDay() As Integer
    'Only set at UpdateDailyTotal propery (Called if CurrentVolume reading is less than previous volume reading = PLC couter reset)
    Get
      Return totalVolumePrevDay_
    End Get
  End Property

#Region "DecaTherm Calculations"
  Private heatContent_ As Integer
  Public Property HeatContent() As Integer
    Get
      Return heatContent_
    End Get
    Set(ByVal value As Integer)
      heatContent_ = value
      GasUseDay.HeatContent = value
      GasUseJob.HeatContent = value
    End Set
  End Property
#End Region

#Region "Database Subroutines"

  Public Sub UpdateDyelotGasUsage()
    Dim sql As String = Nothing
    Dim dyelot As String = Nothing
    Dim redye As String = Nothing
    Try

      '1st figure out the dyelot and redye values using the parent.job string, splitting at the '@' character, if found
      Dim Batch = Split(parent_.Job)
      If Batch.Count > 1 Then
        'Redye due to finding an ampersand
        dyelot = SqlString(Batch(0))     ' TODO?   SqlConvert.ToSqlString(Batch(0))
        redye = SqlString(Batch(1))      ' TODO?   SqlConvert.ToSqlString(Batch(1))
      Else
        'Not a redye
        dyelot = SqlString(parent_.Job)  ' TODO?   SqlConvert.ToSqlString(parent_.Job)
        redye = SqlString(0)             ' TODO?  SqlConvert.ToSqlString(0)
      End If

      'Update Dyelots: GasUsed field with Current Volume Used
      ' TODO?
      '      sql = "UPDATE [Dyelots] SET GasUsed=" & SqlConvert.ToSqlString(GasUseJob.TotalVolume) &
      '            " WHERE Dyelot=" & dyelot & " AND ReDye=" & redye
      sql = "UPDATE [Dyelots] SET GasUsed=" & SqlString(GasUseJob.TotalVolume) &
            " WHERE Dyelot=" & dyelot & " AND ReDye=" & redye

      parent_.DbExecute(sql)

      totalVolumePrevJob_ = GasUseJob.TotalVolume

    Catch ex As Exception
      parent_.LogEvent(LogEventType.Error, 1, ex.ToString)
    End Try
  End Sub

  Public Sub InitializeDailyTotal(ByVal totalVolume As Integer)
    Try
      'Set Background Values
      totalVolumePrevDay_ = GasUseDay.TotalVolume

      'Set VolumeStartDay to current PLC CFM Value and Reset Counter Wraps Per Day
      GasUseDay.InitializeNewStart(GasUseDay.VolumeFromPlc)

      'Set LastUpdate Value
      lastDailyUpdate_ = Date.Now
    Catch ex As Exception
      parent_.LogEvent(LogEventType.Error, 1, ex.ToString)
    End Try
  End Sub

  Public Sub UpdateDailyTotal(ByVal totalVolume As Integer)
    Dim sql As String = Nothing
    Try

      'Make sure that Date and Time are always entered into database (##/##/####  00:00:00)
      'Because this subroutine runs when switch from pm to am, it's now the next day from when the gas was used...
      Dim date_ As String = (Date.Now.AddDays(-1)).ToShortDateString & " " & Date.Now.ToShortTimeString

      'Update Utilities: DateTime, GasUsed with Daily Total Volume
      'sql = "INSERT INTO [Utilities] " &
      '            " ([Date],[GasUsed]) " &
      '            " VALUES('" & date_ & "'," & SqlConvert.ToSqlString(totalVolume) & ")"
      sql = "INSERT INTO [Utilities] ([Date],[GasUsed]) VALUES('" & date_ & "'," & SqlString(totalVolume) & ")"

      If parent_ IsNot Nothing Then
        parent_.DbExecute(sql)
      End If

      'Set Background Values
      totalVolumePrevDay_ = totalVolume

      'Set VolumeStartDay to current PLC CFM Value and Reset Counter Wraps Per Day
      GasUseDay.InitializeNewStart(GasUseDay.VolumeFromPlc)

      'Set LastUpdate Value
      lastDailyUpdate_ = Date.Now

    Catch ex As Exception
      parent_.LogEvent(LogEventType.Error, 1, ex.ToString)
    End Try
  End Sub

  Private wasPM_ As Boolean
  Private isPM_ As Boolean
  Public ReadOnly Property IsPM() As Boolean
    Get
      Return isPM_
    End Get
  End Property

  Private lastDailyUpdate_ As DateTime
  Public ReadOnly Property LastDailyUpdate() As DateTime
    Get
      Return lastDailyUpdate_
    End Get
  End Property

#End Region

#Region "Split Function"

  ' http://msdn.microsoft.com/en-us/library/b873y76a.aspx
  'List of possible characters: {" "c, ","c, "."c, ":"c, CChar(vbTab)}
  Private Shared ReadOnly comma_ As Char = ","c
  Private Shared ReadOnly ampersand_ As Char = "@"c
  Public Shared Function Split(ByVal value As String) As Collections.ObjectModel.ReadOnlyCollection(Of String)
    ' Assemble into a collection
    Dim coll As New List(Of String)
    If Not String.IsNullOrEmpty(value) Then
      Dim startIndex As Integer, valueLength As Integer = value.Length
      Do While startIndex < valueLength
        Dim find As Integer = startIndex
        Do
          Dim ch As Char = value.Chars(find)
          If ch = ampersand_ Then Exit Do 'Found an Ampersand
          find += 1 : If find = valueLength Then coll.Add(value.Substring(startIndex)) : GoTo retnow
        Loop
        coll.Add(value.Substring(startIndex, find - startIndex))
        startIndex = find + 1
        If startIndex = valueLength Then Exit Do
      Loop
      ' And return it as a simple string array
retnow:
    End If
    Return coll.AsReadOnly
  End Function

#End Region

End Class
