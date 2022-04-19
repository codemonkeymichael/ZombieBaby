using Iot.Device.Pwm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieBaby.Light;

internal class Eyes
{
    public static void On(GpioController piGPIOController)
    {
        piGPIOController.Write(13, PinValue.High);
        piGPIOController.Write(19, PinValue.High);
    }

    public static void Off(GpioController piGPIOController)
    {
        piGPIOController.Write(13, PinValue.Low);
        piGPIOController.Write(19, PinValue.Low);
    }

    public static void Half(Pca9685 motorController)
    {
        motorController.SetDutyCycle(3, 0.005);
    }
}

