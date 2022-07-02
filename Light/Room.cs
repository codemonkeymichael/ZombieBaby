using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieBaby.Utilities;

namespace ZombieBaby.Light;

public class Room
{

    public static void Chan1WhiteOn()
    {
        Console.WriteLine("Chan 1 White On");
        DMXserial.SetDMX(1,255, 255, 255);
    }

    public static void Chan1WhiteOff()
    {
        Console.WriteLine("Chan 1 White Off");
        DMXserial.SetDMX(1, 0, 0, 0);
    }

}
