using CSGODrinkingGameServer.Interfaces;
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
        }

        public void resetQueue()
        {
            penaltyQueue = new List<double>();
        }

        public void registerDrinkPenalty(double duration)
        {
            if (currentThread == null || !currentThread.IsAlive)
            {
                penaltyQueue.Add(duration);
                currentThread = new Thread(() => activatePump()) { 
                    //This keeps the Thread running even if registerDrinkPenalty terminates
                    IsBackground = false 
                };
                currentThread.Start();
            }
            else
            {
                //If a penalty is still running the penalty gets saved
                penaltyQueue.Add(duration);
            }
        }

        private void activatePump()
        {
            while(penaltyQueue.Count > 0)
            {
                var duration = penaltyQueue[0];
                penaltyQueue.RemoveAt(0);

                int durationmms = (int)Math.Round(duration * 1000);
                _arduino.writeToArduino("1");
                Thread.Sleep(durationmms);
                _arduino.writeToArduino("0");
                Thread.Sleep(50);
            }
            
        }

        
    }
}
