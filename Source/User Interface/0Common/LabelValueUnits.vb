Public Class LabelValueUnits : Inherits Control

  Sub New()
    MyBase.New
    DoubleBuffered = True

    Size = New Size(120, 24)
  End Sub

  Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
    MyBase.OnPaintBackground(e)

    Using myBrush As New Drawing.SolidBrush(BackColor)
      e.Graphics.FillRectangle(myBrush, 0, 0, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1)
    End Using
  End Sub

  Protected Overrides Sub OnPaint(e As PaintEventArgs)
    MyBase.OnPaint(e)

    ' Draw background
    Using myBrush As New Drawing.SolidBrush(BackColor)
      e.Graphics.FillRectangle(myBrush, 0, 0, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1)
    End Using

    ' Draw text
    Using myBrush As New Drawing.SolidBrush(ForeColor)
      Dim sfLeft = New StringFormat With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Center}
      Dim sfRight = New StringFormat With {.Alignment = StringAlignment.Far, .LineAlignment = StringAlignment.Center}
      e.Graphics.DrawString(Description, Font, myBrush, e.ClipRectangle, sfLeft)
      e.Graphics.DrawString(Value & Units, Font, myBrush, e.ClipRectangle, sfRight)
    End Using
  End Sub

  Private description_ As String
  <Category("Adaptive")>
  Property Description As String
    Get
      Return description_
    End Get
    Set(newValue As String)
      If newValue <> description_ Then
        description_ = newValue
        Invalidate()
      End If
    End Set
  End Property

  Private value_ As String
  <Category("Adaptive")>
  Property Value As String
    Get
      Return value_
    End Get
    Set(newValue As String)
      If newValue <> value_ Then
        value_ = newValue
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

  <Category("Adaptive")>
  ReadOnly Property ValueDouble As Double
    Get
      Dim tryDouble As Double
      If Double.TryParse(Value, tryDouble) Then Return tryDouble
      Return -1
    End Get
  End Property

  <Category("Adaptive")>
  ReadOnly Property ValueInteger As Integer
    Get
      Dim tryInteger As Integer
      If Integer.TryParse(Value, tryInteger) Then Return tryInteger
      Return -1
    End Get
  End Property

End Class
