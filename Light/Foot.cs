using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieBaby.Utilities;

namespace ZombieBaby.Light
{
    public static class Foot
    {
        public static void On()
        {
 
            Gpios.footLight.DutyCycle = 0.9;
            Console.WriteLine("Foot On " + Gpios.footLight.DutyCycle);
        }

        public static void Off()
        {           
            Gpios.footLight.DutyCycle = 0.1;
            Console.WriteLine("Foot Off " + Gpios.footLight.DutyCycle);
        }
    }
}
