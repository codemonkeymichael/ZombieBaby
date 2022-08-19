using ZombieBaby.Utilities;

namespace ZombieBaby.Playlists;

public static class Defcon3
{
    public static void Sleep()
    {
        Console.WriteLine("Playlists Defcon3 Sleep");

        Movement.Body.Release();
        Movement.Head.LeftHalf(); //Away from the crowd

        int iteration = 0;
        while (Status.CurrentStatus == 3)
        {
            Thread sleepIn = new Thread(() => Audio.Audio.Play(Audio.Audio.AudioType.SleepingIn));
            sleepIn.Start();
            Movement.Body.SleepingBreath();

            if(iteration == 15 || iteration == 8 || iteration == 2 )
            {
                Movement.Head.Left();
                Thread.Sleep(600);               
                Movement.Head.RightHalf();
                Thread.Sleep(200);
                Thread dream = new Thread(() => Audio.Audio.Play(Audio.Audio.AudioType.Dreaming));
                dream.Start();
                Thread.Sleep(200);
                Movement.Head.Center();
                Thread.Sleep(400);
                Movement.Head.Right();
                Thread.Sleep(800);
                Movement.Head.LeftHalf();
            }

            Thread.Sleep(400);
            Thread fan = new Thread(() => Effects.Fan.OnOff(2000));
            fan.Start();
            Thread.Sleep(900);
            Movement.Body.Release();
            Audio.Audio.Play(Audio.Audio.AudioType.SleepingOut);

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
