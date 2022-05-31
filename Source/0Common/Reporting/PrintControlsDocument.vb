'DT's Print Control Class
Imports System.Windows.Forms

Public Class PrintControlsDocument : Inherits Printing.PrintDocument
  ' Make a delegate to quickly call the protected method Control.OnPrint
  Private Delegate Sub OnPrint__(ByVal control As Control, ByVal e As PaintEventArgs)
  Private Shared ReadOnly onPrint_ As OnPrint__ = DirectCast([Delegate].CreateDelegate(GetType(OnPrint__), _
    GetType(Control).GetMethod("OnPaint", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)), OnPrint__)

  Private ReadOnly control_ As Control, copyright_ As String, printHeaderAndFooter_ As Boolean, doNotPrint_() As Control
  Private ReadOnly customer_ As String
  Private printDate As Date = Date.Now, pageNumber_ As Integer

  Private graph_ As Graphics

  Public Sub New(ByVal control As Control, ByVal copyright As String, ByVal printHeaderAndFooter As Boolean, ByVal customer As String, ByVal ParamArray doNotPrint() As Control)
    control_ = control : copyright_ = copyright : printHeaderAndFooter_ = printHeaderAndFooter
    customer_ = customer
    doNotPrint_ = doNotPrint
  End Sub


  Public Sub New(ByVal graph As Graphics, ByVal copyright As String, ByVal printHeaderAndFooter As Boolean, ByVal customer As String, ByVal ParamArray doNotPrint() As Control)
    graph_ = graph : copyright_ = copyright : printHeaderAndFooter_ = printHeaderAndFooter
    customer_ = customer
    doNotPrint_ = doNotPrint
  End Sub


  ' Get ready to print (again)
  Protected Overrides Sub OnBeginPrint(ByVal e As Printing.PrintEventArgs)
    pageNumber_ = 0
  End Sub

  Protected Overrides Sub OnPrintPage(ByVal e As Printing.PrintPageEventArgs)
    ' Start with the full printing area
    Dim graphics = e.Graphics, rc = e.MarginBounds

    ' The header and footer
    pageNumber_ += 1
    If printHeaderAndFooter_ Then
      PrintSupport.PrintHeaderAndFooter(graphics, rc, Me, printDate, pageNumber_, Nothing, copyright_, customer_)
    End If

    graphics.TranslateTransform(rc.Left, rc.Top)

    Dim crc = control_.ClientRectangle
    ' Fit the width, but keep the height proportional
    Dim sx = CType(rc.Width / crc.Width, Single)
    'graphics.ScaleTransform(sx, sx)

    PrintControls(graphics, control_.Controls, doNotPrint_)
    '      If startRow_ < owner_.Items.Count Then e.HasMorePages = True
  End Sub

  Private Shared Sub PrintControls(ByVal graphics As Graphics, ByVal controls As Control.ControlCollection, ByVal doNotPrint As IEnumerable(Of Control))
    Dim pe = New PaintEventArgs(graphics, Nothing)  ' a required instance
    For Each x As Control In controls
      For Each i In doNotPrint
        If i Is x Then GoTo nextControl
      Next i
      Dim graphicsState = graphics.Save()
      ' Translate so the control paints in the right place
      Dim bounds = x.Bounds : graphics.TranslateTransform(bounds.Left, bounds.Top)
      If TypeOf x Is Label Then
        Using br = New SolidBrush(x.ForeColor)
          graphics.DrawString(x.Text, x.Font, br, 0, 0)
        End Using
      ElseIf TypeOf x Is Panel Then
        Dim pa = DirectCast(x, Panel)
        If pa.BorderStyle <> BorderStyle.None Then
          graphics.DrawRectangle(Pens.Black, 0, 0, bounds.Width - 1, bounds.Height - 1)
        End If
      ElseIf x.GetType.Name = "HistoryGraphView" Then
        ' Use some inside information
        Dim args() As Object = {graphics, New Rectangle(0, 0, bounds.Width, bounds.Height)}
        x.GetType.InvokeMember("PaintTracesKey", Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.InvokeMethod, Nothing, x, args)
        x.GetType.InvokeMember("Paint", Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.InvokeMethod, Nothing, x, args)
      Else
        onPrint_(x, pe)
      End If
      ' Print any child controls
      PrintControls(graphics, x.Controls, doNotPrint)
      graphics.Restore(graphicsState)
nextControl:
    Next x
  End Sub
End Class

