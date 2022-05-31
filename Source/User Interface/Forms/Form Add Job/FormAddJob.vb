Imports Utilities.Sql

' Version [2022-03-08 DH] - 
'   - Using MW&W Form Add Job Version 1.0.7 [2017-02-24 DH] - 
'   - Add function to signal operator if attempting to release 'Locked Setpoints' if parameter is not enabled
'   - Add parameter function to display/hide commands not defined in Recipe.  MW&W may use programs that only adjust Air Temp or Width, Etc.


Public Class FormAddJob
  Private dtPrograms As System.Data.DataTable
  Private programNext_ As New FrameProgram

  Private controlCode_ As ControlCode
  Public Property ControlCode() As ControlCode
    Get
      Return controlCode_
    End Get
    Set(ByVal value As ControlCode)
      controlCode_ = value
    End Set
  End Property

  Public Sub New()
    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    InitializeForm()
  End Sub

  Public Sub New(ByVal controlcode As ControlCode)
    InitializeComponent()
    controlCode_ = controlcode
    InitializeForm()
  End Sub

  Private Sub InitializeForm()
    FillDataTablePrograms()
    FillListBoxPrograms()
    FillListBoxReworks()
    SetupListViewComparison()

    ' Set Lock Setpoints status if not enabled with Parameter
    If ControlCode.Parameters.ProgramSetpointEnable = 0 Then
      checkBoxLock.CheckState = CheckState.Checked
    End If
    If ControlCode.Parameters.ProgramSetpointWidthEnable = 0 Then
      checkBoxLockWidth.CheckState = CheckState.Checked
    End If

    'Reset Loaded Flag
    alreadyLoaded_ = False

    'Set initial focus to program number box for keyboard efficiency
    TextBoxProgramNumber.Focus()

    'Set timer interval and enable timer
    TimerMain.Interval = 1000
    TimerMain.Enabled = True
  End Sub

