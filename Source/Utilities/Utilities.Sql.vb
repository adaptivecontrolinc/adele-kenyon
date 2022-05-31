Imports System.Data
Imports System.Globalization.CultureInfo

Namespace Utilities
  Public NotInheritable Class Sql

#Region " Select "

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function GetDataRow(connectionString As String, selectString As String) As DataRow
      ' Return a data row from the connection string and select string parameters
      '   return nothing if no row found, more than one row found or an exception is thrown
      Try
        Dim table As New DataTable With {.Locale = InvariantCulture}
        Dim adapter As New SqlClient.SqlDataAdapter

        adapter.SelectCommand = New SqlClient.SqlCommand
        With adapter.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : adapter.Fill(table) : .Connection.Close()
        End With

        If table.Rows.Count = 1 Then Return table.Rows(0)
      Catch ex As Exception
        Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function GetDataRow(connectionString As String, selectString As String, tableName As String) As DataRow
      ' Return a data row from the connection string and select string parameters and explicitly set the table name
      '   return nothing if no row found, more than one row found or an exception is thrown
      Try
        Dim table As New DataTable(tableName) With {.Locale = InvariantCulture}
        Dim adapter As New SqlClient.SqlDataAdapter

        adapter.SelectCommand = New SqlClient.SqlCommand
        With adapter.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : adapter.Fill(table) : .Connection.Close()
        End With

        If table.Rows.Count = 1 Then Return table.Rows(0)
      Catch ex As Exception
        Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function


    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function GetDataRowWithSchema(connectionString As String, selectString As String) As DataRow
      ' Return a data row from the connection string and select string parameters, apply source schema
      '   return nothing if no row found, more than one row found or an exception is thrown
      Try
        Dim table As New DataTable With {.Locale = InvariantCulture}
        Dim adapter As New SqlClient.SqlDataAdapter

        adapter.SelectCommand = New SqlClient.SqlCommand
        With adapter.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open()
          adapter.FillSchema(table, SchemaType.Source)
          adapter.Fill(table)
          .Connection.Close()
        End With

        If table.Rows.Count = 1 Then Return table.Rows(0)
      Catch ex As Exception
        Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function GetDataRowWithSchema(connectionString As String, selectString As String, tableName As String) As DataRow
      ' Return a data row from connection string and select string parameters and explicitly set the table name, apply source schema
      '   return nothing if no row found, more than one row found or an exception is thrown
      Try
        Dim table As New DataTable(tableName) With {.Locale = InvariantCulture}
        Dim adapter As New SqlClient.SqlDataAdapter

        adapter.SelectCommand = New SqlClient.SqlCommand
        With adapter.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open()
          adapter.FillSchema(table, SchemaType.Source)
          adapter.Fill(table)
          .Connection.Close()
        End With

        If table.Rows.Count = 1 Then Return table.Rows(0)
      Catch ex As Exception
        Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function


    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function GetDataTable(connectionString As String, selectString As String) As DataTable
      Try
        ' Return a populated data table from the connection string and select string parameters
        '   return nothing if no table found or an exception is thrown
        Dim table As New DataTable With {.Locale = InvariantCulture}
        Dim adapter As New SqlClient.SqlDataAdapter

        adapter.SelectCommand = New SqlClient.SqlCommand
        With adapter.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : adapter.Fill(table) : .Connection.Close()
        End With

        Return table
      Catch ex As Exception
        Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function GetDataTable(connectionString As String, selectString As String, tableName As String) As DataTable
      ' Return a data table from the connection string and select string parameters and explicitly set the tablename
      '   return nothing if no table found or an exception is thrown
      Try
        Dim table As New DataTable(tableName) With {.Locale = InvariantCulture}
        Dim adapter As New SqlClient.SqlDataAdapter

        adapter.SelectCommand = New SqlClient.SqlCommand
        With adapter.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : adapter.Fill(table) : .Connection.Close()
        End With

        Return table
      Catch ex As Exception
        Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function GetDataTableWithSchema(ByVal connectionString As String, ByVal selectString As String) As DataTable
      ' Return a data table from the connection string and select string parameters, apply the source schema
      '   return nothing if no table found or an exception is thrown
      Try
        Dim table As New DataTable With {.Locale = InvariantCulture}
        Dim adapter As New SqlClient.SqlDataAdapter

        adapter.SelectCommand = New SqlClient.SqlCommand
        With adapter.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open()
          adapter.FillSchema(table, SchemaType.Source)
          adapter.Fill(table)
          .Connection.Close()
        End With

        Return table
      Catch ex As Exception
        Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function GetDataTableWithSchema(connectionString As String, selectString As String, tableName As String) As DataTable
      ' Return a data table from the connection string and select string parameters and explicitly set the tablename, apply the source schema
      '   return nothing if no table found or an exception is thrown
      Try
        Dim table As New DataTable(tableName) With {.Locale = InvariantCulture}
        Dim adapter As New SqlClient.SqlDataAdapter

        adapter.SelectCommand = New SqlClient.SqlCommand
        With adapter.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open()
          adapter.FillSchema(table, SchemaType.Source)
          adapter.Fill(table)
          .Connection.Close()
        End With

        Return table
      Catch ex As Exception
        Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function


    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function GetDataTableEmpty(connectionString As String, tableName As String) As DataTable
      ' Return an empty data table from the connection string tablename, do NOT apply the source schema (column names / types only)
      '   return nothing if no table found or an exception is thrown
      Try
        Dim table As New DataTable(tableName) With {.Locale = InvariantCulture}
        Dim adapter As New SqlClient.SqlDataAdapter

        adapter.SelectCommand = New SqlClient.SqlCommand
        With adapter.SelectCommand
          .CommandText = "SELECT TOP 0 * FROM " & tableName
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : adapter.Fill(table) : .Connection.Close()
        End With

        Return table
      Catch ex As Exception
        Utilities.Log.LogError(ex, tableName)
      End Try
      Return Nothing
    End Function


    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function GetDataTableSchema(connectionString As String, tableName As String) As DataTable
      ' Return the source schema for the target table
      Try
        Dim table As New DataTable(tableName) With {.Locale = InvariantCulture}
        Dim adapter As New SqlClient.SqlDataAdapter

        adapter.SelectCommand = New SqlClient.SqlCommand
        With adapter.SelectCommand
          .CommandText = "SELECT * FROM " & tableName
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : adapter.FillSchema(table, SchemaType.Source) : .Connection.Close()
        End With

        Return table
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function GetDataTableSchema(connectionString As String, tableName As String, primaryKey As String) As DataTable
      Try
        ' Return the source schema for the target table and explicitly set the primary key
        Dim table As New DataTable(tableName) With {.Locale = InvariantCulture}
        Dim adapter As New SqlClient.SqlDataAdapter

        adapter.SelectCommand = New SqlClient.SqlCommand
        With adapter.SelectCommand
          .CommandText = "SELECT * FROM " & tableName
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : adapter.FillSchema(table, SchemaType.Source) : .Connection.Close()
        End With

        'Set primary key if it has been passed
        table.PrimaryKey = New DataColumn() {table.Columns(primaryKey)}

        ' Set autoincrement if primary key is "ID" -  a little bit naughty
        If primaryKey = "ID" Then table.Columns("ID").AutoIncrement = True

        Return table
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return Nothing
    End Function

    Public Shared Sub ResetDBAllowNull(row As DataRow)
      If row Is Nothing Then Exit Sub
      ResetDBAllowNull(row.Table)
    End Sub

    Public Shared Sub ResetDBAllowNull(table As DataTable)
      If table Is Nothing Then Exit Sub
      For Each column As DataColumn In table.Columns
        Try
          column.AllowDBNull = True
        Catch
        End Try
      Next
    End Sub

    Public Shared Sub ResetDBNullReadOnly(row As DataRow)
      If row Is Nothing Then Exit Sub
      ResetDBNullReadOnly(row.Table)
    End Sub

    Public Shared Sub ResetDBNullReadOnly(table As DataTable)
      If table Is Nothing Then Exit Sub
      For Each column As DataColumn In table.Columns
        Try
          column.AllowDBNull = True
          column.ReadOnly = False
        Catch
        End Try
      Next
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SqlSelect(connectionString As String, selectString As String) As DataTable
      Return GetDataTable(connectionString, selectString)
    End Function

#End Region

#Region " Insert "
    ' Always load data table with the table name if it is going to be used to insert 

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function InsertDataRow(ByVal connectionString As String, ByVal row As DataRow) As Boolean
      Return InsertDataRow(connectionString, row, "ID")
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function InsertDataRow(ByVal connectionString As String, ByVal row As DataRow, primaryKey As String) As Boolean
      Try
        'Must have a valid data row
        If row Is Nothing Then Return False
        Dim table As DataTable = row.Table

        Dim adapter As New SqlClient.SqlDataAdapter
        adapter.InsertCommand = New SqlClient.SqlCommand

        With adapter.InsertCommand
          'Add parameters and build insert string based on table columns
          AddSqlInsertParameters(table, .Parameters)
          BuildSqlInsertString(table, adapter, primaryKey)

          'Set values of all the parameters from the data row
          Dim parameterName As String
          For Each column As DataColumn In table.Columns
            parameterName = "@" & column.ColumnName
            If .Parameters.Contains(parameterName) Then
              .Parameters(parameterName).Value = row(column.ColumnName)
            End If
          Next

          ' FOR DEBUG - just so we can easily check the string
          Dim sql = .CommandText

          ' Check to see if we have an auto increment column
          Dim autoIncrementColumn As DataColumn = Nothing
          For Each column As DataColumn In table.Columns
            If column.AutoIncrement Then autoIncrementColumn = column
          Next

          If autoIncrementColumn Is Nothing Then
            .Connection = New SqlClient.SqlConnection(connectionString)
            .Connection.Open() : .ExecuteNonQuery() : .Connection.Close()
          Else
            Dim autoIncrementValue As Integer = -1
            .CommandText = .CommandText & "; SELECT CAST(scope_identity() AS int);"
            .Connection = New SqlClient.SqlConnection(connectionString)
            .Connection.Open() : autoIncrementValue = Integer.Parse(.ExecuteScalar().ToString) : .Connection.Close()

            ' Write the autoincrement value into the column 
            Dim autoIncrementRowValue = NullToZeroInteger(row(autoIncrementColumn))
            If autoIncrementRowValue <> autoIncrementValue Then
              ' Toggle readonly if set
              If autoIncrementColumn.ReadOnly Then
                autoIncrementColumn.ReadOnly = False
                Try
                  ' This can throw an error if the new db autoincrement value happens to be in the datatable
                  '   because a value is automatically set when a row is added...
                  row(autoIncrementColumn) = autoIncrementValue
                Catch ex As Exception
                  Utilities.Log.LogError(ex)  ' just to see how often this happens
                End Try
                autoIncrementColumn.ReadOnly = True
              Else
                row(autoIncrementColumn) = autoIncrementValue
              End If
            End If
          End If
        End With

        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    Public Shared Function InsertDataTable(ByVal connectionString As String, ByVal table As DataTable) As Boolean
      Return InsertDataTable(connectionString, table, "ID")
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function InsertDataTable(ByVal connectionString As String, ByVal table As DataTable, primaryKey As String) As Boolean
      Try
        'Must have a valid data table
        If table Is Nothing OrElse table.Rows.Count <= 0 Then Return False

        Dim adapter As New SqlClient.SqlDataAdapter
        adapter.InsertCommand = New SqlClient.SqlCommand

        With adapter.InsertCommand
          ' Setup connection just once
          .Connection = New SqlClient.SqlConnection(connectionString)

          'Add sql parameters and build the insert string based on table schema
          AddSqlInsertParameters(table, .Parameters)
          BuildSqlInsertString(table, adapter, primaryKey)

          ' See if we have an autoincrement column
          Dim autoIncrementColumn As DataColumn = Nothing
          For Each column As DataColumn In table.Columns
            If column.AutoIncrement Then autoIncrementColumn = column
          Next

          .Connection.Open()
          For Each row As DataRow In table.Rows
            Try
              'Set values of all the parameters from the data row
              Dim parameterName As String
              For Each column As DataColumn In table.Columns
                parameterName = "@" & column.ColumnName
                If .Parameters.Contains(parameterName) Then
                  .Parameters(parameterName).Value = row(column.ColumnName)
                End If
              Next

              ' FOR DEBUG - just so we can easily check the string
              Dim sql = .CommandText

              If autoIncrementColumn Is Nothing Then
                .ExecuteNonQuery()
              Else
                Dim autoIncrementValue As Integer = -1
                .CommandText = .CommandText & "; SELECT CAST(scope_identity() AS int);"
                autoIncrementValue = Integer.Parse(.ExecuteScalar().ToString)

                ' Write autoincrement value into the column if it has not been written
                Dim autoIncrementRowValue = NullToZeroInteger(row(autoIncrementColumn))
                If autoIncrementRowValue <> autoIncrementValue Then
                  ' Toggle readonly if set
                  If autoIncrementColumn.ReadOnly Then
                    autoIncrementColumn.ReadOnly = False
                    Try
                      ' This can throw an error if the new db autoincrement value happens to be in the datatable
                      '   because a value is automatically set when a row is added...
                      row(autoIncrementColumn) = autoIncrementValue
                    Catch ex As Exception
                      Utilities.Log.LogError(ex)  ' just to see how often this happens
                    End Try
                    autoIncrementColumn.ReadOnly = True
                  Else
                    row(autoIncrementColumn) = autoIncrementValue
                  End If
                End If
              End If

            Catch ex As Exception
              Utilities.Log.LogError(ex)
            End Try
          Next
          .Connection.Close()
        End With

        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    Public Shared Sub AddSqlInsertParameters(table As DataTable, sqlParameters As SqlClient.SqlParameterCollection)
      Try
        If table Is Nothing OrElse sqlParameters Is Nothing Then Exit Sub

        With sqlParameters
          .Clear()
          For Each column As DataColumn In table.Columns
            'Create a SqlParameter for every column in the table - exclude auto increment and read only columns
            If column.AutoIncrement = False AndAlso column.ReadOnly = False Then
              Dim parameter As New SqlClient.SqlParameter
              parameter.ParameterName = "@" & column.ColumnName
              parameter.SqlDbType = GetSqlDataType(column)
              .Add(parameter)
            End If
          Next
        End With
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Sub BuildSqlInsertString(ByVal table As DataTable, adapter As SqlClient.SqlDataAdapter, primaryKey As String)
      'Make a Sql insert string (using sql parameters) to insert a row into the database
      '  Don't try to insert a value for an AutoIncrement column
      Try
        With adapter.InsertCommand
          If table Is Nothing Then Exit Sub

          Dim sqlColumns As String = Nothing
          Dim sqlValues As String = Nothing
          For Each column As DataColumn In table.Columns
            ' Exclude auto increment and readonly columns
            If column.AutoIncrement = False AndAlso column.ReadOnly = False Then
              ' Just to be on the safe side
              If column.ColumnName <> primaryKey Then
                ' List of columns we will set values for
                If sqlColumns Is Nothing Then
                  sqlColumns = "[" & column.ColumnName.ToString & "]"
                Else
                  sqlColumns &= ",[" & column.ColumnName.ToString & "]"
                End If
                ' List of corresponding values to set the columns too (SqlParameters)
                If sqlValues Is Nothing Then
                  sqlValues = "@" & column.ColumnName.ToString
                Else
                  sqlValues &= ",@" & column.ColumnName.ToString
                End If
              End If
            End If
          Next

          .CommandText = "INSERT INTO " & table.TableName & " (" & sqlColumns & ") VALUES(" & sqlValues & ")"

        End With
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SqlInsert(ByVal connectionString As String, ByVal insertString As String) As Integer
      Try
        Dim rows As Integer
        Dim da As New SqlClient.SqlDataAdapter
        da.InsertCommand = New SqlClient.SqlCommand
        With da.InsertCommand
          .CommandText = insertString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : rows = .ExecuteNonQuery() : .Connection.Close()
        End With

        Return rows
      Catch ex As Exception
        Utilities.Log.LogError(ex, insertString)
      End Try
      Return -1
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SqlInsertReturnIdentity(ByVal connectionString As String, ByVal insertString As String) As Integer
      Try
        Dim identity As Integer
        Dim da As New SqlClient.SqlDataAdapter() With {.InsertCommand = New SqlClient.SqlCommand}
        With da.InsertCommand
          .CommandText = insertString & "; SELECT CAST(scope_identity() AS int);"
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : identity = Integer.Parse(.ExecuteScalar.ToString) : .Connection.Close()
        End With

        Return identity
      Catch ex As Exception
        Utilities.Log.LogError(ex, insertString)
      End Try
      Return -1
    End Function


#End Region

