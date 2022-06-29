using ZombieBaby.Utilities;

namespace ZombieBaby.Movement;
public class Carrage
{
    private static decimal up { get; } = 0.04222m;
    private static decimal down { get; } = 0.09555m;

    public static void Up()
    {
        Console.WriteLine("Rock up " + up);        
        Motor.motorController.SetDutyCycle(3, decimal.ToDouble(up));
    }

    public static void Down()
    {
        Console.WriteLine("Rock down " + down);
        Motor.motorController.SetDutyCycle(3, decimal.ToDouble(down));
    }

    public static void Release()
    {
        Console.WriteLine("Rock off");
        Motor.motorController.SetDutyCycle(3, 0);
    }

    public static void Rock(int count)
    {
        for(var i = 0; i < count; i++)
        {
            Carrage.Down();
            Thread.Sleep(200);
            Carrage.Up();
            Thread.Sleep(200);
        }
        Carrage.Release();
    }
}