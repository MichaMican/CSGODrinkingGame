﻿using CSGODrinkingGameServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CSGODrinkingGameServer.Manager
{
    public class DrinkManager : IDrinkManager
    {
        private IArduinoSerial _arduino;
        private Thread currentThread;
        private List<double> penaltyQueue;
        public DrinkManager(IArduinoSerial arduino)
        {
            _arduino = arduino;
            penaltyQueue = new List<double>();
            if(currentThread == null || !currentThread.IsAlive)
            {
                currentThread = new Thread(() => queueHandler())
                {
                    //This keeps the Thread running even if registerDrinkPenalty terminates
                    IsBackground = false
                };
                currentThread.Start();
            }
        }

        public void resetQueue()
        {
            penaltyQueue = new List<double>();
        }

        public void registerDrinkPenalty(double duration)
        {
            penaltyQueue.Add(duration);
        }

        private void activatePump()
        {
            while(penaltyQueue.Count > 0)
            {
                var duration = penaltyQueue[0];
                penaltyQueue.RemoveAt(0);

                int durationmms = (int)Math.Round(duration * 1000);
                Console.WriteLine("Starting pump");
                //_arduino.writeToArduino("1");
                Thread.Sleep(durationmms);
                Console.WriteLine("Stopping pump");
                //_arduino.writeToArduino("0");
                Thread.Sleep(50);
            }
            
        }


        private void queueHandler()
        {
            Console.WriteLine("Starting queue Handling thread");
            while (true)
            {
                if(penaltyQueue.Count > 0)
                {
                    activatePump();
                } 
                else
                {
                    Console.WriteLine("Thread HEARTBEAT");
                    Thread.Sleep(2000);
                }
            }
        }

    }
}
