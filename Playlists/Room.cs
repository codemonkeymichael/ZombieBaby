using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieBaby.Utilities;

namespace ZombieBaby.Playlists;

public static class Room
{
    public static void Strobe(int count = 1, int duration = 250)
    {
        Console.WriteLine("Playlists Strobe");
        DMX.RecordChannels();
        for (int i = 0; i < count; i++)
        {
            Light.Room.All(255,0);
            Thread.Sleep(duration);
            DMX.ReadChannels();
            Thread.Sleep(duration);
        }
    }

    public static void WaveAll(int count = 2, int duration = 5000)
    {
        Console.WriteLine("Playlists WaveAll");
        DMX.RecordChannels();
        for (int i = 0; i < count; i++)
        {
            Light.Room.All(255, duration);
            Thread.Sleep(duration/2);
            DMX.ReadChannels(duration);
            Thread.Sleep(duration/2);
        }
    }

    public static void Stage1(int count = 2, int duration = 5000)
    {
        Console.WriteLine("Playlists Stage1");
     
     
    }
}
