using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieBaby.Utilities;

namespace ZombieBaby.Light;

public class Room
{
    public static void AllOn()
    {
        Console.WriteLine("All On");
        //Set the channel model
        //Upstage
        DMX.ChannelList.Channels[0].ChannelValue = 255;
        DMX.ChannelList.Channels[1].ChannelValue = 255;
        DMX.ChannelList.Channels[2].ChannelValue = 255;
        //Stage Left
        DMX.ChannelList.Channels[3].ChannelValue = 255;
        DMX.ChannelList.Channels[4].ChannelValue = 255;
        DMX.ChannelList.Channels[5].ChannelValue = 255;
        //Stage Right
        DMX.ChannelList.Channels[6].ChannelValue = 255;
        DMX.ChannelList.Channels[7].ChannelValue = 255;
        DMX.ChannelList.Channels[8].ChannelValue = 255;
        //Ground Effects
        DMX.ChannelList.Channels[9].ChannelValue = 255;
        DMX.ChannelList.Channels[10].ChannelValue = 255;
        DMX.ChannelList.Channels[11].ChannelValue = 255;
        //DMX.SendDMX();
    }

    public static void AllOff()
    {
        Console.WriteLine("All Off");
        //Set the channel model
        DMX.ChannelList.Channels[0].ChannelValue = 0;
        DMX.ChannelList.Channels[1].ChannelValue = 0;
        DMX.ChannelList.Channels[2].ChannelValue = 0;
        //Stage Left
        DMX.ChannelList.Channels[3].ChannelValue = 0;
        DMX.ChannelList.Channels[4].ChannelValue = 0;
        DMX.ChannelList.Channels[5].ChannelValue = 0;
        //Stage Right
        DMX.ChannelList.Channels[6].ChannelValue = 0;
        DMX.ChannelList.Channels[7].ChannelValue = 0;
        DMX.ChannelList.Channels[8].ChannelValue = 0;
        //Ground Effects
        DMX.ChannelList.Channels[9].ChannelValue = 0;
        DMX.ChannelList.Channels[10].ChannelValue = 0;
        DMX.ChannelList.Channels[11].ChannelValue = 0;
        //DMX.SendDMX();
    }

}
