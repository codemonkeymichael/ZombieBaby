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
        //Set the channel model
        DMXserial.ChannelList.Channels[0].ChannelValue = 255;
        DMXserial.ChannelList.Channels[1].ChannelValue = 255;
        DMXserial.ChannelList.Channels[2].ChannelValue = 255;
        DMXserial.SendDMX();
    }

    public static void Chan1WhiteOff()
    {
        Console.WriteLine("Chan 1 White Off");
        //Set the channel model
        DMXserial.ChannelList.Channels[0].ChannelValue = 0;
        DMXserial.ChannelList.Channels[1].ChannelValue = 0;
        DMXserial.ChannelList.Channels[2].ChannelValue = 0;
        DMXserial.SendDMX();
    }

}