#Region " Update "
    ' Always load the data table with the table name if it is going to be used to update

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function UpdateDataRow(connectionString As String, row As DataRow) As Boolean
      Return UpdateDataRow(connectionString, row, "ID")
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function UpdateDataRow(connectionString As String, row As DataRow, primaryKey As String) As Boolean
      'Write changes in a data row back to the database
      '  return false if write fails or an exception is thrown
      Try
        'Make sure the data row is not empty
        If row Is Nothing Then Return False
        Dim table = row.Table

        Dim adapter As New SqlClient.SqlDataAdapter
        adapter.UpdateCommand = New SqlClient.SqlCommand

        With adapter.UpdateCommand
          'Add sql parameters and build the insert string based on table schema
          AddSqlUpdateParameters(table, .Parameters, primaryKey)
          BuildSqlUpdateString(table, adapter, primaryKey)

          'Set values of all the parameters from the data row
          Dim parameterName As String
          For Each column As DataColumn In table.Columns
            parameterName = "@" & column.ColumnName
            If .Parameters.Contains(parameterName) Then
              .Parameters(parameterName).Value = row(column.ColumnName)
            End If
          Next

          Dim test = .CommandText

          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : .ExecuteNonQuery() : .Connection.Close()
        End With

        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    '<System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function UpdateDataTable(connectionString As String, table As DataTable) As Boolean
      Return UpdateDataTable(connectionString, table, "ID")
    End Function

    '<System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function UpdateDataTable(connectionString As String, table As DataTable, primaryKey As String) As Boolean
      Try
        'Make sure the data table is not empty
        If table Is Nothing OrElse table.Rows.Count <= 0 Then Return False

        Dim adapter As New SqlClient.SqlDataAdapter
        adapter.UpdateCommand = New SqlClient.SqlCommand

        With adapter.UpdateCommand
          ' Setup connection just once
          .Connection = New SqlClient.SqlConnection(connectionString)

          'Add sql parameters and build the insert string based on table columns
          AddSqlUpdateParameters(table, .Parameters, primaryKey)
          BuildSqlUpdateString(table, adapter, primaryKey)

          .Connection.Open()
          For Each row As DataRow In table.Rows
            Try

              'Set values of all the parameters from the data row
              Dim parameterName As String
              For Each column As DataColumn In table.Columns
                parameterName = "@" & column.ColumnName
                If .Parameters.Contains(parameterName) Then
                  .Parameters(parameterName).Value = row(column.ColumnName)
                End If
              Next

              Dim test = .CommandText

              .ExecuteNonQuery()

            Catch ex As Exception
              Utilities.Log.LogError(ex)
            End Try
          Next
          .Connection.Close()
        End With

        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function


    Public Shared Sub AddSqlUpdateParameters(table As DataTable, sqlParameters As SqlClient.SqlParameterCollection, primaryKey As String)
      Try
        If table Is Nothing OrElse sqlParameters Is Nothing Then Exit Sub

        With sqlParameters
          .Clear()
          For Each column As DataColumn In table.Columns
            'Create a SqlParameter for every column in the table - exclude auto increment and read only columns
            If column.AutoIncrement = False AndAlso column.ReadOnly = False Then
              Dim parameter As New SqlClient.SqlParameter
              parameter.ParameterName = "@" & column.ColumnName
              parameter.SqlDbType = GetSqlDataType(column)
              .Add(parameter)
            End If
          Next

          ' Make sure the parameter collection includes the primary key
          '   If the primary key is auto increment it may have been excluded because it is readonly
          Dim parameterName = "@" & primaryKey
          If Not sqlParameters.Contains(parameterName) Then
            Dim column = table.Columns(primaryKey)
            Dim parameter As New SqlClient.SqlParameter
            parameter.ParameterName = parameterName
            parameter.SqlDbType = GetSqlDataType(column)
            sqlParameters.Add(parameter)
          End If
        End With

      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Sub BuildSqlUpdateString(table As DataTable, adapter As SqlClient.SqlDataAdapter, primaryKey As String)
      Try
        With adapter.UpdateCommand
          'Make sure we have a DataTable to work with
          If table Is Nothing Then Exit Sub

          'Make the sql column update string
          Dim sqlColumns As String = Nothing
          For Each column As DataColumn In table.Columns
            If column.AutoIncrement = False AndAlso column.ReadOnly = False Then
              If column.ColumnName <> primaryKey Then
                If sqlColumns Is Nothing Then
                  sqlColumns = "[" & column.ColumnName.ToString & "]=@" & column.ColumnName.ToString
                Else
                  sqlColumns &= ",[" & column.ColumnName.ToString & "]=@" & column.ColumnName.ToString
                End If
              End If
            End If
          Next

          ' Build the update command using SqlParameters 
          .CommandText = "UPDATE [" & table.TableName & "] SET " & sqlColumns & " WHERE [" & primaryKey & "]=@" & primaryKey

        End With
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SqlUpdate(connectionString As String, updateString As String) As Integer
      Try
        Dim rows As Integer
        Dim da As New SqlClient.SqlDataAdapter
        da.UpdateCommand = New SqlClient.SqlCommand
        With da.UpdateCommand
          .CommandText = updateString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : rows = .ExecuteNonQuery() : .Connection.Close()
        End With

        Return rows
      Catch ex As Exception
        Utilities.Log.LogError(ex, updateString)
      End Try
      Return -1
    End Function

#End Region

#Region " Delete "

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SqlDelete(ByVal connectionString As String, ByVal deleteString As String) As Integer
      Try
        Dim rows As Integer
        Dim da As New SqlClient.SqlDataAdapter
        da.DeleteCommand = New SqlClient.SqlCommand
        With da.DeleteCommand
          .CommandText = deleteString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : rows = .ExecuteNonQuery() : .Connection.Close()
        End With

        Return rows
      Catch ex As Exception
        Utilities.Log.LogError(ex, deleteString)
      End Try
      Return -1
    End Function

#End Region

#Region " Sql data type mapping  "

    Public Shared Function GetSqlDataType(column As DataColumn) As SqlDbType
      If column Is Nothing Then Return Nothing
      Return DataTypeMap(column.DataType)
    End Function

    Public Shared Function GetSqlDataType(dataType As Type) As SqlDbType
      If dataType Is Nothing Then Return Nothing
      Return DataTypeMap(dataType)
    End Function

    ' Map framework DataType to SqlDbType
    '   don't add Integer or Datetime they will throw a key error because they are the same as int32 / Date
    Public Shared DataTypeMap As New Dictionary(Of Type, SqlDbType) From {
      {GetType(Boolean), SqlDbType.Bit},
      {GetType(Byte), SqlDbType.TinyInt},
      {GetType(Int16), SqlDbType.SmallInt},
      {GetType(Int32), SqlDbType.Int},
      {GetType(Int64), SqlDbType.BigInt},
      {GetType(Single), SqlDbType.Float},
      {GetType(Double), SqlDbType.Float},
      {GetType(Decimal), SqlDbType.Decimal},
      {GetType(Date), SqlDbType.DateTime},
      {GetType(TimeSpan), SqlDbType.Time},
      {GetType(DateTimeOffset), SqlDbType.DateTimeOffset},
      {GetType(Byte()), SqlDbType.Image},
      {GetType(Char()), SqlDbType.NVarChar},
      {GetType(String), SqlDbType.NVarChar},
      {GetType(Guid), SqlDbType.UniqueIdentifier},
      {GetType(Object), SqlDbType.Variant}
    }

    ' Map SqlDbType to framework DataType  
    Private Shared SqlDbTypeMap As New Dictionary(Of SqlDbType, Type) From {
      {SqlDbType.BigInt, GetType(Int64)},
      {SqlDbType.Binary, GetType(Byte())},
      {SqlDbType.Bit, GetType(Boolean)},
      {SqlDbType.Char, GetType(String)},
      {SqlDbType.Date, GetType(DateTime)},
      {SqlDbType.DateTime, GetType(DateTime)},
      {SqlDbType.DateTime2, GetType(DateTime)},
      {SqlDbType.DateTimeOffset, GetType(DateTimeOffset)},
      {SqlDbType.Decimal, GetType(Decimal)},
      {SqlDbType.Float, GetType(Double)},
      {SqlDbType.Image, GetType(Byte())},
      {SqlDbType.Int, GetType(Int32)},
      {SqlDbType.Money, GetType(Decimal)},
      {SqlDbType.NChar, GetType(String)},
      {SqlDbType.NText, GetType(String)},
      {SqlDbType.NVarChar, GetType(String)},
      {SqlDbType.Real, GetType(String)},
      {SqlDbType.SmallDateTime, GetType(DateTime)},
      {SqlDbType.SmallInt, GetType(Int16)},
      {SqlDbType.SmallMoney, GetType(Decimal)},
      {SqlDbType.Text, GetType(String)},
      {SqlDbType.Time, GetType(TimeSpan)},
      {SqlDbType.Timestamp, GetType(Byte())},
      {SqlDbType.TinyInt, GetType(Byte)},
      {SqlDbType.UniqueIdentifier, GetType(Guid)},
      {SqlDbType.VarBinary, GetType(Byte())},
      {SqlDbType.VarChar, GetType(String)},
      {SqlDbType.Variant, GetType(Object)},
      {SqlDbType.Xml, GetType(String)}
    }

#End Region

#Region " Save Row / Table Changes "

    ' Save the table row changes back to the data base using the row state to determine whether to insert, update or delete
    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SaveDataTable(ByVal connectionString As String, ByVal table As System.Data.DataTable) As Boolean
      Dim primaryKey As String = "ID"
      Return SaveDataTable(connectionString, table, primaryKey)
    End Function

    ' Save the table row changes back to the data base using the row state to determine whether to insert, update or delete
    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SaveDataTable(ByVal connectionString As String, ByVal table As System.Data.DataTable, primaryKey As String) As Boolean
      Dim tableName As String = table.TableName
      Return SaveDataTable(connectionString, table, primaryKey, tableName)
    End Function

    '<System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SaveDataTable(ByVal connectionString As String, ByVal table As System.Data.DataTable, primaryKey As String, tableName As String) As Boolean
      If table Is Nothing Then Return False
      Try
        For Each row As System.Data.DataRow In table.Rows
          SaveDataRow(connectionString, row, primaryKey, tableName)
        Next
        table.AcceptChanges()
        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    ' Save the row changes back to the data base using the row state to determine whether to insert, update or delete
    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SaveDataRow(connectionString As String, row As System.Data.DataRow) As Boolean
      If row Is Nothing Then Return False
      Return SaveDataRow(connectionString, row, "ID", row.Table.TableName)
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SaveDataRow(connectionString As String, row As System.Data.DataRow, primaryKey As String) As Boolean
      If row Is Nothing Then Return False
      Return SaveDataRow(connectionString, row, primaryKey, row.Table.TableName)
    End Function

    '<System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SaveDataRow(connectionString As String, row As System.Data.DataRow, primaryKey As String, tableName As String) As Boolean
      If row Is Nothing Then Return False
      Try
        Select Case row.RowState
          Case DataRowState.Deleted
            Dim sql = "DELETE FROM " & tableName & " WHERE [" & primaryKey & "]=" & row(primaryKey, DataRowVersion.Original).ToString
            SqlDelete(connectionString, sql)

          Case DataRowState.Added
            InsertDataRow(connectionString, row)
            row.AcceptChanges()

          Case DataRowState.Detached
            InsertDataRow(connectionString, row)

          Case DataRowState.Modified
            UpdateDataRow(connectionString, row, primaryKey)
            row.AcceptChanges()
        End Select

        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    Public Shared Function TableHasChanges(table As DataTable) As Boolean
      If table Is Nothing Then Return False
      For Each row As DataRow In table.Rows
        If row.RowState <> DataRowState.Unchanged Then Return True
      Next
      Return False
    End Function

#End Region

#Region " Sync Table "

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SyncTableData(ByVal connectionString As String, ByVal dtNewData As System.Data.DataTable, ByVal sqlSelectOldData As String) As Boolean
      Try
        'Get the old data from the database
        Dim dtOldData As System.Data.DataTable = GetDataTable(connectionString, sqlSelectOldData, dtNewData.TableName)

        'Sync the new data
        Return SyncTableData(connectionString, dtNewData, dtOldData)
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SyncTableData(ByVal connectionString As String, ByVal dtNewData As System.Data.DataTable, ByVal dtOldData As System.Data.DataTable) As Boolean
      Try
        'Loop through the new data - insert any new records, update any existing records
        For Each dr As System.Data.DataRow In dtNewData.Rows
          If dr.RowState <> DataRowState.Deleted Then
            If dr.IsNull("ID") Then
              InsertDataRow(connectionString, dr)
            Else
              UpdateDataRow(connectionString, dr)
            End If
          End If
        Next

        'Finally loop through dtOldData and delete any rows that do not exist in dtNewData from the database
        Dim idOld As Integer, idNew As Integer
        For Each drOld As System.Data.DataRow In dtOldData.Rows
          If Integer.TryParse(drOld("ID").ToString, idOld) Then
            Dim deleteRow As Boolean = True
            For Each drNew As System.Data.DataRow In dtNewData.Rows
              If drNew.RowState <> DataRowState.Deleted Then
                If Integer.TryParse(drNew("ID").ToString, idNew) Then
                  'Row found - don't delete
                  If idOld = idNew Then
                    deleteRow = False
                    Exit For
                  End If
                End If
              End If
            Next
            If deleteRow Then
              Dim sql As String = "DELETE FROM " & dtOldData.TableName & " WHERE ID=" & drOld("ID").ToString
              SqlDelete(connectionString, sql)
            End If
          End If
        Next
        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function


#End Region

#Region " Copy "

    ' Clear existing table and copy new data in
    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function CopyTableData(sourceTable As System.Data.DataTable, targetTable As System.Data.DataTable) As Boolean
      Try
        ' Clear existing data
        targetTable.Clear()

        ' Copy all rows from source table to target table
        For Each row As System.Data.DataRow In sourceTable.Rows
          Try
            Dim newRow As System.Data.DataRow = targetTable.NewRow
            If CopyRowData(row, newRow) Then
              targetTable.Rows.Add(newRow)
            End If
          Catch ex As Exception
            'Don't let one error stop the copy
          End Try
        Next
        Return True
      Catch ex As Exception
        'Ignore errors
      End Try
      Return False
    End Function


    ' Clear existing table and copy new data in
    '<System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function CopyTableData(sourceTable As System.Data.DataTable, targetTable As System.Data.DataTable, skipColumn As String) As Boolean
      Try
        ' Clear existing data
        targetTable.Clear()

        ' Copy all rows from source table to target table
        For Each row As System.Data.DataRow In sourceTable.Rows
          Try
            Dim newRow As System.Data.DataRow = targetTable.NewRow
            If CopyRowData(row, newRow, skipColumn) Then
              targetTable.Rows.Add(newRow)
            End If
          Catch ex As Exception
            'Don't let one error stop the copy
          End Try
        Next
        Return True
      Catch ex As Exception
        'Ignore errors
      End Try
      Return False
    End Function

    ' Overwrite matching data from one table to another
    '  Add or clear rows if tables have a different number of rows
    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function OverwriteTableData(sourceTable As System.Data.DataTable, targetTable As System.Data.DataTable) As Boolean
      Try
        If sourceTable.Rows.Count >= targetTable.Rows.Count Then
          For rowIndex As Integer = 0 To sourceTable.Rows.Count - 1
            If rowIndex < targetTable.Rows.Count Then
              CopyRowData(sourceTable.Rows(rowIndex), targetTable.Rows(rowIndex))
            Else
              Dim newRow = targetTable.NewRow
              CopyRowData(sourceTable.Rows(rowIndex), newRow)
              targetTable.Rows.Add(newRow)
            End If
          Next
        Else
          For rowIndex As Integer = 0 To targetTable.Rows.Count - 1
            If rowIndex < sourceTable.Rows.Count Then
              CopyRowData(sourceTable.Rows(rowIndex), targetTable.Rows(rowIndex))
            Else
              For colIndex As Integer = 0 To targetTable.Columns.Count - 1
                targetTable.Rows(rowIndex)(colIndex) = DBNull.Value
              Next
            End If
          Next
        End If

        Return True
      Catch ex As Exception
        'Ignore errors
      End Try
      Return False
    End Function


    Public Shared Function CopyRow(sourceRow As DataRow) As DataRow
      If sourceRow Is Nothing Then Return Nothing
      Dim newRow = sourceRow.Table.NewRow
      CopyRowData(sourceRow, newRow)
      Return newRow
    End Function

    'Copy matching data from source to target (name and data type must match)
    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function CopyRowData(sourceRow As System.Data.DataRow, targetRow As System.Data.DataRow) As Boolean
      Try
        ' Clear existing data - these rows may not be from the same table
        For Each column As System.Data.DataColumn In targetRow.Table.Columns
          If column.AllowDBNull Then targetRow(column.ColumnName) = DBNull.Value
        Next

        ' Copy matching data from source row to target row
        For Each column As System.Data.DataColumn In sourceRow.Table.Columns
          Try
            Dim columnName As String = column.ColumnName
            If targetRow.Table.Columns.Contains(columnName) Then
              If column.DataType Is targetRow.Table.Columns(columnName).DataType Then
                targetRow(columnName) = sourceRow(columnName)
              End If
            End If
          Catch ex As Exception
            'Don't let one error stop the copy
          End Try
        Next
        Return True
      Catch ex As Exception
        'Ignore errors
      End Try
      Return False
    End Function

    'Copy matching data from source to target (name and data type must match)
    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function CopyRowData(sourceRow As System.Data.DataRow, targetRow As System.Data.DataRow, skipColumn As String) As Boolean
      Try
        ' Clear existing data - these rows may not be from the same table
        For Each targetColumn As System.Data.DataColumn In targetRow.Table.Columns
          If targetColumn.ColumnName.ToLower <> skipColumn.ToLower Then
            If targetColumn.AllowDBNull Then targetRow(targetColumn.ColumnName) = DBNull.Value
          End If
        Next

        ' Copy matching data from source row to target row
        For Each sourceColumn As System.Data.DataColumn In sourceRow.Table.Columns
          Try
            Dim columnName As String = sourceColumn.ColumnName
            If columnName.ToLower <> skipColumn.ToLower Then
              If targetRow.Table.Columns.Contains(columnName) Then
                Dim targetColumn = targetRow.Table.Columns(columnName)
                If sourceColumn.DataType Is targetColumn.DataType Then
                  targetRow(columnName) = sourceRow(columnName)
                End If
              End If
            End If
          Catch ex As Exception
            'Don't let one error stop the copy
          End Try
        Next
        Return True
      Catch ex As Exception
        'Ignore errors
      End Try
      Return False
    End Function
