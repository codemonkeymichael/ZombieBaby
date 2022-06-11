﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieBaby.Light
{
    public static class Blinders
    {

        public static void On(GpioController piGPIOController)
        {
            Console.WriteLine("Blinders On");
            piGPIOController.Write(Gpios.Blinders, PinValue.High);
        }

        public static void Off(GpioController piGPIOController)
        {
            Console.WriteLine("Blinders Off");
            piGPIOController.Write(Gpios.Blinders, PinValue.Low);
        }
    }
}
