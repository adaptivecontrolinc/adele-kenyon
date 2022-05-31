<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UtilitiesControl
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
    Me.components = New System.ComponentModel.Container
    Me.gridUtilities = New System.Windows.Forms.DataGridView
    Me.gbGasUsage = New System.Windows.Forms.GroupBox
    Me.btnGasUsageRefresh = New System.Windows.Forms.Button
    Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
    Me.gbAirTempStatus = New System.Windows.Forms.GroupBox
    Me.gbZone1 = New System.Windows.Forms.GroupBox
    Me.gbZone2 = New System.Windows.Forms.GroupBox
    Me.gbZone3 = New System.Windows.Forms.GroupBox
    Me.gbZone4 = New System.Windows.Forms.GroupBox
    Me.gbZone5 = New System.Windows.Forms.GroupBox
    Me.gbZone6 = New System.Windows.Forms.GroupBox
    Me.lbAirZone6Actual = New MimicControls.Label
    Me.lbAirZone6Setpoint = New MimicControls.Label
    Me.outputZone6 = New MimicControls.ValueLabel
    Me.lbAirZone5Actual = New MimicControls.Label
    Me.lbAirZone5Setpoint = New MimicControls.Label
    Me.outputZone5 = New MimicControls.ValueLabel
    Me.lbAirZone4Actual = New MimicControls.Label
    Me.lbAirZone4Setpoint = New MimicControls.Label
    Me.outputZone4 = New MimicControls.ValueLabel
    Me.lbAirZone3Actual = New MimicControls.Label
    Me.lbAirZone3Setpoint = New MimicControls.Label
    Me.outputZone3 = New MimicControls.ValueLabel
    Me.lbAirZone2Actual = New MimicControls.Label
    Me.lbAirZone2Setpoint = New MimicControls.Label
    Me.outputZone2 = New MimicControls.ValueLabel
    Me.lbAirZone1Actual = New MimicControls.Label
    Me.lbAirZone1Setpoint = New MimicControls.Label
    Me.outputZone1 = New MimicControls.ValueLabel
    Me.Label5 = New MimicControls.Label
    Me.lbGasUsedDecatherms = New MimicControls.Label
    Me.vlGasFlowRate = New MimicControls.ValueLabel
    Me.vlGasUsedCubicFeet = New MimicControls.ValueLabel
    CType(Me.gridUtilities, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.gbGasUsage.SuspendLayout()
    Me.gbAirTempStatus.SuspendLayout()
    Me.gbZone1.SuspendLayout()
    Me.gbZone2.SuspendLayout()
    Me.gbZone3.SuspendLayout()
    Me.gbZone4.SuspendLayout()
    Me.gbZone5.SuspendLayout()
    Me.gbZone6.SuspendLayout()
    Me.SuspendLayout()
    '
    'gridUtilities
    '
    Me.gridUtilities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.gridUtilities.Location = New System.Drawing.Point(6, 25)
    Me.gridUtilities.Name = "gridUtilities"
    Me.gridUtilities.Size = New System.Drawing.Size(599, 550)
    Me.gridUtilities.TabIndex = 1
    '
    'gbGasUsage
    '
    Me.gbGasUsage.BackColor = System.Drawing.Color.Transparent
    Me.gbGasUsage.Controls.Add(Me.lbGasUsedDecatherms)
    Me.gbGasUsage.Controls.Add(Me.vlGasFlowRate)
    Me.gbGasUsage.Controls.Add(Me.vlGasUsedCubicFeet)
    Me.gbGasUsage.Controls.Add(Me.btnGasUsageRefresh)
    Me.gbGasUsage.Controls.Add(Me.gridUtilities)
    Me.gbGasUsage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.gbGasUsage.Font = New System.Drawing.Font("Tahoma", 11.25!)
    Me.gbGasUsage.ForeColor = System.Drawing.Color.Blue
    Me.gbGasUsage.Location = New System.Drawing.Point(3, 3)
    Me.gbGasUsage.Name = "gbGasUsage"
    Me.gbGasUsage.Size = New System.Drawing.Size(611, 652)
    Me.gbGasUsage.TabIndex = 7
    Me.gbGasUsage.TabStop = False
    Me.gbGasUsage.Text = "Gas Usage"
    '
    'btnGasUsageRefresh
    '
    Me.btnGasUsageRefresh.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.btnGasUsageRefresh.Location = New System.Drawing.Point(6, 581)
    Me.btnGasUsageRefresh.Name = "btnGasUsageRefresh"
    Me.btnGasUsageRefresh.Size = New System.Drawing.Size(94, 64)
    Me.btnGasUsageRefresh.TabIndex = 235
    Me.btnGasUsageRefresh.Text = "Refresh"
    Me.btnGasUsageRefresh.UseVisualStyleBackColor = True
    '
    'Timer1
    '
    Me.Timer1.Enabled = True
    Me.Timer1.Interval = 1000
    '
    'gbAirTempStatus
    '
    Me.gbAirTempStatus.BackColor = System.Drawing.Color.Transparent
    Me.gbAirTempStatus.Controls.Add(Me.gbZone6)
    Me.gbAirTempStatus.Controls.Add(Me.gbZone5)
    Me.gbAirTempStatus.Controls.Add(Me.gbZone4)
    Me.gbAirTempStatus.Controls.Add(Me.gbZone3)
    Me.gbAirTempStatus.Controls.Add(Me.gbZone2)
    Me.gbAirTempStatus.Controls.Add(Me.gbZone1)
    Me.gbAirTempStatus.Controls.Add(Me.Label5)
    Me.gbAirTempStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.gbAirTempStatus.Font = New System.Drawing.Font("Tahoma", 11.25!)
    Me.gbAirTempStatus.ForeColor = System.Drawing.Color.Blue
    Me.gbAirTempStatus.Location = New System.Drawing.Point(620, 3)
    Me.gbAirTempStatus.Name = "gbAirTempStatus"
    Me.gbAirTempStatus.Size = New System.Drawing.Size(406, 652)
    Me.gbAirTempStatus.TabIndex = 39
    Me.gbAirTempStatus.TabStop = False
    Me.gbAirTempStatus.Text = "Air Temp Status"
    '
    'gbZone1
    '
    Me.gbZone1.Controls.Add(Me.lbAirZone1Actual)
    Me.gbZone1.Controls.Add(Me.lbAirZone1Setpoint)
    Me.gbZone1.Controls.Add(Me.outputZone1)
    Me.gbZone1.ForeColor = System.Drawing.Color.Black
    Me.gbZone1.Location = New System.Drawing.Point(10, 20)
    Me.gbZone1.Name = "gbZone1"
    Me.gbZone1.Size = New System.Drawing.Size(390, 90)
    Me.gbZone1.TabIndex = 40
    Me.gbZone1.TabStop = False
    Me.gbZone1.Text = "Zone 1"
    '
    'gbZone2
    '
    Me.gbZone2.Controls.Add(Me.lbAirZone2Actual)
    Me.gbZone2.Controls.Add(Me.lbAirZone2Setpoint)
    Me.gbZone2.Controls.Add(Me.outputZone2)
    Me.gbZone2.ForeColor = System.Drawing.Color.Black
    Me.gbZone2.Location = New System.Drawing.Point(10, 116)
    Me.gbZone2.Name = "gbZone2"
    Me.gbZone2.Size = New System.Drawing.Size(390, 90)
    Me.gbZone2.TabIndex = 41
    Me.gbZone2.TabStop = False
    Me.gbZone2.Text = "Zone 2"
    '
    'gbZone3
    '
    Me.gbZone3.Controls.Add(Me.lbAirZone3Actual)
    Me.gbZone3.Controls.Add(Me.lbAirZone3Setpoint)
    Me.gbZone3.Controls.Add(Me.outputZone3)
    Me.gbZone3.ForeColor = System.Drawing.Color.Black
    Me.gbZone3.Location = New System.Drawing.Point(10, 212)
    Me.gbZone3.Name = "gbZone3"
    Me.gbZone3.Size = New System.Drawing.Size(390, 90)
    Me.gbZone3.TabIndex = 41
    Me.gbZone3.TabStop = False
    Me.gbZone3.Text = "Zone 3"
    '
    'gbZone4
    '
    Me.gbZone4.Controls.Add(Me.lbAirZone4Actual)
    Me.gbZone4.Controls.Add(Me.lbAirZone4Setpoint)
    Me.gbZone4.Controls.Add(Me.outputZone4)
    Me.gbZone4.ForeColor = System.Drawing.Color.Black
    Me.gbZone4.Location = New System.Drawing.Point(10, 308)
    Me.gbZone4.Name = "gbZone4"
    Me.gbZone4.Size = New System.Drawing.Size(390, 90)
    Me.gbZone4.TabIndex = 41
    Me.gbZone4.TabStop = False
    Me.gbZone4.Text = "Zone 4"
    '
    'gbZone5
    '
    Me.gbZone5.Controls.Add(Me.lbAirZone5Actual)
    Me.gbZone5.Controls.Add(Me.lbAirZone5Setpoint)
    Me.gbZone5.Controls.Add(Me.outputZone5)
    Me.gbZone5.ForeColor = System.Drawing.Color.Black
    Me.gbZone5.Location = New System.Drawing.Point(10, 404)
    Me.gbZone5.Name = "gbZone5"
    Me.gbZone5.Size = New System.Drawing.Size(390, 90)
    Me.gbZone5.TabIndex = 41
    Me.gbZone5.TabStop = False
    Me.gbZone5.Text = "Zone 5"
    '
    'gbZone6
    '
    Me.gbZone6.Controls.Add(Me.lbAirZone6Actual)
    Me.gbZone6.Controls.Add(Me.lbAirZone6Setpoint)
    Me.gbZone6.Controls.Add(Me.outputZone6)
    Me.gbZone6.ForeColor = System.Drawing.Color.Black
    Me.gbZone6.Location = New System.Drawing.Point(10, 500)
    Me.gbZone6.Name = "gbZone6"
    Me.gbZone6.Size = New System.Drawing.Size(390, 90)
    Me.gbZone6.TabIndex = 42
    Me.gbZone6.TabStop = False
    Me.gbZone6.Text = "Zone 6"
    '
    'lbAirZone6Actual
    '
    Me.lbAirZone6Actual.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAirZone6Actual.ForeColor = System.Drawing.Color.Black
    Me.lbAirZone6Actual.Location = New System.Drawing.Point(15, 45)
    Me.lbAirZone6Actual.Name = "lbAirZone6Actual"
    Me.lbAirZone6Actual.Size = New System.Drawing.Size(85, 16)
    Me.lbAirZone6Actual.TabIndex = 35
    Me.lbAirZone6Actual.Text = "Actual: 0.0  F"
    '
    'lbAirZone6Setpoint
    '
    Me.lbAirZone6Setpoint.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAirZone6Setpoint.ForeColor = System.Drawing.Color.Black
    Me.lbAirZone6Setpoint.Location = New System.Drawing.Point(15, 25)
    Me.lbAirZone6Setpoint.Name = "lbAirZone6Setpoint"
    Me.lbAirZone6Setpoint.Size = New System.Drawing.Size(97, 16)
    Me.lbAirZone6Setpoint.TabIndex = 34
    Me.lbAirZone6Setpoint.Text = "Setpoint: 0.0  F"
    '
    'outputZone6
    '
    Me.outputZone6.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.outputZone6.ForeColor = System.Drawing.Color.Black
    Me.outputZone6.Format = "Output: 0.0 %"
    Me.outputZone6.Location = New System.Drawing.Point(15, 65)
    Me.outputZone6.Name = "outputZone6"
    Me.outputZone6.Size = New System.Drawing.Size(89, 16)
    Me.outputZone6.TabIndex = 33
    '
    'lbAirZone5Actual
    '
    Me.lbAirZone5Actual.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAirZone5Actual.ForeColor = System.Drawing.Color.Black
    Me.lbAirZone5Actual.Location = New System.Drawing.Point(15, 45)
    Me.lbAirZone5Actual.Name = "lbAirZone5Actual"
    Me.lbAirZone5Actual.Size = New System.Drawing.Size(85, 16)
    Me.lbAirZone5Actual.TabIndex = 35
    Me.lbAirZone5Actual.Text = "Actual: 0.0  F"
    '
    'lbAirZone5Setpoint
    '
    Me.lbAirZone5Setpoint.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAirZone5Setpoint.ForeColor = System.Drawing.Color.Black
    Me.lbAirZone5Setpoint.Location = New System.Drawing.Point(15, 25)
    Me.lbAirZone5Setpoint.Name = "lbAirZone5Setpoint"
    Me.lbAirZone5Setpoint.Size = New System.Drawing.Size(97, 16)
    Me.lbAirZone5Setpoint.TabIndex = 34
    Me.lbAirZone5Setpoint.Text = "Setpoint: 0.0  F"
    '
    'outputZone5
    '
    Me.outputZone5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.outputZone5.ForeColor = System.Drawing.Color.Black
    Me.outputZone5.Format = "Output: 0.0 %"
    Me.outputZone5.Location = New System.Drawing.Point(15, 65)
    Me.outputZone5.Name = "outputZone5"
    Me.outputZone5.Size = New System.Drawing.Size(89, 16)
    Me.outputZone5.TabIndex = 33
    '
    'lbAirZone4Actual
    '
    Me.lbAirZone4Actual.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAirZone4Actual.ForeColor = System.Drawing.Color.Black
    Me.lbAirZone4Actual.Location = New System.Drawing.Point(15, 45)
    Me.lbAirZone4Actual.Name = "lbAirZone4Actual"
    Me.lbAirZone4Actual.Size = New System.Drawing.Size(85, 16)
    Me.lbAirZone4Actual.TabIndex = 35
    Me.lbAirZone4Actual.Text = "Actual: 0.0  F"
    '
    'lbAirZone4Setpoint
    '
    Me.lbAirZone4Setpoint.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAirZone4Setpoint.ForeColor = System.Drawing.Color.Black
    Me.lbAirZone4Setpoint.Location = New System.Drawing.Point(15, 25)
    Me.lbAirZone4Setpoint.Name = "lbAirZone4Setpoint"
    Me.lbAirZone4Setpoint.Size = New System.Drawing.Size(97, 16)
    Me.lbAirZone4Setpoint.TabIndex = 34
    Me.lbAirZone4Setpoint.Text = "Setpoint: 0.0  F"
    '
    'outputZone4
    '
    Me.outputZone4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.outputZone4.ForeColor = System.Drawing.Color.Black
    Me.outputZone4.Format = "Output: 0.0 %"
    Me.outputZone4.Location = New System.Drawing.Point(15, 65)
    Me.outputZone4.Name = "outputZone4"
    Me.outputZone4.Size = New System.Drawing.Size(89, 16)
    Me.outputZone4.TabIndex = 33
    '
    'lbAirZone3Actual
    '
    Me.lbAirZone3Actual.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAirZone3Actual.ForeColor = System.Drawing.Color.Black
    Me.lbAirZone3Actual.Location = New System.Drawing.Point(15, 45)
    Me.lbAirZone3Actual.Name = "lbAirZone3Actual"
    Me.lbAirZone3Actual.Size = New System.Drawing.Size(85, 16)
    Me.lbAirZone3Actual.TabIndex = 35
    Me.lbAirZone3Actual.Text = "Actual: 0.0  F"
    '
    'lbAirZone3Setpoint
    '
    Me.lbAirZone3Setpoint.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAirZone3Setpoint.ForeColor = System.Drawing.Color.Black
    Me.lbAirZone3Setpoint.Location = New System.Drawing.Point(15, 25)
    Me.lbAirZone3Setpoint.Name = "lbAirZone3Setpoint"
    Me.lbAirZone3Setpoint.Size = New System.Drawing.Size(97, 16)
    Me.lbAirZone3Setpoint.TabIndex = 34
    Me.lbAirZone3Setpoint.Text = "Setpoint: 0.0  F"
    '
    'outputZone3
    '
    Me.outputZone3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.outputZone3.ForeColor = System.Drawing.Color.Black
    Me.outputZone3.Format = "Output: 0.0 %"
    Me.outputZone3.Location = New System.Drawing.Point(15, 65)
    Me.outputZone3.Name = "outputZone3"
    Me.outputZone3.Size = New System.Drawing.Size(89, 16)
    Me.outputZone3.TabIndex = 33
    '
    'lbAirZone2Actual
    '
    Me.lbAirZone2Actual.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAirZone2Actual.ForeColor = System.Drawing.Color.Black
    Me.lbAirZone2Actual.Location = New System.Drawing.Point(15, 45)
    Me.lbAirZone2Actual.Name = "lbAirZone2Actual"
    Me.lbAirZone2Actual.Size = New System.Drawing.Size(85, 16)
    Me.lbAirZone2Actual.TabIndex = 35
    Me.lbAirZone2Actual.Text = "Actual: 0.0  F"
    '
    'lbAirZone2Setpoint
    '
    Me.lbAirZone2Setpoint.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAirZone2Setpoint.ForeColor = System.Drawing.Color.Black
    Me.lbAirZone2Setpoint.Location = New System.Drawing.Point(15, 25)
    Me.lbAirZone2Setpoint.Name = "lbAirZone2Setpoint"
    Me.lbAirZone2Setpoint.Size = New System.Drawing.Size(97, 16)
    Me.lbAirZone2Setpoint.TabIndex = 34
    Me.lbAirZone2Setpoint.Text = "Setpoint: 0.0  F"
    '
    'outputZone2
    '
    Me.outputZone2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.outputZone2.ForeColor = System.Drawing.Color.Black
    Me.outputZone2.Format = "Output: 0.0 %"
    Me.outputZone2.Location = New System.Drawing.Point(15, 65)
    Me.outputZone2.Name = "outputZone2"
    Me.outputZone2.Size = New System.Drawing.Size(89, 16)
    Me.outputZone2.TabIndex = 33
    '
    'lbAirZone1Actual
    '
    Me.lbAirZone1Actual.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAirZone1Actual.ForeColor = System.Drawing.Color.Black
    Me.lbAirZone1Actual.Location = New System.Drawing.Point(15, 45)
    Me.lbAirZone1Actual.Name = "lbAirZone1Actual"
    Me.lbAirZone1Actual.Size = New System.Drawing.Size(85, 16)
    Me.lbAirZone1Actual.TabIndex = 35
    Me.lbAirZone1Actual.Text = "Actual: 0.0  F"
    '
    'lbAirZone1Setpoint
    '
    Me.lbAirZone1Setpoint.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAirZone1Setpoint.ForeColor = System.Drawing.Color.Black
    Me.lbAirZone1Setpoint.Location = New System.Drawing.Point(15, 25)
    Me.lbAirZone1Setpoint.Name = "lbAirZone1Setpoint"
    Me.lbAirZone1Setpoint.Size = New System.Drawing.Size(97, 16)
    Me.lbAirZone1Setpoint.TabIndex = 34
    Me.lbAirZone1Setpoint.Text = "Setpoint: 0.0  F"
    '
    'outputZone1
    '
    Me.outputZone1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.outputZone1.ForeColor = System.Drawing.Color.Black
    Me.outputZone1.Format = "Output: 0.0 %"
    Me.outputZone1.Location = New System.Drawing.Point(15, 65)
    Me.outputZone1.Name = "outputZone1"
    Me.outputZone1.Size = New System.Drawing.Size(89, 16)
    Me.outputZone1.TabIndex = 33
    '
    'Label5
    '
    Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label5.ForeColor = System.Drawing.Color.Black
    Me.Label5.Location = New System.Drawing.Point(10, 198)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(12, 16)
    Me.Label5.TabIndex = 27
    Me.Label5.Text = "."
    '
    'lbGasUsedDecatherms
    '
    Me.lbGasUsedDecatherms.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
    Me.lbGasUsedDecatherms.ForeColor = System.Drawing.Color.Black
    Me.lbGasUsedDecatherms.Location = New System.Drawing.Point(106, 603)
    Me.lbGasUsedDecatherms.Name = "lbGasUsedDecatherms"
    Me.lbGasUsedDecatherms.Size = New System.Drawing.Size(123, 16)
    Me.lbGasUsedDecatherms.TabIndex = 240
    Me.lbGasUsedDecatherms.Text = "DecaTherms: 0 Dth"
    '
    'vlGasFlowRate
    '
    Me.vlGasFlowRate.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlGasFlowRate.ForeColor = System.Drawing.Color.Black
    Me.vlGasFlowRate.Format = "Gas Flow Rate: 0 cfm"
    Me.vlGasFlowRate.Location = New System.Drawing.Point(106, 625)
    Me.vlGasFlowRate.Name = "vlGasFlowRate"
    Me.vlGasFlowRate.Size = New System.Drawing.Size(131, 16)
    Me.vlGasFlowRate.TabIndex = 239
    '
    'vlGasUsedCubicFeet
    '
    Me.vlGasUsedCubicFeet.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.vlGasUsedCubicFeet.ForeColor = System.Drawing.Color.Black
    Me.vlGasUsedCubicFeet.Format = "Gas Used - Cubic Feet: 0 "
    Me.vlGasUsedCubicFeet.Location = New System.Drawing.Point(106, 581)
    Me.vlGasUsedCubicFeet.Name = "vlGasUsedCubicFeet"
    Me.vlGasUsedCubicFeet.Size = New System.Drawing.Size(154, 16)
    Me.vlGasUsedCubicFeet.TabIndex = 238
    '
    'UtilitiesControl
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BackColor = System.Drawing.Color.White
    Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
    Me.Controls.Add(Me.gbAirTempStatus)
    Me.Controls.Add(Me.gbGasUsage)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
    Me.Name = "UtilitiesControl"
    Me.Size = New System.Drawing.Size(1029, 658)
    CType(Me.gridUtilities, System.ComponentModel.ISupportInitialize).EndInit()
    Me.gbGasUsage.ResumeLayout(False)
    Me.gbGasUsage.PerformLayout()
    Me.gbAirTempStatus.ResumeLayout(False)
    Me.gbAirTempStatus.PerformLayout()
    Me.gbZone1.ResumeLayout(False)
    Me.gbZone1.PerformLayout()
    Me.gbZone2.ResumeLayout(False)
    Me.gbZone2.PerformLayout()
    Me.gbZone3.ResumeLayout(False)
    Me.gbZone3.PerformLayout()
    Me.gbZone4.ResumeLayout(False)
    Me.gbZone4.PerformLayout()
    Me.gbZone5.ResumeLayout(False)
    Me.gbZone5.PerformLayout()
    Me.gbZone6.ResumeLayout(False)
    Me.gbZone6.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents gridUtilities As System.Windows.Forms.DataGridView
  Friend WithEvents gbGasUsage As System.Windows.Forms.GroupBox
  Friend WithEvents btnGasUsageRefresh As System.Windows.Forms.Button
  Friend WithEvents lbGasUsedDecatherms As MimicControls.Label
  Friend WithEvents vlGasFlowRate As MimicControls.ValueLabel
  Friend WithEvents vlGasUsedCubicFeet As MimicControls.ValueLabel
  Friend WithEvents Timer1 As System.Windows.Forms.Timer
  Friend WithEvents Label5 As MimicControls.Label
  Friend WithEvents lbAirZone1Setpoint As MimicControls.Label
  Friend WithEvents outputZone1 As MimicControls.ValueLabel
  Friend WithEvents gbAirTempStatus As System.Windows.Forms.GroupBox
  Friend WithEvents gbZone1 As System.Windows.Forms.GroupBox
  Friend WithEvents lbAirZone1Actual As MimicControls.Label
  Friend WithEvents gbZone6 As System.Windows.Forms.GroupBox
  Friend WithEvents lbAirZone6Actual As MimicControls.Label
  Friend WithEvents lbAirZone6Setpoint As MimicControls.Label
  Friend WithEvents outputZone6 As MimicControls.ValueLabel
  Friend WithEvents gbZone5 As System.Windows.Forms.GroupBox
  Friend WithEvents lbAirZone5Actual As MimicControls.Label
  Friend WithEvents lbAirZone5Setpoint As MimicControls.Label
  Friend WithEvents outputZone5 As MimicControls.ValueLabel
  Friend WithEvents gbZone4 As System.Windows.Forms.GroupBox
  Friend WithEvents lbAirZone4Actual As MimicControls.Label
  Friend WithEvents lbAirZone4Setpoint As MimicControls.Label
  Friend WithEvents outputZone4 As MimicControls.ValueLabel
  Friend WithEvents gbZone3 As System.Windows.Forms.GroupBox
  Friend WithEvents lbAirZone3Actual As MimicControls.Label
  Friend WithEvents lbAirZone3Setpoint As MimicControls.Label
  Friend WithEvents outputZone3 As MimicControls.ValueLabel
  Friend WithEvents gbZone2 As System.Windows.Forms.GroupBox
  Friend WithEvents lbAirZone2Actual As MimicControls.Label
  Friend WithEvents lbAirZone2Setpoint As MimicControls.Label
  Friend WithEvents outputZone2 As MimicControls.ValueLabel

End Class
