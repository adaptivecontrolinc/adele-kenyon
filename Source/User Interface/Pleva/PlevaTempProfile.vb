Public Class PlevaTempProfile
  Private controlWidth_ As Integer = 465
  Private controlHeigth_ As Integer = 220
  Private ellipseWidth_ As Integer = 20
  Private ellipseHeight_ As Integer = 20

  Private maximum_ As Integer = 4500, minimum_ As Integer = 0
  Private valueTemp1_ As Integer
  Private valueTemp2_ As Integer
  Private valueTemp3_ As Integer
  Private valueTemp4_ As Integer

  Private xPositionTemp1_ As Integer = 26
  Private xPositionTemp2_ As Integer = 151
  Private xPositionTemp3_ As Integer = 285
  Private xPositionTemp4_ As Integer = 410

  Private yPositionTemp1_ As Integer
  Private yPositionTemp2_ As Integer
  Private yPositionTemp3_ As Integer
  Private yPositionTemp4_ As Integer



  Public Sub ResizeControl(ByVal Width As Integer, ByVal Height As Integer, ByVal CircleDiameter As Integer)

    'Set control size properties
    controlHeigth_ = Height
    controlWidth_ = Width

    'Set ellipse size based on circle diameter
    ellipseHeight_ = CircleDiameter
    ellipseWidth_ = CircleDiameter

    'Redraw the user control
    Me.Refresh()

  End Sub

  Public Sub UpdateValues(ByVal Temp1 As Integer, ByVal Temp2 As Integer, ByVal Temp3 As Integer, ByVal Temp4 As Integer)

    valueTemp1_ = Temp1
    valueTemp2_ = Temp2
    valueTemp3_ = Temp3
    valueTemp4_ = Temp4

    yPositionTemp1_ = (controlHeigth_ - ellipseHeight_ \ 2) - MulDiv(controlHeigth_, valueTemp1_, (maximum_ - minimum_))
    yPositionTemp2_ = (controlHeigth_ - ellipseHeight_ \ 2) - MulDiv(controlHeigth_, valueTemp2_, (maximum_ - minimum_))
    yPositionTemp3_ = (controlHeigth_ - ellipseHeight_ \ 2) - MulDiv(controlHeigth_, valueTemp3_, (maximum_ - minimum_))
    yPositionTemp4_ = (controlHeigth_ - ellipseHeight_ \ 2) - MulDiv(controlHeigth_, valueTemp4_, (maximum_ - minimum_))

    'Redraw the user control
    Me.Refresh()

  End Sub

  Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
    MyBase.OnPaint(e)

    Using g As Graphics = e.Graphics

      'Draw Line Profiles
      DrawLineInt(e, xPositionTemp1_ + (ellipseHeight_ \ 2), yPositionTemp1_ + (ellipseWidth_ \ 2), xPositionTemp2_ + (ellipseHeight_ \ 2), yPositionTemp2_ + (ellipseWidth_ \ 2))
      DrawLineInt(e, xPositionTemp2_ + (ellipseHeight_ \ 2), yPositionTemp2_ + (ellipseWidth_ \ 2), xPositionTemp3_ + (ellipseHeight_ \ 2), yPositionTemp3_ + (ellipseWidth_ \ 2))
      DrawLineInt(e, xPositionTemp3_ + (ellipseHeight_ \ 2), yPositionTemp3_ + (ellipseWidth_ \ 2), xPositionTemp4_ + (ellipseHeight_ \ 2), yPositionTemp4_ + (ellipseWidth_ \ 2))

      'Draw Temp Profiles
      DrawTemp(e, xPositionTemp1_, yPositionTemp1_)
      DrawTemp(e, xPositionTemp2_, yPositionTemp2_)
      DrawTemp(e, xPositionTemp3_, yPositionTemp3_)
      DrawTemp(e, xPositionTemp4_, yPositionTemp4_)

      'Draw Temp Value Strings
      DrawStringFloat(e, valueTemp1_, xPositionTemp1_, yPositionTemp1_)
      DrawStringFloat(e, valueTemp2_, xPositionTemp2_, yPositionTemp2_)
      DrawStringFloat(e, valueTemp3_, xPositionTemp3_, yPositionTemp3_)
      DrawStringFloat(e, valueTemp4_, xPositionTemp4_, yPositionTemp4_)

    End Using


  End Sub

  Public Sub DrawTemp(ByVal e As PaintEventArgs, ByVal x As Integer, ByVal y As Integer)
    ' Create solid brush.
    Dim blueBrush As New SolidBrush(Color.Blue)
    ' Draw ellipse to screen.
    e.Graphics.FillEllipse(blueBrush, x, y, ellipseWidth_, ellipseHeight_)

  End Sub

  'http://msdn.microsoft.com/en-us/library/aa327558(VS.71).aspx
  Public Sub DrawLineInt(ByVal e As PaintEventArgs, ByVal x1 As Integer, ByVal y1 As Integer, _
                                                    ByVal x2 As Integer, ByVal y2 As Integer)
    ' Create pen.
    Dim blackPen As New Pen(Color.Black, 3)
    ' Draw line to screen.
    e.Graphics.DrawLine(blackPen, x1, y1, x2, y2)
  End Sub

  'http://msdn.microsoft.com/en-us/library/aa327575(VS.71).aspx
  Public Sub DrawStringFloat(ByVal e As PaintEventArgs, ByVal tempValue As Integer, _
                             ByVal xPosition As Integer, ByVal yPosition As Integer)
    ' Create string to draw.
    Dim drawString As [String] = (tempValue / 10).ToString & "F"
    ' Create font and brush.
    Dim drawFont As New Font("Tahoma", 11)
    Dim drawBrush As New SolidBrush(Color.Black)
    ' Create point for upper-left corner of drawing.
    Dim x As Single = xPosition - ((ellipseWidth_ + drawString.Length) \ 2)
    Dim y As Single
    If tempValue <= 3000 Then
      y = yPosition - (ellipseHeight_)
    Else
      y = yPosition + (ellipseHeight_)
    End If

    ' Draw string to screen.
    e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y)
  End Sub

#Region "Properties"

  <Description("The upper bound of the value property"), Category("Behavior"), DefaultValue(100)> Public Overridable Property Maximum() As Integer
    Get
      Return maximum_
    End Get
    Set(ByVal value As Integer)
      If maximum_ <> value Then maximum_ = value : Invalidate()
    End Set
  End Property

  <Description("The lower bound of the value property"), DefaultValue(0), Category("Behavior")> _
  Public Overridable Property Minimum() As Integer
    Get
      Return minimum_
    End Get
    Set(ByVal value As Integer)
      If minimum_ <> value Then minimum_ = value : Invalidate()
    End Set
  End Property

#End Region





End Class
