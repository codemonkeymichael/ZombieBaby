using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Device.Pwm;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using Iot.Device.ServoMotor;
using Iot.Device.Pwm;
using ZombieBaby.Animation;
//using ZombieBaby.Animation;


namespace ZombieBaby;
public static class Program
{
    /// <summary>
    /// Pi GPIO Controller
    /// </summary>
    public static GpioController piGPIOController = new GpioController();

    /// <summary>
    /// PWM 
    /// </summary>
    public static PwmChannel sleepingLights = PwmChannel.Create(0, 0, 400, 0.001);



    /// <summary>
    /// PI-GPIO-19. (13) Foot Lights(PWM Dimmable Channel 1)
    /// </summary>
    public static PwmChannel awakeLights = PwmChannel.Create(0, 1, 400, 0.001);

    public static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Zombie Baby is Running 0");

        piGPIOController.OpenPin(14, PinMode.Output, PinValue.Low);

        Eyes.On(piGPIOController);
        Thread.Sleep(1000);


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
        //    pca9685.SetDutyCycle(0, 0.04); //Open

        //    Thread.Sleep(500);
        //    Console.WriteLine("Blink Eyes Closed");
        //    pca9685.SetDutyCycle(0, 0.07); //Closed

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


        //    pca9685.SetDutyCycle(0, 0.0);
        //    pca9685.SetDutyCycle(1, 0.0);
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



        //Sit Up Demo
        var busId = 1;

        var deviceAddress = Pca9685.I2cAddressBase;// + selectedI2cAddress;

        I2cConnectionSettings settings = new(busId, deviceAddress);
        I2cDevice i2c = I2cDevice.Create(settings);

        Pca9685 motorController = new(i2c);

        Console.WriteLine($"PCA9685 is ready on I2C bus {i2c.ConnectionSettings.BusId} with address {i2c.ConnectionSettings.DeviceAddress}");
        Console.WriteLine($"PWM Frequency: {motorController.PwmFrequency}Hz");

        using (var pca9685 = new Pca9685(i2c, pwmFrequency: 50))
        {
            decimal start = 0.05222m; //up
            decimal end = 0.08991m; //down          

            Console.WriteLine("Lay Down");
            pca9685.SetDutyCycle(2, (double)end);

            Thread.Sleep(2500);

            Console.WriteLine("Sit up");
            pca9685.SetDutyCycle(2, (double)start);

            Thread.Sleep(2500);


            //Time easing

            decimal stepSize = 0.00025m;
            Console.WriteLine("Step Size " + stepSize);
            int numberOfSteps = (int)Math.Round((end - start) / stepSize, MidpointRounding.AwayFromZero);
            Console.WriteLine("Number Of Steps " + numberOfSteps);
            int easingSteps = (int)numberOfSteps / 3;
            Console.WriteLine("Easing Steps " + easingSteps);
            int fast = 4;
            int slow = 40;
            int fastSlowDiff = slow - fast;
            Console.WriteLine("Fast Slow Diff " + fastSlowDiff);
            int stepsForEachTime = easingSteps / fastSlowDiff;
            Console.WriteLine("Steps For Each Time " + stepsForEachTime);

            int easingStepsCounter = stepsForEachTime;
            int currentEasingTime = slow;
            int totalStepsCounter = numberOfSteps;
            for (decimal i = start; i < end; i = i + stepSize)
            {
                //Ease In
                if (easingStepsCounter < 1 & totalStepsCounter > (numberOfSteps - easingSteps))
                {
                    Console.WriteLine("Ease In");
                    easingStepsCounter = stepsForEachTime;
                    currentEasingTime--;
                    if (currentEasingTime < fast) currentEasingTime = fast;
                }
                //Ease Out
                if (totalStepsCounter < easingSteps & easingStepsCounter < 1)
                {
                    Console.WriteLine("Ease Out");
                    easingStepsCounter = stepsForEachTime;
                    currentEasingTime++;
                    if (currentEasingTime > slow) currentEasingTime = slow;

                }
                i = Decimal.Round(i, 5);
                Console.WriteLine(totalStepsCounter + " Loop Move Down Slow to " + (double)i + " speed " + currentEasingTime);
                pca9685.SetDutyCycle(2, (double)i);
                Thread.Sleep(currentEasingTime);
                easingStepsCounter--;
                totalStepsCounter--;
            }

            pca9685.SetDutyCycle(2, 0.0);
        }

        Thread.Sleep(1000);
        Eyes.Off(piGPIOController);
    }


    //static void CurrentDomain_ProcessExit(object sender, EventArgs e)
    //{
    //    //TODO: Make this work
    //    //Close a pins and stop playing music
    //    Console.WriteLine("I quit!");
    //}
}



