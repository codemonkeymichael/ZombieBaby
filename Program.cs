using System.Timers;
using ZombieBaby.Light;
using ZombieBaby.Utilities;

namespace ZombieBaby;
class Program
{

    static void Main(string[] args)
    {
        //If the user stops the program
        Console.CancelKeyPress += new ConsoleCancelEventHandler(CloseHandler);

        Console.Clear();
        Console.WriteLine("Zombie Baby is Running Ver 0.5");

        DMXserial.connect();
        Gpios io = new Gpios(); //Just need to hit the constructor here
        Motor mo = new Motor(); //Just need to hit the constructor here

        //This thread keeps the whole program running and flickering.
        Thread flicker = new Thread(() => Ambient.Flicker());
        flicker.Start();

        Light.Ambient.GroundEffect();

       DMXChannels chanList = new DMXChannels();
        chanList.Channels = new List<DMXChannel>();
        for (int i = 0; i < 15; i++)
        {
            DMXChannel c = new DMXChannel();
            c.ChannelId = i + 1;
            c.ChannelValue = 255;
            c.TargetValue = 0;
            c.DMXStepsPerFrame = 1;
            c.FramesPerDMXStep = 1;
            c.DMXStepUP = false;
            chanList.Channels.Add(c);
        }

        DMXserial.ChannelList = chanList;


        System.Timers.Timer animation = new System.Timers.Timer();
        animation.Interval = 10; //25
        animation.Start();
        animation.Elapsed += new ElapsedEventHandler(DMXserial.AnimateChannels);
        DMXserial.SendDMX();

        //while (true)
        //{
        //    Movement.Carrage.Down();
        //    Thread.Sleep(2000);
        //    Movement.Carrage.Up();
        //    Thread.Sleep(2000);
        //}


        //Audio.Audio.Cue();
        //Audio.Audio.Play();

        //var inputLastState = PinValue.Low;    

        //while (true)
        //{
        //    var click = Gpios.piGPIOController.Read(Gpios.InputTrigger);
        //    if (click != inputLastState)
        //    {
        //        inputLastState = click;
        //        if (click == PinValue.High)
        //        {
        //            switch (Status.CurrentStatus)
        //            {
        //                case 0:                           
        //                    Thread action1 = new Thread(() => Playlists.Defcon3.Defcon3A());
        //                    action1.Start();                      
        //                    break;
        //                case 1:
        //                    Status.StopStatus1 = true;
        //                    Thread status2 = new Thread(() => Status.Status2(30));
        //                    status2.Start();
        //                    Thread action2 = new Thread(() => Playlists.Defcon2.Defcon2A());
        //                    action2.Start();
        //                    break;
        //                case 2:
        //                    Status.StopStatus2 = true;
        //                    Thread status3 = new Thread(() => Status.Status3(15));
        //                    status3.Start();
        //                    Thread action3 = new Thread(() => Playlists.Defcon1.SitUp());
        //                    action3.Start();
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //        Thread.Sleep(250);
        //    }
        //} 
    }

    private static void AnimateChannels(object? sender, ElapsedEventArgs e)
    {
        Console.WriteLine("Ping");
    }

    private static void CloseHandler(object? sender, ConsoleCancelEventArgs e)
    {
        Utilities.DMXserial.disconnect();
        Movement.Body.Release();
        Movement.Carriage.Release();
        Movement.Eyes.Release();
        Movement.Head.Release();
        Effects.Fan.Off();
        Console.WriteLine("Bye for now :)");
    }



    //static void CurrentDomain_ProcessExit(object sender, EventArgs e)
    //{
    //    //TODO: Make this work
    //    //Close a pins and stop playing music
    //    Console.WriteLine("I quit!");
    //}

}



