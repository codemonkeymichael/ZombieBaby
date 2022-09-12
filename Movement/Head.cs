using ZombieBaby.Utilities;

namespace ZombieBaby.Movement;

public class Head
{

    private static double center { get; } = 0.1;
    private static double right { get; } = 0.113;
    private static double rightHalf { get; } = 0.105;
    private static double left { get; } = 0.08; //0.01
    private static double leftHalf { get; } = 0.09; //0.05
    public void Center()
    {
        Console.WriteLine("Center head fast " + center);
        Motor.motorController.SetDutyCycle(1, center);
    }

    public void Right()
    {
        Console.WriteLine("Head Right fast " + right);
        Motor.motorController.SetDutyCycle(1, right); 
    }

    public void RightHalf()
    {
        Console.WriteLine("Head Right Half fast " + rightHalf);
        Motor.motorController.SetDutyCycle(1, rightHalf);
    }

    public void Left()
    {
        Console.WriteLine("Head Left fast " + left);
        Motor.motorController.SetDutyCycle(1, left); 
    }

    public void LeftHalf()
    {
        Console.WriteLine("Head Left Half fast " + leftHalf);
        Motor.motorController.SetDutyCycle(1, leftHalf);
    }

    public void Release()
    {
        Console.WriteLine("Head Release");
        Motor.motorController.SetDutyCycle(1, 0);
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

