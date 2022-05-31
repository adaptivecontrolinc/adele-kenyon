<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PlevaControl
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
    Me.gpPlevaConfiguration = New System.Windows.Forms.GroupBox()
    Me.lbHumidityActual = New MimicControls.Label()
    Me.lbFabricDistanceAtTemp = New MimicControls.Label()
    Me.lbTimeActual = New MimicControls.Label()
    Me.btnAdjustHumidity = New System.Windows.Forms.Button()
    Me.lbPlevaHumidityStatus = New MimicControls.Label()
    Me.btnPlevaHumidityEnabled = New System.Windows.Forms.Button()
    Me.lbPlevaTempStatus = New MimicControls.Label()
    Me.btnPlevaTempEnabled = New System.Windows.Forms.Button()
    Me.btnAdjustTime = New System.Windows.Forms.Button()
    Me.btnAdjustTemp = New System.Windows.Forms.Button()
    Me.vlTimeDesired = New MimicControls.ValueLabel()
    Me.vlHumidityDesired = New MimicControls.ValueLabel()
    Me.btnHumidityInc = New System.Windows.Forms.Button()
    Me.btnTimeInc = New System.Windows.Forms.Button()
    Me.btnHumidityDec = New System.Windows.Forms.Button()
    Me.btnTimeDec = New System.Windows.Forms.Button()
    Me.btnTempDec = New System.Windows.Forms.Button()
    Me.btnTempInc = New System.Windows.Forms.Button()
    Me.vlTempDesired = New MimicControls.ValueLabel()
    Me.vlGasFlowRate = New MimicControls.ValueLabel()
    Me.vlGasUsedCubicFeet = New MimicControls.ValueLabel()
    Me.vlFanExhaustSpeed2 = New MimicControls.ValueLabel()
    Me.vlFabricSpeedLimitHigh = New MimicControls.ValueLabel()
    Me.vlFabricSpeedLimitLow = New MimicControls.ValueLabel()
    Me.vlFanExhaustSpeed1 = New MimicControls.ValueLabel()
    Me.outputZone6 = New MimicControls.ValueLabel()
    Me.outputZone5 = New MimicControls.ValueLabel()
    Me.outputZone4 = New MimicControls.ValueLabel()
    Me.outputZone3 = New MimicControls.ValueLabel()
    Me.outputZone2 = New MimicControls.ValueLabel()
    Me.lbAirZone5 = New MimicControls.Label()
    Me.lbAirZone6 = New MimicControls.Label()
    Me.Label5 = New MimicControls.Label()
    Me.lbAirZone3 = New MimicControls.Label()
    Me.lbAirZone2 = New MimicControls.Label()
    Me.lbAirZone4 = New MimicControls.Label()
    Me.lbAirZone1 = New MimicControls.Label()
    Me.outputZone1 = New MimicControls.ValueLabel()
    Me.PlevaTempProfile1 = New PlevaTempProfile()
    Me.vlFabricSpeed = New MimicControls.ValueLabel()
    Me.vlPlevaTemp4 = New MimicControls.ValueLabel()
    Me.vlPlevaTemp3 = New MimicControls.ValueLabel()
    Me.vlPlevaTemp2 = New MimicControls.ValueLabel()
    Me.vlPlevaTemp1 = New MimicControls.ValueLabel()
    Me.lbGasUsedDecatherms = New MimicControls.Label()
    Me.lbExhaustSpeed1Limits = New MimicControls.Label()
    Me.lbExhaustSpeed2Limits = New MimicControls.Label()
    Me.gpPlevaConfiguration.SuspendLayout()
    Me.SuspendLayout()
    '
    'Timer1
    '
    Me.Timer1.Enabled = True
    Me.Timer1.Interval = 1000
    '
    'gpPlevaConfiguration
    '
    Me.gpPlevaConfiguration.BackColor = System.Drawing.Color.Transparent
    Me.gpPlevaConfiguration.Controls.Add(Me.lbHumidityActual)
    Me.gpPlevaConfiguration.Controls.Add(Me.lbFabricDistanceAtTemp)
    Me.gpPlevaConfiguration.Controls.Add(Me.lbTimeActual)
    Me.gpPlevaConfiguration.Controls.Add(Me.btnAdjustHumidity)
    Me.gpPlevaConfiguration.Controls.Add(Me.lbPlevaHumidityStatus)
    Me.gpPlevaConfiguration.Controls.Add(Me.btnPlevaHumidityEnabled)
    Me.gpPlevaConfiguration.Controls.Add(Me.lbPlevaTempStatus)
    Me.gpPlevaConfiguration.Controls.Add(Me.btnPlevaTempEnabled)
    Me.gpPlevaConfiguration.Controls.Add(Me.btnAdjustTime)
    Me.gpPlevaConfiguration.Controls.Add(Me.btnAdjustTemp)
    Me.gpPlevaConfiguration.Controls.Add(Me.vlTimeDesired)
    Me.gpPlevaConfiguration.Controls.Add(Me.vlHumidityDesired)
    Me.gpPlevaConfiguration.Controls.Add(Me.btnHumidityInc)
    Me.gpPlevaConfiguration.Controls.Add(Me.btnTimeInc)
    Me.gpPlevaConfiguration.Controls.Add(Me.btnHumidityDec)
    Me.gpPlevaConfiguration.Controls.Add(Me.btnTimeDec)
    Me.gpPlevaConfiguration.Controls.Add(Me.btnTempDec)
    Me.gpPlevaConfiguration.Controls.Add(Me.btnTempInc)
    Me.gpPlevaConfiguration.Controls.Add(Me.vlTempDesired)
    Me.gpPlevaConfiguration.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.gpPlevaConfiguration.Font = New System.Drawing.Font("Tahoma", 11.25!)
    Me.gpPlevaConfiguration.ForeColor = System.Drawing.Color.Blue
    Me.gpPlevaConfiguration.Location = New System.Drawing.Point(3, 3)
    Me.gpPlevaConfiguration.Name = "gpPlevaConfiguration"
    Me.gpPlevaConfiguration.Size = New System.Drawing.Size(521, 284)
    Me.gpPlevaConfiguration.TabIndex = 6
    Me.gpPlevaConfiguration.TabStop = False
    Me.gpPlevaConfiguration.Text = "Pleva Configuration"
    '
    'lbHumidityActual
    '
    Me.lbHumidityActual.ForeColor = System.Drawing.Color.Black
    Me.lbHumidityActual.Location = New System.Drawing.Point(105, 260)
    Me.lbHumidityActual.Name = "lbHumidityActual"
    Me.lbHumidityActual.Size = New System.Drawing.Size(173, 18)
    Me.lbHumidityActual.TabIndex = 242
    Me.lbHumidityActual.Text = "Humidity Actual: 0.0 g/kg"
    '
    'lbFabricDistanceAtTemp
    '
    Me.lbFabricDistanceAtTemp.ForeColor = System.Drawing.Color.Black
    Me.lbFabricDistanceAtTemp.Location = New System.Drawing.Point(105, 164)
    Me.lbFabricDistanceAtTemp.Name = "lbFabricDistanceAtTemp"
    Me.lbFabricDistanceAtTemp.Size = New System.Drawing.Size(176, 18)
    Me.lbFabricDistanceAtTemp.TabIndex = 241
    Me.lbFabricDistanceAtTemp.Text = "Distance at Temp: 0 yrds"
    '
    'lbTimeActual
    '
    Me.lbTimeActual.ForeColor = System.Drawing.Color.Black
    Me.lbTimeActual.Location = New System.Drawing.Point(105, 129)
    Me.lbTimeActual.Name = "lbTimeActual"
    Me.lbTimeActual.Size = New System.Drawing.Size(143, 18)
    Me.lbTimeActual.TabIndex = 240
    Me.lbTimeActual.Text = "Time Actual: 0.0 sec"
    '
    'btnAdjustHumidity
    '
    Me.btnAdjustHumidity.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.btnAdjustHumidity.Location = New System.Drawing.Point(6, 228)
    Me.btnAdjustHumidity.Name = "btnAdjustHumidity"
    Me.btnAdjustHumidity.Size = New System.Drawing.Size(43, 51)
    Me.btnAdjustHumidity.TabIndex = 239
    Me.btnAdjustHumidity.Text = "Set"
    Me.btnAdjustHumidity.UseVisualStyleBackColor = True
    '
    'lbPlevaHumidityStatus
    '
    Me.lbPlevaHumidityStatus.ForeColor = System.Drawing.Color.Black
    Me.lbPlevaHumidityStatus.Location = New System.Drawing.Point(105, 203)
    Me.lbPlevaHumidityStatus.Name = "lbPlevaHumidityStatus"
    Me.lbPlevaHumidityStatus.Size = New System.Drawing.Size(149, 18)
    Me.lbPlevaHumidityStatus.TabIndex = 238
    Me.lbPlevaHumidityStatus.Text = "Pleva Humidity Status"
    '
    'btnPlevaHumidityEnabled
    '
    Me.btnPlevaHumidityEnabled.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.btnPlevaHumidityEnabled.Location = New System.Drawing.Point(6, 203)
    Me.btnPlevaHumidityEnabled.Name = "btnPlevaHumidityEnabled"
    Me.btnPlevaHumidityEnabled.Size = New System.Drawing.Size(89, 25)
    Me.btnPlevaHumidityEnabled.TabIndex = 237
    Me.btnPlevaHumidityEnabled.Text = "Active"
    Me.btnPlevaHumidityEnabled.UseVisualStyleBackColor = True
    '
    'lbPlevaTempStatus
    '
    Me.lbPlevaTempStatus.ForeColor = System.Drawing.Color.Black
    Me.lbPlevaTempStatus.Location = New System.Drawing.Point(105, 27)
    Me.lbPlevaTempStatus.Name = "lbPlevaTempStatus"
    Me.lbPlevaTempStatus.Size = New System.Drawing.Size(132, 18)
    Me.lbPlevaTempStatus.TabIndex = 236
    Me.lbPlevaTempStatus.Text = "Pleva Temp Status"
    '
    'btnPlevaTempEnabled
    '
    Me.btnPlevaTempEnabled.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.btnPlevaTempEnabled.Location = New System.Drawing.Point(6, 25)
    Me.btnPlevaTempEnabled.Name = "btnPlevaTempEnabled"
    Me.btnPlevaTempEnabled.Size = New System.Drawing.Size(89, 25)
    Me.btnPlevaTempEnabled.TabIndex = 235
    Me.btnPlevaTempEnabled.Text = "Active"
    Me.btnPlevaTempEnabled.UseVisualStyleBackColor = True
    '
    'btnAdjustTime
    '
    Me.btnAdjustTime.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.btnAdjustTime.Location = New System.Drawing.Point(6, 105)
    Me.btnAdjustTime.Name = "btnAdjustTime"
    Me.btnAdjustTime.Size = New System.Drawing.Size(43, 51)
    Me.btnAdjustTime.TabIndex = 234
    Me.btnAdjustTime.Text = "Set"
    Me.btnAdjustTime.UseVisualStyleBackColor = True
    '
    'btnAdjustTemp
    '
    Me.btnAdjustTemp.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.btnAdjustTemp.Location = New System.Drawing.Point(6, 50)
    Me.btnAdjustTemp.Name = "btnAdjustTemp"
    Me.btnAdjustTemp.Size = New System.Drawing.Size(43, 51)
    Me.btnAdjustTemp.TabIndex = 233
    Me.btnAdjustTemp.Text = "Set"
    Me.btnAdjustTemp.UseVisualStyleBackColor = True
    '
    'vlTimeDesired
    '
    Me.vlTimeDesired.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTimeDesired.ForeColor = System.Drawing.Color.Black
    Me.vlTimeDesired.Format = "Time Desired: 0.0 sec"
    Me.vlTimeDesired.Location = New System.Drawing.Point(105, 105)
    Me.vlTimeDesired.Name = "vlTimeDesired"
    Me.vlTimeDesired.Size = New System.Drawing.Size(152, 18)
    Me.vlTimeDesired.TabIndex = 231
    '
    'vlHumidityDesired
    '
    Me.vlHumidityDesired.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlHumidityDesired.ForeColor = System.Drawing.Color.Black
    Me.vlHumidityDesired.Format = "Humidity Desired: 0.0 g/kg"
    Me.vlHumidityDesired.Location = New System.Drawing.Point(105, 233)
    Me.vlHumidityDesired.Name = "vlHumidityDesired"
    Me.vlHumidityDesired.Size = New System.Drawing.Size(182, 18)
    Me.vlHumidityDesired.TabIndex = 230
    '
    'btnHumidityInc
    '
    Me.btnHumidityInc.BackColor = System.Drawing.Color.Transparent
    Me.btnHumidityInc.BackgroundImage = Global.My.Resources.Resources.Increase
    Me.btnHumidityInc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
    Me.btnHumidityInc.ForeColor = System.Drawing.Color.Black
    Me.btnHumidityInc.Location = New System.Drawing.Point(50, 228)
    Me.btnHumidityInc.Name = "btnHumidityInc"
    Me.btnHumidityInc.Size = New System.Drawing.Size(45, 25)
    Me.btnHumidityInc.TabIndex = 229
    Me.btnHumidityInc.UseVisualStyleBackColor = False
    '
    'btnTimeInc
    '
    Me.btnTimeInc.BackgroundImage = Global.My.Resources.Resources.Increase
    Me.btnTimeInc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
    Me.btnTimeInc.ForeColor = System.Drawing.Color.Black
    Me.btnTimeInc.Location = New System.Drawing.Point(50, 105)
    Me.btnTimeInc.Name = "btnTimeInc"
    Me.btnTimeInc.Size = New System.Drawing.Size(45, 25)
    Me.btnTimeInc.TabIndex = 228
    Me.btnTimeInc.UseVisualStyleBackColor = True
    '
    'btnHumidityDec
    '
    Me.btnHumidityDec.BackgroundImage = Global.My.Resources.Resources.Decrease
    Me.btnHumidityDec.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
    Me.btnHumidityDec.ForeColor = System.Drawing.Color.Black
    Me.btnHumidityDec.Location = New System.Drawing.Point(50, 254)
    Me.btnHumidityDec.Name = "btnHumidityDec"
    Me.btnHumidityDec.Size = New System.Drawing.Size(45, 25)
    Me.btnHumidityDec.TabIndex = 227
    Me.btnHumidityDec.UseVisualStyleBackColor = True
    '
    'btnTimeDec
    '
    Me.btnTimeDec.BackgroundImage = Global.My.Resources.Resources.Decrease
    Me.btnTimeDec.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
    Me.btnTimeDec.ForeColor = System.Drawing.Color.Black
    Me.btnTimeDec.Location = New System.Drawing.Point(50, 131)
    Me.btnTimeDec.Name = "btnTimeDec"
    Me.btnTimeDec.Size = New System.Drawing.Size(45, 25)
    Me.btnTimeDec.TabIndex = 226
    Me.btnTimeDec.UseVisualStyleBackColor = True
    '
    'btnTempDec
    '
    Me.btnTempDec.BackgroundImage = Global.My.Resources.Resources.Decrease
    Me.btnTempDec.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
    Me.btnTempDec.ForeColor = System.Drawing.Color.Black
    Me.btnTempDec.Location = New System.Drawing.Point(50, 76)
    Me.btnTempDec.Name = "btnTempDec"
    Me.btnTempDec.Size = New System.Drawing.Size(45, 25)
    Me.btnTempDec.TabIndex = 225
    Me.btnTempDec.UseVisualStyleBackColor = True
    '
    'btnTempInc
    '
    Me.btnTempInc.BackgroundImage = Global.My.Resources.Resources.Increase
    Me.btnTempInc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
    Me.btnTempInc.ForeColor = System.Drawing.Color.Black
    Me.btnTempInc.Location = New System.Drawing.Point(50, 50)
    Me.btnTempInc.Name = "btnTempInc"
    Me.btnTempInc.Size = New System.Drawing.Size(45, 25)
    Me.btnTempInc.TabIndex = 224
    Me.btnTempInc.UseVisualStyleBackColor = True
    '
    'vlTempDesired
    '
    Me.vlTempDesired.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTempDesired.ForeColor = System.Drawing.Color.Black
    Me.vlTempDesired.Format = "Temp Desired: 0.0 F"
    Me.vlTempDesired.Location = New System.Drawing.Point(105, 57)
    Me.vlTempDesired.Name = "vlTempDesired"
    Me.vlTempDesired.Size = New System.Drawing.Size(144, 18)
    Me.vlTempDesired.TabIndex = 220
    '
    'vlGasFlowRate
    '
    Me.vlGasFlowRate.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlGasFlowRate.ForeColor = System.Drawing.Color.Black
    Me.vlGasFlowRate.Format = "Gas Flow Rate: 0 cfm"
    Me.vlGasFlowRate.Location = New System.Drawing.Point(9, 337)
    Me.vlGasFlowRate.Name = "vlGasFlowRate"
    Me.vlGasFlowRate.Size = New System.Drawing.Size(131, 16)
    Me.vlGasFlowRate.TabIndex = 31
    '
    'vlGasUsedCubicFeet
    '
    Me.vlGasUsedCubicFeet.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlGasUsedCubicFeet.ForeColor = System.Drawing.Color.Black
    Me.vlGasUsedCubicFeet.Format = "Gas Used - Cubic Feet: 0 "
    Me.vlGasUsedCubicFeet.Location = New System.Drawing.Point(9, 293)
    Me.vlGasUsedCubicFeet.Name = "vlGasUsedCubicFeet"
    Me.vlGasUsedCubicFeet.Size = New System.Drawing.Size(154, 16)
    Me.vlGasUsedCubicFeet.TabIndex = 30
    '
    'vlFanExhaustSpeed2
    '
    Me.vlFanExhaustSpeed2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlFanExhaustSpeed2.ForeColor = System.Drawing.Color.Black
    Me.vlFanExhaustSpeed2.Format = "Exhaust 2 Speed: 0 RPM"
    Me.vlFanExhaustSpeed2.Location = New System.Drawing.Point(779, 323)
    Me.vlFanExhaustSpeed2.Name = "vlFanExhaustSpeed2"
    Me.vlFanExhaustSpeed2.Size = New System.Drawing.Size(148, 16)
    Me.vlFanExhaustSpeed2.TabIndex = 29
    '
    'vlFabricSpeedLimitHigh
    '
    Me.vlFabricSpeedLimitHigh.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlFabricSpeedLimitHigh.ForeColor = System.Drawing.Color.Black
    Me.vlFabricSpeedLimitHigh.Format = "Fabric Speed Limit High: 0.0 ypm"
    Me.vlFabricSpeedLimitHigh.Location = New System.Drawing.Point(562, 279)
    Me.vlFabricSpeedLimitHigh.Name = "vlFabricSpeedLimitHigh"
    Me.vlFabricSpeedLimitHigh.Size = New System.Drawing.Size(223, 18)
    Me.vlFabricSpeedLimitHigh.TabIndex = 28
    '
    'vlFabricSpeedLimitLow
    '
    Me.vlFabricSpeedLimitLow.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlFabricSpeedLimitLow.ForeColor = System.Drawing.Color.Black
    Me.vlFabricSpeedLimitLow.Format = "Fabric Speed Limit Low: 0.0 ypm"
    Me.vlFabricSpeedLimitLow.Location = New System.Drawing.Point(562, 255)
    Me.vlFabricSpeedLimitLow.Name = "vlFabricSpeedLimitLow"
    Me.vlFabricSpeedLimitLow.Size = New System.Drawing.Size(220, 18)
    Me.vlFabricSpeedLimitLow.TabIndex = 27
    '
    'vlFanExhaustSpeed1
    '
    Me.vlFanExhaustSpeed1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlFanExhaustSpeed1.ForeColor = System.Drawing.Color.Black
    Me.vlFanExhaustSpeed1.Format = "Exhaust 1 Speed: 0 RPM"
    Me.vlFanExhaustSpeed1.Location = New System.Drawing.Point(266, 323)
    Me.vlFanExhaustSpeed1.Name = "vlFanExhaustSpeed1"
    Me.vlFanExhaustSpeed1.Size = New System.Drawing.Size(148, 16)
    Me.vlFanExhaustSpeed1.TabIndex = 26
    '
    'outputZone6
    '
    Me.outputZone6.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.outputZone6.ForeColor = System.Drawing.Color.Black
    Me.outputZone6.Format = "Output: 0.0 %"
    Me.outputZone6.Location = New System.Drawing.Point(758, 468)
    Me.outputZone6.Name = "outputZone6"
    Me.outputZone6.Size = New System.Drawing.Size(89, 16)
    Me.outputZone6.TabIndex = 25
    '
    'outputZone5
    '
    Me.outputZone5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.outputZone5.ForeColor = System.Drawing.Color.Black
    Me.outputZone5.Format = "Output: 0.0 %"
    Me.outputZone5.Location = New System.Drawing.Point(632, 468)
    Me.outputZone5.Name = "outputZone5"
    Me.outputZone5.Size = New System.Drawing.Size(89, 16)
    Me.outputZone5.TabIndex = 24
    '
    'outputZone4
    '
    Me.outputZone4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.outputZone4.ForeColor = System.Drawing.Color.Black
    Me.outputZone4.Format = "Output: 0.0 %"
    Me.outputZone4.Location = New System.Drawing.Point(504, 468)
    Me.outputZone4.Name = "outputZone4"
    Me.outputZone4.Size = New System.Drawing.Size(89, 16)
    Me.outputZone4.TabIndex = 23
    '
    'outputZone3
    '
    Me.outputZone3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.outputZone3.ForeColor = System.Drawing.Color.Black
    Me.outputZone3.Format = "Output: 0.0 %"
    Me.outputZone3.Location = New System.Drawing.Point(372, 468)
    Me.outputZone3.Name = "outputZone3"
    Me.outputZone3.Size = New System.Drawing.Size(89, 16)
    Me.outputZone3.TabIndex = 22
    '
    'outputZone2
    '
    Me.outputZone2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.outputZone2.ForeColor = System.Drawing.Color.Black
    Me.outputZone2.Format = "Output: 0.0 %"
    Me.outputZone2.Location = New System.Drawing.Point(245, 468)
    Me.outputZone2.Name = "outputZone2"
    Me.outputZone2.Size = New System.Drawing.Size(89, 16)
    Me.outputZone2.TabIndex = 21
    '
    'lbAirZone5
    '
    Me.lbAirZone5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAirZone5.ForeColor = System.Drawing.Color.Black
    Me.lbAirZone5.Location = New System.Drawing.Point(632, 451)
    Me.lbAirZone5.Name = "lbAirZone5"
    Me.lbAirZone5.Size = New System.Drawing.Size(91, 16)
    Me.lbAirZone5.TabIndex = 20
    Me.lbAirZone5.Text = "Z5: 0.0 / 0.0 F"
    '
    'lbAirZone6
    '
    Me.lbAirZone6.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAirZone6.ForeColor = System.Drawing.Color.Black
    Me.lbAirZone6.Location = New System.Drawing.Point(758, 451)
    Me.lbAirZone6.Name = "lbAirZone6"
    Me.lbAirZone6.Size = New System.Drawing.Size(91, 16)
    Me.lbAirZone6.TabIndex = 19
    Me.lbAirZone6.Text = "Z6: 0.0 / 0.0 F"
    '
    'Label5
    '
    Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label5.ForeColor = System.Drawing.Color.Black
    Me.Label5.Location = New System.Drawing.Point(632, 451)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(12, 16)
    Me.Label5.TabIndex = 18
    Me.Label5.Text = "."
    '
    'lbAirZone3
    '
    Me.lbAirZone3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAirZone3.ForeColor = System.Drawing.Color.Black
    Me.lbAirZone3.Location = New System.Drawing.Point(372, 451)
    Me.lbAirZone3.Name = "lbAirZone3"
    Me.lbAirZone3.Size = New System.Drawing.Size(91, 16)
    Me.lbAirZone3.TabIndex = 17
    Me.lbAirZone3.Text = "Z3: 0.0 / 0.0 F"
    '
    'lbAirZone2
    '
    Me.lbAirZone2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAirZone2.ForeColor = System.Drawing.Color.Black
    Me.lbAirZone2.Location = New System.Drawing.Point(245, 451)
    Me.lbAirZone2.Name = "lbAirZone2"
    Me.lbAirZone2.Size = New System.Drawing.Size(95, 16)
    Me.lbAirZone2.TabIndex = 16
    Me.lbAirZone2.Text = "Z2.: 0.0 / 0.0 F"
    '
    'lbAirZone4
    '
    Me.lbAirZone4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAirZone4.ForeColor = System.Drawing.Color.Black
    Me.lbAirZone4.Location = New System.Drawing.Point(504, 451)
    Me.lbAirZone4.Name = "lbAirZone4"
    Me.lbAirZone4.Size = New System.Drawing.Size(91, 16)
    Me.lbAirZone4.TabIndex = 16
    Me.lbAirZone4.Text = "Z4: 0.0 / 0.0 F"
    '
    'lbAirZone1
    '
    Me.lbAirZone1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAirZone1.ForeColor = System.Drawing.Color.Black
    Me.lbAirZone1.Location = New System.Drawing.Point(113, 451)
    Me.lbAirZone1.Name = "lbAirZone1"
    Me.lbAirZone1.Size = New System.Drawing.Size(91, 16)
    Me.lbAirZone1.TabIndex = 15
    Me.lbAirZone1.Text = "Z1: 0.0 / 0.0 F"
    '
    'outputZone1
    '
    Me.outputZone1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.outputZone1.ForeColor = System.Drawing.Color.Black
    Me.outputZone1.Format = "Output: 0.0 %"
    Me.outputZone1.Location = New System.Drawing.Point(113, 468)
    Me.outputZone1.Name = "outputZone1"
    Me.outputZone1.Size = New System.Drawing.Size(89, 16)
    Me.outputZone1.TabIndex = 14
    '
    'PlevaTempProfile1
    '
    Me.PlevaTempProfile1.Location = New System.Drawing.Point(562, 12)
    Me.PlevaTempProfile1.Maximum = 4500
    Me.PlevaTempProfile1.Name = "PlevaTempProfile1"
    Me.PlevaTempProfile1.Size = New System.Drawing.Size(452, 209)
    Me.PlevaTempProfile1.TabIndex = 12
    '
    'vlFabricSpeed
    '
    Me.vlFabricSpeed.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlFabricSpeed.ForeColor = System.Drawing.Color.Black
    Me.vlFabricSpeed.Format = "Frame Speed: 0.0 ypm"
    Me.vlFabricSpeed.Location = New System.Drawing.Point(562, 231)
    Me.vlFabricSpeed.Name = "vlFabricSpeed"
    Me.vlFabricSpeed.Size = New System.Drawing.Size(160, 18)
    Me.vlFabricSpeed.TabIndex = 11
    '
    'vlPlevaTemp4
    '
    Me.vlPlevaTemp4.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlPlevaTemp4.ForeColor = System.Drawing.Color.Black
    Me.vlPlevaTemp4.Format = "0.0 F"
    Me.vlPlevaTemp4.Location = New System.Drawing.Point(723, 535)
    Me.vlPlevaTemp4.Name = "vlPlevaTemp4"
    Me.vlPlevaTemp4.Size = New System.Drawing.Size(42, 18)
    Me.vlPlevaTemp4.TabIndex = 10
    '
    'vlPlevaTemp3
    '
    Me.vlPlevaTemp3.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlPlevaTemp3.ForeColor = System.Drawing.Color.Black
    Me.vlPlevaTemp3.Format = "0.0 F"
    Me.vlPlevaTemp3.Location = New System.Drawing.Point(598, 535)
    Me.vlPlevaTemp3.Name = "vlPlevaTemp3"
    Me.vlPlevaTemp3.Size = New System.Drawing.Size(42, 18)
    Me.vlPlevaTemp3.TabIndex = 9
    '
    'vlPlevaTemp2
    '
    Me.vlPlevaTemp2.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlPlevaTemp2.ForeColor = System.Drawing.Color.Black
    Me.vlPlevaTemp2.Format = "0.0 F"
    Me.vlPlevaTemp2.Location = New System.Drawing.Point(464, 535)
    Me.vlPlevaTemp2.Name = "vlPlevaTemp2"
    Me.vlPlevaTemp2.Size = New System.Drawing.Size(42, 18)
    Me.vlPlevaTemp2.TabIndex = 8
    '
    'vlPlevaTemp1
    '
    Me.vlPlevaTemp1.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlPlevaTemp1.ForeColor = System.Drawing.Color.Black
    Me.vlPlevaTemp1.Format = "0.0 F"
    Me.vlPlevaTemp1.Location = New System.Drawing.Point(339, 535)
    Me.vlPlevaTemp1.Name = "vlPlevaTemp1"
    Me.vlPlevaTemp1.Size = New System.Drawing.Size(42, 18)
    Me.vlPlevaTemp1.TabIndex = 7
    '
    'lbGasUsedDecatherms
    '
    Me.lbGasUsedDecatherms.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbGasUsedDecatherms.ForeColor = System.Drawing.Color.Black
    Me.lbGasUsedDecatherms.Location = New System.Drawing.Point(9, 315)
    Me.lbGasUsedDecatherms.Name = "lbGasUsedDecatherms"
    Me.lbGasUsedDecatherms.Size = New System.Drawing.Size(119, 16)
    Me.lbGasUsedDecatherms.TabIndex = 237
    Me.lbGasUsedDecatherms.Text = "DecaTherms: 0 Dth"
    '
    'lbExhaustSpeed1Limits
    '
    Me.lbExhaustSpeed1Limits.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbExhaustSpeed1Limits.ForeColor = System.Drawing.Color.Black
    Me.lbExhaustSpeed1Limits.Location = New System.Drawing.Point(266, 345)
    Me.lbExhaustSpeed1Limits.Name = "lbExhaustSpeed1Limits"
    Me.lbExhaustSpeed1Limits.Size = New System.Drawing.Size(185, 16)
    Me.lbExhaustSpeed1Limits.TabIndex = 238
    Me.lbExhaustSpeed1Limits.Text = "Limit Low: 0 RPM  High: 0 RPM"
    '
    'lbExhaustSpeed2Limits
    '
    Me.lbExhaustSpeed2Limits.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbExhaustSpeed2Limits.ForeColor = System.Drawing.Color.Black
    Me.lbExhaustSpeed2Limits.Location = New System.Drawing.Point(779, 345)
    Me.lbExhaustSpeed2Limits.Name = "lbExhaustSpeed2Limits"
    Me.lbExhaustSpeed2Limits.Size = New System.Drawing.Size(185, 16)
    Me.lbExhaustSpeed2Limits.TabIndex = 239
    Me.lbExhaustSpeed2Limits.Text = "Limit Low: 0 RPM  High: 0 RPM"
    '
    'PlevaControl
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BackColor = System.Drawing.Color.White
    Me.BackgroundImage = Global.My.Resources.Resources.FramePNG
    Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
    Me.Controls.Add(Me.lbExhaustSpeed2Limits)
    Me.Controls.Add(Me.lbExhaustSpeed1Limits)
    Me.Controls.Add(Me.lbGasUsedDecatherms)
    Me.Controls.Add(Me.vlGasFlowRate)
    Me.Controls.Add(Me.vlGasUsedCubicFeet)
    Me.Controls.Add(Me.vlFanExhaustSpeed2)
    Me.Controls.Add(Me.vlFabricSpeedLimitHigh)
    Me.Controls.Add(Me.vlFabricSpeedLimitLow)
    Me.Controls.Add(Me.vlFanExhaustSpeed1)
    Me.Controls.Add(Me.outputZone6)
    Me.Controls.Add(Me.outputZone5)
    Me.Controls.Add(Me.outputZone4)
    Me.Controls.Add(Me.outputZone3)
    Me.Controls.Add(Me.outputZone2)
    Me.Controls.Add(Me.lbAirZone5)
    Me.Controls.Add(Me.lbAirZone6)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.lbAirZone3)
    Me.Controls.Add(Me.lbAirZone2)
    Me.Controls.Add(Me.lbAirZone4)
    Me.Controls.Add(Me.lbAirZone1)
    Me.Controls.Add(Me.outputZone1)
    Me.Controls.Add(Me.PlevaTempProfile1)
    Me.Controls.Add(Me.vlFabricSpeed)
    Me.Controls.Add(Me.vlPlevaTemp4)
    Me.Controls.Add(Me.vlPlevaTemp3)
    Me.Controls.Add(Me.vlPlevaTemp2)
    Me.Controls.Add(Me.vlPlevaTemp1)
    Me.Controls.Add(Me.gpPlevaConfiguration)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
    Me.Name = "PlevaControl"
    Me.Size = New System.Drawing.Size(1029, 658)
    Me.gpPlevaConfiguration.ResumeLayout(False)
    Me.gpPlevaConfiguration.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Timer1 As System.Windows.Forms.Timer
  Friend WithEvents vlTempDesired As MimicControls.ValueLabel
  Friend WithEvents gpPlevaConfiguration As System.Windows.Forms.GroupBox
  Friend WithEvents btnTempDec As System.Windows.Forms.Button
  Friend WithEvents btnTempInc As System.Windows.Forms.Button
  Friend WithEvents btnHumidityDec As System.Windows.Forms.Button
  Friend WithEvents btnTimeDec As System.Windows.Forms.Button
  Friend WithEvents btnHumidityInc As System.Windows.Forms.Button
  Friend WithEvents btnTimeInc As System.Windows.Forms.Button
  Friend WithEvents vlHumidityDesired As MimicControls.ValueLabel
  Friend WithEvents vlTimeDesired As MimicControls.ValueLabel
  Friend WithEvents vlPlevaTemp1 As MimicControls.ValueLabel
  Friend WithEvents vlPlevaTemp2 As MimicControls.ValueLabel
  Friend WithEvents vlPlevaTemp3 As MimicControls.ValueLabel
  Friend WithEvents vlPlevaTemp4 As MimicControls.ValueLabel
  Friend WithEvents vlFabricSpeed As MimicControls.ValueLabel
  Friend WithEvents PlevaTempProfile1 As PlevaTempProfile
  Friend WithEvents outputZone1 As MimicControls.ValueLabel
  Friend WithEvents lbAirZone1 As MimicControls.Label
  Friend WithEvents lbAirZone4 As MimicControls.Label
  Friend WithEvents lbAirZone2 As MimicControls.Label
  Friend WithEvents lbAirZone3 As MimicControls.Label
  Friend WithEvents Label5 As MimicControls.Label
  Friend WithEvents lbAirZone6 As MimicControls.Label
  Friend WithEvents lbAirZone5 As MimicControls.Label
  Friend WithEvents outputZone2 As MimicControls.ValueLabel
  Friend WithEvents outputZone3 As MimicControls.ValueLabel
  Friend WithEvents outputZone4 As MimicControls.ValueLabel
  Friend WithEvents outputZone5 As MimicControls.ValueLabel
  Friend WithEvents outputZone6 As MimicControls.ValueLabel
  Friend WithEvents vlFanExhaustSpeed1 As MimicControls.ValueLabel
  Friend WithEvents btnAdjustTemp As System.Windows.Forms.Button
  Friend WithEvents btnAdjustTime As System.Windows.Forms.Button
  Friend WithEvents lbPlevaTempStatus As MimicControls.Label
  Friend WithEvents btnPlevaTempEnabled As System.Windows.Forms.Button
  Friend WithEvents lbPlevaHumidityStatus As MimicControls.Label
  Friend WithEvents btnPlevaHumidityEnabled As System.Windows.Forms.Button
  Friend WithEvents btnAdjustHumidity As System.Windows.Forms.Button
  Friend WithEvents lbTimeActual As MimicControls.Label
  Friend WithEvents vlFabricSpeedLimitLow As MimicControls.ValueLabel
  Friend WithEvents vlFabricSpeedLimitHigh As MimicControls.ValueLabel
  Friend WithEvents lbFabricDistanceAtTemp As MimicControls.Label
  Friend WithEvents vlFanExhaustSpeed2 As MimicControls.ValueLabel
  Friend WithEvents vlGasUsedCubicFeet As MimicControls.ValueLabel
  Friend WithEvents vlGasFlowRate As MimicControls.ValueLabel
  Friend WithEvents lbGasUsedDecatherms As MimicControls.Label
  Friend WithEvents lbHumidityActual As MimicControls.Label
  Friend WithEvents lbExhaustSpeed1Limits As MimicControls.Label
  Friend WithEvents lbExhaustSpeed2Limits As MimicControls.Label

End Class
