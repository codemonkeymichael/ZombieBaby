using ZombieBaby.Light;
using ZombieBaby.Movement;
using ZombieBaby.Utilities;

namespace ZombieBaby;
public static class Program
{

    public static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Zombie Baby is Running Ver 0.5");

        Gpios io = new Gpios(); //Just need to hit the constructor here
        Motor mo = new Motor(); //Just need to hit the constructor here

        //This thread keeps the whole program running and flickering.
        Thread flicker = new Thread(() => Ambient.Flicker());
        flicker.Start();

      

        var inputLastState = PinValue.Low;
        bool up = false;
        while (true)
        {
            var click = Gpios.piGPIOController.Read(Gpios.InputTrigger);
            if (click != inputLastState)
            {
                inputLastState = click;
                if (click == PinValue.High)
                {
                    if (Status.CurrentStatus == 0)
                    {
                        Playlists.Test.All();
                    }
                }

                Thread.Sleep(1000);
            }

            //if (click == PinValue.Low) Console.WriteLine("Low");
            //Console.WriteLine(click);
            //Thread.Sleep(100);
            //}



            //piGPIOController.OpenPin(14, PinMode.Output, PinValue.Low);

            //Eyes.On(piGPIOController);





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



