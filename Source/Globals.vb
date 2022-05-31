Imports System.Windows.Forms

Module Globals

  ' Connection to host database
  Property SqlConnectionHost As String

  ' Connection to dispenser database
  Property SqlConnectionDispenser As String

  Property StandardDispenseMinutes As Integer = 16

  Property CurrentTime As Date

  ReadOnly Property CurrentTimeDisplay As String
    Get
      Return CurrentTime.ToString
    End Get
  End Property

  ReadOnly Property Progress(actual As Double, target As Double) As String
    Get
      Return actual.ToString("#0.00") & " / " & target.ToString("#0.00")
    End Get
  End Property

  Function GetValue(value As Integer, defaultValue As Integer) As Integer
    If value <= 0 Then Return defaultValue
    Return value
  End Function

  Function GetValue(value As Integer, defaultValue As Integer, min As Integer, max As Integer) As Integer
    If value <= 0 Then Return MinMax(defaultValue, min, max)
    Return MinMax(value, min, max)
  End Function

  Function GetDyelot(Job As String) As String
    If String.IsNullOrEmpty(Job) Then Return Nothing
    Dim data = Job.Split("@".ToCharArray)
    Return data(0)
  End Function

  Function GetReDye(Job As String) As Integer
    If String.IsNullOrEmpty(Job) Then Return 0
    Dim data = Job.Split("@".ToCharArray)
    If data.Length = 2 Then Return CInt(data(1))
    Return 0
  End Function


  Private newLineBytes As Byte() = {&HD, &HA}
  Public Property NewLine As String = System.Text.ASCIIEncoding.ASCII.GetString(newLineBytes)

  Public ReadOnly Property DefaultTimeStamp As String
    Get
      Return Date.Now.ToString("HHmmss")
    End Get
  End Property

  Public ReadOnly Property DefaultDateAndTimeStamp As String
    Get
      Return Date.Now.ToString("yyMMddHHmmss")
    End Get
  End Property


  Public Function ShowWarning(warning As String) As DialogResult
    Return MessageBox.Show(warning, "Adaptive", MessageBoxButtons.OK, MessageBoxIcon.Warning)
  End Function

  Public Function ShowInformation(information As String) As DialogResult
    Return MessageBox.Show(information, "Adaptive", MessageBoxButtons.OK, MessageBoxIcon.Information)
  End Function

  Public Function ShowQuestion(question As String) As DialogResult
    Return MessageBox.Show(question, "Adaptive", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
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


End Module