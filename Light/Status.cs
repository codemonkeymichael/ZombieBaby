using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieBaby.Utilities;

namespace ZombieBaby.Light
{
    public static class Status
    {
        public static int CurrentStatus { get; set; } = 0;
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

        public static void SleepingStatus1()
        {
            CurrentStatus = 1;
            Console.WriteLine($"Sleeping Status = {CurrentStatus}");           
            int satusDuration = 400; //*50
            bool IsOn = false;
            while (CurrentStatus == 1 & satusDuration > 0)
            {
                if (IsOn)
                {
                    Off();
                    IsOn = !IsOn;
                }
                else
                {
                    On();
                    IsOn = !IsOn;
                }
                satusDuration--;
                Thread.Sleep(50);
            }
            if (CurrentStatus == 1) CurrentStatus = 0;
        }
    }
}
