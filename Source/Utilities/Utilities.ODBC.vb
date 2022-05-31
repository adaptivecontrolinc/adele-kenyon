Imports System.Data
Imports System.Globalization.CultureInfo


Partial Public NotInheritable Class Utilities
  Public NotInheritable Class ODBCUtil

#Region " Select "

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function GetDataRowODBC(ByVal connectionString As String, ByVal selectString As String) As DataRow
      'Get a data row from the passed connection string and select statement
      '  return nothing if no row found, more than one row found or an exception is thrown
      Try
        Dim dt As New DataTable
        Dim da As New Odbc.OdbcDataAdapter
        da.SelectCommand = New Odbc.OdbcCommand
        With da.SelectCommand
          .CommandText = selectString
          .Connection = New Odbc.OdbcConnection(connectionString)
          .Connection.Open() : da.Fill(dt) : .Connection.Close()
          If dt.Rows.Count = 1 Then Return dt.Rows(0)
        End With

      Catch ex As Exception
        Debug.Print("GetDataRowODBC: " & selectString)
        Debug.Print(ex.Message)
        Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function GetDataRowODBC(ByVal connectionString As String, ByVal selectString As String, ByVal tableName As String) As DataRow
      'Get a data row from the passed connection string and select statement 
      '  set the table name so this row can be used with the update and insert functions here...
      '  return nothing if no row found, more than one row found or an exception is thrown
      Try
        Dim dt As New DataTable(tableName)
        Dim da As New Odbc.OdbcDataAdapter

        da.SelectCommand = New Odbc.OdbcCommand
        With da.SelectCommand
          .CommandText = selectString
          .Connection = New Odbc.OdbcConnection(connectionString)
          .Connection.Open() : da.Fill(dt) : .Connection.Close()
          If dt.Rows.Count = 1 Then Return dt.Rows(0)
        End With

      Catch ex As Exception
        Debug.Print("GetDataRowODBC: " & selectString)
        Debug.Print(ex.Message)
        Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function GetDataRowODBC(ByVal connectionString As String, ByVal tableName As String, ByVal autokey As Integer) As DataRow
      'Get a data row from the passed connection string table name and id
      '  set the table name so this row can be used with the update and insert functions here...
      '  return nothing if no row found, more than one row found or an exception is thrown
      Try
        Dim dt As New DataTable(tableName)
        Dim da As New Odbc.OdbcDataAdapter

        da.SelectCommand = New Odbc.OdbcCommand
        With da.SelectCommand
          .CommandText = "SELECT * FROM " & tableName & " WHERE autokey=" & autokey.ToString(InvariantCulture)
          .Connection = New Odbc.OdbcConnection(connectionString)
          .Connection.Open() : da.Fill(dt) : .Connection.Close()
          If dt.Rows.Count = 1 Then Return dt.Rows(0)
        End With

      Catch Ex As Exception
        Debug.Print("GetDataRowODBC " & tableName & " " & autokey.ToString(InvariantCulture))
        Debug.Print(Ex.Message)
        Utilities.Log.LogError(Ex)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function GetDataTableODBC(ByVal connectionString As String, ByVal selectString As String) As DataTable
      Try
        'Get a data table from the passed connection string and select string
        '  return nothing if no table found or an exception is thrown
        Dim dt As New DataTable
        Dim da As New Odbc.OdbcDataAdapter

        da.SelectCommand = New Odbc.OdbcCommand
        With da.SelectCommand
          .CommandText = selectString
          .Connection = New Odbc.OdbcConnection(connectionString)
          .Connection.Open() : da.Fill(dt) : .Connection.Close()
          Return dt
        End With

      Catch ex As Exception
        Debug.Print("GetDataTableODBC: " & selectString)
        Debug.Print(ex.Message)
        Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function GetDataTableODBC(ByVal connectionString As String, ByVal selectString As String, ByVal tableName As String) As DataTable
      'Get a data table from the passed connection string and select string
      '  set the table name so this table can be used with the update and insert functions here...
      '  return nothing if no table found or an exception is thrown
      Try
        Dim dt As New DataTable(tableName)
        Dim da As New Odbc.OdbcDataAdapter

        da.SelectCommand = New Odbc.OdbcCommand
        With da.SelectCommand

          .CommandText = selectString
          .Connection = New Odbc.OdbcConnection(connectionString)
          .Connection.Open() : da.Fill(dt) : .Connection.Close()
          Return dt
        End With

      Catch ex As Exception
        Debug.Print("GetDataTableODBC: " & selectString)
        Debug.Print(ex.Message)
        Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function GetDataTableSchemaODBC(ByVal connectionString As String, ByVal tableName As String) As DataTable
      'Get the Table Schema for the target table
      '  this will give the structure of table so we can use DataTable.NewRow
      '  assumes the table has an autokey column that is AutoIncrement - not sure this has much effect
      '  return nothing if no table found or an exception is thrown
      Try
        Dim dt As New DataTable(tableName)
        Dim da As New Odbc.OdbcDataAdapter

        da.SelectCommand = New Odbc.OdbcCommand
        With da.SelectCommand
          .CommandText = "SELECT * FROM " & tableName
          .Connection = New Odbc.OdbcConnection(connectionString)
          .Connection.Open() : da.FillSchema(dt, SchemaType.Source) : .Connection.Close()
        End With

        'Set the AutoIncrement properties - this is not imported with the schema so we must manually set it
        dt.Columns("autokey").AutoIncrement = True
        dt.Columns("autokey").AutoIncrementSeed = 1
        dt.Columns("autokey").AutoIncrementStep = 1

        Return dt
      Catch ex As Exception
        Debug.Print("GetDataTableSchemaODBC: " & tableName)
        Debug.Print(ex.Message)
        Utilities.Log.LogError(ex)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function GetDataTableSchemaODBC(ByVal connectionString As String, ByVal tableName As String, ByVal primaryKey As String) As DataTable
      Try
        'Get the Schema for the target table
        '  this will give the structure of table so we can use DataTable.NewRow
        '  doing this here and passing a reference to reduce the number of calls to the database
        Dim dt As New DataTable(tableName)
        Dim da As New Odbc.OdbcDataAdapter

        da.SelectCommand = New Odbc.OdbcCommand
        With da.SelectCommand
          .CommandText = "SELECT * FROM " & tableName
          .Connection = New Odbc.OdbcConnection(connectionString)
          .Connection.Open() : da.FillSchema(dt, SchemaType.Source) : .Connection.Close()
        End With

        'Set primary key if it has been passed
        dt.PrimaryKey = New DataColumn() {dt.Columns(primaryKey)}

        Return dt
      Catch ex As Exception
        Debug.Print("GetDataTableSchemaODBC: " & tableName)
        Debug.Print(ex.Message)
        Utilities.Log.LogError(ex)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function ODBCSelect(ByVal connectionString As String, ByVal selectString As String) As DataTable
      Return GetDataTableODBC(connectionString, selectString)
    End Function



