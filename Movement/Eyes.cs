using ZombieBaby.Utilities;

namespace ZombieBaby.Movement;

public static class Eyes
{

    private static decimal open { get; } = 0.05222m;
    private static decimal closed { get; } = 0.08991m;
    public static void Open()
    {
        Console.WriteLine("Eyes Open fast");
        Motor.motorController.SetDutyCycle(0, decimal.ToDouble(open));
    }

    public static void Closed()
    {
        Console.WriteLine("Eyes Closed fast");
        Motor.motorController.SetDutyCycle(0, decimal.ToDouble(closed));
    }

}

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

