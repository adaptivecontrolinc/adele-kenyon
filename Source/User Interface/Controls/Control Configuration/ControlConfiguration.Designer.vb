<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ControlConfiguration
  Inherits System.Windows.Forms.UserControl

  'UserControl overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
    Me.gbAirTemp = New System.Windows.Forms.GroupBox()
    Me.ButtonSetAllTemps = New System.Windows.Forms.Button()
    Me.AirTemp_Zone8 = New SetpointAdjustThin()
    Me.AirTemp_Zone7 = New SetpointAdjustThin()
    Me.AirTemp_Zone6 = New SetpointAdjustThin()
    Me.AirTemp_Zone5 = New SetpointAdjustThin()
    Me.AirTemp_Zone4 = New SetpointAdjustThin()
    Me.AirTemp_Zone3 = New SetpointAdjustThin()
    Me.AirTemp_Zone2 = New SetpointAdjustThin()
    Me.AirTemp_Zone1 = New SetpointAdjustThin()
    Me.gbFanTop = New System.Windows.Forms.GroupBox()
    Me.ButtonSetAllFansTop = New System.Windows.Forms.Button()
    Me.FanTop_Zone8 = New SetpointAdjustThin()
    Me.FanTop_Zone7 = New SetpointAdjustThin()
    Me.FanTop_Zone6 = New SetpointAdjustThin()
    Me.FanTop_Zone5 = New SetpointAdjustThin()
    Me.FanTop_Zone4 = New SetpointAdjustThin()
    Me.FanTop_Zone3 = New SetpointAdjustThin()
    Me.FanTop_Zone2 = New SetpointAdjustThin()
    Me.FanTop_Zone1 = New SetpointAdjustThin()
    Me.gbTransport = New System.Windows.Forms.GroupBox()
    Me.Stripper = New SetpointAdjustThin()
    Me.Padder2 = New SetpointAdjustThin()
    Me.Conveyor = New SetpointAdjustThin()
    Me.Padder1 = New SetpointAdjustThin()
    Me.SelvageRight = New SetpointAdjustThin()
    Me.SelvageLeft = New SetpointAdjustThin()
    Me.OverfeedBottom = New SetpointAdjustThin()
    Me.OverfeedTop = New SetpointAdjustThin()
    Me.TenterRight = New SetpointAdjustThin()
    Me.TenterLeft = New SetpointAdjustThin()
    Me.TenterChain = New SetpointAdjustThin()
    Me.gbWidthScrew = New System.Windows.Forms.GroupBox()
    Me.ButtonSetAllWidths = New System.Windows.Forms.Button()
    Me.WidthScrew_5 = New SetpointAdjustThin()
    Me.WidthScrew_4 = New SetpointAdjustThin()
    Me.WidthScrew_3 = New SetpointAdjustThin()
    Me.WidthScrew_2 = New SetpointAdjustThin()
    Me.WidthScrew_1 = New SetpointAdjustThin()
    Me.buttonCancelJob = New System.Windows.Forms.Button()
    Me.BtnAdjustIncrement = New System.Windows.Forms.Button()
    Me.gbFanExhaust = New System.Windows.Forms.GroupBox()
    Me.FanExhaust2 = New SetpointAdjustThin()
    Me.FanExhaust1 = New SetpointAdjustThin()
    Me.TabControl1 = New System.Windows.Forms.TabControl()
    Me.TabPage1 = New System.Windows.Forms.TabPage()
    Me.ButtonPrintScreen = New System.Windows.Forms.Button()
    Me.ButtonSupervisor = New System.Windows.Forms.Button()
    Me.TabPage2 = New System.Windows.Forms.TabPage()
    Me.buttonCancelJob2 = New System.Windows.Forms.Button()
    Me.BtnAdjustIncrement2 = New System.Windows.Forms.Button()
    Me.gbFanBottom = New System.Windows.Forms.GroupBox()
    Me.ButtonSetAllFansBottom = New System.Windows.Forms.Button()
    Me.FanBottom_Zone8 = New SetpointAdjustThin()
    Me.FanBottom_Zone7 = New SetpointAdjustThin()
    Me.FanBottom_Zone6 = New SetpointAdjustThin()
    Me.FanBottom_Zone5 = New SetpointAdjustThin()
    Me.FanBottom_Zone4 = New SetpointAdjustThin()
    Me.FanBottom_Zone3 = New SetpointAdjustThin()
    Me.FanBottom_Zone2 = New SetpointAdjustThin()
    Me.FanBottom_Zone1 = New SetpointAdjustThin()
    Me.SwitchIncrement = New MimicControls.PowerSwitch()
    Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
    Me.gbAirTemp.SuspendLayout()
    Me.gbFanTop.SuspendLayout()
    Me.gbTransport.SuspendLayout()
    Me.gbWidthScrew.SuspendLayout()
    Me.gbFanExhaust.SuspendLayout()
    Me.TabControl1.SuspendLayout()
    Me.TabPage1.SuspendLayout()
    Me.TabPage2.SuspendLayout()
    Me.gbFanBottom.SuspendLayout()
    Me.SuspendLayout()
    '
    'Timer1
    '
    Me.Timer1.Enabled = True
    Me.Timer1.Interval = 10000
    '
    'gbAirTemp
    '
    Me.gbAirTemp.BackColor = System.Drawing.Color.Transparent
    Me.gbAirTemp.Controls.Add(Me.ButtonSetAllTemps)
    Me.gbAirTemp.Controls.Add(Me.AirTemp_Zone8)
    Me.gbAirTemp.Controls.Add(Me.AirTemp_Zone7)
    Me.gbAirTemp.Controls.Add(Me.AirTemp_Zone6)
    Me.gbAirTemp.Controls.Add(Me.AirTemp_Zone5)
    Me.gbAirTemp.Controls.Add(Me.AirTemp_Zone4)
    Me.gbAirTemp.Controls.Add(Me.AirTemp_Zone3)
    Me.gbAirTemp.Controls.Add(Me.AirTemp_Zone2)
    Me.gbAirTemp.Controls.Add(Me.AirTemp_Zone1)
    Me.gbAirTemp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.gbAirTemp.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.gbAirTemp.ForeColor = System.Drawing.Color.Blue
    Me.gbAirTemp.Location = New System.Drawing.Point(3, 166)
    Me.gbAirTemp.Name = "gbAirTemp"
    Me.gbAirTemp.Size = New System.Drawing.Size(1013, 160)
    Me.gbAirTemp.TabIndex = 1
    Me.gbAirTemp.TabStop = False
    '
    'ButtonSetAllTemps
    '
    Me.ButtonSetAllTemps.FlatStyle = System.Windows.Forms.FlatStyle.System
    Me.ButtonSetAllTemps.Font = New System.Drawing.Font("Tahoma", 12.0!)
    Me.ButtonSetAllTemps.Location = New System.Drawing.Point(931, 91)
    Me.ButtonSetAllTemps.Name = "ButtonSetAllTemps"
    Me.ButtonSetAllTemps.Size = New System.Drawing.Size(76, 63)
    Me.ButtonSetAllTemps.TabIndex = 153
    Me.ButtonSetAllTemps.Text = "Set All Temps"
    Me.ButtonSetAllTemps.UseVisualStyleBackColor = True
    '
    'AirTemp_Zone8
    '
    Me.AirTemp_Zone8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.AirTemp_Zone8.BackColor = System.Drawing.SystemColors.WindowText
    Me.AirTemp_Zone8.Increment = 0
    Me.AirTemp_Zone8.Index = 0
    Me.AirTemp_Zone8.IsSupervisor = False
    Me.AirTemp_Zone8.Location = New System.Drawing.Point(815, 15)
    Me.AirTemp_Zone8.Name = "AirTemp_Zone8"
    Me.AirTemp_Zone8.Size = New System.Drawing.Size(110, 140)
    Me.AirTemp_Zone8.TabIndex = 52
    '
    'AirTemp_Zone7
    '
    Me.AirTemp_Zone7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.AirTemp_Zone7.BackColor = System.Drawing.SystemColors.WindowText
    Me.AirTemp_Zone7.Increment = 0
    Me.AirTemp_Zone7.Index = 0
    Me.AirTemp_Zone7.IsSupervisor = False
    Me.AirTemp_Zone7.Location = New System.Drawing.Point(699, 15)
    Me.AirTemp_Zone7.Name = "AirTemp_Zone7"
    Me.AirTemp_Zone7.Size = New System.Drawing.Size(110, 140)
    Me.AirTemp_Zone7.TabIndex = 51
    '
    'AirTemp_Zone6
    '
    Me.AirTemp_Zone6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.AirTemp_Zone6.BackColor = System.Drawing.SystemColors.WindowText
    Me.AirTemp_Zone6.Increment = 0
    Me.AirTemp_Zone6.Index = 0
    Me.AirTemp_Zone6.IsSupervisor = False
    Me.AirTemp_Zone6.Location = New System.Drawing.Point(583, 15)
    Me.AirTemp_Zone6.Name = "AirTemp_Zone6"
    Me.AirTemp_Zone6.Size = New System.Drawing.Size(110, 140)
    Me.AirTemp_Zone6.TabIndex = 50
    '
    'AirTemp_Zone5
    '
    Me.AirTemp_Zone5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.AirTemp_Zone5.BackColor = System.Drawing.SystemColors.WindowText
    Me.AirTemp_Zone5.Increment = 0
    Me.AirTemp_Zone5.Index = 0
    Me.AirTemp_Zone5.IsSupervisor = False
    Me.AirTemp_Zone5.Location = New System.Drawing.Point(467, 15)
    Me.AirTemp_Zone5.Name = "AirTemp_Zone5"
    Me.AirTemp_Zone5.Size = New System.Drawing.Size(110, 140)
    Me.AirTemp_Zone5.TabIndex = 49
    '
    'AirTemp_Zone4
    '
    Me.AirTemp_Zone4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.AirTemp_Zone4.BackColor = System.Drawing.SystemColors.WindowText
    Me.AirTemp_Zone4.Increment = 0
    Me.AirTemp_Zone4.Index = 0
    Me.AirTemp_Zone4.IsSupervisor = False
    Me.AirTemp_Zone4.Location = New System.Drawing.Point(351, 15)
    Me.AirTemp_Zone4.Name = "AirTemp_Zone4"
    Me.AirTemp_Zone4.Size = New System.Drawing.Size(110, 140)
    Me.AirTemp_Zone4.TabIndex = 48
    '
    'AirTemp_Zone3
    '
    Me.AirTemp_Zone3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.AirTemp_Zone3.BackColor = System.Drawing.SystemColors.WindowText
    Me.AirTemp_Zone3.Increment = 0
    Me.AirTemp_Zone3.Index = 0
    Me.AirTemp_Zone3.IsSupervisor = False
    Me.AirTemp_Zone3.Location = New System.Drawing.Point(235, 15)
    Me.AirTemp_Zone3.Name = "AirTemp_Zone3"
    Me.AirTemp_Zone3.Size = New System.Drawing.Size(110, 140)
    Me.AirTemp_Zone3.TabIndex = 47
    '
    'AirTemp_Zone2
    '
    Me.AirTemp_Zone2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.AirTemp_Zone2.BackColor = System.Drawing.SystemColors.WindowText
    Me.AirTemp_Zone2.Increment = 0
    Me.AirTemp_Zone2.Index = 0
    Me.AirTemp_Zone2.IsSupervisor = False
    Me.AirTemp_Zone2.Location = New System.Drawing.Point(119, 15)
    Me.AirTemp_Zone2.Name = "AirTemp_Zone2"
    Me.AirTemp_Zone2.Size = New System.Drawing.Size(110, 140)
    Me.AirTemp_Zone2.TabIndex = 46
    '
    'AirTemp_Zone1
    '
    Me.AirTemp_Zone1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.AirTemp_Zone1.BackColor = System.Drawing.SystemColors.WindowText
    Me.AirTemp_Zone1.Increment = 0
    Me.AirTemp_Zone1.Index = 0
    Me.AirTemp_Zone1.IsSupervisor = False
    Me.AirTemp_Zone1.Location = New System.Drawing.Point(3, 15)
    Me.AirTemp_Zone1.Name = "AirTemp_Zone1"
    Me.AirTemp_Zone1.Size = New System.Drawing.Size(110, 140)
    Me.AirTemp_Zone1.TabIndex = 45
    '
    'gbFanTop
    '
    Me.gbFanTop.Controls.Add(Me.ButtonSetAllFansTop)
    Me.gbFanTop.Controls.Add(Me.FanTop_Zone8)
    Me.gbFanTop.Controls.Add(Me.FanTop_Zone7)
    Me.gbFanTop.Controls.Add(Me.FanTop_Zone6)
    Me.gbFanTop.Controls.Add(Me.FanTop_Zone5)
    Me.gbFanTop.Controls.Add(Me.FanTop_Zone4)
    Me.gbFanTop.Controls.Add(Me.FanTop_Zone3)
    Me.gbFanTop.Controls.Add(Me.FanTop_Zone2)
    Me.gbFanTop.Controls.Add(Me.FanTop_Zone1)
    Me.gbFanTop.Font = New System.Drawing.Font("Tahoma", 11.25!)
    Me.gbFanTop.ForeColor = System.Drawing.Color.Black
    Me.gbFanTop.Location = New System.Drawing.Point(3, 0)
    Me.gbFanTop.Name = "gbFanTop"
    Me.gbFanTop.Size = New System.Drawing.Size(1012, 160)
    Me.gbFanTop.TabIndex = 2
    Me.gbFanTop.TabStop = False
    '
    'ButtonSetAllFansTop
    '
    Me.ButtonSetAllFansTop.FlatStyle = System.Windows.Forms.FlatStyle.System
    Me.ButtonSetAllFansTop.Font = New System.Drawing.Font("Tahoma", 12.0!)
    Me.ButtonSetAllFansTop.Location = New System.Drawing.Point(930, 91)
    Me.ButtonSetAllFansTop.Name = "ButtonSetAllFansTop"
    Me.ButtonSetAllFansTop.Size = New System.Drawing.Size(76, 63)
    Me.ButtonSetAllFansTop.TabIndex = 155
    Me.ButtonSetAllFansTop.Text = "Set All Top Fans"
    Me.ButtonSetAllFansTop.UseVisualStyleBackColor = True
    '
    'FanTop_Zone8
    '
    Me.FanTop_Zone8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.FanTop_Zone8.BackColor = System.Drawing.Color.Transparent
    Me.FanTop_Zone8.Increment = 0
    Me.FanTop_Zone8.Index = 0
    Me.FanTop_Zone8.IsSupervisor = False
    Me.FanTop_Zone8.Location = New System.Drawing.Point(815, 14)
    Me.FanTop_Zone8.Name = "FanTop_Zone8"
    Me.FanTop_Zone8.Size = New System.Drawing.Size(110, 140)
    Me.FanTop_Zone8.TabIndex = 56
    '
    'FanTop_Zone7
    '
    Me.FanTop_Zone7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.FanTop_Zone7.BackColor = System.Drawing.Color.Transparent
    Me.FanTop_Zone7.Increment = 0
    Me.FanTop_Zone7.Index = 0
    Me.FanTop_Zone7.IsSupervisor = False
    Me.FanTop_Zone7.Location = New System.Drawing.Point(699, 14)
    Me.FanTop_Zone7.Name = "FanTop_Zone7"
    Me.FanTop_Zone7.Size = New System.Drawing.Size(110, 140)
    Me.FanTop_Zone7.TabIndex = 55
    '
    'FanTop_Zone6
    '
    Me.FanTop_Zone6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.FanTop_Zone6.BackColor = System.Drawing.Color.Transparent
    Me.FanTop_Zone6.Increment = 0
    Me.FanTop_Zone6.Index = 0
    Me.FanTop_Zone6.IsSupervisor = False
    Me.FanTop_Zone6.Location = New System.Drawing.Point(583, 14)
    Me.FanTop_Zone6.Name = "FanTop_Zone6"
    Me.FanTop_Zone6.Size = New System.Drawing.Size(110, 140)
    Me.FanTop_Zone6.TabIndex = 54
    '
    'FanTop_Zone5
    '
    Me.FanTop_Zone5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.FanTop_Zone5.BackColor = System.Drawing.Color.Transparent
    Me.FanTop_Zone5.Increment = 0
    Me.FanTop_Zone5.Index = 0
    Me.FanTop_Zone5.IsSupervisor = False
    Me.FanTop_Zone5.Location = New System.Drawing.Point(467, 14)
    Me.FanTop_Zone5.Name = "FanTop_Zone5"
    Me.FanTop_Zone5.Size = New System.Drawing.Size(110, 140)
    Me.FanTop_Zone5.TabIndex = 53
    '
    'FanTop_Zone4
    '
    Me.FanTop_Zone4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.FanTop_Zone4.BackColor = System.Drawing.Color.Transparent
    Me.FanTop_Zone4.Increment = 0
    Me.FanTop_Zone4.Index = 0
    Me.FanTop_Zone4.IsSupervisor = False
    Me.FanTop_Zone4.Location = New System.Drawing.Point(351, 14)
    Me.FanTop_Zone4.Name = "FanTop_Zone4"
    Me.FanTop_Zone4.Size = New System.Drawing.Size(110, 140)
    Me.FanTop_Zone4.TabIndex = 52
    '
    'FanTop_Zone3
    '
    Me.FanTop_Zone3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.FanTop_Zone3.BackColor = System.Drawing.Color.Transparent
    Me.FanTop_Zone3.Increment = 0
    Me.FanTop_Zone3.Index = 0
    Me.FanTop_Zone3.IsSupervisor = False
    Me.FanTop_Zone3.Location = New System.Drawing.Point(235, 14)
    Me.FanTop_Zone3.Name = "FanTop_Zone3"
    Me.FanTop_Zone3.Size = New System.Drawing.Size(110, 140)
    Me.FanTop_Zone3.TabIndex = 51
    '
    'FanTop_Zone2
    '
    Me.FanTop_Zone2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.FanTop_Zone2.BackColor = System.Drawing.Color.Transparent
    Me.FanTop_Zone2.Increment = 0
    Me.FanTop_Zone2.Index = 0
    Me.FanTop_Zone2.IsSupervisor = False
    Me.FanTop_Zone2.Location = New System.Drawing.Point(119, 14)
    Me.FanTop_Zone2.Name = "FanTop_Zone2"
    Me.FanTop_Zone2.Size = New System.Drawing.Size(110, 140)
    Me.FanTop_Zone2.TabIndex = 50
    '
    'FanTop_Zone1
    '
    Me.FanTop_Zone1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.FanTop_Zone1.BackColor = System.Drawing.Color.Transparent
    Me.FanTop_Zone1.Increment = 0
    Me.FanTop_Zone1.Index = 0
    Me.FanTop_Zone1.IsSupervisor = False
    Me.FanTop_Zone1.Location = New System.Drawing.Point(3, 14)
    Me.FanTop_Zone1.Name = "FanTop_Zone1"
    Me.FanTop_Zone1.Size = New System.Drawing.Size(110, 140)
    Me.FanTop_Zone1.TabIndex = 49
    '
    'gbTransport
    '
    Me.gbTransport.BackColor = System.Drawing.Color.Transparent
    Me.gbTransport.Controls.Add(Me.Stripper)
    Me.gbTransport.Controls.Add(Me.Padder2)
    Me.gbTransport.Controls.Add(Me.Conveyor)
    Me.gbTransport.Controls.Add(Me.Padder1)
    Me.gbTransport.Controls.Add(Me.SelvageRight)
    Me.gbTransport.Controls.Add(Me.SelvageLeft)
    Me.gbTransport.Controls.Add(Me.OverfeedBottom)
    Me.gbTransport.Controls.Add(Me.OverfeedTop)
    Me.gbTransport.Controls.Add(Me.TenterRight)
    Me.gbTransport.Controls.Add(Me.TenterLeft)
    Me.gbTransport.Controls.Add(Me.TenterChain)
    Me.gbTransport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.gbTransport.Font = New System.Drawing.Font("Tahoma", 11.25!)
    Me.gbTransport.ForeColor = System.Drawing.Color.Blue
    Me.gbTransport.Location = New System.Drawing.Point(3, 330)
    Me.gbTransport.Name = "gbTransport"
    Me.gbTransport.Size = New System.Drawing.Size(809, 307)
    Me.gbTransport.TabIndex = 4
    Me.gbTransport.TabStop = False
    '
    'Stripper
    '
    Me.Stripper.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.Stripper.BackColor = System.Drawing.SystemColors.WindowText
    Me.Stripper.Increment = 0
    Me.Stripper.Index = 0
    Me.Stripper.IsSupervisor = False
    Me.Stripper.Location = New System.Drawing.Point(696, 15)
    Me.Stripper.Name = "Stripper"
    Me.Stripper.Size = New System.Drawing.Size(110, 140)
    Me.Stripper.TabIndex = 58
    Me.Stripper.Visible = False
    '
    'Padder2
    '
    Me.Padder2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.Padder2.BackColor = System.Drawing.SystemColors.WindowText
    Me.Padder2.Increment = 0
    Me.Padder2.Index = 0
    Me.Padder2.IsSupervisor = False
    Me.Padder2.Location = New System.Drawing.Point(580, 161)
    Me.Padder2.Name = "Padder2"
    Me.Padder2.Size = New System.Drawing.Size(110, 140)
    Me.Padder2.TabIndex = 57
    '
    'Conveyor
    '
    Me.Conveyor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.Conveyor.BackColor = System.Drawing.SystemColors.WindowText
    Me.Conveyor.Increment = 0
    Me.Conveyor.Index = 0
    Me.Conveyor.IsSupervisor = False
    Me.Conveyor.Location = New System.Drawing.Point(464, 15)
    Me.Conveyor.Name = "Conveyor"
    Me.Conveyor.Size = New System.Drawing.Size(110, 140)
    Me.Conveyor.TabIndex = 56
    Me.Conveyor.Visible = False
    '
    'Padder1
    '
    Me.Padder1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.Padder1.BackColor = System.Drawing.SystemColors.WindowText
    Me.Padder1.Increment = 0
    Me.Padder1.Index = 0
    Me.Padder1.IsSupervisor = False
    Me.Padder1.Location = New System.Drawing.Point(580, 15)
    Me.Padder1.Name = "Padder1"
    Me.Padder1.Size = New System.Drawing.Size(110, 140)
    Me.Padder1.TabIndex = 55
    '
    'SelvageRight
    '
    Me.SelvageRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.SelvageRight.BackColor = System.Drawing.SystemColors.WindowText
    Me.SelvageRight.Increment = 0
    Me.SelvageRight.Index = 0
    Me.SelvageRight.IsSupervisor = False
    Me.SelvageRight.Location = New System.Drawing.Point(348, 161)
    Me.SelvageRight.Name = "SelvageRight"
    Me.SelvageRight.Size = New System.Drawing.Size(110, 140)
    Me.SelvageRight.TabIndex = 54
    '
    'SelvageLeft
    '
    Me.SelvageLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.SelvageLeft.BackColor = System.Drawing.SystemColors.WindowText
    Me.SelvageLeft.Increment = 0
    Me.SelvageLeft.Index = 0
    Me.SelvageLeft.IsSupervisor = False
    Me.SelvageLeft.Location = New System.Drawing.Point(348, 15)
    Me.SelvageLeft.Name = "SelvageLeft"
    Me.SelvageLeft.Size = New System.Drawing.Size(110, 140)
    Me.SelvageLeft.TabIndex = 53
    '
    'OverfeedBottom
    '
    Me.OverfeedBottom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.OverfeedBottom.BackColor = System.Drawing.SystemColors.WindowText
    Me.OverfeedBottom.Increment = 0
    Me.OverfeedBottom.Index = 0
    Me.OverfeedBottom.IsSupervisor = False
    Me.OverfeedBottom.Location = New System.Drawing.Point(232, 161)
    Me.OverfeedBottom.Name = "OverfeedBottom"
    Me.OverfeedBottom.Size = New System.Drawing.Size(110, 140)
    Me.OverfeedBottom.TabIndex = 52
    '
    'OverfeedTop
    '
    Me.OverfeedTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.OverfeedTop.BackColor = System.Drawing.SystemColors.WindowText
    Me.OverfeedTop.Increment = 0
    Me.OverfeedTop.Index = 0
    Me.OverfeedTop.IsSupervisor = False
    Me.OverfeedTop.Location = New System.Drawing.Point(232, 15)
    Me.OverfeedTop.Name = "OverfeedTop"
    Me.OverfeedTop.Size = New System.Drawing.Size(110, 140)
    Me.OverfeedTop.TabIndex = 51
    '
    'TenterRight
    '
    Me.TenterRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.TenterRight.BackColor = System.Drawing.SystemColors.WindowText
    Me.TenterRight.Increment = 0
    Me.TenterRight.Index = 0
    Me.TenterRight.IsSupervisor = False
    Me.TenterRight.Location = New System.Drawing.Point(119, 161)
    Me.TenterRight.Name = "TenterRight"
    Me.TenterRight.Size = New System.Drawing.Size(110, 140)
    Me.TenterRight.TabIndex = 50
    '
    'TenterLeft
    '
    Me.TenterLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.TenterLeft.BackColor = System.Drawing.SystemColors.WindowText
    Me.TenterLeft.Increment = 0
    Me.TenterLeft.Index = 0
    Me.TenterLeft.IsSupervisor = False
    Me.TenterLeft.Location = New System.Drawing.Point(119, 15)
    Me.TenterLeft.Name = "TenterLeft"
    Me.TenterLeft.Size = New System.Drawing.Size(110, 140)
    Me.TenterLeft.TabIndex = 49
    '
    'TenterChain
    '
    Me.TenterChain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.TenterChain.BackColor = System.Drawing.SystemColors.WindowText
    Me.TenterChain.Increment = 0
    Me.TenterChain.Index = 0
    Me.TenterChain.IsSupervisor = False
    Me.TenterChain.Location = New System.Drawing.Point(3, 15)
    Me.TenterChain.Name = "TenterChain"
    Me.TenterChain.Size = New System.Drawing.Size(110, 140)
    Me.TenterChain.TabIndex = 48
    '
    'gbWidthScrew
    '
    Me.gbWidthScrew.BackColor = System.Drawing.Color.Transparent
    Me.gbWidthScrew.Controls.Add(Me.ButtonSetAllWidths)
    Me.gbWidthScrew.Controls.Add(Me.WidthScrew_5)
    Me.gbWidthScrew.Controls.Add(Me.WidthScrew_4)
    Me.gbWidthScrew.Controls.Add(Me.WidthScrew_3)
    Me.gbWidthScrew.Controls.Add(Me.WidthScrew_2)
    Me.gbWidthScrew.Controls.Add(Me.WidthScrew_1)
    Me.gbWidthScrew.Font = New System.Drawing.Font("Tahoma", 11.25!)
    Me.gbWidthScrew.ForeColor = System.Drawing.Color.Blue
    Me.gbWidthScrew.Location = New System.Drawing.Point(3, 0)
    Me.gbWidthScrew.Name = "gbWidthScrew"
    Me.gbWidthScrew.Size = New System.Drawing.Size(1012, 160)
    Me.gbWidthScrew.TabIndex = 12
    Me.gbWidthScrew.TabStop = False
    '
    'ButtonSetAllWidths
    '
    Me.ButtonSetAllWidths.FlatStyle = System.Windows.Forms.FlatStyle.System
    Me.ButtonSetAllWidths.Font = New System.Drawing.Font("Tahoma", 12.0!)
    Me.ButtonSetAllWidths.Location = New System.Drawing.Point(931, 91)
    Me.ButtonSetAllWidths.Name = "ButtonSetAllWidths"
    Me.ButtonSetAllWidths.Size = New System.Drawing.Size(76, 63)
    Me.ButtonSetAllWidths.TabIndex = 154
    Me.ButtonSetAllWidths.Text = "Set All Widths"
    Me.ButtonSetAllWidths.UseVisualStyleBackColor = True
    '
    'WidthScrew_5
    '
    Me.WidthScrew_5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.WidthScrew_5.BackColor = System.Drawing.SystemColors.WindowText
    Me.WidthScrew_5.Increment = 0
    Me.WidthScrew_5.Index = 0
    Me.WidthScrew_5.IsSupervisor = False
    Me.WidthScrew_5.Location = New System.Drawing.Point(467, 15)
    Me.WidthScrew_5.Name = "WidthScrew_5"
    Me.WidthScrew_5.Size = New System.Drawing.Size(110, 140)
    Me.WidthScrew_5.TabIndex = 50
    '
    'WidthScrew_4
    '
    Me.WidthScrew_4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.WidthScrew_4.BackColor = System.Drawing.SystemColors.WindowText
    Me.WidthScrew_4.Increment = 0
    Me.WidthScrew_4.Index = 0
    Me.WidthScrew_4.IsSupervisor = False
    Me.WidthScrew_4.Location = New System.Drawing.Point(351, 15)
    Me.WidthScrew_4.Name = "WidthScrew_4"
    Me.WidthScrew_4.Size = New System.Drawing.Size(110, 140)
    Me.WidthScrew_4.TabIndex = 49
    '
    'WidthScrew_3
    '
    Me.WidthScrew_3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.WidthScrew_3.BackColor = System.Drawing.SystemColors.WindowText
    Me.WidthScrew_3.Increment = 0
    Me.WidthScrew_3.Index = 0
    Me.WidthScrew_3.IsSupervisor = False
    Me.WidthScrew_3.Location = New System.Drawing.Point(235, 15)
    Me.WidthScrew_3.Name = "WidthScrew_3"
    Me.WidthScrew_3.Size = New System.Drawing.Size(110, 140)
    Me.WidthScrew_3.TabIndex = 48
    '
    'WidthScrew_2
    '
    Me.WidthScrew_2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.WidthScrew_2.BackColor = System.Drawing.SystemColors.WindowText
    Me.WidthScrew_2.Increment = 0
    Me.WidthScrew_2.Index = 0
    Me.WidthScrew_2.IsSupervisor = False
    Me.WidthScrew_2.Location = New System.Drawing.Point(119, 15)
    Me.WidthScrew_2.Name = "WidthScrew_2"
    Me.WidthScrew_2.Size = New System.Drawing.Size(110, 140)
    Me.WidthScrew_2.TabIndex = 47
    '
    'WidthScrew_1
    '
    Me.WidthScrew_1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.WidthScrew_1.BackColor = System.Drawing.SystemColors.WindowText
    Me.WidthScrew_1.Increment = 0
    Me.WidthScrew_1.Index = 0
    Me.WidthScrew_1.IsSupervisor = False
    Me.WidthScrew_1.Location = New System.Drawing.Point(3, 15)
    Me.WidthScrew_1.Name = "WidthScrew_1"
    Me.WidthScrew_1.Size = New System.Drawing.Size(110, 140)
    Me.WidthScrew_1.TabIndex = 46
    '
    'buttonCancelJob
    '
    Me.buttonCancelJob.Font = New System.Drawing.Font("Tahoma", 12.0!)
    Me.buttonCancelJob.Location = New System.Drawing.Point(912, 518)
    Me.buttonCancelJob.Name = "buttonCancelJob"
    Me.buttonCancelJob.Size = New System.Drawing.Size(100, 50)
    Me.buttonCancelJob.TabIndex = 14
    Me.buttonCancelJob.Text = "Cancel Job"
    Me.buttonCancelJob.UseVisualStyleBackColor = True
    '
    'BtnAdjustIncrement
    '
    Me.BtnAdjustIncrement.FlatStyle = System.Windows.Forms.FlatStyle.System
    Me.BtnAdjustIncrement.Font = New System.Drawing.Font("Tahoma", 12.0!)
    Me.BtnAdjustIncrement.Location = New System.Drawing.Point(912, 573)
    Me.BtnAdjustIncrement.Name = "BtnAdjustIncrement"
    Me.BtnAdjustIncrement.Size = New System.Drawing.Size(100, 63)
    Me.BtnAdjustIncrement.TabIndex = 152
    Me.BtnAdjustIncrement.Text = "Adjust Increment: (0.1)"
    Me.BtnAdjustIncrement.UseVisualStyleBackColor = True
    '
    'gbFanExhaust
    '
    Me.gbFanExhaust.BackColor = System.Drawing.Color.Transparent
    Me.gbFanExhaust.Controls.Add(Me.FanExhaust2)
    Me.gbFanExhaust.Controls.Add(Me.FanExhaust1)
    Me.gbFanExhaust.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.gbFanExhaust.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.gbFanExhaust.ForeColor = System.Drawing.Color.Black
    Me.gbFanExhaust.Location = New System.Drawing.Point(3, 326)
    Me.gbFanExhaust.Name = "gbFanExhaust"
    Me.gbFanExhaust.Size = New System.Drawing.Size(229, 160)
    Me.gbFanExhaust.TabIndex = 41
    Me.gbFanExhaust.TabStop = False
    '
    'FanExhaust2
    '
    Me.FanExhaust2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.FanExhaust2.BackColor = System.Drawing.Color.Transparent
    Me.FanExhaust2.Increment = 0
    Me.FanExhaust2.Index = 0
    Me.FanExhaust2.IsSupervisor = False
    Me.FanExhaust2.Location = New System.Drawing.Point(116, 14)
    Me.FanExhaust2.Name = "FanExhaust2"
    Me.FanExhaust2.Size = New System.Drawing.Size(110, 140)
    Me.FanExhaust2.TabIndex = 53
    '
    'FanExhaust1
    '
    Me.FanExhaust1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.FanExhaust1.BackColor = System.Drawing.Color.Transparent
    Me.FanExhaust1.Increment = 0
    Me.FanExhaust1.Index = 0
    Me.FanExhaust1.IsSupervisor = False
    Me.FanExhaust1.Location = New System.Drawing.Point(3, 14)
    Me.FanExhaust1.Name = "FanExhaust1"
    Me.FanExhaust1.Size = New System.Drawing.Size(110, 140)
    Me.FanExhaust1.TabIndex = 52
    '
    'TabControl1
    '
    Me.TabControl1.Controls.Add(Me.TabPage1)
    Me.TabControl1.Controls.Add(Me.TabPage2)
    Me.TabControl1.Location = New System.Drawing.Point(3, 4)
    Me.TabControl1.Name = "TabControl1"
    Me.TabControl1.SelectedIndex = 0
    Me.TabControl1.Size = New System.Drawing.Size(1026, 671)
    Me.TabControl1.TabIndex = 153
    '
    'TabPage1
    '
    Me.TabPage1.BackColor = System.Drawing.Color.Black
    Me.TabPage1.Controls.Add(Me.ButtonPrintScreen)
    Me.TabPage1.Controls.Add(Me.ButtonSupervisor)
    Me.TabPage1.Controls.Add(Me.gbWidthScrew)
    Me.TabPage1.Controls.Add(Me.gbTransport)
    Me.TabPage1.Controls.Add(Me.buttonCancelJob)
    Me.TabPage1.Controls.Add(Me.BtnAdjustIncrement)
    Me.TabPage1.Controls.Add(Me.gbAirTemp)
    Me.TabPage1.Font = New System.Drawing.Font("Tahoma", 9.75!)
    Me.TabPage1.Location = New System.Drawing.Point(4, 24)
    Me.TabPage1.Name = "TabPage1"
    Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage1.Size = New System.Drawing.Size(1018, 643)
    Me.TabPage1.TabIndex = 0
    Me.TabPage1.Text = "Temps & Transport"
    '
    'ButtonPrintScreen
    '
    Me.ButtonPrintScreen.Font = New System.Drawing.Font("Tahoma", 12.0!)
    Me.ButtonPrintScreen.Location = New System.Drawing.Point(912, 393)
    Me.ButtonPrintScreen.Name = "ButtonPrintScreen"
    Me.ButtonPrintScreen.Size = New System.Drawing.Size(100, 50)
    Me.ButtonPrintScreen.TabIndex = 155
    Me.ButtonPrintScreen.Text = "Print"
    Me.ButtonPrintScreen.UseVisualStyleBackColor = True
    '
    'ButtonSupervisor
    '
    Me.ButtonSupervisor.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.ButtonSupervisor.Font = New System.Drawing.Font("Tahoma", 12.0!)
    Me.ButtonSupervisor.Location = New System.Drawing.Point(912, 449)
    Me.ButtonSupervisor.Name = "ButtonSupervisor"
    Me.ButtonSupervisor.Size = New System.Drawing.Size(100, 63)
    Me.ButtonSupervisor.TabIndex = 154
    Me.ButtonSupervisor.Text = "Supervisor"
    Me.ButtonSupervisor.UseVisualStyleBackColor = False
    '
    'TabPage2
    '
    Me.TabPage2.BackColor = System.Drawing.Color.Black
    Me.TabPage2.Controls.Add(Me.buttonCancelJob2)
    Me.TabPage2.Controls.Add(Me.BtnAdjustIncrement2)
    Me.TabPage2.Controls.Add(Me.gbFanBottom)
    Me.TabPage2.Controls.Add(Me.gbFanTop)
    Me.TabPage2.Controls.Add(Me.gbFanExhaust)
    Me.TabPage2.Font = New System.Drawing.Font("Tahoma", 9.75!)
    Me.TabPage2.Location = New System.Drawing.Point(4, 24)
    Me.TabPage2.Name = "TabPage2"
    Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage2.Size = New System.Drawing.Size(1018, 643)
    Me.TabPage2.TabIndex = 1
    Me.TabPage2.Text = "Fan Speeds"
    '
    'buttonCancelJob2
    '
    Me.buttonCancelJob2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.buttonCancelJob2.Location = New System.Drawing.Point(912, 518)
    Me.buttonCancelJob2.Name = "buttonCancelJob2"
    Me.buttonCancelJob2.Size = New System.Drawing.Size(100, 50)
    Me.buttonCancelJob2.TabIndex = 153
    Me.buttonCancelJob2.Text = "Cancel Job"
    Me.buttonCancelJob2.UseVisualStyleBackColor = True
    '
    'BtnAdjustIncrement2
    '
    Me.BtnAdjustIncrement2.FlatStyle = System.Windows.Forms.FlatStyle.System
    Me.BtnAdjustIncrement2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.BtnAdjustIncrement2.Location = New System.Drawing.Point(912, 574)
    Me.BtnAdjustIncrement2.Name = "BtnAdjustIncrement2"
    Me.BtnAdjustIncrement2.Size = New System.Drawing.Size(100, 63)
    Me.BtnAdjustIncrement2.TabIndex = 154
    Me.BtnAdjustIncrement2.Text = "Adjust Increment: (0.1)"
    Me.BtnAdjustIncrement2.UseVisualStyleBackColor = True
    '
    'gbFanBottom
    '
    Me.gbFanBottom.Controls.Add(Me.ButtonSetAllFansBottom)
    Me.gbFanBottom.Controls.Add(Me.FanBottom_Zone8)
    Me.gbFanBottom.Controls.Add(Me.FanBottom_Zone7)
    Me.gbFanBottom.Controls.Add(Me.FanBottom_Zone6)
    Me.gbFanBottom.Controls.Add(Me.FanBottom_Zone5)
    Me.gbFanBottom.Controls.Add(Me.FanBottom_Zone4)
    Me.gbFanBottom.Controls.Add(Me.FanBottom_Zone3)
    Me.gbFanBottom.Controls.Add(Me.FanBottom_Zone2)
    Me.gbFanBottom.Controls.Add(Me.FanBottom_Zone1)
    Me.gbFanBottom.Font = New System.Drawing.Font("Tahoma", 11.25!)
    Me.gbFanBottom.ForeColor = System.Drawing.Color.Black
    Me.gbFanBottom.Location = New System.Drawing.Point(3, 160)
    Me.gbFanBottom.Name = "gbFanBottom"
    Me.gbFanBottom.Size = New System.Drawing.Size(1012, 160)
    Me.gbFanBottom.TabIndex = 57
    Me.gbFanBottom.TabStop = False
    '
    'ButtonSetAllFansBottom
    '
    Me.ButtonSetAllFansBottom.FlatStyle = System.Windows.Forms.FlatStyle.System
    Me.ButtonSetAllFansBottom.Font = New System.Drawing.Font("Tahoma", 12.0!)
    Me.ButtonSetAllFansBottom.Location = New System.Drawing.Point(930, 91)
    Me.ButtonSetAllFansBottom.Name = "ButtonSetAllFansBottom"
    Me.ButtonSetAllFansBottom.Size = New System.Drawing.Size(76, 63)
    Me.ButtonSetAllFansBottom.TabIndex = 156
    Me.ButtonSetAllFansBottom.Text = "Set All " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Btm Fans"
    Me.ButtonSetAllFansBottom.UseVisualStyleBackColor = True
    '
    'FanBottom_Zone8
    '
    Me.FanBottom_Zone8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.FanBottom_Zone8.BackColor = System.Drawing.Color.Transparent
    Me.FanBottom_Zone8.Increment = 0
    Me.FanBottom_Zone8.Index = 0
    Me.FanBottom_Zone8.IsSupervisor = False
    Me.FanBottom_Zone8.Location = New System.Drawing.Point(815, 14)
    Me.FanBottom_Zone8.Name = "FanBottom_Zone8"
    Me.FanBottom_Zone8.Size = New System.Drawing.Size(110, 140)
    Me.FanBottom_Zone8.TabIndex = 56
    '
    'FanBottom_Zone7
    '
    Me.FanBottom_Zone7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.FanBottom_Zone7.BackColor = System.Drawing.Color.Transparent
    Me.FanBottom_Zone7.Increment = 0
    Me.FanBottom_Zone7.Index = 0
    Me.FanBottom_Zone7.IsSupervisor = False
    Me.FanBottom_Zone7.Location = New System.Drawing.Point(699, 14)
    Me.FanBottom_Zone7.Name = "FanBottom_Zone7"
    Me.FanBottom_Zone7.Size = New System.Drawing.Size(110, 140)
    Me.FanBottom_Zone7.TabIndex = 55
    '
    'FanBottom_Zone6
    '
    Me.FanBottom_Zone6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.FanBottom_Zone6.BackColor = System.Drawing.Color.Transparent
    Me.FanBottom_Zone6.Increment = 0
    Me.FanBottom_Zone6.Index = 0
    Me.FanBottom_Zone6.IsSupervisor = False
    Me.FanBottom_Zone6.Location = New System.Drawing.Point(583, 14)
    Me.FanBottom_Zone6.Name = "FanBottom_Zone6"
    Me.FanBottom_Zone6.Size = New System.Drawing.Size(110, 140)
    Me.FanBottom_Zone6.TabIndex = 54
    '
    'FanBottom_Zone5
    '
    Me.FanBottom_Zone5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.FanBottom_Zone5.BackColor = System.Drawing.Color.Transparent
    Me.FanBottom_Zone5.Increment = 0
    Me.FanBottom_Zone5.Index = 0
    Me.FanBottom_Zone5.IsSupervisor = False
    Me.FanBottom_Zone5.Location = New System.Drawing.Point(467, 14)
    Me.FanBottom_Zone5.Name = "FanBottom_Zone5"
    Me.FanBottom_Zone5.Size = New System.Drawing.Size(110, 140)
    Me.FanBottom_Zone5.TabIndex = 53
    '
    'FanBottom_Zone4
    '
    Me.FanBottom_Zone4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.FanBottom_Zone4.BackColor = System.Drawing.Color.Transparent
    Me.FanBottom_Zone4.Increment = 0
    Me.FanBottom_Zone4.Index = 0
    Me.FanBottom_Zone4.IsSupervisor = False
    Me.FanBottom_Zone4.Location = New System.Drawing.Point(351, 14)
    Me.FanBottom_Zone4.Name = "FanBottom_Zone4"
    Me.FanBottom_Zone4.Size = New System.Drawing.Size(110, 140)
    Me.FanBottom_Zone4.TabIndex = 52
    '
    'FanBottom_Zone3
    '
    Me.FanBottom_Zone3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.FanBottom_Zone3.BackColor = System.Drawing.Color.Transparent
    Me.FanBottom_Zone3.Increment = 0
    Me.FanBottom_Zone3.Index = 0
    Me.FanBottom_Zone3.IsSupervisor = False
    Me.FanBottom_Zone3.Location = New System.Drawing.Point(235, 14)
    Me.FanBottom_Zone3.Name = "FanBottom_Zone3"
    Me.FanBottom_Zone3.Size = New System.Drawing.Size(110, 140)
    Me.FanBottom_Zone3.TabIndex = 51
    '
    'FanBottom_Zone2
    '
    Me.FanBottom_Zone2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.FanBottom_Zone2.BackColor = System.Drawing.Color.Transparent
    Me.FanBottom_Zone2.Increment = 0
    Me.FanBottom_Zone2.Index = 0
    Me.FanBottom_Zone2.IsSupervisor = False
    Me.FanBottom_Zone2.Location = New System.Drawing.Point(119, 14)
    Me.FanBottom_Zone2.Name = "FanBottom_Zone2"
    Me.FanBottom_Zone2.Size = New System.Drawing.Size(110, 140)
    Me.FanBottom_Zone2.TabIndex = 50
    '
    'FanBottom_Zone1
    '
    Me.FanBottom_Zone1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.FanBottom_Zone1.BackColor = System.Drawing.Color.Transparent
    Me.FanBottom_Zone1.Increment = 0
    Me.FanBottom_Zone1.Index = 0
    Me.FanBottom_Zone1.IsSupervisor = False
    Me.FanBottom_Zone1.Location = New System.Drawing.Point(3, 14)
    Me.FanBottom_Zone1.Name = "FanBottom_Zone1"
    Me.FanBottom_Zone1.Size = New System.Drawing.Size(110, 140)
    Me.FanBottom_Zone1.TabIndex = 49
    '
    'SwitchIncrement
    '
    Me.SwitchIncrement.ForeColor = System.Drawing.Color.Black
    Me.SwitchIncrement.Location = New System.Drawing.Point(966, 574)
    Me.SwitchIncrement.Name = "SwitchIncrement"
    Me.SwitchIncrement.Size = New System.Drawing.Size(0, 0)
    Me.SwitchIncrement.TabIndex = 8
    '
    'PrintDialog1
    '
    Me.PrintDialog1.UseEXDialog = True
    '
    'ControlConfiguration
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BackColor = System.Drawing.Color.White
    Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
    Me.Controls.Add(Me.TabControl1)
    Me.Controls.Add(Me.SwitchIncrement)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
    Me.Name = "ControlConfiguration"
    Me.Size = New System.Drawing.Size(1026, 678)
    Me.gbAirTemp.ResumeLayout(False)
    Me.gbFanTop.ResumeLayout(False)
    Me.gbTransport.ResumeLayout(False)
    Me.gbWidthScrew.ResumeLayout(False)
    Me.gbFanExhaust.ResumeLayout(False)
    Me.TabControl1.ResumeLayout(False)
    Me.TabPage1.ResumeLayout(False)
    Me.TabPage2.ResumeLayout(False)
    Me.gbFanBottom.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents Timer1 As System.Windows.Forms.Timer
  Friend WithEvents gbAirTemp As System.Windows.Forms.GroupBox
  Friend WithEvents gbFanTop As System.Windows.Forms.GroupBox
  Friend WithEvents gbTransport As System.Windows.Forms.GroupBox
  Friend WithEvents SwitchIncrement As MimicControls.PowerSwitch
  Friend WithEvents gbWidthScrew As System.Windows.Forms.GroupBox
  Private WithEvents buttonCancelJob As System.Windows.Forms.Button
  Friend WithEvents BtnAdjustIncrement As System.Windows.Forms.Button
  Friend WithEvents gbFanExhaust As GroupBox
  Friend WithEvents TabControl1 As TabControl
  Friend WithEvents TabPage1 As TabPage
  Friend WithEvents TabPage2 As TabPage
  Friend WithEvents FanTop_Zone8 As SetpointAdjustThin
  Friend WithEvents FanTop_Zone7 As SetpointAdjustThin
  Friend WithEvents FanTop_Zone6 As SetpointAdjustThin
  Friend WithEvents FanTop_Zone5 As SetpointAdjustThin
  Friend WithEvents FanTop_Zone4 As SetpointAdjustThin
  Friend WithEvents FanTop_Zone3 As SetpointAdjustThin
  Friend WithEvents FanTop_Zone2 As SetpointAdjustThin
  Friend WithEvents FanTop_Zone1 As SetpointAdjustThin
  Friend WithEvents FanExhaust2 As SetpointAdjustThin
  Friend WithEvents FanExhaust1 As SetpointAdjustThin
  Friend WithEvents gbFanBottom As GroupBox
  Friend WithEvents FanBottom_Zone8 As SetpointAdjustThin
  Friend WithEvents FanBottom_Zone7 As SetpointAdjustThin
  Friend WithEvents FanBottom_Zone6 As SetpointAdjustThin
  Friend WithEvents FanBottom_Zone5 As SetpointAdjustThin
  Friend WithEvents FanBottom_Zone4 As SetpointAdjustThin
  Friend WithEvents FanBottom_Zone3 As SetpointAdjustThin
  Friend WithEvents FanBottom_Zone2 As SetpointAdjustThin
  Friend WithEvents FanBottom_Zone1 As SetpointAdjustThin
  Friend WithEvents AirTemp_Zone8 As SetpointAdjustThin
  Friend WithEvents AirTemp_Zone7 As SetpointAdjustThin
  Friend WithEvents AirTemp_Zone6 As SetpointAdjustThin
  Friend WithEvents AirTemp_Zone5 As SetpointAdjustThin
  Friend WithEvents AirTemp_Zone4 As SetpointAdjustThin
  Friend WithEvents AirTemp_Zone3 As SetpointAdjustThin
  Friend WithEvents AirTemp_Zone2 As SetpointAdjustThin
  Friend WithEvents AirTemp_Zone1 As SetpointAdjustThin
  Friend WithEvents WidthScrew_5 As SetpointAdjustThin
  Friend WithEvents WidthScrew_4 As SetpointAdjustThin
  Friend WithEvents WidthScrew_3 As SetpointAdjustThin
  Friend WithEvents WidthScrew_2 As SetpointAdjustThin
  Friend WithEvents WidthScrew_1 As SetpointAdjustThin
  Friend WithEvents TenterRight As SetpointAdjustThin
  Friend WithEvents TenterLeft As SetpointAdjustThin
  Friend WithEvents TenterChain As SetpointAdjustThin
  Friend WithEvents Stripper As SetpointAdjustThin
  Friend WithEvents Padder2 As SetpointAdjustThin
  Friend WithEvents Conveyor As SetpointAdjustThin
  Friend WithEvents Padder1 As SetpointAdjustThin
  Friend WithEvents SelvageRight As SetpointAdjustThin
  Friend WithEvents SelvageLeft As SetpointAdjustThin
  Friend WithEvents OverfeedBottom As SetpointAdjustThin
  Friend WithEvents OverfeedTop As SetpointAdjustThin
  Private WithEvents buttonCancelJob2 As Button
  Friend WithEvents BtnAdjustIncrement2 As Button
  Friend WithEvents ButtonSetAllTemps As Button
  Friend WithEvents ButtonSupervisor As Button
  Friend WithEvents ButtonSetAllWidths As Button
  Friend WithEvents ButtonSetAllFansTop As Button
  Friend WithEvents ButtonSetAllFansBottom As Button
  Private WithEvents ButtonPrintScreen As Button
  Friend WithEvents PrintDialog1 As PrintDialog
End Class
