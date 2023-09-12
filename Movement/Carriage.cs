using ZombieBaby.Utilities;

namespace ZombieBaby.Movement;
public class Carriage
{
    private static double up { get; } = 0.03;
    private static double down { get; } = 0.1;

    public void Up()
    {
        Console.WriteLine("Carriage up " + up);
        PwmController.controller.SetDutyCycle(3, up);
    }

    public void Down()
    {
        Console.WriteLine("Carriage down " + down);
        PwmController.controller.SetDutyCycle(3, down);
    }

    public void Release()
    {
        Console.WriteLine("Carriage Release");
        PwmController.controller.SetDutyCycle(3, 0);
    }

}