# Protocol Outline

## Format  

start byte | mode byte | payload bytes... | end byte  

**Start byte**: 02, 0x02, ASCII STX(Start of Text)  
**Mode byte**: 48, 0x30, ASCII "0" or 49, 0x31, ASCII "1"  
**Payload bytes**: contains either message or command data. see below for details.  
**End byte**: 00, 0x00, ASCII NULL  
  
## Messages  
Message data contains any string of chars for general purpos message. Mode byte must be "0" for this mode.

Example: [02,"0","h","e","l","l","o",00] (display the message "hello")  
  
## Commands  
Command data contains a specific string of characters for specific commands. Mode byte must be "1" for this mode.  
payload: command byte | command argument bytes(optional)...  
  
  
### Display power  
Command: ASCII "D"  
  
Arguments:  
Display off: ASCII "0"  
Display on: ASCII "1"  

Example: [02,"1","D","0",00] (turns the Display off)   

### Clear screen  
Command: ASCII "C"  
  
Arguments: NONE  

Example: [02,"1","C",00] (clears the screen)