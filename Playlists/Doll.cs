namespace ZombieBaby.Playlists;

public static class Doll
{ 
    public static void Test()
    {
        Console.WriteLine("Playlists Test()");
        //Playlists.Eyes.Open();
        //Thread.Sleep(1000);
        //Movement.Body.UpSlow();
        Thread.Sleep(3000);
        Movement.Head.Right();
        //Thread.Sleep(500);
        //Playlists.Eyes.Blink(2);
        //Thread.Sleep(2500);
        //Movement.Head.Right();
        //Thread.Sleep(1000);
        //Playlists.Eyes.Blink(2);
        //Thread.Sleep(2000);
        //Movement.Head.Center();
        //Thread.Sleep(1000);
        //Playlists.Eyes.Blink(3);
        //Thread.Sleep(500);
        //Movement.Body.DownSlow();
        Thread.Sleep(3000);
        Movement.Head.Left();
        //Movement.Eyes.Closed();
    }
}
