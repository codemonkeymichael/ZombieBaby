using ZombieBaby.Utilities;

namespace ZombieBaby.Playlists;

public static class Defcon2
{ 
    public static void Defcon2A()
    {


        Console.WriteLine("Playlists Awake1()");

        Status.StopStatus2 = false;
        Status.CurrentStatus = 2;
        Thread status2 = new Thread(() => Status.Status2(30));
        status2.Start();



        Playlists.Eyes.Open();
        Movement.Carriage.Up();
        Thread.Sleep(5000);
        Movement.Carriage.Down();
        Thread.Sleep(5000);
        Movement.Carriage.Release();
        Thread.Sleep(5000);
        Movement.Carriage.Up();
        Thread.Sleep(5000);
        Movement.Carriage.Down();
        Thread.Sleep(5000);
        Movement.Carriage.Release();
        Thread.Sleep(5000);
        Movement.Carriage.Up();
        Thread.Sleep(5000);
        Movement.Carriage.Down();
        Thread.Sleep(5000);
        Movement.Carriage.Release();
        Thread.Sleep(5000);
        Movement.Carriage.Up();
        Thread.Sleep(5000);
        Movement.Carriage.Down();
        Thread.Sleep(5000);
        Movement.Carriage.Release();
        Thread.Sleep(5000);
    }
}
