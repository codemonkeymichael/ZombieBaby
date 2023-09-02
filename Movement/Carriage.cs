using Microsoft.AspNetCore.Components;
using ZombieBaby.Utilities;

namespace ZombieBaby.Movement;
public static class Carriage
{
  
    private static double up { get; } = 0.03;
    private static double down { get; } = 0.1;

    public static void Up()
    {
        Console.WriteLine("Carriage up " + up);        
        //_pwmController.controller.SetDutyCycle(3, up);
    }   

    public static void Down()
    {
        Console.WriteLine("Carriage down " + down);
        //_pwmController.controller.SetDutyCycle(3, down);
    }

    public static void Release()
    {
        Console.WriteLine("Carriage Release");
        //_pwmController.controller.SetDutyCycle(3, 0);
    }

}