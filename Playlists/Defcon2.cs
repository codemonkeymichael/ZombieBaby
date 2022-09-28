using ZombieBaby.Animation;
using ZombieBaby.Utilities;

namespace ZombieBaby.Playlists;

public static class Defcon2
{
    public static void Awake()
    {
        Console.WriteLine("Playlists Defcon2 Awake");
        AnimationPlayer.Play(AnimationPlayer.AnimationType.Awake);
        Thread.Sleep(15000);
    }
}
