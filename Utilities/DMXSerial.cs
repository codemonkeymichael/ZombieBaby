using System;
using System.IO;
using System.IO.Ports;
using System.Timers;

namespace ZombieBaby.Utilities;

public static class DMXserial
{
    private static SerialPort port = new SerialPort("/dev/ttyUSB0", 115200);
    private static byte[] message;
    public static DMXChannels ChannelList = new DMXChannels();
    private static int duration = 0;
    private static System.Timers.Timer animation = new System.Timers.Timer();

    public static bool connect()
    {
        //Instantiate the channel model
        ChannelList.Channels = new List<DMXChannel>();
        for (int i = 0; i <= 100; i++)
        {
            DMXChannel c = new DMXChannel();
            c.ChannelId = i;
            c.ChannelValue = 0;
            c.TargetValue = 0;
            c.DMXStepsPerFrame = 0;
            c.FramesPerDMXStep = 0;
            c.DMXStepUP = true;
            ChannelList.Channels.Add(c);
        }

        try
        {
            Console.WriteLine("port.IsOpen" + port.IsOpen);
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
            Console.WriteLine(ex);       
        }    

        return false;
    }

    public static bool disconnect()
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

    public static bool send(byte[] byteArray)
    {
        if (port == null || !port.IsOpen)
            return false;

        int len = byteArray.Length;
        //Console.WriteLine("Send bytes " + len);

        if (len > 0)
        {
            message = new byte[len+6];
            message[0] = 0x7E;
            message[1] = 0x06;
            message[2] = (byte)((len+1) & 0xFF);
            message[3] = (byte)((len >> 8) & 0xFF);
            message[4] = 0x00;
            byteArray.CopyTo(message, 5);
            message[len+5] = 0xE7;

            port.Write(message, 0, message.Length);   
        }    
        return true;
    }

    public static void AnimateChannels(object sender, ElapsedEventArgs args)
    {
        //Console.SetCursorPosition(0, 15);
        //Console.ForegroundColor = ConsoleColor.Red;
        //Console.WriteLine(" Animation Status  ");
        //Console.ForegroundColor = ConsoleColor.Gray;

        //Console.SetCursorPosition(0, 30);
        //Console.ForegroundColor = ConsoleColor.Gray;
        //Console.WriteLine(" duration = " + duration.ToString());  

        if (duration > 0)
        {
            duration--;

            //Console.WriteLine("Duration = " + duration);

            for (var i = 0; i < ChannelList.Channels.Count(); i++)
            {
                //int xPos = i * 22;
                //Console.SetCursorPosition(xPos, 16);
                //Console.WriteLine("         Value = " + ChannelList.Channels[i].ChannelValue + "                  ");
                //Console.SetCursorPosition(xPos, 17);
                //Console.WriteLine("      Duration = " + duration + "                   ");
                //Console.SetCursorPosition(xPos, 18);
                //Console.WriteLine(" DMX Per Frame = " + ChannelList.Channels[i].DMXStepsPerFrame + "            ");
                //Console.SetCursorPosition(xPos, 19);
                //Console.WriteLine(" Frame Per DMX = " + ChannelList.Channels[i].FramesPerDMXStep + "               ");
                //Console.SetCursorPosition(xPos, 20);
                //Console.WriteLine("        DMX UP = " + ChannelList.Channels[i].DMXStepUP + "             ");

                var DMXRemain = ChannelList.Channels[i].TargetValue - ChannelList.Channels[i].ChannelValue;

                //Console.SetCursorPosition(xPos, 21);
                //Console.WriteLine("    DMX Remain = " + DMXRemain + "              ");

                if (DMXRemain != 0)
                {
                    //More frames than DMX steps. Go slow. One DMX step every few frames.              
                    if (ChannelList.Channels[i].FramesPerDMXStep != 0)
                    {
                        if (ChannelList.Channels[i].FramesPerDMXStep == 1)
                        {
                            if (ChannelList.Channels[i].DMXStepUP)
                            {
                                ChannelList.Channels[i].ChannelValue++;
                            }
                            else
                            {
                                ChannelList.Channels[i].ChannelValue--;
                            }
                            if (duration > 0 && DMXRemain != 0)
                            {
                                //Recalculate the frames to make the target DMX channel value 
                                ChannelList.Channels[i].FramesPerDMXStep = duration / Math.Abs(DMXRemain);
                            }
                        }
                        else
                        {
                            ChannelList.Channels[i].FramesPerDMXStep--;
                        }
                    }

                    //More DMX steps than frames. Go fast. One or many DMX steps per frame.
                    if (ChannelList.Channels[i].DMXStepsPerFrame != 0)
                    {
                        var chanVal = ChannelList.Channels[i].DMXStepsPerFrame + ChannelList.Channels[i].ChannelValue;

                        ChannelList.Channels[i].ChannelValue = chanVal;

                        if (duration > 0 && DMXRemain > 0)
                        {
                            //Recalculate to make the target channel value                   
                            ChannelList.Channels[i].DMXStepsPerFrame = Math.Abs(DMXRemain) / duration;
                            if (!ChannelList.Channels[i].DMXStepUP)
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
                //Console.WriteLine("      Duration = " + duration + "                   ");
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
            animation.Stop();
            duration = 0;
        }
    }

    public static void SendDMX()
    {
        byte[] bytes = new byte[ChannelList.Channels.Count];
        for (int i = 0; i < ChannelList.Channels.Count; i++)
        {
            //Just in case
            if (ChannelList.Channels[i].ChannelValue > 255) ChannelList.Channels[i].ChannelValue = 255;
            if (ChannelList.Channels[i].ChannelValue < 0) ChannelList.Channels[i].ChannelValue = 0;

            bytes[i] = ((byte)ChannelList.Channels[i].ChannelValue);
                        
            //Console.WriteLine("Send Channel " + (i + 1).ToString() + " DMX Value = " + ChannelList.Channels[i].ChannelValue.ToString() + "  ");
        }
        send(bytes);
    }

    public static string[] ports()
    {
        return SerialPort.GetPortNames();
    }

}
