#include <LiquidCrystal.h>
LiquidCrystal lcd(7, 6, 5, 4, 3, 2);

char data = 0;            //Variable for storing received data
  
void setup()
{
    Serial.begin(9600);   //Sets the baud for serial data transmission                               
    pinMode(13, OUTPUT);  //Sets digital pin 13 as output pin

    lcd.begin(16, 2);
    //lcd.print("hello, world!");
}
void loop()
{
   if(Serial.available() > 0)      // Send data only when you receive data:
   {
      lcd.write(Serial.read());
      //lcd.write("-");
   }
}
