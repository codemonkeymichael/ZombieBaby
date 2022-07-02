using System;
using System.IO;
using System.IO.Ports;

namespace ZombieBaby.Utilities;

public static class DMXserial
{
    private static SerialPort port = new SerialPort("/dev/ttyUSB0", 115200);
    private static byte[] message;

    public static bool isConnected  {  get { return port.IsOpen; } }

    public static bool connect()
    {
        //Console.WriteLine("connect()");
        try
        {
            port.Open();
            Console.WriteLine("connected!");
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
            Console.WriteLine("disconnect!");
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
        //disconnect();

        return true;
    }

    public static void SetDMX(int startChan, int red, int green, int blue)
    {
        Console.WriteLine($"Set DMX chan={startChan} red {red}");

        byte[] bytes = new byte[512];
        for (int i = 0; i < 512; i++)
        {
            bytes[i] = ((byte)red);
        }
        var status = DMXserial.send(bytes);
        Console.WriteLine("send status " + status);
    }

    public static string[] ports()
    {
        return SerialPort.GetPortNames();
    }

}
