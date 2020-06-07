const int PUMP_PIN = 10;
String serData;

void setup() {

    pinMode(LED_BUILTIN, OUTPUT);
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
        digitalWrite(LED_BUILTIN, HIGH);
        digitalWrite(PUMP_PIN, HIGH);
    } else if (serData == "0") {
        //Turn pump off
        digitalWrite(LED_BUILTIN, LOW);
        digitalWrite(PUMP_PIN, LOW);
    }

delay(10);
}