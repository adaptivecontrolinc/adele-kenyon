Partial Class ControlCode

  Public ReadOnly LS As New LS(Me)
  Public ReadOnly OP As New OP(Me)


  Public ReadOnly WS As New WS(Me)
  Public ReadOnly WD As New WD(Me)

  Public ReadOnly FS As New FS(Me)
  Public ReadOnly FD As New FD(Me)
  Public ReadOnly ES As New ES(Me)

  Public ReadOnly AT As New AT(Me)

  Public ReadOnly PR As New PR(Me)
  Public ReadOnly TC As New TC(Me)
  Public ReadOnly TLR As New TLR(Me)
  Public ReadOnly OV As New OV(Me)
  Public ReadOnly SV As New SV(Me)
  Public ReadOnly CV As New CV(Me)
  Public ReadOnly ST As New ST(Me)





  'Dancer Pressure - "Setpoint: |0-999|psi Deviance: |0-999|psi"
  '  Public ReadOnly DP As New DP(Me)

  'Pleva Humidity - "Setpoint: |0-999|%"
  'Set the desired Exhaust Humidity recorded by Pleva Humidity Sensor.  Used to control the Exhaust #1 Fan Speed within specified limits.  Set to '0' to disable control.
  '  Public ReadOnly PH As New PH(Me)

  'Pleva Temperature - "Temp:|0-450|F Time:|0-30|sec", "", "", "", CommandType.BatchParameter), _
  'Designed to adjust the fabric speed to reach a desired fabric temp for a desired time. Used to control the Tenter Chain Speed within specified limits.  Set to '0' to disable control.
  '  Public ReadOnly PT As New PT(Me)

End Class
