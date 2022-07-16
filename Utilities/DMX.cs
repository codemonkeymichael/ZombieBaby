using System;
using System.IO;
using System.IO.Ports;
using System.Timers;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace ZombieBaby.Utilities;

public class DMXChan
{
    public int ChannelId { get; set; }
    public int ChannelValue { get; set; }
}

//public class DMXChans
//{
//    public List<DMXChan> Channels { get; set; }
//    public int _duration { get; set; }
//}
public class DMXChannels
{
    public List<DMXChannel> Channels { get; set; }
    public int Duration { get; set; }
}

public class DMXChannel
{
    public int ChannelId { get; set; }
    public int ChannelValue { get; set; }
    public int TargetValue { get; set; }
    public int DMXStepsPerFrame { get; set; }
    public int FramesPerDMXStep { get; set; }
    //public bool DMXStepUP { get; set; }
}



public static class DMX
{
    private static SerialPort port = new SerialPort("/dev/ttyUSB0", 115200); //9600
    private static byte[] message;
    public static DMXChannels ChannelList = new DMXChannels();
    public static DMXChannels ChannelListTemp = new DMXChannels();
    private static int _duration = 0; //
    private static System.Timers.Timer animationFrameTimer = new System.Timers.Timer();

    public static void Update(List<DMXChan> chans, int duration = 0 )
    {
        Console.WriteLine("Update");
        _duration = duration;
        RecordChannels();

        foreach (DMXChan chan in chans)
        {
            var cl = ChannelList.Channels.Where(x => x.ChannelId == chan.ChannelId).FirstOrDefault();
            
            //cl.ChannelId = chan.ChannelId;
            //cl.ChannelValue = chan.ChannelValue; 
            cl.TargetValue = chan.ChannelValue;
            //c.DMXStepsPerFrame = 0;
            //c.FramesPerDMXStep = 0;
            //cl.DMXStepUP = true;           
          
        }

        animationFrameTimer.Interval = 10; //25
        animationFrameTimer.Start();
        animationFrameTimer.Elapsed += new ElapsedEventHandler(AnimateChannels);
 
    }

