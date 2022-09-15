using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieBaby.Utilities;

namespace ZombieBaby.Effects;

public class Fan
{
    public void On()
    {
        Console.WriteLine("Fan On");
        Gpios.piGPIOController.Write(Gpios.Fan, PinValue.High);
    }
    public void Off()
    {
        Console.WriteLine("Fan Off");
        Gpios.piGPIOController.Write(Gpios.Fan, PinValue.Low);
    }

    public void OnOffQuick()
    {
        On();
        Thread.Sleep(2500);
        Off();
    }

    public void OnOffLong()
    {
        On();
        Thread.Sleep(15000);
        Off();
    }
}
