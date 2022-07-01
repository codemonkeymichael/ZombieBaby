namespace ZombieBaby.Playlists;

public static class Doll
{ 
    public static void Test()
    {
        Console.WriteLine("Playlists Test()");
        //Playlists.Eyes.Open();
        //Thread.Sleep(1000);
        Movement.Body.UpFast();
        //Thread.Sleep(3000);
        //Movement.Head.Right();
        //Thread.Sleep(200);
        //Playlists.Eyes.Blink(2);
        //Thread.Sleep(3000);
        //Movement.Head.Left();
        //Thread.Sleep(200);
        //Playlists.Eyes.Blink(2);
        Thread.Sleep(3000);
        //Movement.Head.Center();
        //Playlists.Eyes.Blink(3);
        Movement.Body.DownSlow();
    }
}