#End Region

#Region " Compare "

    Public Enum EDataCompare
      Same
      Different
      [Error]
    End Enum

    ' Compare two tables to see if the data and structure are the same
    '   Use this to see if data has been changed (take a copy of original table to compare)
    Public Shared Function CompareTables(table1 As DataTable, table2 As DataTable) As EDataCompare
      Return CompareTables(table1, table2, Nothing)
    End Function

    Public Shared Function CompareTables(table1 As DataTable, table2 As DataTable, skipColumns As String()) As EDataCompare
      Try
        ' Some easy ones
        If table1 Is Nothing AndAlso table2 Is Nothing Then Return EDataCompare.Same
        If table1 Is Nothing AndAlso table2 IsNot Nothing Then Return EDataCompare.Different
        If table2 Is Nothing AndAlso table1 IsNot Nothing Then Return EDataCompare.Different
        If table1.Columns.Count <> table2.Columns.Count Then Return EDataCompare.Different
        If table1.Rows.Count <> table2.Rows.Count Then Return EDataCompare.Different

        If table1 Is table2 Then Return EDataCompare.Same   'TODO return a specific enum for this case ??
        If table1.Rows.Count = 0 Then Return EDataCompare.Same

        'Compare column names and data types
        For i As Integer = 0 To table1.Columns.Count - 1
          If table1.Columns(i).ColumnName <> table2.Columns(i).ColumnName Then Return EDataCompare.Different
          If Not table1.Columns(i).DataType Is table2.Columns(i).DataType Then Return EDataCompare.Different
        Next

        'Compare each row
        For i As Integer = 0 To table1.Rows.Count - 1
          Dim result = CompareRows(table1.Rows(i), table2.Rows(i), skipColumns)
          If result <> EDataCompare.Same Then Return result
        Next

        ' Must be the same
        Return EDataCompare.Same
      Catch ex As Exception
        ' Ignore for now
      End Try
      Return EDataCompare.Error
    End Function

    ' Compare two rows to see if the data is the same
    '   Use this to see if data has been changed (take a copy of original row to compare)
    Public Shared Function CompareRows(row1 As DataRow, row2 As DataRow) As EDataCompare
      Return CompareRows(row1, row2, Nothing)
    End Function

    Public Shared Function CompareRows(row1 As DataRow, row2 As DataRow, skipColumns As String()) As EDataCompare
      Try
        For i As Integer = 0 To row1.Table.Columns.Count - 1
          ' Do not test columns in the array (usually LastModified etc.)
          If CheckColumn(row1.Table.Columns(i).ColumnName, skipColumns) Then
            If row1(i).ToString <> row2(i).ToString Then Return EDataCompare.Different
          End If
        Next
        ' Must be the same
        Return EDataCompare.Same
      Catch ex As Exception
        ' Ignore for now
      End Try
      Return EDataCompare.Error
    End Function

    Private Shared Function SkipColumn(columnName As String, skipColumns As String()) As Boolean
      Return ColumnInList(columnName, skipColumns)
    End Function

    Private Shared Function CheckColumn(columnName As String, skipColumns As String()) As Boolean
      Return Not ColumnInList(columnName, skipColumns)
    End Function

    Private Shared Function ColumnInList(columnName As String, ColumnList As String()) As Boolean
      If ColumnList IsNot Nothing Then
        For Each column In ColumnList
          If columnName.Equals(column, StringComparison.InvariantCultureIgnoreCase) Then Return True
        Next
      End If
      Return False
    End Function

    Shared Function HasChanges(table As DataTable) As Boolean
      If table Is Nothing OrElse table.Rows.Count <= 0 Then Return False
      For Each row As DataRow In table.Rows
        If row.RowState <> DataRowState.Unchanged Then Return True
      Next
      Return False
    End Function

    Shared Function HasChanges(row As DataRow) As Boolean
      If row Is Nothing Then Return False
      Return row.RowState <> DataRowState.Unchanged
    End Function

#End Region

#Region " Utilities "

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SqlExecute(ByVal connectionString As String, ByVal sqlString As String) As Boolean
      Try
        Dim da As New SqlClient.SqlDataAdapter
        da.UpdateCommand = New SqlClient.SqlCommand
        With da.UpdateCommand
          .CommandText = sqlString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : .ExecuteNonQuery() : .Connection.Close()
        End With

        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex, sqlString)
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SqlString(ByVal value As Object) As String
      If value Is Nothing Then Return "Null"
      Return SqlString(value.ToString)
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SqlString(ByVal value As String) As String
      Try
        If String.IsNullOrEmpty(value) Then Return "Null"

        'Wrap single quotes round string or return "Null" if string is empty
        Dim returnString As String = Nothing

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

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SqlDateString(ByVal value As String) As String
      Try
        If value = Nothing Then
          Return "Null"
        Else
          Dim tryDate As Date
          If Date.TryParse(value, tryDate) Then
            Return SqlDateString(tryDate)
          Else
            Return "Null"
          End If
        End If
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return "Null"
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SqlDateString(ByVal value As Date) As String
      Try
        If value = Nothing Then
          Return "Null"
        Else
          Return SqlString(value.ToString(InvariantCulture))
          'Return SqlString(value.ToString("O", InvariantCulture))
        End If
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return "Null"
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function EmptyStringToNull(ByVal dr As DataRow, ByVal column As String) As String
      Try
        If dr.IsNull(column) Then Return "Null"
        If dr(column).ToString.Length <= 0 Then Return "Null"
        Return SqlString(dr(column).ToString)
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return "Null"
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Sub EmptyStringToNull(ByRef dataCell As Object, ByVal text As String)
      'Set dataCell to text if there's actually something in text, else set it to Null
      '  Useful when updating data from a textbox and we want an empty text box to be saved as Null
      If text.Length > 0 Then
        dataCell = text
      Else
        dataCell = System.DBNull.Value
      End If
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function EmptyDateToNull([date] As Date) As Object
      If [date] = Nothing Then Return DBNull.Value
      Return [date]
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToNothing(ByVal value As String) As String
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

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToFalseBoolean(ByVal value As String) As Boolean
      Try
        'Check to see if it's null
        If String.IsNullOrEmpty(value) Then Return False

        'See if we can parse it
        Dim tryBoolean As Boolean
        If Boolean.TryParse(value, tryBoolean) Then Return tryBoolean
      Catch ex As Exception
        'No code
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToFalseBoolean(ByVal value As Object) As Boolean
      Try
        'Make sure we have something
        If value Is Nothing Then Return False

        'If this is a boolean then just return the value
        If TypeOf value Is Boolean Then Return DirectCast(value, Boolean)

        'If this is an integer return true if it is not equal to 0
        If TypeOf value Is Integer Then Return (DirectCast(value, Integer) <> 0)

        'See if we can parse it
        Dim tryBoolean As Boolean
        If Boolean.TryParse(value.ToString, tryBoolean) Then Return tryBoolean
      Catch ex As Exception
        'No code
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToZeroInteger(ByVal value As String) As Integer
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

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToZeroInteger(ByVal value As Object) As Integer
      Try
        'Make sure we have something
        If value Is Nothing Then Return 0

        'If this is an integer then just return the value
        If TypeOf value Is Integer Then Return DirectCast(value, Integer)

        'See if we can parse it
        Dim tryInteger As Integer
        If Integer.TryParse(value.ToString, tryInteger) Then Return tryInteger
      Catch ex As Exception
        'No code
      End Try
      Return 0
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToZeroDouble(ByVal value As String) As Double
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

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToZeroDouble(ByVal value As Object) As Double
      Try
        'Make sure we have something
        If value Is Nothing Then Return 0

        'If this is a double just return the value
        If TypeOf value Is Double Then Return DirectCast(value, Double)

        'See if we can parse it
        Dim tryDouble As Double
        If Double.TryParse(value.ToString, tryDouble) Then Return tryDouble
      Catch ex As Exception
        'No code
      End Try
      Return 0
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToNothingString(ByVal value As Object) As String
      Try
        'If this is a string return the string
        If TypeOf value Is String Then Return DirectCast(value, String)
      Catch ex As Exception
        'No code
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToNothingDate(ByVal value As Object) As Date
      Try
        'If this is not a date then return nothing
        If Not (TypeOf value Is Date) Then Return Nothing

        Return DirectCast(value, Date)
      Catch ex As Exception
        'No code
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToNothingDate(ByVal value As Object, convertToLocal As Boolean) As Date
      Try
        'If this is not a date then return nothing
        If Not (TypeOf value Is Date) Then Return Nothing

        Dim valueDate = DirectCast(value, Date)
        If convertToLocal Then
          Return valueDate.ToLocalTime
        Else
          Return valueDate
        End If
      Catch ex As Exception
        'No code
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToNothingDateString(ByVal value As Object) As String
      Try
        'If this is not a date then return nothing
        If Not (TypeOf value Is Date) Then Return Nothing

        Dim valueDate = DirectCast(value, Date)
        Return valueDate.ToString
      Catch ex As Exception
        'No code
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToNothingDateString(ByVal value As Object, convertToLocal As Boolean) As String
      Try
        'If this is not a date then return nothing
        If Not (TypeOf value Is Date) Then Return Nothing

        Dim valueDate = DirectCast(value, Date)
        If convertToLocal Then
          Return valueDate.ToLocalTime.ToString
        Else
          Return valueDate.ToString
        End If
      Catch ex As Exception
        'No code
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToNothingDateString(ByVal value As Object, convertToLocal As Boolean, dateFormat As String) As String
      Try
        'If this is not a date then return nothing
        If Not (TypeOf value Is Date) Then Return Nothing

        Dim valueDate = DirectCast(value, Date)
        If convertToLocal Then
          Return valueDate.ToLocalTime.ToString(dateFormat)
        Else
          Return valueDate.ToString(dateFormat)
        End If
      Catch ex As Exception
        'No code
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function TrueFalseToInteger(value As Boolean) As Integer
      If value = True Then Return -1
      Return 0
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function TrueFalseToInteger(value As Boolean, trueValue As Integer) As Integer
      If value = True Then Return trueValue
      Return 0
    End Function

    Public Shared Function GetInteger(row As DataRow, columnName As String) As Integer
      If row Is Nothing Then Return -1
      If Not row.Table.Columns.Contains(columnName) Then Return -1
      If row.IsNull(columnName) Then Return 0

      Dim value As Integer
      If Integer.TryParse(row(columnName).ToString, value) Then
        Return value
      Else
        Return -1
      End If
    End Function

    Public Shared Function GetDouble(row As DataRow, columnName As String) As Double
      If row Is Nothing Then Return -1
      If Not row.Table.Columns.Contains(columnName) Then Return -1
      If row.IsNull(columnName) Then Return 0

      Dim value As Double
      If Double.TryParse(row(columnName).ToString, value) Then
        Return value
      Else
        Return -1
      End If
    End Function

    Public Shared Function GetDouble(row As DataRow, columnName As String, digits As Integer) As Double
      If row Is Nothing Then Return -1
      If Not row.Table.Columns.Contains(columnName) Then Return -1
      If row.IsNull(columnName) Then Return 0

      Dim value As Double
      If Double.TryParse(row(columnName).ToString, value) Then
        Return Math.Round(value, digits)
      Else
        Return -1
      End If
    End Function

    Public Shared Function GetString(row As DataRow, columnName As String) As String
      If row Is Nothing Then Return Nothing
      If Not row.Table.Columns.Contains(columnName) Then Return Nothing
      If row.IsNull(columnName) Then Return Nothing

      Return row(columnName).ToString
    End Function

#End Region

#Region " Convert Dates "

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Sub ConvertToLocalTime(table As System.Data.DataTable)
      Try
        For Each row As System.Data.DataRow In table.Rows
          For Each column As System.Data.DataColumn In table.Columns
            If column.DataType Is GetType(Date) Then
              If Not row.IsNull(column) Then row(column) = DirectCast(row(column), Date).ToLocalTime
            End If
          Next
        Next
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Sub ConvertToLocalTime(row As System.Data.DataRow)
      Try
        For Each column As System.Data.DataColumn In row.Table.Columns
          If column.DataType Is GetType(Date) Then
            If Not row.IsNull(column) Then row(column) = DirectCast(row(column), Date).ToLocalTime
          End If
        Next
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Sub ConvertToLocalTime(table As System.Data.DataTable, columnName As String)
      Try
        Dim tryDate As Date
        For Each row As System.Data.DataRow In table.Rows
          If Date.TryParse(row(columnName).ToString, tryDate) Then
            If Not row.IsNull(columnName) Then row(columnName) = DirectCast(row(columnName), Date).ToLocalTime
          End If
        Next
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Sub ConvertToLocalTime(row As System.Data.DataRow, columnName As String)
      Try
        Dim tryDate As Date
        If Date.TryParse(row(columnName).ToString, tryDate) Then
          If Not row.IsNull(columnName) Then row(columnName) = DirectCast(row(columnName), Date).ToLocalTime
        End If
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Sub ConvertToUniversalTime(table As System.Data.DataTable)
      Try
        For Each row As System.Data.DataRow In table.Rows
          For Each column As System.Data.DataColumn In table.Columns
            If column.DataType Is GetType(Date) Then
              If Not row.IsNull(column) Then row(column) = DirectCast(row(column), Date).ToUniversalTime
            End If
          Next
        Next
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Sub ConvertToUniversalTime(row As System.Data.DataRow)
      Try
        For Each column As System.Data.DataColumn In row.Table.Columns
          If column.DataType Is GetType(Date) Then
            If Not row.IsNull(column) Then row(column) = DirectCast(row(column), Date).ToUniversalTime
          End If
        Next
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Sub ConvertToUniversalTime(table As System.Data.DataTable, columnName As String)
      Try
        Dim tryDate As Date
        For Each row As System.Data.DataRow In table.Rows
          If Date.TryParse(row(columnName).ToString, tryDate) Then
            If Not row.IsNull(columnName) Then row(columnName) = DirectCast(row(columnName), Date).ToUniversalTime
          End If
        Next
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Sub ConvertToUniversalTime(row As System.Data.DataRow, columnName As String)
      Try
        Dim tryDate As Date
        If Date.TryParse(row(columnName).ToString, tryDate) Then
          If Not row.IsNull(columnName) Then row(columnName) = DirectCast(row(columnName), Date).ToUniversalTime
        End If
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

#End Region

#Region " Table Configuration "

    Public Shared Function AddTable(connectionString As String, tableName As String, columnString As String) As Boolean
      Dim sql As String = Nothing
      Try
        sql = "CREATE TABLE " & tableName & "([ID] [int] IDENTITY(1,1) NOT NULL," & columnString & " CONSTRAINT [PK_" & tableName & "] PRIMARY KEY CLUSTERED ([ID] ASC))"
        Return SqlExecute(connectionString, sql)

      Catch ex As Exception
        Utilities.Log.LogError(ex, sql)
      End Try
      Return False
    End Function

    Public Shared Function UpdateTable(connectionString As String, tableName As String, columnString As String) As Boolean
      Dim sql As String = Nothing
      Try
        ' TODO - Add Code
      Catch ex As Exception
        Utilities.Log.LogError(ex, sql)
      End Try
      Return False
    End Function

    Public Shared Function TableExists(connectionString As String, tableName As String) As Boolean
      Dim sql As String = Nothing
      Try
        ' Get the catalog view with all the table names
        sql = "SELECT Name FROM sys.tables ORDER BY Name"
        Dim sysTables = GetDataTable(connectionString, sql)

        Return TableExists(tableName, sysTables)
      Catch ex As Exception
        Utilities.Log.LogError(ex, sql)
      End Try
      Return False
    End Function

    Public Shared Function TableExists(tableName As String, sysTables As System.Data.DataTable) As Boolean
      Try
        If sysTables Is Nothing Then Return False

        For Each row As System.Data.DataRow In sysTables.Rows
          If row("Name").ToString.Equals(tableName, StringComparison.OrdinalIgnoreCase) Then Return True
        Next
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    Public Shared Function AlterTableAddColumns(connectionString As String, tableName As String, columnSqlList As String) As Boolean
      Dim sql As String = Nothing
      Try
        ' Get the table structure
        Dim table = GetDataTableSchema(connectionString, tableName)

        ' Odd but just in case
        If table Is Nothing Then Return False

        ' Split the column list into an array 
        Dim columnSqlArray = columnSqlList.Split(",".ToCharArray, System.StringSplitOptions.RemoveEmptyEntries)

        ' Make sure we have some data to work with
        If columnSqlArray Is Nothing OrElse columnSqlArray.Length <= 0 Then Return False

        ' Loop through the list and check to see if any of the columns are already in the table before add the column to the
        '   alter table sql string
        For i = 0 To columnSqlArray.GetUpperBound(0)
          If Not ColumnExists(columnSqlArray(i), table) Then
            If sql Is Nothing Then
              sql = columnSqlArray(i)
            Else
              sql = sql & "," & columnSqlArray(i)
            End If
          End If
        Next

        ' Make sure we still have columns to add
        If sql Is Nothing Then Return False

        ' Execute the alter table statment
        sql = "ALTER TABLE " & tableName & " ADD " & sql
        Utilities.Sql.SqlExecute(connectionString, sql)
      Catch ex As Exception
        Utilities.Log.LogError(ex, sql)
      End Try
      Return False
    End Function

    Private Shared Function ColumnExists(columnSql As String, table As System.Data.DataTable) As Boolean
      Try
        ' Assumes "[]" around column name
        Dim endOfNameIndex = columnSql.IndexOf("]")
        Dim columnName = columnSql.Substring(1, endOfNameIndex - 1)  ' [1]

        For Each column As System.Data.DataColumn In table.Columns
          If column.ColumnName.Equals(columnName, StringComparison.OrdinalIgnoreCase) Then Return True
        Next
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

