using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Device.Pwm;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using Iot.Device.ServoMotor;
using Iot.Device.Pwm;
//using ZombieBaby.Animation;


namespace ZombieBaby;
public static class Program
{


    public static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Zombie Baby is Running 3");

        var busId = 1;
        var selectedI2cAddress = 0b000000; // A5 A4 A3 A2 A1 A0
        var deviceAddress = Pca9685.I2cAddressBase + selectedI2cAddress;

        I2cConnectionSettings settings = new(busId, deviceAddress);
        using I2cDevice i2c = I2cDevice.Create(settings);

        using Pca9685 motorController = new(i2c);
        //Console.WriteLine("Set the PWM freq at 50hz for the SG90 servo");
        //motorController.PwmFrequency = 50;

        Console.WriteLine($"PCA9685 is ready on I2C bus {i2c.ConnectionSettings.BusId} with address {i2c.ConnectionSettings.DeviceAddress}");
        Console.WriteLine($"PWM Frequency: {motorController.PwmFrequency}Hz");

        //GpioController piGPIOController = new GpioController();
        //piGPIOController.OpenPin(19, PinMode.Output, PinValue.Low);
        //piGPIOController.OpenPin(13, PinMode.Output, PinValue.Low);

        //PwmChannel eyes = motorController.CreatePwmChannel(0); // channel 0
        PwmChannel head = motorController.CreatePwmChannel(1); // channel 1
        head.Start();
        head.DutyCycle = 0.5;
        Thread.Sleep(2000);
        head.Stop();
  



       //Animation.Body.Head(motorController);

        //Animation.Eyes.Blink(motorController, 2);
        //Light.Eyes.On(piGPIOController);
        //Thread.Sleep(2000);
        //Light.Eyes.Off(piGPIOController);
        //Light.Eyes.Half(motorController);
        //Thread.Sleep(500);
        //Animation.Body.SitUp(motorController);


        i2c.Dispose();
        motorController.Dispose();

    }

 
    //static void CurrentDomain_ProcessExit(object sender, EventArgs e)
    //{
    //    //TODO: Make this work
    //    //Close a pins and stop playing music
    //    Console.WriteLine("I quit!");
    //}
}



