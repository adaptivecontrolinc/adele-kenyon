'******************************************************************************************************
'******                                 Mimic Setup Properties                         ****************
'******************************************************************************************************
'Frame Configuration Setup for Allen-Bradley VersaView 1550M screen
' touchscreen : 1024x768 at 96pixels/inch

'Mimic
'VSD page size: 10.65in x 6.82in 
'PNG size: screen resoloution / 10.65x6.82
'Mimic Size: 1029pixels x 658pixels (no scroll bar when in expert on bottom or side)
'******************************************************************************************************
'******************************************************************************************************
Public Class UtilitiesControl

  Private currentDate_ As String = Date.Now.AddDays(-30).ToShortDateString
  Private sqlGasUsage_ As String = "SELECT [Date], [GasUsed] FROM [Utilities] WHERE ([Date] > '" & currentDate_ & "') ORDER BY [Date] DESC "

  Private dataGridSetup_ As Boolean

  Private controlCode_ As ControlCode
  Public Property ControlCode() As ControlCode
    Get
      Return controlCode_
    End Get
    Set(ByVal value As ControlCode)
      controlCode_ = value
    End Set
  End Property

  Public Sub New(ByVal controlCode As ControlCode)
    DoubleBuffered = True  ' no flicker 

    controlCode_ = controlCode

    ' Add any initialization after the InitializeComponent() call.
    InitializeComponent()

    SetupUtilitiesControl()

  End Sub

  Private Sub SetupUtilitiesControl()
    If controlCode_ Is Nothing Then

    Else
      Try
        SetupDataGridUtilities()
        SetRowColor()
      Catch ex As Exception
        Message("Could Not Load Data Table: " & ex.Message)
      End Try
    End If
  End Sub

  Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
    If controlCode_ Is Nothing Then Exit Sub

    If Runtime.Remoting.RemotingServices.IsTransparentProxy(ControlCode) Then
      ' disable (maybe make invisible) some buttons, etc

      btnGasUsageRefresh.Visible = False
    End If

    With controlCode_

      vlGasUsedCubicFeet.Value = .GasUsage.GasUseJob.TotalVolume
      lbGasUsedDecatherms.Text = "DecaTherms: " & (.GasUsage.GasUseJob.DecaTherm.ToString) & " Dth"
      vlGasFlowRate.Value = .GasUsage.GasUseJob.CubicFeetPerMinute

      'Group Box 1
      gbZone1.Text = "Zone 1 (" & .AirTemp_Zone(1).Coms_ScanInterval.ToString & "ms)"
      lbAirZone1Setpoint.Text = "Setpoint: " & (.IO.RemoteValue(1) / 10).ToString & "F"
      lbAirZone1Actual.Text = "Actual: " & (.IO.AirTempActual(1) / 10).ToString & "F"
      outputZone1.Value = .IO.WorkingOutput(1) / 1000

      'Group Box 2
      gbZone2.Text = "Zone 2 (" & .AirTemp_Zone(2).Coms_ScanInterval.ToString & "ms)"
      lbAirZone2Setpoint.Text = "Setpoint: " & (.IO.RemoteValue(2) / 10).ToString & "F"
      lbAirZone2Actual.Text = "Actual: " & (.IO.AirTempActual(2) / 10).ToString & "F"
      outputZone2.Value = .IO.WorkingOutput(2) / 1000

      'Group Box 3
      gbZone3.Text = "Zone 3 (" & .AirTemp_Zone(3).Coms_ScanInterval.ToString & "ms)"
      lbAirZone3Setpoint.Text = "Setpoint: " & (.IO.RemoteValue(3) / 10).ToString & "F"
      lbAirZone3Actual.Text = "Actual: " & (.IO.AirTempActual(3) / 10).ToString & "F"
      outputZone3.Value = .IO.WorkingOutput(3) / 1000

      'Group Box 4
      gbZone4.Text = "Zone 4 (" & .AirTemp_Zone(4).Coms_ScanInterval.ToString & "ms)"
      lbAirZone4Setpoint.Text = "Setpoint: " & (.IO.RemoteValue(4) / 10).ToString & "F"
      lbAirZone4Actual.Text = "Actual: " & (.IO.AirTempActual(4) / 10).ToString & "F"
      outputZone4.Value = .IO.WorkingOutput(4) / 1000

      ' TODO 
      ' Zones 5 - 8


    End With

    ' Test to see that we've just past 1am (using midnight to the current days usage)
    '    and update the daily GasUsage value in Utilities Table
    If Date.Now.Hour > 13 Then
      isPM_ = True
    Else : isPM_ = False
    End If
    If Not isPM_ Then
      If wasPM_ Then
        gridUtilities.DataSource = ControlCode.Parent.DbGetDataTable(sqlGasUsage_)
      End If
    End If
    If Date.Now.Hour > 13 Then
      wasPM_ = True
    Else : wasPM_ = False
    End If

  End Sub

  Private Sub SetupDataGridUtilities()
    Try
      With gridUtilities
        .DataSource = ControlCode.Parent.DbGetDataTable(sqlGasUsage_)
        .Columns.Clear()
        .AllowUserToAddRows = False
        .AllowUserToDeleteRows = False
        .AllowUserToOrderColumns = False
        .AllowUserToResizeColumns = False
        .AllowUserToResizeRows = False
        .AutoGenerateColumns = False
        .BackgroundColor = Color.Silver
        .BorderStyle = System.Windows.Forms.BorderStyle.None
        .CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Single
        .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        .ColumnHeadersHeight = 35
        .EditMode = DataGridViewEditMode.EditProgrammatically
        .MultiSelect = False
        .ReadOnly = True
        .RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        .RowHeadersWidth = 20
        .ScrollBars = ScrollBars.Vertical
        .SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        .AlternatingRowsDefaultCellStyle.BackColor = Color.Cornsilk

        AddTextColumn(gridUtilities, "Date", "Date", 200, DataGridViewContentAlignment.MiddleCenter)
        AddTextColumn(gridUtilities, "GasUsed", "Gas Used (cf)", DataGridViewContentAlignment.MiddleCenter)


      End With
    Catch ex As Exception

    End Try
  End Sub

