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
    private static int Defcon1ThreadCounter = 0;

    /// <summary>
    /// 
    /// </summary>
    public static void ElevateDefcon()
    {
        CurrentStatus--;
        if (CurrentStatus == 0) CurrentStatus = 1;
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
                Defcon1();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Sleeping
    /// </summary>
    /// <param name="durationSeconds"></param>
    private static void Defcon3(int durationSeconds)
    {
        CurrentStatus = 3;
        //Thread dc3 = new Thread(() => Playlists.Defcon3.Sleep());
        //dc3.Start();
        Console.WriteLine($"-- Defcon {CurrentStatus}");

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

    }

    /// <summary>
    /// Awake
    /// </summary>
    /// <param name="durationSeconds"></param>
    private static void Defcon2(int durationSeconds)
    {
        CurrentStatus = 2;
        //Thread dc2 = new Thread(() => Playlists.Defcon2.Awake());
        //dc2.Start();
        Console.WriteLine($"-- Defcon {CurrentStatus}");
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
        if (CurrentStatus == 2)
        {
            PreviousStatus = 2;
            Defcon3(20);
        }

    }

    /// <summary>
    /// Sitting Up
    /// </summary>
    /// <param name="durationSeconds"></param>
    private static void Defcon1()
    {
        if (Defcon1ThreadCounter > 0)
        {
            //Run again          
            Thread dc1 = new Thread(() => Playlists.Defcon1.Talk());
            dc1.Start();
            Defcon1ThreadCounter++;
            Console.WriteLine($"-- Defcon {CurrentStatus} Again Threads Running {Defcon1ThreadCounter}");
            while (
                CurrentStatus == 1
                &&
                (
                    dc1.ThreadState == ThreadState.WaitSleepJoin ||
                    dc1.ThreadState == ThreadState.Running
                )
            )
            {
                ////Blink Once 
                //On();
                //Thread.Sleep(100);
                //Off();
                Thread.Sleep(900);
                //Console.WriteLine("   ThreadState = " + dc1.ThreadState);   
            }
            Defcon1ThreadCounter--;
        }
        else
        {
            //Run the first time
            CurrentStatus = 1;
            Defcon1ThreadCounter++;
            Console.WriteLine($"-- Defcon {CurrentStatus} Again Threads Running {Defcon1ThreadCounter}");

            Thread dc1 = new Thread(() => Playlists.Defcon1.SitUp());
            dc1.Start();
            while (
                CurrentStatus == 1
                &&
                (
                    dc1.ThreadState == ThreadState.WaitSleepJoin ||
                    dc1.ThreadState == ThreadState.Running
                )
            )
            {
                ////Blink Once 
                //On();
                //Thread.Sleep(100);
                //Off();
                Thread.Sleep(900);
                //Console.WriteLine("   ThreadState = " + dc1.ThreadState);
            }
            Defcon1ThreadCounter--;
        }
        Console.WriteLine($"++ Defcon {CurrentStatus} Again Threads Running {Defcon1ThreadCounter}");

        if (Defcon1ThreadCounter <= 0) Defcon1End();


        Console.WriteLine($"          Status = Defcon {CurrentStatus}");
    }

    private static void Defcon1End()
    {
        Console.WriteLine($"++++++++++++++++++++++++++  Defcon1End");
        PreviousStatus = 1;
        var ds = new Movement.Body();
        ds.DownSlow();
        Thread.Sleep(2000);
        var bc = new Playlists.Eyes();
        bc.BlinkClosed();
        Defcon3(5);
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