#End Region

#If 0 Then

#Region " Select "

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function GetDataRow(ByVal connectionString As String, ByVal selectString As String) As DataRow
      'Get a data row from the passed connection string and select statement
      '  return nothing if no row found, more than one row found or an exception is thrown
      Try
        Dim dt As New DataTable
        Dim da As New SqlClient.SqlDataAdapter

        da.SelectCommand = New SqlClient.SqlCommand
        With da.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : da.Fill(dt) : .Connection.Close()
          If dt.Rows.Count = 1 Then Return dt.Rows(0)
        End With

      Catch ex As Exception
        'Just return nothing if we get an error
        'Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function GetDataRow(ByVal connectionString As String, ByVal selectString As String, ByVal tableName As String) As DataRow
      'Get a data row from the passed connection string and select statement 
      '  set the table name so this row can be used with the update and insert functions here...
      '  return nothing if no row found, more than one row found or an exception is thrown
      Try
        Dim dt As New DataTable(tableName)
        Dim da As New SqlClient.SqlDataAdapter

        da.SelectCommand = New SqlClient.SqlCommand
        With da.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : da.Fill(dt) : .Connection.Close()
          If dt.Rows.Count = 1 Then Return dt.Rows(0)
        End With

      Catch ex As Exception
        'Just return nothing if we get an error
        'Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function GetDataRow(ByVal connectionString As String, ByVal tableName As String, ByVal id As Integer) As DataRow
      'Get a data row from the passed connection string table name and id
      '  set the table name so this row can be used with the update and insert functions here...
      '  return nothing if no row found, more than one row found or an exception is thrown
      Try
        Dim dt As New DataTable(tableName)
        Dim da As New SqlClient.SqlDataAdapter

        da.SelectCommand = New SqlClient.SqlCommand
        With da.SelectCommand
          .CommandText = "SELECT * FROM " & tableName & " WHERE ID=" & id.ToString(InvariantCulture)
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : da.Fill(dt) : .Connection.Close()
          If dt.Rows.Count = 1 Then Return dt.Rows(0)
        End With

      Catch Ex As Exception
        'Just return nothing if we get an error
        'Utilities.Log.LogError(Ex)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function GetDataTable(ByVal connectionString As String, ByVal selectString As String) As DataTable
      Try
        'Get a data table from the passed connection string and select string
        '  return nothing if no table found or an exception is thrown
        Dim dt As New DataTable
        Dim da As New SqlClient.SqlDataAdapter

        da.SelectCommand = New SqlClient.SqlCommand
        With da.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : da.Fill(dt) : .Connection.Close()
          Return dt
        End With

      Catch ex As Exception
        'Just return nothing if we get an error
        '    Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function GetDataTable(ByVal connectionString As String, ByVal selectString As String, ByVal tableName As String) As DataTable
      'Get a data table from the passed connection string and select string
      '  set the table name so this table can be used with the update and insert functions here...
      '  return nothing if no table found or an exception is thrown
      Try
        Dim dt As New DataTable(tableName)
        Dim da As New SqlClient.SqlDataAdapter

        da.SelectCommand = New SqlClient.SqlCommand
        With da.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : da.Fill(dt) : .Connection.Close()
          Return dt
        End With

      Catch ex As Exception
        'Just return nothing if we get an error
        Dim message As String = ex.Message
        'Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function GetDataTableSchema(ByVal connectionString As String, ByVal tableName As String) As DataTable
      'Get the Table Schema for the target table
      '  this will give the structure of table so we can use DataTable.NewRow
      '  assumes the table has an ID column that is AutoIncrement - not sure this has much effect
      '  return nothing if no table found or an exception is thrown
      Try
        Dim dt As New DataTable(tableName)
        Dim da As New SqlClient.SqlDataAdapter

        da.SelectCommand = New SqlClient.SqlCommand
        With da.SelectCommand
          .CommandText = "SELECT * FROM " & tableName
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : da.FillSchema(dt, SchemaType.Source) : .Connection.Close()
        End With

        'Set the AutoIncrement properties - this is not imported with the schema so we must manually set it
        dt.Columns("ID").AutoIncrement = True
        dt.Columns("ID").AutoIncrementSeed = 1
        dt.Columns("ID").AutoIncrementStep = 1

        Return dt
      Catch ex As Exception
        'Just return nothing if we get an error
        'Utilities.Log.LogError(ex)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function GetDataTableSchema(ByVal connectionString As String, ByVal tableName As String, ByVal primaryKey As String) As DataTable
      Try
        'Get the Schema for the target table
        '  this will give the structure of table so we can use DataTable.NewRow
        '  doing this here and passing a reference to reduce the number of calls to the database
        Dim dt As New DataTable(tableName)
        Dim da As New SqlClient.SqlDataAdapter

        da.SelectCommand = New SqlClient.SqlCommand
        With da.SelectCommand
          .CommandText = "SELECT * FROM " & tableName
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : da.FillSchema(dt, SchemaType.Source) : .Connection.Close()
        End With

        'Set primary key if it has been passed
        dt.PrimaryKey = New DataColumn() {dt.Columns(primaryKey)}

        Return dt
      Catch ex As Exception
        'Just return nothing if we get an error
        'Utilities.Log.LogError(ex)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function SqlSelect(ByVal connectionString As String, ByVal selectString As String) As DataTable
      Return GetDataTable(connectionString, selectString)
    End Function

#End Region

#Region " Update "

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function UpdateDataRow(ByVal connectionString As String, ByVal dr As DataRow) As Boolean
      'Write changes in a data row back to the database
      '  return false if write fails or an exception is thrown
      Try
        'Make sure the data row is not empty
        If dr Is Nothing Then Return False
        Dim dt As DataTable = dr.Table

        Dim da As New SqlClient.SqlDataAdapter
        da.UpdateCommand = New SqlClient.SqlCommand

        With da.UpdateCommand
          'Add parameters and build insert string based on table schema
          AddSqlParameters(dt, .Parameters)
          BuildSqlUpdateString(dt, .CommandText)

          'Set values of all parameters from the data row
          For Each dc As DataColumn In dt.Columns
            .Parameters("@" & dc.ColumnName).Value = dr(dc.ColumnName)
          Next

          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open()
          Dim rows As Integer = .ExecuteNonQuery()
          .Connection.Close()
        End With

        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Sub BuildSqlUpdateString(ByVal dt As DataTable, ByRef commandText As String)
      'Build a sql update string based on the table schema
      Dim sql As String = Nothing
      Try
        'Make sure we have a DataTable to work with
        If dt Is Nothing Then Exit Sub

        'Make the sql string
        For Each dc As DataColumn In dt.Columns
          If dc.ColumnName.ToLower <> "id" Then
            sql &= "[" & dc.ColumnName.ToString & "]=@" & dc.ColumnName.ToString & ","
          End If
        Next
        'remove the last comma
        sql = sql.Substring(0, sql.Length - 1)

        commandText = "UPDATE " & dt.TableName & " SET " & sql & " WHERE ID=@ID"
      Catch ex As Exception
        Utilities.Log.LogError(ex, sql)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function SqlUpdate(ByVal connectionString As String, ByVal updateString As String) As Integer
      Try
        Dim rows As Integer
        Dim da As New SqlClient.SqlDataAdapter
        da.UpdateCommand = New SqlClient.SqlCommand
        With da.UpdateCommand
          .CommandText = updateString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : rows = .ExecuteNonQuery() : .Connection.Close()
        End With

        Return rows
      Catch ex As Exception
        Utilities.Log.LogError(ex, updateString)
      End Try
      Return -1
    End Function

#End Region

#Region " Delete "

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function SqlDelete(ByVal connectionString As String, ByVal deleteString As String) As Integer
      Try
        Dim rows As Integer
        Dim da As New SqlClient.SqlDataAdapter
        da.DeleteCommand = New SqlClient.SqlCommand
        With da.DeleteCommand
          .CommandText = deleteString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : rows = .ExecuteNonQuery() : .Connection.Close()
        End With

        Return rows
      Catch ex As Exception
        Utilities.Log.LogError(ex, deleteString)
      End Try
      Return -1
    End Function

#End Region

#Region " Insert "

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function InsertDataRow(ByVal connectionString As String, ByVal dr As DataRow) As Boolean
      Try
        'Must have a valid data row
        If dr Is Nothing Then Return False

        Dim dt As DataTable = dr.Table
        Dim da As New SqlClient.SqlDataAdapter

        da.InsertCommand = New SqlClient.SqlCommand
        With da.InsertCommand

          'Add parameters and build insert string based on table schema
          AddSqlParameters(dt, .Parameters)
          BuildSqlInsertString(dt, .CommandText)

          'Set values of all parameters from the data row
          For Each dc As DataColumn In dt.Columns
            .Parameters("@" & dc.ColumnName).Value = dr(dc.ColumnName)
          Next

          Dim autoIncrement As Integer = -1
          .CommandText = .CommandText & "; SELECT CAST(scope_identity() AS int);"
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : autoIncrement = Integer.Parse(.ExecuteScalar().ToString) : .Connection.Close()
          dr("ID") = autoIncrement
        End With

        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function InsertDataRowWithID(ByVal connectionString As String, ByVal dr As DataRow) As Boolean
      Try
        'Must have a valid data row
        If dr Is Nothing Then Return False

        Dim dt As DataTable = dr.Table
        Dim da As New SqlClient.SqlDataAdapter

        da.InsertCommand = New SqlClient.SqlCommand
        With da.InsertCommand

          'Add parameters and build insert string based on table schema
          AddSqlParameters(dt, .Parameters)
          BuildSqlInsertStringWithID(dt, .CommandText)

          'Set values of all parameters from the data row
          For Each dc As DataColumn In dt.Columns
            .Parameters("@" & dc.ColumnName).Value = dr(dc.ColumnName)
          Next

          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : .ExecuteNonQuery() : .Connection.Close()
        End With

        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function InsertDataTable(ByVal connectionString As String, ByVal dt As DataTable) As Boolean
      Try
        'Must have a valid data table
        If dt Is Nothing Then Return False

        Dim rows As Integer
        Dim da As New SqlClient.SqlDataAdapter
        With da
          .InsertCommand = New SqlClient.SqlCommand

          With .InsertCommand
            AddSqlParameters(dt, .Parameters)
            BuildSqlInsertString(dt, .CommandText)

            .Connection = New SqlClient.SqlConnection(connectionString)
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
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function InsertDataTable(ByVal connectionString As String, ByVal dt As DataTable, ByVal sqlDelete As String) As Boolean
      'Insert a datatable into the database - this is used to update detail tables using a transactiion
      '  we start the transaction, delete the original records then insert the new records
      '  the transaction will be rolled back if any errors occur
      'Create the connection and declare the command and transaction objects
      Try
        Dim connection As New SqlClient.SqlConnection(connectionString)
        Dim deleteCommand As SqlClient.SqlCommand
        Dim insertCommand As SqlClient.SqlCommand
        Dim transaction As SqlClient.SqlTransaction

        'Open the connection and make the command and transaction objects on this connection
        connection.Open()
        transaction = connection.BeginTransaction
        deleteCommand = connection.CreateCommand : deleteCommand.Transaction = transaction
        insertCommand = connection.CreateCommand : insertCommand.Transaction = transaction

        Dim sql As String = Nothing
        Try
          'Delete any existing records
          With deleteCommand
            sql = sqlDelete ' just so we get the sql statement if we throw an error
            .CommandText = sql
            .ExecuteNonQuery()
          End With

          'Insert new or modified records
          With insertCommand
            'Create the parameters we will use for the insert command
            AddSqlParameters(dt, .Parameters)

            'Insert all the datarows preserving ID where it is set
            For Each dr As DataRow In dt.Rows
              If dr.RowState <> DataRowState.Deleted Then
                'Set values of all parameters from the data row
                For Each dc As DataColumn In dt.Columns
                  insertCommand.Parameters("@" & dc.ColumnName).Value = dr(dc.ColumnName)
                Next
                'Make the insert string this will be different if ID is null (new record)
                If dr.IsNull("ID") Then
                  BuildSqlInsertString(dt, sql)
                  sql &= "; SELECT CAST(scope_identity() AS int);"
                  .CommandText = sql
                  dr("ID") = Integer.Parse(.ExecuteScalar().ToString)
                Else
                  BuildSqlInsertStringWithID(dt, sql)
                  sql = "SET IDENTITY_INSERT " & dt.TableName & " ON; " & sql & "; SET IDENTITY_INSERT " & dt.TableName & " OFF;"
                  .CommandText = sql
                  .ExecuteNonQuery()
                End If
              End If
            Next
          End With

          'Commit the changes 
          transaction.Commit()
          Return True
        Catch ex As Exception
          Utilities.Log.LogError(ex, sql)
        End Try
        transaction.Rollback()

      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Sub BuildSqlInsertString(ByVal dt As DataTable, ByRef commandText As String)
      'Make a Sql insert (using sql parameters) to insert a row into the database
      '  assumes ID is an AutoIncrement field and will be filled in by the database
      Try
        If dt Is Nothing Then Exit Sub

        Dim columns As String = "", values As String = ""
        For Each dc As DataColumn In dt.Columns
          If dc.ColumnName <> "ID" Then
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
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Sub BuildSqlInsertStringWithID(ByVal dt As DataTable, ByRef commandText As String)
      'Make a Sql insert (using sql parameters) to insert a row into the database
      '  assumes ID is an AutoIncrement field but trys to insert the existing ID value 
      '  this can be used to delete and re-insert records with the original ID values
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
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function SqlInsert(ByVal connectionString As String, ByVal insertString As String) As Integer
      Try
        Dim rows As Integer
        Dim da As New SqlClient.SqlDataAdapter
        da.InsertCommand = New SqlClient.SqlCommand
        With da.InsertCommand
          .CommandText = insertString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : rows = .ExecuteNonQuery() : .Connection.Close()
        End With

        Return rows
      Catch ex As Exception
        Utilities.Log.LogError(ex, insertString)
      End Try
      Return -1
    End Function
#End Region

#Region " Copy "

    ' Copy matching data from one table to another
    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function CopyTableData(sourceTable As System.Data.DataTable, targetTable As System.Data.DataTable) As Boolean
      Try
        'Clear existing data
        targetTable.Clear()

        For Each row As System.Data.DataRow In sourceTable.Rows
          Try
            Dim newRow As System.Data.DataRow = targetTable.NewRow
            If Utilities.Sql.CopyRowData(row, newRow) Then
              targetTable.Rows.Add(newRow)
            End If
          Catch ex As Exception
            'Don't let one error stop the copy
          End Try
        Next
        Return True
      Catch ex As Exception
        'Ignore errors
      End Try
      Return False
    End Function

    'Copy matching data from source to target (name and data type must match)
    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function CopyRowData(sourceRow As System.Data.DataRow, targetRow As System.Data.DataRow) As Boolean
      Try
        For Each column As System.Data.DataColumn In sourceRow.Table.Columns
          Try
            Dim columnName As String = column.ColumnName
            If targetRow.Table.Columns.Contains(columnName) Then
              If column.DataType Is targetRow.Table.Columns(columnName).DataType Then
                targetRow(columnName) = sourceRow(columnName)
              End If
            End If
          Catch ex As Exception
            'Don't let one error stop the copy
          End Try
        Next
        Return True
      Catch ex As Exception
        'Ignore errors
      End Try
      Return False
    End Function

#End Region

