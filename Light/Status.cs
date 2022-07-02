using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieBaby.Utilities;

namespace ZombieBaby.Light;

public static class Status
{
    public static int CurrentStatus { get; set; } = 0;
    public static bool StopStatus1 { get; set; } = false;
    public static bool StopStatus2 { get; set; } = false;
    public static void On()
    {
        //Console.WriteLine("Status On");
        Gpios.piGPIOController.Write(Gpios.Status, PinValue.High);
    }

    public static void Off()
    {
        //Console.WriteLine("Status Off");
        Gpios.piGPIOController.Write(Gpios.Status, PinValue.Low);
    }

    public static void Status1()
    {
        CurrentStatus = 1;
        Console.WriteLine($"Status = {CurrentStatus}");
        int satusDuration = 20; //*1000 = 15sec

        while (CurrentStatus == 1 & satusDuration > 0 & !StopStatus1)
        {
            //Blink Once 
            On();
            Thread.Sleep(100);
            Off();
            Thread.Sleep(900);
            satusDuration--;
        }
        if (CurrentStatus == 1) CurrentStatus = 0;
        Console.WriteLine($"Status = {CurrentStatus}");
    }

    public static void Status2()
    {
        CurrentStatus = 2;
        Console.WriteLine($"Status = {CurrentStatus}");
        int satusDuration = 20; //*1000 = 20sec
        bool IsOn = false;
        while (CurrentStatus == 2 & satusDuration > 0 & !StopStatus2)
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
            satusDuration--;
        }
        if (CurrentStatus == 2)
        {
            StopStatus1 = false;
            Status1();
        }
    }

    public static void Status3()
    {
        CurrentStatus = 3;
        Console.WriteLine($"Status = {CurrentStatus}");
        int satusDuration = 20; //*1000 = 20sec

        while (CurrentStatus == 3 & satusDuration > 0)
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
            satusDuration--;
        }
        if (CurrentStatus == 3)
        {
            StopStatus2 = false;
            Status2();
        }
    }
}
