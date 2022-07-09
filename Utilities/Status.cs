using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieBaby.Utilities;

public static class Status
{
    public static int CurrentStatus { get; set; } = 0;
    public static bool StopStatus1 { get; set; } = false;
    public static bool StopStatus2 { get; set; } = false;


    public static void Status1(int durationSeconds)
    {
        CurrentStatus = 1;
        Console.WriteLine($"Status = {CurrentStatus}");

        while (CurrentStatus == 1 & durationSeconds > 0 & !StopStatus1)
        {
            //Blink Once 
            On();
            Thread.Sleep(100);
            Off();
            Thread.Sleep(900);
            durationSeconds--;
        }
        if (CurrentStatus == 1) CurrentStatus = 0;
        Console.WriteLine($"Status = {CurrentStatus}");
    }

    public static void Status2(int durationSeconds)
    {
        CurrentStatus = 2;
        Console.WriteLine($"Status = {CurrentStatus}");
        while (CurrentStatus == 2 & durationSeconds > 0 & !StopStatus2)
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
            StopStatus1 = false;
            Status1(5);
        }
    }

    public static void Status3(int durationSeconds)
    {
        CurrentStatus = 3;
        Console.WriteLine($"Status = {CurrentStatus}");
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
            StopStatus2 = false;
            Status2(5);
        }
    }

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
}
