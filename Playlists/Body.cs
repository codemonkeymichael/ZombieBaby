namespace ZombieBaby.Playlists;

public static class Body
{
    public static void SitUpAndScream()
    {
        Console.WriteLine("Playlists SitUp()");

        Thread blowSmoke = new Thread(() => Playlists.Smoke.BlowBlinders());
        blowSmoke.Start();
        Playlists.Eyes.Open();
        Thread.Sleep(4250); //Waiting for smoke to blow
        Movement.Body.UpFastEaseOut();
        Thread.Sleep(3000);
        Movement.Head.Right();
        Thread.Sleep(200);
        Playlists.Eyes.Blink(2);
        Thread.Sleep(3000);
        Movement.Head.Left();
        Thread.Sleep(200);
        Playlists.Eyes.Blink(2);
        Thread.Sleep(3000);
        Movement.Head.Center();
        Playlists.Eyes.Blink(3);
        Movement.Body.DownEaseBoth();
    }

    public static void SitUpALittleBit()
    {
        Console.WriteLine("Playlists SitUpALittleBit()");
        Movement.Body.Release();
        Thread.Sleep(150);
        Movement.Body.UpALittleSlow();
    }
}
