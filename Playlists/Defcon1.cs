using ZombieBaby.Utilities;

namespace ZombieBaby.Playlists;

public static class Defcon1
{ 
    public static void SitUp()
    {
        Console.WriteLine("Playlists Awake1()");
        Movement.Body.Release();
        Thread blowSmoke = new Thread(() => Playlists.Smoke.BlowBlinders());
        blowSmoke.Start();
        Thread.Sleep(1500);
        Playlists.Eyes.Open();
        Thread.Sleep(3000);
        Movement.Body.UpFastEaseOut();
        Thread.Sleep(1000);
        //DMX.ChannelList.Channels[5].ChannelValue = 30;
        ///DMX.ChannelList.Channels[6].ChannelValue = 15;
        ///DMX.ChannelList.Channels[7].ChannelValue = 25;
        //DMX.SendDMX();
        Thread.Sleep(2000);
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
        //DMX.ChannelList.Channels[5].ChannelValue = 0;
        //DMX.ChannelList.Channels[6].ChannelValue = 0;
        //DMX.ChannelList.Channels[7].ChannelValue = 0;
        //DMX.SendDMX();
        Movement.Body.DownEaseBoth();
        Thread.Sleep(2000);
        Playlists.Eyes.Closed();
        Movement.Head.Release();
        Light.Ambient.GroundEffect(1);
    }
}
