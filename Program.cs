using ZombieBaby.Light;
using ZombieBaby.Movement;
using ZombieBaby.Utilities;

namespace ZombieBaby;
public static class Program
{

    public static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Zombie Baby is Running Ver 0.5");

        Gpios io = new Gpios(); //Just need to hit the constructor here
        Motor mo = new Motor(); //Just need to hit the constructor here

        //This thread keeps the whole program running and flickering.
        Thread flicker = new Thread(() => Ambient.Flicker());
        flicker.Start();


        Playlists.Doll.Test();


        //Audio.Audio.Cue();
        //Audio.Audio.Play();



        //var inputLastState = PinValue.Low;
        //bool up = false;
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
        //                    Thread status1 = new Thread(() => Light.Status.Status1());
        //                    status1.Start();
        //                    Playlists.Blinders.Flash();
        //                    break;
        //                case 1:
        //                    Thread status2 = new Thread(() => Light.Status.Status2());
        //                    status2.Start();
        //                    Playlists.Smoke.Blow();
        //                    break;
        //                case 2:
        //                    Thread status3 = new Thread(() => Light.Status.Status3());
        //                    status3.Start();
        //                    Playlists.Doll.SitUpDownDemo();
        //                    break;
        //                default:
        //                    break;
        //            }               
        //        }
        //        Thread.Sleep(1000);
        //    }
        //}


        //static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        //{
        //    //TODO: Make this work
        //    //Close a pins and stop playing music
        //    Console.WriteLine("I quit!");
        //}
    }
}



