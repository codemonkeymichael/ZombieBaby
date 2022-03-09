using Iot.Device.Pwm;
using LibVLCSharp.Shared;
using System.Device.Pwm;

namespace ZombieBaby;

public static class Program
{
    //public static GpioController piGPIOController = new GpioController();
    private static I2cConnectionSettings connectionSettings = new I2cConnectionSettings(1, 0x20);
    private static I2cDevice device = I2cDevice.Create(connectionSettings);
    //private static Mcp23017 mcp23017 = new Mcp23017(device);
    //public static GpioController mcp20GPIOController = new GpioController(PinNumberingScheme.Logical, mcp23017);

    public static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Zombie Baby is Running");

        using (var pca9685 = new Pca9685(device, pwmFrequency: 50))
        {
            pca9685.SetDutyCycleAllChannels(0.3); // 30% fill
            PwmChannel firstChannel = pca9685.CreatePwmChannel(0); // channel 0
            //PwmChannel secondChannel = pca9685.CreatePwmChannel(1); // channel 1

            firstChannel.DutyCycle = 0.0; // min
            //secondChannel.DutyCycle = 1.0; // max

            // note: SetDutyCycleAllChannels cannot be used anymore
            //       because it would interfere with firstChannel and secondChannel setting
            //       this cannot be done either:
            //       pca9685.SetDutyCycle(1, 0.7);

            //pca9685.SetDutyCycle(2, 0.7); // channel 2: 70%
        }



    }

    static void CurrentDomain_ProcessExit(object sender, EventArgs e)
    {
        //TODO: Make this work
        //Close a pins and stop playing music
        Console.WriteLine("I quit!");
    }
}
