#include <Arduino.h>
#include <WiFi.h>
#include <HTTPClient.h>
#include <WiFiUdp.h>

const char* ssid = "Pixel_2219";
const char* password = "testwifi1234!";

char packetBuffer[512]; //buffer to hold incoming packet

uint8_t  ReplyBuffer[] = "acknowledged";       // a string to send back

const int PORT = 4242; // same as UDP server

uint8_t const HOST[] = "192.168.140.92";

WiFiUDP Udp;
bool status = false;

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
   int ret = Udp.begin(PORT);
   Serial.println(ret);
}
boolean sendUDP(String string) {
  //TODO need update remote ip not board cast up
  int ret = Udp.beginPacket("255.255.255.255", PORT);
  Udp.println(string);
  Udp.endPacket();
  Udp.flush();
  return ret != 0;
}
void loop(){
  if(!status){
    status = sendUDP("teste");
    Serial.println("connected: " + status);
  }
  

}