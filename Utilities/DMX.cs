using System;
using System.IO;
using System.IO.Ports;
using System.Timers;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace ZombieBaby.Utilities;

public class DMXChannel
{
    public int ChannelId { get; set; }
    public int ChannelValue { get; set; }
    public int TargetValue { get; set; }
}

public static class DMX
{
    private static SerialPort port = new SerialPort("/dev/ttyUSB0", 115200); //9600
    private static byte[] message;
    public static List<DMXChannel> ChannelList = new List<DMXChannel>();
    public static List<DMXChannel> ChannelListTemp = new List<DMXChannel>();
    private static int _duration = 0;
    private static System.Timers.Timer animationFrameTimer = new System.Timers.Timer();

    public static void Update(int duration = 0 )
    {
        Console.WriteLine("Update DMX");
        _duration = duration;
        RecordChannels();

        animationFrameTimer.Interval = 25; //25
        animationFrameTimer.Start();
        animationFrameTimer.Elapsed += new ElapsedEventHandler(AnimateChannels);
     }

    public static bool Connect()
    {
        //Instantiate the channel model
        ChannelList = new List<DMXChannel>();
        for (int i = 0; i <= 15; i++)
        {
            DMXChannel c = new DMXChannel();
            c.ChannelId = i;
            c.ChannelValue = 0;
            c.TargetValue = 0;
            ChannelList.Add(c);
        }


        ChannelListTemp = new List<DMXChannel>();
        for (int i = 0; i <= 15; i++)
        {
            DMXChannel c = new DMXChannel();
            c.ChannelId = i;
            c.ChannelValue = 0;
            c.TargetValue = 0;
            ChannelListTemp.Add(c);
        }

        try
        {

            Console.WriteLine("DMX port open " + port.IsOpen);
            Console.WriteLine("DMX port open " + port.BaudRate);
            Console.WriteLine("DMX port open " + port.PortName);
            Console.WriteLine("DMX port open ");
      
            if (!port.IsOpen)
            {
                port.ErrorReceived += Port_ErrorReceived;
                port.Open();
                GC.SuppressFinalize(port);


                Console.WriteLine("DMX connected");
            }
            //SendDMX();
            return true;
        }
        catch (IOException ex)
        {
            Console.WriteLine("Port could not be opened! Error :( " + ex);
        }

        return false;
    }

    private static void Port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
    {
        Console.WriteLine("DMX Port Error");
    }

    public static bool Disconnect()
    {
        try
        {
            port.Close();
            port.Dispose();
            Console.WriteLine("DMX disconnect");
            return true;
        }
        catch (IOException ex)
        {
            Console.WriteLine(ex);
        }
        return false;
    }

    private static bool Send(byte[] byteArray)
    {
        if (port == null || !port.IsOpen)
            return false;

        int len = byteArray.Length;
        //Console.WriteLine("Send bytes " + len);

        if (len > 0)
        {
            message = new byte[len + 6];
            message[0] = 0x7E;
            message[1] = 0x06;
            message[2] = (byte)((len + 1) & 0xFF);
            message[3] = (byte)((len >> 8) & 0xFF);
            message[4] = 0x00;
            byteArray.CopyTo(message, 5);
            message[len + 5] = 0xE7;

            port.Write(message, 0, message.Length);
        }
        return true;
    }

    private static void AnimateChannels(object sender, ElapsedEventArgs args)
    {
        if (_duration > 0)
        {
            _duration = _duration - (int)animationFrameTimer.Interval;
            //How many steps left in the duration?
            int _remainingDurationSteps = _duration / (int)animationFrameTimer.Interval;      
            //Console.WriteLine("Remaining Duration Steps " + _remainingDurationSteps);
            for (var i = 0; i < ChannelList.Count(); i++)
            {                
                //How many steps left in the duration?
                int _remainingDMXSteps = ChannelList[i].TargetValue - ChannelList[i].ChannelValue;
                //Console.WriteLine("Remaining DMX Steps " + _remainingDMXSteps);

                //Figure Out What the DMX value should be.            
                int _dmxStep = (int)Math.Round((double)_remainingDMXSteps / _remainingDurationSteps , MidpointRounding.AwayFromZero); //_remainingDMXSteps / _remainingDurationSteps  ;
                //Console.WriteLine("DMX Step " + _dmxStep);
                ChannelList[i].ChannelValue += _dmxStep;
            }
            SendDMX();
        }
        else
        {
            //Last step End Animation and Clean Up
            for (var i = 0; i < ChannelList.Count(); i++)
            {
                ChannelList[i].ChannelValue = ChannelList[i].TargetValue;
            }

            SendDMX();
            //Clean up animation
            animationFrameTimer.Stop();
            _duration = 0;
        }
    }

    private static void SendDMX()
    {
        byte[] bytes = new byte[ChannelList.Count];
        for (int i = 0; i < ChannelList.Count; i++)
        {
            //Just in case
            if (ChannelList[i].ChannelValue > 255) ChannelList[i].ChannelValue = 255;
            if (ChannelList[i].ChannelValue < 0) ChannelList[i].ChannelValue = 0;

            bytes[i] = ((byte)ChannelList[i].ChannelValue);
            //Console.WriteLine("Send Channel " + (i + 1).ToString() + " DMX Value = " + ChannelList[i].ChannelValue.ToString() + "  ");
        }
        Send(bytes);
    }

    private static string[] Ports()
    {
        return SerialPort.GetPortNames();
    }

    public static void RecordChannels()
    {
        //Console.WriteLine("Record DMX Channels");
        for (int i = 0; i < ChannelList.Count; i++)
        {
            ChannelListTemp[i].ChannelValue = ChannelList[i].ChannelValue;
        }
    }

    public static void ReadChannels(int duration = 0)
    {
        //Console.WriteLine("Read DMX Channels");
        for (int i = 0; i < ChannelListTemp.Count; i++)
        {
            ChannelList[i].TargetValue = ChannelListTemp[i].ChannelValue;
        }
        Update(duration);
    }
}
