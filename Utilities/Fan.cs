using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieBaby.Utilities
{
    public class Fan
    {


        public static void On(GpioController piGPIOController)
        {
            piGPIOController.Write(Gpios.Fan, PinValue.High);
        }

        public static void Off(GpioController piGPIOController)
        {
            piGPIOController.Write(Gpios.Fan, PinValue.Low);
        }
    }
}
