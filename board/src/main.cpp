#include <Arduino.h>
#include <WiFi.h>
#include <HTTPClient.h>

const char* ssid = "Pixel_2219";
const char* password = "testwifi1234!";
//Your Domain name with URL path or IP address with path
String serverName = "http://192.168.179.92:8080/sender";

void wifiSetup(){

   WiFi.mode(WIFI_STA); //Optional
   WiFi.begin(ssid, password);
   Serial.println("\nConnecting");

   while(WiFi.status() != WL_CONNECTED){
      Serial.print(".");
      delay(100);
   }

   Serial.println("\nConnected to the WiFi network");
   Serial.print("Local ESP32 IP: ");
   Serial.println(WiFi.localIP());
}
void setup(){
   Serial.begin(115200);
   delay(1000);
   wifiSetup();
}
void loop(){
    if(WiFi.status()== WL_CONNECTED){
       HTTPClient http;
       http.begin(serverName.c_str());
       // Send HTTP GET request
      int httpResponseCode = http.GET();
      
      if (httpResponseCode>0) {
        Serial.print("HTTP Response code: ");
        Serial.println(httpResponseCode);
        String payload = http.getString();
        Serial.println(payload);
      }
      else {
        Serial.print("Error code: ");
        Serial.println(httpResponseCode);
      }
      // Free resources
      http.end();
    }
    else{
      Serial.println("WiFi Disconnected");
    }

}