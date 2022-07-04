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
        DMXserial.ChannelList.Channels[0].ChannelValue = 255;
        DMXserial.ChannelList.Channels[1].ChannelValue = 255;
        DMXserial.ChannelList.Channels[2].ChannelValue = 255;
        //Stage Left
        DMXserial.ChannelList.Channels[3].ChannelValue = 255;
        DMXserial.ChannelList.Channels[4].ChannelValue = 255;
        DMXserial.ChannelList.Channels[5].ChannelValue = 255;
        //Stage Right
        DMXserial.ChannelList.Channels[6].ChannelValue = 255;
        DMXserial.ChannelList.Channels[7].ChannelValue = 255;
        DMXserial.ChannelList.Channels[8].ChannelValue = 255;
        //Ground Effects
        DMXserial.ChannelList.Channels[9].ChannelValue = 255;
        DMXserial.ChannelList.Channels[10].ChannelValue = 255;
        DMXserial.ChannelList.Channels[11].ChannelValue = 255;
        DMXserial.SendDMX();
    }

    public static void AllOff()
    {
        Console.WriteLine("All Off");
        //Set the channel model
        DMXserial.ChannelList.Channels[0].ChannelValue = 0;
        DMXserial.ChannelList.Channels[1].ChannelValue = 0;
        DMXserial.ChannelList.Channels[2].ChannelValue = 0;
        //Stage Left
        DMXserial.ChannelList.Channels[3].ChannelValue = 0;
        DMXserial.ChannelList.Channels[4].ChannelValue = 0;
        DMXserial.ChannelList.Channels[5].ChannelValue = 0;
        //Stage Right
        DMXserial.ChannelList.Channels[6].ChannelValue = 0;
        DMXserial.ChannelList.Channels[7].ChannelValue = 0;
        DMXserial.ChannelList.Channels[8].ChannelValue = 0;
        //Ground Effects
        DMXserial.ChannelList.Channels[9].ChannelValue = 0;
        DMXserial.ChannelList.Channels[10].ChannelValue = 0;
        DMXserial.ChannelList.Channels[11].ChannelValue = 0;
        DMXserial.SendDMX();
    }

}