#Region " Utilities "

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Sub AddSqlParameters(ByVal dt As DataTable, ByRef pc As SqlClient.SqlParameterCollection)
      Try
        If dt Is Nothing Then Exit Sub

        pc.Clear()
        For Each dc As DataColumn In dt.Columns
          'Create a parameter for every column in the table
          Dim p As New SqlClient.SqlParameter()
          p.ParameterName = "@" & dc.ColumnName
          If dc.DataType.Name = "Int16" Then p.SqlDbType = SqlDbType.SmallInt
          If dc.DataType.Name = "Int32" Then p.SqlDbType = SqlDbType.Int
          If dc.DataType.Name = "String" Then p.SqlDbType = SqlDbType.NVarChar
          If dc.DataType.Name = "DateTime" Then p.SqlDbType = SqlDbType.DateTime
          If dc.DataType.Name = "Double" Then p.SqlDbType = SqlDbType.Float
          If dc.DataType.Name = "Decimal" Then p.SqlDbType = SqlDbType.Decimal
          pc.Add(p)
        Next
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function SqlExecute(ByVal connectionString As String, ByVal sqlString As String) As Boolean
      Try
        Dim da As New SqlClient.SqlDataAdapter
        da.UpdateCommand = New SqlClient.SqlCommand
        With da.UpdateCommand
          .CommandText = sqlString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : .ExecuteNonQuery() : .Connection.Close()
        End With

        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex, sqlString)
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function TableExists(connectionString As String, tableName As String) As Boolean
      Dim sql As String
      Try
        sql = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=" & SqlString(tableName)
        Dim dr As System.Data.DataTable = SqlSelect(connectionString, sql)

        If dr IsNot Nothing AndAlso dr.Rows.Count > 0 Then Return True
      Catch ex As Exception
        'Ignore errors 
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function SqlString(ByVal value As Object) As String
      Try
        'If this is an empty string just return null
        If value Is Nothing Then Return "Null"

        'TODO check for null
        Return SqlString(value.ToString)

      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return "Null"
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function SqlString(ByVal value As String) As String
      Dim returnString As String = Nothing
      Try
        'If this is an empty string just return null
        If value Is Nothing OrElse value.Length = 0 Then Return "Null"

        'Look for single quotes (') and double up
        For i As Integer = 0 To value.Length - 1
          Select Case value.Substring(i, 1)
            Case "'" : returnString &= value.Substring(i, 1) & "'"
            Case Else : returnString &= value.Substring(i, 1)
          End Select
        Next i
        Return "'" & returnString & "'"
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return "Null"
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function SqlDateString(ByVal value As String) As String
      Try
        If value = Nothing OrElse value.Length <= 0 Then
          Return "Null"
        Else
          Dim valueToDate As Date
          If Date.TryParse(value, valueToDate) Then
            Return SqlDateString(valueToDate)
          Else
            Return "Null"
          End If
        End If
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return "Null"
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function SqlDateString(ByVal value As Date) As String
      Try
        If value = Nothing Then
          Return "Null"
        Else
          Return SqlString(value.ToString(InvariantCulture))
        End If
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return "Null"
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function EmptyStringToNull(ByVal dr As DataRow, ByVal column As String) As String
      Try
        If dr.IsNull(column) Then Return "Null"
        If dr(column).ToString.Length <= 0 Then Return "Null"
        Return SqlString(dr(column).ToString)
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return "Null"
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function NullToNothingString(ByVal value As String) As String
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
    Public Shared Function NullToNothingString(ByVal value As Object) As String
      Try
        'Check to see if it's a string
        If TypeOf value Is String Then Return DirectCast(value, String)

        'If not return nothing
        Return Nothing
      Catch ex As Exception
        'No code
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function NullToZeroInteger(ByVal value As String) As Integer
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
    Public Shared Function NullToZeroInteger(ByVal value As Object) As Integer
      Try
        'Check to see if it's an integer
        If TypeOf value Is Integer Then Return DirectCast(value, Integer)

        'Check to see if it's a small integer
        If TypeOf value Is Short Then Return DirectCast(value, Short)

        'If not return 0
        Return 0
      Catch ex As Exception
        'No code
      End Try
      Return 0
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function NullToZeroDouble(ByVal value As String) As Double
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

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function NullToZeroDouble(ByVal value As Object) As Double
      Try
        'Check to see if it's a double
        If TypeOf value Is Double Then Return DirectCast(value, Double)

        'If not return 0
        Return 0
      Catch ex As Exception
        'No code
      End Try
      Return 0
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function NullToNothingDate(ByVal value As String) As Date
      Try
        'Check to see if it's null
        If String.IsNullOrEmpty(value) Then Return Nothing

        'See if we can parse it
        Dim tryDate As Date
        If Date.TryParse(value, tryDate) Then Return tryDate
      Catch ex As Exception
        'No code
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function NullToNothingDate(ByVal value As Object) As Date
      Try
        'Check to see if it's a date
        If TypeOf value Is Date Then Return DirectCast(value, Date)

        'If it's not a date return nothing
        Return Nothing
      Catch ex As Exception
        'No code
      End Try
      Return Nothing
    End Function

    '<System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Function DateToLocalTimeString(ByVal value As Object) As String
      Try
        'Check to see if it's empty
        If value Is Nothing Then Return Nothing

        'Check to see if it's a date
        If TypeOf value Is Date Then Return DirectCast(value, Date).ToLocalTime.ToString

        'If it's not a date return nothing
        Return Nothing
      Catch ex As Exception
        'No code
      End Try
      Return Nothing
    End Function

    '<System.Diagnostics.DebuggerStepThrough()> _
    Public Shared Sub ConvertDateTimeToLocalTime(ByVal table As System.Data.DataTable)
      Dim columnName As String
      Try
        'Make sure we have some data to work with
        If table Is Nothing OrElse table.Rows.Count <= 0 Then Exit Sub

        'Loop through the columns and convert each DateTime column to local time
        For Each column As Data.DataColumn In table.Columns
          If column.DataType Is System.Type.GetType("System.DateTime") Then
            columnName = column.ColumnName
            'Loop through the rows and convert all non-null date columns to local time
            For Each row As System.Data.DataRow In table.Rows
              If Not row.IsNull(columnName) Then
                row(columnName) = DirectCast(row(columnName), Date).ToLocalTime
              End If
            Next
          End If
        Next
      Catch ex As Exception
        'Ignore errors
      End Try
    End Sub

    Public Shared Sub ConvertDateTimeToLocalTime(ByVal row As System.Data.DataRow)
      Dim columnName As String
      Try
        'Get the table associated with this datarow
        Dim table As System.Data.DataTable = row.Table

        'Make sure we have some data to work with
        If table Is Nothing OrElse row Is Nothing Then Exit Sub

        'Loop through the columns and convert each DateTime column to local time
        For Each column As Data.DataColumn In table.Columns
          If column.DataType Is System.Type.GetType("System.DateTime") Then
            columnName = column.ColumnName
            'Only convert non-null values
            If Not row.IsNull(columnName) Then
              row(columnName) = DirectCast(row(columnName), Date).ToLocalTime
            End If
          End If
        Next
      Catch ex As Exception
        'Ignore errors
      End Try
    End Sub

    Public Shared Sub ConvertDateTimeToUniversalTime(ByVal table As System.Data.DataTable)
      Dim columnName As String
      Try
        'Make sure we have some data to work with
        If table Is Nothing OrElse table.Rows.Count <= 0 Then Exit Sub

        'Loop through the columns and convert each date column to local time
        For Each column As Data.DataColumn In table.Columns
          If column.DataType Is System.Type.GetType("System.DateTime") Then
            columnName = column.ColumnName
            'Loop through the rows and convert all non-null date columns to local time
            For Each row As System.Data.DataRow In table.Rows
              If Not row.IsNull(columnName) Then
                row(columnName) = DirectCast(row(columnName), Date).ToUniversalTime
              End If
            Next
          End If
        Next
      Catch ex As Exception
        'Ignore errors
      End Try
    End Sub

    Public Shared Sub ConvertDateTimeToUniversalTime(ByVal row As System.Data.DataRow)
      Dim columnName As String
      Try
        'Get the table associated with this datarow
        Dim table As System.Data.DataTable = row.Table

        'Make sure we have some data to work with
        If table Is Nothing OrElse row Is Nothing Then Exit Sub

        'Loop through the columns and convert each DateTime column to local time
        For Each column As Data.DataColumn In table.Columns
          If column.DataType Is System.Type.GetType("System.DateTime") Then
            columnName = column.ColumnName
            'Only convert non-null values
            If Not row.IsNull(columnName) Then
              row(columnName) = DirectCast(row(columnName), Date).ToUniversalTime
            End If
          End If
        Next
      Catch ex As Exception
        'Ignore errors
      End Try
    End Sub


    ' Compare the data in two data tables - return true if the data is the same 
    '   assumes tables have the same sort order
    Public Shared Function CompareDataTables(table1 As System.Data.DataTable, table2 As System.Data.DataTable) As Boolean
      Try
        'Make sure we have data to work with
        If table1 Is Nothing OrElse table2 Is Nothing Then Return False

        'Make sure the tables have the same number of rows
        If table1.Rows.Count <> table2.Rows.Count Then Return False

        'Check all rows
        For i = 0 To table1.Rows.Count - 1
          If Not CompareDataRow(table1.Rows(i), table2.Rows(i)) Then Return False
        Next

        'If we get this far everything must be the same
        Return True
      Catch ex As Exception
        'Ignore errors
      End Try
      Return False
    End Function

    Public Shared Function CompareDataRow(row1 As System.Data.DataRow, row2 As System.Data.DataRow) As Boolean
      Try
        'Make sure we have data 
        If row1 Is Nothing OrElse row2 Is Nothing Then Return False

        'Make sure the rows have the same number of columns
        If row1.Table.Columns.Count <> row2.Table.Columns.Count Then Return False

        'Check each column
        For i = 0 To row1.Table.Columns.Count - 1
          If row1.Item(i).ToString <> row2.Item(i).ToString Then Return False
        Next

        'If we get this far everything must be the same
        Return True
      Catch ex As Exception
        'Ignore errors
      End Try
      Return False
    End Function

#End Region



    
  Public NotInheritable Class Sql

#Region " Select "

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function GetDataRow(connectionString As String, selectString As String) As DataRow
      ' Return a data row from the connection string and select string parameters
      '   return nothing if no row found, more than one row found or an exception is thrown
      Try
        Dim table As New DataTable With {.Locale = InvariantCulture}
        Dim adapter As New SqlClient.SqlDataAdapter

        adapter.SelectCommand = New SqlClient.SqlCommand
        With adapter.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : adapter.Fill(table) : .Connection.Close()
        End With

        If table.Rows.Count = 1 Then Return table.Rows(0)
      Catch ex As Exception
        Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function GetDataRow(connectionString As String, selectString As String, tableName As String) As DataRow
      ' Return a data row from the connection string and select string parameters and explicitly set the table name
      '   return nothing if no row found, more than one row found or an exception is thrown
      Try
        Dim table As New DataTable(tableName) With {.Locale = InvariantCulture}
        Dim adapter As New SqlClient.SqlDataAdapter

        adapter.SelectCommand = New SqlClient.SqlCommand
        With adapter.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : adapter.Fill(table) : .Connection.Close()
        End With

        If table.Rows.Count = 1 Then Return table.Rows(0)
      Catch ex As Exception
        Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function


    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function GetDataRowWithSchema(connectionString As String, selectString As String) As DataRow
      ' Return a data row from the connection string and select string parameters, apply source schema
      '   return nothing if no row found, more than one row found or an exception is thrown
      Try
        Dim table As New DataTable With {.Locale = InvariantCulture}
        Dim adapter As New SqlClient.SqlDataAdapter

        adapter.SelectCommand = New SqlClient.SqlCommand
        With adapter.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open()
          adapter.FillSchema(table, SchemaType.Source)
          adapter.Fill(table)
          .Connection.Close()
        End With

        If table.Rows.Count = 1 Then Return table.Rows(0)
      Catch ex As Exception
        Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function GetDataRowWithSchema(connectionString As String, selectString As String, tableName As String) As DataRow
      ' Return a data row from connection string and select string parameters and explicitly set the table name, apply source schema
      '   return nothing if no row found, more than one row found or an exception is thrown
      Try
        Dim table As New DataTable(tableName) With {.Locale = InvariantCulture}
        Dim adapter As New SqlClient.SqlDataAdapter

        adapter.SelectCommand = New SqlClient.SqlCommand
        With adapter.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open()
          adapter.FillSchema(table, SchemaType.Source)
          adapter.Fill(table)
          .Connection.Close()
        End With

        If table.Rows.Count = 1 Then Return table.Rows(0)
      Catch ex As Exception
        Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function


    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function GetDataTable(connectionString As String, selectString As String) As DataTable
      Try
        ' Return a populated data table from the connection string and select string parameters
        '   return nothing if no table found or an exception is thrown
        Dim table As New DataTable With {.Locale = InvariantCulture}
        Dim adapter As New SqlClient.SqlDataAdapter

        adapter.SelectCommand = New SqlClient.SqlCommand
        With adapter.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : adapter.Fill(table) : .Connection.Close()
        End With

        Return table
      Catch ex As Exception
        Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function GetDataTable(connectionString As String, selectString As String, tableName As String) As DataTable
      ' Return a data table from the connection string and select string parameters and explicitly set the tablename
      '   return nothing if no table found or an exception is thrown
      Try
        Dim table As New DataTable(tableName) With {.Locale = InvariantCulture}
        Dim adapter As New SqlClient.SqlDataAdapter

        adapter.SelectCommand = New SqlClient.SqlCommand
        With adapter.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : adapter.Fill(table) : .Connection.Close()
        End With

        Return table
      Catch ex As Exception
        Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function GetDataTableWithSchema(ByVal connectionString As String, ByVal selectString As String) As DataTable
      ' Return a data table from the connection string and select string parameters, apply the source schema
      '   return nothing if no table found or an exception is thrown
      Try
        Dim table As New DataTable With {.Locale = InvariantCulture}
        Dim adapter As New SqlClient.SqlDataAdapter

        adapter.SelectCommand = New SqlClient.SqlCommand
        With adapter.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open()
          adapter.FillSchema(table, SchemaType.Source)
          adapter.Fill(table)
          .Connection.Close()
        End With

        Return table
      Catch ex As Exception
        Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function GetDataTableWithSchema(connectionString As String, selectString As String, tableName As String) As DataTable
      ' Return a data table from the connection string and select string parameters and explicitly set the tablename, apply the source schema
      '   return nothing if no table found or an exception is thrown
      Try
        Dim table As New DataTable(tableName) With {.Locale = InvariantCulture}
        Dim adapter As New SqlClient.SqlDataAdapter

        adapter.SelectCommand = New SqlClient.SqlCommand
        With adapter.SelectCommand
          .CommandText = selectString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open()
          adapter.FillSchema(table, SchemaType.Source)
          adapter.Fill(table)
          .Connection.Close()
        End With

        Return table
      Catch ex As Exception
        Utilities.Log.LogError(ex, selectString)
      End Try
      Return Nothing
    End Function


    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function GetDataTableEmpty(connectionString As String, tableName As String) As DataTable
      ' Return an empty data table from the connection string tablename, do NOT apply the source schema (column names / types only)
      '   return nothing if no table found or an exception is thrown
      Try
        Dim table As New DataTable(tableName) With {.Locale = InvariantCulture}
        Dim adapter As New SqlClient.SqlDataAdapter

        adapter.SelectCommand = New SqlClient.SqlCommand
        With adapter.SelectCommand
          .CommandText = "SELECT TOP 0 * FROM " & tableName
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : adapter.Fill(table) : .Connection.Close()
        End With

        Return table
      Catch ex As Exception
        Utilities.Log.LogError(ex, tableName)
      End Try
      Return Nothing
    End Function


    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function GetDataTableSchema(connectionString As String, tableName As String) As DataTable
      ' Return the source schema for the target table
      Try
        Dim table As New DataTable(tableName) With {.Locale = InvariantCulture}
        Dim adapter As New SqlClient.SqlDataAdapter

        adapter.SelectCommand = New SqlClient.SqlCommand
        With adapter.SelectCommand
          .CommandText = "SELECT * FROM " & tableName
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : adapter.FillSchema(table, SchemaType.Source) : .Connection.Close()
        End With

        Return table
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function GetDataTableSchema(connectionString As String, tableName As String, primaryKey As String) As DataTable
      Try
        ' Return the source schema for the target table and explicitly set the primary key
        Dim table As New DataTable(tableName) With {.Locale = InvariantCulture}
        Dim adapter As New SqlClient.SqlDataAdapter

        adapter.SelectCommand = New SqlClient.SqlCommand
        With adapter.SelectCommand
          .CommandText = "SELECT * FROM " & tableName
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : adapter.FillSchema(table, SchemaType.Source) : .Connection.Close()
        End With

        'Set primary key if it has been passed
        table.PrimaryKey = New DataColumn() {table.Columns(primaryKey)}

        ' Set autoincrement if primary key is "ID" -  a little bit naughty
        If primaryKey = "ID" Then table.Columns("ID").AutoIncrement = True

        Return table
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return Nothing
    End Function

    Public Shared Sub ResetDBAllowNull(row As DataRow)
      If row Is Nothing Then Exit Sub
      ResetDBAllowNull(row.Table)
    End Sub

    Public Shared Sub ResetDBAllowNull(table As DataTable)
      If table Is Nothing Then Exit Sub
      For Each column As DataColumn In table.Columns
        Try
          column.AllowDBNull = True
        Catch
        End Try
      Next
    End Sub

    Public Shared Sub ResetDBNullReadOnly(row As DataRow)
      If row Is Nothing Then Exit Sub
      ResetDBNullReadOnly(row.Table)
    End Sub

    Public Shared Sub ResetDBNullReadOnly(table As DataTable)
      If table Is Nothing Then Exit Sub
      For Each column As DataColumn In table.Columns
        Try
          column.AllowDBNull = True
          column.ReadOnly = False
        Catch
        End Try
      Next
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SqlSelect(connectionString As String, selectString As String) As DataTable
      Return GetDataTable(connectionString, selectString)
    End Function

