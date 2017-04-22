#include <LiquidCrystal.h>
LiquidCrystal lcd(7, 6, 5, 4, 3, 2);

char ch;
int term = 1; //variable for storing string termination
char command = '\0';  //variable for storing command mode
int cursorPos = 0;
int protoStage = 0;
  
void setup()
{
    Serial.begin(9600);   //Sets the baud for serial data transmission                               
    pinMode(LED_BUILTIN, OUTPUT);
    lcd.begin(16, 2);
}

void loop()
{
   if(Serial.available() > 0)      // Send data only when you receive data:
   {
       
      ch = Serial.read();

      switch (ch)
      {
        case 0://null terminator "\0"
        {
          term = 1;
        }
        break;
        case 2://Start of Text
        {
          if (term == 1)
          {
            term = 0;
            protoStage = 1;
            lcd.clear();
          }
        }
        break;
        case 10://new line "\n"
        {
          cursorPos++;
          lcd.setCursor(0,cursorPos);
        }
        break;
        default:
        {
          //Serial.write(ch);
          //lcd.write(ch);
          switch (protoStage)
          {
            case 0:
            {
              
            }
            break;
            case 1:
            {
                command = ch;
                protoStage = 2;
            }
            break;
            case 2:
            {
              if(command == 'M')
              {
                Serial.write(ch);
                lcd.write(ch);
              }
            }
            break;
            default:
            {
              
            }
          }
          
          
        }
        break;
      }
   }
}
