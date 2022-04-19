using Iot.Device.Pwm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieBaby.Animation
{
    internal class Body
    {
        private static double down = 0.030029296875 + 0.001; //+ 0.001

        private static double up = 0.12939453125 - 0.03; // - 0.001


        private static double headCenter = 0.7;


        public static void Head(Pca9685 motorController)
        {
            Console.WriteLine("Head");
            motorController.SetDutyCycle(1, headCenter);
     
        }

        public static void SitUp(Pca9685 motorController)
        {
            Console.WriteLine("Sit Up");
            motorController.SetDutyCycle(2, down);
            Thread.Sleep(1000);
            motorController.SetDutyCycle(2, up);
            Thread.Sleep(1000);

            //for (int i = 0; i < times; i++)
            //{
            //    Console.WriteLine("Sit Up");
            //    motorController.SetDutyCycle(1, closed);
            //    Thread.Sleep(500);    
            //}



            //Console.WriteLine("Set the int position min");
            //pca9685.SetDutyCycle(0, min);
            //Thread.Sleep(1000);
            //Console.WriteLine("SG90 Servo This is the fastest it will single step forward.");
            //for (double d = min; d < max; d += 0.001)
            //{
            //    Console.WriteLine("First Loop " + d);
            //    pca9685.SetDutyCycle(0, d);
            //    Thread.Sleep(3);
            //    Console.SetCursorPosition(0, 5);
            //}
            //Thread.Sleep(1000);
            //Console.WriteLine("SG90 Servo This is the fastest it will single step reverse.");
            //for (double dd = max; dd > min; dd += -0.001)
            //{
            //    Console.WriteLine("Second Loop " + dd);
            //    pca9685.SetDutyCycle(0, dd);
            //    Thread.Sleep(3);
            //    Console.SetCursorPosition(0, 6);
            //}
            //Thread.Sleep(1000);
            //Console.WriteLine("To move as fast as it will go just move to a spot without stepping. It's really not much faster than the above.");
            //pca9685.SetDutyCycle(0, max);
            //Thread.Sleep(1000);

            //Console.WriteLine("This is really slow in reverse.");
            //for (double dd = max; dd > min; dd += -0.0001)
            //{
            //    Console.WriteLine("Third Loop " + dd);
            //    pca9685.SetDutyCycle(0, dd);
            //    Thread.Sleep(5);
            //    Console.SetCursorPosition(0, 9);
            //}
            //Console.WriteLine("This is really slow in forward.");
            //Thread.Sleep(1000);
            //for (double d = min; d < max + 0.001; d += 0.0001)
            //{
            //    Console.WriteLine("Fourth Loop " + d);
            //    pca9685.SetDutyCycle(0, d);
            //    Thread.Sleep(5);
            //    Console.SetCursorPosition(0, 10);
            //}
        }
    }
}
