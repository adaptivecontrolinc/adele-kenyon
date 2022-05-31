Public Class ControlWorkListCurrent
  Private selectedItem As ListViewItem
  Private jobs As System.Data.DataTable

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
    InitializeControl()
  End Sub

  Private Sub InitializeControl()
    SetupListView()
  End Sub

  Public Sub Requery()
    Try
      If parent IsNot Nothing Then
        FillTable()
        FillListView()
      End If
    Catch ex As Exception

    End Try
  End Sub

  Private Sub SetupListView()
    With listViewMain
      .View = View.Details
      .BorderStyle = Windows.Forms.BorderStyle.None
      .GridLines = True
      .FullRowSelect = True
      .HeaderStyle = ColumnHeaderStyle.Nonclickable
      .MultiSelect = False

      .Columns.Clear()
      .Columns.Add("Current Jobs", 192)
      .Columns.Add("Dispenser", 70)
      .Columns.Add("Dyelot", 80)
      .Columns.Add("ReDye", 50)
      .Columns.Add("Step", 50)
      .Columns.Add("Destination", 100)
      .Columns.Add("Product", 150)
      .Columns.Add("Amount", 64)
      .Columns.Add("A/M", 50)
    End With
  End Sub

  Private Sub FillTable()
    Dim sql As String = Nothing
    Try
      If parent IsNot Nothing Then
        sql = "SELECT DyelotsBulkedRecipe.ID as ID,DyelotsBulkedRecipe.DyelotID as DyelotID, DyelotsBulkedRecipe.Destination as Destination,DyelotsBulkedRecipe.ProductCode as ProductCode,DyelotsBulkedRecipe.ProductName as ProductName,DyelotsBulkedRecipe.ProductAmount as ProductAmount,DyelotsBulkedRecipe.Dispensed as Dispensed,Dyelots.Dyelot As Dyelot,Dyelots.Redye As ReDye,Dyelots.Machine As Dispenser,Dyelots.State As DyelotState,Dyelots.StartTime As StartTime,Dyelots.DispenseDyelot,Dyelots.DispenseReDye,Dyelots.DispenseCallOff FROM DyelotsBulkedRecipe INNER JOIN Dyelots ON DyelotsBulkedRecipe.DyelotID=Dyelots.ID WHERE Dyelots.State=2 ORDER BY Dyelots.StartTime"
        jobs = controlCode_.Parent.DbGetDataTable(sql)
      End If
    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
  End Sub

  Private Sub FillListView()
    Try
      FillTable()
      listViewMain.Items.Clear()
      Dim lastDyelot, Dyelot As String
      Dim lastReDye, ReDye As Integer
      Dim FirstRow As Boolean = True
      For Each dr As System.Data.DataRow In jobs.Rows
        If Not dr.IsNull("Dyelot") Then
          Dyelot = dr("Dyelot").ToString
          ReDye = Utilities.Sql.NullToZeroInteger(dr("ReDye").ToString)
          If Not (Dyelot = lastDyelot) And (ReDye = lastReDye) Then
            lastDyelot = Dyelot
            lastReDye = ReDye
            AddMainItem(dr)
          Else
            AddSubItem(dr)
          End If
          FirstRow = False
        End If
      Next

    Catch ex As Exception
      Utilities.Log.LogError(ex)
    End Try
  End Sub

  Private Sub AddMainItem(ByVal dr As System.Data.DataRow)
    Dim newItem As New ListViewItem(dr("Dyelot").ToString)
    With newItem
      .Name = dr("ID").ToString
      .Text = dr("Dyelot").ToString
      .SubItems.Add(dr("Dispenser").ToString)
      .SubItems.Add(dr("DispenseDyelot").ToString)
      .SubItems.Add(dr("DispenseReDye").ToString)
      .SubItems.Add(dr("DispenseCallOff").ToString)
      .SubItems.Add(dr("Destination").ToString)
      .SubItems.Add(dr("ProductCode").ToString & "  " & dr("ProductName").ToString.Trim)
      .SubItems.Add(dr("ProductAmount").ToString)
      .SubItems.Add(dr("Dispensed").ToString.Trim)
    End With
    listViewMain.Items.Add(newItem)
  End Sub

  Private Sub AddSubItem(ByVal dr As System.Data.DataRow)
    Dim newItem As New ListViewItem(dr("Dyelot").ToString)
    With newItem
      .Name = dr("ID").ToString
      .Text = Nothing
      .SubItems.Add(" ")
      .SubItems.Add(" ")
      .SubItems.Add(" ")
      .SubItems.Add(" ")
      .SubItems.Add(" ")
      .SubItems.Add(dr("ProductCode").ToString & "  " & dr("ProductName").ToString.Trim)
      .SubItems.Add(dr("ProductAmount").ToString)
      .SubItems.Add(dr("Dispensed").ToString.Trim)
    End With
    listViewMain.Items.Add(newItem)
  End Sub


End Class
