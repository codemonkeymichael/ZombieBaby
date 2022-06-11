﻿using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Device.Pwm;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using Iot.Device.ServoMotor;
using Iot.Device.Pwm;
using ZombieBaby.Animation;
using ZombieBaby.Effects;
using ZombieBaby.Utilities;
using ZombieBaby.Light;



namespace ZombieBaby;
public static class Program
{





    public static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Zombie Baby is Running 0");

        

        var deviceAddress = Pca9685.I2cAddressBase;// + selectedI2cAddress;
        var busId = 1;
        I2cConnectionSettings settings = new(busId, deviceAddress);
        I2cDevice i2c = I2cDevice.Create(settings);
        Pca9685 motorController = new Pca9685(i2c, pwmFrequency: 50);

        GpioController controller = new GpioController();
        Gpios io = new Gpios(controller);

 
        //Body.Up(motorController);
        //Thread.Sleep(2000);
        //Body.Down(motorController);


        //Console.WriteLine($"PCA9685 is ready on I2C bus {i2c.ConnectionSettings.BusId} with address {i2c.ConnectionSettings.DeviceAddress}");
        //Console.WriteLine($"PWM Frequency: {motorController.PwmFrequency}Hz");

        //using (var pca9685 = new Pca9685(i2c, pwmFrequency: 50))




        var inputLastState = PinValue.Low;
        bool up = false;
        while (true)
        {
            var click = controller.Read(Gpios.InputTrigger);
            if(click != inputLastState)
            {
                inputLastState = click;
                if (click == PinValue.High)
                {
                    if (up)
                    {
                        Body.Down(motorController);
                        Blinders.On();
                        Status.On();
                        Light.Eyes.On();
                        Head.On();
                        Foot.On();
                        Fan.On();
                        Smoke.On();
                        up = false;
                    }
                    else
                    {                       
                        Body.Up(motorController);
                        Blinders.Off();
                        Status.Off();
                        Light.Eyes.Off();
                        Head.Off();
                        Foot.Off();
                        Fan.Off();
                        Smoke.Off();
                        up = true;
                    }
                }
               
                Thread.Sleep(1000);
            }
            
            //if (click == PinValue.Low) Console.WriteLine("Low");
            //Console.WriteLine(click);
            Thread.Sleep(100);
        }



        //piGPIOController.OpenPin(14, PinMode.Output, PinValue.Low);

        //Eyes.On(piGPIOController);



        ////Flicking Light
        //sleepingLights.Start();
        //Random rnd = new Random();
        //double cur = 0.5;
        //bool dim = true;

        //while (true)
        //{
        //    double flicker = (double)rnd.NextInt64(4, 9) / (double)10;
        //    double dimSpeed = (double)rnd.NextInt64(1, 4) / (double)1000;
        //    int holdDuration = (int)rnd.NextInt64(25, 150);
        //    Console.WriteLine(flicker + " " + dimSpeed + " " + holdDuration);

        //    if (flicker > cur)
        //    {
        //        for (double d = cur; d < flicker; d = d + dimSpeed)
        //        {
        //            sleepingLights.DutyCycle = d;
        //            Thread.Sleep(1);
        //            cur = d;
        //        }
        //    }
        //    else
        //    {
        //        for (double d = cur; d > flicker; d = d - dimSpeed)
        //        {
        //            sleepingLights.DutyCycle = d;
        //            Thread.Sleep(1);
        //            cur = d;
        //        }

        //    }
        //    Thread.Sleep(holdDuration);
        //}


        ////Eyes Blink
        //var busId = 1;

        //var deviceAddress = Pca9685.I2cAddressBase;// + selectedI2cAddress;

        //I2cConnectionSettings settings = new(busId, deviceAddress);
        //I2cDevice i2c = I2cDevice.Create(settings);

        //Pca9685 motorController = new(i2c);

        //Console.WriteLine($"PCA9685 is ready on I2C bus {i2c.ConnectionSettings.BusId} with address {i2c.ConnectionSettings.DeviceAddress}");
        //Console.WriteLine($"PWM Frequency: {motorController.PwmFrequency}Hz");

        //using (var pca9685 = new Pca9685(i2c, pwmFrequency: 50))
        //{


        //    Console.WriteLine("Blink Eyes Open");
        //    pca9685.SetDutyCycle(0, 0.032); //Open

        //    Thread.Sleep(3500);

        //    Console.WriteLine("Blink Eyes Closed");
        //    pca9685.SetDutyCycle(0, 0.06); //Closed

        //    Thread.Sleep(1500);

        //    pca9685.SetDutyCycle(0, 0.0);

        //    //for (double i = 0.06; i > 0.04; i = i - 0.001)
        //    //{
        //    //    Console.WriteLine("Blink O/C " + i);
        //    //    pca9685.SetDutyCycle(0, i);
        //    //    Thread.Sleep(50);
        //    //}
        //    //Thread.Sleep(2000);
        //    //for (double i = 0.045; i < 0.08; i = i + 0.001)
        //    //{
        //    //    Console.WriteLine("Look Right to Center " + i);
        //    //    pca9685.SetDutyCycle(0, i);
        //    //    Thread.Sleep(50);
        //    //}
        //    //Thread.Sleep(2000);


        //    //pca9685.SetDutyCycle(0, 0.0);
        //    //pca9685.SetDutyCycle(1, 0.0);
        //}


        ////Head Turn Demo
        //var busId = 1;

        //var deviceAddress = Pca9685.I2cAddressBase;// + selectedI2cAddress;

        //I2cConnectionSettings settings = new(busId, deviceAddress);
        //I2cDevice i2c = I2cDevice.Create(settings);

        //Pca9685 motorController = new(i2c);

        //Console.WriteLine($"PCA9685 is ready on I2C bus {i2c.ConnectionSettings.BusId} with address {i2c.ConnectionSettings.DeviceAddress}");
        //Console.WriteLine($"PWM Frequency: {motorController.PwmFrequency}Hz");

        //using (var pca9685 = new Pca9685(i2c, pwmFrequency: 50))
        //{
        //    Console.WriteLine("Center 0.097");
        //    pca9685.SetDutyCycle(1, 0.097);

        //    Thread.Sleep(1550);

        //    Console.WriteLine("Right 0.12");
        //    pca9685.SetDutyCycle(1, 0.12);
        //    //pca9685.SetDutyCycle(1, 0.13);  //this works to far

        //    Thread.Sleep(1550);

        //    Console.WriteLine("Left 0.07");
        //    pca9685.SetDutyCycle(1, 0.07);
        //    //pca9685.SetDutyCycle(1, 0.05);  //this work to far

        //    Thread.Sleep(1550);

        //    Console.WriteLine("Center 0.097");
        //    pca9685.SetDutyCycle(1, 0.097);

        //    Thread.Sleep(1550);

        //    pca9685.SetDutyCycle(1, 0.0);
        //}

    }


    //static void CurrentDomain_ProcessExit(object sender, EventArgs e)
    //{
    //    //TODO: Make this work
    //    //Close a pins and stop playing music
    //    Console.WriteLine("I quit!");
    //}
}



