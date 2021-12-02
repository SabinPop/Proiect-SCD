package com.example.location_mobile_app

import java.time.LocalDateTime
import java.time.LocalDateTime.now
import java.time.format.DateTimeFormatter
import java.util.*

class Location constructor(Latitude: Double, Longitude: Double, TerminalId: String) {
    var id: Int = 0
    var latitude: Double = Latitude
    var longitude: Double = Longitude
    var terminalId: String = TerminalId
    var dateTime: String? = now().format(DateTimeFormatter.ISO_LOCAL_DATE_TIME)

}