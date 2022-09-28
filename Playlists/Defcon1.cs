using ZombieBaby.Animation;
using ZombieBaby.Utilities;

namespace ZombieBaby.Playlists;

public static class Defcon1
{
    public static void SitUp()
    {
        Console.WriteLine("Playlists Defcon1 sit-up and scream");
        AnimationPlayer.Play(AnimationPlayer.AnimationType.Screaming);    
        AnimationPlayer.Play(AnimationPlayer.AnimationType.SittingUp);
        Thread.Sleep(10000); //Hold here

    }

    public static void Talk()
    {
        Console.WriteLine("Playlists Defcon1 talk");
        AnimationPlayer.Play(AnimationPlayer.AnimationType.SittingUp);
        Thread.Sleep(20000); //Hold here 
    }
}
