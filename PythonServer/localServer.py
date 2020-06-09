#!/usr/bin/env python3
# -*- coding: utf-8 -*-
from flask import Flask, request, render_template
from flask_cors import CORS
import json
import threading
import serial

port = 'COM17'
ser = serial.Serial(port, 9600)

# This is important so the write isn't called before the Arduino is ready to receive
x = ser.readline().decode('utf-8')
print(x)

app = Flask(__name__)
CORS(app)

e = threading.Event()

@app.route("/", methods=['POST'])
def write_data():
    try:
        e.set()
        data = request.get_json()
        print(data)
    except:
        return "Bad request", 400 
    return "OK", 200


@app.route("/")
def index():
    return "Method Not Allowed", 405

@app.route("/reset")
def reset():
    deactivatePump()
    return "OK", 200

def startServer():
    app.run(host='127.0.0.1', port=30000, debug=False)

def wait_for_event():
    print('wait_for_event starting')
    event_is_set = e.wait()
    print('event set: %s', event_is_set)
    activatePump()

def sendMessageToArduino(msg):
    ser.write(msg.encode())

def activatePump():
    sendMessageToArduino("1")

def deactivatePump():
    sendMessageToArduino("0")

if __name__ == '__main__':
    t1 = threading.Thread(name='waiter', 
                      target=wait_for_event,
                      args=())
    t1.start()

    t2 = threading.Thread(name='server', 
                      target=startServer,
                      args=())
    t2.start()

    print("Everything started nominal")