#Region "Properties"

  Private wasPM_ As Boolean
  Private isPM_ As Boolean
  Public ReadOnly Property IsPM() As Boolean
    Get
      Return isPM_
    End Get
  End Property

#End Region

#Region "Buttons"

  Private Sub btnGasUsageRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGasUsageRefresh.Click
    Try
      gridUtilities.DataSource = ControlCode.Parent.DbGetDataTable(sqlGasUsage_)
    Catch ex As Exception
      Message("Could Not Refresh Data Table: " & ex.Message)
    End Try
  End Sub

  Private Function Message(ByVal text As String) As Boolean
    MessageBox.Show(text.PadRight(64), "Adaptive Control", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
    Return False
  End Function

#End Region

#Region "DataGrid Utilities"

  Public Sub AddTextColumn(ByVal datagrid As DataGridView, ByVal name As String, ByVal headerText As String, ByVal width As Integer, ByVal alignment As System.Windows.Forms.DataGridViewContentAlignment)
    Dim column As New System.Windows.Forms.DataGridViewTextBoxColumn
    With column
      .DataPropertyName = name
      .DefaultCellStyle.Alignment = alignment
      .HeaderText = headerText
      .Name = name
      .ReadOnly = True
      .Width = width
    End With
    datagrid.Columns.Add(column)
  End Sub

  Public Sub AddTextColumn(ByVal datagrid As DataGridView, ByVal name As String, ByVal headerText As String, ByVal alignment As System.Windows.Forms.DataGridViewContentAlignment)
    Dim column As New System.Windows.Forms.DataGridViewTextBoxColumn
    With column
      .DataPropertyName = name
      .DefaultCellStyle.Alignment = alignment
      .HeaderText = headerText
      .Name = name
      .ReadOnly = True
      .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    End With
    datagrid.Columns.Add(column)
  End Sub

  Private Sub SetRowColor()
    Try
      With gridUtilities
        For Each row As DataGridViewRow In .Rows
          'Dim DispenseState As String = row.Cells("State").FormattedValue.ToString
          'If DispenseState = "309" Then
          ' row.DefaultCellStyle.BackColor = Color.Red
          ' End If
        Next
      End With
    Catch ex As Exception

    End Try
  End Sub

#End Region

End Class
