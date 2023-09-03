using Iot.Device.Pwm;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieBaby.Utilities;


public static class PwmController
{
    /// <summary>
    /// Pca9685 PWM Controller
    /// </summary>
    public static Pca9685 controller
    {
        get
        {
            var deviceAddress = Pca9685.I2cAddressBase;
            int busId = 1;
            I2cConnectionSettings settings = new(busId, deviceAddress);
            I2cDevice i2c = I2cDevice.Create(settings);
            return new Pca9685(i2c, pwmFrequency: 50.0);  //50  4095.0};
            //return null;
        }           
    }

}
