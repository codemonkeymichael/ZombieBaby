using ZombieBaby.Utilities;

namespace ZombieBaby.Playlists;

public static class Defcon2
{
    public static void Awake()
    {
        Console.WriteLine("Playlists Defcon2 Awake");

        Thread rock = new Thread(() => Movement.Carriage.Rock(3));
        rock.Start();
        Thread.Sleep(400);

        //Blink Open Eyes
        Playlists.Eyes.Open();

        Movement.Head.Center();

        while (Status.CurrentStatus == 2)
        {
            //Blink
            //Speak
            Thread.Sleep(400);
        }

        //Blink Close Eyes
        Playlists.Eyes.Closed();

    }
}
