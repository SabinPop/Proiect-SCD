package com.example.location_mobile_app

import android.Manifest
import android.annotation.SuppressLint
import android.content.pm.PackageManager
import android.os.Bundle
import android.os.Looper
import android.provider.Settings
import android.widget.Button
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.core.app.ActivityCompat
import com.google.android.gms.location.*
import com.google.gson.Gson
import okhttp3.MediaType.Companion.toMediaType
import okhttp3.OkHttpClient
import okhttp3.Request
import okhttp3.RequestBody.Companion.toRequestBody
import java.io.IOException

class MainActivity : AppCompatActivity() {

    private lateinit var fusedLocationClient: FusedLocationProviderClient
    private lateinit var locationRequest: LocationRequest
    private lateinit var locationCallback: LocationCallback

    private lateinit var terminalId: String
    private lateinit var locationObject: Location

    private val client = OkHttpClient()

    @SuppressLint("HardwareIds")
    override fun onCreate(savedInstanceState: Bundle?) {

        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        title = "Location Mobile App"

        //get reference to view components
        val button  = findViewById<Button>(R.id.button)
        val latitudeText = findViewById<TextView>(R.id.latitude_textview)
        val longitudeText = findViewById<TextView>(R.id.longitude_textview)
        val deviceIdText = findViewById<TextView>(R.id.deviceId_textview)
        val deviceId: String = Settings.Secure.getString(contentResolver, Settings.Secure.ANDROID_ID)
        deviceIdText.text = deviceId
        terminalId = deviceId
        //set onClick event for the button
        setButtonListener(button)

        getLocationUpdates(latitudeText, longitudeText)

    }

    private fun sendPostRequest(location: Location) {
        var isOnSuccess = false
        val url = "https://scd-location-api.azurewebsites.net/api/Location/create"
        val gson = Gson()
        val json = gson.toJson(location)
        println(json)

        val request = Request.Builder().url(url)
            .post(json.toRequestBody("application/json".toMediaType()))
            .build()
        try {
            client.newCall(request).execute().use { response ->
                    if (!response.isSuccessful) {
                        isOnSuccess = false
                    }
                    isOnSuccess = true
                }
        }
        catch (ex: IOException){
            ex.printStackTrace()
        }
        finally {
            this@MainActivity.runOnUiThread {
                Toast.makeText(
                    this@MainActivity,
                    if (isOnSuccess) "Location sent to server" else "Server unreachable",
                    Toast.LENGTH_SHORT
                )
                    .show()
            }
        }
    }

    private fun setButtonListener(button: Button){
        button.setOnClickListener{
            Thread {
                Looper.prepare()
                sendPostRequest(locationObject)
            }.start()
        }
    }

    private fun getLocationUpdates(latitude: TextView, longitude: TextView)
    {
        fusedLocationClient = LocationServices.getFusedLocationProviderClient(this@MainActivity)
        locationRequest = LocationRequest()
        locationRequest.interval = 10000
        locationRequest.fastestInterval = 10000
        locationRequest.smallestDisplacement = 10f // 10 m
        locationRequest.priority = LocationRequest.PRIORITY_HIGH_ACCURACY
        locationCallback = object : LocationCallback() {
            override fun onLocationResult(locationResult: LocationResult?) {
                locationResult ?: return

                if (locationResult.locations.isNotEmpty()) {
                    // get latest location
                    val location =
                        locationResult.lastLocation
                    // use location object
                    // get latitude , longitude
                    latitude.text = location.latitude.toString()
                    longitude.text = location.longitude.toString()
                    locationObject = Location(location.latitude, location.longitude, terminalId)
                }
            }
        }
    }

    //start location updates
    private fun startLocationUpdates() {
        if (ActivityCompat.checkSelfPermission(
                this,
                Manifest.permission.ACCESS_FINE_LOCATION
            ) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(
                this,
                Manifest.permission.ACCESS_COARSE_LOCATION
            ) != PackageManager.PERMISSION_GRANTED
        ) {
            // TODO: Consider calling
            //    ActivityCompat#requestPermissions
            ActivityCompat.requestPermissions(this@MainActivity,
                arrayOf(Manifest.permission.ACCESS_FINE_LOCATION), 1)
            // here to request the missing permissions, and then overriding
            //   public void onRequestPermissionsResult(int requestCode, String[] permissions,
            //                                          int[] grantResults)
            // to handle the case where the user grants the permission. See the documentation
            // for ActivityCompat#requestPermissions for more details.
            return
        }
        fusedLocationClient.requestLocationUpdates(
            locationRequest,
            locationCallback,
            null /* Looper */
        )
    }

    // stop location updates
    private fun stopLocationUpdates() {
        fusedLocationClient.removeLocationUpdates(locationCallback)
    }

    // stop receiving location update when activity not visible/foreground
    override fun onPause() {
        super.onPause()
        stopLocationUpdates()
    }

    // start receiving location update when activity  visible/foreground
    override fun onResume() {
        super.onResume()
        startLocationUpdates()
    }

}