#End Region

#Region " Insert "
    ' Always load data table with the table name if it is going to be used to insert 

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function InsertDataRow(ByVal connectionString As String, ByVal row As DataRow) As Boolean
      Return InsertDataRow(connectionString, row, "ID")
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function InsertDataRow(ByVal connectionString As String, ByVal row As DataRow, primaryKey As String) As Boolean
      Try
        'Must have a valid data row
        If row Is Nothing Then Return False
        Dim table As DataTable = row.Table

        Dim adapter As New SqlClient.SqlDataAdapter
        adapter.InsertCommand = New SqlClient.SqlCommand

        With adapter.InsertCommand
          'Add parameters and build insert string based on table columns
          AddSqlInsertParameters(table, .Parameters)
          BuildSqlInsertString(table, adapter, primaryKey)

          'Set values of all the parameters from the data row
          Dim parameterName As String
          For Each column As DataColumn In table.Columns
            parameterName = "@" & column.ColumnName
            If .Parameters.Contains(parameterName) Then
              .Parameters(parameterName).Value = row(column.ColumnName)
            End If
          Next

          ' FOR DEBUG - just so we can easily check the string
          Dim sql = .CommandText

          ' Check to see if we have an auto increment column
          Dim autoIncrementColumn As DataColumn = Nothing
          For Each column As DataColumn In table.Columns
            If column.AutoIncrement Then autoIncrementColumn = column
          Next

          If autoIncrementColumn Is Nothing Then
            .Connection = New SqlClient.SqlConnection(connectionString)
            .Connection.Open() : .ExecuteNonQuery() : .Connection.Close()
          Else
            Dim autoIncrementValue As Integer = -1
            .CommandText = .CommandText & "; SELECT CAST(scope_identity() AS int);"
            .Connection = New SqlClient.SqlConnection(connectionString)
            .Connection.Open() : autoIncrementValue = Integer.Parse(.ExecuteScalar().ToString) : .Connection.Close()

            ' Write the autoincrement value into the column 
            Dim autoIncrementRowValue = NullToZeroInteger(row(autoIncrementColumn))
            If autoIncrementRowValue <> autoIncrementValue Then
              ' Toggle readonly if set
              If autoIncrementColumn.ReadOnly Then
                autoIncrementColumn.ReadOnly = False
                Try
                  ' This can throw an error if the new db autoincrement value happens to be in the datatable
                  '   because a value is automatically set when a row is added...
                  row(autoIncrementColumn) = autoIncrementValue
                Catch ex As Exception
                  Utilities.Log.LogError(ex)  ' just to see how often this happens
                End Try
                autoIncrementColumn.ReadOnly = True
              Else
                row(autoIncrementColumn) = autoIncrementValue
              End If
            End If
          End If
        End With

        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    Public Shared Function InsertDataTable(ByVal connectionString As String, ByVal table As DataTable) As Boolean
      Return InsertDataTable(connectionString, table, "ID")
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function InsertDataTable(ByVal connectionString As String, ByVal table As DataTable, primaryKey As String) As Boolean
      Try
        'Must have a valid data table
        If table Is Nothing OrElse table.Rows.Count <= 0 Then Return False

        Dim adapter As New SqlClient.SqlDataAdapter
        adapter.InsertCommand = New SqlClient.SqlCommand

        With adapter.InsertCommand
          ' Setup connection just once
          .Connection = New SqlClient.SqlConnection(connectionString)

          'Add sql parameters and build the insert string based on table schema
          AddSqlInsertParameters(table, .Parameters)
          BuildSqlInsertString(table, adapter, primaryKey)

          ' See if we have an autoincrement column
          Dim autoIncrementColumn As DataColumn = Nothing
          For Each column As DataColumn In table.Columns
            If column.AutoIncrement Then autoIncrementColumn = column
          Next

          .Connection.Open()
          For Each row As DataRow In table.Rows
            Try
              'Set values of all the parameters from the data row
              Dim parameterName As String
              For Each column As DataColumn In table.Columns
                parameterName = "@" & column.ColumnName
                If .Parameters.Contains(parameterName) Then
                  .Parameters(parameterName).Value = row(column.ColumnName)
                End If
              Next

              ' FOR DEBUG - just so we can easily check the string
              Dim sql = .CommandText

              If autoIncrementColumn Is Nothing Then
                .ExecuteNonQuery()
              Else
                Dim autoIncrementValue As Integer = -1
                .CommandText = .CommandText & "; SELECT CAST(scope_identity() AS int);"
                autoIncrementValue = Integer.Parse(.ExecuteScalar().ToString)

                ' Write autoincrement value into the column if it has not been written
                Dim autoIncrementRowValue = NullToZeroInteger(row(autoIncrementColumn))
                If autoIncrementRowValue <> autoIncrementValue Then
                  ' Toggle readonly if set
                  If autoIncrementColumn.ReadOnly Then
                    autoIncrementColumn.ReadOnly = False
                    Try
                      ' This can throw an error if the new db autoincrement value happens to be in the datatable
                      '   because a value is automatically set when a row is added...
                      row(autoIncrementColumn) = autoIncrementValue
                    Catch ex As Exception
                      Utilities.Log.LogError(ex)  ' just to see how often this happens
                    End Try
                    autoIncrementColumn.ReadOnly = True
                  Else
                    row(autoIncrementColumn) = autoIncrementValue
                  End If
                End If
              End If

            Catch ex As Exception
              Utilities.Log.LogError(ex)
            End Try
          Next
          .Connection.Close()
        End With

        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    Public Shared Sub AddSqlInsertParameters(table As DataTable, sqlParameters As SqlClient.SqlParameterCollection)
      Try
        If table Is Nothing OrElse sqlParameters Is Nothing Then Exit Sub

        With sqlParameters
          .Clear()
          For Each column As DataColumn In table.Columns
            'Create a SqlParameter for every column in the table - exclude auto increment and read only columns
            If column.AutoIncrement = False AndAlso column.ReadOnly = False Then
              Dim parameter As New SqlClient.SqlParameter
              parameter.ParameterName = "@" & column.ColumnName
              parameter.SqlDbType = GetSqlDataType(column)
              .Add(parameter)
            End If
          Next
        End With
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Sub BuildSqlInsertString(ByVal table As DataTable, adapter As SqlClient.SqlDataAdapter, primaryKey As String)
      'Make a Sql insert string (using sql parameters) to insert a row into the database
      '  Don't try to insert a value for an AutoIncrement column
      Try
        With adapter.InsertCommand
          If table Is Nothing Then Exit Sub

          Dim sqlColumns As String = Nothing
          Dim sqlValues As String = Nothing
          For Each column As DataColumn In table.Columns
            ' Exclude auto increment and readonly columns
            If column.AutoIncrement = False AndAlso column.ReadOnly = False Then
              ' Just to be on the safe side
              If column.ColumnName <> primaryKey Then
                ' List of columns we will set values for
                If sqlColumns Is Nothing Then
                  sqlColumns = "[" & column.ColumnName.ToString & "]"
                Else
                  sqlColumns &= ",[" & column.ColumnName.ToString & "]"
                End If
                ' List of corresponding values to set the columns too (SqlParameters)
                If sqlValues Is Nothing Then
                  sqlValues = "@" & column.ColumnName.ToString
                Else
                  sqlValues &= ",@" & column.ColumnName.ToString
                End If
              End If
            End If
          Next

          .CommandText = "INSERT INTO " & table.TableName & " (" & sqlColumns & ") VALUES(" & sqlValues & ")"

        End With
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SqlInsert(ByVal connectionString As String, ByVal insertString As String) As Integer
      Try
        Dim rows As Integer
        Dim da As New SqlClient.SqlDataAdapter
        da.InsertCommand = New SqlClient.SqlCommand
        With da.InsertCommand
          .CommandText = insertString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : rows = .ExecuteNonQuery() : .Connection.Close()
        End With

        Return rows
      Catch ex As Exception
        Utilities.Log.LogError(ex, insertString)
      End Try
      Return -1
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SqlInsertReturnIdentity(ByVal connectionString As String, ByVal insertString As String) As Integer
      Try
        Dim identity As Integer
        Dim da As New SqlClient.SqlDataAdapter() With {.InsertCommand = New SqlClient.SqlCommand}
        With da.InsertCommand
          .CommandText = insertString & "; SELECT CAST(scope_identity() AS int);"
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : identity = Integer.Parse(.ExecuteScalar.ToString) : .Connection.Close()
        End With

        Return identity
      Catch ex As Exception
        Utilities.Log.LogError(ex, insertString)
      End Try
      Return -1
    End Function


#End Region

#Region " Update "
    ' Always load the data table with the table name if it is going to be used to update

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function UpdateDataRow(connectionString As String, row As DataRow) As Boolean
      Return UpdateDataRow(connectionString, row, "ID")
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function UpdateDataRow(connectionString As String, row As DataRow, primaryKey As String) As Boolean
      'Write changes in a data row back to the database
      '  return false if write fails or an exception is thrown
      Try
        'Make sure the data row is not empty
        If row Is Nothing Then Return False
        Dim table = row.Table

        Dim adapter As New SqlClient.SqlDataAdapter
        adapter.UpdateCommand = New SqlClient.SqlCommand

        With adapter.UpdateCommand
          'Add sql parameters and build the insert string based on table schema
          AddSqlUpdateParameters(table, .Parameters, primaryKey)
          BuildSqlUpdateString(table, adapter, primaryKey)

          'Set values of all the parameters from the data row
          Dim parameterName As String
          For Each column As DataColumn In table.Columns
            parameterName = "@" & column.ColumnName
            If .Parameters.Contains(parameterName) Then
              .Parameters(parameterName).Value = row(column.ColumnName)
            End If
          Next

          Dim test = .CommandText

          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : .ExecuteNonQuery() : .Connection.Close()
        End With

        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    '<System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function UpdateDataTable(connectionString As String, table As DataTable) As Boolean
      Return UpdateDataTable(connectionString, table, "ID")
    End Function

    '<System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function UpdateDataTable(connectionString As String, table As DataTable, primaryKey As String) As Boolean
      Try
        'Make sure the data table is not empty
        If table Is Nothing OrElse table.Rows.Count <= 0 Then Return False

        Dim adapter As New SqlClient.SqlDataAdapter
        adapter.UpdateCommand = New SqlClient.SqlCommand

        With adapter.UpdateCommand
          ' Setup connection just once
          .Connection = New SqlClient.SqlConnection(connectionString)

          'Add sql parameters and build the insert string based on table columns
          AddSqlUpdateParameters(table, .Parameters, primaryKey)
          BuildSqlUpdateString(table, adapter, primaryKey)

          .Connection.Open()
          For Each row As DataRow In table.Rows
            Try

              'Set values of all the parameters from the data row
              Dim parameterName As String
              For Each column As DataColumn In table.Columns
                parameterName = "@" & column.ColumnName
                If .Parameters.Contains(parameterName) Then
                  .Parameters(parameterName).Value = row(column.ColumnName)
                End If
              Next

              Dim test = .CommandText

              .ExecuteNonQuery()

            Catch ex As Exception
              Utilities.Log.LogError(ex)
            End Try
          Next
          .Connection.Close()
        End With

        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function


    Public Shared Sub AddSqlUpdateParameters(table As DataTable, sqlParameters As SqlClient.SqlParameterCollection, primaryKey As String)
      Try
        If table Is Nothing OrElse sqlParameters Is Nothing Then Exit Sub

        With sqlParameters
          .Clear()
          For Each column As DataColumn In table.Columns
            'Create a SqlParameter for every column in the table - exclude auto increment and read only columns
            If column.AutoIncrement = False AndAlso column.ReadOnly = False Then
              Dim parameter As New SqlClient.SqlParameter
              parameter.ParameterName = "@" & column.ColumnName
              parameter.SqlDbType = GetSqlDataType(column)
              .Add(parameter)
            End If
          Next

          ' Make sure the parameter collection includes the primary key
          '   If the primary key is auto increment it may have been excluded because it is readonly
          Dim parameterName = "@" & primaryKey
          If Not sqlParameters.Contains(parameterName) Then
            Dim column = table.Columns(primaryKey)
            Dim parameter As New SqlClient.SqlParameter
            parameter.ParameterName = parameterName
            parameter.SqlDbType = GetSqlDataType(column)
            sqlParameters.Add(parameter)
          End If
        End With

      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Sub BuildSqlUpdateString(table As DataTable, adapter As SqlClient.SqlDataAdapter, primaryKey As String)
      Try
        With adapter.UpdateCommand
          'Make sure we have a DataTable to work with
          If table Is Nothing Then Exit Sub

          'Make the sql column update string
          Dim sqlColumns As String = Nothing
          For Each column As DataColumn In table.Columns
            If column.AutoIncrement = False AndAlso column.ReadOnly = False Then
              If column.ColumnName <> primaryKey Then
                If sqlColumns Is Nothing Then
                  sqlColumns = "[" & column.ColumnName.ToString & "]=@" & column.ColumnName.ToString
                Else
                  sqlColumns &= ",[" & column.ColumnName.ToString & "]=@" & column.ColumnName.ToString
                End If
              End If
            End If
          Next

          ' Build the update command using SqlParameters 
          .CommandText = "UPDATE [" & table.TableName & "] SET " & sqlColumns & " WHERE [" & primaryKey & "]=@" & primaryKey

        End With
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SqlUpdate(connectionString As String, updateString As String) As Integer
      Try
        Dim rows As Integer
        Dim da As New SqlClient.SqlDataAdapter
        da.UpdateCommand = New SqlClient.SqlCommand
        With da.UpdateCommand
          .CommandText = updateString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : rows = .ExecuteNonQuery() : .Connection.Close()
        End With

        Return rows
      Catch ex As Exception
        Utilities.Log.LogError(ex, updateString)
      End Try
      Return -1
    End Function

#End Region

#Region " Delete "

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SqlDelete(ByVal connectionString As String, ByVal deleteString As String) As Integer
      Try
        Dim rows As Integer
        Dim da As New SqlClient.SqlDataAdapter
        da.DeleteCommand = New SqlClient.SqlCommand
        With da.DeleteCommand
          .CommandText = deleteString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : rows = .ExecuteNonQuery() : .Connection.Close()
        End With

        Return rows
      Catch ex As Exception
        Utilities.Log.LogError(ex, deleteString)
      End Try
      Return -1
    End Function

#End Region

#Region " Sql data type mapping  "

    Public Shared Function GetSqlDataType(column As DataColumn) As SqlDbType
      If column Is Nothing Then Return Nothing
      Return DataTypeMap(column.DataType)
    End Function

    Public Shared Function GetSqlDataType(dataType As Type) As SqlDbType
      If dataType Is Nothing Then Return Nothing
      Return DataTypeMap(dataType)
    End Function

    ' Map framework DataType to SqlDbType
    '   don't add Integer or Datetime they will throw a key error because they are the same as int32 / Date
    Public Shared DataTypeMap As New Dictionary(Of Type, SqlDbType) From {
      {GetType(Boolean), SqlDbType.Bit},
      {GetType(Byte), SqlDbType.TinyInt},
      {GetType(Int16), SqlDbType.SmallInt},
      {GetType(Int32), SqlDbType.Int},
      {GetType(Int64), SqlDbType.BigInt},
      {GetType(Single), SqlDbType.Float},
      {GetType(Double), SqlDbType.Float},
      {GetType(Decimal), SqlDbType.Decimal},
      {GetType(Date), SqlDbType.DateTime},
      {GetType(TimeSpan), SqlDbType.Time},
      {GetType(DateTimeOffset), SqlDbType.DateTimeOffset},
      {GetType(Byte()), SqlDbType.Image},
      {GetType(Char()), SqlDbType.NVarChar},
      {GetType(String), SqlDbType.NVarChar},
      {GetType(Guid), SqlDbType.UniqueIdentifier},
      {GetType(Object), SqlDbType.Variant}
    }

    ' Map SqlDbType to framework DataType  
    Private Shared SqlDbTypeMap As New Dictionary(Of SqlDbType, Type) From {
      {SqlDbType.BigInt, GetType(Int64)},
      {SqlDbType.Binary, GetType(Byte())},
      {SqlDbType.Bit, GetType(Boolean)},
      {SqlDbType.Char, GetType(String)},
      {SqlDbType.Date, GetType(DateTime)},
      {SqlDbType.DateTime, GetType(DateTime)},
      {SqlDbType.DateTime2, GetType(DateTime)},
      {SqlDbType.DateTimeOffset, GetType(DateTimeOffset)},
      {SqlDbType.Decimal, GetType(Decimal)},
      {SqlDbType.Float, GetType(Double)},
      {SqlDbType.Image, GetType(Byte())},
      {SqlDbType.Int, GetType(Int32)},
      {SqlDbType.Money, GetType(Decimal)},
      {SqlDbType.NChar, GetType(String)},
      {SqlDbType.NText, GetType(String)},
      {SqlDbType.NVarChar, GetType(String)},
      {SqlDbType.Real, GetType(String)},
      {SqlDbType.SmallDateTime, GetType(DateTime)},
      {SqlDbType.SmallInt, GetType(Int16)},
      {SqlDbType.SmallMoney, GetType(Decimal)},
      {SqlDbType.Text, GetType(String)},
      {SqlDbType.Time, GetType(TimeSpan)},
      {SqlDbType.Timestamp, GetType(Byte())},
      {SqlDbType.TinyInt, GetType(Byte)},
      {SqlDbType.UniqueIdentifier, GetType(Guid)},
      {SqlDbType.VarBinary, GetType(Byte())},
      {SqlDbType.VarChar, GetType(String)},
      {SqlDbType.Variant, GetType(Object)},
      {SqlDbType.Xml, GetType(String)}
    }

