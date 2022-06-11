using Iot.Device.Pwm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieBaby.Utilities;

namespace ZombieBaby.Light;

public class Eyes
{
    public static void On()
    {
        Console.WriteLine("Eyes On");
        Gpios.piGPIOController.Write(Gpios.Eyes, PinValue.High);
        //Gpios.piGPIOController.Write(19, PinValue.High);
    }

    public static void Off()
    {
        Console.WriteLine("Eyes Off");
        Gpios.piGPIOController.Write(Gpios.Eyes, PinValue.Low);
        //Gpios.piGPIOController.Write(19, PinValue.Low);
    }


}

