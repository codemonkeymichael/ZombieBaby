using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieBaby.Utilities;

namespace ZombieBaby.Light;

public static class Blinders
{

    public static void On()
    {
        Console.WriteLine("Blinders On");
        Gpios.piGPIOController.Write(Gpios.Blinders, PinValue.High);
    }

    public static void Off()
    {
        Console.WriteLine("Blinders Off");
        Gpios.piGPIOController.Write(Gpios.Blinders, PinValue.Low);
    }

    public static void OnOff()
    {
        On();
        Thread.Sleep(1000);
        //Flicker off          
        Off();
        Thread.Sleep(45);
        On();
        Thread.Sleep(20);
        Off();
 
    }
}
