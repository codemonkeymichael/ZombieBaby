using ZombieBaby.Utilities;

namespace ZombieBaby.Movement;
public static class Body
{
    private static decimal up { get; } = 0.043m;
    private static decimal down { get; } = 0.079m;
    private static decimal stepSize { get; } = 0.001m;

    public static void Release()
    {
        Console.WriteLine("Body Release");
        Motor.motorController.SetDutyCycle(2, 0);
    }
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
        int steps = (int)Math.Round((down - up) / stepSize, MidpointRounding.AwayFromZero);
        Console.WriteLine("Number Of Steps " + steps);
        int easingSteps = steps / 2;
        Console.WriteLine("Easing Steps " + easingSteps);
        int currentStepDuration = 40;

        for (decimal i = up; i < down; i = i + stepSize)
        {
            if (steps < easingSteps)
            {
                Console.WriteLine("Ease Out");
                currentStepDuration += 2;
            }
            else
            {
                Console.WriteLine("Ease In");
                currentStepDuration -= 2;
            }
            Console.WriteLine(steps + " Motor Position " + (double)i + "  This Step Duration " + currentStepDuration);
            Motor.motorController.SetDutyCycle(2, (double)i);
            Thread.Sleep(currentStepDuration);
            steps--;
        }
    }
}