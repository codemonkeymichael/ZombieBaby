using ZombieBaby.Utilities;

namespace ZombieBaby.Movement;
public static class Body
{
    private static decimal up { get; } = 0.05222m;
    private static decimal down { get; } = 0.08991m; 


    public static void Up()
    {
        Console.WriteLine("Sit up fast");
        Motor.motorController.SetDutyCycle(2, decimal.ToDouble(up));
    }

    public static void Down()
    {
        Console.WriteLine("Lay down fast");
        Motor.motorController.SetDutyCycle(2, decimal.ToDouble(down));
    }

    public static void UpSlow()
    {
        Console.WriteLine("Sit up slow");
        decimal stepSize = 0.001m;
        for (decimal i = down; i > up; i = i - stepSize)
        {
            //Console.WriteLine(i);
            Motor.motorController.SetDutyCycle(2, (double)i);
            Thread.Sleep(50);
        }
    }

    public static void DownSlow()
    {
        Console.WriteLine("Lay down slow");
        decimal stepSize = 0.001m;
        for (decimal i = up; i < down; i = i + stepSize)
        {
            //Console.WriteLine(i);
            Motor.motorController.SetDutyCycle(2, (double)i);
            Thread.Sleep(50);
        }
    }

    public static void UpDownSlow()
    {
        Console.WriteLine("Movement UpDownSlow()");
        UpSlow();
        Thread.Sleep(5000);
        DownSlow();
    }



}

////Sit Up Demo
//var busId = 1;

//var deviceAddress = Pca9685.I2cAddressBase;// + selectedI2cAddress;

//I2cConnectionSettings settings = new(busId, deviceAddress);
//I2cDevice i2c = I2cDevice.Create(settings);

//Pca9685 motorController = new(i2c);

//Console.WriteLine($"PCA9685 is ready on I2C bus {i2c.ConnectionSettings.BusId} with address {i2c.ConnectionSettings.DeviceAddress}");
//Console.WriteLine($"PWM Frequency: {motorController.PwmFrequency}Hz");

//using (var pca9685 = new Pca9685(i2c, pwmFrequency: 50))
//{
//    decimal start = 0.05222m; //up
//    decimal end = 0.08991m; //down
//    decimal stepSize = 0.001m;


//Console.WriteLine("up");
//pca9685.SetDutyCycle(2, decimal.ToDouble(start));
//Thread.Sleep(5000);
//Console.WriteLine("down");
//pca9685.SetDutyCycle(2, decimal.ToDouble(end));
//Thread.Sleep(1000);
//Console.WriteLine("mid ish");
//pca9685.SetDutyCycle(2, 0.07);
//Thread.Sleep(1000);
//Console.WriteLine("up up");
//pca9685.SetDutyCycle(2, 0.04);
//Thread.Sleep(1000);



//for (decimal i = start; i < end; i = i + stepSize)
//{
//    Console.WriteLine(i);
//    pca9685.SetDutyCycle(2, (double)i);
//    Thread.Sleep(100);
//}
// }



//    //Time easing

//    decimal stepSize = 0.00025m;
//    Console.WriteLine("Step Size " + stepSize);
//    int numberOfSteps = (int)Math.Round((end - start) / stepSize, MidpointRounding.AwayFromZero);
//    Console.WriteLine("Number Of Steps " + numberOfSteps);
//    int easingSteps = (int)numberOfSteps / 3;
//    Console.WriteLine("Easing Steps " + easingSteps);
//    int fast = 4;
//    int slow = 40;
//    int fastSlowDiff = slow - fast;
//    Console.WriteLine("Fast Slow Diff " + fastSlowDiff);
//    int stepsForEachTime = easingSteps / fastSlowDiff;
//    Console.WriteLine("Steps For Each Time " + stepsForEachTime);

//    int easingStepsCounter = stepsForEachTime;
//    int currentEasingTime = slow;
//    int totalStepsCounter = numberOfSteps;
//    for (decimal i = start; i < end; i = i + stepSize)
//    {
//        //Ease In
//        if (easingStepsCounter < 1 & totalStepsCounter > (numberOfSteps - easingSteps))
//        {
//            Console.WriteLine("Ease In");
//            easingStepsCounter = stepsForEachTime;
//            currentEasingTime--;
//            if (currentEasingTime < fast) currentEasingTime = fast;
//        }
//        //Ease Out
//        if (totalStepsCounter < easingSteps & easingStepsCounter < 1)
//        {
//            Console.WriteLine("Ease Out");
//            easingStepsCounter = stepsForEachTime;
//            currentEasingTime++;
//            if (currentEasingTime > slow) currentEasingTime = slow;

//        }
//        i = Decimal.Round(i, 5);
//        Console.WriteLine(totalStepsCounter + " Loop Move Down Slow to " + (double)i + " speed " + currentEasingTime);
//        pca9685.SetDutyCycle(2, (double)i);
//        Thread.Sleep(currentEasingTime);
//        easingStepsCounter--;
//        totalStepsCounter--;
//    }

//    pca9685.SetDutyCycle(2, 0.0);
//}
