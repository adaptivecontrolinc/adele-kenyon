Partial Public NotInheritable Class Settings

  Public Shared ReadOnly Property DefaultCulture() As System.Globalization.CultureInfo
    Get
      Return System.Globalization.CultureInfo.InvariantCulture
    End Get
  End Property

  Public Shared Property Demo As Integer = 0

  Public Shared Property ConnectionStringLocal As String = "data source=(local);initial catalog=Kenyon;user id=Adaptive;password=Control"

  Public Shared Property Version() As String = "AdeleKnits-KenyonFrame"



#If 0 Then

  Public Shared Property MicrospeedEnabled() As String = "False"

  Public Shared ReadOnly Property MicroSpeedIsEnabled() As Boolean
    Get
      Return (MicrospeedEnabled.Trim.ToLower = "true")
    End Get
  End Property

  Public Shared Property HoneywellEnabled() As String = "False"

  Public Shared ReadOnly Property HoneywellIsEnabled() As Boolean
    Get
      Return ((HoneywellEnabled.ToLower) = "true")
    End Get
  End Property



#End If
  Public Shared Sub Load()
    Try

      'Get application and file path
      Dim appPath As String = My.Application.Info.DirectoryPath
      Dim filePath As String = appPath & "\Settings.xml"

      'If the file doses not exist then just use defaults...
      If Not My.Computer.FileSystem.FileExists(filePath) Then Exit Sub

      'Read the settings into a dataset
      Dim dsSettings As New System.Data.DataSet
      dsSettings.ReadXml(filePath)

      With dsSettings
        If .Tables.Contains("settings") Then
          With .Tables("settings")
            For Each dr As System.Data.DataRow In .Rows
              Select Case dr("name").ToString.ToLower

                Case "Demo".ToLower
                  If Not dr.IsNull("value") Then Demo = Integer.Parse(dr("value").ToString)

                Case "ConnectionStringLocal".ToLower
                  If Not dr.IsNull("value") Then ConnectionStringLocal = dr("value").ToString

                Case "Version".ToLower
                  If Not dr.IsNull("value") Then Version = dr("value").ToString

                  'Case "MicrospeedEnabled".ToLower
                  '  If Not dr.IsNull("value") Then MicrospeedEnabled = dr("value").ToString

                  'Case "HoneywellEnabled".ToLower
                  '  If Not dr.IsNull("value") Then HoneywellEnabled = dr("value").ToString


              End Select
            Next
          End With
        End If
      End With

    Catch ex As Exception
      'TODO - log error ?
    End Try
  End Sub

  Public Shared Sub Save()
    Try

      'Create a settings dataset
      Dim dsSettings As New System.Data.DataSet("Root")

      'Create a settings table
      Dim dtSettings As New System.Data.DataTable("Settings")
      dtSettings.Columns.Add("Name", Type.GetType("System.String"))
      dtSettings.Columns.Add("Value", Type.GetType("System.String"))

      'Create rows
      Dim newRow As System.Data.DataRow

      ' Demo
      newRow = dtSettings.NewRow
      newRow("Name") = "Demo" : newRow("Value") = Demo
      dtSettings.Rows.Add(newRow)

      ' ConnectionStringLocal
      newRow = dtSettings.NewRow
      newRow("Name") = "ConnectionStringLocal" : newRow("Value") = ConnectionStringLocal
      dtSettings.Rows.Add(newRow)

      'Version
      newRow = dtSettings.NewRow
      newRow("Name") = "Version" : newRow("Value") = Version
      dtSettings.Rows.Add(newRow)

      ''Microspeed Enabled
      'newRow = dtSettings.NewRow
      'newRow("Name") = "MicrospeedEnabled" : newRow("Value") = MicrospeedEnabled
      'dtSettings.Rows.Add(newRow)

      ''Honeywell Enabled
      'newRow = dtSettings.NewRow
      'newRow("Name") = "HoneywellEnabled" : newRow("Value") = HoneywellEnabled
      'dtSettings.Rows.Add(newRow)

      'Add the DataTable to the DataSet
      dsSettings.Tables.Add(dtSettings)

      'Get application and file path
      Dim appPath As String = My.Application.Info.DirectoryPath
      Dim filePath As String = appPath & "\Settings.xml"

      'Save this DataSet
      dsSettings.WriteXml(filePath)

    Catch ex As Exception
      Dim ErrorText As String = "Settings.Save(): " & ex.Message
      Utilities.Log.LogError(ErrorText)
      '    MsgBox(ErrorText, MsgBoxStyle.Critical, "Adaptive Control")
    End Try
  End Sub

End Class
