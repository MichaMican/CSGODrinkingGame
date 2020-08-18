using CSGODrinkingGameServer.Exceptions;
using CSGODrinkingGameServer.Interfaces;
using CSGODrinkingGameServer.Models;
using CSGODrinkingGameServer.Models.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;

namespace CSGODrinkingGameServer.IO
{
    public class ArduinoSerial : IArduinoSerial
    {
        private readonly Settings _settings;
        private SerialPort _serialPort;
        public ArduinoSerial(IOptions<Settings> options)
        {
            _settings = options.Value;
            _serialPort = new SerialPort();
            _serialPort.PortName = _settings.ArduinoPort;
            _serialPort.BaudRate = 9600;
            //_serialPort.Open();
        }

        public void writeToArduino(string value)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Write(value);
            } 
            else
            {
                throw new PortClosedException();
            }
        }
    }
}
