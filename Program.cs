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

        DMX.Connect();

        Gpios io = new Gpios(); //Just need to hit the constructor here TODO Make this static
        Motor mo = new Motor(); //Just need to hit the constructor here TODO Make this static

        //This thread keeps the whole program running and flickering.
        Thread flicker = new Thread(() => Ambient.Flicker());
        flicker.Start();

        //Light.Ambient.GroundEffect();

        //int value = 0;

        //while (true)
        //{
        //if (value == 0) { value = 255; } else { value = 0; };

        //List<DMXChan> chanList = new List<DMXChan>();
        //for (int i = 0; i < 15; i++)
        //{
        //    DMXChan chan = new DMXChan();
        //    chan.ChannelValue = 255;
        //    chan.ChannelId = i;
        //    chanList.Add(chan);
        //}
        //DMX.Update(chanList, 1000);
        //Thread.Sleep(2000);
        //}


        //while (true)
        //{
        //    Movement.Carrage.Down();
        //    Thread.Sleep(2000);
        //    Movement.Carrage.Up();
        //    Thread.Sleep(2000);
        //}


        //Audio.Audio.Cue();
        //Audio.Audio.Play();

        var inputLastState = PinValue.Low;

        while (true)
        {
            var click = Gpios.piGPIOController.Read(Gpios.InputTrigger);
            if (click != inputLastState)
            {
                inputLastState = click;
                if (click == PinValue.High)
                {
                    switch (Status.CurrentStatus)
                    {
                        case 0:
                            Thread action1 = new Thread(() => Playlists.Defcon3.Defcon3A());
                            action1.Start();
                            break;
                        case 1:
                            Status.StopStatus1 = true;
                            Thread status2 = new Thread(() => Status.Status2(30));
                            status2.Start();
                            Thread action2 = new Thread(() => Playlists.Defcon2.Defcon2A());
                            action2.Start();
                            break;
                        case 2:
                            Status.StopStatus2 = true;
                            Thread status3 = new Thread(() => Status.Status3(15));
                            status3.Start();
                            Thread action3 = new Thread(() => Playlists.Defcon1.SitUp());
                            action3.Start();
                            break;
                        default:
                            break;
                    }
                }
                Thread.Sleep(250);
            }
        }
    }

    private static void CloseHandler(object? sender, ConsoleCancelEventArgs e)
    {
        Console.WriteLine("");
        Utilities.DMX.Disconnect();
        Movement.Body.Release();
        Movement.Carriage.Release();
        Movement.Eyes.Release();
        Movement.Head.Release();
        Effects.Fan.Off();
        Console.WriteLine("Bye for now :)");
    }
}