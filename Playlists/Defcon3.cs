using ZombieBaby.Utilities;

namespace ZombieBaby.Playlists;

public static class Defcon3
{
    public static void Sleep1()
    {
        Console.WriteLine("Playlists Sleep1()");
        Playlists.Room.Strobe();
        Thread.Sleep(1000);    
       

    }
}
