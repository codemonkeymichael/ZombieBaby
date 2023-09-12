﻿using Iot.Device.Pwm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieBaby.Utilities;

namespace ZombieBaby.Light;

public class Eyes
{
    public void On()
    {
        Console.WriteLine("Eyes Light On");
        Gpios.piGPIOController.Write(Gpios.Eyes, PinValue.High);
    }

    public void Off()
    {
        Console.WriteLine("Eyes Light Off");
        Gpios.piGPIOController.Write(Gpios.Eyes, PinValue.Low);     
    }


}

