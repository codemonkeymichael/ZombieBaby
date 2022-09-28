using AutoMapper;
using LibVLCSharp.Shared;
using System.Diagnostics;
using System.Text.Json;
using System.Timers;
using ZombieBaby.Animation;
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

        //Get the IP address of this pi
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

        //DMX Light Init
        DMX.Connect();
        Light.Ambient.GroundEffect(10, 5000);
        Light.Ambient.Room();

        //VLC Audio Player Init
        Core.Initialize();

        Gpios io = new Gpios(); 
        Motor mo = new Motor();

        Thread flicker = new Thread(() => Ambient.Flicker());
        flicker.Start();

        AnimationPlayer.InitAnimation();
        Thread.Sleep(2000);

        //Movement.Carriage mc = new Movement.Carriage();
        //mc.Up();

        var inputLastState = PinValue.Low;

        while (true)
        {
            var click = Gpios.piGPIOController.Read(Gpios.InputTrigger);
            if (click != inputLastState)
            {
                Console.WriteLine($"Click ");
                inputLastState = click;
                if (click == PinValue.High)
                {
                    Thread status = new Thread(() => Status.ElevateDefcon());
                    status.Start();                 
                }
                Thread.Sleep(500);
            }
        }
    }

    private static void CloseHandler(object? sender, ConsoleCancelEventArgs e)
    {
                  

        Console.WriteLine("");
        Utilities.DMX.Disconnect();
        Movement.Body b = new Movement.Body();
        b.Release();
        Movement.Carriage c = new Movement.Carriage();
        c.Release();
        Light.Eyes le = new Light.Eyes();
        le.Off();
        Movement.Eyes me = new Movement.Eyes(); 
        me.Closed();
        Thread.Sleep(1000);
        me.Release();
        Movement.Head mh = new Movement.Head();
        mh.Release();
        Effects.Fan ef = new Effects.Fan();
        ef.Off();
        Console.WriteLine("Bye for now :)");
    }
}