namespace ZombieBaby.Playlists;

public static class Smoke
{
    public static void Blow()
    {
        Thread smoke = new Thread(() => Effects.Smoke.OnOff());
        smoke.Start();
        Thread.Sleep(3000); //How long to smoke before turning on the fan
        Effects.Fan.OnOff(15000);
    }

    public static void BlowBlinders()
    {
        Thread smoke = new Thread(() => Effects.Smoke.OnOff());
        smoke.Start();
        Thread.Sleep(3000); //How long to smoke before turning on the fan
        Thread fan = new Thread(() => Effects.Fan.OnOff(15000));
        fan.Start();
        Thread.Sleep(2000);
        Thread blinders = new Thread(() => Light.Blinders.OnOff());
        blinders.Start();
        Thread.Sleep(800);
        Playlists.Room.Strobe();
        Thread.Sleep(500);
        Light.Blinders.OnOff();
        Playlists.Room.Strobe();
    }
}
