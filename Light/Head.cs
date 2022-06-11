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
        public static void On()
        {
            Gpios.headLight.Start();
            Gpios.headLight.DutyCycle = 0.9;
            Console.WriteLine("Head On " + Gpios.headLight.DutyCycle);
        }

        public static void Off()
        {
            Gpios.headLight.Start();
            Gpios.headLight.DutyCycle = 0.1;
            Console.WriteLine("Head Off " + Gpios.headLight.DutyCycle);
        }
    }
}
