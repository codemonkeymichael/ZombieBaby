using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieBaby.Utilities;

namespace ZombieBaby.Effects
{
    public static class Fan
    {
        public static void On()
        {
            Console.WriteLine("Fan On");
            Gpios.piGPIOController.Write(Gpios.Fan, PinValue.High);
        }
        public static void Off()
        {
            Console.WriteLine("Fan Off");
            Gpios.piGPIOController.Write(Gpios.Fan, PinValue.Low);
        }
    }
}
