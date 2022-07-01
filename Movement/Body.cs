using ZombieBaby.Utilities;

namespace ZombieBaby.Movement;
public static class Body
{
    private static decimal up { get; } = 0.043m;
    private static decimal down { get; } = 0.079m;
    private static decimal stepSize { get; } = 0.001m;


    public static void UpFast()
    {
        Console.WriteLine("Body Sit up fast");
        Motor.motorController.SetDutyCycle(2, decimal.ToDouble(up));
    }

    public static void DownFast()
    {
        Console.WriteLine("Body Lay down fast");
        Motor.motorController.SetDutyCycle(2, decimal.ToDouble(down));
    }

    public static void UpMed()
    {
        Console.WriteLine("Body Sit up medium");   
        for (decimal i = down; i > up; i = i - stepSize)
        {
            Motor.motorController.SetDutyCycle(2, (double)i);
            Thread.Sleep(7);
        }
    }

    public static void DownMed()
    {
        Console.WriteLine("Body Lay down slow");
        for (decimal i = up; i < down; i = i + stepSize)
        {
            Motor.motorController.SetDutyCycle(2, (double)i);
            Thread.Sleep(7);
        }
    }

    public static void UpSlow()
    {
        Console.WriteLine("Body Sit up slow");
        for (decimal i = down; i > up; i = i - stepSize)
        {
            Motor.motorController.SetDutyCycle(2, (double)i);
            Thread.Sleep(40);
        }
    }

    public static void DownSlow()
    {
        Console.WriteLine("Body Lay down slow");
        for (decimal i = up; i < down; i = i + stepSize)
        {
            Motor.motorController.SetDutyCycle(2, (double)i);
            Thread.Sleep(15);
        }
    }
    public static void UpDownSlow()
    {
        Console.WriteLine("Body Up Down Slow");
        UpSlow();
        Thread.Sleep(5000);
        DownSlow();
    } 

    public static void UpFastEaseOut()
    {
        decimal stepSize = 0.001m;
        int numberOfSteps = (int)Math.Round((down - up) / stepSize, MidpointRounding.AwayFromZero);
        Console.WriteLine("Number Of Steps " + numberOfSteps);
        int easingSteps = numberOfSteps / 2; 
        Console.WriteLine("Easing Steps " + easingSteps);    
        int currentStepDuration= 4;

        for (decimal i = down; i > up; i = i - stepSize)
        {
            //Ease Out
            if (numberOfSteps < easingSteps) {
                currentStepDuration += 3;        
            }            
            Console.WriteLine(numberOfSteps + " Motor Position " + (double)i + "  This Step Duration " + currentStepDuration);
            Motor.motorController.SetDutyCycle(2, (double)i);
            Thread.Sleep(currentStepDuration);
            numberOfSteps--;
        }
    }

    public static void DownEaseBoth()
    {
      
        int numberOfSteps = (int)Math.Round((down - up) / stepSize, MidpointRounding.AwayFromZero);
        Console.WriteLine("Number Of Steps " + numberOfSteps);
        int easingSteps = numberOfSteps / 2;
        Console.WriteLine("Easing Steps " + easingSteps);
        int currentStepDuration = 4;

        for (decimal i = down; i > up; i = i - stepSize)
        {
            //Ease Out
            if (numberOfSteps < easingSteps)
            {
                currentStepDuration += 3;
            }
            Console.WriteLine(numberOfSteps + " Motor Position " + (double)i + "  This Step Duration " + currentStepDuration);
            Motor.motorController.SetDutyCycle(2, (double)i);
            Thread.Sleep(currentStepDuration);
            numberOfSteps--;
        }
    }
}


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