#End Region

#Region " Save Row / Table Changes "

    ' Save the table row changes back to the data base using the row state to determine whether to insert, update or delete
    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SaveDataTable(ByVal connectionString As String, ByVal table As System.Data.DataTable) As Boolean
      Dim primaryKey As String = "ID"
      Return SaveDataTable(connectionString, table, primaryKey)
    End Function

    ' Save the table row changes back to the data base using the row state to determine whether to insert, update or delete
    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SaveDataTable(ByVal connectionString As String, ByVal table As System.Data.DataTable, primaryKey As String) As Boolean
      Dim tableName As String = table.TableName
      Return SaveDataTable(connectionString, table, primaryKey, tableName)
    End Function

    '<System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SaveDataTable(ByVal connectionString As String, ByVal table As System.Data.DataTable, primaryKey As String, tableName As String) As Boolean
      If table Is Nothing Then Return False
      Try
        For Each row As System.Data.DataRow In table.Rows
          SaveDataRow(connectionString, row, primaryKey, tableName)
        Next
        table.AcceptChanges()
        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    ' Save the row changes back to the data base using the row state to determine whether to insert, update or delete
    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SaveDataRow(connectionString As String, row As System.Data.DataRow) As Boolean
      If row Is Nothing Then Return False
      Return SaveDataRow(connectionString, row, "ID", row.Table.TableName)
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SaveDataRow(connectionString As String, row As System.Data.DataRow, primaryKey As String) As Boolean
      If row Is Nothing Then Return False
      Return SaveDataRow(connectionString, row, primaryKey, row.Table.TableName)
    End Function

    '<System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SaveDataRow(connectionString As String, row As System.Data.DataRow, primaryKey As String, tableName As String) As Boolean
      If row Is Nothing Then Return False
      Try
        Select Case row.RowState
          Case DataRowState.Deleted
            Dim sql = "DELETE FROM " & tableName & " WHERE [" & primaryKey & "]=" & row(primaryKey, DataRowVersion.Original).ToString
            SqlDelete(connectionString, sql)

          Case DataRowState.Added
            InsertDataRow(connectionString, row)
            row.AcceptChanges()

          Case DataRowState.Detached
            InsertDataRow(connectionString, row)

          Case DataRowState.Modified
            UpdateDataRow(connectionString, row, primaryKey)
            row.AcceptChanges()
        End Select

        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    Public Shared Function TableHasChanges(table As DataTable) As Boolean
      If table Is Nothing Then Return False
      For Each row As DataRow In table.Rows
        If row.RowState <> DataRowState.Unchanged Then Return True
      Next
      Return False
    End Function

#End Region

#Region " Sync Table "

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SyncTableData(ByVal connectionString As String, ByVal dtNewData As System.Data.DataTable, ByVal sqlSelectOldData As String) As Boolean
      Try
        'Get the old data from the database
        Dim dtOldData As System.Data.DataTable = GetDataTable(connectionString, sqlSelectOldData, dtNewData.TableName)

        'Sync the new data
        Return SyncTableData(connectionString, dtNewData, dtOldData)
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SyncTableData(ByVal connectionString As String, ByVal dtNewData As System.Data.DataTable, ByVal dtOldData As System.Data.DataTable) As Boolean
      Try
        'Loop through the new data - insert any new records, update any existing records
        For Each dr As System.Data.DataRow In dtNewData.Rows
          If dr.RowState <> DataRowState.Deleted Then
            If dr.IsNull("ID") Then
              InsertDataRow(connectionString, dr)
            Else
              UpdateDataRow(connectionString, dr)
            End If
          End If
        Next

        'Finally loop through dtOldData and delete any rows that do not exist in dtNewData from the database
        Dim idOld As Integer, idNew As Integer
        For Each drOld As System.Data.DataRow In dtOldData.Rows
          If Integer.TryParse(drOld("ID").ToString, idOld) Then
            Dim deleteRow As Boolean = True
            For Each drNew As System.Data.DataRow In dtNewData.Rows
              If drNew.RowState <> DataRowState.Deleted Then
                If Integer.TryParse(drNew("ID").ToString, idNew) Then
                  'Row found - don't delete
                  If idOld = idNew Then
                    deleteRow = False
                    Exit For
                  End If
                End If
              End If
            Next
            If deleteRow Then
              Dim sql As String = "DELETE FROM " & dtOldData.TableName & " WHERE ID=" & drOld("ID").ToString
              SqlDelete(connectionString, sql)
            End If
          End If
        Next
        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function


#End Region

#Region " Copy "

    ' Clear existing table and copy new data in
    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function CopyTableData(sourceTable As System.Data.DataTable, targetTable As System.Data.DataTable) As Boolean
      Try
        ' Clear existing data
        targetTable.Clear()

        ' Copy all rows from source table to target table
        For Each row As System.Data.DataRow In sourceTable.Rows
          Try
            Dim newRow As System.Data.DataRow = targetTable.NewRow
            If CopyRowData(row, newRow) Then
              targetTable.Rows.Add(newRow)
            End If
          Catch ex As Exception
            'Don't let one error stop the copy
          End Try
        Next
        Return True
      Catch ex As Exception
        'Ignore errors
      End Try
      Return False
    End Function


    ' Clear existing table and copy new data in
    '<System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function CopyTableData(sourceTable As System.Data.DataTable, targetTable As System.Data.DataTable, skipColumn As String) As Boolean
      Try
        ' Clear existing data
        targetTable.Clear()

        ' Copy all rows from source table to target table
        For Each row As System.Data.DataRow In sourceTable.Rows
          Try
            Dim newRow As System.Data.DataRow = targetTable.NewRow
            If CopyRowData(row, newRow, skipColumn) Then
              targetTable.Rows.Add(newRow)
            End If
          Catch ex As Exception
            'Don't let one error stop the copy
          End Try
        Next
        Return True
      Catch ex As Exception
        'Ignore errors
      End Try
      Return False
    End Function

    ' Overwrite matching data from one table to another
    '  Add or clear rows if tables have a different number of rows
    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function OverwriteTableData(sourceTable As System.Data.DataTable, targetTable As System.Data.DataTable) As Boolean
      Try
        If sourceTable.Rows.Count >= targetTable.Rows.Count Then
          For rowIndex As Integer = 0 To sourceTable.Rows.Count - 1
            If rowIndex < targetTable.Rows.Count Then
              CopyRowData(sourceTable.Rows(rowIndex), targetTable.Rows(rowIndex))
            Else
              Dim newRow = targetTable.NewRow
              CopyRowData(sourceTable.Rows(rowIndex), newRow)
              targetTable.Rows.Add(newRow)
            End If
          Next
        Else
          For rowIndex As Integer = 0 To targetTable.Rows.Count - 1
            If rowIndex < sourceTable.Rows.Count Then
              CopyRowData(sourceTable.Rows(rowIndex), targetTable.Rows(rowIndex))
            Else
              For colIndex As Integer = 0 To targetTable.Columns.Count - 1
                targetTable.Rows(rowIndex)(colIndex) = DBNull.Value
              Next
            End If
          Next
        End If

        Return True
      Catch ex As Exception
        'Ignore errors
      End Try
      Return False
    End Function


    Public Shared Function CopyRow(sourceRow As DataRow) As DataRow
      If sourceRow Is Nothing Then Return Nothing
      Dim newRow = sourceRow.Table.NewRow
      CopyRowData(sourceRow, newRow)
      Return newRow
    End Function

    'Copy matching data from source to target (name and data type must match)
    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function CopyRowData(sourceRow As System.Data.DataRow, targetRow As System.Data.DataRow) As Boolean
      Try
        ' Clear existing data - these rows may not be from the same table
        For Each column As System.Data.DataColumn In targetRow.Table.Columns
          If column.AllowDBNull Then targetRow(column.ColumnName) = DBNull.Value
        Next

        ' Copy matching data from source row to target row
        For Each column As System.Data.DataColumn In sourceRow.Table.Columns
          Try
            Dim columnName As String = column.ColumnName
            If targetRow.Table.Columns.Contains(columnName) Then
              If column.DataType Is targetRow.Table.Columns(columnName).DataType Then
                targetRow(columnName) = sourceRow(columnName)
              End If
            End If
          Catch ex As Exception
            'Don't let one error stop the copy
          End Try
        Next
        Return True
      Catch ex As Exception
        'Ignore errors
      End Try
      Return False
    End Function

    'Copy matching data from source to target (name and data type must match)
    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function CopyRowData(sourceRow As System.Data.DataRow, targetRow As System.Data.DataRow, skipColumn As String) As Boolean
      Try
        ' Clear existing data - these rows may not be from the same table
        For Each targetColumn As System.Data.DataColumn In targetRow.Table.Columns
          If targetColumn.ColumnName.ToLower <> skipColumn.ToLower Then
            If targetColumn.AllowDBNull Then targetRow(targetColumn.ColumnName) = DBNull.Value
          End If
        Next

        ' Copy matching data from source row to target row
        For Each sourceColumn As System.Data.DataColumn In sourceRow.Table.Columns
          Try
            Dim columnName As String = sourceColumn.ColumnName
            If columnName.ToLower <> skipColumn.ToLower Then
              If targetRow.Table.Columns.Contains(columnName) Then
                Dim targetColumn = targetRow.Table.Columns(columnName)
                If sourceColumn.DataType Is targetColumn.DataType Then
                  targetRow(columnName) = sourceRow(columnName)
                End If
              End If
            End If
          Catch ex As Exception
            'Don't let one error stop the copy
          End Try
        Next
        Return True
      Catch ex As Exception
        'Ignore errors
      End Try
      Return False
    End Function
#End Region

#Region " Compare "

    Public Enum EDataCompare
      Same
      Different
      [Error]
    End Enum

    ' Compare two tables to see if the data and structure are the same
    '   Use this to see if data has been changed (take a copy of original table to compare)
    Public Shared Function CompareTables(table1 As DataTable, table2 As DataTable) As EDataCompare
      Return CompareTables(table1, table2, Nothing)
    End Function

    Public Shared Function CompareTables(table1 As DataTable, table2 As DataTable, skipColumns As String()) As EDataCompare
      Try
        ' Some easy ones
        If table1 Is Nothing AndAlso table2 Is Nothing Then Return EDataCompare.Same
        If table1 Is Nothing AndAlso table2 IsNot Nothing Then Return EDataCompare.Different
        If table2 Is Nothing AndAlso table1 IsNot Nothing Then Return EDataCompare.Different
        If table1.Columns.Count <> table2.Columns.Count Then Return EDataCompare.Different
        If table1.Rows.Count <> table2.Rows.Count Then Return EDataCompare.Different

        If table1 Is table2 Then Return EDataCompare.Same   'TODO return a specific enum for this case ??
        If table1.Rows.Count = 0 Then Return EDataCompare.Same

        'Compare column names and data types
        For i As Integer = 0 To table1.Columns.Count - 1
          If table1.Columns(i).ColumnName <> table2.Columns(i).ColumnName Then Return EDataCompare.Different
          If Not table1.Columns(i).DataType Is table2.Columns(i).DataType Then Return EDataCompare.Different
        Next

        'Compare each row
        For i As Integer = 0 To table1.Rows.Count - 1
          Dim result = CompareRows(table1.Rows(i), table2.Rows(i), skipColumns)
          If result <> EDataCompare.Same Then Return result
        Next

        ' Must be the same
        Return EDataCompare.Same
      Catch ex As Exception
        ' Ignore for now
      End Try
      Return EDataCompare.Error
    End Function

    ' Compare two rows to see if the data is the same
    '   Use this to see if data has been changed (take a copy of original row to compare)
    Public Shared Function CompareRows(row1 As DataRow, row2 As DataRow) As EDataCompare
      Return CompareRows(row1, row2, Nothing)
    End Function

    Public Shared Function CompareRows(row1 As DataRow, row2 As DataRow, skipColumns As String()) As EDataCompare
      Try
        For i As Integer = 0 To row1.Table.Columns.Count - 1
          ' Do not test columns in the array (usually LastModified etc.)
          If CheckColumn(row1.Table.Columns(i).ColumnName, skipColumns) Then
            If row1(i).ToString <> row2(i).ToString Then Return EDataCompare.Different
          End If
        Next
        ' Must be the same
        Return EDataCompare.Same
      Catch ex As Exception
        ' Ignore for now
      End Try
      Return EDataCompare.Error
    End Function

    Private Shared Function SkipColumn(columnName As String, skipColumns As String()) As Boolean
      Return ColumnInList(columnName, skipColumns)
    End Function

    Private Shared Function CheckColumn(columnName As String, skipColumns As String()) As Boolean
      Return Not ColumnInList(columnName, skipColumns)
    End Function

    Private Shared Function ColumnInList(columnName As String, ColumnList As String()) As Boolean
      If ColumnList IsNot Nothing Then
        For Each column In ColumnList
          If columnName.Equals(column, StringComparison.InvariantCultureIgnoreCase) Then Return True
        Next
      End If
      Return False
    End Function

    Shared Function HasChanges(table As DataTable) As Boolean
      If table Is Nothing OrElse table.Rows.Count <= 0 Then Return False
      For Each row As DataRow In table.Rows
        If row.RowState <> DataRowState.Unchanged Then Return True
      Next
      Return False
    End Function

    Shared Function HasChanges(row As DataRow) As Boolean
      If row Is Nothing Then Return False
      Return row.RowState <> DataRowState.Unchanged
    End Function

#End Region

