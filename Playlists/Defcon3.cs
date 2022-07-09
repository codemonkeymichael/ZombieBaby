using ZombieBaby.Utilities;

namespace ZombieBaby.Playlists;

public static class Defcon3
{
    public static void Defcon3A()
    {
        Console.WriteLine("Playlists Defcon3A()");

        Status.StopStatus1 = false;
        Status.CurrentStatus = 1;
        Thread status1 = new Thread(() => Status.Status1(60));
        status1.Start();
        Thread brething = new Thread(() => Movement.Body.Breathing(1));
        brething.Start();

        //Loop all this
        //Thread breathing 
        //Thread head moving
        //Thread eyes twitching
        //Thread in audio clips 
        //This should last 1 minute before it expires

    }
}
