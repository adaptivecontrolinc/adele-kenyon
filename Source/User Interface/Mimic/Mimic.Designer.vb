<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Mimic
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
    Me.vlBottomFanZone8 = New MimicControls.ValueLabel()
    Me.vlTopFanZone8 = New MimicControls.ValueLabel()
    Me.vlBottomFanZone7 = New MimicControls.ValueLabel()
    Me.vlTopFanZone7 = New MimicControls.ValueLabel()
    Me.vlWidth3 = New MimicControls.ValueLabel()
    Me.vlTempActualZone8 = New MimicControls.ValueLabel()
    Me.vlTempActualZone7 = New MimicControls.ValueLabel()
    Me.vlTempDesiredZone8 = New MimicControls.ValueLabel()
    Me.vlTempDesiredZone7 = New MimicControls.ValueLabel()
    Me.vlPadderSpeed1 = New MimicControls.ValueLabel()
    Me.vlFabricSpeedLimitHigh = New MimicControls.ValueLabel()
    Me.vlFabricSpeedLimitLow = New MimicControls.ValueLabel()
    Me.vlFabricSpeed = New MimicControls.ValueLabel()
    Me.vlBottomFanZone6 = New MimicControls.ValueLabel()
    Me.vlTopFanZone6 = New MimicControls.ValueLabel()
    Me.vlBottomFanZone4 = New MimicControls.ValueLabel()
    Me.vlTopFanZone4 = New MimicControls.ValueLabel()
    Me.vlBottomFanZone2 = New MimicControls.ValueLabel()
    Me.vlTopFanZone2 = New MimicControls.ValueLabel()
    Me.vlBottomFanZone5 = New MimicControls.ValueLabel()
    Me.vlTopFanZone5 = New MimicControls.ValueLabel()
    Me.vlBottomFanZone3 = New MimicControls.ValueLabel()
    Me.vlTopFanZone3 = New MimicControls.ValueLabel()
    Me.vlBottomFanZone1 = New MimicControls.ValueLabel()
    Me.vlTopFanZone1 = New MimicControls.ValueLabel()
    Me.vlTempDesiredZone6 = New MimicControls.ValueLabel()
    Me.vlTempDesiredZone5 = New MimicControls.ValueLabel()
    Me.vlTempDesiredZone4 = New MimicControls.ValueLabel()
    Me.vlTempDesiredZone3 = New MimicControls.ValueLabel()
    Me.vlTempDesiredZone2 = New MimicControls.ValueLabel()
    Me.vlTempDesiredZone1 = New MimicControls.ValueLabel()
    Me.vlTempActualZone6 = New MimicControls.ValueLabel()
    Me.vlTempActualZone5 = New MimicControls.ValueLabel()
    Me.vlTempActualZone4 = New MimicControls.ValueLabel()
    Me.vlTempActualZone3 = New MimicControls.ValueLabel()
    Me.vlTempActualZone2 = New MimicControls.ValueLabel()
    Me.vlTempActualZone1 = New MimicControls.ValueLabel()
    Me.vlFanExhaust2 = New MimicControls.ValueLabel()
    Me.vlFanExhaust1 = New MimicControls.ValueLabel()
    Me.vlWidthMain = New MimicControls.ValueLabel()
    Me.vlWidthRear = New MimicControls.ValueLabel()
    Me.vlWidth2 = New MimicControls.ValueLabel()
    Me.vlWidth1 = New MimicControls.ValueLabel()
    Me.vlSelvageRight = New MimicControls.ValueLabel()
    Me.vlSelvageLeft = New MimicControls.ValueLabel()
    Me.vlOverfeed = New MimicControls.ValueLabel()
    Me.vlPadderSpeed2 = New MimicControls.ValueLabel()
    Me.SuspendLayout()
    '
    'Timer1
    '
    Me.Timer1.Enabled = True
    Me.Timer1.Interval = 1000
    '
    'vlBottomFanZone8
    '
    Me.vlBottomFanZone8.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlBottomFanZone8.ForeColor = System.Drawing.Color.Black
    Me.vlBottomFanZone8.Format = "Bottom: 0.0 %"
    Me.vlBottomFanZone8.Location = New System.Drawing.Point(806, 116)
    Me.vlBottomFanZone8.Name = "vlBottomFanZone8"
    Me.vlBottomFanZone8.NumberScale = 1000
    Me.vlBottomFanZone8.Size = New System.Drawing.Size(91, 16)
    Me.vlBottomFanZone8.TabIndex = 269
    '
    'vlTopFanZone8
    '
    Me.vlTopFanZone8.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTopFanZone8.ForeColor = System.Drawing.Color.Black
    Me.vlTopFanZone8.Format = "Top: 0.0 %"
    Me.vlTopFanZone8.Location = New System.Drawing.Point(806, 96)
    Me.vlTopFanZone8.Name = "vlTopFanZone8"
    Me.vlTopFanZone8.NumberScale = 1000
    Me.vlTopFanZone8.Size = New System.Drawing.Size(73, 16)
    Me.vlTopFanZone8.TabIndex = 268
    '
    'vlBottomFanZone7
    '
    Me.vlBottomFanZone7.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlBottomFanZone7.ForeColor = System.Drawing.Color.Black
    Me.vlBottomFanZone7.Format = "Bottom: 0.0 %"
    Me.vlBottomFanZone7.Location = New System.Drawing.Point(806, 339)
    Me.vlBottomFanZone7.Name = "vlBottomFanZone7"
    Me.vlBottomFanZone7.NumberScale = 1000
    Me.vlBottomFanZone7.Size = New System.Drawing.Size(91, 16)
    Me.vlBottomFanZone7.TabIndex = 267
    '
    'vlTopFanZone7
    '
    Me.vlTopFanZone7.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTopFanZone7.ForeColor = System.Drawing.Color.Black
    Me.vlTopFanZone7.Format = "Top: 0.0 %"
    Me.vlTopFanZone7.Location = New System.Drawing.Point(806, 319)
    Me.vlTopFanZone7.Name = "vlTopFanZone7"
    Me.vlTopFanZone7.NumberScale = 1000
    Me.vlTopFanZone7.Size = New System.Drawing.Size(73, 16)
    Me.vlTopFanZone7.TabIndex = 266
    '
    'vlWidth3
    '
    Me.vlWidth3.BackColor = System.Drawing.Color.White
    Me.vlWidth3.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlWidth3.ForeColor = System.Drawing.Color.Black
    Me.vlWidth3.Format = "0.0 in"
    Me.vlWidth3.Location = New System.Drawing.Point(573, 218)
    Me.vlWidth3.Name = "vlWidth3"
    Me.vlWidth3.Size = New System.Drawing.Size(44, 18)
    Me.vlWidth3.TabIndex = 265
    '
    'vlTempActualZone8
    '
    Me.vlTempActualZone8.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTempActualZone8.ForeColor = System.Drawing.Color.Black
    Me.vlTempActualZone8.Format = "0.0 F"
    Me.vlTempActualZone8.Location = New System.Drawing.Point(860, 530)
    Me.vlTempActualZone8.Name = "vlTempActualZone8"
    Me.vlTempActualZone8.Size = New System.Drawing.Size(37, 16)
    Me.vlTempActualZone8.TabIndex = 264
    '
    'vlTempActualZone7
    '
    Me.vlTempActualZone7.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTempActualZone7.ForeColor = System.Drawing.Color.Black
    Me.vlTempActualZone7.Format = "0.0 F"
    Me.vlTempActualZone7.Location = New System.Drawing.Point(805, 530)
    Me.vlTempActualZone7.Name = "vlTempActualZone7"
    Me.vlTempActualZone7.Size = New System.Drawing.Size(37, 16)
    Me.vlTempActualZone7.TabIndex = 263
    '
    'vlTempDesiredZone8
    '
    Me.vlTempDesiredZone8.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTempDesiredZone8.ForeColor = System.Drawing.Color.Black
    Me.vlTempDesiredZone8.Format = "0.0 F"
    Me.vlTempDesiredZone8.Location = New System.Drawing.Point(860, 508)
    Me.vlTempDesiredZone8.Name = "vlTempDesiredZone8"
    Me.vlTempDesiredZone8.Size = New System.Drawing.Size(37, 16)
    Me.vlTempDesiredZone8.TabIndex = 262
    '
    'vlTempDesiredZone7
    '
    Me.vlTempDesiredZone7.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTempDesiredZone7.ForeColor = System.Drawing.Color.Black
    Me.vlTempDesiredZone7.Format = "0.0 F"
    Me.vlTempDesiredZone7.Location = New System.Drawing.Point(805, 508)
    Me.vlTempDesiredZone7.Name = "vlTempDesiredZone7"
    Me.vlTempDesiredZone7.Size = New System.Drawing.Size(37, 16)
    Me.vlTempDesiredZone7.TabIndex = 261
    '
    'vlPadderSpeed1
    '
    Me.vlPadderSpeed1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlPadderSpeed1.ForeColor = System.Drawing.Color.Black
    Me.vlPadderSpeed1.Format = "Padder 1: 0.0 YPM"
    Me.vlPadderSpeed1.Location = New System.Drawing.Point(10, 530)
    Me.vlPadderSpeed1.Name = "vlPadderSpeed1"
    Me.vlPadderSpeed1.Size = New System.Drawing.Size(114, 16)
    Me.vlPadderSpeed1.TabIndex = 260
    '
    'vlFabricSpeedLimitHigh
    '
    Me.vlFabricSpeedLimitHigh.Font = New System.Drawing.Font("Tahoma", 9.75!)
    Me.vlFabricSpeedLimitHigh.ForeColor = System.Drawing.Color.Black
    Me.vlFabricSpeedLimitHigh.Format = "Fabric Speed Limit High: 0.0 ypm"
    Me.vlFabricSpeedLimitHigh.Location = New System.Drawing.Point(10, 50)
    Me.vlFabricSpeedLimitHigh.Name = "vlFabricSpeedLimitHigh"
    Me.vlFabricSpeedLimitHigh.Size = New System.Drawing.Size(198, 16)
    Me.vlFabricSpeedLimitHigh.TabIndex = 240
    '
    'vlFabricSpeedLimitLow
    '
    Me.vlFabricSpeedLimitLow.Font = New System.Drawing.Font("Tahoma", 9.75!)
    Me.vlFabricSpeedLimitLow.ForeColor = System.Drawing.Color.Black
    Me.vlFabricSpeedLimitLow.Format = "Fabric Speed Limit Low: 0.0 ypm"
    Me.vlFabricSpeedLimitLow.Location = New System.Drawing.Point(10, 30)
    Me.vlFabricSpeedLimitLow.Name = "vlFabricSpeedLimitLow"
    Me.vlFabricSpeedLimitLow.Size = New System.Drawing.Size(196, 16)
    Me.vlFabricSpeedLimitLow.TabIndex = 239
    '
    'vlFabricSpeed
    '
    Me.vlFabricSpeed.Font = New System.Drawing.Font("Tahoma", 9.75!)
    Me.vlFabricSpeed.ForeColor = System.Drawing.Color.Black
    Me.vlFabricSpeed.Format = "Frame Speed: 0.0 ypm"
    Me.vlFabricSpeed.Location = New System.Drawing.Point(10, 10)
    Me.vlFabricSpeed.Name = "vlFabricSpeed"
    Me.vlFabricSpeed.Size = New System.Drawing.Size(140, 16)
    Me.vlFabricSpeed.TabIndex = 238
    '
    'vlBottomFanZone6
    '
    Me.vlBottomFanZone6.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlBottomFanZone6.ForeColor = System.Drawing.Color.Black
    Me.vlBottomFanZone6.Format = "Bottom: 0.0 %"
    Me.vlBottomFanZone6.Location = New System.Drawing.Point(700, 116)
    Me.vlBottomFanZone6.Name = "vlBottomFanZone6"
    Me.vlBottomFanZone6.NumberScale = 1000
    Me.vlBottomFanZone6.Size = New System.Drawing.Size(91, 16)
    Me.vlBottomFanZone6.TabIndex = 61
    '
    'vlTopFanZone6
    '
    Me.vlTopFanZone6.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTopFanZone6.ForeColor = System.Drawing.Color.Black
    Me.vlTopFanZone6.Format = "Top: 0.0 %"
    Me.vlTopFanZone6.Location = New System.Drawing.Point(700, 96)
    Me.vlTopFanZone6.Name = "vlTopFanZone6"
    Me.vlTopFanZone6.NumberScale = 1000
    Me.vlTopFanZone6.Size = New System.Drawing.Size(73, 16)
    Me.vlTopFanZone6.TabIndex = 60
    '
    'vlBottomFanZone4
    '
    Me.vlBottomFanZone4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlBottomFanZone4.ForeColor = System.Drawing.Color.Black
    Me.vlBottomFanZone4.Format = "Bottom: 0.0 %"
    Me.vlBottomFanZone4.Location = New System.Drawing.Point(590, 116)
    Me.vlBottomFanZone4.Name = "vlBottomFanZone4"
    Me.vlBottomFanZone4.NumberScale = 1000
    Me.vlBottomFanZone4.Size = New System.Drawing.Size(91, 16)
    Me.vlBottomFanZone4.TabIndex = 59
    '
    'vlTopFanZone4
    '
    Me.vlTopFanZone4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTopFanZone4.ForeColor = System.Drawing.Color.Black
    Me.vlTopFanZone4.Format = "Top: 0.0 %"
    Me.vlTopFanZone4.Location = New System.Drawing.Point(590, 96)
    Me.vlTopFanZone4.Name = "vlTopFanZone4"
    Me.vlTopFanZone4.NumberScale = 1000
    Me.vlTopFanZone4.Size = New System.Drawing.Size(73, 16)
    Me.vlTopFanZone4.TabIndex = 58
    '
    'vlBottomFanZone2
    '
    Me.vlBottomFanZone2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlBottomFanZone2.ForeColor = System.Drawing.Color.Black
    Me.vlBottomFanZone2.Format = "Bottom: 0.0 %"
    Me.vlBottomFanZone2.Location = New System.Drawing.Point(480, 118)
    Me.vlBottomFanZone2.Name = "vlBottomFanZone2"
    Me.vlBottomFanZone2.NumberScale = 1000
    Me.vlBottomFanZone2.Size = New System.Drawing.Size(91, 16)
    Me.vlBottomFanZone2.TabIndex = 57
    '
    'vlTopFanZone2
    '
    Me.vlTopFanZone2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTopFanZone2.ForeColor = System.Drawing.Color.Black
    Me.vlTopFanZone2.Format = "Top: 0.0%"
    Me.vlTopFanZone2.Location = New System.Drawing.Point(480, 96)
    Me.vlTopFanZone2.Name = "vlTopFanZone2"
    Me.vlTopFanZone2.NumberScale = 1000
    Me.vlTopFanZone2.Size = New System.Drawing.Size(69, 16)
    Me.vlTopFanZone2.TabIndex = 56
    '
    'vlBottomFanZone5
    '
    Me.vlBottomFanZone5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlBottomFanZone5.ForeColor = System.Drawing.Color.Black
    Me.vlBottomFanZone5.Format = "Bottom: 0.0 %"
    Me.vlBottomFanZone5.Location = New System.Drawing.Point(700, 339)
    Me.vlBottomFanZone5.Name = "vlBottomFanZone5"
    Me.vlBottomFanZone5.NumberScale = 1000
    Me.vlBottomFanZone5.Size = New System.Drawing.Size(91, 16)
    Me.vlBottomFanZone5.TabIndex = 55
    '
    'vlTopFanZone5
    '
    Me.vlTopFanZone5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTopFanZone5.ForeColor = System.Drawing.Color.Black
    Me.vlTopFanZone5.Format = "Top: 0.0 %"
    Me.vlTopFanZone5.Location = New System.Drawing.Point(700, 319)
    Me.vlTopFanZone5.Name = "vlTopFanZone5"
    Me.vlTopFanZone5.NumberScale = 1000
    Me.vlTopFanZone5.Size = New System.Drawing.Size(73, 16)
    Me.vlTopFanZone5.TabIndex = 54
    '
    'vlBottomFanZone3
    '
    Me.vlBottomFanZone3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlBottomFanZone3.ForeColor = System.Drawing.Color.Black
    Me.vlBottomFanZone3.Format = "Bottom: 0.0 %"
    Me.vlBottomFanZone3.Location = New System.Drawing.Point(590, 339)
    Me.vlBottomFanZone3.Name = "vlBottomFanZone3"
    Me.vlBottomFanZone3.NumberScale = 1000
    Me.vlBottomFanZone3.Size = New System.Drawing.Size(91, 16)
    Me.vlBottomFanZone3.TabIndex = 53
    '
    'vlTopFanZone3
    '
    Me.vlTopFanZone3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTopFanZone3.ForeColor = System.Drawing.Color.Black
    Me.vlTopFanZone3.Format = "Top: 0.0 %"
    Me.vlTopFanZone3.Location = New System.Drawing.Point(590, 319)
    Me.vlTopFanZone3.Name = "vlTopFanZone3"
    Me.vlTopFanZone3.NumberScale = 1000
    Me.vlTopFanZone3.Size = New System.Drawing.Size(73, 16)
    Me.vlTopFanZone3.TabIndex = 52
    '
    'vlBottomFanZone1
    '
    Me.vlBottomFanZone1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlBottomFanZone1.ForeColor = System.Drawing.Color.Black
    Me.vlBottomFanZone1.Format = "Bottom: 0.0 %"
    Me.vlBottomFanZone1.Location = New System.Drawing.Point(481, 339)
    Me.vlBottomFanZone1.Name = "vlBottomFanZone1"
    Me.vlBottomFanZone1.NumberScale = 1000
    Me.vlBottomFanZone1.Size = New System.Drawing.Size(91, 16)
    Me.vlBottomFanZone1.TabIndex = 45
    '
    'vlTopFanZone1
    '
    Me.vlTopFanZone1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTopFanZone1.ForeColor = System.Drawing.Color.Black
    Me.vlTopFanZone1.Format = "Top: 0.0 %"
    Me.vlTopFanZone1.Location = New System.Drawing.Point(480, 319)
    Me.vlTopFanZone1.Name = "vlTopFanZone1"
    Me.vlTopFanZone1.NumberScale = 1000
    Me.vlTopFanZone1.Size = New System.Drawing.Size(73, 16)
    Me.vlTopFanZone1.TabIndex = 44
    '
    'vlTempDesiredZone6
    '
    Me.vlTempDesiredZone6.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTempDesiredZone6.ForeColor = System.Drawing.Color.Black
    Me.vlTempDesiredZone6.Format = "0.0 F"
    Me.vlTempDesiredZone6.Location = New System.Drawing.Point(750, 508)
    Me.vlTempDesiredZone6.Name = "vlTempDesiredZone6"
    Me.vlTempDesiredZone6.Size = New System.Drawing.Size(37, 16)
    Me.vlTempDesiredZone6.TabIndex = 43
    '
    'vlTempDesiredZone5
    '
    Me.vlTempDesiredZone5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTempDesiredZone5.ForeColor = System.Drawing.Color.Black
    Me.vlTempDesiredZone5.Format = "0.0 F"
    Me.vlTempDesiredZone5.Location = New System.Drawing.Point(700, 508)
    Me.vlTempDesiredZone5.Name = "vlTempDesiredZone5"
    Me.vlTempDesiredZone5.Size = New System.Drawing.Size(37, 16)
    Me.vlTempDesiredZone5.TabIndex = 42
    '
    'vlTempDesiredZone4
    '
    Me.vlTempDesiredZone4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTempDesiredZone4.ForeColor = System.Drawing.Color.Black
    Me.vlTempDesiredZone4.Format = "0.0 F"
    Me.vlTempDesiredZone4.Location = New System.Drawing.Point(645, 508)
    Me.vlTempDesiredZone4.Name = "vlTempDesiredZone4"
    Me.vlTempDesiredZone4.Size = New System.Drawing.Size(37, 16)
    Me.vlTempDesiredZone4.TabIndex = 41
    '
    'vlTempDesiredZone3
    '
    Me.vlTempDesiredZone3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTempDesiredZone3.ForeColor = System.Drawing.Color.Black
    Me.vlTempDesiredZone3.Format = "0.0 F"
    Me.vlTempDesiredZone3.Location = New System.Drawing.Point(590, 508)
    Me.vlTempDesiredZone3.Name = "vlTempDesiredZone3"
    Me.vlTempDesiredZone3.Size = New System.Drawing.Size(37, 16)
    Me.vlTempDesiredZone3.TabIndex = 40
    '
    'vlTempDesiredZone2
    '
    Me.vlTempDesiredZone2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTempDesiredZone2.ForeColor = System.Drawing.Color.Black
    Me.vlTempDesiredZone2.Format = "0.0 F"
    Me.vlTempDesiredZone2.Location = New System.Drawing.Point(535, 508)
    Me.vlTempDesiredZone2.Name = "vlTempDesiredZone2"
    Me.vlTempDesiredZone2.Size = New System.Drawing.Size(37, 16)
    Me.vlTempDesiredZone2.TabIndex = 39
    '
    'vlTempDesiredZone1
    '
    Me.vlTempDesiredZone1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTempDesiredZone1.ForeColor = System.Drawing.Color.Black
    Me.vlTempDesiredZone1.Format = "0.0 F"
    Me.vlTempDesiredZone1.Location = New System.Drawing.Point(480, 508)
    Me.vlTempDesiredZone1.Name = "vlTempDesiredZone1"
    Me.vlTempDesiredZone1.Size = New System.Drawing.Size(37, 16)
    Me.vlTempDesiredZone1.TabIndex = 38
    '
    'vlTempActualZone6
    '
    Me.vlTempActualZone6.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTempActualZone6.ForeColor = System.Drawing.Color.Black
    Me.vlTempActualZone6.Format = "0.0 F"
    Me.vlTempActualZone6.Location = New System.Drawing.Point(750, 530)
    Me.vlTempActualZone6.Name = "vlTempActualZone6"
    Me.vlTempActualZone6.Size = New System.Drawing.Size(37, 16)
    Me.vlTempActualZone6.TabIndex = 37
    '
    'vlTempActualZone5
    '
    Me.vlTempActualZone5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTempActualZone5.ForeColor = System.Drawing.Color.Black
    Me.vlTempActualZone5.Format = "0.0 F"
    Me.vlTempActualZone5.Location = New System.Drawing.Point(700, 530)
    Me.vlTempActualZone5.Name = "vlTempActualZone5"
    Me.vlTempActualZone5.Size = New System.Drawing.Size(37, 16)
    Me.vlTempActualZone5.TabIndex = 36
    '
    'vlTempActualZone4
    '
    Me.vlTempActualZone4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTempActualZone4.ForeColor = System.Drawing.Color.Black
    Me.vlTempActualZone4.Format = "0.0 F"
    Me.vlTempActualZone4.Location = New System.Drawing.Point(645, 530)
    Me.vlTempActualZone4.Name = "vlTempActualZone4"
    Me.vlTempActualZone4.Size = New System.Drawing.Size(37, 16)
    Me.vlTempActualZone4.TabIndex = 35
    '
    'vlTempActualZone3
    '
    Me.vlTempActualZone3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTempActualZone3.ForeColor = System.Drawing.Color.Black
    Me.vlTempActualZone3.Format = "0.0 F"
    Me.vlTempActualZone3.Location = New System.Drawing.Point(590, 530)
    Me.vlTempActualZone3.Name = "vlTempActualZone3"
    Me.vlTempActualZone3.Size = New System.Drawing.Size(37, 16)
    Me.vlTempActualZone3.TabIndex = 34
    '
    'vlTempActualZone2
    '
    Me.vlTempActualZone2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTempActualZone2.ForeColor = System.Drawing.Color.Black
    Me.vlTempActualZone2.Format = "0.0 F"
    Me.vlTempActualZone2.Location = New System.Drawing.Point(535, 530)
    Me.vlTempActualZone2.Name = "vlTempActualZone2"
    Me.vlTempActualZone2.Size = New System.Drawing.Size(37, 16)
    Me.vlTempActualZone2.TabIndex = 33
    '
    'vlTempActualZone1
    '
    Me.vlTempActualZone1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlTempActualZone1.ForeColor = System.Drawing.Color.Black
    Me.vlTempActualZone1.Format = "0.0 F"
    Me.vlTempActualZone1.Location = New System.Drawing.Point(480, 530)
    Me.vlTempActualZone1.Name = "vlTempActualZone1"
    Me.vlTempActualZone1.Size = New System.Drawing.Size(37, 16)
    Me.vlTempActualZone1.TabIndex = 26
    '
    'vlFanExhaust2
    '
    Me.vlFanExhaust2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlFanExhaust2.ForeColor = System.Drawing.Color.Black
    Me.vlFanExhaust2.Format = "0.0 %"
    Me.vlFanExhaust2.Location = New System.Drawing.Point(795, 422)
    Me.vlFanExhaust2.Name = "vlFanExhaust2"
    Me.vlFanExhaust2.NumberScale = 1000
    Me.vlFanExhaust2.Size = New System.Drawing.Size(42, 16)
    Me.vlFanExhaust2.TabIndex = 24
    '
    'vlFanExhaust1
    '
    Me.vlFanExhaust1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlFanExhaust1.ForeColor = System.Drawing.Color.Black
    Me.vlFanExhaust1.Format = "0.0 %"
    Me.vlFanExhaust1.Location = New System.Drawing.Point(544, 422)
    Me.vlFanExhaust1.Name = "vlFanExhaust1"
    Me.vlFanExhaust1.NumberScale = 1000
    Me.vlFanExhaust1.Size = New System.Drawing.Size(42, 16)
    Me.vlFanExhaust1.TabIndex = 23
    '
    'vlWidthMain
    '
    Me.vlWidthMain.BackColor = System.Drawing.Color.White
    Me.vlWidthMain.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlWidthMain.ForeColor = System.Drawing.Color.Black
    Me.vlWidthMain.Format = "0.0 in"
    Me.vlWidthMain.Location = New System.Drawing.Point(733, 218)
    Me.vlWidthMain.Name = "vlWidthMain"
    Me.vlWidthMain.Size = New System.Drawing.Size(44, 18)
    Me.vlWidthMain.TabIndex = 20
    '
    'vlWidthRear
    '
    Me.vlWidthRear.BackColor = System.Drawing.Color.White
    Me.vlWidthRear.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlWidthRear.ForeColor = System.Drawing.Color.Black
    Me.vlWidthRear.Format = "0.0 in"
    Me.vlWidthRear.Location = New System.Drawing.Point(918, 218)
    Me.vlWidthRear.Name = "vlWidthRear"
    Me.vlWidthRear.Size = New System.Drawing.Size(44, 18)
    Me.vlWidthRear.TabIndex = 19
    '
    'vlWidth2
    '
    Me.vlWidth2.BackColor = System.Drawing.Color.White
    Me.vlWidth2.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlWidth2.ForeColor = System.Drawing.Color.Black
    Me.vlWidth2.Format = "0.0 in"
    Me.vlWidth2.Location = New System.Drawing.Point(436, 218)
    Me.vlWidth2.Name = "vlWidth2"
    Me.vlWidth2.Size = New System.Drawing.Size(44, 18)
    Me.vlWidth2.TabIndex = 18
    '
    'vlWidth1
    '
    Me.vlWidth1.BackColor = System.Drawing.Color.White
    Me.vlWidth1.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlWidth1.ForeColor = System.Drawing.Color.Black
    Me.vlWidth1.Format = "0.0 in"
    Me.vlWidth1.Location = New System.Drawing.Point(342, 218)
    Me.vlWidth1.Name = "vlWidth1"
    Me.vlWidth1.Size = New System.Drawing.Size(44, 18)
    Me.vlWidth1.TabIndex = 17
    '
    'vlSelvageRight
    '
    Me.vlSelvageRight.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlSelvageRight.ForeColor = System.Drawing.Color.Black
    Me.vlSelvageRight.Format = "Selvage (R): 0.0 ypm"
    Me.vlSelvageRight.Location = New System.Drawing.Point(168, 275)
    Me.vlSelvageRight.Name = "vlSelvageRight"
    Me.vlSelvageRight.Size = New System.Drawing.Size(130, 16)
    Me.vlSelvageRight.TabIndex = 16
    '
    'vlSelvageLeft
    '
    Me.vlSelvageLeft.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlSelvageLeft.ForeColor = System.Drawing.Color.Black
    Me.vlSelvageLeft.Format = "Selvage (L): 0.0 ypm"
    Me.vlSelvageLeft.Location = New System.Drawing.Point(168, 155)
    Me.vlSelvageLeft.Name = "vlSelvageLeft"
    Me.vlSelvageLeft.Size = New System.Drawing.Size(128, 16)
    Me.vlSelvageLeft.TabIndex = 15
    '
    'vlOverfeed
    '
    Me.vlOverfeed.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlOverfeed.ForeColor = System.Drawing.Color.Black
    Me.vlOverfeed.Format = "Overfeed: 0.0 ypm"
    Me.vlOverfeed.Location = New System.Drawing.Point(222, 579)
    Me.vlOverfeed.Name = "vlOverfeed"
    Me.vlOverfeed.Size = New System.Drawing.Size(115, 16)
    Me.vlOverfeed.TabIndex = 14
    '
    'vlPadderSpeed2
    '
    Me.vlPadderSpeed2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlPadderSpeed2.ForeColor = System.Drawing.Color.Black
    Me.vlPadderSpeed2.Format = "Padder 2: 0.0 YPM"
    Me.vlPadderSpeed2.Location = New System.Drawing.Point(10, 552)
    Me.vlPadderSpeed2.Name = "vlPadderSpeed2"
    Me.vlPadderSpeed2.Size = New System.Drawing.Size(114, 16)
    Me.vlPadderSpeed2.TabIndex = 13
    '
    'Mimic
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BackColor = System.Drawing.Color.White
    Me.BackgroundImage = Global.My.Resources.Resources.AdeleFrameMimic
    Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
    Me.Controls.Add(Me.vlBottomFanZone8)
    Me.Controls.Add(Me.vlTopFanZone8)
    Me.Controls.Add(Me.vlBottomFanZone7)
    Me.Controls.Add(Me.vlTopFanZone7)
    Me.Controls.Add(Me.vlWidth3)
    Me.Controls.Add(Me.vlTempActualZone8)
    Me.Controls.Add(Me.vlTempActualZone7)
    Me.Controls.Add(Me.vlTempDesiredZone8)
    Me.Controls.Add(Me.vlTempDesiredZone7)
    Me.Controls.Add(Me.vlPadderSpeed1)
    Me.Controls.Add(Me.vlFabricSpeedLimitHigh)
    Me.Controls.Add(Me.vlFabricSpeedLimitLow)
    Me.Controls.Add(Me.vlFabricSpeed)
    Me.Controls.Add(Me.vlBottomFanZone6)
    Me.Controls.Add(Me.vlTopFanZone6)
    Me.Controls.Add(Me.vlBottomFanZone4)
    Me.Controls.Add(Me.vlTopFanZone4)
    Me.Controls.Add(Me.vlBottomFanZone2)
    Me.Controls.Add(Me.vlTopFanZone2)
    Me.Controls.Add(Me.vlBottomFanZone5)
    Me.Controls.Add(Me.vlTopFanZone5)
    Me.Controls.Add(Me.vlBottomFanZone3)
    Me.Controls.Add(Me.vlTopFanZone3)
    Me.Controls.Add(Me.vlBottomFanZone1)
    Me.Controls.Add(Me.vlTopFanZone1)
    Me.Controls.Add(Me.vlTempDesiredZone6)
    Me.Controls.Add(Me.vlTempDesiredZone5)
    Me.Controls.Add(Me.vlTempDesiredZone4)
    Me.Controls.Add(Me.vlTempDesiredZone3)
    Me.Controls.Add(Me.vlTempDesiredZone2)
    Me.Controls.Add(Me.vlTempDesiredZone1)
    Me.Controls.Add(Me.vlTempActualZone6)
    Me.Controls.Add(Me.vlTempActualZone5)
    Me.Controls.Add(Me.vlTempActualZone4)
    Me.Controls.Add(Me.vlTempActualZone3)
    Me.Controls.Add(Me.vlTempActualZone2)
    Me.Controls.Add(Me.vlTempActualZone1)
    Me.Controls.Add(Me.vlFanExhaust2)
    Me.Controls.Add(Me.vlFanExhaust1)
    Me.Controls.Add(Me.vlWidthMain)
    Me.Controls.Add(Me.vlWidthRear)
    Me.Controls.Add(Me.vlWidth2)
    Me.Controls.Add(Me.vlWidth1)
    Me.Controls.Add(Me.vlSelvageRight)
    Me.Controls.Add(Me.vlSelvageLeft)
    Me.Controls.Add(Me.vlOverfeed)
    Me.Controls.Add(Me.vlPadderSpeed2)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
    Me.Name = "Mimic"
    Me.Size = New System.Drawing.Size(1029, 658)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Timer1 As System.Windows.Forms.Timer
  Friend WithEvents vlPadderSpeed2 As MimicControls.ValueLabel
  Friend WithEvents vlOverfeed As MimicControls.ValueLabel
  Friend WithEvents vlSelvageLeft As MimicControls.ValueLabel
  Friend WithEvents vlSelvageRight As MimicControls.ValueLabel
  Friend WithEvents vlWidth1 As MimicControls.ValueLabel
  Friend WithEvents vlWidth2 As MimicControls.ValueLabel
  Friend WithEvents vlWidthRear As MimicControls.ValueLabel
  Friend WithEvents vlWidthMain As MimicControls.ValueLabel
  Friend WithEvents vlFanExhaust1 As MimicControls.ValueLabel
  Friend WithEvents vlFanExhaust2 As MimicControls.ValueLabel
  Friend WithEvents vlTempActualZone6 As MimicControls.ValueLabel
  Friend WithEvents vlTempActualZone5 As MimicControls.ValueLabel
  Friend WithEvents vlTempActualZone4 As MimicControls.ValueLabel
  Friend WithEvents vlTempActualZone3 As MimicControls.ValueLabel
  Friend WithEvents vlTempActualZone2 As MimicControls.ValueLabel
  Friend WithEvents vlTempActualZone1 As MimicControls.ValueLabel
  Friend WithEvents vlTempDesiredZone1 As MimicControls.ValueLabel
  Friend WithEvents vlTempDesiredZone2 As MimicControls.ValueLabel
  Friend WithEvents vlTempDesiredZone3 As MimicControls.ValueLabel
  Friend WithEvents vlTempDesiredZone5 As MimicControls.ValueLabel
  Friend WithEvents vlTempDesiredZone6 As MimicControls.ValueLabel
  Friend WithEvents vlTempDesiredZone4 As MimicControls.ValueLabel
  Friend WithEvents vlTopFanZone1 As MimicControls.ValueLabel
  Friend WithEvents vlBottomFanZone1 As MimicControls.ValueLabel
  Friend WithEvents vlBottomFanZone3 As MimicControls.ValueLabel
  Friend WithEvents vlTopFanZone3 As MimicControls.ValueLabel
  Friend WithEvents vlBottomFanZone5 As MimicControls.ValueLabel
  Friend WithEvents vlTopFanZone5 As MimicControls.ValueLabel
  Friend WithEvents vlBottomFanZone2 As MimicControls.ValueLabel
  Friend WithEvents vlTopFanZone2 As MimicControls.ValueLabel
  Friend WithEvents vlBottomFanZone4 As MimicControls.ValueLabel
  Friend WithEvents vlTopFanZone4 As MimicControls.ValueLabel
  Friend WithEvents vlBottomFanZone6 As MimicControls.ValueLabel
  Friend WithEvents vlTopFanZone6 As MimicControls.ValueLabel
  Friend WithEvents vlFabricSpeedLimitHigh As MimicControls.ValueLabel
  Friend WithEvents vlFabricSpeedLimitLow As MimicControls.ValueLabel
  Friend WithEvents vlFabricSpeed As MimicControls.ValueLabel
  Friend WithEvents vlPadderSpeed1 As ValueLabel
  Friend WithEvents vlTempDesiredZone7 As ValueLabel
  Friend WithEvents vlTempDesiredZone8 As ValueLabel
  Friend WithEvents vlTempActualZone7 As ValueLabel
  Friend WithEvents vlTempActualZone8 As ValueLabel
  Friend WithEvents vlWidth3 As ValueLabel
  Friend WithEvents vlBottomFanZone7 As ValueLabel
  Friend WithEvents vlTopFanZone7 As ValueLabel
  Friend WithEvents vlBottomFanZone8 As ValueLabel
  Friend WithEvents vlTopFanZone8 As ValueLabel
End Class
