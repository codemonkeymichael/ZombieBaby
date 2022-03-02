using LibVLCSharp.Shared;

namespace ZombieBaby;

public static class Program
{
    public static GpioController piGPIOController = new GpioController();
    private static I2cConnectionSettings connectionSettings = new I2cConnectionSettings(1, 0x20);
    private static I2cDevice device = I2cDevice.Create(connectionSettings);
    private static Mcp23017 mcp23017 = new Mcp23017(device);
    public static GpioController mcp20GPIOController = new GpioController(PinNumberingScheme.Logical, mcp23017);

    public static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Zombie Baby is Running");



    

    }

    static void CurrentDomain_ProcessExit(object sender, EventArgs e)
    {
        //TODO: Make this work
        //Close a pins and stop playing music
        Console.WriteLine("I quit!");
    }
}
