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
        Console.WriteLine("Playlist Strobe");
        //DMX.RecordChannels();
        for (int i = 0; i < count; i++)
        {
            Light.Room.AllOn();
            Thread.Sleep(duration);
            //DMX.ReadChannels();
            Thread.Sleep(duration);
        }
    }
}
