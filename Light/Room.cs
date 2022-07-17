using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieBaby.Utilities;

namespace ZombieBaby.Light;

public class Room
{
    public static void All(int val = 0, int duration = 0)
    {
        Console.WriteLine("All On");
        foreach (var chan in DMX.ChannelList)
        {
            chan.TargetValue = 255;
        }
        DMX.Update(duration);
    }

  

}
