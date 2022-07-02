using System;
using System.IO;
using System.IO.Ports;

namespace DMXtable
{
    class DMXserial
    {
        SerialPort port;
        byte[] message;

        public DMXserial()
        {
            port = new SerialPort("/dev/ttyUSB0", 115200);
        }

        public bool connect()
        {
            //Console.WriteLine("connect()");
            try
            {         
                port.Open();
                //Console.WriteLine("connected!");
                return true;              
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);       
            }
            return false;
        }

        public bool disconnect()
        {
            try
            {
                port.Close();
                port.Dispose();
                return true;
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        public bool send(byte[] byteArray)
        {
            if (port == null || !port.IsOpen)
                return false;

            int len = byteArray.Length;

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

        public string[] ports()
        {
            return SerialPort.GetPortNames();
        }

        public bool isConnected
        {
            get { return port.IsOpen; }
        }
    }
}
