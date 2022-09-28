using ZombieBaby.Utilities;

namespace ZombieBaby.Movement;
public class Carriage
{
    private static decimal up { get; } = 0.04m;
    private static decimal down { get; } = 0.09m;

    public void Up()
    {
        Console.WriteLine("Carriage up " + up);        
        Motor.motorController.SetDutyCycle(3, decimal.ToDouble(up));
    }

    public void Down()
    {
        Console.WriteLine("Carriage down " + down);
        Motor.motorController.SetDutyCycle(3, decimal.ToDouble(down));
    }

    public void Release()
    {
        Console.WriteLine("Carriage Release");
        Motor.motorController.SetDutyCycle(3, 0);
    }

}