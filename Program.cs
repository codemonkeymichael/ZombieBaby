using LibVLCSharp.Shared;
using System.Diagnostics;
using System.Timers;
using ZombieBaby.Audio;
using ZombieBaby.Light;
using ZombieBaby.Utilities;

namespace ZombieBaby;
class Program
{
    static void Main(string[] args)
    {
        //If the user stops the program
        Console.CancelKeyPress += new ConsoleCancelEventHandler(CloseHandler);

        Console.Clear();
        Console.WriteLine("Zombie Baby is Running Ver 0.7");

        var proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "hostname",  //my linux command i want to execute
                Arguments = "-I",  //the argument to return ip address
                UseShellExecute = false,
                RedirectStandardOutput = true,  //redirect output to my code here
                CreateNoWindow = true //do not show a window
            }
        };

        proc.Start();  //start the process
        while (!proc.StandardOutput.EndOfStream)  //wait until entire stream from output read in
        {
            Console.WriteLine(proc.StandardOutput.ReadLine());  //this contains the ip output                    
        }

        AudioPlayer.InitAudio();

        DMX.Connect();
        Light.Ambient.GroundEffect(10, 5000);

        //VLC Audio Player Init
        Core.Initialize();

        Gpios io = new Gpios(); //Just need to hit the constructor here TODO Make this static
        Motor mo = new Motor(); //Just need to hit the constructor here TODO Make this static

        Thread flicker = new Thread(() => Ambient.Flicker());
        flicker.Start();

        var inputLastState = PinValue.Low;

        while (true)
        {
            var click = Gpios.piGPIOController.Read(Gpios.InputTrigger);
            if (click != inputLastState)
            {
                Console.WriteLine($"Click");
                inputLastState = click;
                if (click == PinValue.High)
                {
                    Thread status = new Thread(() => Status.ElevateDefcon());
                    status.Start();
                }
                Thread.Sleep(250);
            }
        }
    }

    private static void CloseHandler(object? sender, ConsoleCancelEventArgs e)
    {
        Console.WriteLine("");
        Utilities.DMX.Disconnect();
        Movement.Body.Release();
        Movement.Carriage.Release();
        Light.Eyes.Off();
        Movement.Eyes.Closed();
        Thread.Sleep(1000);
        Movement.Eyes.Release();
        Movement.Head.Release();
        Effects.Fan.Off();
        Console.WriteLine("Bye for now :)");
    }
}