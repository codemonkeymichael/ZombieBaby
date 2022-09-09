using ZombieBaby.Audio;
using ZombieBaby.Utilities;

namespace ZombieBaby.Playlists;

public static class Defcon3
{
    public static void Sleep()
    {
        Console.WriteLine("Playlists Defcon3 Sleep");

        //Movement.Body.Release();
        //Movement.Head.Right(); //Away from the crowd

        int iteration = 0;
        while (Status.CurrentStatus == 3)
        {
            //Thread sleepIn = new Thread(() => AudioPlayer.Play(AudioPlayer.AudioType.SleepingIn));
            //sleepIn.Start();
            //Movement.Body.SleepingBreath();

            AudioPlayer.Play(AudioPlayer.AudioType.SleepingIn);


            if (iteration == 15 || iteration == 8 || iteration == 2)
            {
                Thread.Sleep(500);
                //Thread dreamAudio = new Thread(() => AudioPlayer.Play(AudioPlayer.AudioType.Dreaming, 800));
                //dreamAudio.Start();          
                
                //Thread dreamHead = new Thread(() => Playlists.Head.Nightmare1(AudioPlayer.CurrentTrackDuration));
                //dreamHead.Start();

                AudioPlayer.Play(AudioPlayer.AudioType.Dreaming);
   
            }

            Thread.Sleep(900);
            //Thread fan = new Thread(() => Effects.Fan.OnOff(2000));
            //fan.Start();
            //Thread.Sleep(900);
            //Movement.Body.Release();
            AudioPlayer.Play(AudioPlayer.AudioType.SleepingOut);

            //if (iteration == 0)
            //{
            //    Movement.Body.SleepingBreath();
            //    Thread.Sleep(500);
            //    Movement.Head.LeftHalf();
            //    //Audio()
            //    Thread.Sleep(2050);
            //    Head.Center();
            //    Thread.Sleep(500);
            //    Head.LeftHalf();
            //    Thread.Sleep(500);
            //    Head.Center();
            //}
            iteration++;
            Thread.Sleep(3050);



            //Loop all this
            //Thread breathing 
            //Thread head moving
            //Thread eyes twitching
            //Thread in audio clips 
            //This should last 1 minute before it expires

        }
    }
}
