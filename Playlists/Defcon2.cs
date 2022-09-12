using ZombieBaby.Animation;
using ZombieBaby.Utilities;

namespace ZombieBaby.Playlists;

public static class Defcon2
{
    public static void Awake()
    {
        Console.WriteLine("Playlists Defcon2 Awake");

        if (Status.CurrentStatus == 2)
        {
            AnimationPlayer.Play(AnimationPlayer.AnimationType.Awake);

            //Blink
            //Speak
            //Thread.Sleep(400);
        }

        //Blink Close Eyes
        //Playlists.Eyes.Closed();

    }
}