#Region "List View Configuration"

  Private Sub FillDataTablePrograms()
    Dim sql As String = Nothing
    Try
      sql = "SELECT * FROM Programs ORDER BY ProgramNumber"
      dtPrograms = controlCode_.Parent.DbGetDataTable(sql)

    Catch ex As Exception
    End Try
  End Sub

  Private Sub FillListBoxPrograms()
    Dim sql As String = Nothing
    Dim dt As System.Data.DataTable = Nothing
    Try
      sql = "SELECT * FROM Programs WHERE ProgramNumber <= 99999 ORDER BY ProgramNumber"
      dt = controlCode_.Parent.DbGetDataTable(sql)
      With listBoxPrograms
        .Items.Clear()

        Dim newItem As String
        For Each dr As System.Data.DataRow In dt.Rows
          newItem = dr("ProgramNumber").ToString & "  " & dr("Name").ToString
          .Items.Add(newItem)
        Next
      End With
    Catch ex As Exception
    End Try
  End Sub

  Private Sub FillListBoxReworks()
    Dim sql As String = Nothing
    Dim dt As System.Data.DataTable = Nothing
    Try
      sql = "SELECT * FROM Programs WHERE ProgramNumber > 99999 ORDER BY ProgramNumber"
      dt = controlCode_.Parent.DbGetDataTable(sql)
      With ListBoxRework
        .Items.Clear()

        Dim newItem As String
        For Each dr As System.Data.DataRow In dt.Rows
          newItem = dr("ProgramNumber").ToString & "  " & dr("Name").ToString
          .Items.Add(newItem)
        Next
      End With
    Catch ex As Exception
    End Try
  End Sub

  Private Sub SetupListViewComparison()
    With ListViewComparison
      .View = View.Details
      .BorderStyle = Windows.Forms.BorderStyle.None
      .GridLines = True
      '.FullRowSelect = True
      .HeaderStyle = ColumnHeaderStyle.Nonclickable
      .MultiSelect = False

      .Columns.Clear()
      .Columns.Add("Command", 100)
      .Columns.Add("Current Job", 125, HorizontalAlignment.Center)
      .Columns.Add("Next Job", 125, HorizontalAlignment.Center)
      .Columns.Add("Change", 100, HorizontalAlignment.Center)
    End With
  End Sub

  Private Sub FillListViewComparison()
    Try
      ListViewComparison.Items.Clear()

      If ControlCode.Parameters.JobDisplayAllCommands >= 1 Then
        ' Display all defined commands available, regardless of them being defined within the procedure

        ' Fill Air Temp Values
        For i As Integer = 1 To ControlCode.AirTemp_Zone.Length - 1
          AddNewItem("Air Temp " & i.ToString, controlCode_.AirTemp_Zone(i).SetpointDesired \ 10, programNext_.AirTemp(i), "F")
        Next i

        ' Fill Width Values
        For i As Integer = 1 To controlCode_.Width_Screw.Length - 1
          AddNewItem("Width Screw " & i.ToString, controlCode_.Width_Screw(i).SetpointDesired \ 10, programNext_.Width(i), "in")
        Next i

        ' Fill Transport Values (TenterChain/Overfeed/Selvage Left & Right/Padder
        AddNewItem("Tenter Chain", controlCode_.Tenter.SetpointDesired \ 10, programNext_.TenterChain, "YPM")

        AddNewItem("Selvage Left", controlCode_.SelvageLeft.SetpointDesired \ 10, programNext_.SelvageLeft, "%")
        AddNewItem("Selvage Right", controlCode_.SelvageRight.SetpointDesired \ 10, programNext_.SelvageRight, "%")

        AddNewItem("Padder 1", controlCode_.Padder(1).SetpointDesired \ 10, programNext_.Padder(1), "%")
        AddNewItem("Padder 2", controlCode_.Padder(2).SetpointDesired \ 10, programNext_.Padder(2), "%")
        ' TODO   AddNewItem("Pad Pressure", controlCode_.PadDancerPress.SetpointDesired \ 10, programNext_.PadPress, "psi")

        AddNewItem("Overfeed", controlCode_.OverfeedTop.SetpointDesired \ 10, programNext_.OverfeedTop, "%")


      Else
        ' Display only the commands defined within the procedure

        ' Fill Air Temp Values
        For i As Integer = 1 To ControlCode.AirTemp_Zone.Length - 1
          AddNewItem("Air Temp " & i.ToString, controlCode_.AirTemp_Zone(i).SetpointDesired \ 10, programNext_.AirTemp(i), "F", programNext_.AirTempSet(i))
        Next i

        ' Fill Width Values
        For i As Integer = 1 To ControlCode.Width_Screw.Length - 1
          AddNewItem("Width Screw " & i.ToString, controlCode_.Width_Screw(i).SetpointDesired \ 10, programNext_.Width(i), "in", programNext_.WidthSet(i))
        Next i

        ' Fill Transport Values (TenterChain/Overfeed/Selvage Left & Right/Padder
        AddNewItem("Tenter Chain", controlCode_.Tenter.SetpointDesired \ 10, programNext_.TenterChain, "YPM", programNext_.TenterChainSet)
        AddNewItem("Overfeed Top", controlCode_.OverfeedTop.SetpointDesired \ 10, programNext_.OverfeedTop, "%", programNext_.OverfeedTopSet)
        AddNewItem("Overfeed Bottom", controlCode_.OverfeedBot.SetpointDesired \ 10, programNext_.OverfeedBottom, "%", programNext_.OverfeedBottomSet)
        AddNewItem("Padder 1", controlCode_.Padder(1).SetpointDesired \ 10, programNext_.Padder(1), "%", programNext_.PadderSet(1))
        AddNewItem("Padder 2", controlCode_.Padder(2).SetpointDesired \ 10, programNext_.Padder(2), "%", programNext_.PadderSet(2))
        '        AddNewItem("Stripper", controlCode_.Stripper.SetpointDesired \ 10, programNext_.Stripper, "%", programNext_.StripperSet)
        '        AddNewItem("Folder", controlCode_.Folder.SetpointDesired \ 10, programNext_.Folder, "%", programNext_.FolderSet)

        ' Fan Speeds Top
        For i As Integer = 1 To ControlCode.FanTop_Speed.Length - 1
          AddNewItem("Fan Top " & i.ToString, controlCode_.FanTop_Speed(i).SetpointDesired \ 10, programNext_.FanSpeedTop(i), "%", programNext_.FanSpeedTopSet(i))
        Next i
        ' Fan Speeds Bottom
        For i As Integer = 1 To ControlCode.FanBottom_Speed.Length - 1
          AddNewItem("Fan Bottom " & i.ToString, controlCode_.FanBottom_Speed(i).SetpointDesired \ 10, programNext_.FanSpeedBottom(i), "%", programNext_.FanSpeedBottomSet(i))
        Next i
        ' Fan Exhaust Speeds TODO


      End If

    Catch ex As Exception
      Utilities.Log.LogError(ex)
    End Try
  End Sub

  Private Sub AddNewItem(ByVal command As String, ByVal setpointcurrent As Integer, ByVal setpointnew As Integer, ByVal units As String)
    Dim newListViewItem As New ListViewItem(command)
    newListViewItem.SubItems.Add(setpointcurrent.ToString & " " & units)
    newListViewItem.SubItems.Add(setpointnew.ToString & " " & units)

    Dim deviation As Integer = setpointnew - setpointcurrent
    newListViewItem.SubItems.Add(deviation.ToString & " " & units)
    If Math.Abs(deviation) > 50 Then newListViewItem.BackColor = Color.Orange

    ListViewComparison.Items.Add(newListViewItem)
  End Sub

  Private Sub AddNewItem(ByVal command As String, ByVal setpointcurrent As Integer, ByVal setpointnew As Integer, ByVal units As String, ByVal IsActive As Boolean)
    Dim newListViewItem As New ListViewItem(command)
    newListViewItem.SubItems.Add(setpointcurrent.ToString & " " & units)
    newListViewItem.SubItems.Add(setpointnew.ToString & " " & units)

    ' Do Not display this line if the command is not active
    If IsActive Then
      Dim deviation As Integer = setpointnew - setpointcurrent
      newListViewItem.SubItems.Add(deviation.ToString & " " & units)
      If Math.Abs(deviation) > 50 Then newListViewItem.BackColor = Color.Orange

      ListViewComparison.Items.Add(newListViewItem)

    Else

    End If
  End Sub

  Private Sub FillBatchParameterDetails()
    Try

      'Display Checked box if LockSetpoints Batch Parameter is set to '1'
      If programNext_.LockSetpoints = 1 Then
        checkBoxLock.CheckState = CheckState.Checked
      Else : checkBoxLock.CheckState = CheckState.Unchecked
      End If
      ' Set Lock Setpoints status if not enabled with Parameter
      If ControlCode.Parameters.ProgramSetpointEnable = 0 Then
        checkBoxLock.CheckState = CheckState.Checked
      End If

    Catch ex As Exception
      Utilities.Log.LogError(ex)
    End Try
  End Sub

