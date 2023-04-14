#define STRIP_PIN 13     // пин ленты
#define NUMLEDS 110      // кол-во светодиодов
#include <microLED.h>
microLED<NUMLEDS, STRIP_PIN, MLED_NO_CLOCK, LED_WS2818, ORDER_GRB, CLI_AVER> strip;
String str1;
void setup() {
Serial.begin(9600);
strip.setBrightness(100);
}

void loop(){
  if(Serial.available() > 0 ){
    str1 = Serial.readString();
    Serial.print(str1);
    if(str1 != 0 ){
      if(str1 == "off"){
        off();
        strip.show();
      }

      if(str1 == "Rainbow"){
        Serial.print("Good");
        rainbow(255);
        strip.show();
        delay(30);
      }

      if(1 == str1.substring(0,1).toInt()){
      int colorK = str1.substring(1,5).toInt();
      int brK = str1.substring(5,8).toInt();
      setKelvin(colorK, brK);
      strip.show();
      }
    }
  }
}  


void setKelvin(int k, int br){
  strip.setBrightness(br);
  for(int i = 0; i < NUMLEDS; i++){
    strip.set(i, mKelvin(k));
  }
}

void rainbow(int br){
    strip.setBrightness(br);
  static byte counter = 0;
  for (int i = 0; i < NUMLEDS; i++) {
    strip.set(i, mWheel8(counter + i * 255 / NUMLEDS));   // counter смещает цвет
  }
  counter += 3;   // counter имеет тип byte и при достижении 255 сбросится в 0
}

void off(){
  strip.setBrightness(0);
}