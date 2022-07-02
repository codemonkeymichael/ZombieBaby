using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieBaby.Utilities;

namespace ZombieBaby.Effects;

public static class Smoke
{
    public static void On()
    {
        Console.WriteLine("Smoke On");
        Gpios.piGPIOController.Write(Gpios.Smoke, PinValue.High);
    }

    public static void Off()
    {
        Console.WriteLine("Smoke Off");
        Gpios.piGPIOController.Write(Gpios.Smoke, PinValue.Low);
    }

    public static void OnOff()
    {
        On();
        Thread.Sleep(9000);
        Off();

    }
}
