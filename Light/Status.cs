using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieBaby.Utilities;

namespace ZombieBaby.Light
{
    public static class Status
    {
        public static void On()
        {
            Console.WriteLine("Status On");
            Gpios.piGPIOController.Write(Gpios.Status, PinValue.High);
        }

        public static void Off()
        {
            Console.WriteLine("Status Off");
            Gpios.piGPIOController.Write(Gpios.Status, PinValue.Low);
        }
    }
}
