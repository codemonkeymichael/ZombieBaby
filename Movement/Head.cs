using ZombieBaby.Utilities;

namespace ZombieBaby.Movement;

public static class Head
{

    private static decimal center { get; } = 0.097m;
    private static decimal right { get; } = 0.12m;
    private static decimal left { get; } = 0.07m;
    public static void Center()
    {
        Console.WriteLine("Center head fast");
        Motor.motorController.SetDutyCycle(1, decimal.ToDouble(center));
    }

    public static void Right()
    {
        Console.WriteLine("Right head fast");
        Motor.motorController.SetDutyCycle(1, decimal.ToDouble(right));
    }

    public static void Left()
    {
        Console.WriteLine("Left head fast");
        Motor.motorController.SetDutyCycle(1, decimal.ToDouble(left));
    }

}

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
//    //pca9685.SetDutyCycle(1, 0.13);  //this was to far

//    Thread.Sleep(1550);

//    Console.WriteLine("Left 0.07");
//    pca9685.SetDutyCycle(1, 0.07);
//    //pca9685.SetDutyCycle(1, 0.05);  //this was to far

//    Thread.Sleep(1550);

//    Console.WriteLine("Center 0.097");
//    pca9685.SetDutyCycle(1, 0.097);

//    Thread.Sleep(1550);

//    pca9685.SetDutyCycle(1, 0.0);
//}

//}

