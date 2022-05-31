' Version [2011-10-11]

Partial Class Pleva

  <Parameter(0, 1), Category("Pleva Temperature"), _
    Description("Set to '1' to enable fabric speed adjustment based on Pleva temperature input and command parameters.")> _
  Public Parameters_PlevaTemperatureEnable As Integer
  <Parameter(20, 300), Category("Pleva Temperature"), _
    Description("Time, in seconds, to delay pleva adjustment when switching from 'Stopped' or 'Threading' states. Minimum 15sec, Maximum 60sec.")> _
  Public Parameters_PlevaSpeedInitializeDelay As Integer

  <Parameter(0, 100), Category("Pleva Temperature"), _
    Description("Distance, in tenth yards, between zone fabric temperature sensors (9 ft = 3 yds = 30).")> _
  Public Parameters_PlevaZoneLength As Integer
  <Parameter(1, 300), Category("Pleva Temperature"), _
    Description("Time, in seconds, to delay between fabric speed adjustments when enabled and controlling. Minimum 1sec, Maximum 300sec.")> _
  Public Parameters_PlevaSpeedAdjustDelay As Integer
  <Parameter(3, 120), Category("Pleva Temperature"), _
    Description("Delay, in seconds, to delay once adjusting fabric speed.  Used for both acceleration and deceleration.  Minimum 3sec, Maximum 120sec.")> _
  Public Parameters_PlevaSpeedAccelDelay As Integer
  <Parameter(0, 1000), Category("Pleva Temperature"), _
    Description("Adjust factor to offset the new fabric speed based on the deviation from desired time at temp.")> _
  Public Parameters_PlevaSpeedAdjustFactor As Integer
  <Parameter(0, 1000), Category("Pleva Temperature"), _
    Description("Maximum Allowed adjustment, in tenths YPM, to offset the current fabric speed when requesting a new fabric speed. Minimum 0, Maximum 200 (20.0ypm).")> _
  Public Parameters_PlevaSpeedAdjustMax As Integer
  <Parameter(2, 30), Category("Pleva Temperature"), _
    Description("Allowable Dwell Time Error, in tenths of seconds, between Time Desired and Time Actual at Desired Temp before adjustments are made to Fabric Speed.  Minimum 2 (0.2sec), Maximum 30 (3.0sec).")> _
  Public Parameters_PlevaDwellTimeDeadband As Integer

  <Parameter(0, 1), Category("Pleva Humidity"), _
    Description("Set to '1' to enable exhaust fan speed adjustment based on Pleva humidity input and command parameters.")> _
  Public Parameters_PlevaHumidityEnable As Integer
  <Parameter(0, 1), Category("Pleva Humidity"), _
    Description("Set to '1' so that both Exhaust Fans will be set to the same value during a desired humidity update.  If set to '0' only Exhaust Fan 1 will adjust.")> _
  Public Parameters_PlevaSetBothExhaustFans As Integer
  <Parameter(15, 120), Category("Pleva Humidity"), _
    Description("Time, in seconds, to delay pleva Exhaust Fan Speed adjustment when switching from 'Stopped' or 'Threading' states.")> _
  Public Parameters_PlevaFanSpeedInitializeDelay As Integer
  <Parameter(5, 120), Category("Pleva Humidity"), _
    Description("Time, in seconds, to delay between Exhaust Fan Speed adjustments when enabled and controlling.")> _
  Public Parameters_PlevaFanSpeedAdjustDelay As Integer
  <Parameter(5, 120), Category("Pleva Humidity"), _
    Description("Delay, in seconds, to delay once adjusting exhaust fan speed.  Used for both acceleration and deceleration.")> _
  Public Parameters_PlevaFanSpeedAccelDelay As Integer
  <Parameter(0, 500), Category("Pleva Humidity"), _
    Description("Adjust factor to offset the new exhaust fan speed based on the humidity error between actual & desired. Percent adjustment of error.")> _
  Public Parameters_PlevaFanSpeedAdjustFactor As Integer
  <Parameter(0, 5000), Category("Pleva Humidity"), _
    Description("Maximum Allowed adjustment, in tenths RMP, to offset the current exhaust fan speed when requesting a new exhaust fan speed.")> _
  Public Parameters_PlevaFanSpeedAdjustMax As Integer
  <Parameter(2, 250), Category("Pleva Humidity"), _
    Description("Allowable Humidity Percent Error, in tenths of Percent, between Humidity Desired and Humidity Actual while running before adjustments are made to Exhaust Fan Speed.")> _
  Public Parameters_PlevaHumidityDeadband As Integer


End Class