    public static bool Connect()
    {
        //Instantiate the channel model
        ChannelList.Channels = new List<DMXChannel>();
        for (int i = 0; i <= 15; i++)
        {
            DMXChannel c = new DMXChannel();
            c.ChannelId = i;
            c.ChannelValue = 0;
            c.TargetValue = 0;
            c.DMXStepsPerFrame = 1;
            c.FramesPerDMXStep = 1;
            //c.DMXStepUP = true;
            ChannelList.Channels.Add(c);
        }


        ChannelListTemp.Channels = new List<DMXChannel>();
        for (int i = 0; i <= 15; i++)
        {
            DMXChannel c = new DMXChannel();
            c.ChannelId = i;
            c.ChannelValue = 0;
            c.TargetValue = 0;
            c.DMXStepsPerFrame = 1;
            c.FramesPerDMXStep = 1;
            //c.DMXStepUP = true;
            ChannelListTemp.Channels.Add(c);
        }

        try
        {
            if (!port.IsOpen)
            {
                port.Open();
                Console.WriteLine("DMX connected");
            }
            SendDMX();
            return true;
        }
        catch (IOException ex)
        {
            Console.WriteLine("Port could not be opened! Error :( " + ex);
        }

        return false;
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
        //Console.SetCursorPosition(0, 15);
        //Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("AnimateChannels");
        //Console.ForegroundColor = ConsoleColor.Gray;

        //Console.SetCursorPosition(0, 30);
        //Console.ForegroundColor = ConsoleColor.Gray;
        //Console.WriteLine(" _duration = " + _duration.ToString());

        if (_duration > 0)
        {
            _duration--;

            //Console.WriteLine("_duration = " + _duration);

            for (var i = 0; i < ChannelList.Channels.Count(); i++)
            {
                //int xPos = i * 22;
                //Console.SetCursorPosition(xPos, 16);
                //Console.WriteLine("Value = " + ChannelList.Channels[i].ChannelValue);
                ////Console.SetCursorPosition(xPos, 17);
                //Console.WriteLine("_duration = " + _duration);
                ////Console.SetCursorPosition(xPos, 18);
                //Console.WriteLine("DMX Per Frame = " + ChannelList.Channels[i].DMXStepsPerFrame);
                ////Console.SetCursorPosition(xPos, 19);
                //Console.WriteLine("Frame Per DMX = " + ChannelList.Channels[i].FramesPerDMXStep);
                ////Console.SetCursorPosition(xPos, 20);
                //Console.WriteLine("DMX UP = " + ChannelList.Channels[i].DMXStepUP);

                var DMXRemain = ChannelList.Channels[i].TargetValue - ChannelList.Channels[i].ChannelValue;
                
                //Console.SetCursorPosition(xPos, 21);
                Console.WriteLine("DMX Remain = " + DMXRemain);

                if (DMXRemain != 0)
                {
                    //More frames than DMX steps. Go slow. One DMX step every few frames.              
                    if (ChannelList.Channels[i].FramesPerDMXStep != 0)
                    {
                        if (ChannelList.Channels[i].FramesPerDMXStep == 1)
                        {
                            if (ChannelList.Channels[i].ChannelValue < ChannelList.Channels[i].TargetValue)
                            {
                                ChannelList.Channels[i].ChannelValue++;
                                Console.WriteLine("Up Chan=" + i + " Val=" + ChannelList.Channels[i].ChannelValue);
                            }
                            else
                            {
                                ChannelList.Channels[i].ChannelValue--;
                                Console.WriteLine("Down Chan=" + i + " Val=" + ChannelList.Channels[i].ChannelValue);
                            }
                            if (_duration > 0 && DMXRemain != 0)
                            {
                                //Recalculate the frames to make the target DMX channel value 
                                ChannelList.Channels[i].FramesPerDMXStep = _duration / Math.Abs(DMXRemain);
                            }
                        }
                        else
                        {
                            ChannelList.Channels[i].FramesPerDMXStep--;
                            Console.WriteLine("Down Chan=" + i + " Val=" + ChannelList.Channels[i].ChannelValue);
                        }
                    }

                    //More DMX steps than frames. Go fast. One or many DMX steps per frame.
                    if (ChannelList.Channels[i].DMXStepsPerFrame != 0)
                    {
                        var chanVal = ChannelList.Channels[i].DMXStepsPerFrame + ChannelList.Channels[i].ChannelValue;

                        ChannelList.Channels[i].ChannelValue = chanVal;

                        if (_duration > 0 && DMXRemain > 0)
                        {
                            //Recalculate to make the target channel value                   
                            ChannelList.Channels[i].DMXStepsPerFrame = Math.Abs(DMXRemain) / _duration;
                            if (ChannelList.Channels[i].ChannelValue > ChannelList.Channels[i].TargetValue)
                            {
                                ChannelList.Channels[i].DMXStepsPerFrame = -ChannelList.Channels[i].DMXStepsPerFrame;
                            }
                        }
                    }
                }
            }


            SendDMX();
        }
        else
        {
            //Last step End Animation and Clean Up
            for (var i = 0; i < ChannelList.Channels.Count(); i++)
            {
                ChannelList.Channels[i].ChannelValue = ChannelList.Channels[i].TargetValue;

                //int xPos = i * 22;
                //Console.SetCursorPosition(xPos, 16);
                //Console.WriteLine("         Value = " + ChannelList.Channels[i].ChannelValue + "                  ");
                //Console.SetCursorPosition(xPos, 17);
                //Console.WriteLine("      _duration = " + _duration + "                   ");
                //Console.SetCursorPosition(xPos, 18);
                //Console.WriteLine(" DMX Per Frame = " + ChannelList.Channels[i].DMXStepsPerFrame + "            ");
                //Console.SetCursorPosition(xPos, 19);
                //Console.WriteLine(" Frame Per DMX = " + ChannelList.Channels[i].FramesPerDMXStep + "               ");
                //Console.SetCursorPosition(xPos, 20);
                //Console.WriteLine("        DMX UP = " + ChannelList.Channels[i].DMXStepUP + "             ");

                //var DMXRemain = ChannelList.Channels[i].TargetValue - ChannelList.Channels[i].ChannelValue;
                //Console.SetCursorPosition(xPos, 21);
                //Console.WriteLine("    DMX Remain = " + DMXRemain + "              ");
            }

            SendDMX();
            //Clean up animation
            animationFrameTimer.Stop();
            _duration = 0;
        }
    }

    private static void SendDMX()
    {
        byte[] bytes = new byte[ChannelList.Channels.Count];
        for (int i = 0; i < ChannelList.Channels.Count; i++)
        {
            //Just in case
            if (ChannelList.Channels[i].ChannelValue > 255) ChannelList.Channels[i].ChannelValue = 255;
            if (ChannelList.Channels[i].ChannelValue < 0) ChannelList.Channels[i].ChannelValue = 0;

            bytes[i] = ((byte)ChannelList.Channels[i].ChannelValue);

            Console.WriteLine("Send Channel " + (i + 1).ToString() + " DMX Value = " + ChannelList.Channels[i].ChannelValue.ToString() + "  ");
        }
        Send(bytes);
    }

    private static string[] Ports()
    {
        return SerialPort.GetPortNames();
    }

    private static void RecordChannels()
    {
        // Console.WriteLine("RecordChannels");
        for (int i = 0; i < ChannelList.Channels.Count; i++)
        {
            ChannelListTemp.Channels[i].ChannelValue = ChannelList.Channels[i].ChannelValue;
        }

    }

    private static void ReadChannels()
    {
        //Console.WriteLine("ReadChannels");
        for (int i = 0; i < ChannelListTemp.Channels.Count; i++)
        {
            ChannelList.Channels[i].ChannelValue = ChannelListTemp.Channels[i].ChannelValue;
        }
        SendDMX();
    }
}