Public NotInheritable Class PrintSupport
  Private Sub New()
  End Sub

  Public Shared Sub SetMargins(ByVal pageSettings As Printing.PageSettings)
    ' Set some printing defaults - we go fairly close to the edges
    With pageSettings
      With .Margins
        .Left = 50 : .Top = 50 : .Right = 60 : .Bottom = 60
      End With
    End With
  End Sub

  Public Shared Sub PrintHeaderAndFooter(ByVal graphics As Graphics, ByRef rc As Rectangle, _
                                         ByVal printDocument As Printing.PrintDocument, _
                                         ByVal printDate As Date, ByVal pageNumber As Integer, _
                                         ByVal appIcon As Icon, ByVal copyright As String, ByVal productName As String)
    ' Print a header and footer
    Using mainFont = New Font("Tahoma", 10, FontStyle.Bold)
      With graphics
        'Header
        Const headerShadingHeight As Integer = 23
        Using brShading = New SolidBrush(Color.FromArgb(214, 236, 243))
          .FillRectangle(brShading, rc.Left, rc.Top, rc.Width, headerShadingHeight)
          .DrawString(printDocument.DocumentName, mainFont, Brushes.Black, rc.Left + 4, rc.Top + 4)
          Static appBitmap As Bitmap : If appBitmap Is Nothing AndAlso appIcon IsNot Nothing Then appBitmap = appIcon.ToBitmap : appBitmap.MakeTransparent()
          If appBitmap IsNot Nothing Then .DrawImage(appBitmap, rc.Right - 16 - 4, rc.Top + 3, 16, 16)
          .DrawString(productName, mainFont, Brushes.Black, rc.Right - 2 - 16 - 4 - 2, rc.Top + 4, New StringFormat(StringFormatFlags.DirectionRightToLeft))
          rc.Y += headerShadingHeight : rc.Height -= headerShadingHeight
          Const headerGap As Integer = 8 + 4 + 10
          rc.Y += headerGap : rc.Height -= headerGap

          ' Footer
          Const footerShadingHeight As Integer = 20 ' 18
          rc.Height -= footerShadingHeight
          .FillRectangle(brShading, rc.Left, rc.Bottom, rc.Width, footerShadingHeight)
          Using footerFont = New Font("Tahoma", 7)
            .DrawString("Page" & " " & pageNumber.ToString(InvariantCulture), footerFont, Brushes.Black, rc.Left + 4, rc.Bottom + 3)
            Using sf = New StringFormat With {.Alignment = StringAlignment.Far}
              Dim str = "Printed" & " " & printDate.ToShortDateString & " " & printDate.ToShortTimeString
              If Not String.IsNullOrEmpty(copyright) Then str &= "  © " & copyright
              .DrawString(str, footerFont, Brushes.Black, rc.Right - 2, rc.Bottom + 3, sf) 'here
            End Using
          End Using
          Const footerGap As Integer = 8 + 6
          rc.Height -= footerGap
        End Using
      End With
    End Using
  End Sub

  Public Shared ReadOnly Property StandardFont() As Font
    Get
      Return SystemFonts.DialogFont
    End Get
  End Property

  Public Shared Function NoPrintersInstalled() As Boolean
    If Printing.PrinterSettings.InstalledPrinters.Count <> 0 Then Return False
    If MessageBox.Show("Before you can perform printer-related tasks such as page setup or printing a document," & _
                       " you need to install a printer.  Do you want to install a printer now?", _
         Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
      Microsoft.VisualBasic.Shell("rundll32.exe Shell32.dll,SHHelpShortcuts_RunDLL AddPrinter")
    End If
    Return True
  End Function

  Public Shared Sub ShowPropertiesDialog(ByVal printerSettings As Printing.PrinterSettings, ByVal owner As IWin32Window)
    Dim hPrinter As IntPtr : NativeMethods.OpenPrinter(printerSettings.PrinterName, hPrinter, IntPtr.Zero)
    Dim bytes = NativeMethods.DocumentProperties(IntPtr.Zero, hPrinter, printerSettings.PrinterName, IntPtr.Zero, IntPtr.Zero, 0)
    Dim devModeOutput = Runtime.InteropServices.Marshal.AllocHGlobal(bytes)
    Const IDOK As Integer = 1

    Const DM_OUT_BUFFER As Integer = 2, DM_IN_PROMPT As Integer = 4, DM_IN_BUFFER As Integer = 8

    Dim ret1 = NativeMethods.DocumentProperties(owner.Handle, hPrinter, printerSettings.PrinterName, devModeOutput, _
                                                printerSettings.GetHdevmode(), DM_IN_BUFFER Or DM_IN_PROMPT Or DM_OUT_BUFFER)
    NativeMethods.ClosePrinter(hPrinter)
    If ret1 <> IDOK Then Exit Sub

    printerSettings.SetHdevmode(devModeOutput)
  End Sub

  ' ---------------------------------------------------------------------------
  Private NotInheritable Class NativeMethods
    Private Sub New()
    End Sub
    Public Declare Auto Function OpenPrinter Lib "winspool.drv" (ByVal name As String, ByRef handle As IntPtr, ByVal defaults As IntPtr) As Integer
    Public Declare Function ClosePrinter Lib "winspool.drv" (ByVal handle As IntPtr) As Integer
    Public Declare Auto Function DocumentProperties Lib "winspool.drv" ( _
       ByVal hWnd As IntPtr, ByVal hPrinter As IntPtr, ByVal deviceName As String, _
       ByVal devModeOutput As IntPtr, ByVal devModeInput As IntPtr, ByVal mode As Integer) As Integer
  End Class

End Class