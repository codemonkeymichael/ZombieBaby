using ZombieBaby.Utilities;

namespace ZombieBaby.Movement;
public static class Body
{
    private static double up { get; } = 0.043;
    private static double down { get; } = 0.078;
    private static double stepSize { get; } = 0.001;

    public static void Release()
    {
        Console.WriteLine("Body Release");
        Motor.motorController.SetDutyCycle(2, 0);
    }
    public static void UpFast()
    {
        Console.WriteLine("Body Sit up fast");
        Motor.motorController.SetDutyCycle(2, up);
    }

    public static void DownFast()
    {
        Console.WriteLine("Body Lay down fast");
        Motor.motorController.SetDutyCycle(2, down);
    }

    public static void UpMed()
    {
        Console.WriteLine("Body Sit up medium");
        for (double i = down; i > up; i = i - stepSize)
        {
            Motor.motorController.SetDutyCycle(2, i);
            Thread.Sleep(7);
        }
    }

    public static void DownMed()
    {
        Console.WriteLine("Body Lay down slow");
        for (double i = up; i < down; i = i + stepSize)
        {
            Motor.motorController.SetDutyCycle(2, i);
            Thread.Sleep(7);
        }
    }

    public static void UpSlow()
    {
        Console.WriteLine("Body Sit up slow");
        for (double i = down; i > up; i = i - stepSize)
        {
            Motor.motorController.SetDutyCycle(2, i);
            Thread.Sleep(40);
        }
    }

    public static void DownSlow()
    {
        Console.WriteLine("Body Lay down slow");
        for (double i = up; i < down; i = i + stepSize)
        {
            Motor.motorController.SetDutyCycle(2, i);
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
        Console.WriteLine("Body Up Fast Ease Out");
        int numberOfSteps = (int)Math.Round((down - up) / stepSize, MidpointRounding.AwayFromZero);
        //Console.WriteLine("Number Of Steps " + numberOfSteps);
        int easingSteps = numberOfSteps / 2;
        //Console.WriteLine("Easing Steps " + easingSteps);    
        int currentStepDuration = 4;

        for (double i = down; i > up; i = i - stepSize)
        {
            //Ease Out
            if (numberOfSteps < easingSteps)
            {
                currentStepDuration += 3;
            }
            //Console.WriteLine(numberOfSteps + " Motor Position " + (double)i + "  This Step Duration " + currentStepDuration);
            Motor.motorController.SetDutyCycle(2, i);
            Thread.Sleep(currentStepDuration);
            numberOfSteps--;
        }
    }

    public static void DownEaseBoth()
    {
        Console.WriteLine("Body Down Ease Both");
        int steps = (int)Math.Round((down - up) / stepSize, MidpointRounding.AwayFromZero);
        //Console.WriteLine("Number Of Steps " + steps);
        int easingSteps = steps / 2;
        //Console.WriteLine("Easing Steps " + easingSteps);
        int currentStepDuration = 40;

        for (double i = up; i < down; i = i + stepSize)
        {
            if (steps < easingSteps)
            {
                //Console.WriteLine("Ease Out");
                currentStepDuration += 2;
            }
            else
            {
                //Console.WriteLine("Ease In");
                currentStepDuration -= 2;
            }
            //Console.WriteLine(steps + " Motor Position " + (double)i + "  This Step Duration " + currentStepDuration);
            Motor.motorController.SetDutyCycle(2, i);
            Thread.Sleep(currentStepDuration);
            steps--;
        }
        Release();
    }

    public static void Breathing(int ContinueOnStatus)
    {
        Console.WriteLine("Breathing");
        Release();

        //TODO Add Breathing sounds 

        Random rnd = new Random();

        while (ContinueOnStatus == Status.CurrentStatus)
        {
            double downish = (double)rnd.NextInt64(73, 75) / (double)1000; 
           
            Console.WriteLine(downish);
            for (double i = down; i > downish; i = i - stepSize)
            {
                //Console.WriteLine(numberOfSteps + " Motor Position " + (double)i + "  This Step Duration " + currentStepDuration);
                Motor.motorController.SetDutyCycle(2, (double)i);
                Thread.Sleep(75);
            }
            Thread.Sleep(400);
            Thread fan = new Thread(() => Effects.Fan.OnOff(2000));
            fan.Start();
            Thread.Sleep(900);
            Release();
            Thread.Sleep(4050);
        }

    }
}