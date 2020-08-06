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
        private List<double> penaltyQueue
        public DrinkManager(IArduinoSerial arduino)
        {
            _arduino = arduino;
        }

        public void registerDrinkPenalty(double duration)
        {
            if (currentThread == null || !currentThread.IsAlive)
            {
                int durationms = (int)Math.Round(duration * 1000);
                currentThread = new Thread(() => activatePump(durationms)) { 
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

        private void activatePump(int duration)
        {
            _arduino.writeToArduino("1");
            Thread.Sleep(duration);
            _arduino.writeToArduino("0");
        }
    }
}
