namespace ZombieBaby.Playlists;

public class Body
{
    private readonly Movement.Body movementBody = new Movement.Body();

    public void SitUp()
    {
        Console.WriteLine("Playlists SitUp()");
        //Movement.Body.Release();


        //Thread blowSmoke = new Thread(() => Playlists.Smoke.BlowBlinders());
        //blowSmoke.Start();
        //Playlists.Eyes.Open();
        //Thread.Sleep(1000);
        //Movement.Body.UpFastEaseOut();
        //Thread.Sleep(3000);
        //Movement.Head.Right();
        //Thread.Sleep(200);
        //Playlists.Eyes.Blink(2);
        //Thread.Sleep(3000);
        //Movement.Head.Left();
        //Thread.Sleep(200);
        //Playlists.Eyes.Blink(2);
        //Thread.Sleep(3000);
        //Movement.Head.Center();
        //Playlists.Eyes.Blink(3);
        //Movement.Body.DownEaseBoth();
    }

    public void SitUpALittleBit()
    {
        Console.WriteLine("Playlists SitUpALittleBit()");
        movementBody.Release();
        Thread.Sleep(150);
        movementBody.UpALitleSlow();



    }
}
