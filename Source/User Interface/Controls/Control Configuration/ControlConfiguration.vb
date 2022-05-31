'******************************************************************************************************
'******                                 Mimic Setup Properties                         ****************
'******************************************************************************************************
'Frame Configuration Setup for Allen-Bradley VersaView 1550M screen
' touchscreen : 1024x768 at 96pixels/inch

'Mimic
'VSD page size: 10.65in x 6.82in 
'PNG size: screen resoloution / 10.65x6.82
'Mimic Size: 1029pixels x 658pixels (no scroll bar when in expert on bottom or side)
'   I make it a drawable area of 
'      1024 x 703 in Normal mode
'      1024 x 676 in Expert mode


' Default background color used: 192, 255, 255
' Site color due to overhead lighting and touchscreen position/angle: black for contrast
'******************************************************************************************************
'******************************************************************************************************
Public Class ControlConfiguration

  Private airTemps As New List(Of SetpointAdjustThin)
  Private fanSpeeds As New List(Of SetpointAdjustThin)
  Private widthScrews As New List(Of SetpointAdjustThin)

  Private controlCode As ControlCode

  Public Sub New(ByVal controlCode As ControlCode)
    Me.controlCode = controlCode

    ' This call is required by the Windows Form Designer.
    InitializeComponent()


    ' Add any initialization after the InitializeComponent() call.
    InitializeControl()

  End Sub

  Public Sub InitializeControl()
    DoubleBuffered = True

    increment_ = 1
    BtnAdjustIncrement.Text = "Adjust Increment: (" & (increment_ / 10).ToString & ")"

    AirTemp_Zone1.Index = 1
    AirTemp_Zone2.Index = 2
    AirTemp_Zone3.Index = 3
    AirTemp_Zone4.Index = 4
    AirTemp_Zone5.Index = 5
    AirTemp_Zone6.Index = 6
    AirTemp_Zone7.Index = 7
    AirTemp_Zone8.Index = 8
    airTemps.Add(AirTemp_Zone1)
    airTemps.Add(AirTemp_Zone2)
    airTemps.Add(AirTemp_Zone3)
    airTemps.Add(AirTemp_Zone4)
    airTemps.Add(AirTemp_Zone5)
    airTemps.Add(AirTemp_Zone6)
    airTemps.Add(AirTemp_Zone7)
    airTemps.Add(AirTemp_Zone8)

    FanTop_Zone1.Index = 1
    FanTop_Zone2.Index = 2
    FanTop_Zone3.Index = 3
    FanTop_Zone4.Index = 4
    FanTop_Zone5.Index = 5
    FanTop_Zone6.Index = 6
    FanTop_Zone7.Index = 7
    FanTop_Zone8.Index = 8

    FanBottom_Zone1.Index = 11
    FanBottom_Zone2.Index = 12
    FanBottom_Zone3.Index = 13
    FanBottom_Zone4.Index = 14
    FanBottom_Zone5.Index = 15
    FanBottom_Zone6.Index = 16
    FanBottom_Zone7.Index = 17
    FanBottom_Zone8.Index = 18

    FanExhaust1.Index = 21
    FanExhaust2.Index = 22

    fanSpeeds.Add(FanTop_Zone1)
    fanSpeeds.Add(FanTop_Zone2)
    fanSpeeds.Add(FanTop_Zone3)
    fanSpeeds.Add(FanTop_Zone4)
    fanSpeeds.Add(FanTop_Zone5)
    fanSpeeds.Add(FanTop_Zone6)
    fanSpeeds.Add(FanTop_Zone7)
    fanSpeeds.Add(FanTop_Zone8)
    fanSpeeds.Add(FanBottom_Zone1)
    fanSpeeds.Add(FanBottom_Zone2)
    fanSpeeds.Add(FanBottom_Zone3)
    fanSpeeds.Add(FanBottom_Zone4)
    fanSpeeds.Add(FanBottom_Zone5)
    fanSpeeds.Add(FanBottom_Zone6)
    fanSpeeds.Add(FanBottom_Zone7)
    fanSpeeds.Add(FanBottom_Zone8)
    fanSpeeds.Add(FanExhaust1)
    fanSpeeds.Add(FanExhaust2)


    Padder1.Index = 1
    Padder2.Index = 2
    OverfeedTop.Index = 1
    OverfeedBottom.Index = 1
    SelvageLeft.Index = 1
    SelvageRight.Index = 1
    TenterChain.Index = 1
    TenterLeft.Index = 1
    TenterRight.Index = 1
    Conveyor.Index = 1
    Stripper.Index = 1

    'FanDucon.Index = 1


    WidthScrew_1.Index = 1
    WidthScrew_2.Index = 2
    WidthScrew_3.Index = 3
    WidthScrew_4.Index = 4
    WidthScrew_5.Index = 5

    widthScrews.Add(WidthScrew_1)
    widthScrews.Add(WidthScrew_2)
    widthScrews.Add(WidthScrew_3)
    widthScrews.Add(WidthScrew_4)
    widthScrews.Add(WidthScrew_5)


    If controlCode IsNot Nothing Then
      With controlCode

        'Air Temp Controllers - Honeywell
        For Each item As SetpointAdjustThin In airTemps
          item.Connect(.AirTemp_Zone(item.Index))
          item.Increment = increment_
        Next

        'Fan Speed Controllers - PLC Based
        For Each item As SetpointAdjustThin In fanSpeeds
          If item.Index <= 10 Then
            'Fan Top - Index 1-8
            item.Connect(.FanTop_Speed(item.Index))
            item.Increment = increment_
          ElseIf item.Index > 10 AndAlso item.Index <= 20 Then
            'Fan Bottom - Index 11-18
            item.Connect(.FanBottom_Speed(item.Index - 10))
            item.Increment = increment_
          Else
            'Fan Exhaust - Index 21-22
            item.Connect(.FanExhaust_Speed(item.Index - 20))
            item.Increment = increment_
          End If
        Next

        'Width Screw Controllers - Microspeed
        For Each item As SetpointAdjustThin In widthScrews
          item.Connect(.Width_Screw(item.Index))
          item.Increment = increment_
        Next


        ' TRANSPORT ITEMS
        Padder1.Connect(.Padder(1))
        Padder2.Connect(.Padder(2))
        OverfeedTop.Connect(.OverfeedTop)
        OverfeedBottom.Connect(.OverfeedBot)
        SelvageLeft.Connect(.SelvageLeft)
        SelvageRight.Connect(.SelvageRight)
        TenterChain.Connect(.Tenter)
        TenterLeft.Connect(.TenterLeft)
        TenterRight.Connect(.TenterRight)
        '       Conveyor.Connect(.Conveyor)
        '       Stripper.Connect(.Stripper)

        '       FanDucon.Connect(.FanDucon_Speed)


        '       Conveyor

        ' TODO
        '   Vacuum_Zone1.Connect(.Extractor_Speed(1))
        '   Vacuum_Zone2.Connect(.Extractor_Speed(2))

      End With
    End If

    Timer1.Interval = 2500
    Timer1.Enabled = True
  End Sub

  Private Sub Timer1_Tick_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    If controlCode Is Nothing Then Exit Sub

    ' Delay for the control code to startup completely 
    Static PowerUpTimer As New Timer With {.Seconds = 8}
    If Not PowerUpTimer.Finished Then Exit Sub


    If Runtime.Remoting.RemotingServices.IsTransparentProxy(controlCode) Then
      ' disable (maybe make invisible) some buttons, etc
      BtnAdjustIncrement.Visible = False

      For Each item As SetpointAdjustThin In airTemps
        item.ButtonIncrease.Visible = False
        item.ButtonDecrease.Visible = False
      Next
      For Each item As SetpointAdjustThin In fanSpeeds
        item.ButtonIncrease.Visible = False
        item.ButtonDecrease.Visible = False
      Next
      For Each item As SetpointAdjustThin In widthScrews
        item.ButtonIncrease.Visible = False
        item.ButtonDecrease.Visible = False
      Next

      Padder1.ButtonIncrease.Visible = False
      Padder1.ButtonDecrease.Visible = False
      Padder2.ButtonIncrease.Visible = False
      Padder2.ButtonDecrease.Visible = False
      OverfeedTop.ButtonIncrease.Visible = False
      OverfeedTop.ButtonDecrease.Visible = False
      OverfeedBottom.ButtonIncrease.Visible = False
      OverfeedBottom.ButtonDecrease.Visible = False
      SelvageLeft.ButtonIncrease.Visible = False
      SelvageLeft.ButtonDecrease.Visible = False
      SelvageRight.ButtonIncrease.Visible = False
      SelvageRight.ButtonDecrease.Visible = False
      TenterChain.ButtonIncrease.Visible = False
      TenterChain.ButtonDecrease.Visible = False
      TenterLeft.ButtonIncrease.Visible = False
      TenterLeft.ButtonDecrease.Visible = False
      TenterRight.ButtonIncrease.Visible = False
      TenterRight.ButtonDecrease.Visible = False
      Conveyor.ButtonIncrease.Visible = False
      Conveyor.ButtonDecrease.Visible = False
      Stripper.ButtonIncrease.Visible = False
      Stripper.ButtonDecrease.Visible = False

      buttonCancelJob.Visible = False
      ButtonSupervisor.Visible = False
    End If

    If controlCode IsNot Nothing Then
      If Me.Visible Then

        With controlCode

          'Air Temp Controllers - Honeywell
          For Each item As SetpointAdjustThin In airTemps
            'Verify that the honeywell setpoint adjust function is enabled from parameters
            If .Parameters.HoneywellSetpointAdjustEnable <> 1 Then
              item.ButtonDecrease.Visible = False
              item.ButtonIncrease.Visible = False
            Else
              item.ButtonDecrease.Visible = True
              item.ButtonIncrease.Visible = True
            End If
            'Refresh Values/Display
            item.PicBox.Visible = True
            item.RefreshValues(.User.IsSupervisor)
          Next

          'Fan Speed Controllers - Microspeed
          For Each item As SetpointAdjustThin In fanSpeeds
            item.PicBox.Visible = False
            item.RefreshValues(.User.IsSupervisor)
          Next

          'Width Screw Controllers - Microspeed
          For Each item As SetpointAdjustThin In widthScrews
            item.PicBox.Visible = False
            item.RefreshValues(.User.IsSupervisor)
          Next

          'Transport Controllers - Microspeed
          Padder1.PicBox.Visible = False
          Padder1.RefreshValues(.User.IsSupervisor)

          Padder2.PicBox.Visible = False
          Padder2.RefreshValues(.User.IsSupervisor)

          OverfeedTop.PicBox.Visible = False
          OverfeedTop.RefreshValues(.User.IsSupervisor)

          OverfeedBottom.PicBox.Visible = False
          OverfeedBottom.RefreshValues(.User.IsSupervisor)

          SelvageLeft.PicBox.Visible = False
          SelvageLeft.RefreshValues(.User.IsSupervisor)

          SelvageRight.PicBox.Visible = False
          SelvageRight.RefreshValues(.User.IsSupervisor)

          TenterChain.PicBox.Visible = False
          TenterChain.RefreshValues(.User.IsSupervisor)

          TenterLeft.PicBox.Visible = False
          TenterLeft.RefreshValues(.User.IsSupervisor)

          TenterRight.PicBox.Visible = False
          TenterRight.RefreshValues(.User.IsSupervisor)

          Conveyor.PicBox.Visible = False
          Conveyor.RefreshValues(.User.IsSupervisor)

          Stripper.PicBox.Visible = False
          Stripper.RefreshValues(.User.IsSupervisor)

          '      FanDucon.PicBox.Visible = False
          '      FanDucon.RefreshValues(.User.IsSupervisor)

          '      Vacuum_Zone1.PicBox.Visible = False
          '      Vacuum_Zone1.RefreshValues()
          '      Vacuum_Zone2.PicBox.Visible = False
          '      Vacuum_Zone2.RefreshValues()

          'Pleva Values
          '          lbPlevaTemp1.Value = .Graph_PlevaTemp1 / 10
          '          lbPlevaTemp2.Value = .Graph_PlevaTemp2 / 10
          '          lbPlevaTemp3.Value = .Graph_PlevaTemp3 / 10
          '          lbPlevaTemp4.Value = .Graph_PlevaTemp4 / 10
          '          lbPlevaHumidity.Value = .Graph_PlevaHumidity / 10


          If .User.IsSupervisor Then
            ButtonSupervisor.BackColor = ButtonColorOn
          Else
            ButtonSupervisor.BackColor = ButtonColorOff
          End If

        End With
      End If
    Else
      Exit Sub
    End If

  End Sub

  Private Function Message(ByVal text As String) As Boolean
    MessageBox.Show(text.PadRight(64), "Adaptive Control", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
    Return False
  End Function

  Sub buttonCancelJob_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonCancelJob.Click
    If controlCode Is Nothing Then Exit Sub
    With controlCode
      If .Parent.IsProgramRunning Then .Parent.StopJob()
    End With
  End Sub

  Private Sub buttonCancelJob2_Click(sender As Object, e As EventArgs) Handles buttonCancelJob2.Click
    If controlCode Is Nothing Then Exit Sub
    With controlCode
      If .Parent.IsProgramRunning Then .Parent.StopJob()
    End With
  End Sub


  Private Sub BtnAdjustIncrement_Click(sender As Object, e As EventArgs) Handles BtnAdjustIncrement.Click
    AdjustIncrement()
  End Sub
  Private Sub BtnAdjustIncrement2_Click(sender As Object, e As EventArgs) Handles BtnAdjustIncrement2.Click
    AdjustIncrement()
  End Sub

  Private increment_ As Integer = 0
  Private Sub AdjustIncrement()
    If controlCode Is Nothing Then Exit Sub

    If controlCode.Parameters.SetpointIncrementEnable > 0 Then
      Select Case increment_
        Case 0
          increment_ = 1
        Case 1
          increment_ = 5
        Case 5
          increment_ = 10
        Case 10
          increment_ = 100
        Case 100
          increment_ = 1
      End Select

      'Air Temp Controllers - Honeywell
      For Each item As SetpointAdjustThin In airTemps
        item.Increment = increment_
      Next

      'Fan Speed Controllers - Microspeed
      For Each item As SetpointAdjustThin In fanSpeeds
        item.Increment = increment_
      Next

      'Width Screw Controllers - Microspeed
      For Each item As SetpointAdjustThin In widthScrews
        item.Increment = increment_
      Next

      'Transport Controllers - Microspeed
      Padder1.Increment = increment_
      Padder2.Increment = increment_
      OverfeedTop.Increment = increment_
      OverfeedBottom.Increment = increment_
      SelvageLeft.Increment = increment_
      SelvageRight.Increment = increment_
      TenterChain.Increment = increment_
      TenterLeft.Increment = increment_
      TenterRight.Increment = increment_
      Conveyor.Increment = increment_
      Stripper.Increment = increment_

      BtnAdjustIncrement.Text = "Adjust Increment: (" & (increment_ / 10).ToString & ")"
      BtnAdjustIncrement2.Text = "Adjust Increment: (" & (increment_ / 10).ToString & ")"

    Else
      Me.Message("Parameter 'Setpoint Increment Enable' not enabled.  Setpoint increment cannot be adjusted.")
    End If
  End Sub

  Private Sub ButtonSupervisor_Click(sender As Object, e As EventArgs) Handles ButtonSupervisor.Click
    If controlCode Is Nothing Then Exit Sub

    With controlCode
      If .User.IsSupervisor Then
        .User.SetOperator()
        ButtonSupervisor.BackColor = ButtonColorOff
      Else
        .User.Load() ' reload user data - supervisor password may have changed
        If .User.Supervisor > 0 Then
          Using newform As New FormPassword
            newform.Text = "Enter Supervisor Password"
            newform.Password = controlCode.User.UserData.Supervisor
            If newform.ShowDialog(Me) = DialogResult.OK Then
              .User.SetSupervisor()
              ButtonSupervisor.BackColor = ButtonColorOn
            End If
          End Using
        Else
          ' Supervisor password is disabled
          .User.SetSupervisor()
          ButtonSupervisor.BackColor = ButtonColorOn
        End If

      End If
    End With
  End Sub

  Private Sub ButtonSetAllTemps_Click(sender As Object, e As EventArgs) Handles ButtonSetAllTemps.Click
    If controlCode Is Nothing Then Exit Sub

    With controlCode
      If .Parameters.HoneywellSetpointAdjustEnable >= 1 Then
        Dim setpointMin As Integer = .AirTemp_Zone(1).LimitLower
        Dim setpointMax As Integer = .AirTemp_Zone(1).LimitUpper
        Dim setpointCurrent As Integer = .AirTemp_Zone(1).RemoteValue

        If .User.IsSupervisor Then
          ' Display and use Parameter Maximum and Minimum Setpoints
          setpointMin = .Parameters.HoneywellSetpointMin
          setpointMax = .Parameters.HoneywellSetpointMax
        End If

        ' Use keyboard form to enable updating all setpoints at one time
        Using newKeyPad As New FormKeypad("Adjust All Temps", setpointMin, setpointMax, setpointCurrent, "F", 10)
          newKeyPad.ShowDialog()
          If newKeyPad.WasConfirmed Then
            Dim response As Boolean = True

            ' Honeywell Controllers
            For i As Integer = 1 To .AirTemp_Zone.Length - 1
              With .AirTemp_Zone(i)
                If .IChangeSetpoint(newKeyPad.NewSetpoint, controlCode.User.IsSupervisor) Then
                Else : response = False
                End If
              End With
            Next i

            newKeyPad.Close()
          ElseIf newKeyPad.WasCancelled Then
            newKeyPad.Close()
          End If
        End Using
      Else
        Message("Feature Not Currently Enabled")
      End If
    End With
  End Sub

  Private Sub ButtonSetAllWidths_Click(sender As Object, e As EventArgs) Handles ButtonSetAllWidths.Click
    If controlCode Is Nothing Then Exit Sub
    With controlCode
      If .Parameters.WidthScrewAdjustEnabled >= 1 Then

        Dim setpointMin As Integer = .Width_Screw(3).LimitLower
        Dim setpointMax As Integer = .Width_Screw(3).LimitUpper
        Dim setpointCurrent As Integer = .Width_Screw(3).ActiveSetpointValue

        If .User.IsSupervisor Then
          ' Display and use Parameter Maximum and Minimum Setpoints
          setpointMin = .Parameters.WidthScrewSetpointMin
          setpointMax = .Parameters.WidthScrewSetpointMax
        End If

        ' Use keyboard form to enable updating all setpoints at one time
        Using newKeyPad As New FormKeypad("Adjust Width Screws", setpointMin, setpointMax, setpointCurrent, "in", 10)
          newKeyPad.ShowDialog()
          If newKeyPad.WasConfirmed Then
            Dim response As Boolean = True

            ' Update all Width Screws:
            'For i As Integer = 1 To .Width_Screw.Length - 1
            '  With .Width_Screw(i)
            '    If .ChangeSetpoint(newKeyPad.NewSetpoint, controlCode.User.IsSupervisor) Then
            '    Else : response = False
            '    End If
            '  End With
            'Next i
            ' 2022-04-19 - Only update width screws: 3, 4, & 5 
            For i As Integer = 3 To 5
              With .Width_Screw(i)
                If .IChangeSetpoint(newKeyPad.NewSetpoint, controlCode.User.IsSupervisor) Then
                Else : response = False
                End If
              End With
            Next i

            newKeyPad.Close()
          ElseIf newKeyPad.WasCancelled Then
            newKeyPad.Close()
          End If
        End Using


#If 0 Then
        ' TODO
       ' Polartec Kenyon method 
        Dim activeSV(5) As Integer
        Dim minSetpoint(5) As Integer
        Dim maxSetpoint(5) As Integer
        For i As Integer = 1 To .Width_Screw.Length - 1
          activeSV(i) = .Width_Screw(i).ActiveSetpointValue

          minSetpoint(i) = .Width_Screw(i).LimitLower
          maxSetpoint(i) = .Width_Screw(i).LimitUpper
        Next i

        Using newForm As New FormMimicWidths
          newForm.Setup("Set Widths - inches", "  """, activeSV(1), activeSV(2), activeSV(3), activeSV(4), activeSV(5))
          For i As Integer = 1 To 5
            newForm.SetupLimits(i, minSetpoint(i), maxSetpoint(i))
          Next i

          If newForm.ShowDialog(Me) = DialogResult.OK Then
            For i As Integer = 1 To 5

              If newForm.ValueInteger(i) >= 0 AndAlso newForm.ValueChecked(i) Then
                controlCode.Width_Screw(i).ChangeSetpoint(newForm.ValueInteger(i) * 10, controlCode.User.IsSupervisor)
              End If
            Next i
          End If
        End Using
#End If

      Else
        Message("Feature Not Currently Enabled")
      End If

    End With
  End Sub

  Private Sub ButtonSetAllFansTop_Click(sender As Object, e As EventArgs) Handles ButtonSetAllFansTop.Click
    If controlCode Is Nothing Then Exit Sub

    With controlCode
      If .Parameters.FanSpeedAdjustEnabled >= 1 Then
        Dim setpointMin As Integer = .FanTop_Speed(1).SetpointMinimum   '
        Dim setpointMax As Integer = .FanTop_Speed(1).SetpointMaximum
        Dim setpointCurrent As Integer = .FanTop_Speed(1).SetpointActual

        If .User.IsSupervisor Then
          ' Display and use Parameter Maximum and Minimum Setpoints
          setpointMin = .Parameters.FanSpeedSetpointMin
          setpointMax = .Parameters.FanSpeedSetpointMax
        End If

        ' Use keyboard form to enable updating all setpoints at one time
        Using newKeyPad As New FormKeypad("Adjust All Top Fans", setpointMin, setpointMax, setpointCurrent, "%", 10)
          newKeyPad.ShowDialog()
          If newKeyPad.WasConfirmed Then
            Dim response As Boolean = True

            ' Top Fan Speeds
            For i As Integer = 1 To .FanTop_Speed.Length - 1
              With .FanTop_Speed(i)
                If .IChangeSetpoint(newKeyPad.NewSetpoint, controlCode.User.IsSupervisor) Then
                Else : response = False
                End If
              End With
            Next i

            newKeyPad.Close()
          ElseIf newKeyPad.WasCancelled Then
            newKeyPad.Close()
          End If
        End Using

      Else
        Message("Feature Not Currently Enabled")
      End If
    End With
  End Sub

  Private Sub ButtonSetAllFansBottom_Click(sender As Object, e As EventArgs) Handles ButtonSetAllFansBottom.Click
    If controlCode Is Nothing Then Exit Sub

    With controlCode
      If .Parameters.FanSpeedAdjustEnabled >= 1 Then
        Dim setpointMin As Integer = .FanBottom_Speed(1).SetpointMinimum   '
        Dim setpointMax As Integer = .FanBottom_Speed(1).SetpointMaximum
        Dim setpointCurrent As Integer = .FanBottom_Speed(1).SetpointActual

        If .User.IsSupervisor Then
          ' Display and use Parameter Maximum and Minimum Setpoints
          setpointMin = .Parameters.FanSpeedSetpointMin
          setpointMax = .Parameters.FanSpeedSetpointMax
        End If

        ' Use keyboard form to enable updating all setpoints at one time
        Using newKeyPad As New FormKeypad("Adjust All Bottom Fans", setpointMin, setpointMax, setpointCurrent, "%", 10)
          newKeyPad.ShowDialog()
          If newKeyPad.WasConfirmed Then
            Dim response As Boolean = True

            ' Bottom Fan Speeds
            For i As Integer = 1 To .FanBottom_Speed.Length - 1
              With .FanBottom_Speed(i)
                If .IChangeSetpoint(newKeyPad.NewSetpoint, controlCode.User.IsSupervisor) Then
                Else : response = False
                End If
              End With
            Next i

            newKeyPad.Close()
          ElseIf newKeyPad.WasCancelled Then
            newKeyPad.Close()
          End If
        End Using

      Else
        Message("Feature Not Currently Enabled")
      End If
    End With
  End Sub


  ' Declare the PrintDocument object.
  Private WithEvents docToPrint As New Printing.PrintDocument

  Private Sub ButtonPrintScreen_Click(sender As Object, e As EventArgs) Handles ButtonPrintScreen.Click
    '  SendKeys.Send("%{PRTSC}")
    '  Dim ScreenShot As Image = Clipboard.GetImage
    '  ScreenShot.Save("c:\ScreenShot.jpg", System.Drawing.Imaging.ImageFormat.Jpeg)

    Dim bounds As Rectangle
    Dim screenshot As System.Drawing.Bitmap
    Dim graph As Graphics
    bounds = Screen.PrimaryScreen.Bounds
    screenshot = New System.Drawing.Bitmap(bounds.Width, bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb)
    graph = Graphics.FromImage(screenshot)
    graph.CopyFromScreen(0, 0, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy)
    '   screenshot.Save("C:\print.jpg", Imaging.ImageFormat.Bmp)



#If 0 Then
        
    Dim batch As String = ""
    Dim programName As String = ""
    Dim programNumber As Integer = 0
    If controlCode.Parent.IsProgramRunning Then
      With controlCode.Parent
        batch = .Job
        programName = .ProgramName
        programNumber = .ProgramNumber
      End With
    End If

    Dim doc = New PrintControlsDocument(graph, "", False, "") With {.DocumentName = "Batch: " & batch}
    
    Using frm = New PrintPreviewDialog With {.Document = doc}
      frm.ShowDialog()
    End Using

    'Print the Document
    '    doc.Print()
       
    'Print Preview
    Using frm = New PrintPreviewDialog With {.Document = doc} ' , .WindowState = FormWindowState.Maximized}
      frm.ShowDialog()
    End Using
         
    ' Allow the user to choose the page range he or she would
    ' like to print.
    PrintDialog1.AllowSomePages = True

    ' Show the help button.
    PrintDialog1.ShowHelp = True

    ' Set the Document property to the PrintDocument for 
    ' which the PrintPage Event has been handled. To display the
    ' dialog, either this property or the PrinterSettings property 
    ' must be set 
    PrintDialog1.Document = docToPrint

    Dim result As DialogResult = PrintDialog1.ShowDialog()

    ' If the result is OK then print the document.
    If (result = DialogResult.OK) Then
      docToPrint.Print()
    End If

#End If

  End Sub

  ' The PrintDialog will print the document
  ' by handling the document's PrintPage event.
  Private Sub document_PrintPage(ByVal sender As Object,
     ByVal e As System.Drawing.Printing.PrintPageEventArgs) _
         Handles docToPrint.PrintPage

    ' Insert code to render the page here.
    ' This code will be called when the control is drawn.

    ' The following code will render a simple
    ' message on the printed document.
    Dim text As String = "In document_PrintPage method."
    Dim printFont As New System.Drawing.Font("Arial", 35, System.Drawing.FontStyle.Regular)

    ' Draw the content.
    e.Graphics.DrawString(text, printFont, System.Drawing.Brushes.Black, 10, 10)

  End Sub
End Class
