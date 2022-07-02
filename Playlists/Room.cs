using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieBaby.Playlists;

public static class Room
{
    public static void Strobe(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Light.Room.Chan1WhiteOn();
            Thread.Sleep(100);
            Light.Room.Chan1WhiteOff();
            Thread.Sleep(100);
        }
    }
}
