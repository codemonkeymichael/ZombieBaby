using ZombieBaby.Utilities;

namespace ZombieBaby.Movement;
public class Body
{
    private double up { get; } = 0.043;

    private double downish { get; } = 0.070;
    private double down { get; } = 0.078;
    private double stepSize { get; } = 0.001;

    public void Release()
    {
        Console.WriteLine("Body Release");
        Motor.motorController.SetDutyCycle(2, 0);
    }
    public void UpFast()
    {
        Console.WriteLine("Body Sit up fast");
        Motor.motorController.SetDutyCycle(2, up);
    }

    public void DownFast()
    {
        Console.WriteLine("Body Lay down fast");
        Motor.motorController.SetDutyCycle(2, down);
    }

    public void UpMed()
    {
        Console.WriteLine("Body Sit up medium");
        for (double i = down; i > up; i = i - stepSize)
        {
            Motor.motorController.SetDutyCycle(2, i);
            Thread.Sleep(7);
        }
    }

    public void DownMed()
    {
        Console.WriteLine("Body Lay down slow");
        for (double i = up; i < down; i = i + stepSize)
        {
            Motor.motorController.SetDutyCycle(2, i);
            Thread.Sleep(7);
        }
    }
    public void UpALitleSlow()
    {
        Console.WriteLine("Body Sit up a little bit slow");
        for (double i = down; i > downish; i = i - stepSize)
        {
            Motor.motorController.SetDutyCycle(2, i);
            Thread.Sleep(40);
        }
    }

    public void UpSlow()
    {
        Console.WriteLine("Body Sit up slow");
        for (double i = down; i > up; i = i - stepSize)
        {
            Motor.motorController.SetDutyCycle(2, i);
            Thread.Sleep(40);
        }
    }

    public void DownSlow()
    {
        Console.WriteLine("Body Lay down slow");
        for (double i = up; i < down; i = i + stepSize)
        {
            Motor.motorController.SetDutyCycle(2, i);
            Thread.Sleep(15);
        }
    }
    public void UpDownSlow()
    {
        Console.WriteLine("Body Up Down Slow");
        UpSlow();
        Thread.Sleep(5000);
        DownSlow();
    }

    public void UpFastEaseOut()
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

    public void DownEaseBoth()
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

    public void SleepingBreath()
    {
        Console.WriteLine("Sleeping Breath");
        double downish = 0.077;

        for (double i = down; i > downish; i = i - stepSize)
        {
            Motor.motorController.SetDutyCycle(2, (double)i);
            Thread.Sleep(60);
        }
    }
}