#End Region

#Region " Update "

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function UpdateDataRowODBC(ByVal connectionString As String, ByVal dr As DataRow) As Boolean
      'Write changes in a data row back to the database
      '  return false if write fails or an exception is thrown
      Try
        'Make sure the data row is not empty
        If dr Is Nothing Then Return False
        Dim dt As DataTable = dr.Table

        Dim da As New Odbc.OdbcDataAdapter
        da.UpdateCommand = New Odbc.OdbcCommand

        With da.UpdateCommand

          'Add parameters and build insert string based on table schema
          AddODBCParameters(dt, .Parameters, dr)
          BuildODBCUpdateString(dt, .CommandText)
          'Set values of all parameters from the data row
          '  For Each dc As DataColumn In dt.Columns
          ' .Parameters("@" & dc.ColumnName).Value = dr(dc.ColumnName)
          ' Next
          .Connection = New Odbc.OdbcConnection(connectionString)
          .Connection.Open()
          Dim rows As Integer
          rows = .ExecuteNonQuery()
          .Connection.Close()
        End With

        Return True
      Catch ex As Exception
        Debug.Print("UpdateDataRowODBC: " & dr.Table.TableName)
        Debug.Print(ex.Message)
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Sub BuildODBCUpdateString(ByVal dt As DataTable, ByRef commandText As String)
      'Build a sql update string based on the table schema
      Dim odbcstring As String = Nothing
      Dim autokeyvalue As String = Nothing
      Try
        'Make sure we have a DataTable to work with
        If dt Is Nothing Then Exit Sub

        'Make the sql string
        For Each dc As DataColumn In dt.Columns
          If dc.ColumnName.ToLower <> "autokey" Then
            'odbcstring &= "[" & dc.ColumnName.ToString & "]=@" & dc.ColumnName.ToString & ","
            odbcstring &= "[" & dc.ColumnName.ToString & "]=?," '& dc.ColumnName.ToString & ","
          End If
        Next
        'remove the last comma
        odbcstring = odbcstring.Substring(0, odbcstring.Length - 1)

        commandText = "UPDATE " & dt.TableName & " SET " & odbcstring & " WHERE autokey=?"
      Catch ex As Exception
        Debug.Print("BuildODBCUpdateString: " & dt.TableName)
        Debug.Print(ex.Message)
        Utilities.Log.LogError(ex, odbcstring)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function ODBCUpdate(ByVal connectionString As String, ByVal updateString As String) As Integer
      Try
        Dim rows As Integer
        Dim da As New Odbc.OdbcDataAdapter
        da.UpdateCommand = New Odbc.OdbcCommand
        With da.UpdateCommand
          .CommandText = updateString
          .Connection = New Odbc.OdbcConnection(connectionString)
          .Connection.Open() : rows = .ExecuteNonQuery() : .Connection.Close()
        End With

        Return rows
      Catch ex As Exception
        Debug.Print("ODBCUpdate: " & updateString)
        Debug.Print(ex.Message)
        Utilities.Log.LogError(ex, updateString)
      End Try
      Return -1
    End Function

