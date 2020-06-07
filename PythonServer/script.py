import serial
import numpy as np
import time

port = 'COM17'

ser = serial.Serial(port, 9600)

# This is important so the write isn't called before the Arduino is ready to receive
x = ser.readline().decode('utf-8')
print(x)


def sendMessageToArduino(msg):
    ser.write(msg.encode())

def activatePump():
    sendMessageToArduino("1")

def deactivatePump():
    sendMessageToArduino("0")

while True:
    activatePump()
    time.sleep(5)
    deactivatePump()
    time.sleep(5)
