using ZombieBaby.Animation;
using ZombieBaby.Utilities;

namespace ZombieBaby.Playlists;

public static class Show
{
    public static void GunShot()
    {
        Console.WriteLine("Playlists Gun Shot");
        AnimationPlayer.Play(AnimationPlayer.AnimationType.GunShot);
        //Thread.Sleep(30000); //Hold here
    }

    //public static void Talk()
    //{
    //    Console.WriteLine("Playlists Defcon1 talk");
    //    AnimationPlayer.Play(AnimationPlayer.AnimationType.SittingUp);
    //    Thread.Sleep(30000); //Hold here 
    //}
}