#End Region

#Region " Delete "

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function ODBCDelete(ByVal connectionString As String, ByVal deleteString As String) As Integer
      Try
        Dim rows As Integer
        Dim da As New Odbc.OdbcDataAdapter
        da.DeleteCommand = New Odbc.OdbcCommand
        With da.DeleteCommand
          .CommandText = deleteString
          .Connection = New Odbc.OdbcConnection(connectionString)
          .Connection.Open() : rows = .ExecuteNonQuery() : .Connection.Close()
        End With

        Return rows
      Catch ex As Exception
        Debug.Print("ODBCDelete: " & deleteString)
        Debug.Print(ex.Message)
        Utilities.Log.LogError(ex, deleteString)
      End Try
      Return -1
    End Function

#End Region

#Region " Insert "

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function InsertDataRowODBC(ByVal connectionString As String, ByVal dr As DataRow) As Boolean
      Try
        'Must have a valid data row
        If dr Is Nothing Then Return False

        Dim dt As DataTable = dr.Table
        Dim da As New Odbc.OdbcDataAdapter

        da.InsertCommand = New Odbc.OdbcCommand
        With da.InsertCommand

          'Add parameters and build insert string based on table schema
          AddODBCParameters(dt, .Parameters, dr)
          BuildODBCInsertString(dt, .CommandText)

          'Set values of all parameters from the data row
          ' For Each dc As DataColumn In dt.Columns
          ' .Parameters("@" & dc.ColumnName).Value = dr(dc.ColumnName)
          'Next

          Dim autoIncrement As Integer = -1
          .CommandText = .CommandText & "; SELECT CAST(scope_identity() AS int);"
          .Connection = New Odbc.OdbcConnection(connectionString)
          .Connection.Open() : autoIncrement = Integer.Parse(.ExecuteScalar().ToString) : .Connection.Close()
          dr("autokey") = autoIncrement
        End With

        Return True
      Catch ex As Exception
        Debug.Print("InsertDataRowODBC: " & dr.Table.TableName)
        Debug.Print(ex.Message)
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function
#If 0 Then
    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function InsertDataTableODBC(ByVal connectionString As String, ByVal dt As DataTable) As Boolean
      Try
        'Must have a valid data table
        If dt Is Nothing Then Return False
        Dim rows As Integer
        Dim da As New Odbc.OdbcDataAdapter
        With da
          .InsertCommand = New Odbc.OdbcCommand

          With .InsertCommand
            '   AddODBCParameters(dt, .Parameters, dr)
            BuildODBCInsertString(dt, .CommandText)

            .Connection = New Odbc.OdbcConnection(connectionString)
            For Each dr As DataRow In dt.Rows
              For Each dc As DataColumn In dt.Columns
                .Parameters("@" & dc.ColumnName).Value = dr(dc.ColumnName)
              Next
              .Connection.Open() : rows = .ExecuteNonQuery() : .Connection.Close()
            Next
          End With
        End With

        Return True
      Catch ex As Exception
        Debug.Print("InsertDataRowODBC: " & dt.TableName)
        Debug.Print(ex.Message)
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function InsertDataTableODBC(ByVal connectionString As String, ByVal dt As DataTable, ByVal ODBCDelete As String) As Boolean
      'Insert a datatable into the database - this is used to update detail tables using a transactiion
      '  we start the transaction, delete the original records then insert the new records
      '  the transaction will be rolled back if any errors occur
      'Create the connection and declare the command and transaction objects
      Try
        Dim connection As New Odbc.OdbcConnection(connectionString)
        Dim deleteCommand As Odbc.OdbcCommand
        Dim insertCommand As Odbc.OdbcCommand
        Dim transaction As Odbc.OdbcTransaction

        'Open the connection and make the command and transaction objects on this connection
        connection.Open()
        transaction = connection.BeginTransaction
        deleteCommand = connection.CreateCommand : deleteCommand.Transaction = transaction
        insertCommand = connection.CreateCommand : insertCommand.Transaction = transaction

        Dim ODBCstring As String = Nothing
        Try
          'Delete any existing records
          With deleteCommand
            ODBCstring = ODBCDelete ' just so we get the sql statement if we throw an error
            .CommandText = ODBCstring
            .ExecuteNonQuery()
          End With

          'Insert new or modified records
          With insertCommand
            'Create the parameters we will use for the insert command
            AddODBCParameters(dt, .Parameters)

            'Insert all the datarows preserving autokey where it is set
            For Each dr As DataRow In dt.Rows
              If dr.RowState <> DataRowState.Deleted Then
                'Set values of all parameters from the data row
                For Each dc As DataColumn In dt.Columns
                  insertCommand.Parameters("@" & dc.ColumnName).Value = dr(dc.ColumnName)
                Next
                'Make the insert string this will be different if autokey is null (new record)
                If dr.IsNull("autokey") Then
                  BuildODBCInsertString(dt, ODBCstring)
                  ODBCstring &= "; SELECT CAST(scope_identity() AS int);"
                  .CommandText = ODBCstring
                  dr("autokey") = Integer.Parse(.ExecuteScalar().ToString)
                Else
                  MakeODBCInsertStringWithID(dt, ODBCstring)
                  ODBCstring = "SET IDENTITY_INSERT " & dt.TableName & " ON; " & ODBCstring & "; SET IDENTITY_INSERT " & dt.TableName & " OFF;"
                  .CommandText = ODBCstring
                  .ExecuteNonQuery()
                End If
              End If
            Next
          End With

          'Commit the changes 
          transaction.Commit()
          Return True
        Catch ex As Exception
          Utilities.Log.LogError(ex, ODBCstring)
        End Try
        transaction.Rollback()

      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function
