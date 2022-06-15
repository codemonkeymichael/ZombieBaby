using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieBaby.Utilities;

namespace ZombieBaby.Light
{
    public static class Head
    {
        public static bool FadeOut { get; set; } = false;
        public static void On()
        {          
            Gpios.headLight.DutyCycle = 0.9;
            Console.WriteLine("Head On " + Gpios.headLight.DutyCycle);
        }

        public static void Off()
        {   
            Gpios.headLight.DutyCycle = 0.1;
            Console.WriteLine("Head Off " + Gpios.headLight.DutyCycle);
        }

        public static void Flicker()
        {
            Flicker(4, 9, 1, 4, 25, 150);
        }

        public static void Flicker(int dimMin, int dimMax, int speedMin, int speedMax, int holdMin, int holdMax)
        {
            Console.WriteLine("Head Flicker Started");
            Random rnd = new Random();
            double cur = 0.5;
            bool dim = true;

            while (true)
            {
                double flicker = (double)rnd.NextInt64(dimMin, dimMax) / (double)10; //4 9
                double dimSpeed = (double)rnd.NextInt64(speedMin, speedMax) / (double)1000; //1 4
                int holdDuration = (int)rnd.NextInt64(25, 150); //25 150
                if (FadeOut) flicker = 0;
                //Console.WriteLine(flicker + " " + dimSpeed + " " + holdDuration + " " + FadeOut);


                if (flicker > cur)
                {
                    //This will fade the light up to the new dim level
                    for (double d = cur; d < flicker; d = d + dimSpeed)
                    {
                        Gpios.headLight.DutyCycle = d;
                        Thread.Sleep(1);
                        cur = d;
                    }
                }
                else
                {
                    //This will fade the light down to the new dim level
                    for (double d = cur; d > flicker; d = d - dimSpeed)
                    {
                        Gpios.headLight.DutyCycle = d;
                        Thread.Sleep(2);
                        cur = d;
                    }

                }
                Thread.Sleep(holdDuration);
            }
        }
    }
}
