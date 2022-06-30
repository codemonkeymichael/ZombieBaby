namespace ZombieBaby.Playlists;

public static class Smoke
{
    public static void Blow()
    {
        Thread smoke = new Thread(() => Effects.Smoke.OnOff());
        smoke.Start();
        Thread.Sleep(4000); //How long to smoke before turning on the fan
        Effects.Fan.OnOff();
    }

 
}