#Region " Utilities "

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SqlExecute(ByVal connectionString As String, ByVal sqlString As String) As Boolean
      Try
        Dim da As New SqlClient.SqlDataAdapter
        da.UpdateCommand = New SqlClient.SqlCommand
        With da.UpdateCommand
          .CommandText = sqlString
          .Connection = New SqlClient.SqlConnection(connectionString)
          .Connection.Open() : .ExecuteNonQuery() : .Connection.Close()
        End With

        Return True
      Catch ex As Exception
        Utilities.Log.LogError(ex, sqlString)
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SqlString(ByVal value As Object) As String
      If value Is Nothing Then Return "Null"
      Return SqlString(value.ToString)
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SqlString(ByVal value As String) As String
      Try
        If String.IsNullOrEmpty(value) Then Return "Null"

        'Wrap single quotes round string or return "Null" if string is empty
        Dim returnString As String = Nothing

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

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SqlDateString(ByVal value As String) As String
      Try
        If value = Nothing Then
          Return "Null"
        Else
          Dim tryDate As Date
          If Date.TryParse(value, tryDate) Then
            Return SqlDateString(tryDate)
          Else
            Return "Null"
          End If
        End If
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return "Null"
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function SqlDateString(ByVal value As Date) As String
      Try
        If value = Nothing Then
          Return "Null"
        Else
          Return SqlString(value.ToString(InvariantCulture))
          'Return SqlString(value.ToString("O", InvariantCulture))
        End If
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return "Null"
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function EmptyStringToNull(ByVal dr As DataRow, ByVal column As String) As String
      Try
        If dr.IsNull(column) Then Return "Null"
        If dr(column).ToString.Length <= 0 Then Return "Null"
        Return SqlString(dr(column).ToString)
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return "Null"
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Sub EmptyStringToNull(ByRef dataCell As Object, ByVal text As String)
      'Set dataCell to text if there's actually something in text, else set it to Null
      '  Useful when updating data from a textbox and we want an empty text box to be saved as Null
      If text.Length > 0 Then
        dataCell = text
      Else
        dataCell = System.DBNull.Value
      End If
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function EmptyDateToNull([date] As Date) As Object
      If [date] = Nothing Then Return DBNull.Value
      Return [date]
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToNothing(ByVal value As String) As String
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

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToFalseBoolean(ByVal value As String) As Boolean
      Try
        'Check to see if it's null
        If String.IsNullOrEmpty(value) Then Return False

        'See if we can parse it
        Dim tryBoolean As Boolean
        If Boolean.TryParse(value, tryBoolean) Then Return tryBoolean
      Catch ex As Exception
        'No code
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToFalseBoolean(ByVal value As Object) As Boolean
      Try
        'Make sure we have something
        If value Is Nothing Then Return False

        'If this is a boolean then just return the value
        If TypeOf value Is Boolean Then Return DirectCast(value, Boolean)

        'If this is an integer return true if it is not equal to 0
        If TypeOf value Is Integer Then Return (DirectCast(value, Integer) <> 0)

        'See if we can parse it
        Dim tryBoolean As Boolean
        If Boolean.TryParse(value.ToString, tryBoolean) Then Return tryBoolean
      Catch ex As Exception
        'No code
      End Try
      Return False
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToZeroInteger(ByVal value As String) As Integer
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

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToZeroInteger(ByVal value As Object) As Integer
      Try
        'Make sure we have something
        If value Is Nothing Then Return 0

        'If this is an integer then just return the value
        If TypeOf value Is Integer Then Return DirectCast(value, Integer)

        'See if we can parse it
        Dim tryInteger As Integer
        If Integer.TryParse(value.ToString, tryInteger) Then Return tryInteger
      Catch ex As Exception
        'No code
      End Try
      Return 0
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToZeroDouble(ByVal value As String) As Double
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

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToZeroDouble(ByVal value As Object) As Double
      Try
        'Make sure we have something
        If value Is Nothing Then Return 0

        'If this is a double just return the value
        If TypeOf value Is Double Then Return DirectCast(value, Double)

        'See if we can parse it
        Dim tryDouble As Double
        If Double.TryParse(value.ToString, tryDouble) Then Return tryDouble
      Catch ex As Exception
        'No code
      End Try
      Return 0
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToNothingString(ByVal value As Object) As String
      Try
        'If this is a string return the string
        If TypeOf value Is String Then Return DirectCast(value, String)
      Catch ex As Exception
        'No code
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToNothingDate(ByVal value As Object) As Date
      Try
        'If this is not a date then return nothing
        If Not (TypeOf value Is Date) Then Return Nothing

        Return DirectCast(value, Date)
      Catch ex As Exception
        'No code
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToNothingDate(ByVal value As Object, convertToLocal As Boolean) As Date
      Try
        'If this is not a date then return nothing
        If Not (TypeOf value Is Date) Then Return Nothing

        Dim valueDate = DirectCast(value, Date)
        If convertToLocal Then
          Return valueDate.ToLocalTime
        Else
          Return valueDate
        End If
      Catch ex As Exception
        'No code
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToNothingDateString(ByVal value As Object) As String
      Try
        'If this is not a date then return nothing
        If Not (TypeOf value Is Date) Then Return Nothing

        Dim valueDate = DirectCast(value, Date)
        Return valueDate.ToString
      Catch ex As Exception
        'No code
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToNothingDateString(ByVal value As Object, convertToLocal As Boolean) As String
      Try
        'If this is not a date then return nothing
        If Not (TypeOf value Is Date) Then Return Nothing

        Dim valueDate = DirectCast(value, Date)
        If convertToLocal Then
          Return valueDate.ToLocalTime.ToString
        Else
          Return valueDate.ToString
        End If
      Catch ex As Exception
        'No code
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function NullToNothingDateString(ByVal value As Object, convertToLocal As Boolean, dateFormat As String) As String
      Try
        'If this is not a date then return nothing
        If Not (TypeOf value Is Date) Then Return Nothing

        Dim valueDate = DirectCast(value, Date)
        If convertToLocal Then
          Return valueDate.ToLocalTime.ToString(dateFormat)
        Else
          Return valueDate.ToString(dateFormat)
        End If
      Catch ex As Exception
        'No code
      End Try
      Return Nothing
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function TrueFalseToInteger(value As Boolean) As Integer
      If value = True Then Return -1
      Return 0
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Function TrueFalseToInteger(value As Boolean, trueValue As Integer) As Integer
      If value = True Then Return trueValue
      Return 0
    End Function

    Public Shared Function GetInteger(row As DataRow, columnName As String) As Integer
      If row Is Nothing Then Return -1
      If Not row.Table.Columns.Contains(columnName) Then Return -1
      If row.IsNull(columnName) Then Return 0

      Dim value As Integer
      If Integer.TryParse(row(columnName).ToString, value) Then
        Return value
      Else
        Return -1
      End If
    End Function

    Public Shared Function GetDouble(row As DataRow, columnName As String) As Double
      If row Is Nothing Then Return -1
      If Not row.Table.Columns.Contains(columnName) Then Return -1
      If row.IsNull(columnName) Then Return 0

      Dim value As Double
      If Double.TryParse(row(columnName).ToString, value) Then
        Return value
      Else
        Return -1
      End If
    End Function

    Public Shared Function GetDouble(row As DataRow, columnName As String, digits As Integer) As Double
      If row Is Nothing Then Return -1
      If Not row.Table.Columns.Contains(columnName) Then Return -1
      If row.IsNull(columnName) Then Return 0

      Dim value As Double
      If Double.TryParse(row(columnName).ToString, value) Then
        Return Math.Round(value, digits)
      Else
        Return -1
      End If
    End Function

    Public Shared Function GetString(row As DataRow, columnName As String) As String
      If row Is Nothing Then Return Nothing
      If Not row.Table.Columns.Contains(columnName) Then Return Nothing
      If row.IsNull(columnName) Then Return Nothing

      Return row(columnName).ToString
    End Function

#End Region

#Region " Convert Dates "

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Sub ConvertToLocalTime(table As System.Data.DataTable)
      Try
        For Each row As System.Data.DataRow In table.Rows
          For Each column As System.Data.DataColumn In table.Columns
            If column.DataType Is GetType(Date) Then
              If Not row.IsNull(column) Then row(column) = DirectCast(row(column), Date).ToLocalTime
            End If
          Next
        Next
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Sub ConvertToLocalTime(row As System.Data.DataRow)
      Try
        For Each column As System.Data.DataColumn In row.Table.Columns
          If column.DataType Is GetType(Date) Then
            If Not row.IsNull(column) Then row(column) = DirectCast(row(column), Date).ToLocalTime
          End If
        Next
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Sub ConvertToLocalTime(table As System.Data.DataTable, columnName As String)
      Try
        Dim tryDate As Date
        For Each row As System.Data.DataRow In table.Rows
          If Date.TryParse(row(columnName).ToString, tryDate) Then
            If Not row.IsNull(columnName) Then row(columnName) = DirectCast(row(columnName), Date).ToLocalTime
          End If
        Next
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Sub ConvertToLocalTime(row As System.Data.DataRow, columnName As String)
      Try
        Dim tryDate As Date
        If Date.TryParse(row(columnName).ToString, tryDate) Then
          If Not row.IsNull(columnName) Then row(columnName) = DirectCast(row(columnName), Date).ToLocalTime
        End If
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Sub ConvertToUniversalTime(table As System.Data.DataTable)
      Try
        For Each row As System.Data.DataRow In table.Rows
          For Each column As System.Data.DataColumn In table.Columns
            If column.DataType Is GetType(Date) Then
              If Not row.IsNull(column) Then row(column) = DirectCast(row(column), Date).ToUniversalTime
            End If
          Next
        Next
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Sub ConvertToUniversalTime(row As System.Data.DataRow)
      Try
        For Each column As System.Data.DataColumn In row.Table.Columns
          If column.DataType Is GetType(Date) Then
            If Not row.IsNull(column) Then row(column) = DirectCast(row(column), Date).ToUniversalTime
          End If
        Next
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Sub ConvertToUniversalTime(table As System.Data.DataTable, columnName As String)
      Try
        Dim tryDate As Date
        For Each row As System.Data.DataRow In table.Rows
          If Date.TryParse(row(columnName).ToString, tryDate) Then
            If Not row.IsNull(columnName) Then row(columnName) = DirectCast(row(columnName), Date).ToUniversalTime
          End If
        Next
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Shared Sub ConvertToUniversalTime(row As System.Data.DataRow, columnName As String)
      Try
        Dim tryDate As Date
        If Date.TryParse(row(columnName).ToString, tryDate) Then
          If Not row.IsNull(columnName) Then row(columnName) = DirectCast(row(columnName), Date).ToUniversalTime
        End If
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
    End Sub

#End Region

#Region " Table Configuration "

    Public Shared Function AddTable(connectionString As String, tableName As String, columnString As String) As Boolean
      Dim sql As String = Nothing
      Try
        sql = "CREATE TABLE " & tableName & "([ID] [int] IDENTITY(1,1) NOT NULL," & columnString & " CONSTRAINT [PK_" & tableName & "] PRIMARY KEY CLUSTERED ([ID] ASC))"
        Return SqlExecute(connectionString, sql)

      Catch ex As Exception
        Utilities.Log.LogError(ex, sql)
      End Try
      Return False
    End Function

    Public Shared Function UpdateTable(connectionString As String, tableName As String, columnString As String) As Boolean
      Dim sql As String = Nothing
      Try
        ' TODO - Add Code
      Catch ex As Exception
        Utilities.Log.LogError(ex, sql)
      End Try
      Return False
    End Function

    Public Shared Function TableExists(connectionString As String, tableName As String) As Boolean
      Dim sql As String = Nothing
      Try
        ' Get the catalog view with all the table names
        sql = "SELECT Name FROM sys.tables ORDER BY Name"
        Dim sysTables = GetDataTable(connectionString, sql)

        Return TableExists(tableName, sysTables)
      Catch ex As Exception
        Utilities.Log.LogError(ex, sql)
      End Try
      Return False
    End Function

    Public Shared Function TableExists(tableName As String, sysTables As System.Data.DataTable) As Boolean
      Try
        If sysTables Is Nothing Then Return False

        For Each row As System.Data.DataRow In sysTables.Rows
          If row("Name").ToString.Equals(tableName, StringComparison.OrdinalIgnoreCase) Then Return True
        Next
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

    Public Shared Function AlterTableAddColumns(connectionString As String, tableName As String, columnSqlList As String) As Boolean
      Dim sql As String = Nothing
      Try
        ' Get the table structure
        Dim table = GetDataTableSchema(connectionString, tableName)

        ' Odd but just in case
        If table Is Nothing Then Return False

        ' Split the column list into an array 
        Dim columnSqlArray = columnSqlList.Split(",".ToCharArray, System.StringSplitOptions.RemoveEmptyEntries)

        ' Make sure we have some data to work with
        If columnSqlArray Is Nothing OrElse columnSqlArray.Length <= 0 Then Return False

        ' Loop through the list and check to see if any of the columns are already in the table before add the column to the
        '   alter table sql string
        For i = 0 To columnSqlArray.GetUpperBound(0)
          If Not ColumnExists(columnSqlArray(i), table) Then
            If sql Is Nothing Then
              sql = columnSqlArray(i)
            Else
              sql = sql & "," & columnSqlArray(i)
            End If
          End If
        Next

        ' Make sure we still have columns to add
        If sql Is Nothing Then Return False

        ' Execute the alter table statment
        sql = "ALTER TABLE " & tableName & " ADD " & sql
        Utilities.Sql.SqlExecute(connectionString, sql)
      Catch ex As Exception
        Utilities.Log.LogError(ex, sql)
      End Try
      Return False
    End Function

    Private Shared Function ColumnExists(columnSql As String, table As System.Data.DataTable) As Boolean
      Try
        ' Assumes "[]" around column name
        Dim endOfNameIndex = columnSql.IndexOf("]")
        Dim columnName = columnSql.Substring(1, endOfNameIndex - 1)  ' [1]

        For Each column As System.Data.DataColumn In table.Columns
          If column.ColumnName.Equals(columnName, StringComparison.OrdinalIgnoreCase) Then Return True
        Next
      Catch ex As Exception
        Utilities.Log.LogError(ex)
      End Try
      Return False
    End Function

#End Region

  End Class
    
#If 0 Then

Public Class DbCentral
  Private ReadOnly connectionString_ As String
  Public Sub New(ByVal connectionString As String)
    connectionString_ = connectionString
  End Sub

  Public Function DbGetDataTable(ByVal sql As String) As DataTable
    Using dbConnection As New SqlClient.SqlConnection(connectionString_)
      dbConnection.Open()
      Using da As New SqlClient.SqlDataAdapter(sql, dbConnection)
        With da
          Try
            Dim ret As New DataTable : ret.Locale = InvariantCulture
            .Fill(ret)
            Return ret
          Catch ex As Exception
            With ex.Data : .Add("ConnectionString", connectionString_) : .Add("CommandText", sql) : End With
            Throw
          End Try
        End With
      End Using
    End Using
  End Function

  Public Function DbExecute(ByVal sql As String) As Integer
    Using dbConnection As New SqlClient.SqlConnection(connectionString_)
      dbConnection.Open()
      With dbConnection.CreateCommand
        .CommandText = sql : .CommandTimeout = 0
        Try
          Return .ExecuteNonQuery()
        Catch ex As Exception
          With ex.Data : .Add("ConnectionString", connectionString_) : .Add("CommandText", sql) : End With
          Throw
        End Try
      End With
    End Using
  End Function


End Class

' --------------------------------------------------------
Public NotInheritable Class SqlConvert
  Public Shared Function ToSqlString(ByVal value As DateTime) As String
    Dim ret As String = String.Format(InvariantCulture, _
                         "{0:0}-{1:0}-{2:0} {3:00}:{4:00}:{5:00}", _
                         New Object() {value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second})
    Return "'" & ret & "'"
  End Function

  Public Shared Function ToSqlString(ByVal value As String) As String
    If value Is Nothing Then Return "Null"

    With New System.Text.StringBuilder
      .Append("'"c)
      value = value.Replace("�"c, "'"c)  ' some strange other sort of quote
      value = value.Replace("'", "''")
      .Append(value)
      .Append("'"c)
      Return .ToString
    End With
  End Function

  Public Shared Function ToSqlString(ByVal value As Boolean) As String
    If value Then Return "1"
    Return "Null"
  End Function

  Public Shared Function ToSqlString(ByVal value As Short) As String
    Return value.ToString(InvariantCulture)
  End Function

  Public Shared Function ToSqlString(ByVal value As Integer) As String
    Return value.ToString(InvariantCulture)
  End Function

  Public Shared Function ToSqlString(ByVal value As Long) As String
    Return value.ToString(InvariantCulture)
  End Function

  Public Shared Function ToSqlString(ByVal value As Single) As String
    Return value.ToString(InvariantCulture)
  End Function

  Public Shared Function ToSqlString(ByVal value As Double) As String
    Return value.ToString(InvariantCulture)
  End Function

  Public Shared Function ToSqlString(ByVal value As Byte()) As String
    With New System.Text.StringBuilder
      .Append("0X")
      For i As Integer = 0 To value.Length - 1
        .Append(value(i).ToString("X2", InvariantCulture))
      Next i
      Return .ToString
    End With
  End Function

  Public Shared Function ToSqlString(ByVal value As Object) As String
    If TypeOf value Is DateTime Then Return ToSqlString(DirectCast(value, DateTime))
    If value Is Nothing OrElse TypeOf value Is DBNull Then Return "Null"
    Dim valueAsString As String = TryCast(value, String)
    If valueAsString IsNot Nothing Then Return ToSqlString(valueAsString)
    If TypeOf value Is Boolean Then Return ToSqlString(DirectCast(value, Boolean))
    If TypeOf value Is Short Then Return ToSqlString(DirectCast(value, Short))
    If TypeOf value Is Integer Then Return ToSqlString(DirectCast(value, Integer))
    If TypeOf value Is Long Then Return ToSqlString(DirectCast(value, Long))
    If TypeOf value Is Single Then Return ToSqlString(DirectCast(value, Single))
    If TypeOf value Is Double Then Return ToSqlString(DirectCast(value, Double))
    If TypeOf value Is Byte() Then Return ToSqlString(DirectCast(value, Byte()))
    Return value.ToString  ' this is failure, really
  End Function
End Class

' --------------------------------------------------------
Public NotInheritable Class Null
  Private Sub New()
  End Sub

  Public Shared Function NullToEmptyString(ByVal value As Object) As String
    If value Is Nothing OrElse TypeOf value Is DBNull Then Return String.Empty
    Dim str As String = TryCast(value, String) : If str IsNot Nothing Then Return str
    Return CType(value, String)
  End Function

  Public Shared Function NullToZeroInteger(ByVal value As Object) As Integer
    If value Is Nothing OrElse TypeOf value Is DBNull Then Return 0

    'See if we can parse it
    Dim tryInteger As Integer
    If Integer.TryParse(value.ToString, tryInteger) Then
      Return tryInteger
    Else
      Return 0
    End If


    'If TypeOf value Is Integer Then Return DirectCast(value, Integer)
    Return CType(value, Integer)
  End Function

  Public Shared Function NullToZeroDouble(ByVal value As Object) As Double
    If value Is Nothing OrElse TypeOf value Is DBNull Then Return 0
    If TypeOf value Is Double Then Return DirectCast(value, Double)
    Return CType(value, Double)
  End Function

  Public Shared Function NullToZeroDate(ByVal value As Object) As Date
    If value Is Nothing OrElse TypeOf value Is DBNull Then Return Date.MinValue
    If TypeOf value Is Date Then Return DirectCast(value, Date)
    ' Sometimes we get a number which is actually an Ole Automation date
    Return Date.FromOADate(CType(value, Double))
  End Function

  Public Shared Function NullToFalse(ByVal value As Object) As Boolean
    If value Is Nothing OrElse TypeOf value Is DBNull Then Return False
    If TypeOf value Is Boolean Then Return DirectCast(value, Boolean)
    Return CType(value, Boolean)
  End Function

  Public Shared Function NullToNothingByteArray(ByVal value As Object) As Byte()
    If value Is Nothing OrElse TypeOf value Is DBNull Then Return Nothing
    Return DirectCast(value, Byte())
  End Function



  Shared Function bcdtodecimal(bcd As Integer) As Integer
    Dim ret As Integer
    Dim mult = 1

    Do While bcd <> 0
      ret = ret + (bcd Mod 16) * mult
      mult = mult * 10
      bcd = bcd \ 16
    Loop
    Return ret
  End Function

End Class

#End If
#End If
  End Class
End Namespace