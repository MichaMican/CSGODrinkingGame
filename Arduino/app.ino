const int PUMP_PIN = 13;
String serData;

void setup() {

    pinMode(PUMP_PIN, OUTPUT);
    Serial.begin(9600);
    // This signals the PC that the Arduino is ready to receive data
    Serial.println("Arduino is ready!");
}

void loop() {

    //clear String before next input
    serData = "";

    while(Serial.available() > 0){
        char rec = Serial.read();
        serData += rec;
    }

    if(serData == "1"){
        //Turn pump on
        digitalWrite(PUMP_PIN, HIGH);
    } else if (serData == "0") {
        //Turn pump off
        digitalWrite(PUMP_PIN, LOW);
    }

delay(10);
}