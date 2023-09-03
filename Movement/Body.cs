using Microsoft.AspNetCore.Components;
using ZombieBaby.Utilities;

namespace ZombieBaby.Movement;
public static class Body
{
    private static double up { get; } = 0.041;
    private static double downish { get; } = 0.074;
    private static double breath { get; } = 0.076;
    private static double down { get; } = 0.078;
    private static double stepSize { get; } = 0.0005;
    private static double full { get; } = 0.9005;



    public static void Release()
    {
        Console.WriteLine($"Body Release  pwmFrequency: {PwmController.controller.PwmFrequency}");
        PwmController.controller.SetDutyCycle(2, 0);
    }
    public static void UpFast()
    {
        Console.WriteLine("Body Sit up fast");
        PwmController.controller.SetDutyCycle(2, up);
    }

    //public void DownFast()
    //{
    //    Console.WriteLine("Body Lay down fast");
    //    Motor.motorController.SetDutyCycle(2, down);
    //}

    public static void UpMed()
    {
        Console.WriteLine("Body Sit up medium");
        for (double i = down; i > up; i = i - stepSize)
        {
            PwmController.controller.SetDutyCycle(2, i);
            Thread.Sleep(4);
        }
    }

    public static void DownMed()
    {
        Console.WriteLine("Body Lay down slow");
        for (double i = up; i < down; i = i + stepSize)
        {
            PwmController.controller.SetDutyCycle(2, i);
            Thread.Sleep(5);
        }
    }
    public static void UpALittleSlow()
    {
        Console.WriteLine("Body Sit up a little bit slow");
        for (double i = down; i > downish; i = i - stepSize)
        {
            PwmController.controller.SetDutyCycle(2, i);
            Thread.Sleep(40);
        }
    }

    public static void UpSlow()
    {
        Console.WriteLine("Body Sit up slow");
        for (double i = down; i > up; i = i - stepSize)
        {
            {
                PwmController.controller.SetDutyCycle(2, i);
                Console.WriteLine($"  - GetDutyCycle(2) {i}");
                Thread.Sleep(40);
            }
        }
    }

    public static void DownSlow()
    {
        Console.WriteLine("Body Lay down slow");
        for (double i = up; i < down; i = i + stepSize)
        {
            PwmController.controller.SetDutyCycle(2, i);
            Console.WriteLine($"  - GetDutyCycle(2) {i}");
            Thread.Sleep(15);
        }
        Release();
    }
    public static void UpDownSlow()
    {
        Console.WriteLine($"Body Up Down Slow  pwmFrequency: {PwmController.controller.PwmFrequency} ");
        UpSlow();
        Thread.Sleep(4000);
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
        var currentStepSize = 0.001;
        for (double i = downish; i > up; i = i - currentStepSize)
        {
            //Ease Out
            if (numberOfSteps < easingSteps)
            {
                currentStepDuration += 3;
                currentStepSize = currentStepSize - 0.0001;
            }
            //Console.WriteLine(numberOfSteps + " Motor Position " + (double)i + "  This Step Duration " + currentStepDuration);
            PwmController.controller.SetDutyCycle(2, i);
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
            PwmController.controller.SetDutyCycle(2, i);
            Thread.Sleep(currentStepDuration);
            steps--;
        }
        Release();
    }

    public static void SleepingBreath()
    {
        Console.WriteLine("Sleeping Breath");

        for (double i = down; i > breath; i = i - stepSize)
        {
            PwmController.controller.SetDutyCycle(2, (double)i);
            Thread.Sleep(60);
        }
    }

    public static void Up()
    {
        PwmController.controller.SetDutyCycle(2, up);
    }

    public static void UpForward()
    {
        PwmController.controller.SetDutyCycle(2, up - 0.001);
    }

    public static void UpBack()
    {
        PwmController.controller.SetDutyCycle(2, up + 0.001);
    }
}