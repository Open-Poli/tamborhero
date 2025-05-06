int A0pin = A0;
int A1pin = A1;
int A4pin = A4;
int A5pin = A5;
 
int redLedPin = 3;
int greenLedPin = 2;
int blueLedPin = 10;
 
int redLedPin2 = 4;
int greenLedPin2 = 5;
int blueLedPin2 = 6;
 
int redLedPin3 = 7;
int greenLedPin3 = 8;
int blueLedPin3 = 9;
 
int redLedPin4 = 12;
int greenLedPin4 = 13;
int blueLedPin4 = 11;

void setup() {
  Serial.begin(9600);
  pinMode(A0pin, INPUT);
  pinMode(A1pin, INPUT);
  pinMode(A4pin, INPUT);
  pinMode(A5pin, INPUT);
 
  pinMode(redLedPin, OUTPUT);
  pinMode(greenLedPin, OUTPUT);
  pinMode(blueLedPin, OUTPUT);
 
  pinMode(redLedPin2, OUTPUT);
  pinMode(greenLedPin2, OUTPUT);
  pinMode(blueLedPin2, OUTPUT);
 
  pinMode(redLedPin3, OUTPUT);
  pinMode(greenLedPin3, OUTPUT);
  pinMode(blueLedPin3, OUTPUT);
 
  pinMode(redLedPin4, OUTPUT);
  pinMode(greenLedPin4, OUTPUT);
  pinMode(blueLedPin4, OUTPUT);
}
 
void loop() {
  int A0read = analogRead(A0pin);
  int A1read = analogRead(A1pin);
  int A4read = analogRead(A4pin);
  int A5read = analogRead(A5pin);
 
  if(A0read > 50 && A1read > 10 && A1read < 550){ //0 y 1
    Serial.write(6);
    
    Serial.flush();

    Serial.println(A0read);
    Serial.println(A1read);
    
    analogWrite(redLedPin3, 255);
    analogWrite(greenLedPin3, 255);
    analogWrite(blueLedPin3, 0);
    analogWrite(redLedPin2, 255);
    analogWrite(greenLedPin2, 255);
    analogWrite(blueLedPin2, 255);

    delay(20);

  }else if(A0read > 50 && A4read > 50){ //0 y 4
    Serial.write(7);
    
    Serial.flush();
    
    Serial.println(A4read);
    Serial.println(A0read);

    analogWrite(redLedPin3, 255);
    analogWrite(greenLedPin3, 255);
    analogWrite(blueLedPin3, 0);
    analogWrite(redLedPin, 255);
    analogWrite(greenLedPin, 255);
    analogWrite(blueLedPin, 255);

    delay(20);

  }else if(A0read > 50 && A5read > 50 && A5read < 500){ //0 y 5
    Serial.write(8);

    Serial.flush();

    Serial.println(A0read);
    Serial.println(A5read);

    analogWrite(redLedPin3, 255);
    analogWrite(greenLedPin3, 255);
    analogWrite(blueLedPin3, 0);
    analogWrite(redLedPin4, 255);

    delay(20);


  }else if(A1read > 50 && A1read < 550 && A4read > 50){ //1 y 4
    Serial.write(9);

    Serial.flush();

    Serial.println(A1read);
    Serial.println(A4read);

    analogWrite(redLedPin2, 255);
    analogWrite(greenLedPin2, 255);
    analogWrite(blueLedPin2, 255);
    analogWrite(redLedPin, 255);
    analogWrite(greenLedPin, 255);
    analogWrite(blueLedPin, 255);
    
    delay(20);

  }else if(A1read > 50 && A1read < 550 && A5read > 50 && A5read < 500){ //1 y 5
    Serial.write(2);
    
    Serial.flush();

    Serial.println(A1read);
    Serial.println(A5read);

    analogWrite(redLedPin2, 255);
    analogWrite(greenLedPin2, 255);
    analogWrite(blueLedPin2, 255);   
    analogWrite(redLedPin4, 255);
    
    delay(20);


  }else if(A4read > 50 && A5read > 50 && A5read < 500){//4 y 5
    Serial.write(3);

    Serial.flush();
    
    Serial.println(A4read);
    Serial.println(A5read);

    analogWrite(redLedPin, 255);
    analogWrite(greenLedPin, 255);
    analogWrite(blueLedPin, 255);
    analogWrite(redLedPin4, 255);
    
    delay(20);


  }else if (A0read > 250) {
    Serial.write(0);
    Serial.flush();
    Serial.println(A0read);
    analogWrite(redLedPin3, 255);
    analogWrite(greenLedPin3, 255);
    analogWrite(blueLedPin3, 0);//izquierda cambiar a verde
    delay(20);

  }
  else if (A1read > 400 && A1read < 550) {
    Serial.write(1);
    Serial.flush();
    Serial.println(A1read);
    analogWrite(redLedPin2, 255);
    analogWrite(greenLedPin2, 255);
    analogWrite(blueLedPin2, 255);
    delay(20);
  }
  else if (A4read > 400) {
    Serial.write(4);
    Serial.flush();
    Serial.println(A4read);
    analogWrite(redLedPin, 255);
    analogWrite(greenLedPin, 255);
    analogWrite(blueLedPin, 255);//centro izquierda cambiar a azul
    delay(20);
  }
  else if (A5read > 300 && A5read < 500) {
    Serial.write(5);
    Serial.flush();
    Serial.println(A5read);
    analogWrite(redLedPin4, 255);
    delay(20);

 
  }
  else
  {
    analogWrite(redLedPin, 0);
    analogWrite(greenLedPin, 0);
    analogWrite(blueLedPin, 0);

    analogWrite(redLedPin2, 0);
    analogWrite(greenLedPin2, 0);
    analogWrite(blueLedPin2, 0);

    analogWrite(redLedPin3, 0);
    analogWrite(greenLedPin3, 0);
    analogWrite(blueLedPin3, 0);

    analogWrite(redLedPin4, 0);
  }
  
 
}