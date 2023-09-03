using ZombieBaby.Animation;
using ZombieBaby.Utilities;

namespace ZombieBaby.Playlists;

public static class Defcon3
{
    public static void Sleep()
    {
        Console.WriteLine("Playlists Defcon3 Sleep");
        Movement.Body.Release();       
        Movement.Head.Right(); //Away from the crowd
        if (Status.PreviousStatus == 2)
        {                 
            Playlists.Eyes.BlinkClosed();
        }

        int iteration = 0;
        while (Status.CurrentStatus == 3)
        {
            iteration++;

            AnimationPlayer.Play(AnimationPlayer.AnimationType.SleepingIn);
            Console.WriteLine("Sleep() Iteration " + iteration);
            if (iteration == 15 || iteration == 8 || iteration == 2)
            {
                Thread.Sleep(500);
                Console.WriteLine("I'm having a bad dream!");
                AnimationPlayer.Play(AnimationPlayer.AnimationType.Dreaming);
            }

            Thread.Sleep(200);
            AnimationPlayer.Play(AnimationPlayer.AnimationType.SleepingOut);


            Thread.Sleep(2050);

            if (iteration > 17) break;

        }
    }
}
