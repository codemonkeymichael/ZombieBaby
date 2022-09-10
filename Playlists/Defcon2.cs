using ZombieBaby.Utilities;

namespace ZombieBaby.Playlists;

public static class Defcon2
{
    public static void Awake()
    {
        Console.WriteLine("Playlists Defcon2 Awake");

        Movement.Carriage c = new Movement.Carriage();
        Thread rock = new Thread(() => c.Rock());
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
