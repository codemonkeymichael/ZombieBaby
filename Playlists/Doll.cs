namespace ZombieBaby.Playlists;

public static class Doll
{ 
    public static void SitUpDownDemo()
    {
        Console.WriteLine("Playlists SitUpDownDemo()");
        Playlists.Eyes.Open();
        Thread.Sleep(1000);
        Movement.Body.UpSlow();
        Thread.Sleep(1000);
        Movement.Head.Right();
        Thread.Sleep(500);
        Playlists.Eyes.Blink(2);
        Thread.Sleep(2500);
        Movement.Head.Right();
        Thread.Sleep(1000);
        Playlists.Eyes.Blink(2);
        Thread.Sleep(2000);
        Movement.Head.Center();
        Thread.Sleep(1000);
        Playlists.Eyes.Blink(3);
        Thread.Sleep(500);
        Movement.Body.DownSlow();
        Thread.Sleep(3000);
        Movement.Eyes.Closed();
    }
}
