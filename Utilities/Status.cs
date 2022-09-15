using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieBaby.Utilities;

/// <summary>
/// Root control of everything
/// </summary>
public static class Status
{
    public static int CurrentStatus = 4;
    public static int PreviousStatus = 4;

    /// <summary>
    /// 
    /// </summary>
    public static void ElevateDefcon()
    {
        if (CurrentStatus > 1)
        {
            CurrentStatus--;
            Console.WriteLine($"ElevateDefcon() CurrentStatus = {CurrentStatus}");
            switch (CurrentStatus)
            {
                case 3:
                    Defcon3(60);
                    break;
                case 2:
                    Defcon2(30);
                    break;
                case 1:
                    Defcon1(45);
                    break;
                default:
                    break;
            }
        }
    }


    /// <summary>
    /// Sleeping
    /// </summary>
    /// <param name="durationSeconds"></param>
    private static void Defcon3(int durationSeconds)
    {
        CurrentStatus = 3;
        Thread dc3 = new Thread(() => Playlists.Defcon3.Sleep());
        dc3.Start();
        Console.WriteLine($"          Status3 = Defcon {CurrentStatus}");

        while (CurrentStatus == 3 & durationSeconds > 0)
        {
            //Blink Three Times
            On();
            Thread.Sleep(100); //1
            Off();
            Thread.Sleep(100);
            On();
            Thread.Sleep(100); //2
            Off();
            Thread.Sleep(100);
            On();
            Thread.Sleep(100); //3
            Off();
            Thread.Sleep(500);
            durationSeconds--;
        }
        if (CurrentStatus == 3)
        {
            PreviousStatus = 3;
            CurrentStatus = 4;
        }
        Console.WriteLine($"          Status3 = Defcon {CurrentStatus}");
    }

    /// <summary>
    /// Awake
    /// </summary>
    /// <param name="durationSeconds"></param>
    private static void Defcon2(int durationSeconds)
    {
        CurrentStatus = 2;
        Thread dc2 = new Thread(() => Playlists.Defcon2.Awake());
        dc2.Start();
        Console.WriteLine($"          Status2 = Defcon {CurrentStatus}");
        while (CurrentStatus == 2 & durationSeconds > 0)
        {
            //Blink Twice
            On();
            Thread.Sleep(100);
            Off();
            Thread.Sleep(100);
            On();
            Thread.Sleep(100);
            Off();
            Thread.Sleep(700);
            durationSeconds--;
        }
        if (CurrentStatus == 2) { 
            PreviousStatus = 2;
            Defcon3(20); 
        }
        Console.WriteLine($"          Status2 = Defcon {CurrentStatus}");

    }

    /// <summary>
    /// Sitting Up
    /// </summary>
    /// <param name="durationSeconds"></param>
    private static void Defcon1(int durationSeconds)
    {
        CurrentStatus = 1;
        Thread dc1 = new Thread(() => Playlists.Defcon1.SitUp());
        dc1.Start();
        Console.WriteLine($"          Status1 = Defcon {CurrentStatus}");
        while (CurrentStatus == 1 & durationSeconds > 0)
        {
            //Blink Once 
            On();
            Thread.Sleep(100);
            Off();
            Thread.Sleep(900);
            durationSeconds--;
        }
        if (CurrentStatus == 1)
        {
            PreviousStatus = 1;
            Defcon3(5);
        };
        Console.WriteLine($"          Status1 = Defcon {CurrentStatus}");
    }

    private static void On()
    {
        //Console.WriteLine("Status On");
        Gpios.piGPIOController.Write(Gpios.Status, PinValue.High);
    }

    private static void Off()
    {
        //Console.WriteLine("Status Off");
        Gpios.piGPIOController.Write(Gpios.Status, PinValue.Low);
    }
}
