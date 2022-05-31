Public Class LabelSetpoint : Inherits Control

  Sub New()
    MyBase.New
    DoubleBuffered = True

    Size = New Size(120, 24)
  End Sub

  Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
    MyBase.OnPaintBackground(e)

    Using myBrush As New SolidBrush(BackColor)
      e.Graphics.FillRectangle(myBrush, 0, 0, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1)
    End Using
  End Sub

  Protected Overrides Sub OnPaint(e As PaintEventArgs)
    MyBase.OnPaint(e)

    Dim textLeft = Description
    Dim textMiddle = CurrentValue & Units
    Dim textRight = NewValue & Units

    Dim sfLeft = New StringFormat With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Center}
    Dim sfMiddle = New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
    Dim sfRight = New StringFormat With {.Alignment = StringAlignment.Far, .LineAlignment = StringAlignment.Center}

    Dim rectLeft = New Rectangle(0, 0, e.ClipRectangle.Width \ 2, e.ClipRectangle.Height - 1)
    Dim rectRight = New Rectangle(0, 0, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1)

    ' Draw background
    Using myBrush As New SolidBrush(BackColor)
      e.Graphics.FillRectangle(myBrush, 0, 0, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1)
    End Using

    ' Draw description and current value
    Using myBrush As New SolidBrush(ColorCurrent)
      e.Graphics.DrawString(textLeft, Font, myBrush, rectLeft, sfLeft)
      e.Graphics.DrawString(textMiddle, Font, myBrush, rectLeft, sfRight)
    End Using

    ' Draw new value
    Using myBrush As New SolidBrush(ColorNew)
      e.Graphics.DrawString(textRight, Font, myBrush, rectRight, sfRight)
    End Using

    ' Draw border if selected
    If Selected Then
      Using myPen As New Pen(ColorSelected)
        e.Graphics.DrawRectangle(myPen, 0, 0, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1)
      End Using
    End If
  End Sub

#Region " Properties "

  Private description_ As String
  <Category("Adaptive")>
  Property Description As String
    Get
      Return description_
    End Get
    Set(value As String)
      If value <> description_ Then
        description_ = value
        Invalidate()
      End If
    End Set
  End Property

  Private currentValue_ As String
  <Category("Adaptive")>
  Property CurrentValue As String
    Get
      Return currentValue_
    End Get
    Set(value As String)
      If value <> currentValue_ Then
        currentValue_ = value
        Invalidate()
      End If
    End Set
  End Property

  Private newValue_ As String
  <Category("Adaptive")>
  Property NewValue As String
    Get
      Return newValue_
    End Get
    Set(value As String)
      If value <> newValue_ Then
        newValue_ = value
        Invalidate()
      End If
    End Set
  End Property

  Private units_ As String
  <Category("Adaptive")>
  Property Units As String
    Get
      Return units_
    End Get
    Set(newValue As String)
      If newValue <> units_ Then
        units_ = newValue
        Invalidate()
      End If
    End Set
  End Property

  Private selected_ As Boolean
  <Category("Adaptive")>
  Property Selected As Boolean
    Get
      Return selected_
    End Get
    Set(value As Boolean)
      If value <> selected_ Then
        selected_ = value
        Invalidate()
      End If
    End Set
  End Property

  <Category("Adaptive")>
  Property ColorSelected As Color = Color.LightBlue

  <Category("Adaptive")>
  Property ColorCurrent As Color = Color.DarkGreen

  <Category("Adaptive")>
  Property ColorNew As Color = Color.DarkBlue


  <Category("Adaptive")>
  ReadOnly Property CurrentValueDouble As Double
    Get
      Dim tryDouble As Double
      If Double.TryParse(CurrentValue, tryDouble) Then Return tryDouble
      Return -1
    End Get
  End Property

  <Category("Adaptive")>
  ReadOnly Property NewValueDouble As Double
    Get
      Dim tryDouble As Double
      If Double.TryParse(NewValue, tryDouble) Then Return tryDouble
      Return -1
    End Get
  End Property

  <Category("Adaptive")>
  ReadOnly Property CurrentValueInteger As Integer
    Get
      Dim tryInteger As Integer
      If Integer.TryParse(CurrentValue, tryInteger) Then Return tryInteger
      Return -1
    End Get
  End Property

  <Category("Adaptive")>
  ReadOnly Property NewValueInteger As Integer
    Get
      Dim tryInteger As Integer
      If Integer.TryParse(NewValue, tryInteger) Then Return tryInteger
      Return -1
    End Get
  End Property

#End Region


#Region " Focus "

  'Protected Overrides Sub OnClick(ByVal e As System.EventArgs)
  '  ' TODO
  'End Sub

  'Protected Overrides Sub OnDoubleClick(ByVal e As System.EventArgs)
  '  ' TODO
  'End Sub

  'Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
  '  ' TODO
  'End Sub

  'Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
  '  ' TODO
  'End Sub

#End Region


End Class
