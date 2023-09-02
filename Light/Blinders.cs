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
        PwmController.controller.SetDutyCycle(7, 1.0);
    }

    public static void Off()
    {
        Console.WriteLine("Blinders Off");
        PwmController.controller.SetDutyCycle(7, 0.0);
    }

    public static void OffSoft()
    {
        Console.WriteLine("Blinders Off Soft");

        for (double i = 0.95; i > 0.05; i -= 0.05)
        {
            PwmController.controller.SetDutyCycle(7, i);
            Thread.Sleep(2);
        }
        Off();

    }

    public static void OnOff()
    {
        On();
        Thread.Sleep(250);           
        OffSoft();
       
 
    }
}
