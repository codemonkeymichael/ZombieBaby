using Iot.Device.Pwm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieBaby.Movement
{
    internal class Eyes
    {
        //private static double closed = 0.030029296875 + 0.001; //+ 0.001
        //private static double open = 0.12939453125 - 0.001; // - 0.001 
        //private static double open = 0.12939453125 - 0.03; // - 0.001
        //private static double closed = 0.14;
        private static double closed = 0.121;
        //private static double open = 0.59;


        public static void On(GpioController piGPIOController)
        {
            piGPIOController.Write(14, PinValue.High);
        }

        public static void Off(GpioController piGPIOController)
        {
            piGPIOController.Write(14, PinValue.Low);
        }


        public static void Blink(Pca9685 motorController, int times)
        {

        }
    }
}
