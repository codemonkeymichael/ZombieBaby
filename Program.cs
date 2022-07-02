using ZombieBaby.Light;
using ZombieBaby.Movement;
using ZombieBaby.Utilities;

namespace ZombieBaby;
class Program
{

    static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Zombie Baby is Running Ver 0.5");

        DMXserial.connect();
        Gpios io = new Gpios(); //Just need to hit the constructor here
        Motor mo = new Motor(); //Just need to hit the constructor here

        //This thread keeps the whole program running and flickering.
        Thread flicker = new Thread(() => Ambient.Flicker());
        flicker.Start();

        //Audio.Audio.Cue();
        //Audio.Audio.Play();

        var inputLastState = PinValue.Low;
        bool up = false;     
        
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
                            Light.Status.StopStatus1 = false;
                            Thread status1 = new Thread(() => Light.Status.Status1());
                            status1.Start();
                            Thread action1 = new Thread(() => Playlists.Defcon3.Sleep1());
                            action1.Start();                      
                            break;
                        case 1:
                            Light.Status.StopStatus1 = true;
                            Thread status2 = new Thread(() => Light.Status.Status2());
                            status2.Start();
                            Thread action2 = new Thread(() => Playlists.Defcon2.Awake1());
                            action2.Start();
                            break;
                        case 2:
                            Light.Status.StopStatus2 = true;
                            Thread status3 = new Thread(() => Light.Status.Status3());
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



    //static void CurrentDomain_ProcessExit(object sender, EventArgs e)
    //{
    //    //TODO: Make this work
    //    //Close a pins and stop playing music
    //    Console.WriteLine("I quit!");
    //}
}



