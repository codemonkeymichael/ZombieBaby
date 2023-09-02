using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieBaby.Utilities;

namespace ZombieBaby.Light;

public static class Room
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

    public static void OneChannel(int channel = 0, int value = 0)
    {
        Console.WriteLine($"OneChannel chan={channel} val={value}" );
        DMX.ChannelList[channel].TargetValue = value;
       
    }



}