#End If
    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Sub BuildODBCInsertString(ByVal dt As DataTable, ByRef commandText As String)
      'Make a Sql insert (using sql parameters) to insert a row into the database
      '  assumes autokey is an AutoIncrement field and will be filled in by the database
      Try
        If dt Is Nothing Then Exit Sub

        Dim columns As String = "", values As String = ""
        For Each dc As DataColumn In dt.Columns
          If dc.ColumnName <> "autokey" Then
            If columns.Length = 0 Then
              columns = "[" & dc.ColumnName.ToString & "]"
            Else
              columns &= ",[" & dc.ColumnName.ToString & "]"
            End If
            If values.Length = 0 Then
              values = "@" & dc.ColumnName.ToString
            Else
              values &= ",@" & dc.ColumnName.ToString
            End If
          End If
        Next
        commandText = "INSERT INTO " & dt.TableName & " (" & columns & ") VALUES(" & values & ")"
      Catch ex As Exception
        Debug.Print("BuildODBCInsertString: " & dt.TableName)
        Debug.Print(ex.Message)
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Sub MakeODBCInsertStringWithID(ByVal dt As DataTable, ByRef commandText As String)
      'Make a Sql insert (using sql parameters) to insert a row into the database
      '  assumes autokey is an AutoIncrement field but trys to insert the existing autokey value 
      '  this can be used to delete and re-insert records with the original autokey values
      Try
        If dt Is Nothing Then Exit Sub

        Dim columns As String = "", values As String = ""
        For Each dc As DataColumn In dt.Columns
          If columns.Length = 0 Then
            columns = "[" & dc.ColumnName.ToString & "]"
          Else
            columns &= ",[" & dc.ColumnName.ToString & "]"
          End If
          If values.Length = 0 Then
            values = "@" & dc.ColumnName.ToString
          Else
            values &= ",@" & dc.ColumnName.ToString
          End If
        Next
        commandText = "INSERT INTO " & dt.TableName & " (" & columns & ") VALUES(" & values & ")"
      Catch ex As Exception
        Debug.Print("MakeODBCInsertStringWithID: " & dt.TableName)
        Debug.Print(ex.Message)
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function ODBCInsert(ByVal connectionString As String, ByVal insertString As String) As Integer
      Try
        Dim rows As Integer
        Dim da As New Odbc.OdbcDataAdapter
        da.InsertCommand = New Odbc.OdbcCommand
        With da.InsertCommand
          .CommandText = insertString
          .Connection = New Odbc.OdbcConnection(connectionString)
          .Connection.Open() : rows = .ExecuteNonQuery() : .Connection.Close()
        End With

        Return rows
      Catch ex As Exception
        Debug.Print("ODBCInsert: " & insertString)
        Debug.Print(ex.Message)
        Utilities.Log.LogError(ex, insertString)
      End Try
      Return -1
    End Function
