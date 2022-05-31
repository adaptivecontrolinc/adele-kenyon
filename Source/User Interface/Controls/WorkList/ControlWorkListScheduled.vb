Imports Utilities.Sql

Public Class ControlWorkListScheduled
  Private selectedItem As ListViewItem
  Private jobs As System.Data.DataTable
  Private programs As System.Data.DataTable

  Private controlCode_ As ControlCode
  Public Property ControlCode() As ControlCode
    Get
      Return controlCode_
    End Get
    Set(ByVal value As ControlCode)
      controlCode_ = value
      If controlCode_ IsNot Nothing Then
      End If
    End Set
  End Property

  Public Sub New()
    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    InitializeControl()
  End Sub

  Private Sub InitializeControl()
    SetupListView()
  End Sub

  Public Sub Requery()
    SaveSelectedItem()
    FillJobsTable()
    FillListView()
    RestoreSelectedItem()

  End Sub

#Region "ToolStrip Functions"

  Private Sub toolStripMain_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolStripMain.ItemClicked
    Select Case e.ClickedItem.Name
      Case "toolStripButtonNew"
        If controlCode_ IsNot Nothing AndAlso controlCode_.FirstScanDone Then
          Using FormAddJob As New FormAddJob(controlCode_)
            FormAddJob.ShowDialog(Me)
            Requery()
          End Using
        End If
        If Not listViewMain.Focused Then Exit Sub

      Case "toolStripButtonFirst"
        MoveFirst()

      Case "toolStripButtonSooner"
        MoveSooner()

      Case "toolStripButtonLater"
        MoveLater()

      Case "toolStripButtonLast"
        MoveLast()

      Case "toolStripButtonDelete"
        Delete()

      Case "toolStripButtonRefresh"
        Requery()

      Case "toolStripButtonBlocked"
        ToggleBlocked()
    End Select
  End Sub 'toolStripMain_ItemClicked

  Private Sub Delete()
    'Get the selected dyelot
    Dim job As String = GetSelectedDyelot()

    'If there is a selected dyelot (some text value)
    If job <> "" Then
      Dim reply As DialogResult
      reply = MessageBox.Show("Are you sure you want to delete this job?" & Environment.NewLine & job, "Adaptive Control", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
      If reply = DialogResult.Yes Then
        DeleteDyelot(job)
        Requery()
      End If
    Else
      'No Job is selected
      MessageBox.Show("Please select a job to delete", "Adaptive Control", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
    End If

  End Sub 'Delete

  Private Sub DeleteDyelot(ByVal id As Integer)
    Dim sql As String = Nothing
    Try
      'make sure controlcode has been set
      If controlCode_ IsNot Nothing Then

        'Delete the local data
        sql = "DELETE FROM Dyelots WHERE ID=" & id.ToString
        controlCode_.Parent.DbExecute(sql)

        'Delete from local DyelotsBulkedRecipe as well just in case the foreign is not setup properly
        sql = "DELETE FROM DyelotsBulkedRecipe WHERE DyelotID=" & id.ToString
        controlCode_.Parent.DbExecute(sql)

      End If

    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
  End Sub 'DeleteDyelot(id)

  Private Sub DeleteDyelot(ByVal job As String)
    Dim sql As String = Nothing
    Try
      'make sure controlcode has been set
      If controlCode_ IsNot Nothing Then

        'Delete the local data
        sql = "DELETE FROM Dyelots WHERE " & job
        controlCode_.Parent.DbExecute(sql)

      End If
    Catch ex As Exception

    End Try
  End Sub 'DeleteDyelot(job)

  Private Sub ToggleBlocked()
    'Toggle blocked for the selected dyelot
    Dim sql As String = Nothing
    Try
      'make sure controlcode has been set
      If controlCode_ IsNot Nothing Then

        'Get the selected Dyelot (if any)
        Dim job As String = GetSelectedDyelot()
        If job = "" Then Exit Sub

        'TODO - insert the test functions here to verify old job to new job parameter constraints

        'Get the current dyelot row so we know for sure what the blocked value is
        sql = "SELECT * FROM Dyelots WHERE " & job
        Dim dyelots As System.Data.DataTable
        dyelots = controlCode_.Parent.DbGetDataTable(sql)

        'Was the Unblock button pressed w/o selecting a Job first?
        If dyelots Is Nothing OrElse dyelots.Rows.Count <= 0 Then
          MessageBox.Show("Please Select a Job to Unblock", "Unblock", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1)
        End If

        'Have a Matching Dyelot, Pass the details to the new job form to be loaded
        If dyelots.Rows.Count >= 1 Then
          Using newForm As New FormAddJob(controlCode_)
            newForm.LoadDyelot(dyelots.Rows(0))
            newForm.ShowDialog(Me)
          End Using
        End If

        Requery()
      End If

    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
  End Sub 'ToggleBlocked

  Private Sub MoveFirst()
    Dim sql As String = Nothing
    Try
      'make sure controlcode has been set
      If controlCode_ IsNot Nothing Then

        'Get the selected Dyelot (if any)
        Dim job As String = GetSelectedDyelot()
        If job = "" Then Exit Sub

        'Default start time to use
        Dim newStartTime As Date = Date.UtcNow

        'Get StartTime from the first scheduled job (if any) 
        '  and set new start time accordingly
        Dim dr As System.Data.DataRow = GetFirstScheduledJob()
        If dr IsNot Nothing Then
          If Not dr.IsNull("StartTime") Then
            newStartTime = DirectCast(dr("StartTime"), Date).AddMinutes(-1)
          End If
        End If

        'Update the start time of the selected job
        sql = "UPDATE Dyelots SET StartTime='" & newStartTime & "' WHERE " & job
        controlCode_.Parent.DbExecute(sql)

        'Refresh list view
        Requery()
      End If

    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
  End Sub 'MoveFirst

  Private Sub MoveSooner()
    Dim sql As String = Nothing
    Try
      'make sure controlcode has been set
      If controlCode_ IsNot Nothing Then

        'Get the selected Dyelot (if any)
        Dim job As String = GetSelectedDyelot()
        If job = "" Then Exit Sub

        'Get StartTime from the first scheduled job (if any) 
        '  and set new start time accordingly
        Dim dr As System.Data.DataRow = GetPreviousJob(job)
        If (dr Is Nothing) OrElse dr.IsNull("StartTime") Then Exit Sub

        'New start time to use
        Dim newStartTime As Date = DirectCast(dr("StartTime"), Date).AddMinutes(-2)

        'Update the start time of the selected job
        sql = "UPDATE Dyelots SET StartTime='" & newStartTime & "' WHERE " & job
        controlCode_.Parent.DbExecute(sql)

        'Refresh list view
        Requery()
      End If

    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
  End Sub 'MoveSooner

  Private Sub MoveLater()
    Dim sql As String = Nothing
    Try
      'make sure controlcode has been set
      If controlCode_ IsNot Nothing Then

        'Get the selected Dyelot (if any)
        Dim job As String = GetSelectedDyelot()
        If job = "" Then Exit Sub

        'Get StartTime from the first scheduled job (if any) 
        '  and set new start time accordingly
        Dim dr As System.Data.DataRow = GetNextJob(job)
        If (dr Is Nothing) OrElse dr.IsNull("StartTime") Then Exit Sub

        'New start time to use
        Dim newStartTime As Date = DirectCast(dr("StartTime"), Date).AddMinutes(2)

        'Update the start time of the selected job
        sql = "UPDATE Dyelots SET StartTime='" & newStartTime & "' WHERE " & job
        controlCode_.Parent.DbExecute(sql)

        'Refresh list view
        Requery()
      End If

    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
  End Sub 'MoveLater

  Private Sub MoveLast()
    Dim sql As String = Nothing
    Try
      'make sure controlcode has been set
      If controlCode_ IsNot Nothing Then

        'Get the selected Dyelot (if any)
        Dim job As String = GetSelectedDyelot()
        If job = "" Then Exit Sub

        'Default start time to use
        Dim newStartTime As Date = Date.UtcNow

        'Get StartTime from the last scheduled job (if any)
        '  and update new start time accordingly
        Dim dr As System.Data.DataRow = GetLastScheduledJob()
        If dr IsNot Nothing Then
          If Not dr.IsNull("StartTime") Then
            newStartTime = DirectCast(dr("StartTime"), Date).AddMinutes(1)
          End If
        End If

        'Update the start time of the selected job
        sql = "UPDATE Dyelots SET StartTime='" & newStartTime & "' WHERE " & job
        controlCode_.Parent.DbExecute(sql)

        'Refresh list view
        Requery()
      End If

    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
  End Sub 'MoveLast

#End Region

#Region " Dyelot functions "

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
  End Function 'GetFirstScheduledJob

  Private Function GetLastScheduledJob() As System.Data.DataRow
    'Get the last scheduled job from the local database
    Dim sql As String = Nothing
    Try
      'make sure controlcode has been set
      If controlCode_ IsNot Nothing Then

        sql = "SELECT TOP (1) * FROM Dyelots WHERE (State Is Null) ORDER BY StartTime DESC"
        With controlCode_.Parent.DbGetDataTable(sql)
          If .Rows Is Nothing OrElse .Rows.Count <> 1 Then Return Nothing
          Return .Rows(0)
        End With
      End If

    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
    Return Nothing
  End Function 'GetLastScheduledJob

  Private Function GetPreviousJob(ByVal id As Integer) As System.Data.DataRow
    'Get the job preceding this one
    Dim sql As String = Nothing
    Try

      'make sure controlcode has been set
      If controlCode_ IsNot Nothing Then
        sql = "SELECT * FROM Dyelots WHERE (State Is Null) ORDER BY StartTime ASC"
        With controlCode_.Parent.DbGetDataTable(sql)
          If .Rows.Count > 0 Then
            For i As Integer = 0 To .Rows.Count - 1
              If NullToZeroInteger(.Rows(i)("id")) = id Then
                If i > 0 Then Return .Rows(i - 1)
              End If
            Next i
          End If
        End With
      End If

    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
    Return Nothing
  End Function 'GetPreviousJob(id)

  Private Function GetPreviousJob(ByVal job As String) As System.Data.DataRow
    'Get the job preceding this one
    Dim sql As String = Nothing
    Try

      'make sure controlcode has been set
      If controlCode_ IsNot Nothing Then
        sql = "SELECT * FROM Dyelots WHERE (State Is Null) ORDER BY StartTime ASC"
        With controlCode_.Parent.DbGetDataTable(sql)
          If .Rows.Count > 0 Then
            For i As Integer = 0 To .Rows.Count - 1
              Dim presentDyelot As String = NullToNothingString(.Rows(i)("Dyelot"))
              Dim presentRedye As String = NullToZeroInteger(.Rows(i)("Redye")).ToString
              Dim presentJob As String = "(Dyelot='" & presentDyelot & "') AND (ReDye=" & presentRedye & ")"
              If presentJob.ToLower = job.ToLower Then
                'we have a match!
                If i > 0 Then Return .Rows(i - 1)
              End If
            Next i
          End If
        End With
      End If
    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
    Return Nothing
  End Function 'GetPreviousJob(job)

  Private Function GetNextJob(ByVal id As Integer) As System.Data.DataRow
    'Get the job after this one
    Dim sql As String = Nothing
    Try

      'make sure controlcode has been set
      If controlCode_ IsNot Nothing Then
        sql = "SELECT * FROM Dyelots WHERE (State Is Null) ORDER BY StartTime ASC"
        With controlCode_.Parent.DbGetDataTable(sql)
          If .Rows.Count > 0 Then
            For i As Integer = 0 To .Rows.Count - 1
              If NullToZeroInteger(.Rows(i)("id")) = id Then
                If i < (.Rows.Count - 1) Then Return .Rows(i + 1)
              End If
            Next i
          End If
        End With
      End If

    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
    Return Nothing
  End Function 'GetNextJob(id)

  Private Function GetNextJob(ByVal job As String) As System.Data.DataRow
    'Get the job after this one
    Dim sql As String = Nothing
    Try

      'make sure controlcode has been set
      If controlCode_ IsNot Nothing Then
        sql = "SELECT * FROM Dyelots WHERE (State Is Null) ORDER BY StartTime ASC"
        With controlCode_.Parent.DbGetDataTable(sql)
          If .Rows.Count > 0 Then
            For i As Integer = 0 To .Rows.Count - 1
              Dim presentDyelot As String = NullToNothingString(.Rows(i)("Dyelot")) ' TODO Null.NullToEmptyString(.Rows(i)("Dyelot"))
              Dim presentRedye As String = NullToZeroInteger(.Rows(i)("Redye")).ToString
              Dim presentJob As String = "(Dyelot='" & presentDyelot & "') AND (ReDye=" & presentRedye & ")"
              If presentJob.ToLower = job.ToLower Then
                'we have a match!
                If i < (.Rows.Count - 1) Then Return .Rows(i + 1)
              End If
            Next i
          End If
        End With
      End If

    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
    Return Nothing
  End Function 'GetNextJob(job)

#End Region

#Region " List View "

  Private Sub SetupListView()
    With listViewMain
      .View = View.Details
      .BorderStyle = Windows.Forms.BorderStyle.None
      .GridLines = True
      .FullRowSelect = True
      .HeaderStyle = ColumnHeaderStyle.Nonclickable
      .MultiSelect = False

      .Columns.Clear()
      .Columns.Add("Dyelot", 230)
      .Columns.Add("ReDye", 70)
      .Columns.Add("Blocked", 80, HorizontalAlignment.Center)
      .Columns.Add("Program", 110, HorizontalAlignment.Center)
      .Columns.Add("Program Name", 220)
      .Columns.Add("Program Notes", 310)
    End With
  End Sub 'SetupListView

#If 0 Then
        sql = "SELECT Dyelot,Redye,Blocked,State As DyelotState,StartTime," & _
             "Programs.ProgramNumber as ProgramNumber,Programs.Name as ProgramName," & _
             "Programs.Notes as ProgramNotes " & _
             "FROM Dyelots INNER JOIN Programs " & _
             "ON CAST(CAST(Dyelots.Program As nvarchar(50)) AS Int)=Programs.ProgramNumber " & _
             "WHERE Dyelots.State Is Null " & _
             "ORDER BY Dyelots.StartTime "


       sql = "SELECT Dyelot,Redye,Blocked,State As DyelotState,StartTime,Program " & _
             "FROM Dyelots " & _
             "WHERE Dyelots.State Is Null " & _
             "ORDER BY Dyelots.StartTime "
#End If

  Private Sub FillJobsTable()
    Dim sql As String = Nothing

    Try
      'make sure controlcode has been set
      If controlCode_ IsNot Nothing Then

        sql = "SELECT ProgramNumber, Name, Notes " &
              "FROM Programs " &
              "ORDER BY ProgramNumber "
        programs = controlCode_.Parent.DbGetDataTable(sql)

        ' TODO - why use the " - " hyphen for hte program name and notes?
        sql = "SELECT Dyelot,Redye,Blocked,State As DyelotState,StartTime,Program, " &
             "'-' as ProgramName, '-' as ProgramNotes " &
             "FROM Dyelots " &
             "WHERE Dyelots.State Is Null " &
             "ORDER BY Dyelots.StartTime "
        jobs = controlCode_.Parent.DbGetDataTable(sql)


        If (jobs IsNot Nothing) AndAlso (programs IsNot Nothing) Then
          For Each row_Jobs As System.Data.DataRow In jobs.Rows
            Dim ProgramScheduled As Integer = Nothing
            ProgramScheduled = GetFirstScheduledProgram(row_Jobs("Program").ToString)
            For Each row_Programs As System.Data.DataRow In programs.Rows
              If CInt(row_Programs("ProgramNumber")) = ProgramScheduled Then
                row_Jobs("ProgramName") = row_Programs("Name")
                row_Jobs("ProgramNotes") = row_Programs("Notes")
                Exit For
              End If
            Next row_Programs
          Next row_Jobs
        End If

      End If
    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
  End Sub 'FillJobsTable

  Private Function GetFirstScheduledProgram(ByVal ProgramsString As String) As Integer
    Dim ProgramInt As Integer = 0
    Try

      Dim charSeparators() As Char = {","c}
      Dim result() As String 'Result of the Split method on given ProgramsString

      result = ProgramsString.Split(charSeparators, StringSplitOptions.None)

      If (ProgramsString <> "") Then
        ProgramInt = CInt(result(0))
        Return ProgramInt
      End If

    Catch ex As Exception
      Utilities.Log.LogError(ex)
    End Try
    Return 0
  End Function

  Private Sub FillListView()
    Try
      FillJobsTable()
      listViewMain.Items.Clear()
      Dim lastDyelot, Dyelot As String
      Dim lastReDye, ReDye As Integer
      Dim FirstRow As Boolean = True
      If jobs IsNot Nothing Then
        For Each dr As System.Data.DataRow In jobs.Rows
          If Not dr.IsNull("Dyelot") Then
            Dyelot = NullToNothingString(dr("Dyelot")) 'TODO?  NullToEmptyString(dr("Dyelot"))
            ReDye = NullToZeroInteger(dr("ReDye"))
            If Not ((Dyelot = lastDyelot) And (ReDye = lastReDye)) Then
              lastDyelot = Dyelot
              lastReDye = ReDye
              AddMainItem(dr)
            Else
              AddSubItem(dr)
            End If
            FirstRow = False
          End If
        Next
      End If
    Catch ex As Exception
      Utilities.Log.LogError(ex)
    End Try
  End Sub 'FillListView

  Private Sub AddMainItem(ByVal dr As System.Data.DataRow)
    Dim newItem As New ListViewItem(dr("Dyelot").ToString)
    With newItem
      .Text = dr("Dyelot").ToString
      .SubItems.Add(dr("ReDye").ToString)
      If NullToZeroInteger(dr("Blocked").ToString) = 1 Then
        .SubItems.Add("|")
      Else
        .SubItems.Add(" ")
      End If
      .SubItems.Add(dr("Program").ToString)
      '.SubItems.Add(dr("ProgramNumber").ToString)
      .SubItems.Add(dr("ProgramName").ToString)
      .SubItems.Add(dr("ProgramNotes").ToString.Trim)
    End With
    listViewMain.Items.Add(newItem)
  End Sub 'AddMainItem

  Private Sub AddSubItem(ByVal dr As System.Data.DataRow)
    Dim newItem As New ListViewItem(dr("Dyelot").ToString)
    With newItem
      .SubItems.Add(" ")
      .SubItems.Add(" ")
      If NullToZeroInteger(dr("Blocked").ToString) = 1 Then
        .SubItems.Add("|")
      Else
        .SubItems.Add(" ")
      End If
      .SubItems.Add(" ")
      .SubItems.Add(" ")
    End With
    listViewMain.Items.Add(newItem)
  End Sub 'AddSubItem

  Private Sub SaveSelectedItem()
    selectedItem = Nothing
    If listViewMain.SelectedItems.Count > 0 Then selectedItem = listViewMain.SelectedItems(0)
  End Sub 'SaveSelectedItem

  Private Sub RestoreSelectedItem()
    With listViewMain
      If selectedItem IsNot Nothing Then
        For Each item As ListViewItem In .Items
          If item.Text = selectedItem.Text Then
            item.Selected = True
          End If
        Next
      End If
    End With
    selectedItem = Nothing
  End Sub 'RestoreSelectedItem

  Private Function GetSelectedDyelotID() As Integer
    Dim defaultValue As Integer = -1
    Try
      With listViewMain
        If .SelectedItems.Count > 0 Then
          'Get the ID of this row, match it to the row in the datatable and return DyelotID
          Dim selectedID As Integer = Integer.Parse(.SelectedItems(0).Name)
          Dim id As Integer
          For Each dr As System.Data.DataRow In jobs.Rows
            id = Integer.Parse(dr("id").ToString)
            If id = selectedID Then
              Return Integer.Parse(dr("DyelotID").ToString)
            End If
          Next
        End If
      End With
    Catch ex As Exception
      'just return default value on error
    End Try
    Return defaultValue
  End Function 'GetSelectedDyelotID

  Private Function GetSelectedDyelot() As String 'returns: (Dyelot= 'xxxx') AND (ReDye= x)
    Dim defaultValue As String = ""
    Try
      With listViewMain
        If .SelectedItems.Count > 0 Then
          'Get the Dyelot & Redye of this row, match it to a row in the datatable and return the string
          Dim dyelot As String = .SelectedItems(0).Text.ToString
          Dim redye As String = .SelectedItems(0).SubItems(1).Text.ToString
          Dim selectedJob As String = dyelot & " " & redye
          Dim job As String
          For Each dr As System.Data.DataRow In jobs.Rows
            job = (dr("Dyelot").ToString) & " " & Integer.Parse(dr("Redye").ToString)
            If job = selectedJob Then
              Return "(Dyelot='" & (dr("Dyelot").ToString) & "') AND (ReDye=" & Integer.Parse(dr("Redye").ToString) & ")"
            End If
          Next
        End If

      End With
    Catch ex As Exception
      'Return default value on error only
    End Try
    Return defaultValue
  End Function 'GetSelectedDyelot

  Private Sub listViewMain_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles listViewMain.SelectedIndexChanged
    'Only allow main line to be selected
    With listViewMain
      If .SelectedItems.Count > 0 Then
        If .SelectedIndices(0) > 0 Then
          If .SelectedItems(0).Text = Nothing Then
            .Items(.SelectedIndices(0) - 1).Selected = True
          End If
        End If
      End If
    End With
  End Sub 'listViewMain_SelectedIndexChanged

#End Region

  Private Sub StartDyelot()
    Try
      'Check to see if dyelot already exists
      Dim dyelots As System.Data.DataTable
      dyelots = controlCode_.Parent.DbGetDataTable("SELECT * FROM Dyelots WHERE State Is Null AND Dyelot='" & textBoxDyelot.Text & " - " & controlCode_.ComputerName & "' ORDER BY ReDye")

      'If we have no matching dyelots open new job form with the dyelot name from the text box
      If dyelots Is Nothing OrElse dyelots.Rows.Count <= 0 Then
        Using newForm As New FormAddJob(controlCode_)
          newForm.LoadDyelot(textBoxDyelot.Text)
          newForm.ShowDialog(Me)
        End Using
      End If

      'If we have matching dyelots pass in the dyelot row to the new job form so we can load dyelot info
      If dyelots.Rows.Count >= 1 Then
        Using newForm As New FormAddJob(controlCode_)
          newForm.LoadDyelot(dyelots.Rows(0))
          newForm.ShowDialog(Me)
        End Using
      End If

      'Requery WorkList to update display details
      Requery()

    Catch ex As Exception
      'Show error message ?
      MessageBox.Show("Adaptive", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Try
  End Sub

  Private Sub textBoxDyelot_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles textBoxDyelot.KeyDown
    Select Case e.KeyCode
      Case Keys.Enter
        e.Handled = True
        StartDyelot()
        textBoxDyelot.Text = Nothing
      Case Keys.Tab
        ReFocusTextBoxDyelot()
      Case Keys.Delete
        textBoxDyelot.Text = ""
    End Select
  End Sub

  Private Sub listViewMain_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles listViewMain.KeyDown
    Select Case e.KeyCode
      Case Keys.Delete
        Delete()
      Case Keys.Up
        MoveSooner()
      Case Keys.Down
        MoveLater()
    End Select
  End Sub

  Public Sub ReFocusTextBoxDyelot()
    textBoxDyelot.Focus()
  End Sub

  Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
    MyBase.OnPaint(e)
    ReFocusTextBoxDyelot()
  End Sub

End Class
