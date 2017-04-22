# Protocol Outline

## Format  

start byte | command byte | argument bytes... | end byte  

**Start byte**: 02, 0x02, ASCII STX(Start of Text)  
**Command byte**: single byte for command selection. see below for details.  
**Argument bytes**: (optional, depends on command) contains command data. see below for details.  
**End byte**: 00, 0x00, ASCII NULL  
  
## Commands  

### Message  
    Message data contains any string of chars for general purpos message.
    Command: ASCII "M"
    Argument:
    Message string: array of bytes that represent the message to be displayed

    Example: [02,"M","h","e","l","l","o",00] (display the message "hello")  

### Display power  
    Command: ASCII "D"  

    Arguments:  
    Display off: ASCII "0"  
    Display on: ASCII "1"  

    Example: [02,"D","0",00] (turns the Display off)   

### Clear screen  
    Command: ASCII "C"  
    
    Arguments: NONE  

    Example: [02,"C",00] (clears the screen)