#End Region

#Region " Utilities "

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Sub AddODBCParameters(ByVal dt As DataTable, ByRef pc As Odbc.OdbcParameterCollection, ByVal dr As DataRow)
      Try
        If dt Is Nothing Then Exit Sub

        pc.Clear()
        For Each dc As DataColumn In dt.Columns
          If dc.ColumnName <> "AutoKey" Then
            Dim pname As String = "@" & dc.ColumnName
            'Create a parameter for every column in the table
            Dim ptype As Odbc.OdbcType
            If dc.DataType.Name = "Int16" Then ptype = Odbc.OdbcType.SmallInt
            If dc.DataType.Name = "Int32" Then ptype = Odbc.OdbcType.Int
            If dc.DataType.Name = "String" Then ptype = Odbc.OdbcType.NVarChar
            If dc.DataType.Name = "DateTime" Then ptype = Odbc.OdbcType.DateTime
            If dc.DataType.Name = "Double" Then ptype = Odbc.OdbcType.Double
            If dc.DataType.Name = "Decimal" Then ptype = Odbc.OdbcType.Decimal
            If dc.DataType.Name = "Single" Then ptype = Odbc.OdbcType.Real
            pc.Add(pname, ptype).Value = dr(dc.ColumnName)
          End If
        Next
        For Each dc As DataColumn In dt.Columns
          If dc.ColumnName = "AutoKey" Then
            Dim pname As String = "@" & dc.ColumnName
            'Create a parameter for every column in the table
            Dim ptype As Odbc.OdbcType
            If dc.DataType.Name = "Int16" Then ptype = Odbc.OdbcType.SmallInt
            If dc.DataType.Name = "Int32" Then ptype = Odbc.OdbcType.Int
            If dc.DataType.Name = "String" Then ptype = Odbc.OdbcType.NVarChar
            If dc.DataType.Name = "DateTime" Then ptype = Odbc.OdbcType.DateTime
            If dc.DataType.Name = "Double" Then ptype = Odbc.OdbcType.Double
            If dc.DataType.Name = "Decimal" Then ptype = Odbc.OdbcType.Decimal
            If dc.DataType.Name = "Single" Then ptype = Odbc.OdbcType.Real

            pc.Add(pname, ptype).Value = dr(dc.ColumnName)
            Exit For
          End If
        Next

      Catch ex As Exception
        Debug.Print("MakeODBCParameters: " & dt.TableName)
        Debug.Print(ex.Message)
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function ODBCString(ByVal value As String) As String
      Try
        If value.Length = 0 Then Return "'Null'"

        'Wrap single quotes round string or return "Null" if string is empty
        Dim returnString As String = ""

        'Look for single quotes (') and double up
        For i As Integer = 0 To value.Length - 1
          Select Case value.Substring(i, 1)
            Case "'" : returnString &= value.Substring(i, 1) & "'"
            Case Else : returnString &= value.Substring(i, 1)
          End Select
        Next i
        Return "'" & returnString & "'"
      Catch ex As Exception
        Return "Null"
      End Try
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function ODBCDateString(ByVal value As String) As String
      Try
        If value = Nothing Then
          Return "'Null'"
        Else
          Dim tryDate As Date
          If Date.TryParse(value, tryDate) Then
            Return ODBCDateString(tryDate)
          Else
            Return "'Null'"
          End If
        End If
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return "'Null'"
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function ODBCDateString(ByVal value As Date) As String
      Try
        If value = Nothing Then
          Return "'Null'"
        Else
          Return ODBCString(value.ToString(InvariantCulture))
        End If
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return "'Null'"
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function EmptyStringToNullODBC(ByVal dr As DataRow, ByVal column As String) As String
      Try
        If dr.IsNull(column) Then Return "'Null'"
        If dr(column).ToString.Length <= 0 Then Return "'Null'"
        Return ODBCString(dr(column).ToString)
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return "'Null'"
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function NullToNothingODBC(ByVal value As String) As String
      Try
        'Check to see if it's null
        If String.IsNullOrEmpty(value) Then Return Nothing

        'String isn't null so just return the string
        Return value
      Catch ex As Exception
        'No code
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function NullToZeroIntegerODBC(ByVal value As String) As Integer
      Try
        'Check to see if it's null
        If String.IsNullOrEmpty(value) Then Return 0

        'See if we can parse it
        Dim tryInteger As Integer
        If Integer.TryParse(value, tryInteger) Then Return tryInteger
      Catch ex As Exception
        'No code
      End Try
      Return 0
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function NullToZeroDoubleODBC(ByVal value As String) As Double
      Try
        'Check to see if it's null
        If String.IsNullOrEmpty(value) Then Return 0

        'See if we can parse it
        Dim tryDouble As Double
        If Double.TryParse(value, tryDouble) Then Return tryDouble
      Catch ex As Exception
        'No code
      End Try
      Return 0
    End Function

#End Region

  End Class
End Class