#End Region

  Public Sub LoadDyelot(ByVal dyelot As String)
    'Set dyelot name
    TextBoxJob.Text = dyelot
  End Sub

  Private alreadyLoaded_ As Boolean 'flag to set if we've scanned a job that's already in the schedule

  Public Sub LoadDyelot(ByVal drDyelot As System.Data.DataRow)
    ' Starting a scheduled job
    alreadyLoaded_ = True

    'Set dyelot name
    TextBoxJob.Text = drDyelot("Dyelot").ToString
    TextBoxJob.Enabled = False
    buttonKeyboard.Enabled = False

    listBoxPrograms.Enabled = False 'Don't allow changing of program once scheduled
    ListBoxRework.Enabled = False '

    'Set program
    Dim programNumberToSet As Integer = NullToZeroInteger(drDyelot("Program").ToString)
    Dim programNumber As String

    If programNumberToSet <= 99999 Then
      'Standard Production Program
      For Each program As String In listBoxPrograms.Items
        programNumber = GetProgramNumber(program)
        If programNumber.ToLower.Trim = programNumberToSet.ToString.ToLower.Trim Then
          listBoxPrograms.SelectedItem = program

          Exit For
        End If
      Next program
    Else
      'Rework Deviation Program (width values often cut in half of the original production program)
      TabControlPrograms.SelectTab(TabPage2) 'Set the initial focus to the "Rework" tab
      For Each program As String In ListBoxRework.Items
        programNumber = GetProgramNumber(program)
        If programNumber.ToLower.Trim = programNumberToSet.ToString.ToLower.Trim Then
          ListBoxRework.SelectedItem = program
          TextBoxJob.Focus() 'Set focus back to dyelot name
          Exit For
        End If
      Next program
    End If

    Dim dyelotParameters As String = drDyelot("Parameters").ToString
    ' Example: (With LineFeeds)
    '     LS,0,1
    ' TODO - better method?
    If dyelotParameters = "LS,1,0" Then
      checkBoxLock.CheckState = CheckState.Checked
    ElseIf dyelotParameters = "LS,0,1" Then
      checkBoxLock.CheckState = CheckState.Checked
      checkBoxLockWidth.CheckState = CheckState.Checked
    ElseIf dyelotParameters = "LS,1,1" Then
      checkBoxLock.CheckState = CheckState.Checked
      checkBoxLockWidth.CheckState = CheckState.Checked
    End If


    ' Set Lock Setpoints status if not enabled with Parameter
    If ControlCode.Parameters.ProgramSetpointEnable = 0 Then
      checkBoxLock.CheckState = CheckState.Checked
    End If
    If ControlCode.Parameters.ProgramSetpointWidthEnable = 0 Then
      checkBoxLockWidth.CheckState = CheckState.Checked
    End If



  End Sub

  Private Sub TextBoxDyelotName_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxJob.KeyPress
    Try
      Dim keypressed As Char = e.KeyChar

      'ENTER key is pressed
      If keypressed = Microsoft.VisualBasic.ChrW(Keys.Return) Then
        If Not alreadyLoaded_ Then
          AddNewJob()
        Else
          StartNewJob()
        End If
      ElseIf keypressed = Microsoft.VisualBasic.Chr(Keys.Escape) Then
        Clear()
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
      End If

    Catch ex As Exception
    End Try
  End Sub

  Private Sub buttonOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonOK.Click
    Try
      If Not alreadyLoaded_ Then
        AddNewJob()
      Else
        StartNewJob()
      End If

      'Close the Add Job Form
      Me.Close()

    Catch ex As Exception
      controlCode_.Parent.LogException(ex)
    End Try
  End Sub

  Private Sub buttonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonCancel.Click
    Clear()
    Me.DialogResult = Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Function CheckData() As Boolean
    Try
      If listBoxPrograms.SelectedItem IsNot Nothing Then
      ElseIf ListBoxRework.SelectedItem IsNot Nothing Then
      Else
        Return Message("Please select a new program.")
      End If
      Return True
    Catch ex As Exception
      'some code
    End Try
    Return False
  End Function

  Private Function AddJob() As Boolean
    Try
      'Get data
      Dim drProgram As DataRow = GetSelectedProgramRow()

      'Check once again - a bit redundant really but you can never be too careful
      If drProgram Is Nothing Then Return False

      'If TextBoxJob.Text = "" Then Return Message("Please Enter a Name for this job.")
      If TextBoxJob.Text = "" Then
        '  Return Message("Please Enter a Name for this job.")
        ' Assign a new job name based on datetime
        TextBoxJob.Text = CurrentTime.ToShortDateString & " " & CurrentTime.ToLongTimeString
      End If

      Dyelot = TextBoxJob.Text & " - " & controlCode_.ComputerName

      Return True
    Catch ex As Exception
      'some code
    End Try
    Return False
  End Function

  Private Sub AddNewJob()
    Try
      If CheckData() Then
        If AddJob() Then
          TestDyelotExist(Dyelot)
          AddToLocalDB(controlCode_.Parent, ProgramNumber, True)
          Me.DialogResult = Windows.Forms.DialogResult.OK
          Me.Close()
        Else
        End If
      End If

    Catch ex As Exception
      Dim message As String = ex.Message
      MessageBox.Show("Could not create the new job.", "Adaptive Control", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Try
  End Sub

  Private Function GetSelectedProgramRow() As System.Data.DataRow
    Dim selectedText As String = Nothing
    Try
      If listBoxPrograms.SelectedItem IsNot Nothing Then
        selectedText = listBoxPrograms.SelectedItem.ToString
      ElseIf ListBoxRework.SelectedItem IsNot Nothing Then
        selectedText = ListBoxRework.SelectedItem.ToString
      Else
        Return Nothing
      End If

      'Get the product row for this product
      For Each dr As System.Data.DataRow In dtPrograms.Rows
        Dim program As String = NullToZeroInteger(dr("ProgramNumber").ToString).ToString & "  " & NullToNothingString(dr("Name"))
        If program.ToLower = selectedText.ToLower Then
          Return dr
        End If
      Next

    Catch ex As Exception
      controlCode_.Parent.LogException(ex)
    End Try
    Return Nothing
  End Function

  Private Sub listBoxPrograms_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listBoxPrograms.SelectedIndexChanged
    Try

      If listBoxPrograms.SelectedItem Is Nothing Then Exit Sub
      Dim selectedText As String = listBoxPrograms.SelectedItem.ToString

      'Get the Program Number for this row to update the textbox
      For Each dr As System.Data.DataRow In dtPrograms.Rows
        Dim program As String = NullToZeroInteger(dr("ProgramNumber").ToString).ToString & "  " & NullToNothingString(dr("Name"))
        If program.ToLower = selectedText.ToLower Then
          Dim programnumber As String = NullToZeroInteger(dr("ProgramNumber").ToString).ToString
          TextBoxProgramNumber.Text = programnumber
          Me.ProgramNumber = programnumber
          Exit For
        End If
      Next

      Dim drProgram As System.Data.DataRow = GetSelectedProgramRow()

      ' Clear ProgramNext if set
      programNext_ = New FrameProgram

      ' Get a new program 
      programNext_.Load(drProgram)

      FillListViewComparison()
      FillBatchParameterDetails()

      'Set Focus to Dyelot Name Box for keyboard efficiency
      TextBoxJob.Focus()

    Catch ex As Exception
      Utilities.Log.LogError(ex)
    End Try
  End Sub

  Private Sub listBoxRework_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBoxRework.SelectedIndexChanged
    Try

      If ListBoxRework.SelectedItem Is Nothing Then Exit Sub
      Dim selectedText As String = ListBoxRework.SelectedItem.ToString

      'Get the Program Number for this row to update the textbox
      For Each dr As System.Data.DataRow In dtPrograms.Rows
        Dim program As String = NullToNothingString(dr("ProgramNumber")) & "  " & NullToNothingString(dr("Name"))
        If program.ToLower = selectedText.ToLower Then
          Dim programnumber As String = NullToNothingString(dr("ProgramNumber"))
          TextBoxProgramNumber.Text = programnumber
          Me.ProgramNumber = programnumber
          Exit For
        End If
      Next

      Dim drProgram As System.Data.DataRow = GetSelectedProgramRow()
      programNext_.Load(drProgram)
      FillListViewComparison()
      FillBatchParameterDetails()

      'Set Focus to Dyelot Name Box for keyboard efficiency
      TextBoxJob.Focus()

    Catch ex As Exception
      Utilities.Log.LogError(ex)
    End Try
  End Sub


  Private Sub buttonKeyboard_Click(sender As Object, e As EventArgs) Handles buttonKeyboard.Click
    Using newform As New FormQwerty
      If newform.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
        TextBoxJob.Text = newform.Value
      End If
    End Using
  End Sub

#If 0 Then
  Private Sub buttonKeyboard_Click(sender As Object, e As EventArgs) Handles buttonKeyboard.Click
    Using newform As New FormQwerty
      newform.Text = "Enter Job..."
      If newform.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
        TextBoxJob.Text = newform.Value
      End If
    End Using
  End Sub
#End If

  Private Sub checkBoxLock_Click(sender As Object, e As EventArgs) Handles checkBoxLock.Click
    ' If User attempts to change LockSetpoints checkbox status, confirm option is enabled & signal otherwise

    ' Set Lock Setpoints status if not enabled with Parameter
    If (ControlCode.Parameters.ProgramSetpointEnable = 0) AndAlso Not checkBoxLock.Checked Then
      checkBoxLock.CheckState = CheckState.Checked

      Message("Option is disabled by Parameter 'Program Setpoint Enable' set to '0'.  Please set parameter 'Program Setpoint Enable' to '1' to enable updating setpoints.")
      Exit Sub
    End If
  End Sub


  Private Sub checkBoxLockWidth_Click(sender As Object, e As EventArgs) Handles checkBoxLockWidth.Click
    ' If User attempts to change LockSetpoints checkbox status, confirm option is enabled & signal otherwise

    ' Set Lock Setpoints status if not enabled with Parameter
    If (ControlCode.Parameters.ProgramSetpointWidthEnable = 0) AndAlso Not checkBoxLockWidth.Checked Then
      checkBoxLockWidth.CheckState = CheckState.Checked

      Message("Option is disabled by Parameter 'Program Setpoint Width Enable' set to '0'.  Please set parameter 'Program Setpoint Width Enable' to '1' to enable updating width setpoints.")
      Exit Sub
    End If
  End Sub

  Private Function Message(ByVal text As String) As Boolean
    MessageBox.Show(text.PadRight(64), "Adaptive Control", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
    Return False
  End Function

  Private Sub TimerMain_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerMain.Tick
    TimerMain.Enabled = False
    Try
      'TextBoxDyelotName.Focus()
    Catch ex As Exception
    End Try
    TimerMain.Enabled = True
  End Sub


#Region " New Job/Dyelot Properties & Methods "

  Property Dyelot As String
  Property Redye As Integer
  Property UnBlock As Boolean
  Property ProgramNumber As String

  Public Class SqlInsert
    Private ReadOnly table_ As String, fields_ As New System.Text.StringBuilder, values_ As New System.Text.StringBuilder

    Public Sub New(ByVal table As String)
      table_ = table
    End Sub
    Public Sub Add(ByVal field As String, ByVal value As Object)
      With fields_
        If .Length <> 0 Then .Append(", ")
        .Append(field)
      End With
      With values_
        If .Length <> 0 Then .Append(", ")
        .Append(SqlString(value))
      End With
    End Sub
    Public Overrides Function ToString() As String
      With New System.Text.StringBuilder
        .Append("INSERT INTO ") : .Append(table_)
        .Append(" (") : .Append(fields_) : .Append(") VALUES (")
        .Append(values_) : .Append(")"c)
        Return .ToString
      End With
    End Function
  End Class

  Public Sub TestDyelotExist(ByVal dyelot As String)
    Try
      Dim sql As String = "SELECT TOP (1) Dyelot, ReDye FROM Dyelots WHERE Dyelot='" & dyelot & "' ORDER BY Redye DESC"
      Dim dt As System.Data.DataTable = controlCode_.Parent.DbGetDataTable(sql)

      With dt
        If .Rows Is Nothing OrElse .Rows.Count <> 1 Then
          Redye = 0
        Else
          Redye = NullToZeroInteger(.Rows(0)("Redye")) + 1
        End If
      End With

    Catch ex As Exception
      Utilities.Log.LogError(ex)
    End Try
  End Sub

  Public Sub AddToLocalDB(ByVal parent As ACParent, ByVal program As String, ByVal blocked As Boolean)
    Dim sql As String = Nothing
    Dim lockSetpoints_ As String = "LS"
    Try

      With New SqlInsert("Dyelots")
        .Add("Dyelot", Dyelot)
        .Add("Redye", Redye)
        .Add("Machine", parent.ControlSystemName)
        .Add("Program", program)
        If blocked Then .Add("Blocked", 1)

        ' Build the LockSetpoints batch parameter ("LS,1,1") for total lock or ("LS,0,0") for no locked values
        If checkBoxLock.CheckState = CheckState.Checked Then
          lockSetpoints_ &= ",1"
        Else : lockSetpoints_ &= ",0"
        End If
        If checkBoxLockWidth.CheckState = CheckState.Checked Then
          lockSetpoints_ &= ",1"
        Else : lockSetpoints_ &= ",0"
        End If

        ' Assemble the batch string (LS,0,0 NewLine PT,Temp,Time NewLine PH,Humidity)
        '        .Add("Parameters", lockSetpoints_ & Environment.NewLine &
        '                           "PT," & textboxPlevaTemp.Text.Trim & "," & textboxPlevaTime.Text.Trim & Environment.NewLine &
        '                           "PH," & textboxPlevaHumidity.Text.Trim)
        .Add("Parameters", lockSetpoints_)

        sql = .ToString
      End With

      parent.DbExecute(sql)

    Catch ex As Exception
      Utilities.Log.LogError(ex)
    End Try
  End Sub

  Private Sub StartNewJob()
    Try

      'Get the StartTime required to move this selected Job to the soonest position
      GetNewStartTime()

      'Cancel the current job and Unblock this Job
      UnBlockJob(controlCode_.Parent)

      'Close the Add Job Form
      Me.Close()

    Catch ex As Exception

    End Try
  End Sub

  Private newStartTime_ As Date
  Private Sub GetNewStartTime()
    Dim sql As String = Nothing
    Try

      'Default start time to use
      newStartTime_ = Date.UtcNow

      'Get StartTime from the first scheduled job (if any) 
      '  and set new start time accordingly
      Dim dr As System.Data.DataRow = GetFirstScheduledJob()
      If dr IsNot Nothing Then
        If Not dr.IsNull("StartTime") Then
          newStartTime_ = DirectCast(dr("StartTime"), Date).AddMinutes(-5)
        End If
      End If

    Catch ex As Exception

    End Try
  End Sub

  Private Function GetFirstScheduledJob() As System.Data.DataRow
    'Get the first scheduled job from the local database
    Dim sql As String = Nothing
    Try
      'make sure controlcode has been set
      If controlCode_ IsNot Nothing Then

        sql = "SELECT TOP (1) * FROM Dyelots WHERE (State Is Null) ORDER BY StartTime ASC"
        With controlCode_.Parent.DbGetDataTable(sql)
          If .Rows Is Nothing OrElse .Rows.Count <> 1 Then Return Nothing
          Return .Rows(0)
        End With
      End If

    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
    Return Nothing
  End Function

  Public Sub UnBlockJob(ByVal parent As ACParent)
    Dim sql As String = Nothing
    Dim lockSetpoints_ As String = "LS"
    Try

      'Select the last scheduled job matching this dyelot (test for redyes) and get the redye value
      sql = "SELECT TOP(1) * FROM Dyelots WHERE (State is NULL) AND Dyelot='" & TextBoxJob.Text & "' ORDER BY StartTime"

      Dim dt As System.Data.DataTable = parent.DbGetDataTable(sql)

      If dt.Rows.Count > 0 Then
        'We found a scheduled job matching our dyelot

        Dim parameters_ As String = Nothing

        ' Build the LockSetpoints batch parameter ("LS,1,1") for total lock or ("LS,0,0") for no locked values
        If checkBoxLock.CheckState = CheckState.Checked Then
          lockSetpoints_ &= ",1"
        Else : lockSetpoints_ &= ",0"
        End If
        If checkBoxLockWidth.CheckState = CheckState.Checked Then
          lockSetpoints_ &= ",1"
        Else : lockSetpoints_ &= ",0"
        End If




#If 0 Then
        ' Assemble the batch parameter string
        parameters_ = "'" & lockSetpoints_ & Environment.NewLine &
                      "PT," & textboxPlevaTemp.Text.Trim & "," & textboxPlevaTime.Text.Trim & "," & textboxPlevaTimeTenths.Text.Trim & Environment.NewLine &
                      "PH," & textboxPlevaHumidity.Text.Trim & "'"

        If checkBoxLock.CheckState = CheckState.Checked Then
          parameters_ = "'LS,1" & Environment.NewLine &
                        "PT," & textboxPlevaTemp.Text.Trim & "," & textboxPlevaTime.Text.Trim & "," & textboxPlevaTimeTenths.Text.Trim & Environment.NewLine &
                        "PH," & textboxPlevaHumidity.Text.Trim & "'"
        Else
          parameters_ = "'LS,0" & Environment.NewLine &
                        "PT," & textboxPlevaTemp.Text.Trim & "," & textboxPlevaTime.Text.Trim & "," & textboxPlevaTimeTenths.Text.Trim & Environment.NewLine &
                        "PH," & textboxPlevaHumidity.Text.Trim & "'"
        End If
        
#End If

        ' Assemble the batch parameter string
        parameters_ = "'" & lockSetpoints_ & "'"


        controlCode_.OP.Cancel() 'Cancel Current Job
        For Each dr As DataRow In dt.Rows
          Dim redye_ As Integer = DirectCast(dr("Redye"), Integer)
          Dim unblockstring_ As String = "UPDATE Dyelots SET Blocked=NULL, StartTime='" & newStartTime_ & "', Parameters=" & parameters_ & " WHERE Dyelot='" & TextBoxJob.Text & "' AND Redye=" & redye_
          parent.DbExecute(unblockstring_)
        Next dr

      Else
        Message("No Scheduled Program Found Matching Dyelot: " & TextBoxJob.Text)
      End If

    Catch ex As Exception
      parent.LogEvent(LogEventType.Error, 1, ex.ToString)
    End Try
  End Sub 'UnBlockJob

#End Region

  Public ReadOnly Property Value() As String
    Get
      Value = TextBoxProgramNumber.Text
    End Get
  End Property

  Public Sub Clear()
    TextBoxProgramNumber.Text = ""
  End Sub

  Private Sub SelectProgram()
    Try
      If TextBoxProgramNumber.Text.Length > 0 Then

        With listBoxPrograms
          For Each item As Object In .Items
            If item.ToString.Substring(0, TextBoxProgramNumber.TextLength) = TextBoxProgramNumber.Text Then
              .SelectedItem = item
              Dim drProgram As System.Data.DataRow = GetSelectedProgramRow()
              programNext_.Load(drProgram)
              FillListViewComparison()
              FillBatchParameterDetails()
            End If
          Next
        End With
      End If
    Catch ex As Exception
      'some code
    End Try
  End Sub

#Region " UTILITIES "

  Private Function GetProgramNumber(ByVal program As String) As String
    Try
      Dim index As Integer = program.IndexOf(" ")
      If index > 0 Then Return program.Substring(0, index).Trim

    Catch ex As Exception
      'Do nothing
    End Try
    Return Nothing
  End Function

#End Region

End Class